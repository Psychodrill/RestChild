using RestChild.Domain;

namespace RestChild.Extensions.Extensions
{
    public static class OrganizationBankExtension
    {
        /// <summary>
        ///     получение банковских реквизитов
        /// </summary>
        public static string GetInfo(this OrganizationBank self)
        {
            if (self == null)
            {
                return null;
            }

            return $"{self.Account} в {self.Bank} ({self.Bik})";
        }
    }
}
