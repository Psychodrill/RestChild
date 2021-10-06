using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Transactions;
using log4net;
using RestChild.Booking.Logic.Contracts;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
using RestChild.Comon.Dto.Booking;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using TimeOfRest = RestChild.Comon.Dto.Booking.TimeOfRest;

namespace RestChild.Booking.Logic
{
    public class Booking
    {
        /// <summary> 10 секунд. </summary>
        private const int WaitInterval = 2000;

        /// <summary> использование слотов примитив синхронизации. </summary>
        //private static readonly CountdownEvent Ce = new CountdownEvent(0);
        private static readonly ReaderWriterLock Rwl = new ReaderWriterLock();

        /// <summary> время жизни пребронирования. </summary>
        public static readonly TimeSpan TimeLifeBookingInterval;

        /// <summary>
        ///     ограничения для инвалидов
        /// </summary>
        public static readonly long?[] SubRestrictionForInvalid =
            Settings.Default.InvalidSubRestriction?.Cast<string>().Select(v => v.LongParse()).Where(l => l.HasValue)
                .ToArray();

        /// <summary>
        ///     льготы не для выплаты
        /// </summary>
        public static long?[] BenefitNotForPayment =
            Settings.Default?.BenefitNotForPayment?.Cast<string>()
                .Select(s => s.LongParse())
                .Where(s => s.HasValue)
                .Select(s => s)
                .ToArray() ?? new long?[0];

        public static readonly Dictionary<long, long> TypeOfRestDecode;
        private static bool _inited;

        static Booking()
        {
            Services = new Dictionary<string, Hotel>();
            Requests = new Dictionary<string, RequestDto>();
            SearchService = new Dictionary<string, Dictionary<string, Hotel>>();
            TimeOfRests = new Dictionary<long, TimeOfRest>();

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["TimeLifeBookingInterval"]))
            {
                TimeLifeBookingInterval = new TimeSpan(0, 45, 0);
            }
            else if (
                !TimeSpan.TryParse(ConfigurationManager.AppSettings["TimeLifeBookingInterval"],
                    out TimeLifeBookingInterval))
            {
                throw new ConfigurationErrorsException("Не правильно настроен параметр TimeLifeBookingInterval.");
            }

            TypeOfRestDecode = new Dictionary<long, long>();
        }

        /// <summary>
        ///     список место отдыха по ключу
        /// </summary>
        private static Dictionary<string, RequestDto> Requests { get; }

        /// <summary>
        ///     список место отдыха по ключу
        /// </summary>
        private static Dictionary<string, Hotel> Services { get; }

        /// <summary>
        ///     список мест отдыха по ключу
        /// </summary>
        private static Dictionary<long, TimeOfRest> TimeOfRests { get; }

        /// <summary>
        ///     получение списка мест отдыха
        /// </summary>
        private static Dictionary<string, Dictionary<string, Hotel>> SearchService { get; }

        /// <summary>
        ///     освобождение слотов.
        /// </summary>
        protected static void ReleaseService()
        {
            Rwl.ReleaseReaderLock();
        }

        /// <summary>
        ///     использование слотов.
        /// </summary>
        protected static void UseService()
        {
            if (!_inited)
            {
                throw new ApplicationException("Система еще не инициализирована");
            }

            Rwl.AcquireReaderLock(-1);
        }

        /// <summary>
        ///     Получить вид отдыха по заявлению
        /// </summary>
        public static BaseRequest AppendTypeOfRestByRequest(BaseRequest request)
        {
            UseService();
            try
            {
                if (!string.IsNullOrWhiteSpace(request.DocumentNumber) && Requests.ContainsKey(request.DocumentNumber))
                {
                    request.Request = Requests[request.DocumentNumber];
                    request.TypeOfRestId = request.Request?.TypeOfRest;
                }

                return request;
            }
            finally
            {
                ReleaseService();
            }
        }

        /// <summary>
        ///     обновить заявку
        /// </summary>
        public static bool UpdateRequest(long requestId)
        {
            try
            {
                LogManager.GetLogger(typeof(Booking)).InfoFormat("Обновление заявки RequestId={0}", requestId);

                RequestDto requestDto = null;

                using (var unitOfWork = new UnitOfWork())
                {
                    var request = unitOfWork.GetById<Request>(requestId);
                    if (request == null)
                    {
                        return false;
                    }

                    Rwl.AcquireWriterLock(WaitInterval);
                    try
                    {
                        if (request.StatusId != (long) StatusEnum.DecisionMaking &&
                            request.StatusId != (long) StatusEnum.DecisionMakingCovid)
                        {
                            if (!string.IsNullOrWhiteSpace(request.RequestNumber) &&
                                Requests.ContainsKey(request.RequestNumber))
                            {
                                Requests.Remove(request.RequestNumber);
                            }
                        }
                        else if (Requests.ContainsKey(request.RequestNumber))
                        {
                            var requestToUpdate = Requests[request.RequestNumber];
                            requestToUpdate.Attendants =
                                request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps ||
                                request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps
                                    ? 1
                                    : request.CountAttendants;
                            requestToUpdate.Places =
                                request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps ||
                                request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps
                                    ? 0
                                    : request.CountPlace;
                        }
                        else if (!Requests.ContainsKey(request.RequestNumber))
                        {
                            if (!request.TypeOfRestId.HasValue || !request.PlaceOfRestId.HasValue ||
                                !request.TimeOfRestId.HasValue || !request.IsFirstCompany)
                            {
                                return false;
                            }

                            var times = new List<long?> {request.TimeOfRestId};
                            if (!Settings.Default.SearchOnlyBaseTime)
                            {
                                times.AddRange(request.TimesOfRest.Select(t => t.TimeOfRestId).ToList());
                            }

                            var places = new List<long?> {request.PlaceOfRestId};
                            if (!Settings.Default.SearchOnlyBasePlace)
                            {
                                places.AddRange(request.PlacesOfRest.Select(t => t.PlaceOfRestId).ToList());
                            }

                            var res = new List<Tuple<DateTime, DateTime>>();
                            foreach (var c in request.Child)
                            {
                                if (!c.DateOfBirth.HasValue)
                                {
                                    continue;
                                }

                                var r = request.TypeOfRest.TypeOfRestBenefitRestrictions.FirstOrDefault(b =>
                                    b.BenefitTypeId == c.BenefitTypeId);

                                res.Add(
                                    new Tuple<DateTime, DateTime>(
                                        c.DateOfBirth.Value.AddYears(r?.MinAge ?? (int) request.TypeOfRest?.MinAge),
                                        c.DateOfBirth.Value.AddYears(
                                            (r?.MaxAge ?? (int) request.TypeOfRest?.MaxAge) + 1)));
                            }

                            requestDto = new RequestDto
                            {
                                Id = request.Id,
                                TypeOfRest = request.TypeOfRestId.Value,
                                Attendants =
                                    request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps ||
                                    request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps
                                        ? 1
                                        : request.CountAttendants,
                                Places = request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps ||
                                         request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps
                                    ? 0
                                    : request.CountPlace,
                                PlaceOfRest = places.ToArray(),
                                RequestNumber = request.RequestNumber,
                                TimeOfRest = times.ToArray(),
                                MayBeMoney = (request.TypeOfRest?.MayBeMoney ?? false) && !request.Child.Any(c =>
                                    BenefitNotForPayment.Contains(
                                        c.BenefitType?.SameBenefitId ?? c.BenefitTypeId)),
                                Dates =
                                    request.StatusId == (long) StatusEnum.DecisionMakingCovid ? null : res.ToArray(),
                                Group = GetGroupForRequest(request),
                                Covid19 = request.StatusId == (long) StatusEnum.DecisionMakingCovid
                            };
                            Requests.Add(request.RequestNumber, requestDto);
                        }
                    }
                    finally
                    {
                        Rwl.ReleaseWriterLock();
                    }

                    if (requestDto != null)
                    {
                        LogManager.GetLogger(typeof(Booking))
                            .InfoFormat("Обновление заявки завершено requestId={0}, rn={2}, GroupId={1}", requestId,
                                requestDto.Group, requestDto.RequestNumber);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(Booking)).Error("Ошибка обновления заявки", ex);
                return false;
            }
        }

        private static long GetGroupForRequest(Request request)
        {
            return request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsInvalid ||
                   request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsComplex ||
                   request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsOrphan ||
                   request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsPoor
                ? request.Child.Select(c =>
                      c.TypeOfSubRestriction?.RestrictionGroupId ??
                      (long?) RestrictionGroupEnum.NoAccessibleEnvironment).DefaultIfEmpty().Min() ??
                  (long) RestrictionGroupEnum.NoAccessibleEnvironment
                : (long) RestrictionGroupEnum.NoAccessibleEnvironment;
        }


        /// <summary>
        ///     обновить тур
        /// </summary>
        /// <param name="tourId"></param>
        public static bool UpdateTour(long tourId)
        {
            try
            {
                LogManager.GetLogger(typeof(Booking)).InfoFormat("Обновление блока мест TourId={0}", tourId);
                Rwl.AcquireWriterLock(WaitInterval);
                try
                {
                    using (var unitOfWork = new UnitOfWork())
                    {
                        var tour = unitOfWork.GetSet<Tour>().FirstOrDefault(t => t.Id == tourId);
                        FillHotelAndTour(tour);
                        LogManager.GetLogger(typeof(Booking))
                            .InfoFormat("Обновление блока мест завершено TourId={0}", tourId);
                        return true;
                    }
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(Booking)).Error("Ошибка обновления блока мест", ex);
                return false;
            }
        }

        /// <summary>
        ///     инициализация данных по расписанию
        /// </summary>
        public static bool LoadServices()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["disableInit"]) &&
                ConfigurationManager.AppSettings["disableInit"].ToLower(CultureInfo.InvariantCulture) == "true")
            {
                LogManager.GetLogger(typeof(Booking)).Warn("Загрузка расписания приема отключена!");
                return false;
            }

            LogManager.GetLogger(typeof(Booking)).Warn("Загрузка расписания приема начата...");
            Rwl.AcquireWriterLock(-1);
            try
            {
                _inited = false;
                TypeOfRestDecode.Clear();
                Services.Clear();
                Requests.Clear();
                SearchService.Clear();
                TimeOfRests.Clear();

                using (var unitOfWork = new UnitOfWork())
                {
                    FillDecodeTypeOfRest(unitOfWork);

                    var requests = unitOfWork.GetSet<Request>()
                        .Where(r => r.IsFirstCompany && !r.IsDeleted &&
                                    (r.StatusId == (long) StatusEnum.DecisionMaking ||
                                     r.StatusId == (long) StatusEnum.DecisionMakingCovid))
                        .ToList();

                    //c.Child.Any(ch => BenefitNotForPayment.Contains(ch.BenefitTypeId))

                    foreach (var request in requests)
                    {
                        if (!request.TypeOfRestId.HasValue || !request.PlaceOfRestId.HasValue ||
                            !request.TimeOfRestId.HasValue)
                        {
                            continue;
                        }

                        var times = new List<long?> {request.TimeOfRestId};
                        if (!Settings.Default.SearchOnlyBaseTime)
                        {
                            times.AddRange(request.TimesOfRest.Select(t => t.TimeOfRestId).ToList());
                        }

                        var places = new List<long?> {request.PlaceOfRestId};
                        if (!Settings.Default.SearchOnlyBasePlace)
                        {
                            places.AddRange(request.PlacesOfRest.Select(t => t.PlaceOfRestId).ToList());
                        }

                        var res = new List<Tuple<DateTime, DateTime>>();
                        foreach (var c in request.Child)
                        {
                            if (!c.DateOfBirth.HasValue)
                            {
                                continue;
                            }

                            var r = request.TypeOfRest.TypeOfRestBenefitRestrictions.FirstOrDefault(b =>
                                b.BenefitTypeId == c.BenefitTypeId);

                            res.Add(
                                new Tuple<DateTime, DateTime>(
                                    c.DateOfBirth.Value.AddYears(r?.MinAge ?? (int) request.TypeOfRest?.MinAge),
                                    c.DateOfBirth.Value.AddYears((r?.MaxAge ?? (int) request.TypeOfRest?.MaxAge) + 1)));
                        }

                        var requestDto = new RequestDto
                        {
                            Id = request.Id,
                            TypeOfRest = request.TypeOfRestId.Value,
                            Attendants =
                                request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps ||
                                request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps
                                    ? 1
                                    : request.CountAttendants,
                            Places =
                                request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps ||
                                request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps
                                    ? 0
                                    : request.CountPlace,
                            PlaceOfRest = places.ToArray(),
                            RequestNumber = request.RequestNumber,
                            TimeOfRest = times.ToArray(),
                            MayBeMoney =
                                request.TypeOfRest.MayBeMoney && !request.Child.Any(ch =>
                                    BenefitNotForPayment.Contains(
                                        ch.BenefitType?.SameBenefitId ?? ch.BenefitTypeId)),
                            Dates = request.StatusId == (long) StatusEnum.DecisionMakingCovid ? null : res.ToArray(),
                            Group = GetGroupForRequest(request),
                            Covid19 = request.StatusId == (long) StatusEnum.DecisionMakingCovid
                        };

                        Requests.Add(request.RequestNumber, requestDto);
                        /*LogManager.GetLogger(typeof(Booking))
                            .InfoFormat("Обновление заявки завершено requestId={0}, rn={2}, GroupId={1}", requestDto.Id,
                                requestDto.Group, requestDto.RequestNumber);*/
                    }

                    var indexServer = Settings.Default.IndexServer;
                    var loadedKeys =
                        TypeOfRestDecode.Values.Distinct()
                            .Where(item => GetServerIndexClient(new BaseRequest {TypeOfRestId = item}) == indexServer)
                            .Distinct()
                            .Select(i => (long?) i)
                            .ToList();

                    var toursQuery =
                        unitOfWork.GetSet<Tour>()
                            .Include("Files")
                            .Include("Volumes")
                            .Include("Volumes.Hotels")
                            .Include("Volumes.TypeOfRooms")
                            .Where(t => t.IsActive && t.DateIncome > DateTime.Now &&
                                        (t.TypeOfRest.ForMPGU ||
                                         t.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsOther))
                            .Where(t => loadedKeys.Contains(t.TypeOfRestId))
                            .Where(t => t.StateId == StateMachineStateEnum.Tour.Formed);

                    var tours = toursQuery.ToList();
                    foreach (var tour in tours)
                    {
                        FillHotelAndTour(tour);
                    }

                    _inited = true;
                }
            }
            finally
            {
                Rwl.ReleaseWriterLock();
            }

            LogManager.GetLogger(typeof(Booking)).Warn("Загрузка расписания приема закончена...");

            return true;
        }

        private static string MakeDescription(Hotels hotel)
        {
            if (hotel == null)
            {
                return null;
            }

            var res = new StringBuilder();
            if (!string.IsNullOrEmpty(hotel.Description) || hotel.Squere.HasValue || hotel.PoolAvailability ||
                hotel.MedicalOfficeAvailability || hotel.OutdoorPondAvailability)
            {
                if (!string.IsNullOrEmpty(hotel.Description))
                {
                    res.AppendFormat("<div style='white-space: pre-wrap'>{0}</div>\n", hotel.Description);
                }

                if (hotel.Squere.HasValue)
                {
                    res.AppendFormat("<div>Площадь территории, га: {0}</div>", hotel.Squere.FormatEx());
                }

                if (hotel.MedicalOfficeAvailability)
                {
                    res.AppendLine("<div>Наличие медицинского кабинета</div>");
                }

                if (hotel.OutdoorPondAvailability)
                {
                    res.AppendLine(
                        $"<div>Наличие открытого водоема{(string.IsNullOrEmpty(hotel.OutdoorPondName) ? string.Empty : " (" + hotel.OutdoorPondName + ")")}</div>");
                }

                if (hotel.PoolAvailability)
                {
                    res.AppendLine("<div>Наличие бассейна</div>");
                }
            }

            if (!string.IsNullOrEmpty(hotel.DrivingDirections))
            {
                res.AppendLine("<h3>Маршрут проезда</h3>");
                res.AppendFormat("<div style='white-space: pre-wrap'>{0}</div>\n", hotel.DrivingDirections);
            }

            if (hotel.TakeChildUp3Years)
            {
                res.AppendLine("<div>Для детей до 3-х лет</div>");
            }

            if (hotel.LibraryAvailability || hotel.ComputerClassAvailability || hotel.CinimaAvailability ||
                !string.IsNullOrEmpty(hotel.OtherLeisure))
            {
                res.AppendLine("<h3>Досуг</h3>");
                var items = new List<string>();
                if (hotel.LibraryAvailability)
                {
                    items.Add("Библиотека");
                }

                if (hotel.ComputerClassAvailability)
                {
                    items.Add("Компьютерный класс");
                }

                if (hotel.CinimaAvailability)
                {
                    items.Add(
                        $"Кинозал{(string.IsNullOrEmpty(hotel.CinimaTimetable) ? string.Empty : " (" + hotel.CinimaTimetable + ")")}");
                }

                if (items.Any())
                {
                    res.AppendFormat("<div>{0}</div>\n", string.Join(", ", items));
                }

                if (!string.IsNullOrEmpty(hotel.OtherLeisure))
                {
                    res.AppendFormat("<div style='white-space: pre-wrap'>{0}</div>\n", hotel.OtherLeisure);
                }
            }

            return res.ToString();
        }

        private static void FillHotelAndTour(Tour tour)
        {
            var firstVolume = tour.Volumes.FirstOrDefault();
            if (firstVolume != null && tour.DateIncome.HasValue && tour.DateOutcome.HasValue)
            {
                var h = new Hotel
                {
                    Name = firstVolume.NullSafe(fv => fv.Hotels.Name),
                    HotelId = firstVolume.NullSafe(fv => fv.HotelsId) ?? 0,
                    PlaceOfRestId = firstVolume.NullSafe(fv => fv.Hotels.PlaceOfRestId) ?? 0,
                    TypeOfRestId = tour.TypeOfRestId ?? 0,
                    SubjectOfRestId = tour.SubjectOfRestId,
                    Address = firstVolume.NullSafe(fv => fv.Hotels.Address),
                    Description = MakeDescription(firstVolume.NullSafe(fv => fv.Hotels)),
                    AccessibleEnvironment = firstVolume.NullSafe(fv => fv.Hotels.AccessibleEnvironment),
                    Photos = new List<FileLink>(),
                    TimeOfRests = new List<TimeOfRest>(),
                    ForFirstRequest = tour.ForMultipleStageCompany,
                    Group = tour.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex ||
                            tour.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsPoor ||
                            tour.TypeOfRestId == (long) TypeOfRestEnum.RestWithParents
                        ? tour.RestrictionGroupId ?? (long) RestrictionGroupEnum.NoAccessibleEnvironment
                        : (long) RestrictionGroupEnum.NoAccessibleEnvironment
                };

                var tr = new TimeOfRest
                {
                    Rooms = tour.Volumes.Any(v => v.TypeOfRoomsId.HasValue)
                        ? tour.Volumes.Where(v => v.TypeOfRooms != null).Select(v => new RoomShedule
                        {
                            Id = v.Id,
                            Place = v.TypeOfRooms.CountBasePlace,
                            PlaceAddon = v.TypeOfRooms.CountAddonPlace,
                            Left = (v.CountRooms ?? 0) - (v.CountBusyRooms ?? 0),
                            Description = v.TypeOfRooms.Name
                        }).ToList()
                        : null,
                    Id = tour.Id,
                    Bookings = new Dictionary<Guid, BookingRequest>(),
                    Start = tour.DateIncome.Value,
                    End = tour.DateOutcome.Value,
                    TimeOfRestId = tour.TimeOfRestId ?? 0,
                    LeftPlaces = (firstVolume.CountPlace ?? 0) > 0 && (firstVolume.CountBusyPlace ?? 0) >= 0
                        ? (firstVolume.CountPlace ?? 0) - (firstVolume.CountBusyPlace ?? 0)
                        : 0,
                    Hotel = h,
                    NeedAdd = tour.StateId == StateMachineStateEnum.Tour.Formed && tour.IsActive,
                    OpenBooking = tour.StartBooking,
                    CloseBooking = tour.EndBooking
                };

                h.TimeOfRests.Add(tr);

                if (firstVolume.Hotels?.Files != null && firstVolume.Hotels.Files.Any())
                {
                    h.Photos.AddRange(
                        firstVolume.Hotels.Files.Where(f => f.FileType.IsPhoto).OrderByDescending(f => f.IsMainPhoto)
                            .ThenBy(f => f.Id)
                            .Select(f => new FileLink {Url = f.FileUrl, Title = f.FileName}));
                }

                h.Photos.AddRange(
                    tour.Files.Where(f => f.FileType.IsPhoto)
                        .Select(f => new FileLink {Url = f.FileUrl, Title = f.FileName}));


                foreach (var photo in h.Photos.Where(p => !p.Url.StartsWith("http://")).ToList())
                {
                    photo.Url = string.Format(Settings.Default.HotelFileUrl, photo.Url,
                        $"{photo.Title}{Path.GetExtension(photo.Url)}");
                }

                LogManager.GetLogger(typeof(Booking))
                    .InfoFormat("Добавление места отдыха tour.Id={0}, Name='{1}', NeedAdd='{2}', Group={3}",
                        tour.NullSafe(t => t.Id),
                        h.Name,
                        string.Join("; ", h.TimeOfRests.Select(tor => $"{tor.Id}:{tor.TimeOfRestId}:{tor.NeedAdd}")),
                        h.Group);

                AddHotelAndTour(h);
            }
        }

        public static void FillDecodeTypeOfRest(UnitOfWork unitOfWork)
        {
            var trs = unitOfWork.GetSet<TypeOfRest>().ToList();
            TypeOfRestDecode.Clear();

            foreach (var tr in trs)
            {
                var parent = tr;
                while (parent != null && !parent.ForTour)
                {
                    parent = parent.Parent;
                }

                TypeOfRestDecode.Add(tr.Id, parent?.Id ?? tr.Id);
            }
        }

        /// <summary>
        ///     Добавление отеля и тура
        /// </summary>
        /// <param name="hotel"></param>
        private static void AddHotelAndTour(Hotel hotel)
        {
            foreach (var tr in hotel.TimeOfRests)
            {
                // есть ключ тура
                if (TimeOfRests.ContainsKey(tr.Id))
                {
                    var oldHotel = TimeOfRests[tr.Id].Hotel;
                    if (oldHotel != null)
                    {
                        oldHotel.TimeOfRests.RemoveAll(t => t.Id == tr.Id);
                        if (!oldHotel.TimeOfRests.Any())
                        {
                            var keys = GetKeys(oldHotel, tr);

                            foreach (var key in keys)
                            {
                                if (SearchService.ContainsKey(key))
                                {
                                    var lists = SearchService[key];
                                    if (lists.ContainsKey(oldHotel.Key))
                                    {
                                        lists.Remove(oldHotel.Key);
                                    }
                                }
                            }

                            Services.Remove(oldHotel.Key);
                        }
                    }

                    TimeOfRests.Remove(tr.Id);
                }
            }

            var timeOfRests = hotel.TimeOfRests.Where(t => t.NeedAdd).ToList();
            if (Services.ContainsKey(hotel.Key))
            {
                var ch = Services[hotel.Key];

                foreach (var timeRest in timeOfRests)
                {
                    if (ch.TimeOfRests.Any(tr => tr.Id == timeRest.Id))
                    {
                        ch.TimeOfRests.RemoveAll(tr => tr.Id == timeRest.Id);
                    }

                    ch.TimeOfRests.Add(timeRest);
                    timeRest.Hotel = ch;
                }

                hotel = ch;
            }
            else
            {
                foreach (var timeRest in timeOfRests)
                {
                    timeRest.Hotel = hotel;
                }

                Services.Add(hotel.Key, hotel);
            }

            foreach (var timeRest in timeOfRests)
            {
                var keys = GetKeys(hotel, timeRest);
                foreach (var key in keys)
                {
                    if (!SearchService.ContainsKey(key))
                    {
                        SearchService.Add(key, new Dictionary<string, Hotel>());
                    }

                    var dict = SearchService[key];

                    if (!dict.ContainsKey(hotel.Key))
                    {
                        dict.Add(hotel.Key, hotel);
                    }
                }

                TimeOfRests.Add(timeRest.Id, timeRest);
            }
        }

        private static string[] GetKeys(Hotel oldHotel, TimeOfRest tr)
        {
            var keys = new[]
            {
                $"{oldHotel.TypeOfRestId}_{tr.TimeOfRestId}_{oldHotel.PlaceOfRestId}_{oldHotel.ForFirstRequest}_{oldHotel.Group}",
                $"{oldHotel.TypeOfRestId}_{tr.TimeOfRestId}_{string.Empty}_{oldHotel.ForFirstRequest}_{oldHotel.Group}",
                $"{oldHotel.TypeOfRestId}_{string.Empty}_{oldHotel.PlaceOfRestId}_{oldHotel.ForFirstRequest}_{oldHotel.Group}",
                $"{oldHotel.TypeOfRestId}_{string.Empty}_{string.Empty}_{oldHotel.ForFirstRequest}_{oldHotel.Group}"
            };
            return keys;
        }

        /// <summary>
        ///     освобождение бронирования
        /// </summary>
        internal static BookingResult ReleaseBooking(BookingRequest request)
        {
            using (var uw = new UnitOfWork())
            {
                var bookings =
                    uw.GetSet<Domain.Booking>().Include(b => b.TourVolume).Where(b => b.Code == request.BookingGuid)
                        .ToList();

                if (!bookings.Any())
                {
                    return new BookingResult
                        {ErrorMessage = "Нет указанного бронирования для его отмены.", IsError = true};
                }

                var firstBooking = bookings.First();

                if (firstBooking.TourVolume?.TourId == null)
                {
                    return new BookingResult {ErrorMessage = "Нет блока мест для снятия бронирования.", IsError = true};
                }

                using (var tran = uw.GetTransactionScope())
                {
                    uw.AutoDetectChangesDisable();
                    return ReleaseBookingInternal(uw, tran, bookings, firstBooking, true);
                }
            }
        }

        private static BookingResult ReleaseBookingInternal(UnitOfWork uw, TransactionScope tran,
            List<Domain.Booking> bookings
            , Domain.Booking firstBooking, bool commitTran)
        {
            UseService();
            try
            {
                if (firstBooking?.TourVolume?.TourId == null)
                {
                    throw new ApplicationException("Error on release booking internal");
                }

                var service = TimeOfRests.ContainsKey(firstBooking.TourVolume.TourId.Value)
                    ? TimeOfRests[firstBooking.TourVolume.TourId.Value]
                    : new TimeOfRest();

                lock (service)
                {
                    var rollbackBooking = new List<Domain.Booking>();

                    foreach (var booking in bookings)
                    {
                        if (!booking.Canceled)
                        {
                            booking.Canceled = true;
                            uw.Context.Entry(booking).State = EntityState.Modified;
                            var tv = booking.TourVolume;
                            if (tv.CountBusyPlace.HasValue && booking.CountPlace.HasValue &&
                                tv.CountBusyPlace > 0)
                            {
                                tv.CountBusyPlace -= booking.CountPlace;
                                tv.CountBusyPlace -= booking.CountAttendants ?? 0;
                            }

                            if (tv.CountBusyRooms.HasValue && booking.CountRooms.HasValue)
                            {
                                tv.CountBusyRooms -= booking.CountRooms;
                            }

                            uw.Context.Entry(tv).State = EntityState.Modified;

                            rollbackBooking.Add(booking);
                        }
                    }

                    uw.SaveChanges();

                    foreach (var booking in rollbackBooking)
                    {
                        if (booking.CountPlace.HasValue && service.Rooms == null)
                        {
                            service.LeftPlaces += booking.CountPlace ?? 0;
                            service.LeftPlaces += booking.CountAttendants ?? 0;
                        }

                        if (booking.CountRooms.HasValue && service.Rooms != null)
                        {
                            var room = service.Rooms.FirstOrDefault(r => r.Id == booking.TourVolumeId);
                            if (room != null)
                            {
                                room.Left += booking.CountRooms ?? 0;
                            }
                        }
                    }

                    if (commitTran)
                    {
                        tran.Complete();
                    }
                }

                return new BookingResult();
            }
            finally
            {
                ReleaseService();
            }
        }

        /// <summary>
        ///     получение вариантов размещения.
        /// </summary>
        public static BookingVariationPlacementResponse VariationPlacement(BookingVariationPlacementRequest request)
        {
            if (!_inited)
            {
                return new BookingVariationPlacementResponse {Locations = new List<Location>()};
            }

            UseService();
            try
            {
                if (!Services.ContainsKey(request.HotelKey))
                {
                    return null;
                }

                var places = request.Places;
                var attendants = request.Attendants;

                if (!string.IsNullOrWhiteSpace(request.DocumentNumber))
                {
                    if (!Requests.ContainsKey(request.DocumentNumber))
                    {
                        return null;
                    }

                    var dto = Requests[request.DocumentNumber];
                    places = dto.Places;
                    attendants = dto.Attendants;
                }

                var service = Services[request.HotelKey];

                lock (service)
                {
                    var tr = service.TimeOfRests.FirstOrDefault(t => t.Id == request.TourId);
                    if (tr == null)
                    {
                        return null;
                    }

                    var result = new BookingVariationPlacementResponse
                    {
                        TourId = request.TourId,
                        Locations = tr.GetVariationPlacement(places, attendants, request.WithBookingDate)
                    };

                    if (result.Locations != null)
                    {
                        var minRooms = result.Locations
                            .Select(x => x.Places?.Select(y => y.CountRooms ?? 0).DefaultIfEmpty().Sum() ?? 0)
                            .DefaultIfEmpty().Min();

                        result.Locations = result.Locations
                            .Where(x => (x.Places?.Select(y => y.CountRooms ?? 0).DefaultIfEmpty().Sum() ?? 0) ==
                                        minRooms)
                            .ToList();
                    }


                    return result;
                }
            }
            finally
            {
                ReleaseService();
            }
        }

        /// <summary>
        ///     выполнить бронирование.
        /// </summary>
        internal static BookingResult MakeBooking(BookingRequest request, long? accountId)
        {
            if (!_inited)
            {
                return new BookingResult
                {
                    ErrorMessage =
                        "Срок предварительного бронирования места истек, повторите процесс бронирования места.",
                    IsError = true
                };
            }

            var places = request.Places;
            var attendants = request.Attendants;

            UseService();
            try
            {
                var oldBookingGuid = new List<Domain.Booking>();
                using (var uw = new UnitOfWork())
                {
                    uw.AutoDetectChangesDisable();

                    using (var tran = uw.GetTransactionScope())
                    {
                        if (!request.BookingGuid.HasValue)
                        {
                            return new BookingResult
                                {ErrorMessage = "В запросе отсутствует идентификатор бронирования.", IsError = true};
                        }

                        if (!TimeOfRests.ContainsKey(request.TourId))
                        {
                            return new BookingResult
                            {
                                ErrorMessage =
                                    "Места отдыха, указанного в запросе, нет. Попробуйте повторить запрос позже.",
                                IsError = true
                            };
                        }

                        var service = TimeOfRests[request.TourId];

                        if (!service.Bookings.ContainsKey(request.BookingGuid.Value))
                        {
                            return new BookingResult
                            {
                                ErrorMessage =
                                    "Срок предварительного бронирования места истек, повторите процесс бронирования места.",
                                IsError = true
                            };
                        }

                        var booking = service.Bookings[request.BookingGuid.Value];
                        RequestDto requestDto = null;
                        if (!string.IsNullOrWhiteSpace(request.DocumentNumber))
                        {
                            if (!Requests.ContainsKey(request.DocumentNumber))
                            {
                                return new BookingResult
                                {
                                    ErrorMessage =
                                        "В выбранном месте отдыха нет свободных мест. Попробуйте повторить запрос позже.",
                                    IsError = true
                                };
                            }

                            requestDto = Requests[request.DocumentNumber];

                            if (requestDto.Booked)
                            {
                                return new BookingResult
                                {
                                    ErrorMessage =
                                        "В заявлении уже осуществлён выбор. Проверьте корректность выбранного места отдыха.",
                                    IsError = true
                                };
                            }

                            places = requestDto.Places;
                            attendants = requestDto.Attendants;
                        }

                        // блокировка что бронирование прошло
                        lock (requestDto ?? new RequestDto())
                        {
                            if (requestDto?.Booked ?? false)
                            {
                                return new BookingResult
                                {
                                    ErrorMessage =
                                        "В заявлении уже осуществлён выбор. Проверьте корректность выбранного места отдыха.",
                                    IsError = true
                                };
                            }

                            if (booking.Places != places || booking.Attendants != attendants)
                            {
                                return new BookingResult
                                {
                                    ErrorMessage =
                                        "Информация в бронировании не соответствует информации резервирования.",
                                    IsError = true
                                };
                            }

                            if (requestDto != null)
                            {
                                oldBookingGuid.AddRange(uw.GetSet<Domain.Booking>()
                                    .Where(r => r.RequestId == requestDto.Id && !r.Canceled)
                                    .ToList());
                            }

                            lock (service)
                            {
                                if (booking.Released)
                                {
                                    return new BookingResult
                                    {
                                        ErrorMessage =
                                            "Срок предварительного бронирования места истек, повторите процесс бронирования места.",
                                        IsError = true
                                    };
                                }

                                if (booking.Rooms != null && booking.Rooms.Any())
                                {
                                    TourVolume firstTour = null;

                                    foreach (var room in booking.Rooms)
                                    {
                                        var tour = uw.GetById<TourVolume>(room.RoomId);
                                        if (tour != null)
                                        {
                                            if (tour.CountRooms.HasValue)
                                            {
                                                tour.CountBusyRooms = (tour.CountBusyRooms ?? 0) + room.Count;
                                            }

                                            firstTour = tour;

                                            uw.Context.Entry(tour).State = EntityState.Modified;
                                            var bookingModel = new Domain.Booking
                                            {
                                                BookingDate = booking.BookingDate ?? DateTime.Now,
                                                Code = request.BookingGuid.Value,
                                                CountPlace = booking.Places,
                                                CountAttendants = booking.Attendants,
                                                CountRooms = room.Count,
                                                TourVolumeId = tour.Id,
                                                TypeOfRestId = booking.TypeOfRestId,
                                                RequestId = requestDto?.Id
                                            };

                                            uw.GetSet<Domain.Booking>().Add(bookingModel);
                                        }
                                        else
                                        {
                                            return new BookingResult
                                            {
                                                ErrorMessage = "Нет места отдыха для выполнения бронирования",
                                                IsError = true
                                            };
                                        }
                                    }

                                    if (!FillRequestInfoAndChangeStatus(uw, requestDto, request.BookingGuid.Value,
                                        firstTour, accountId, request.IsFromMPGU))
                                    {
                                        return new BookingResult
                                        {
                                            ErrorMessage = "Нет места отдыха для выполнения бронирования",
                                            IsError = true
                                        };
                                    }

                                    uw.SaveChanges();
                                }
                                else
                                {
                                    var tour = uw.GetSet<TourVolume>().FirstOrDefault(t => t.TourId == service.Id);
                                    if (tour != null)
                                    {
                                        tour.CountBusyPlace += booking.Places;
                                        tour.CountBusyPlace += booking.Attendants;
                                        uw.Context.Entry(tour).Property(p => p.CountBusyPlace).IsModified = true;
                                        var bookingModel = new Domain.Booking
                                        {
                                            BookingDate = booking.BookingDate ?? DateTime.Now,
                                            Code = request.BookingGuid.Value,
                                            CountPlace = booking.Places,
                                            CountAttendants = booking.Attendants,
                                            TourVolumeId = tour.Id,
                                            TypeOfRestId = booking.TypeOfRestId,
                                            RequestId = requestDto?.Id
                                        };

                                        uw.GetSet<Domain.Booking>().Add(bookingModel);

                                        if (!FillRequestInfoAndChangeStatus(uw, requestDto, request.BookingGuid.Value,
                                            tour, accountId, request.IsFromMPGU))
                                        {
                                            return new BookingResult
                                            {
                                                ErrorMessage = "Нет места отдыха для выполнения бронирования",
                                                IsError = true
                                            };
                                        }

                                        uw.SaveChanges();
                                    }
                                    else
                                    {
                                        return new BookingResult
                                        {
                                            ErrorMessage = "Нет места отдыха для выполнения бронирования",
                                            IsError = true
                                        };
                                    }
                                }

                                booking.Booked = true;
                                if (service.Bookings.ContainsKey(request.BookingGuid.Value))
                                {
                                    service.Bookings.Remove(request.BookingGuid.Value);
                                }

                                foreach (var b in oldBookingGuid)
                                {
                                    ReleaseBookingInternal(uw, tran, new List<Domain.Booking> {b}, b, false);
                                }

                                tran.Complete();
                                return new BookingResult {BookingGuid = request.BookingGuid};
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(Booking))
                    .Error("Ошибка запуска MakeBooking", ex);
                throw;
            }
            finally
            {
                ReleaseService();
            }
        }

        /// <summary>
        ///     заполнить информацию о запросе и изменить статус
        /// </summary>
        private static bool FillRequestInfoAndChangeStatus(UnitOfWork uw, RequestDto requestDto, Guid bookingGuid,
            TourVolume tour, long? accountId, bool isFromMpgu = false)
        {
            if (tour == null)
            {
                return false;
            }

            if (requestDto?.Id != null)
            {
                var req = uw.GetById<Request>(requestDto.Id);
                if (req != null && req.IsFirstCompany &&
                    (req.StatusId == (long) StatusEnum.DecisionMaking && !req.BookingGuid.HasValue ||
                     req.StatusId == (long) StatusEnum.DecisionMakingCovid))
                {
                    req.BookingGuid = bookingGuid;
                    uw.Context.Entry(req).Property(m => m.BookingGuid).IsModified = true;
                    req.TourId = tour.TourId;
                    uw.Context.Entry(req).Property(m => m.TourId).IsModified = true;
                    req.HotelsId = tour.HotelsId ?? tour.Tour?.HotelsId;
                    uw.Context.Entry(req).Property(m => m.HotelsId).IsModified = true;
                    req.SubjectOfRestId = tour.Tour?.SubjectOfRestId;
                    uw.Context.Entry(req).Property(m => m.SubjectOfRestId).IsModified = true;
                    requestDto.Booked = true;

                    uw.AutoDetectChangesEnable();
                    req = uw.RequestChangeStatusInternal(AccessRightEnum.Status.FcToDecisionIsMade, req, null, false,
                        accountId, null, isFromMpgu);

                    if (req?.StatusId != (long) StatusEnum.CertificateIssued &&
                        req?.StatusId != (long) StatusEnum.DecisionIsMade)
                    {
                        return false; // return new BookingResult
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     освободить все просроченные бронирования.
        /// </summary>
        public static void ReleaseAllOverduePreBooking()
        {
            List<BookingRequest> listBooking;

            if (!_inited)
            {
                LogManager.GetLogger(typeof(Booking))
                    .Error("Ошибка запуска ReleaseAllOverduePreBooking - система еще не инициализирована");
                return;
            }

            LogManager.GetLogger(typeof(Booking)).Info("Получение списка мест для освобождения");
            // получение списка бронирований по которым прошел срок
            Rwl.AcquireWriterLock(WaitInterval);
            try
            {
                listBooking =
                    TimeOfRests.Values.SelectMany(b => b.Bookings.Values)
                        .Where(b => DateTime.Now - b.BookingDate > TimeLifeBookingInterval)
                        .ToList();
            }
            finally
            {
                Rwl.ReleaseWriterLock();
            }


            LogManager.GetLogger(typeof(Booking))
                .InfoFormat("Список мест для освобождения получен ({0}- резервирований), освобождение мест",
                    listBooking.Count);

            // освобождение бронирований.
            foreach (var booking in listBooking)
            {
                ReleasePreBooking(booking);
            }

            LogManager.GetLogger(typeof(Booking)).Info("Список просроченные бронирования освобождены");
        }

        /// <summary>
        ///     выполнить пре бронирование.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static BookingResult MakePreBooking(BookingRequest request)
        {
            if (!_inited)
            {
                return new BookingResult
                {
                    ErrorMessage = "Доступные для бронирования места не найдены. Попробуйте повторить запрос позже.",
                    IsError = true
                };
            }

            if (request.Rooms != null && request.Rooms.Any() &&
                request.Rooms.Any(r => !r.Count.HasValue || r.Count <= 0))
            {
                return new BookingResult
                {
                    ErrorMessage = "Доступные для бронирования места не найдены. Попробуйте повторить запрос позже.",
                    IsError = true
                };
            }

            if (request.Places + request.Attendants <= 0 && string.IsNullOrWhiteSpace(request.DocumentNumber))
            {
                return new BookingResult
                {
                    ErrorMessage = "Доступные для бронирования места не найдены. Попробуйте повторить запрос позже.",
                    IsError = true
                };
            }

            var places = request.Places;
            var attendants = request.Attendants;

            UseService();
            try
            {
                if (!TimeOfRests.ContainsKey(request.TourId))
                {
                    return new BookingResult
                    {
                        ErrorMessage =
                            "В выбранном месте отдыха нет свободных мест. Попробуйте повторить запрос позже.",
                        IsError = true
                    };
                }

                if (!string.IsNullOrWhiteSpace(request.DocumentNumber))
                {
                    if (!Requests.ContainsKey(request.DocumentNumber))
                    {
                        return new BookingResult
                        {
                            ErrorMessage =
                                "В выбранном месте отдыха нет свободных мест. Попробуйте повторить запрос позже.",
                            IsError = true
                        };
                    }

                    var dto = Requests[request.DocumentNumber];
                    places = dto.Places;
                    attendants = dto.Attendants;
                    request.Places = places;
                    request.Attendants = attendants;
                }

                var service = TimeOfRests[request.TourId];

                lock (service)
                {
                    if (request.Rooms != null && request.Rooms.Any())
                    {
                        var dict = request.Rooms.GroupBy(r => r.RoomId)
                            .ToDictionary(r => r.Key, d => d.Sum(v => v.Count));

                        if (dict.Keys.Any(k => !service.Rooms.Any(r => r.Id == k && r.Left >= dict[k])))
                        {
                            return new BookingResult
                            {
                                ErrorMessage =
                                    "Выбранное размещение больше не доступно. Попробуйте повторить запрос позже.",
                                IsError = true
                            };
                        }

                        var totalCount = 0;

                        var haveError = false;
                        var errorMessage = string.Empty;

                        foreach (var k in dict.Keys)
                        {
                            var room = service.Rooms.FirstOrDefault(r => r.Id == k && r.Left >= dict[k]);
                            if (room != null)
                            {
                                room.Left -= dict[k] ?? 0;
                                totalCount += room.Place * (dict[k] ?? 0);
                            }
                            else
                            {
                                haveError = true;
                                errorMessage =
                                    "Выбранное размещение больше не доступно. Попробуйте повторить запрос позже.";
                            }
                        }

                        if (!haveError && totalCount != places + attendants)
                        {
                            haveError = true;
                            errorMessage =
                                "Выбранное размещение больше не доступно. Попробуйте повторить запрос позже.";
                        }

                        if (haveError)
                        {
                            foreach (var k in dict.Keys)
                            {
                                var room = service.Rooms.FirstOrDefault(r => r.Id == k && r.Left >= dict[k]);
                                if (room != null)
                                {
                                    room.Left += dict[k] ?? 0;
                                }
                            }

                            return new BookingResult {ErrorMessage = errorMessage, IsError = true};
                        }

                        request.BookingDate = DateTime.Now;
                        request.BookingGuid = Guid.NewGuid();
                        service.Bookings.Add(request.BookingGuid.Value, request);

                        var res = new BookingResult {BookingGuid = request.BookingGuid, IsError = false};
                        return res;
                    }
                    else
                    {
                        if (service.LeftPlaces >= places + attendants)
                        {
                            service.LeftPlaces -= places;
                            service.LeftPlaces -= attendants;

                            request.BookingDate = DateTime.Now;
                            request.BookingGuid = Guid.NewGuid();
                            service.Bookings.Add(request.BookingGuid.Value, request);

                            var res = new BookingResult {BookingGuid = request.BookingGuid, IsError = false};
                            return res;
                        }
                        else
                        {
                            return new BookingResult
                            {
                                ErrorMessage =
                                    "Необходимое количество свободных мест более не доступно. Попробуйте повторить запрос позже.",
                                IsError = true
                            };
                        }
                    }
                }
            }
            finally
            {
                ReleaseService();
            }
        }

        /// <summary>
        ///     получить пре бронирование.
        /// </summary>
        /// <param name="request">нужно передавать 3 параметра typeOfRest, TourId, bookingGuid</param>
        /// <returns></returns>
        internal static BookingRequest GetPreBooking(BookingRequest request)
        {
            UseService();
            try
            {
                if (!TimeOfRests.ContainsKey(request.TourId))
                {
                    return null;
                }

                var service = TimeOfRests[request.TourId];

                if (!request.BookingGuid.HasValue)
                {
                    return null;
                }

                if (!service.Bookings.ContainsKey(request.BookingGuid.Value))
                {
                    return null;
                }

                return service.Bookings[request.BookingGuid.Value];
            }
            finally
            {
                ReleaseService();
            }
        }

        /// <summary>
        ///     выполнить пре бронирование.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static BookingResult ReleasePreBookingPsevdo(BookingRequest request)
        {
            if (!_inited)
            {
                return new BookingResult {ErrorMessage = "Нет бронирования.", IsError = true};
            }

            UseService();
            try
            {
                if (!TimeOfRests.ContainsKey(request.TourId))
                {
                    return new BookingResult {ErrorMessage = "Нет выбранного места отдыха.", IsError = true};
                }

                var service = TimeOfRests[request.TourId];

                if (!request.BookingGuid.HasValue)
                {
                    return new BookingResult
                        {ErrorMessage = "В запросе отсутствует идентификатор бронирования.", IsError = true};
                }

                if (!service.Bookings.ContainsKey(request.BookingGuid.Value))
                {
                    return new BookingResult {ErrorMessage = "Нет бронирования.", IsError = true};
                }

                var booking = service.Bookings[request.BookingGuid.Value];

                lock (service)
                {
                    if (booking.Booked)
                    {
                        return new BookingResult
                        {
                            ErrorMessage =
                                "Нельзя освободить предварительное бронирование так как осуществлено полное бронирование.",
                            IsError = true
                        };
                    }

                    var rnd = new Random();
                    booking.BookingDate = DateTime.Now.Add(-TimeLifeBookingInterval).AddMinutes(rnd.NextDouble() * 15)
                        .AddMinutes(1);
                    var res = new BookingResult {IsError = false};
                    return res;
                }
            }
            finally
            {
                ReleaseService();
            }
        }

        /// <summary>
        ///     выполнить пре бронирование.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static BookingResult ReleasePreBooking(BookingRequest request)
        {
            if (!_inited)
            {
                return new BookingResult {ErrorMessage = "Нет бронирования.", IsError = true};
            }

            UseService();
            try
            {
                if (!TimeOfRests.ContainsKey(request.TourId))
                {
                    return new BookingResult {ErrorMessage = "Нет выбранного места отдыха.", IsError = true};
                }

                var service = TimeOfRests[request.TourId];

                if (!request.BookingGuid.HasValue)
                {
                    return new BookingResult
                        {ErrorMessage = "В запросе отсутствует идентификатор бронирования.", IsError = true};
                }

                if (!service.Bookings.ContainsKey(request.BookingGuid.Value))
                {
                    return new BookingResult {ErrorMessage = "Нет бронирования.", IsError = true};
                }

                var booking = service.Bookings[request.BookingGuid.Value];

                lock (service)
                {
                    if (booking.Booked)
                    {
                        return new BookingResult
                        {
                            ErrorMessage =
                                "Нельзя освободить предварительное бронирование так как осуществеленн полное бронирование.",
                            IsError = true
                        };
                    }
                    else if (booking.Rooms != null && booking.Rooms.Any())
                    {
                        var dict = booking.Rooms.GroupBy(r => r.RoomId)
                            .ToDictionary(r => r.Key, d => d.Sum(v => v.Count));

                        foreach (var k in dict.Keys)
                        {
                            var room = service.Rooms.FirstOrDefault(r => r.Id == k);
                            if (room != null)
                            {
                                room.Left += dict[k] ?? 0;
                            }
                        }

                        if (service.Bookings.ContainsKey(request.BookingGuid.Value))
                        {
                            service.Bookings.Remove(request.BookingGuid.Value);
                        }
                    }
                    else
                    {
                        service.LeftPlaces += booking.Places;
                        service.LeftPlaces += booking.Attendants;
                        if (service.Bookings.ContainsKey(request.BookingGuid.Value))
                        {
                            service.Bookings.Remove(request.BookingGuid.Value);
                        }
                    }

                    booking.Released = true;

                    var res = new BookingResult {IsError = false};
                    return res;
                }
            }
            finally
            {
                ReleaseService();
            }
        }

        /// <summary>
        ///     внутренний метод для получения мест отдыха
        /// </summary>
        /// <summary>
        ///     получение списка мест отдыха.
        /// </summary>
        public static ResultSearch GetHotels(BookingSearchRequest request)
        {
            if (!_inited)
            {
                return new ResultSearch {Hotels = new List<Hotel>(), Count = 0};
            }

            UseService();
            try
            {
                long?[] timeOfRests;
                long?[] placeOfRest = null;
                var places = request.Places;
                var attendants = request.Attendants;
                var typeOfRest = request.TypeOfRestId ?? 0;
                var mayBeMoney = false;
                Tuple<DateTime, DateTime>[] dates = null;
                string key;

                if (!string.IsNullOrWhiteSpace(request.DocumentNumber))
                {
                    if (!Requests.ContainsKey(request.DocumentNumber))
                    {
                        return new ResultSearch {Hotels = new List<Hotel>(), Count = 0};
                    }

                    var dto = Requests[request.DocumentNumber];

                    typeOfRest = dto.TypeOfRest;
                    placeOfRest = dto.PlaceOfRest;
                    timeOfRests = dto.TimeOfRest;
                    places = dto.Places;
                    attendants = dto.Attendants;
                    mayBeMoney = dto.MayBeMoney;
                    dates = dto.Dates;

                    if (!TypeOfRestDecode.ContainsKey(typeOfRest))
                    {
                        return new ResultSearch {Hotels = new List<Hotel>(), Count = 0, MayBeMoney = mayBeMoney};
                    }

                    typeOfRest = TypeOfRestDecode[typeOfRest];
                    key = $"{typeOfRest}___True_{dto.Group}";
                }
                else
                {
                    if (!TypeOfRestDecode.ContainsKey(typeOfRest))
                    {
                        return new ResultSearch
                        {
                            Hotels = new List<Hotel>(), Count = 0, Attendants = attendants, Places = places,
                            MayBeMoney = false
                        };
                    }

                    timeOfRests = request.TimeOfRestId.HasValue ? new[] {request.TimeOfRestId} : null;
                    typeOfRest = TypeOfRestDecode[typeOfRest];
                    key =
                        $"{typeOfRest}_{request.TimeOfRestId}_{request.PlaceOfRestId}_False_{(long) RestrictionGroupEnum.NoAccessibleEnvironment}";
                }

                if (!SearchService.ContainsKey(key))
                {
                    return new ResultSearch
                    {
                        Count = 0, Hotels = new List<Hotel>(), Attendants = attendants, Places = places,
                        MayBeMoney = mayBeMoney
                    };
                }

                var result =
                    SearchService[key].Values.Where(h => placeOfRest == null || placeOfRest.Contains(h.PlaceOfRestId))
                        .Select(i =>
                            new Hotel(i, timeOfRests, dates, places, attendants, request.WithBookingDate ?? true, false))
                        .Where(r => r.TimeOfRests.Any())
                        .ToArray();


                if (!result.Any())
                {
                    result =
                        SearchService[key].Values.Where(h => placeOfRest == null || placeOfRest.Contains(h.PlaceOfRestId))
                            .Select(i =>
                                new Hotel(i, timeOfRests, dates, places, attendants, request.WithBookingDate ?? true, true))
                            .Where(r => r.TimeOfRests.Any())
                            .ToArray();
                }

                var minRooms = result.SelectMany(r =>
                    r.TimeOfRests.Select(x => x.CountRooms)).DefaultIfEmpty().Min();

                result = result.Where(r => r.TimeOfRests.Any(x => x.CountRooms <= minRooms)).ToArray();

                foreach (var e in result)
                {
                    e.TimeOfRests = e.TimeOfRests.Where(x => x.CountRooms <= minRooms).ToList();
                }

                return new ResultSearch
                {
                    Count = result.Length,
                    Hotels = result.Skip(request.FirstRow).Take(request.CountRows).ToList(),
                    Attendants = attendants,
                    Places = places,
                    MayBeMoney = mayBeMoney
                };
            }
            finally
            {
                ReleaseService();
            }
        }

        /// <summary>
        ///     добавление места отдыха
        /// </summary>
        internal static void AddService(Hotel hotel)
        {
            LogManager.GetLogger(typeof(Booking))
                .InfoFormat("Добавление места отдыха Id={0}", hotel.NullSafe(h => h.Key));
            Rwl.AcquireWriterLock(WaitInterval);
            try
            {
                AddHotelAndTour(hotel);
                LogManager.GetLogger(typeof(Booking))
                    .InfoFormat("Добавление места отдыха Id={0} завершено", hotel.NullSafe(h => h.Key));
            }
            finally
            {
                Rwl.ReleaseWriterLock();
            }
        }

        /// <summary>
        ///     получение индекса сервера.
        /// </summary>
        internal static int GetServerIndexClient(BaseRequest request)
        {
            var typeOfRestDecoded = TypeOfRestDecode[request.TypeOfRestId ?? (long) TypeOfRestEnum.ChildRestCamps];
            var count = Settings.Default.ServersList.Count;
            if (count == 0)
            {
                count = 1;
            }

            return (int) Math.Abs(typeOfRestDecoded % count);
        }

        public static void CloseClient(IInternalBookingService service)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var clientChannel = service as IClientChannel;
            clientChannel?.Close();
        }

        /// <summary>
        ///     получение клиента
        /// </summary>
        public static IInternalBookingService GetServiceClient(BaseRequest request)
        {
            var index = GetServerIndexClient(request);

            // Security.Logger.SecurityLogger.AddToLog(SecurityJournalEventType.OutSystemsInteractions, "Взаимодействия с порталом МосРу", $"Запрос по заявлению: {request.DocumentNumber}", "", System.ServiceModel.Web.WebOperationContext.Current?.IncomingRequest?.UserAgent);

            if (Settings.Default.ServersList.Count != 0)
            {
                return GetServiceClient(index);
            }

            return null;
        }

        internal static IInternalBookingService GetServiceClient(int index)
        {
            var urlForRequest = string.Format(Settings.Default.ServersList[index]);
            var factory = new ChannelFactory<IInternalBookingService>("InternalBookingService");
            factory.Endpoint.Address = new EndpointAddress(new Uri(urlForRequest));
            var channel = factory.CreateChannel();
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (channel is IClientChannel clientChannel)
            {
                clientChannel.Closed += delegate { factory.Close(); };
            }

            return channel;
        }
    }
}
