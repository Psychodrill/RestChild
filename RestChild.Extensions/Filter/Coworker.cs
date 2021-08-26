using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class Coworker
    {
        public TaskExecutorTypeEnum TaskExecutorType { get; set; }
        public Bout Bout { get; set; }
        public Party Party { get; set; }
        public AdministratorTour Administrator { get; set; }
        public Counselors Counselor { get; set; }

        public string GetFio()
        {
            if (Administrator != null)
            {
                return Administrator.LastName + " " + Administrator.FirstName + " " + Administrator.MiddleName;
            }

            if (Counselor != null)
            {
                return Counselor.LastName + " " + Counselor.FirstName + " " + Counselor.MiddleName;
            }

            return null;
        }

        public string GetCoworkerType()
        {
            switch (TaskExecutorType)
            {
                case TaskExecutorTypeEnum.Administrator:
                    return "Администратор";
                case TaskExecutorTypeEnum.Counselor:
                    return "Вожатый";
                case TaskExecutorTypeEnum.SeniorCounselor:
                    return "Старший вожатый";
                case TaskExecutorTypeEnum.SwingCounselor:
                    return "Подменный вожатый";
                default:
                    return null;
            }
        }

        public long GetId()
        {
            if (Administrator != null)
            {
                return Administrator.Id;
            }

            if (Counselor != null)
            {
                return Counselor.Id;
            }

            return 0;
        }
    }
}
