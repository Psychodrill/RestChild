using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Extensions.Extensions
{
    public static class ResponsibilityForTaskExtension
    {
        /// <summary>
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetInfo(this ResponsibilityForTask self)
        {
            if (self == null || self.CounselorTaskExecutorType == null || !self.CounselorTaskExecutorTypeId.HasValue)
            {
                return "-";
            }

            switch ((TaskExecutorTypeEnum) self.CounselorTaskExecutorTypeId)
            {
                case TaskExecutorTypeEnum.Administrator:
                    return string.Format("{0} (Администратор смены)",
                        self.AdministratorTour.NullSafe(a => a.GetFio()).FormatEx(""));
                case TaskExecutorTypeEnum.Counselor:
                    return string.Format("{0} (вожатый отряда № {1})",
                        self.Counselors.NullSafe(a => a.GetFio()).FormatEx(""),
                        self.Party.NullSafe(p => p.PartyNumber).FormatEx());
                case TaskExecutorTypeEnum.MosgorturOperator:
                    return string.Format("{0} (оператор Мосгортура)",
                        self.Account.NullSafe(a => a.Name).FormatEx(""));
                case TaskExecutorTypeEnum.SeniorCounselor:
                    return string.Format("{0} (старший вожатый)",
                        self.Counselors.NullSafe(a => a.GetFio()).FormatEx(""));
                case TaskExecutorTypeEnum.SwingCounselor:
                    return string.Format("{0} (подменный вожатый)",
                        self.Counselors.NullSafe(a => a.GetFio()).FormatEx(""));
            }

            return "-";
        }
    }
}
