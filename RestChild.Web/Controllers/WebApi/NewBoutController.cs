using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json;
using RestChild.Comon.Dto.Commercial;
using RestChild.Comon.Enumeration;
using RestChild.Mobile.DAL.Enum;
using RestChild.Mobile.DAL.RepositoryExtensions;
using RestChild.Mobile.Domain;
using RestChild.Web.Models.NewBout;
using RestChild.Web.Models.Task;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    ///     контроллер для получение данных по объектам
    /// </summary>
    [Authorize]
    public class NewBoutController : BaseMobileController
    {
        /// <summary>
        ///     получение списка лагерей
        /// </summary>
        [HttpPost]
        [HttpGet]
        public IList<Camp> GetCamps(string query)
        {
            var q = MobileUw.GetSet<Camp>().Where(x => x.StateId != StateMachineStateEnum.Deleted);

            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToLower().Trim();
                q = q.Where(x => x.Name.ToLower().Contains(query));
            }

            var res = q.OrderBy(x => x.Name.Length).ThenBy(x => x.Name)
                .Take(Settings.Default.WebBtiStreetsResponseCount)
                .ToList().Select(l => new Camp {Id = l.Id, Name = l.Name}).ToList();

            return res;
        }

        /// <summary>
        ///     установка связи вожатого и пользователя
        /// </summary>
        [HttpPost]
        public void LinkPersonal(long uid, string k)
        {
            if (!Security.HasRight(AccessRightEnum.NewBout.View))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (string.IsNullOrWhiteSpace(k))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var personal = MobileUw.GetById<BoutPersonal>(uid);
            var account = MobileUw.GetSet<Account>().FirstOrDefault(a => a.AccountKey == k);

            if (account == null || personal?.Personal == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            personal.Personal.AccountId = account.Id;
            account.Male = personal.Personal.Male;

            var link = MobileUw.WriteHistory(account.Link, "Установка связи пользователь-сотрудник",
                $"Установка связи пользователь сотрудник с кодом {k}.",
                Security.GetCurrentAccountId());
            if (account.Link == null)
            {
                account.LinkId = link.Id;
            }

            MobileUw.SaveChanges();
        }


        /// <summary>
        ///     отказ от подарка
        /// </summary>
        [HttpPost]
        public void GiftCancel(long uid)
        {
            if (!Security.HasRight(AccessRightEnum.GiftReserved.View))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            long?[] validStates = {StateEnum.GiftReserved.Reserved, StateEnum.GiftReserved.Issued};

            var giftReserved = MobileUw.GetById<GiftReserved>(uid);

            if (giftReserved == null || !validStates.Contains(giftReserved.StateId))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var link = MobileUw.WriteHistory(giftReserved.Link, "Отмена выдачи подарка", "Отмена выдачи подарка.",
                giftReserved.StateId, StateEnum.GiftReserved.Canceled, Security.GetCurrentAccountId());
            if (giftReserved.Link == null)
            {
                giftReserved.LinkId = link.Id;
            }

            giftReserved.StateId = StateEnum.GiftReserved.Canceled;
            if (giftReserved.Owner != null)
            {
                giftReserved.Owner.PointsOnAccount += giftReserved.Price;
            }

            MobileUw.SaveChanges();
        }

        /// <summary>
        ///     выдача подарка
        /// </summary>
        [HttpPost]
        public void GiftIssued(long uid, string code)
        {
            if (!Security.HasRight(AccessRightEnum.GiftReserved.View))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var giftReserved = MobileUw.GetById<GiftReserved>(uid);

            if (giftReserved == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (giftReserved.AprovalCode != code)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var link = MobileUw.WriteHistory(giftReserved.Link, "Выдача подарка", "Выдача подарка.",
                giftReserved.StateId, StateEnum.GiftReserved.Issued, Security.GetCurrentAccountId());
            if (giftReserved.Link == null)
            {
                giftReserved.LinkId = link.Id;
            }

            giftReserved.StateId = StateEnum.GiftReserved.Issued;
            MobileUw.SaveChanges();
        }

        /// <summary>
        ///     выдача подарка
        /// </summary>
        [HttpPost]
        public void GiftRequestToIssued(long uid)
        {
            if (!Security.HasRight(AccessRightEnum.GiftReserved.View))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var giftReserved = MobileUw.GetById<GiftReserved>(uid);

            if (giftReserved == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var rnd = new Random();

            var code = rnd.Next(10000, 99999);

            var link = MobileUw.WriteHistory(giftReserved.Link, "Запрос на выдачу подарка", "Запрос на выдачу подарка.",
                Security.GetCurrentAccountId());
            if (giftReserved.Link == null)
            {
                giftReserved.LinkId = link.Id;
            }

            giftReserved.AprovalCode = code.ToString();
            giftReserved.CodeSendDate = DateTime.Now;

            MobileUw.AddEntity(new SendEmailAndSms
            {
                Email = giftReserved.Owner.Email,
                Phone = giftReserved.Owner.Phone,
                DateCreate = DateTime.Now,
                EmailTitle = "Код подтверждения",
                EmailMessage = $"Код подтверждения {code}",
                SmsMessage = $"Код подтверждения {code}",

                //чтобы отправилось только смс
                IsEmailSended = true
            });

            MobileUw.SaveChanges();
        }

        /// <summary>
        ///     Получить смены доступные для данного места отдыха
        /// </summary>
        [HttpPost]
        public IList<BaseResponse> GetChanges(long? campId)
        {
            var changes = MobileUw.GetSet<Bout>().Where(x => x.CampId == campId).OrderByDescending(a => a.DateIncome).Select(ss => new BaseResponse
            {
                Id = new long(),
                Name = ss.Change
            }).ToList();

            return changes;
        }

        /// <summary>
        ///     Получить все задания для выбранного места отдыха и смены
        /// </summary>
        [HttpPost]
        [HttpGet]
        public IList<TasksModel> GetAllTasks(long? campId, string change)
        {
            var boutId = MobileUw.GetSet<Bout>().FirstOrDefault(x => x.CampId == campId && x.Change == change)?.Id;
            var tasks = MobileUw.GetSet<BoutTask>().Where(x => boutId == x.BoutId && x.StateId == StateEnum.BoutTask.Formed)
                .Select(a => new TasksModel { SourceBoutId = boutId, Id = a.Id, Name = a.Name, Rating = a.CamperTasks.Average(ss => ss.Rating), StartDate = a.StartDate })
                .OrderBy(d => d.StartDate).ThenBy(d => d.Name).ToList();

            return tasks;
        }

        /// <summary>
        ///     Добавить задания в заезд
        /// </summary>
        [HttpPost]
        public void AddTasksToBout([FromBody] CopyTasksModel data)
        {
            var sourceBoutTasks = MobileUw.GetSet<BoutTask>().Where(x => data.SourceTaskIds.Contains(x.Id)).ToList();
            var sourceBout = MobileUw.GetById<Bout>(data.SourceBoutId);
            var targetBout = MobileUw.GetById<Bout>(data.TargetBoutId);
            foreach (var sourceBoutTask in sourceBoutTasks)
            {
                var sourceLinkId = sourceBoutTask.LinkId;
                var newBoutTask = new BoutTask();

                newBoutTask = sourceBoutTask;
                newBoutTask.BoutId = targetBout.Id;
                newBoutTask.StateId = StateEnum.BoutTask.Editing;
                newBoutTask.Link = new Link();

                var timesheet = JsonConvert.DeserializeObject<Timesheet>(newBoutTask.Timesheet);
                if (timesheet.State == TimesheetState.Simple)
                {
                    newBoutTask.StartDate = new DateTime();
                    newBoutTask.FinishDate = new DateTime();
                    timesheet.Start = new DateTime();
                    timesheet.End = new DateTime();
                }
                else
                {
                    newBoutTask.StartDate = targetBout.DateIncome;
                    newBoutTask.FinishDate = targetBout.DateOutcome;
                    timesheet.Start = targetBout.DateIncome;
                    timesheet.End = targetBout.DateOutcome;
                }

                string timesheetStr = JsonConvert.SerializeObject(timesheet);
                newBoutTask.Timesheet = timesheetStr;

                MobileUw.AddEntity(newBoutTask);
                MobileUw.SaveChanges();

                var link = MobileUw.GetById<Link>(newBoutTask.LinkId);

                var sourceFileItems = MobileUw.GetSet<FileItem>().Where(x => x.LinkId == sourceLinkId).ToList();
                foreach (var sourceFileItem in sourceFileItems)
                {
                    var newFileItem = new FileItem();
                    newFileItem = sourceFileItem;
                    if (newFileItem != null) newFileItem.LinkId = link.Id;
                    MobileUw.AddEntity(newFileItem);
                }
                MobileUw.SaveChanges();

                MobileUw.WriteHistory(link, "Скопировано",
                    $"Задание скопировано из заезда '{sourceBout.Name}, ID заезда: {sourceBout.Id}'"
                    , Security.GetCurrentAccountId());
                MobileUw.SaveChanges();
            }
        }
    }
}
