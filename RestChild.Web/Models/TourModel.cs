using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;

namespace RestChild.Web.Models
{
    /// <summary>
    /// модель размещения
    /// </summary>
    public class TourModel : ViewModelBase<Tour>
    {
        /// <summary>
        /// конструктор
        /// </summary>
        public TourModel() : base(new Tour())
        {
            Services = new List<TourServiceModel>();
        }

        /// <summary>
        /// конструктор
        /// </summary>
        public TourModel(Tour tour, bool isEditable, bool isServiceEditable)
            : base(tour)
        {
            RoomRates =
                (tour.RoomRates == null ? new List<RoomRates>() : tour.RoomRates.ToList()).OrderBy(
                    rr => rr.NullSafe(t => t.TypeOfRooms.Name))
                .ThenBy(rr => rr.NullSafe(t => t.Accommodation.Name))
                .ThenBy(rr => rr.Id)
                .ToList();

            IsEditable = isEditable;
            IsServiceEditable = isServiceEditable;
            Services = tour.Services?.Where(s => s.IsActive).Where(s => !s.LinkServiceId.HasValue)
                           .Select(s => new TourServiceModel(s, isServiceEditable)).ToList() ??
                       new List<TourServiceModel>();

            RestrictionGroupId = tour.RestrictionGroupId ?? (tour.ForInvalid
                                     ? (long) RestrictionGroupEnum.WithAnAccessibleEnvironment
                                     : (long) RestrictionGroupEnum.NoAccessibleEnvironment);
        }

        /// <summary>
        ///     Статус
        /// </summary>
        public ViewModelState State { get; set; }

        /// <summary>
        ///     Идентификатор перехода
        /// </summary>
        public string StateMachineActionString { get; set; }

        public bool IsEditable { get; protected set; }

        /// <summary>
        /// группа ограничений
        /// </summary>
        public long? RestrictionGroupId { get; set; }

        public bool IsServiceEditable { get; protected set; }

        public string ActiveTab { get; set; }

        [AllowHtml] public string Memo { get; set; }

        public IList<TourServiceModel> Services { get; set; }

        public IList<RoomRates> RoomRates { get; set; }

        /// <summary>
        /// группы ограничений
        /// </summary>
        public IList<RestrictionGroup> GroupRestrictions { get; set; }

        /// <summary>
        ///     прочие заезды в другие даты
        /// </summary>
        public List<Tour> OtherToursInYear { get; set; }

        public override Tour BuildData()
        {
            Data.Name = Data.Name ?? Data.NullSafe(d => d.ToString()) ?? "-";
            Data.RoomRates = RoomRates;
            Data.RestrictionGroupId = RestrictionGroupId ?? (long) RestrictionGroupEnum.NoAccessibleEnvironment;
            Data.Services = Services?.Where(s => s.Data.IsActive).Select(s => s.BuildData()).ToList() ??
                            new List<AddonServices>();
            foreach (var ls in Data.Services.ToList())
            {
                Data.Services.AddRange(ls.LinkServices);
                ls.LinkServices = null;
            }

            return Data;
        }

        public void LoadCollections(WebToursController apiController)
        {
            if (Data != null)
            {
                Data.ChildLists = apiController.GetChildLists(Data.Id);
                Data.Bookings = apiController.GetBookings(Data.Id);
                Data.Childs = apiController.GetChilds(Data.Id);
            }
        }

        #region справочники

        public ICollection<TypeOfRest> TypesOfRest { get; set; }

        public ICollection<TimeOfRest> TimesOfRest { get; set; }

        public ICollection<SubjectOfRest> SubjectsOfRest { get; set; }

        public ICollection<YearOfRest> YearsOfRest { get; set; }

        public ICollection<LimitOnVedomstvo> LimitsOnVedomstvo { get; set; }

        /// <summary>
        ///     виды комнат
        /// </summary>
        public IList<TypeOfRooms> TypeOfRooms { get; set; }

        /// <summary>
        ///     размещения
        /// </summary>
        public IList<Accommodation> Accommodations { get; set; }

        /// <summary>
        ///     Варианты питания
        /// </summary>
        public IList<DiningOptions> DiningOptions { get; set; }

        #endregion
    }
}
