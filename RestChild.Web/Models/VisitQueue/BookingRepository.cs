using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Castle.Core.Logging;
using RestChild.Booking.Logic.Logic;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.MPGUIntegration;
using RestChild.Web.Extensions;
using RestChild.Web.Logic;

namespace RestChild.Web.Models.VisitQueue
{
    /// <summary>
    ///     Репозиторий работы с онлайн очередью на приём
    /// </summary>
    public class BookingRepository : ILogic
    {
        public BookingRepository(IUnitOfWork unitOfWork, ILogger logger)
        {
            UnitOfWork = unitOfWork;
            Logger = logger;
        }

        public ILogger Logger { get; set; }

        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        ///     Техническая проверка существования активной брони с таким же СНИЛС
        /// </summary>
        internal static bool PrebookingSnilsCheck(IUnitOfWork unitOfWork, string snils, long? DepartId)
        {
            var q = unitOfWork.GetSet<MGTBookingVisit>().Where(ss => ss.VisitCell >= DateTime.Now).AsQueryable();
            q = q.Where(ss =>
                (ss.StatusId == (long)MGTVisitBookingStatuses.PrebookingRegistered
                || ss.StatusId == (long)MGTVisitBookingStatuses.BookingRegistered
                //|| ss.StatusId == (long)MGTVisitBookingStatuses.BookingVisited
                //|| ss.StatusId == (long)MGTVisitBookingStatuses.BookingUnvisited
                )
                && ss.DepartmentId == DepartId
            );
            q = q.Where(ss => ss.Persons.Any(sx => sx.Snils.ToLower() == snils.ToLower()));
            return q.Any();
        }

        /// <summary>
        ///     Проверка существования активной брони с таким же СНИЛС
        /// </summary>
        public bool PrebookingSnilsCheck(string snils, long departId = 2)
        {
            return PrebookingSnilsCheck(UnitOfWork, snils, departId);
        }

        /// <summary>
        ///     Получить сетку бронирования
        /// </summary>
        public IEnumerable<BookingVisitGrid> GetVisitGrid(BookingVisitGridFilter filter)
        {
            var result = new List<BookingVisitGrid>();
            var d1 = filter.DateFrom.Date;
            if (d1 < DateTime.Now)
            {
                d1 = DateTime.Now.Date;
            }
            var departId = UnitOfWork.GetSet<MGTVisitTarget>().Where(vt => vt.Id == filter.VisitTargetId).FirstOrDefault().DepartmentId;
            var q = UnitOfWork.GetSet<MGTWorkingDay>().Where(ss => ss.Date >= d1 && !ss.IsDeleted && ss.DepartmentId == departId).AsQueryable();

            if (filter.DateTo.HasValue)
            {
                //максимальное число дней в выгрузке сетки рабочих дней
                var days = 15;
                var d2 = filter.DateTo.Value.Date;
                if (d2 < d1)
                {
                    d2 = d1;
                }
                else if (d1.AddDays(days) < d2)
                {
                    d2 = d1.AddDays(days);
                }

                q = q.Where(ss => ss.Date <= d2);
            }
            else
            {
                q = q.Where(ss => ss.Date <= d1);
            }

            q = q.Where(ss =>
                ss.Windows.Any(sx => !sx.IsCanceled && sx.Targets.Any(sy => sy.Id == filter.VisitTargetId)));

            q = q.OrderBy(ss => ss.Date);

            //существуют ли рабочие дни вообще
            if (!q.Any())
            {
                return new List<BookingVisitGrid>();
            }

            foreach (var day in q.ToList())
            {
                var bvg = new BookingVisitGrid { Date = day.Date.Date, Cells = new List<DateTime>() };
                var windows = day.Windows.Where(sx =>
                    !sx.IsCanceled && sx.Targets.Any(ss => ss.Id == filter.VisitTargetId)).ToArray();
                var min = windows.SelectMany(ss => ss.WorkingPeriods).Min(ss => ss.TimeFrom);
                var max = windows.SelectMany(ss => ss.WorkingPeriods).Max(ss => ss.TimeTo);
                for (var t = min; t <= max; t = t.AddMinutes(day.WorkingInterval))
                {
                    if (t <= DateTime.Now)
                    {
                        continue;
                    }

                    if (!windows.SelectMany(ss => ss.WorkingPeriods).Any(ss => ss.TimeFrom <= t && ss.TimeTo > t))
                    {
                        continue;
                    }

                    var hasWindow = CheckAvailabilityCell(day, filter.VisitTargetId, t);

                    if (hasWindow)
                    {
                        if (filter.VisitSlotsCount > 1)
                        {
                            bool hasLongWindow;
                            var i = 1;
                            var nd = new DateTime(t.Ticks);
                            nd = nd.AddMinutes(day.WorkingInterval);
                            do
                            {
                                hasLongWindow = CheckAvailabilityCell(day, filter.VisitTargetId, nd);
                                nd = nd.AddMinutes(day.WorkingInterval);
                                i++;
                            } while (hasLongWindow && i < filter.VisitSlotsCount);

                            if (hasLongWindow)
                            {
                                bvg.Cells.Add(t);
                            }
                        }
                        else
                        {
                            bvg.Cells.Add(t);
                        }
                    }
                }

                result.Add(bvg);
            }

            return result;
        }

        /// <summary>
        ///     Проверка свободности окна записи
        /// </summary>
        private bool CheckAvailabilityCell(MGTWorkingDay day, long visitTargetId, DateTime dd)
        {
            return CheckAvailabilityCell(UnitOfWork, day, visitTargetId, dd);
        }

        /// <summary>
        ///     Проверка свободности ячейки записи
        /// </summary>
        internal static bool CheckAvailabilityCell(IUnitOfWork unitOfWork, MGTWorkingDay day, long visitTargetId, DateTime dd, long ignoreBookingId = 0)
        {
            //проверка на наличие хотя бы одного окна с нужной целью
            if (!day.Windows.Any(ss => !ss.IsCanceled && ss.Targets.Any(sx => sx.Id == visitTargetId)))
            {
                return false;
            }

            //общее кол-во активных (не отменённых) броней на данное время
            var bookingsQuery = unitOfWork.GetSet<MGTBookingVisit>().Where(ss =>
                (
                    (ss.StatusId == (long)MGTVisitBookingStatuses.PrebookingRegistered
                    || ss.StatusId == (long)MGTVisitBookingStatuses.BookingRegistered
                    || ss.StatusId == (long)MGTVisitBookingStatuses.BookingVisited
                    || ss.StatusId == (long)MGTVisitBookingStatuses.BookingUnvisited)
                    && ss.DepartmentId == day.DepartmentId
                )
                && ss.VisitCell == dd).AsQueryable();
            if (ignoreBookingId > 0)
            {
                bookingsQuery = bookingsQuery.Where(ss => ss.Id != ignoreBookingId && ss.ParrentId != ignoreBookingId);
            }

            var bookings = bookingsQuery.GroupBy(sx => sx.TargetId).Select(ss => new { ss.Key, Count = ss.Count() })
                .ToDictionary(ss => ss.Key);

            var singleWindows = day.Windows.Where(ss =>
                    !ss.IsCanceled
                    && ss.Targets.Count == 1
                    && ss.WorkingPeriods.Any(sx => sx.TimeFrom <= dd && sx.TimeTo > dd))
                .GroupBy(sx => sx.Targets.First().Id).Select(sx => new { Key = (long?)sx.Key, Count = sx.Count() })
                .ToDictionary(ss => ss.Key);


            //первый этап
            if (singleWindows.ContainsKey(visitTargetId))
            {
                if (!bookings.ContainsKey(visitTargetId) ||
                    bookings[visitTargetId].Count < singleWindows[visitTargetId].Count)
                {
                    return true;
                }
            }

            //второй этап (сложный) на будущее
            {
            }

            //третий этап
            {
                var multiWindows = day.Windows.Count(ss =>
                    !ss.IsCanceled
                    && ss.Targets.Count > 1
                    && ss.WorkingPeriods.Any(sx => sx.TimeFrom <= dd && sx.TimeTo > dd));

                if (multiWindows <= 0)
                {
                    return false;
                }

                foreach (var bookingTarget in bookings.Keys)
                {
                    if (singleWindows.ContainsKey(bookingTarget ?? 0))
                    {
                        var overSingleWindow =
                            singleWindows[bookingTarget ?? 0].Count - bookings[bookingTarget ?? 0].Count;
                        if (overSingleWindow < 0)
                        {
                            multiWindows += overSingleWindow;
                        }
                    }
                    else
                    {
                        multiWindows -= bookings[bookingTarget ?? 0].Count;
                    }

                    if (multiWindows <= 0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        ///     Техническое создание пред брони
        /// </summary>
        internal static BookingResult Prebooking(IUnitOfWork unitOfWork, Booking booking, bool mpgu, long departId, ILogger logger = null)
        {
            try
            {
                if (PrebookingSnilsCheck(unitOfWork, booking.SNILS, departId))
                {
                    return new BookingResult
                    {
                        Code = (long)MGTVisitBookingPrebookingStatuses.PrebookingExists,
                        Messeage = "Пребронь на данного человека уже существует"
                    };
                }
                if (unitOfWork.GetSet<MGTBookingVisit>()
                    .Any(d => (d.StatusId == (long)MGTVisitBookingStatuses.PrebookingRegistered
                    || d.StatusId == (long)MGTVisitBookingStatuses.BookingRegistered
                    //|| d.StatusId == (long)MGTVisitBookingStatuses.BookingVisited
                    //|| d.StatusId == (long)MGTVisitBookingStatuses.BookingUnvisited
                    )
                    && d.VisitCell == booking.VisitSlot && d.Persons.Any(s => s.Snils == booking.SNILS)))
                {
                    return new BookingResult
                    {
                        Code = (long)MGTVisitBookingPrebookingStatuses.PrebookingExists,
                        Messeage = "Вы уже записались на приём в это время"
                    };
                }
                if (booking.VisitSlot < DateTime.Now)
                {
                    throw new Exception("нельзя забронировать слот в прошлом");
                }

                var currentVisitSlot = new DateTime(booking.VisitSlot.Ticks);
                var currentVisitSlotDay = new DateTime(booking.VisitSlot.Date.Ticks);

                var day = unitOfWork.GetSet<MGTWorkingDay>()
                    .FirstOrDefault(d => !d.IsDeleted && d.Date == currentVisitSlotDay && d.DepartmentId == departId);
                if (day == null)
                {
                    throw new Exception("рабочий день не найден");
                }

                using (var scope = unitOfWork.GetTransactionScope())
                {
                    var bookings = new List<MGTBookingVisit>();
                    //проверяем занятость для каждого слота в цепочке и если слот свободен создаём запись
                    for (var i = 0; i < booking.VisitSlotsCount; i++)
                    {
                        if (CheckAvailabilityCell(unitOfWork, day, booking.VisitTargetId, currentVisitSlot))
                        {
                            var historyLink = HistoryExtensions.WriteHistory(unitOfWork, null,
                                $"Создание записи на визит ({(mpgu ? "портал" : "оператор")})", string.Empty);

                            bookings.Add(new MGTBookingVisit
                            {
                                StatusId = (long)MGTVisitBookingStatuses.PrebookingRegistered,
                                TargetId = booking.VisitTargetId,
                                VisitCell = currentVisitSlot,
                                WorkingDayId = day.Id,
                                HistoryLink = historyLink,
                                HistoryLinkId = historyLink.Id,
                                DepartmentId = departId,
                                Persons = new List<MGTVisitBookingPerson>
                                {
                                    new MGTVisitBookingPerson
                                    {
                                        Snils = booking.SNILS,
                                        FirstName = booking.FIO,
                                        PersonTypeId = (long) MGTVisitBookingPersonTypes.Declarant
                                    }
                                }
                            });

                            currentVisitSlot =
                                currentVisitSlot.AddMinutes(MGTWorkingDayLogic.TimeIntervalOn(currentVisitSlotDay));
                        }
                    }

                    currentVisitSlot = new DateTime(booking.VisitSlot.Ticks);

                    // количество броней не совпадает с количеством требуемых слотов
                    if (booking.VisitSlotsCount != bookings.Count)
                    {
                        throw new Exception("Бронь не укладывается в свободные слоты");
                    }

                    // сохраняем в базу брони
                    long firstBookingId = 0;
                    for (var i = 0; i < bookings.Count; i++)
                    {
                        var b = bookings[i];
                        if (i > 0)
                        {
                            b.ParrentId = firstBookingId;
                        }

                        unitOfWork.AddEntity(b);
                        unitOfWork.SaveChanges();
                        if (i == 0)
                        {
                            firstBookingId = b.Id;
                        }
                    }

                    //проверяем валидность записей
                    for (var i = 0; i < booking.VisitSlotsCount; i++)
                    {
                        if (!CheckAvailabilityCell(unitOfWork, day, booking.VisitTargetId, currentVisitSlot,
                            firstBookingId))
                        {
                            throw new Exception("Ошибка параллельной записи");
                        }

                        currentVisitSlot =
                            currentVisitSlot.AddMinutes(MGTWorkingDayLogic.TimeIntervalOn(currentVisitSlotDay));
                    }

                    scope.Complete();

                    return new BookingResult
                    {
                        Code = (long)MGTVisitBookingPrebookingStatuses.PrebookingSucsess,
                        Messeage = "Пребронь произведена успешно",
                        BookingId = firstBookingId.ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                logger?.Error("Ошибка бронирования, повторите бронирование позже", ex);

                return new BookingResult
                {
                    Code = (long)MGTVisitBookingPrebookingStatuses.TechError,
                    BookingId = null,
                    Messeage = "Ошибка бронирования, повторите бронирование позже"
                };
            }
        }

        /// <summary>
        ///     Создание пред брони
        /// </summary>
        public BookingResult Prebooking(Booking booking, bool mpgu = false)
        {
            var departId = UnitOfWork.GetSet<MGTVisitTarget>().Where(vt => vt.Id == booking.VisitTargetId).FirstOrDefault().DepartmentId;
            return Prebooking(UnitOfWork, booking, mpgu, (long)departId ,Logger);
        }

        /// <summary>
        ///     Отмена пребронирования
        /// </summary>
        public BookingResult PrebookingCancellation(long bookingId)
        {
            try
            {
                var visit = UnitOfWork.GetSet<MGTBookingVisit>().FirstOrDefault(ss =>
                    ss.StatusId == (long)MGTVisitBookingStatuses.PrebookingRegistered && ss.Id == bookingId);
                if (visit == null)
                {
                    return new BookingResult { Code = 401, Messeage = "Пребронь не обнаружена" };
                }

                visit.StatusId = (long)MGTVisitBookingStatuses.PrebookingCanceled;

                if (visit.Children.Any())
                {
                    foreach (var c in visit.Children)
                    {
                        c.StatusId = (long)MGTVisitBookingStatuses.PrebookingCanceled;
                    }
                }

                UnitOfWork.SaveChanges();
                return new BookingResult { Code = 200, Messeage = "Пребронь аннулирована успешно" };
            }
            catch
            {
                return new BookingResult { Code = 500, BookingId = null, Messeage = "System error" };
            }
        }

        /// <summary>
        ///     Создать сообщение со статусом брони для очереди МПГУ
        /// </summary>
        public static void SendStatusMessage(IUnitOfWork unitOfWork, StatusMessageFilter filter) 
        {
            var mqMpguStatusOutcomingQueue = ConfigurationManager.AppSettings["MqMPGURequestStatusOutcoming"];
            Utils.SendStatusMessage(unitOfWork, mqMpguStatusOutcomingQueue, filter);
        }

        /// <summary>
        ///     Получить данные по ПИН коду
        /// </summary>
        public VisitSUOResult GetSuoVisitData(int pinCode)
        {
            var d1 = DateTime.Now.AddMinutes(-1 * Properties.Settings.Default.SuoVisitTooLate);
            var d2 = DateTime.Now.Date.AddDays(1);
            var d3 = DateTime.Now.AddMinutes(Properties.Settings.Default.SuoVisitTooEarly);
            var pinCodeString = Utils.GeneratePin(pinCode);

            var visit = UnitOfWork.GetSet<MGTBookingVisit>().FirstOrDefault(ss =>
                (ss.StatusId == (long)MGTVisitBookingStatuses.BookingRegistered)
                && ss.VisitCell >= d1
                && ss.VisitCell < d2
                && ss.PINCode == pinCodeString
            );

            //запись на приём не найдена
            if (visit == null)
            {
                return new VisitSUOResult { VisitorData = null, ResultType = VisitSUOResultType.Unvalid };
            }

            //посетитель явился слишком рано
            if (visit.VisitCell > d3)
            {
                return new VisitSUOResult { VisitorData = null, ResultType = VisitSUOResultType.ValidButEarly };
            }

            visit.StatusId = (long)MGTVisitBookingStatuses.BookingVisited;
            UnitOfWork.SaveChanges();

            var applicant = visit.Persons.FirstOrDefault(ss =>
                ss.PersonTypeId == 1) ?? visit.Persons.FirstOrDefault();

            return new VisitSUOResult
            {
                ResultType = VisitSUOResultType.Valid,
                VisitorData = new SUOVisitorData
                {
                    VisitTime = visit.VisitCell,
                    SlotCount = (visit.Children?.Count ?? 0) + 1,
                    EMail = applicant?.Email,
                    FIO = $"{applicant?.LastName} {applicant?.FirstName} {applicant?.MiddleName}",
                    SNILS = applicant?.Snils,
                    Phone = applicant?.Phone,
                    VisitTarget = visit.Target.Name,
                    VisitTargetId = visit.TargetId
                }
            };
        }
    }
}
