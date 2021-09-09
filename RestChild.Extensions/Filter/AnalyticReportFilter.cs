using System;
using System.Collections.Generic;
using RestChild.Comon.ToExcel;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    /// <summary>
    ///     фильтр для аналитических отчетов
    /// </summary>
    public class AnalyticReportFilter
    {
        public string ReportType { get; set; }
        public string ReportName { get; set; }
        public long? HotelId { get; set; }
        public string HotelName { get; set; }
        public long? TimeOfRestId { get; set; }
        public string TimeOfRest { get; set; }
        public long? YearOfRestId { get; set; }
        public string YearOfRest { get; set; }
        public bool NextYearsIncluded { get; set; }
        public ICollection<YearOfRest> YearsOfRest { get; set; }
        public ICollection<City> Cities { get; set; }
        public ICollection<TypeOfTransport> TypeOfTransports { get; set; }
        public ICollection<TypeOfRest> TypeOfRests { get; set; }
        public DateTime? DateStartBegin { get; set; }
        public DateTime? DateStartEnd { get; set; }
        public ICollection<Status> Statuses { get; set; }
        public long? StatusId { get; set; }

        /// <summary>
        ///     Дата начала формирования отчета
        /// </summary>
        public DateTime? DateFormingBegin { get; set; }

        /// <summary>
        ///     Дата окончания формирования отчета
        /// </summary>
        public DateTime? DateFormingEnd { get; set; }

        public int? YearOfBirthDateBegin { get; set; }
        public int? YearOfBirthDateEnd { get; set; }
        public long? DistrictId { get; set; }
        public long? BenefitTypeId { get; set; }
        public long? PlaceOfRestId { get; set; }
        public string PlaceOfRest { get; set; }
        public long? AgencyId { get; set; }
        public string Agency { get; set; }
        public long? OrganizationId { get; set; }
        public string Organization { get; set; }
        public long? SupplierId { get; set; }
        public string Supplier { get; set; }
        public BaseExcelTable Data { get; set; }
        public string ActionName { get; set; }
        public string BenefitType { get; set; }
        public string District { get; set; }

        /// <summary>
        ///     Место прибытия
        /// </summary>
        public virtual long? ArrivalId { get; set; }

        public virtual string Arrival { get; set; }

        /// <summary>
        ///     Место отправления
        /// </summary>
        public virtual long? DepartureId { get; set; }

        public virtual string Departure { get; set; }
        public long? TypeOfRestId { get; set; }
        public string TypeOfRest { get; set; }

        public string FlightNumber { get; set; }

        public long? TypeOfTransportId { get; set; }

        public string TypeOfTransport { get; set; }

        //Видимость фильтров
        public bool TimeOfRestVisibility { get; set; }
        public bool AgencyVisibility { get; set; }
        public bool VedomstvoVisibility { get; set; }
        public bool SupplierVisibility { get; set; }
        public bool PlaceOfRestVisibility { get; set; }
        public bool HotelVisibility { get; set; }
        public bool DateStartVisibility { get; set; }
        public bool YearOfBirthVisibility { get; set; }
        public bool BenefitVisibility { get; set; }
        public bool DistrictVisibility { get; set; }
        public bool ArrivalVisibility { get; set; }
        public bool DepartureVisibility { get; set; }
        public bool TypeOfRestVisibility { get; set; }
        public bool FlightNumberVisibility { get; set; }
        public bool TypeOfTransportVisibility { get; set; }

        /// <summary>
        ///     Показывать возможность выбора опции "и последующие годы"
        /// </summary>
        public bool NextYearsIncludedVisibility { get; set; }

        /// <summary>
        ///     Показывать выбор даты формирования отчета
        /// </summary>
        public bool DateFormingVisibility { get; set; }
    }
}
