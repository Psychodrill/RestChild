using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Extensions.Extensions
{
    public static class TypeOfRestExtension
    {
        /// <summary>
        ///     получение списка элементов для ручного ввода
        /// </summary>
        public static bool IsManualVisible(this TypeOfRest item)
        {
            return item?.Id == (long) TypeOfRestEnum.Compensation ||
                   item?.Id == (long) TypeOfRestEnum.CompensationYouthRest ||
                   item?.FirstRequestCompanySelect == true &&
                   item.Id != (long) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex &&
                   item.Id != (long) TypeOfRestEnum.RestWithParents;
        }
    }
}
