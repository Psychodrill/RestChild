using System.Data.Entity;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     группировка данных
    /// </summary>
    [Task]
    public class GroupItemsTask : BaseTask
    {
        /// <summary>
        ///     хорошие статусы
        /// </summary>
        private static readonly long?[] GoodStatus =
        {
            (long) StatusEnum.CertificateIssued, (long) StatusEnum.Send, (long) StatusEnum.ApplicantCome,
            (long) StatusEnum.WaitApplicant, (long) StatusEnum.Ranging, (long) StatusEnum.DecisionMaking,
            (long) StatusEnum.DecisionMakingCovid, (long) StatusEnum.OnWorking, (long) StatusEnum.OperatorCheck,
            (long) StatusEnum.DecisionIsMade, (long) StatusEnum.IncludedInList, (long) StatusEnum.WaitApplicantMoney,
            (long) StatusEnum.ReadyToPayFull
        };

        /// <summary>
        ///     обработка задачи
        /// </summary>
        protected override void Execute()
        {
            using (var uw = new UnitOfWork())
            {
                uw.NotUpdateLut = true;

                Exclude(uw);
                Include(uw);
                FixUniqeInfo(uw);

                uw.NotUpdateLut = false;
            }
        }

        /// <summary>
        ///     скорректировать данные
        /// </summary>
        private static void FixUniqeInfo(IUnitOfWork uw)
        {
            var childrenToRemove = uw.GetSet<ChildUniqe>().Where(c => !c.Children.Any()).Take(1000).ToList();

            if (childrenToRemove.Any())
            {
                uw.Delete<ChildUniqe>(childrenToRemove);
            }

            uw.SaveChanges();
            uw.DetachAllEntitys();

            var relativeToRemove = uw.GetSet<RelativeUniqe>()
                .Where(c => !c.Relatives.Any())
                .Take(1000).ToList();

            if (relativeToRemove.Any())
            {
                var sub = relativeToRemove.SelectMany(e => e.RelativeRequests).ToList();
                if (sub.Any())
                {
                    uw.Delete<RelativeUniqeApplicant>(sub);
                }

                uw.Delete<RelativeUniqe>(relativeToRemove);
            }

            uw.SaveChanges();
            uw.DetachAllEntitys();

            var children = uw.GetSet<ChildUniqe>().Where(c => !c.LastInfoId.HasValue).Take(4000).ToList();

            foreach (var child in children)
            {
                child.LastInfoId = child.Children?.OrderByDescending(r => r.Request.DateRequest)
                    .Select(r => r.Id).FirstOrDefault();
            }

            uw.SaveChanges();
            uw.DetachAllEntitys();

            var relatives = uw.GetSet<RelativeUniqe>().Where(c => !c.LastInfoId.HasValue).Take(4000).ToList();
            foreach (var relative in relatives)
            {
                relative.LastInfoId = relative.RelativeRequests?.OrderByDescending(r => r.Request.DateRequest)
                    .Select(r => r.ApplicantId).FirstOrDefault();
            }

            uw.SaveChanges();
            uw.DetachAllEntitys();
        }

        /// <summary>
        ///     включить родственников в связку
        /// </summary>
        private static void Include(IUnitOfWork uw)
        {
            var children = uw.GetSet<Child>().Where(c =>
                    !c.ChildUniqeId.HasValue && !string.IsNullOrEmpty(c.Snils) &&
                    GoodStatus.Contains(c.Request.StatusId) && !c.Request.IsDeleted)
                .OrderBy(c => c.Request.DateRequest)
                .Take(1000).ToList();

            foreach (var child in children)
            {
                var snils = child.Snils.Replace(" ", string.Empty).Replace("-", string.Empty);
                var item = uw.GetSet<ChildUniqe>().FirstOrDefault(s => s.Snils == snils) ?? uw.AddEntity(new ChildUniqe
                {
                    Snils = snils
                });

                child.ChildUniqeId = item.Id;

                if (child.Request != null && (item.LastInfo?.Request == null || item.LastInfo.Request.DateRequest <= child.Request.DateRequest))
                {
                    item.LastInfoId = child.Id;
                }

                var relatives = uw.GetSet<RelativeUniqeApplicant>()
                    .Where(a => a.RequestId == child.RequestId && a.RelativeUniqe.Children.All(c => c.Id != item.Id))
                    .Select(v => v.RelativeUniqe).Where(c => c != null).Distinct().ToList();

                item.Relatives.AddRange(relatives);
                uw.SaveChanges();
            }

            uw.SaveChanges();
            uw.DetachAllEntitys();

            var applicantRequest = uw.GetSet<Request>().Where(c =>
                    !c.Applicant.RelativeUniqeId.HasValue && !string.IsNullOrEmpty(c.Applicant.Snils) &&
                    GoodStatus.Contains(c.StatusId) && !c.IsDeleted).OrderBy(c => c.DateRequest)
                .Include(c => c.Applicant)
                .Take(1000).ToList();

            foreach (var applicant in applicantRequest)
            {
                var snils = applicant.Applicant.Snils.Replace(" ", string.Empty).Replace("-", string.Empty);
                var item = uw.GetSet<RelativeUniqe>().FirstOrDefault(s => s.Snils == snils) ?? uw.AddEntity(
                               new RelativeUniqe
                               {
                                   Snils = snils
                               });

                applicant.Applicant.RelativeUniqeId = item.Id;

                var lastInfoRequest = item.RelativeRequests?.FirstOrDefault(r => r.ApplicantId == item.LastInfoId)
                    ?.Request;

                if (lastInfoRequest == null || lastInfoRequest.DateRequest <= applicant.DateRequest)
                {
                    item.LastInfoId = applicant.ApplicantId;
                }

                uw.AddEntity(new RelativeUniqeApplicant
                {
                    ApplicantId = applicant.ApplicantId,
                    RequestId = applicant.Id,
                    RelativeUniqeId = item.Id
                });

                var childrenToRelative = uw.GetSet<Child>()
                    .Where(a => a.RequestId == applicant.Id && a.ChildUniqe.Relatives.All(c => c.Id != item.Id))
                    .Select(v => v.ChildUniqe).Where(c => c != null).Distinct().ToList();

                item.Children.AddRange(childrenToRelative);
                uw.SaveChanges();
            }

            uw.SaveChanges();
            uw.DetachAllEntitys();

            var applicants = uw.GetSet<Applicant>().Where(c =>
                    !c.RelativeUniqeId.HasValue && !string.IsNullOrEmpty(c.Snils) &&
                    GoodStatus.Contains(c.Request.StatusId) && !c.Request.IsDeleted)
                .OrderBy(c => c.Request.DateRequest)
                .Take(1000).ToList();

            foreach (var applicant in applicants)
            {
                var snils = applicant.Snils.Replace(" ", string.Empty).Replace("-", string.Empty);
                var item = uw.GetSet<RelativeUniqe>().FirstOrDefault(s => s.Snils == snils) ?? uw.AddEntity(
                               new RelativeUniqe
                               {
                                   Snils = snils
                               });

                applicant.RelativeUniqeId = item.Id;

                var lastInfoRequest = item.RelativeRequests?.FirstOrDefault(r => r.ApplicantId == item.LastInfoId)
                    ?.Request;

                if (lastInfoRequest == null || lastInfoRequest.DateRequest <= applicant.Request.DateRequest)
                {
                    item.LastInfoId = applicant.Id;
                }

                uw.AddEntity(new RelativeUniqeApplicant
                {
                    ApplicantId = applicant.Id,
                    RequestId = applicant.RequestId,
                    RelativeUniqeId = item.Id
                });

                var childrenToRelative = uw.GetSet<Child>()
                    .Where(a => a.RequestId == applicant.RequestId && a.ChildUniqe.Relatives.All(c => c.Id != item.Id))
                    .Select(v => v.ChildUniqe).Where(c => c != null).Distinct().ToList();

                item.Children.AddRange(childrenToRelative);
                uw.SaveChanges();
            }

            uw.SaveChanges();
            uw.DetachAllEntitys();
        }

        /// <summary>
        ///     исключить уникальные значения
        /// </summary>
        private static void Exclude(IUnitOfWork uw)
        {
            var childrenExclude = uw.GetSet<Child>().Where(c =>
                    c.ChildUniqeId.HasValue &&
                    (!GoodStatus.Contains(c.Request.StatusId) || c.Request.IsDeleted) &&
                    c.Request.StatusId.HasValue).OrderBy(c => c.Request.DateRequest)
                .Take(4000).ToList();

            foreach (var child in childrenExclude)
            {
                child.ChildUniqeId = null;
            }

            uw.SaveChanges();
            uw.DetachAllEntitys();

            var applicantRequestExclude = uw.GetSet<Request>().Where(c =>
                    c.Applicant.RelativeUniqeId.HasValue &&
                    (!GoodStatus.Contains(c.StatusId) || c.IsDeleted) && c.StatusId.HasValue)
                .OrderBy(c => c.DateRequest)
                .Select(c => c.Applicant)
                .Take(4000).ToList();
            foreach (var applicant in applicantRequestExclude)
            {
                applicant.RelativeUniqeId = null;
            }

            uw.SaveChanges();
            uw.DetachAllEntitys();

            var applicantsExclude = uw.GetSet<Applicant>().Where(c =>
                    c.RelativeUniqeId.HasValue &&
                    (!GoodStatus.Contains(c.Request.StatusId) || c.Request.IsDeleted) && c.Request.StatusId.HasValue)
                .OrderBy(c => c.Request.DateRequest)
                .Take(2000).ToList();
            foreach (var applicant in applicantsExclude)
            {
                applicant.RelativeUniqeId = null;
            }

            uw.SaveChanges();
            uw.DetachAllEntitys();

            var relations = uw.GetSet<RelativeUniqeApplicant>().Where(a => !a.Applicant.RelativeUniqeId.HasValue)
                .Take(2000).ToList();

            uw.Delete<RelativeUniqeApplicant>(relations);
            uw.SaveChanges();
        }
    }
}
