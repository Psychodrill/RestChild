using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Http;
using RestChild.Booking.Logic.Logic;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.MPGUIntegration;
using RestChild.Web.Extensions;
using RestChild.Web.Models;
using RestChild.Web.Models.VisitQueue;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    ///     Контроллер для работы с записью в МГТ
    /// </summary>
    [Authorize]
    public class WebBookingMosgorturReestrController : BaseController
    {
        /// <summary>
        ///     Список возможных целей визита
        /// </summary>
        [Route("api/bookingmosgortur/dayinfo")]
        public MGTWorkningDayInfo GetDayInfo(DateTime day)
        {
            return new MGTWorkningDayInfo
            {
                Interval = MGTWorkingDayLogic.TimeIntervalOn(day),
                Intervals = MGTWorkingDayLogic.GetIntervals(day)
            };
        }

        /// <summary>
        ///     Список возможных целей визита
        /// </summary>
        [Route("api/bookingmosgortur/targets")]
        public ICollection<IVisitTarget> GetTargets()
        {
            var targets = UnitOfWork.GetSet<MGTVisitTarget>().Where(ss => ss.IsActive)
                .Select(c => new VisitTarget {Id = c.Id, Name = c.Name}).ToArray();
            return targets;
        }

        /// <summary>
        ///     Список возможных статусов бронирования
        /// </summary>
        [Route("api/bookingmosgortur/Statuses")]
        public ICollection<VisitStatus> GetStatuses(bool showPrebookings = false)
        {
            var targets = UnitOfWork.GetSet<MGTVisitBookingStatus>().AsQueryable();
            if (!showPrebookings)
            {
                targets = targets.Where(ss =>
                    ss.Id != (long) MGTVisitBookingStatuses.PrebookingRegistered &&
                    ss.Id != (long) MGTVisitBookingStatuses.PrebookingCanceled);
            }

            return targets.Select(c => new VisitStatus {Id = c.Id, Name = c.Name}).ToArray();
        }

        /// <summary>
        ///     История рабочего дня
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/bookingmosgortur/workingday/history/{Id:long}")]
        public ICollection<BookingMosgorturReestrDayHistoryModel> GetHistory(long id)
        {
            return UnitOfWork.GetSet<MGTWorkingDaysHistory>().Where(ss => ss.WorkingDayId == id).Select(sx =>
                new BookingMosgorturReestrDayHistoryModel
                {
                    EventDate = sx.EventDate,
                    EventName = sx.EventName,
                    EventDescriptio = sx.EventDescription,
                    Author = sx.Author.Name + " " + sx.Author.Login + "(" + sx.Author.Id + ")"
                }
            ).ToList();
        }

        /// <summary>
        ///     Результаты поиска в реестре записей на приём МГТ
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="asOneList">Выдавать полный список или разбитый по страницам</param>
        /// <returns></returns>
        internal CommonPagedList<BookingMosgorturReestrFilterResultModel> Get(BookingMosgorturReestrFilterModel filter,
            bool asOneList = false)
        {
            var pageSize = Settings.Default.TablePageSize;
            var pageNumber = filter.PageNumber;
            var startRecord = (pageNumber - 1) * pageSize;

            var query = UnitOfWork.GetSet<MGTBookingVisit>().AsQueryable();

            query = FilterQuery(query, filter);

            var totalCount = query.Count();

            query = query.OrderBy(t => t.VisitCell);

            if (!asOneList)
            {
                query = query.Skip(startRecord).Take(pageSize);
            }

            var entity =
                query.Select(ss => new BookingMosgorturReestrFilterResultModel
                {
                    Id = ss.Id,
                    Target = ss.Target.Name,
                    Status = ss.Status.Name,
                    DateShedule = ss.VisitCell,
                    AplicantFIO = ss.Persons
                        .Where(sx => sx.PersonType.Id == (long) MGTVisitBookingPersonTypes.Declarant)
                        .Select(sx => sx.LastName + " " + sx.FirstName + " " + sx.MiddleName).Select(sx => sx.Trim())
                        .FirstOrDefault(),
                    SlotsCount = 1 + ss.Children.Count,
                    PINCode = ss.PINCode,
                    BookingNumber = ss.ServiceNumber,
                    Source = ss.MPGURegNum.Trim() != null ? "МПГУ" :
                        ss.StatusId == (long) MGTVisitBookingStatuses.PrebookingCanceled ||
                        ss.StatusId == (long) MGTVisitBookingStatuses.PrebookingRegistered ? "Пререгистрация" :
                        "Оператор",
                    RegDate = ss.MPGURegDate,
                    AplicantDateBirth = ss.Persons
                        .Where(sx => sx.PersonType.Id == (long) MGTVisitBookingPersonTypes.Declarant)
                        .Select(sx => sx.DateOfBirth)
                        .FirstOrDefault(),
                    AplicantEmail = ss.Persons
                        .Where(sx => sx.PersonType.Id == (long) MGTVisitBookingPersonTypes.Declarant)
                        .Select(sx => sx.Email)
                        .FirstOrDefault(),
                    AplicantSNILS = ss.Persons
                        .Where(sx => sx.PersonType.Id == (long) MGTVisitBookingPersonTypes.Declarant)
                        .Select(sx => sx.Snils)
                        .FirstOrDefault(),
                    AplicantTel = ss.Persons
                        .Where(sx => sx.PersonType.Id == (long) MGTVisitBookingPersonTypes.Declarant)
                        .Select(sx => sx.Phone)
                        .FirstOrDefault(),
                    AplicantMale = ss.Persons
                        .Where(sx => sx.PersonType.Id == (long) MGTVisitBookingPersonTypes.Declarant)
                        .Select(sx => sx.Male)
                        .FirstOrDefault()
                }).ToList();

            //функционал добавления связи с поданным заявлением
            //foreach (var t in entity)
            //{
            //   var bk = t.BookingNumber;
            //   if (!string.IsNullOrWhiteSpace(bk))
            //   {
            //      t.StatementId = UnitOfWork.GetSet<Request>().Where(sx =>
            //            sx.RequestNumberMpgu != null && sx.RequestNumber != null && sx.RequestNumberMpgu != "" &&
            //            sx.RequestNumber != "" && (sx.RequestNumber == bk || sx.RequestNumberMpgu == bk))
            //         .Select(sx => sx.Id).FirstOrDefault();
            //   }
            //}

            return new CommonPagedList<BookingMosgorturReestrFilterResultModel>(entity, pageNumber, pageSize,
                totalCount);
        }

        /// <summary>
        ///     Обработка фильтра
        /// </summary>
        private IQueryable<MGTBookingVisit> FilterQuery(IQueryable<MGTBookingVisit> query,
            BookingMosgorturReestrFilterModel filter)
        {
            query = query.Where(ss => ss.ParrentId == null);
            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.FIO))
                {
                    query = query.Where(ss => ss.Persons.Any(sx =>
                        (sx.LastName + " " + sx.FirstName + " " + sx.MiddleName).ToLower()
                        .Contains(filter.FIO.ToLower())));
                }

                if (filter.Status.HasValue && filter.Status.Value > 0)
                {
                    query = query.Where(ss => ss.StatusId == filter.Status.Value);
                }

                if (!filter.ShowPebookings)
                {
                    query = query.Where(ss =>
                        ss.StatusId != (long) MGTVisitBookingStatuses.PrebookingRegistered &&
                        ss.StatusId != (long) MGTVisitBookingStatuses.PrebookingCanceled);
                }

                if (filter.DateFrom.HasValue)
                {
                    query = query.Where(ss => ss.VisitCell >= filter.DateFrom.Value);
                }

                if (filter.DateTo.HasValue)
                {
                    if (filter.DateTo.Value.Date < filter.DateTo.Value)
                    {
                        query = query.Where(ss => ss.VisitCell <= filter.DateTo.Value);
                    }
                    else
                    {
                        var tm = filter.DateTo.Value.AddDays(1);
                        query = query.Where(ss => ss.VisitCell < tm);
                    }
                }

                if (filter.Target.HasValue && filter.Target.Value > 0)
                {
                    query = query.Where(ss => ss.TargetId == filter.Target.Value);
                }

                if (!string.IsNullOrWhiteSpace(filter.ServiceNumber))
                {
                    query = query.Where(ss => ss.ServiceNumber.Contains(filter.ServiceNumber));
                }

                if (filter.DateRegFrom.HasValue)
                {
                    query = query.Where(ss => ss.MPGURegDate >= filter.DateRegFrom.Value);
                }

                if (filter.DateRegTo.HasValue)
                {
                    if (filter.DateRegTo.Value.Date < filter.DateRegTo.Value)
                    {
                        query = query.Where(ss => ss.MPGURegDate <= filter.DateRegTo.Value);
                    }
                    else
                    {
                        var tm = filter.DateRegTo.Value.AddDays(1);
                        query = query.Where(ss => ss.MPGURegDate < tm);
                    }
                }

                if (filter.Source.HasValue)
                {
                    if (filter.Source.Value == (int) SourceEnum.Mpgu)
                    {
                        query = query.Where(ss => ss.MPGURegNum != null);
                    }
                    else if (filter.Source.Value == (int) SourceEnum.Operator)
                    {
                        query = query.Where(ss => ss.MPGURegNum == null);
                    }
                }
            }

            return query;
        }

        /// <summary>
        ///     вытащить весь список рабочих дней
        /// </summary>
        /// <param name="filter">фильтр параметров ограничивающий список</param>
        /// <returns></returns>
        internal CommonPagedList<MGTWorkingDay> GetDays(BookingMosgorturReestrWorkingDaysFilterModel filter)
        {
            var pageSize = Settings.Default.TablePageSize;
            var pageNumber = filter?.PageNumber ?? 1;
            var startRecord = (pageNumber - 1) * pageSize;

            var query = UnitOfWork.GetSet<MGTWorkingDay>().AsQueryable().Where(d => !d.IsDeleted);
            if (filter != null)
            {
                var df = new DateTime(filter.Date.Year, filter.Date.Month, 1);
                var dt = new DateTime(filter.Date.Year, filter.Date.Month, 1).AddDays(15);
                query = query.Where(ss => ss.Date >= df && ss.Date < dt);
            }


            var totalCount = query.Count();
            var entity =
                query.OrderByDescending(t => t.Date)
                    .Skip(startRecord)
                    .Take(pageSize)
                    .ToList();

            foreach (var d in entity)
            {
                d.WindowCount = UnitOfWork
                    .GetSet<MGTWorkingDayWindow>().Count(ss => !ss.IsCanceled && ss.WorkingDayId == d.Id);
                var temp = UnitOfWork.GetSet<MGTWorkingDayWindow>()
                    .Where(ss => !ss.IsCanceled && ss.WorkingDayId == d.Id)
                    .SelectMany(ss => ss.WorkingPeriods).ToList();

                var min = temp.Min(ss => ss.TimeFrom);
                var max = temp.Max(ss => ss.TimeTo);
                var bm = 0;
                for (var t = min; t <= max; t = t.AddMinutes(MGTWorkingDayLogic.TimeIntervalOn(d.Date.Date)))
                {
                    bm += temp.Count(ss => ss.TimeFrom <= t && ss.TimeTo > t);
                }

                d.BookingMaximum = bm;

                d.BookingCount = UnitOfWork.GetSet<MGTBookingVisit>().Count(ss =>
                    (ss.StatusId == (long) MGTVisitBookingStatuses.PrebookingRegistered ||
                     ss.StatusId == (long) MGTVisitBookingStatuses.BookingVisited ||
                     ss.StatusId == (long) MGTVisitBookingStatuses.BookingUnvisited ||
                     ss.StatusId == (long) MGTVisitBookingStatuses.BookingRegistered) && ss.WorkingDayId == d.Id);
            }

            return new CommonPagedList<MGTWorkingDay>(entity, pageNumber, pageSize, totalCount);
        }

        /// <summary>
        ///     Список бронирования для ScheduleMessage (очень старый функционал, ещё до онлайн очередей)
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("api/bookingmosgortur/bookings")]
        public IHttpActionResult GetBookings()
        {
            /*var bookings = UnitOfWork.GetSet<MGTBookingVisit>().Where(ss =>
                    ss.StatusId == (long) MGTVisitBookingStatuses.PrebookingRegistered ||
                    ss.StatusId == (long) MGTVisitBookingStatuses.BookingRegistered ||
                    ss.StatusId == (long) MGTVisitBookingStatuses.BookingVisited ||
                    ss.StatusId == (long) MGTVisitBookingStatuses.BookingUnvisited && ss.VisitCell > DateTime.Now)
                .Select(ss => ss.Id);*/
            var messages = UnitOfWork.GetSet<ScheduleMessage>().Select(ss => ss.Message).ToArray();

            return Json(messages);
        }

        internal MGTWorkingDayModel GetModel(long id = 0)
        {
            var day = UnitOfWork.GetSet<MGTWorkingDay>().FirstOrDefault(ss => ss.Id == id) ?? new MGTWorkingDay();

            var result = new MGTWorkingDayModel
            {
                Id = day.Id,
                Date = day.Date,
                TimeInterval = day.WorkingInterval,
                IsDeleted = day.IsDeleted,
                BookingCount = day.VisitBookings?.Count(ss =>
                    !(ss.StatusId == (long) MGTVisitBookingStatuses.PrebookingRegistered ||
                      ss.StatusId == (long) MGTVisitBookingStatuses.BookingRegistered ||
                      ss.StatusId == (long) MGTVisitBookingStatuses.BookingVisited ||
                      ss.StatusId == (long) MGTVisitBookingStatuses.BookingUnvisited)) ?? 0,
                SuoVisitTooEarly = day.SuoVisitTooEarly ?? Properties.Settings.Default.SuoVisitTooEarly,
                SuoVisitTooLate = day.SuoVisitTooLate ?? Properties.Settings.Default.SuoVisitTooLate,
            };

            if (day.Id > 0 && day.Windows != null && day.Windows.Count(ss => !ss.IsCanceled) > 0)
            {
                result.Windows = day.Windows.Where(ss => !ss.IsCanceled).OrderBy(ss => ss.WindowNumber)
                    .ThenBy(ss => ss.Id).Select(ss => new MGTWorkingDayWindowModel
                    {
                        Id = ss.Id,
                        IsDeleted = false,
                        SelectedTargets = ss.Targets.Select(sx => sx.Id).ToList(),
                        WindowNumber = ss.WindowNumber,
                        TimeIntervals = ss.WorkingPeriods.Select(sx => new MGTWorkingDayWindowTimeIntervalModel
                        {
                            Id = sx.Id,
                            TimeFromString = sx.TimeFrom.ToString(@"HH\:mm"),
                            TimeToString = sx.TimeTo.ToString(@"HH\:mm")
                        }).ToList()
                    }).ToList();
            }

            if (day.Id < 1)
            {
                result.TimeInterval = MGTWorkingDayLogic.TimeIntervalOn(day.Date);
            }

            return result;
        }

        internal bool CheckMgtDayExists(DateTime date)
        {
            var d = date.Date;
            return UnitOfWork.GetSet<MGTWorkingDay>().Any(ss => ss.Date == d && !ss.IsDeleted);
        }

        internal bool CheckMgtDayExistsAndIsBussy(DateTime date)
        {
            var d = date.Date;
            return UnitOfWork.GetSet<MGTWorkingDay>()
                .Any(ss => ss.Date == d && !ss.IsDeleted &&
                           ss.VisitBookings.Any(sx =>
                               !(sx.StatusId == (long) MGTVisitBookingStatuses.PrebookingCanceled ||
                                 sx.StatusId == (long) MGTVisitBookingStatuses.BookingCanceled ||
                                 sx.StatusId == (long) MGTVisitBookingStatuses.BookingMGTCanceled)));
        }

        internal long SetModel(MGTWorkingDayModel model)
        {
            using (var tran = UnitOfWork.GetTransactionScope())
            {
                var day = UnitOfWork.GetSet<MGTWorkingDay>().FirstOrDefault(ss => ss.Id == model.Id) ??
                          new MGTWorkingDay();

                day.Date = model.Date.Date;
                day.WorkingInterval = model.TimeInterval;
                day.IsDeleted = model.IsDeleted;
                day.SuoVisitTooEarly = model.SuoVisitTooEarly;
                day.SuoVisitTooLate = model.SuoVisitTooLate;

                var currentUser = Security.GetCurrentAccount();

                var history = new MGTWorkingDaysHistory
                {
                    EventDate = DateTime.Now,
                    AuthorId = currentUser.Id
                };

                if (day.Id < 1)
                {
                    day = UnitOfWork.AddEntity(day);
                    history.EventName = "Создание рабочего дня";
                }
                else
                {
                    history.EventName = "Изменение рабочего дня";
                }

                if (model.IsDeleted)
                {
                    history.EventName = "Удаление рабочего дня";
                }
                else
                {
                    if (model.Id > 0)
                    {
                        var windows = day.Windows.Select(ss => (long?) ss.Id).AsQueryable();

                        UnitOfWork.Delete<MGTWindowWorkingPeriod>(UnitOfWork.GetSet<MGTWindowWorkingPeriod>()
                            .Where(ss => windows.Contains(ss.WindowId)));
                        UnitOfWork.Delete<MGTWorkingDayWindow>(UnitOfWork.GetSet<MGTWorkingDayWindow>()
                            .Where(ss => windows.Contains(ss.Id)));
                        UnitOfWork.SaveChanges();
                    }

                    foreach (var window in model.Windows.Where(ss => !ss.IsDeleted))
                    {
                        var dayWindow = new MGTWorkingDayWindow
                        {
                            IsCanceled = false,
                            WorkingDay = day,
                            WindowNumber = window.WindowNumber
                        };

                        dayWindow = UnitOfWork.AddEntity(dayWindow);
                        UnitOfWork.SaveChanges();

                        if (dayWindow.Targets != null)
                        {
                            dayWindow.Targets.Clear();
                            foreach (var target in UnitOfWork.GetSet<MGTVisitTarget>()
                                .Where(sx => window.SelectedTargets.Contains(sx.Id)))
                            {
                                dayWindow.Targets.Add(target);
                            }
                        }
                        else
                        {
                            dayWindow.Targets = UnitOfWork.GetSet<MGTVisitTarget>()
                                .Where(sx => window.SelectedTargets.Contains(sx.Id)).ToList();
                        }


                        foreach (var interval in window.TimeIntervals.Where(ss => !ss.IsDeleted))
                        {
                            var tf = interval.TimeFromString.Split(':');
                            var tt = interval.TimeToString.Split(':');
                            var interval2 = new MGTWindowWorkingPeriod
                            {
                                Window = dayWindow,
                                TimeFrom = day.Date.Date.AddHours(Convert.ToInt32(tf[0]))
                                    .AddMinutes(Convert.ToInt32(tf[1])),
                                TimeTo = day.Date.Date.AddHours(Convert.ToInt32(tt[0]))
                                    .AddMinutes(Convert.ToInt32(tt[1]))
                            };

                            UnitOfWork.AddEntity(interval2);
                            UnitOfWork.SaveChanges();
                        }
                    }
                }

                history.WorkingDay = day;
                UnitOfWork.AddEntity(history);
                UnitOfWork.SaveChanges();

                tran.Complete();

                return day.Id;
            }
        }

        /// <summary>
        ///     Извлечь список целей визита
        /// </summary>
        /// <returns></returns>
        internal ICollection<MGTWorkingDayWindowModel.BookingTargets> GetDayTargets()
        {
            return UnitOfWork.GetSet<MGTVisitTarget>().Where(sx => sx.IsActive).ToList().Select(sx =>
                new MGTWorkingDayWindowModel.BookingTargets
                {
                    Id = sx.Id,
                    Name = sx.Name,
                    IsSet = false
                }).ToList();
        }

        /// <summary>
        ///     Извлечь список льгот
        /// </summary>
        /// <returns></returns>
        internal ICollection<BenefitType> GetChildrenBenefits()
        {
            return UnitOfWork.GetSet<BenefitType>().Where(sx => sx.IsActive).ToList();
        }

        /// <summary>
        ///     Проверка рабочего дня
        /// </summary>
        internal string ValidateBookingDay(DateTime date, TimeSpan time, long targetId, int clotCount = 1)
        {
            var d = date.Date;
            var day = UnitOfWork.GetSet<MGTWorkingDay>().Where(ss => ss.Date == d && !ss.IsDeleted).AsQueryable();
            if (day.Count() != 1)
            {
                return "Для данной даты бронирования не задано рабочее расписание";
            }

            if (!day.SelectMany(ss => ss.Windows).SelectMany(dd => dd.Targets).Select(t => t.Id)
                .Any(sx => sx == targetId))
            {
                return "Для данной даты и цели бронирования визит не возможен";
            }

            var ndate = date.Date.Add(time);

            if (ndate <= DateTime.Now)
            {
                return "Вы не можете забронировать дату в прошлом";
            }

            if (clotCount > 0)
            {
                for (var i = 1; i <= clotCount; i++)
                {
                    if (!BookingRepository.CheckAvailabilityCell(UnitOfWork, day.First(), targetId, ndate))
                    {
                        return "Данная дата и время бронирования уже заняты";
                    }

                    ndate = ndate.AddMinutes(MGTWorkingDayLogic.TimeIntervalOn(d));
                }
            }
            else
            {
                return "Количество необходимых слотов не может быть меньше одного";
            }


            return null;
        }

        /// <summary>
        ///     Сохранить модель
        /// </summary>
        internal long SetModel(BookingMosgorturReestrBooking model, bool mpgu = false)
        {
            long result;
            var prebookingResult = BookingRepository.Prebooking(UnitOfWork,
                new Models.VisitQueue.Booking
                {
                    FIO = model.FirstName,
                    SNILS = model.Snils,
                    VisitTargetId = model.SelectedTarget,
                    VisitSlotsCount = model.SlotsCount,
                    VisitSlot = model.Date.Date.AddTicks(model.Time.Ticks)
                }, mpgu);

            if (prebookingResult.Code != (long) MGTVisitBookingPrebookingStatuses.PrebookingSucsess)
            {
                throw new Exception(prebookingResult.Messeage);
            }

            result = Convert.ToInt64(prebookingResult.BookingId);

            var booking = UnitOfWork.GetSet<MGTBookingVisit>().First(ss => ss.Id == result);

            var serviceNumberNext = UnitOfWork.GetNextNumber(
                $"{ConfigurationManager.AppSettings["exchangeBaseRegistryFromCode"]}-{ConfigurationManager.AppSettings["exchangeSystemSelfSystemCode"]}-{ConfigurationManager.AppSettings["exchangeSystemSelfCode"]}-/" +
                DateTime.Now.Year.ToString().Substring(2));
            //var serviceNumber = $"2064-9300003-064701-{serviceNumberNext:0000000}/{DateTime.Now.Year.ToString().Substring(2)}";
            var serviceNumber =
                $"{ConfigurationManager.AppSettings["exchangeBaseRegistryFromCode"]}-{ConfigurationManager.AppSettings["exchangeSystemSelfSystemCode"]}-{ConfigurationManager.AppSettings["exchangeSystemSelfCode"]}-{serviceNumberNext:0000000}/{DateTime.Now.Year.ToString().Substring(2)}";

            booking.StatusId = (long) MGTVisitBookingStatuses.BookingRegistered;
            booking.PINCode = Utils.GeneratePin(result);
            booking.ServiceNumber = serviceNumber;
            booking.MPGURegDate = DateTime.Now;
            UnitOfWork.SaveChanges();

            var declarant = UnitOfWork.GetSet<MGTVisitBookingPerson>().First(d =>
                d.PersonTypeId == (long) MGTVisitBookingPersonTypes.Declarant && d.VisitBookingId == result);
            declarant.DateOfBirth = model.DateOfBirth;
            declarant.Email = model.Email;
            declarant.FirstName = model.FirstName;
            declarant.LastName = model.LastName;
            declarant.MiddleName = model.MiddleName;
            declarant.Male = model.Male ?? false;
            declarant.Phone = model.Phone;
            declarant.Snils = model.Snils;
            declarant.VisitBooking = booking;
            declarant.TypePerson =
                Enum.GetName(typeof(MGTVisitBookingPersonTypes), MGTVisitBookingPersonTypes.Declarant);
            UnitOfWork.SaveChanges();

            foreach (var child in model.Children)
            {
                if (child.IsDeleted)
                {
                    continue;
                }

                var childToAdd = new MGTVisitBookingPerson
                {
                    Benefit = child.BenefitType,
                    FirstName = child.FirstName,
                    LastName = child.LastName,
                    MiddleName = child.MiddleName,
                    VisitBooking = booking,
                    TypePerson = Enum.GetName(typeof(MGTVisitBookingPersonTypes), MGTVisitBookingPersonTypes.Child),
                    PersonTypeId = (long) MGTVisitBookingPersonTypes.Child,
                    DateOfBirth = child.DateOfBirth
                };
                UnitOfWork.AddEntity(childToAdd);
            }

            return result;
        }

        /// <summary>
        ///     Извлечь бронь
        /// </summary>
        internal BookingMosgorturReestrBooking GetBooking(long id)
        {
            var booking = UnitOfWork.GetSet<MGTBookingVisit>().First(ss => ss.Id == id);
            var children = booking.Persons.Where(sx => sx.PersonTypeId == 2).Select(sx =>
                new BookingMosgorturReestrBooking.Child
                {
                    BenefitType = sx.Benefit,
                    FirstName = sx.FirstName,
                    LastName = sx.LastName,
                    MiddleName = sx.MiddleName,
                    IsDeleted = false,
                    DateOfBirth = sx.DateOfBirth,
                    NoMiddleName = sx.MiddleName == null || sx.MiddleName == string.Empty
                }).ToList();
            var declarant = booking.Persons.First(sx => sx.PersonTypeId == 1);
            var target = UnitOfWork.GetSet<MGTVisitTarget>().FirstOrDefault(ss => ss.Name == booking.Target.Name);
            return new BookingMosgorturReestrBooking
            {
                BookingCode = booking.ServiceNumber, //booking.BookingCode,
                Date = booking.VisitCell.Date,
                Id = booking.Id,
                Time = booking.VisitCell.TimeOfDay,
                Children = children,
                DateOfBirth = declarant.DateOfBirth,
                Email = declarant.Email,
                FirstName = declarant.FirstName,
                SelectedTarget = target == null ? 0 : target.Id,
                LastName = declarant.LastName,
                Male = declarant.Male,
                Snils = declarant.Snils,
                MiddleName = declarant.MiddleName,
                Phone = declarant.Phone,
                Canceld = booking.StatusId == (long) MGTVisitBookingStatuses.BookingCanceled ||
                          booking.StatusId == (long) MGTVisitBookingStatuses.BookingMGTCanceled ||
                          booking.StatusId == (long) MGTVisitBookingStatuses.PrebookingCanceled,
                NoMiddleName = string.IsNullOrWhiteSpace(declarant.MiddleName),
                StatusId = booking.StatusId,
                StatusName = booking.Status.Name,
                SlotsCount = booking.Children.Count + 1,
                HistoryLinkId = booking.HistoryLinkId
            };
        }

        /// <summary>
        ///     Отменить бронирование
        /// </summary>
        /// <param name="id">Идентификатор бронирования</param>
        internal void CancelBooking(long id)
        {
            var newStatus = (long) MGTVisitBookingStatuses.BookingMGTCanceled;

            UnitOfWork.BeginTransaction();
            var booking = UnitOfWork.GetSet<MGTBookingVisit>().First(ss => ss.Id == id);

            booking.HistoryLink = this.WriteHistory(booking.HistoryLink, "Изменение статуса записи на визит (оператор)",
                GetDiff(booking, newStatus));
            booking.HistoryLinkId = booking.HistoryLink?.Id;

            booking.StatusId = newStatus;


            if (!string.IsNullOrWhiteSpace(booking.MPGURegNum))
            {
                var status10190 = ConfigurationManager.AppSettings["MqMPGUStatus10190Name"];
                if (string.IsNullOrWhiteSpace(status10190))
                {
                    status10190 = "отзыв заявления невозможен";
                }

                BookingRepository.SendStatusMessage(UnitOfWork,
                    new StatusMessageFilter(booking.ServiceNumber, 10190, status10190, DateTime.Now));

                var status10801 = ConfigurationManager.AppSettings["MqMPGUStatus1080.1Name"];
                if (string.IsNullOrWhiteSpace(status10801))
                {
                    status10801 = "запись отменена по инициативе офиса ГАУК «МОСГОРТУР»";
                }

                var note10801 = ConfigurationManager.AppSettings["MqMPGUStatus1080.1Title"];
                if (string.IsNullOrWhiteSpace(note10801))
                {
                    note10801 =
                        "Запись на прием отменена (заявление № {0}) по инициативе офиса ГАУК «МОСГОРТУР».<br/>Основание отмены записи направлено на электронную почту, указанную при формировании записи в офис.";
                }

                BookingRepository.SendStatusMessage(UnitOfWork,
                    new StatusMessageFilter(booking.ServiceNumber, 1080, status10801,
                        DateTime.Now)
                    {
                        Note = string.Format(note10801, booking.Id),
                        StatusReason = "1"
                    });
            }

            UnitOfWork.SaveChanges();

            if (booking.Children.Any())
            {
                foreach (var c in booking.Children)
                {
                    c.StatusId = newStatus;
                    UnitOfWork.SaveChanges();
                }
            }

            UnitOfWork.Commit();
        }

        /// <summary>
        ///     Посетитель явился
        /// </summary>
        /// <param name="id">Идентификатор бронирования</param>
        internal void BookingVisited(long id)
        {
            var newStatus = (long) MGTVisitBookingStatuses.BookingVisited;

            UnitOfWork.BeginTransaction();
            var booking = UnitOfWork.GetSet<MGTBookingVisit>().First(ss => ss.Id == id);

            booking.HistoryLink = this.WriteHistory(booking.HistoryLink, "Изменение статуса записи на визит (оператор)",
                GetDiff(booking, newStatus));
            booking.HistoryLinkId = booking.HistoryLink?.Id;

            booking.StatusId = newStatus;

            if (!string.IsNullOrWhiteSpace(booking.MPGURegNum))
            {
                var status10190 = ConfigurationManager.AppSettings["MqMPGUStatus10190Name"];
                if (string.IsNullOrWhiteSpace(status10190))
                {
                    status10190 = "отзыв заявления невозможен";
                }

                BookingRepository.SendStatusMessage(UnitOfWork,
                    new StatusMessageFilter(booking.ServiceNumber, 10190, status10190, DateTime.Now));

                var status1075 = ConfigurationManager.AppSettings["MqMPGUStatus1075Name"];
                if (string.IsNullOrWhiteSpace(status1075))
                {
                    status1075 = "заявитель явился на прием";
                }

                BookingRepository.SendStatusMessage(UnitOfWork,
                    new StatusMessageFilter(booking.ServiceNumber, 1075, status1075, DateTime.Now));
            }


            UnitOfWork.SaveChanges();

            if (booking.Children.Any())
            {
                foreach (var c in booking.Children)
                {
                    c.StatusId = newStatus;
                    UnitOfWork.SaveChanges();
                }
            }

            UnitOfWork.Commit();
        }

        /// <summary>
        ///     Разница в бронированиях
        /// </summary>
        private string GetDiff(MGTBookingVisit persisted, long? newStatus)
        {
            var sb = new StringBuilder();

            if (persisted.StatusId != newStatus)
            {
                var status = UnitOfWork.GetById<MGTVisitBookingStatus>(newStatus);
                sb.AppendLine(
                    $"<li>Изменен статус, старое значение: '{persisted.Status?.Name.FormatEx(string.Empty)}', новое значение: '{status?.Name.FormatEx(string.Empty)}'</li>");
            }

            var res = sb.ToString();
            if (string.IsNullOrWhiteSpace(res))
            {
                return null;
            }

            return $"<ul>{res}</ul>";
        }
    }
}
