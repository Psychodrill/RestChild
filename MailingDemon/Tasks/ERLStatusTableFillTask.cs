using RestChild.Comon;
using RestChild.DAL;
using RestChild.Domain;
using RestChild.ERL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     Заполнение таблицы интеграции с ЕРЛ
    /// </summary>
    [Task]
    public class ERLStatusTableFillTask : BaseTask
    {
        [XmlElement("config")]
        public ERLStatusTableFillConfig Config { get; set; }

        protected override void Execute()
        {
            Logger.Info("ERLStatusTableFillTask started");

            if (Config == null)
            {
                Logger.Info("ERLStatusTableFillTask stopped, config not set");
                return;
            }

            using (var unitOfWork = new UnitOfWork())
            {
                int curentYear = DateTime.Now.Year;

                var years = unitOfWork.GetSet<YearOfRest>().Where(ss => ss.Year == curentYear || ss.Year == (curentYear - 1) || ss.Year == (curentYear - 2)).Select(ss => (long?)ss.Id).AsQueryable();

                //все заявления со статусом "услуга оказана"
                var q = unitOfWork.GetSet<Request>().Where(ss => years.Contains(ss.YearOfRestId)).Where(r => r.StatusId == (long)RestChild.Comon.Enumeration.StatusEnum.CertificateIssued && !r.IsDeleted && !r.TypeOfRest.Commercial).AsQueryable();

                //категории льготных детей
                var bnft = unitOfWork.GetSet<BenefitTypeERL>().Where(ss => ss.IsActive).SelectMany(ss => ss.BenefitTypes).Select(ss => (long?)ss.Id).Distinct().AsQueryable();

                if (Config.RequestIdMin.HasValue && Config.RequestIdMin.Value > 0)
                {
                    q = q.Where(ss => ss.Id >= Config.RequestIdMin.Value);
                }

                if (Config.RequestIdMax.HasValue && Config.RequestIdMax.Value > 0)
                {
                    q = q.Where(ss => ss.Id <= Config.RequestIdMax.Value);
                }

                //дети из льготных категорий
                //q = q.Where(ss => ss.Child.Any(c => ss.TypeOfRest.TypeOfRestERL.IsActive));
                q = q.Where(ss => ss.TypeOfRest.TypeOfRestERL.IsActive && ss.Child.Any(c => (bnft.Contains(c.BenefitTypeId)) || ss.TypeOfRest.TypeOfRestERL.UseApplicant));

                //все дети
                var allChildren = q.Where(ss => !ss.TypeOfRest.TypeOfRestERL.UseApplicant).SelectMany(ss => ss.Child).Where(c => bnft.Contains(c.BenefitTypeId)).AsQueryable();

                //все заявителеи
                var allApplicants = q.Where(ss => ss.TypeOfRest.TypeOfRestERL.UseApplicant).Select(ss => ss.Applicant).AsQueryable();

                //все уже внесённые люди
                var existingPersons = unitOfWork.GetSet<ERLPersonStatus>().Where(sx => sx.ChildId != null || sx.ApplicantId != null).AsQueryable();


                //список кандиддатов на добавление в таблицу на интеграцию с ЕРЛ (с учетом игнорирования по одинаквым СНИЛС)
                //var childrenToAdd = unitOfWork.GetSet<Child>().Where(c => allChildren.Any(ss => ss.Id == c.Id) && !existingPersons.Any(ss => ss.ChildId == c.Id || (c.Snils != null && ((ss.ChildId != null && c.Snils.ToLower() == ss.Child.Snils.ToLower()) || (ss.ApplicantId != null && c.Snils.ToLower() == ss.Applicant.Snils.ToLower()))))).AsQueryable();
                //var applicantsToAdd = unitOfWork.GetSet<Applicant>().Where(c => allApplicants.Any(ss => ss.Id == c.Id) && !existingPersons.Any(ss => ss.ApplicantId == c.Id || (c.Snils != null && ((ss.ChildId != null && c.Snils.ToLower() == ss.Child.Snils.ToLower()) || (ss.ApplicantId != null && c.Snils.ToLower() == ss.Applicant.Snils.ToLower()))))).AsQueryable();

                //список кандиддатов на добавление в таблицу на интеграцию с ЕРЛ (совпадения по СНИЛС не учитываются)
                var childrenToAdd = unitOfWork.GetSet<Child>().Where(c => allChildren.Any(ss => ss.Id == c.Id) && !existingPersons.Any(ss => ss.ChildId == c.Id)).AsQueryable();
                var applicantsToAdd = unitOfWork.GetSet<Applicant>().Where(c => allApplicants.Any(ss => ss.Id == c.Id) && !existingPersons.Any(ss => ss.ApplicantId == c.Id)).AsQueryable();

                if (Config.ChildCount.HasValue && Config.ChildCount.Value > 0)
                {
                    childrenToAdd = childrenToAdd.Take(Config.ChildCount.Value);
                    childrenToAdd = childrenToAdd.Take(Config.ChildCount.Value);
                }

                foreach (var child in childrenToAdd.ToList())
                {
                    var sentChild = unitOfWork.GetSet<ERLPersonStatus>().FirstOrDefault(sx => child.Snils != null && sx.Child.Snils == child.Snils);

                    var _s = new ERLPersonStatus()
                    {
                        ChildId = child.Id,
                        PersonUid = sentChild?.PersonUid,
                        ERLCommited = true,
                        ERLMessageId = Guid.Empty
                    };

                    unitOfWork.AddEntity(_s);
                    unitOfWork.SaveChanges();
                }

                foreach (var applicant in applicantsToAdd.ToList())
                {
                    var sentApplicant = unitOfWork.GetSet<ERLPersonStatus>().FirstOrDefault(sx => applicant.Snils != null && sx.Child.Snils == applicant.Snils);

                    ERLPersonStatus _s = new ERLPersonStatus()
                    {
                        ApplicantId = applicant.Id,
                        PersonUid = sentApplicant?.PersonUid,
                        ERLCommited = true,
                        ERLMessageId = Guid.Empty
                    };

                    unitOfWork.AddEntity(_s);
                    unitOfWork.SaveChanges();
                }

            }

            Logger.Info("ERLStatusTableFillTask finished");
        }
    }
}
