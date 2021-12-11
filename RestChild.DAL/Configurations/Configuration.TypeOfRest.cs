﻿using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Цели обращения
        /// </summary>
        private static void TypeOfRest(Context context)
        {
            var parent1 = new TypeOfRest
            {
                Id = (long) TypeOfRestEnum.ChildRest,
                Name = "Детский отдых",
                IsActive = true,
                Parent = null,
                ForMPGU = true,
                ServiceCode = "048001",
                NeedApplicant = true,
                NeedAttendant = false,
                NeedPlace = true,
                NeedPlacment = false,
                NeedSubject = true,
                MinAge = 7,
                MaxAge = 17,
                UrlToRulesOfRest = "regulations.pdf",
                UrlToListRestriction = "contraindications.pdf",
                UrlToRoolAttendant = "conditions.pdf",
                ForTour = true,
                NeedAccomodation = false,
                NeedBookingDate = true,
                HaveAddonService = true,
                HotelTypeId = (long) HotelTypeEnum.Camp
            };

            var parent2 = new TypeOfRest
            {
                Id = (long) TypeOfRestEnum.RestWithParents,
                Name = "Совместный отдых",
                IsActive = true,
                Parent = null,
                ForMPGU = true,
                ServiceCode = "048002",
                NeedApplicant = true,
                NeedAttendant = true,
                NeedPlace = true,
                NeedPlacment = true,
                NeedSubject = false,
                MinAge = 3,
                MaxAge = 17,
                UrlToRulesOfRest = "regulations.pdf",
                UrlToListRestriction = "contraindications.pdf",
                UrlToRoolAttendant = "conditions.pdf",
                ForTour = true,
                FirstRequestCompanySelect = true,
                NeedAccomodation = true,
                NeedBookingDate = true,
                HaveAddonService = true,
                HotelTypeId = (long) HotelTypeEnum.Hotel
            };

            var parent16 = new TypeOfRest
            {
                Id = (long) TypeOfRestEnum.Money,
                Name = "Сертификат на отдых и оздоровление",
                IsActive = true,
                Parent = null,
                ForMPGU = true,
                ServiceCode = "048002",
                NeedApplicant = true,
                NeedAttendant = false,
                NeedPlace = false,
                NeedPlacment = false,
                NeedSubject = false,
                MinAge = 3,
                MaxAge = 15,
                UrlToRulesOfRest = "regulations.pdf",
                UrlToListRestriction = "contraindications.pdf",
                UrlToRoolAttendant = "conditions.pdf",
                ForTour = false,
                NeedAccomodation = false,
                NeedBookingDate = false,
                HaveAddonService = false,
                Eid = (long) TypeOfRestEnum.Money
            };

            var parent4 = new TypeOfRest
            {
                Id = (long) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex,
                Name = "Дети-инвалиды, дети, оставшиеся без попечения родителей, переданные в приемную семью или на патронатное воспитание, совместный отдых детей разных льготных категорий",
                IsActive = true,
                ParentId = (long) TypeOfRestEnum.RestWithParents,
                ForMPGU = true,
                ServiceCode = "048002",
                NeedApplicant = true,
                NeedAttendant = true,
                NeedPlace = true,
                NeedPlacment = true,
                NeedSubject = false,
                MinAge = 3,
                MaxAge = 17,
                UrlToRulesOfRest = "regulations.pdf",
                UrlToListRestriction = "contraindications.pdf",
                UrlToRoolAttendant = "conditions.pdf",
                ForTour = false,
                NeedAccomodation = true,
                NeedBookingDate = true,
                HaveAddonService = true,
                FirstRequestCompanySelect = true,
                HotelTypeId = (long) HotelTypeEnum.Hotel
            };

            var parent5 = new TypeOfRest
            {
                Id = (long) TypeOfRestEnum.CompensationGroup,
                Name = "Компенсация за путевку",
                IsActive = true,
                Parent = null,
                ForMPGU = false,
                ServiceCode = "",
                NeedApplicant = false,
                NeedAttendant = false,
                NeedPlace = false,
                NeedPlacment = false,
                NeedSubject = false,
                MinAge = 3,
                MaxAge = 23,
                ForTour = false,
                NeedAccomodation = false,
                NeedBookingDate = false,
                HaveAddonService = false,
                Eid = (long) TypeOfRestEnum.CompensationGroup
            };

            context.TypeOfRest.AddOrUpdate(t => t.Id,

                #region Льготные путевки

                parent1,
                parent2,
                parent4,
                parent16,
                parent5,
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    Name = "Путевки, за счет средств федерального бюджета (7-17 лет)",
                    IsActive = true,
                    ParentId = parent1.Id,
                    ForMPGU = true,
                    ServiceCode = "048001",
                    NeedApplicant = true,
                    NeedAttendant = false,
                    NeedPlace = true,
                    NeedPlacment = false,
                    NeedSubject = true,
                    MinAge = 7,
                    MaxAge = 17,
                    UrlToRulesOfRest = "regulationsf.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    ForTour = true,
                    NeedAccomodation = false,
                    NeedBookingDate = true,
                    HaveAddonService = true,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.NotCheck,
                    HotelTypeId = (long) HotelTypeEnum.Camp
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.ChildRestCamps,
                    Name = "Детский лагерь, 7-15 лет",
                    IsActive = true,
                    ParentId = parent1.Id,
                    ForMPGU = true,
                    ServiceCode = "048001",
                    NeedApplicant = true,
                    NeedAttendant = false,
                    NeedPlace = true,
                    NeedPlacment = false,
                    NeedSubject = true,
                    MinAge = 7,
                    MaxAge = 15,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    UrlToRulesOfRest = "regulations.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    NeedAccomodation = false,
                    NeedBookingDate = true,
                    HaveAddonService = true,
                    ForTour = true,
                    FirstRequestCompanySelect = true,
                    ServiceCodeFirstCompany = "048003",
                    MayBeMoney = false,
                    HotelTypeId = (long) HotelTypeEnum.Camp
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.ChildRestOrphanCamps,
                    Name =
                        "Детский лагерь для детей-сирот и детей, оставшихся без попечения родителей, находящихся под опекой, попечительством, в том числе в приемной или патронатной семье, 7-17 лет",
                    IsActive = true,
                    ParentId = parent1.Id,
                    ForMPGU = true,
                    ServiceCode = "048001",
                    NeedApplicant = true,
                    NeedAttendant = false,
                    NeedPlace = true,
                    NeedPlacment = false,
                    NeedSubject = true,
                    MinAge = 7,
                    MaxAge = 17,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    UrlToRulesOfRest = "regulations.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    NeedAccomodation = false,
                    NeedBookingDate = true,
                    HaveAddonService = true,
                    ForTour = true,
                    FirstRequestCompanySelect = true,
                    ServiceCodeFirstCompany = "048003",
                    HotelTypeId = (long) HotelTypeEnum.Camp
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.TentChildrenCamp,
                    Name = "Палаточный детский лагерь, 7-15 лет",
                    IsActive = false,
                    ParentId = parent1.Id,
                    ForMPGU = false,
                    ServiceCode = "048001",
                    NeedApplicant = true,
                    NeedAttendant = false,
                    NeedPlace = true,
                    NeedPlacment = false,
                    NeedSubject = true,
                    MinAge = 7,
                    MaxAge = 15,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    UrlToRulesOfRest = "regulations.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    NeedAccomodation = false,
                    NeedBookingDate = true,
                    HaveAddonService = true,
                    ForTour = true,
                    FirstRequestCompanySelect = true,
                    ServiceCodeFirstCompany = "048003",
                    MayBeMoney = false,
                    HotelTypeId = (long) HotelTypeEnum.Camp
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.TentChildrenCampOrphan,
                    Name =
                        "Палаточный детский лагерь для детей-сирот и детей, оставшихся без попечения родителей, находящихся под опекой, попечительством, в том числе в приемной или патронатной семье, 7-17 лет",
                    IsActive = false,
                    ParentId = parent1.Id,
                    ForMPGU = false,
                    ServiceCode = "048001",
                    NeedApplicant = true,
                    NeedAttendant = false,
                    NeedPlace = true,
                    NeedPlacment = false,
                    NeedSubject = true,
                    MinAge = 7,
                    MaxAge = 17,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    UrlToRulesOfRest = "regulations.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    NeedAccomodation = false,
                    NeedBookingDate = true,
                    HaveAddonService = true,
                    ForTour = true,
                    FirstRequestCompanySelect = true,
                    ServiceCodeFirstCompany = "048003",
                    HotelTypeId = (long) HotelTypeEnum.Camp
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.YouthRestCamps,
                    Name = "Молодёжный отдых",
                    IsActive = true,
                    ParentId = null,
                    ForMPGU = true,
                    ServiceCode = "048001",
                    NeedApplicant = true,
                    NeedAttendant = false,
                    NeedPlace = true,
                    NeedPlacment = false,
                    NeedSubject = true,
                    MinAge = 18,
                    MaxAge = 23,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    UrlToRulesOfRest = "regulations.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    NeedAccomodation = false,
                    NeedBookingDate = true,
                    HaveAddonService = true,
                    ForTour = true,
                    FirstRequestCompanySelect = false,
                    ServiceCodeFirstCompany = "048003",
                    NotChildren = true,
                    HotelTypeId = (long) HotelTypeEnum.Camp
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.YouthRestOrphanCamps,
                    Name =
                        "Молодёжный отдых для лиц из числа детей-сирот и детей, оставшихся без попечения родителей, 18-23 лет",
                    IsActive = true,
                    ParentId = (long) TypeOfRestEnum.YouthRestCamps,
                    ForMPGU = true,
                    ServiceCode = "048001",
                    NeedApplicant = true,
                    NeedAttendant = false,
                    NeedPlace = true,
                    NeedPlacment = false,
                    NeedSubject = false,
                    MinAge = 18,
                    MaxAge = 23,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    UrlToRulesOfRest = "regulations.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    NeedAccomodation = true,
                    NeedBookingDate = true,
                    HaveAddonService = true,
                    ForTour = true,
                    FirstRequestCompanySelect = true,
                    ServiceCodeFirstCompany = "048003",
                    NotChildren = true,
                    HotelTypeId = (long) HotelTypeEnum.Hotel
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.RestWithParentsPoor,
                    Name = "Совместный отдых для детей из малообеспеченных семей, 3-7 лет",
                    IsActive = true,
                    ParentId = parent2.Id,
                    ForMPGU = true,
                    ServiceCode = "048002",
                    NeedApplicant = true,
                    NeedAttendant = true,
                    NeedPlace = true,
                    NeedPlacment = true,
                    NeedSubject = false,
                    MinAge = 3,
                    MaxAge = 7,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    UrlToRulesOfRest = "regulations.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    NeedAccomodation = true,
                    ForTour = false,
                    NeedBookingDate = true,
                    HaveAddonService = true,
                    FirstRequestCompanySelect = true,
                    ServiceCodeFirstCompany = "048003",
                    MayBeMoney = false,
                    HotelTypeId = (long) HotelTypeEnum.Hotel,
                    NeedTypeOfTransport = true
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.RestWithParentsInvalid,
                    Name =
                        "Совместный отдых для детей-инвалидов, детей с ограниченными возможностями здоровья, 4-17 лет",
                    IsActive = true,
                    ParentId = parent4.Id,
                    ForMPGU = true,
                    ServiceCode = "048002",
                    NeedApplicant = true,
                    NeedAttendant = true,
                    NeedPlace = true,
                    NeedPlacment = true,
                    NeedSubject = false,
                    MinAge = 4,
                    MaxAge = 17,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    UrlToRulesOfRest = "regulations.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    NeedAccomodation = true,
                    HaveAddonService = true,
                    NeedBookingDate = true,
                    FirstRequestCompanySelect = true,
                    ForTour = false,
                    ServiceCodeFirstCompany = "048003",
                    HotelTypeId = (long) HotelTypeEnum.Hotel,
                    MayBeMoney = false,
                    NeedTypeOfTransport = true
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.RestWithParentsOrphan,
                    Name =
                        "Совместный отдых для детей-сирот и детей, оставшихся без попечения родителей, находящихся под опекой, попечительством, в том числе в приемной или патронатной семье, 3-17 лет",
                    IsActive = true,
                    ParentId = parent4.Id,
                    ForMPGU = true,
                    ServiceCode = "048002",
                    NeedApplicant = true,
                    NeedAttendant = true,
                    NeedPlace = true,
                    NeedPlacment = true,
                    NeedSubject = false,
                    MinAge = 3,
                    MaxAge = 17,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    UrlToRulesOfRest = "regulations.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    NeedAccomodation = true,
                    HaveAddonService = true,
                    FirstRequestCompanySelect = true,
                    NeedBookingDate = true,
                    ForTour = false,
                    ServiceCodeFirstCompany = "048003",
                    HotelTypeId = (long) HotelTypeEnum.Hotel,
                    NeedTypeOfTransport = true
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.RestWithParentsComplex,
                    Name =
                        "Совместный отдых для детей разных льготных категорий, воспитывающихся в одной семье",
                    IsActive = true,
                    ParentId = parent4.Id,
                    ForMPGU = true,
                    ServiceCode = "048002",
                    NeedApplicant = true,
                    NeedAttendant = true,
                    NeedPlace = true,
                    NeedPlacment = true,
                    NeedSubject = false,
                    MinAge = 3,
                    MaxAge = 17,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    UrlToRulesOfRest = "regulations.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    NeedAccomodation = true,
                    HaveAddonService = true,
                    FirstRequestCompanySelect = true,
                    ServiceCodeFirstCompany = "048003",
                    NeedBookingDate = true,
                    ForTour = false,
                    MayBeMoney = false,
                    HotelTypeId = (long) HotelTypeEnum.Hotel,
                    NeedTypeOfTransport = true
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.RestWithParentsOther,
                    Name =
                        "Прочие виды льгот (0-17)",
                    IsActive = true,
                    ParentId = (long) TypeOfRestEnum.RestWithParents,
                    ForMPGU = false,
                    ServiceCode = "048002",
                    NeedApplicant = true,
                    NeedAttendant = true,
                    NeedPlace = true,
                    NeedPlacment = true,
                    NeedSubject = false,
                    MinAge = 0,
                    MaxAge = 17,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    NeedAccomodation = true,
                    ForTour = true,
                    NeedBookingDate = true,
                    HaveAddonService = true,
                    HotelTypeId = (long) HotelTypeEnum.Hotel
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.Compensation,
                    Name =
                        "Компенсация за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления",
                    IsActive = true,
                    ParentId = parent5.Id,
                    ForMPGU = false,
                    ServiceCode = "",
                    NeedApplicant = true,
                    NeedAttendant = true,
                    NeedPlace = false,
                    NeedPlacment = false,
                    NeedSubject = false,
                    MinAge = 3,
                    MaxAge = 17,
                    ForTour = false,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    Eid = (long) TypeOfRestEnum.Compensation
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.CompensationYouthRest,
                    Name =
                        "Компенсация за путевку лицу из числа детей-сирот и детей, оставшихся без попечения родителей",
                    IsActive = true,
                    ParentId = parent5.Id,
                    ForMPGU = false,
                    ServiceCode = "",
                    NotChildren = true,
                    NeedApplicant = true,
                    NeedAttendant = true,
                    NeedPlace = false,
                    NeedPlacment = false,
                    NeedSubject = false,
                    MinAge = 18,
                    MaxAge = 23,
                    ForTour = false,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    Eid = (long) TypeOfRestEnum.CompensationYouthRest
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.SpecializedСamp,
                    Name =
                        "Профильные лагеря и сироты",
                    IsActive = true,
                    ParentId = null,
                    ForMPGU = false,
                    ServiceCode = "",
                    NeedApplicant = true,
                    NeedAttendant = true,
                    NeedPlace = true,
                    NeedPlacment = true,
                    NeedSubject = false,
                    MinAge = 3,
                    MaxAge = 17,
                    ForTour = true,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForSpecializedCamps,
                    HaveAddonService = true,
                    HotelTypeId = (long) HotelTypeEnum.Camp
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.SpecializedСampFamily,
                    Name =
                        "Отдых для сирот (совместный отдых)",
                    IsActive = true,
                    ParentId = null,
                    ForMPGU = false,
                    ServiceCode = "",
                    NeedApplicant = true,
                    NeedAttendant = true,
                    NeedPlace = true,
                    NeedPlacment = true,
                    NeedSubject = false,
                    MinAge = 3,
                    MaxAge = 21,
                    ForTour = true,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForSpecializedCamps,
                    HaveAddonService = true,
                    HotelTypeId = (long) HotelTypeEnum.Hotel
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.MoneyOn3To7,
                    Name = "Сертификат для детей из малообеспеченных семей, 3-7 лет, и сопровождающих лиц",
                    IsActive = true,
                    ParentId = parent16.Id,
                    ForMPGU = true,
                    ServiceCode = "048002",
                    NeedApplicant = true,
                    NeedAttendant = true,
                    NeedPlace = false,
                    NeedPlacment = false,
                    NeedSubject = false,
                    MinAge = 3,
                    MaxAge = 7,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    UrlToRulesOfRest = "regulations.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    NeedAccomodation = false,
                    HaveAddonService = false,
                    NeedBookingDate = false,
                    FirstRequestCompanySelect = true,
                    ForTour = false,
                    ServiceCodeFirstCompany = "048003",
                    MayBeMoney = false,
                    Eid = (long) TypeOfRestEnum.MoneyOn3To7
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.MoneyOn7To15,
                    Name = "Сертификат на отдых и оздоровление ребёнка, 7-15 лет",
                    IsActive = true,
                    ParentId = parent16.Id,
                    ForMPGU = true,
                    ServiceCode = "048002",
                    NeedApplicant = true,
                    NeedAttendant = false,
                    NeedPlace = false,
                    NeedPlacment = false,
                    NeedSubject = false,
                    MinAge = 7,
                    MaxAge = 15,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    UrlToRulesOfRest = "regulations.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    NeedAccomodation = false,
                    HaveAddonService = false,
                    NeedBookingDate = false,
                    FirstRequestCompanySelect = true,
                    ForTour = false,
                    ServiceCodeFirstCompany = "048003",
                    MayBeMoney = false,
                    Eid = (long) TypeOfRestEnum.MoneyOn7To15
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.MoneyOnInvalidOn4To17,
                    Name = "Сертификат для детей-инвалидов, детей с ограниченными возможностями здоровья, 4-17 лет, и сопровождающих лиц",
                    IsActive = true,
                    ParentId = parent16.Id,
                    ForMPGU = true,
                    ServiceCode = "048002",
                    NeedApplicant = true,
                    NeedAttendant = true,
                    NeedPlace = false,
                    NeedPlacment = false,
                    NeedSubject = false,
                    MinAge = 4,
                    MaxAge = 17,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    UrlToRulesOfRest = "regulations.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    NeedAccomodation = false,
                    HaveAddonService = false,
                    NeedBookingDate = false,
                    FirstRequestCompanySelect = true,
                    ForTour = false,
                    ServiceCodeFirstCompany = "048003",
                    MayBeMoney = false,
                    Eid = (long) TypeOfRestEnum.MoneyOnInvalidOn4To17
                },
                new TypeOfRest
                {
                    Id = (long) TypeOfRestEnum.MoneyOn18,
                    Name = "Сертификат для лиц из числа детей сирот",
                    IsActive = false,
                    ParentId = parent16.Id,
                    ForMPGU = false,
                    ServiceCode = "048002",
                    NeedApplicant = true,
                    NeedAttendant = false,
                    NeedPlace = false,
                    NeedPlacment = false,
                    NeedSubject = false,
                    MinAge = 18,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForOneYear,
                    UrlToRulesOfRest = "regulations.pdf",
                    UrlToListRestriction = "contraindications.pdf",
                    UrlToRoolAttendant = "conditions.pdf",
                    NeedAccomodation = false,
                    HaveAddonService = false,
                    NeedBookingDate = false,
                    FirstRequestCompanySelect = true,
                    ForTour = false,
                    ServiceCodeFirstCompany = "048003",
                    MayBeMoney = true,
                    NotChildren = true,
                    Eid = (long) TypeOfRestEnum.MoneyOn18
                }

                #endregion

            );
            context.SaveChanges();

            SetEidAndLastUpdateTicks(context.TypeOfRest.ToList());
            context.SaveChanges();

            if (context.Tour.Any(t => t.TypeOfRestId == (long) TypeOfRestEnum.ChildRest))
            {
                context.Database.ExecuteSqlCommand($"Update [dbo].[Tour] Set [TypeOfRestId] = {(long) TypeOfRestEnum.ChildRestCamps} Where [TypeOfRestId] = {(long) TypeOfRestEnum.ChildRest}");
            }
        }
    }
}
