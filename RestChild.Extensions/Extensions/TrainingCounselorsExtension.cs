using RestChild.Domain;

namespace RestChild.Extensions.Extensions
{
    /// <summary>
    ///     расширение для обучаемого
    /// </summary>
    public static class TrainingCounselorsExtension
    {
        public static string GetName(this TrainingCounselorsResult data)
        {
            var res = data.Counselors?.GetFio() ?? data.AdministratorTour?.GetFio();
            if (data.Counselors != null)
            {
                res = res + " (вожатый)";
            }
            else if (data.AdministratorTour != null)
            {
                res = res + " (администратор заезда)";
            }

            return res;
        }
    }
}
