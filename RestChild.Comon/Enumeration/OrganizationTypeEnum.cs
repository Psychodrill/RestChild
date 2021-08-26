using System.ComponentModel.DataAnnotations;

namespace RestChild.Comon.Enumeration
{
    public enum OrganizationTypeEnum
    {
        [Display(Name = "Учреждение")] Agency = 1,
        [Display(Name = "Организация")] Organization = 2,

        [Display(Name = "Транспортная организация")]
        TransportOrganization = 3,
        [Display(Name = "Профсоюзы")] TradeUnion = 4,
        [Display(Name = "Профсоюзные лагеря")] TradeUnionCamp = 5
    }
}
