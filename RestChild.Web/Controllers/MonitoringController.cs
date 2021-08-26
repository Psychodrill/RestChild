using RestChild.Comon;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models.Monitoring;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     Мониторинг
    /// </summary>
    [Authorize]
    public partial class MonitoringController : BaseController
    {
        public WebMonitoringController ApiController { get; set; }

        public StateController ApiStateController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiController.SetUnitOfWorkInRefClass(unitOfWork);
        }


        /// <summary>
        ///     Форма выгрузки сводных форм
        /// </summary>
        [HttpGet]
        [Route("CompleteForms")]
        public ActionResult CompleteForms()
        {
            var model = new FilledFormModel
            {
                YearsOfRest = UnitOfWork.GetSet<YearOfRest>().OrderBy(y=>y.Year).ToArray()
            };

            var item = model.YearsOfRest.FirstOrDefault(y => y.Year == DateTime.Today.Year) ?? model.YearsOfRest.LastOrDefault();

            if (item != null)
            {
                model.YearOfRest = item.Id;
            }

            return View(model);
        }

        /// <summary>
        ///     Запись в историю
        /// </summary>
        private void WriteToHistory(long? historyLinkId, string message, string description, long? toStateId,
            long? fromStateId, long? signInfoId)
        {
            UnitOfWork.AddEntity(new History
            {
                AccountId = Security.GetCurrentAccountId(),
                EventCode = message,
                DateChange = DateTime.Now,
                Commentary = description,
                ToStateId = toStateId,
                FromStateId = fromStateId,
                SignInfoId = signInfoId,
                LastUpdateTick = DateTime.Now.Ticks,
                LinkId = historyLinkId
            });
        }

        /// <summary>
        ///     Разослать уведомления
        /// </summary>
        private void SendEmailSend(string accessRight, DateTime? sendEventDate, string message, string title)
        {
            var users = UnitOfWork.GetSet<Account>().Where(ss => ss.Email != null && ss.IsActive && !ss.IsDeleted && ss.Rights.Any(sx => sx.AccessRight.Code == accessRight)).ToList();

            foreach(var u in users)
            {
                var s = new SendEmailAndSms
                {
                    DateToSend = sendEventDate ?? DateTime.Now,
                    DateCreate = DateTime.Now,
                    Email = u.Email,
                    EmailTitle = title,
                    EmailMessage = message
                };

                UnitOfWork.AddEntity(s);
            }
        }
    }
}
