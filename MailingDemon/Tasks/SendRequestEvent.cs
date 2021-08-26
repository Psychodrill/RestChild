using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using RestChild.Comon;
using RestChild.DAL;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     обработка событий для заявлений
    /// </summary>
    [Task]
    public class SendRequestEvent : BaseTask
    {
        [XmlElement("config")] public ConfigBalance Config { get; set; }

        /// <summary>
        ///     отправка статусов
        /// </summary>
        protected override void Execute()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var events = unitOfWork.GetSet<RequestEventPlanied>().Where(e => !e.Processed).Take(1000).ToList();
                foreach (var e in events)
                {
                    var action =
                        unitOfWork
                            .GetSet<StatusAction>()
                            .FirstOrDefault(a => a.Code.ToLower() == e.EventCode.ToString().ToLower());

                    if (action != null)
                    {
                        var status = action.FromStatus.Select(s => (long?) s.Id).ToList();
                        var request = unitOfWork.GetSet<Request>()
                            .Where(r => status.Contains(r.StatusId) && !r.IsDeleted);

                        // если идет проверка в БР то менять статус нельзя.
                        request = request.Where(r => !r.NeedSendForBenefit
                                                     && !r.NeedSendToRelative
                                                     && !r.NeedSendForSnils
                                                     && !r.NeedSendForPassport
                                                     && !r.NeedSendForParent
                                                     && !r.NeedSendForCPMPK
                                                     && !r.Child.Any(c =>
                                                           c.BaseRegistryInfo.Any(b =>
                                                               !b.IsProcessed && !b.NotActual))
                                                     && !r.Attendant.Any(c =>
                                                           c.BaseRegistryInfo.Any(b =>
                                                               !b.IsProcessed && !b.NotActual))
                                                     && !r.Applicant.BaseRegistryInfo.Any(b =>
                                                           !b.IsProcessed && !b.NotActual));

                        if (action.IsFirstCompany.HasValue)
                        {
                            request = request.Where(r => r.IsFirstCompany == action.IsFirstCompany);
                        }

                        if (request.LongCount() == 0)
                        {
                            if (!e.PlanDate.HasValue || e.PlanDate < DateTime.Now)
                            {
                                e.Processed = true;
                                e.DateEvent = DateTime.Now;
                            }

                            unitOfWork.SaveChanges();
                            return;
                        }

                        if (Config != null && Config.CountNodes > 0)
                        {
                            request = request.Where(r => r.Id % Config.CountNodes == Config.IndexNode);
                        }

                        var requestIds = request.OrderBy(r => r.DateRequest).ThenBy(r => r.Id).Select(r => r.Id)
                            .Take(10000).ToArray();

                        foreach (var id in requestIds)
                        {
                            try
                            {
                                var req =
                                    WebRequest.Create(string.Format(ConfigurationManager.AppSettings["sendEventUrl"],
                                        id, action.Code, e.AccountId, e.PlanDate.DateTimeToXml()));
                                using (var res = req.GetResponse())
                                {
                                    res.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.Error("SendRequestEvent error", ex);
                            }
                        }
                    }
                }
            }
        }
    }
}
