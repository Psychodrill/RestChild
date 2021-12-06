using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Migrations;
using RestChild.Comon;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     отцепить все изменения
        /// </summary>
        private static void DetachAllEntitys(DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
        }

        private static void SetEidAndLastUpdateTicks(IEnumerable<IEntityBase> entries)
        {
            foreach (var e in entries)
            {
                e.Eid = e.Id;
                e.LastUpdateTick = DateTime.Now.Ticks;
            }
        }

        /// <summary>
        ///     обновление справочников
        /// </summary>
        //public static void Seed(Context context)
        //{
        //    DetachAllEntitys(context);

        //    TypeViolation(context);

        //    CategoryIncidents(context);

        //    BoutJournalType(context);

        //    TieColor(context);

        //    AccessRight(context);

        //    StateMachine(context);

        //    StateMachineState(context);

        //    TypeOfTransport(context);

        //    TypeOfCamp(context);

        //    TypeOfTransportInRequest(context);

        //    TypeOfLinkPeople(context);

        //    TypeOfService(context);

        //    TypeOfGroupCheck(context);

        //    TypeOfRest(context);

        //    TypeRequestInformationVoucher(context);

        //    Status(context);

        //    StatusAction(context);

        //    Beneficiaries(context);

        //    RestrictionGroup(context);

        //    TypeOfRestriction(context);

        //    BenefitType(context);

        //    TypeOfRestBenefitRestriction(context);

        //    ReportTable(context);

        //    ReportTableRow(context);

        //    ReportSheet(context);

        //    Source(context);

        //    NotNeedTicketReason(context);

        //    TypeOfCalculation(context);

        //    DocumentType(context);

        //    StateMachineAction(context);

        //    StateMachineFromStatus(context);

        //    DeclineReason(context);

        //    FileTypes(context);

        //    ExchangeBaseRegistryType(context);

        //    RequestFileType(context);

        //    TradeUnionStatusByChild(context);

        //    RequestStatusChainForMpgu(context);

        //    ApplicantType(context);

        //    StatusByChild(context);

        //    Reports(context);

        //    TypeOfLimitList(context);

        //    RepresentInterest(context);

        //    TypeOfTransfer(context);

        //    MgtVisitBookingPersonType(context);

        //    SecurityJournalType(context);

        //    MgtDepartment(context);

        //    MgtVisitBookingVisitStatus(context);

        //    MgtVisitTargets(context);

        //    SecuritySettings(context);

        //    OrganizationCollaboratorPostType(context);

        //    FormOfRest(context);

        //    TypeOfDrug(context);

        //    PupilGroupVacationPeriod(context);

        //    TypeOfRestERL(context);

        //    BenefitTypeERL(context);

        //    BtiDistrict(context);

        //    SmallLeisureSubtype(context);

        //    MonitoringFinancialSource(context);

        //    TradeUnionCamperPrivilegePart(context);
        //}
    }
}
