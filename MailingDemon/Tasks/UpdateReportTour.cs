using System;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
    [Task]
    public class UpdateReportTour : BaseTask
    {
        protected override void Execute()
        {
            try
            {
                var exec = new BaseUpdateTour(new Guid(AccessRightEnum.Report.TourReport), TypeOfRestEnum.ChildRest,
                    1000, "Индивидуальный");
                exec.UpdateTourReport();
            }
            catch (Exception ex)
            {
                Logger.Error("UpdateReportTour TypeOfRestEnum.ChildRest", ex);
            }

            try
            {
                var exec = new BaseUpdateTour(new Guid(AccessRightEnum.Report.TourReport),
                    TypeOfRestEnum.RestWithParents, 10000, "Совместный");
                exec.UpdateTourReport();
            }
            catch (Exception ex)
            {
                Logger.Error("UpdateReportTour TypeOfRestEnum.RestWithParents", ex);
            }


            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    var organizationsWithSpecializedCamps = unitOfWork.GetSet<ListOfChilds>()
                        .Where(c => !c.IsDeleted && c.StateId != StateMachineStateEnum.Deleted)
                        .GroupBy(l => l.LimitOnOrganization.LimitOnVedomstvo.Organization).Select(g => g.Key).ToList();
                    var firsttableKey = 20000;
                    var tableKey = firsttableKey;
                    var isFirstTable = true;

                    foreach (var organisation in organizationsWithSpecializedCamps)
                    {
                        var exec = new BaseUpdateTour(new Guid(AccessRightEnum.Report.TourReport),
                            TypeOfRestEnum.SpecializedСamp, tableKey, "Профильный", firsttableKey, !isFirstTable,
                            organisation.Id, organisation.Name, "tour-report-table specialized-tour-report-table");
                        exec.UpdateTourReport();
                        tableKey += 1000;
                        isFirstTable = false;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("UpdateReportTour TypeOfRestEnum.SpecializedСamp", ex);
                }
            }
        }
    }
}
