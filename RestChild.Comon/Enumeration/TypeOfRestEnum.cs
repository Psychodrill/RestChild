using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     вид отдыха
    /// </summary>
    [Serializable]
    [DataContract(Name = "typeOfRestEnum")]
    public enum TypeOfRestEnum
    {
        /// <summary>
        ///     "Отдых детей"
        /// </summary>
        [EnumMember(Value = "1")] ChildRest = 1,

        /// <summary>
        ///     Лица из числа детей-сирот и детей, оставшихся без попечения родителей, 18-23 лет
        /// </summary>
        [EnumMember(Value = "13")] YouthRestCamps = 13,

        /// <summary>
        ///     Детские лагеря 7-15 лет
        /// </summary>
        [EnumMember(Value = "3")] ChildRestCamps = 3,

        /// <summary>
        ///     Детский лагерь для детей оставшихся без попечения родителей, воспитывающихся в приемных или патронатных семьях,
        ///     7-17 лет
        /// </summary>
        [EnumMember(Value = "12")] ChildRestOrphanCamps = 12,

        /// <summary>
        ///     Лица из числа детей-сирот и детей, оставшихся без попечения родителей, 18-23 лет
        /// </summary>
        [EnumMember(Value = "14")] YouthRestOrphanCamps = 14,

        /// <summary>
        ///     "Совместный отдых "
        /// </summary>
        [EnumMember(Value = "2")] RestWithParents = 2,

        /// <summary>
        ///     Дети из малообеспеченных семей 3-7 лет
        /// </summary>
        [EnumMember(Value = "4")] RestWithParentsPoor = 4,

        /// <summary>
        ///     Дети-инвалиды 4-16 лет
        /// </summary>
        [EnumMember(Value = "5")] RestWithParentsInvalid = 5,

        /// <summary>
        ///     Дети, оставшиеся без попечения родителей, переданные в приемную семью или на патронатное воспитание 3-17 лет
        /// </summary>
        [EnumMember(Value = "6")] RestWithParentsOrphan = 6,

        /// <summary>
        ///     Дети-инвалиды 4-16 лет; Дети, оставшиеся без попечения родителей, переданные в приемную семью или на патронатное
        ///     воспитание 3-17 лет; Комплексные семьи 3-17 лет
        /// </summary>
        [EnumMember(Value = "9")] RestWithParentsInvalidOrphanComplex = 9,

        /// <summary>
        ///     Комплексные семьи 3-17 лет
        /// </summary>
        [EnumMember(Value = "10")] RestWithParentsComplex = 10,

        /// <summary>
        ///     Федереальные программа отдыха
        /// </summary>
        [EnumMember(Value = "11")] ChildRestFederalCamps = 11,

        /// <summary>
        ///     Прочие виды льгот для семейного отдыха.
        /// </summary>
        [EnumMember(Value = "7")] RestWithParentsOther = 7,

        /// <summary>
        ///     Профильный лагеря и сироты
        /// </summary>
        [EnumMember(Value = "-1")] SpecializedСamp = -1,

        /// <summary>
        ///     Отдых для сирот (совместный отдых)
        /// </summary>
        [EnumMember(Value = "15")] SpecializedСampFamily = 15,

        /// <summary>
        ///     компенсация за использованый путевку.
        /// </summary>
        [EnumMember(Value = "-2")] Compensation = -2,

        /// <summary>
        ///     компенсация за использованый путевку для моложеного отдыха.
        /// </summary>
        [EnumMember(Value = "19")] CompensationYouthRest = 19,

        /// <summary>
        ///     компенсация за групповой элемент
        /// </summary>
        [EnumMember(Value = "20")] CompensationGroup = 20,

        /// <summary>
        ///     дополнительные путевки
        /// </summary>
        [EnumMember(Value = "8")] CommercicalAddonRequest = 8,

        /// <summary>
        ///     Виза
        /// </summary>
        [EnumMember(Value = "-3")] CommercicalVisa = -3,

        /// <summary>
        ///     отдых с родителями
        /// </summary>
        [EnumMember(Value = "-4")] CommercicalRestWithParent = -4,

        /// <summary>
        ///     индивидуальный
        /// </summary>
        [EnumMember(Value = "-5")] CommercicalCamp = -5,

        /// <summary>
        ///     экскурсии
        /// </summary>
        [EnumMember(Value = "-6")] CommercicalTour = -6,

        /// <summary>
        ///     Составной отдых
        /// </summary>
        [EnumMember(Value = "-7")] CommercicalComposition = -7,

        /// <summary>
        ///     Билеты
        /// </summary>
        [EnumMember(Value = "-8")] CommercicalTickets = -8,

        /// <summary>
        ///     Страховка
        /// </summary>
        [EnumMember(Value = "-9")] CommercicalInsurance = -9,

        /// <summary>
        ///     экскурсии (однодневная)
        /// </summary>
        [EnumMember(Value = "-10")] CommercicalTourOneDays = -10,

        /// <summary>
        ///     дополнительные услуги
        /// </summary>
        [EnumMember(Value = "-11")] CommercicalAddonService = -11,

        /// <summary>
        ///     дополнительные услуги для профильных списков
        /// </summary>
        [EnumMember(Value = "-12")] CommercicalAddonServiceList = -12,

        /// <summary>
        ///     Сертификат на получение выплаты на самостоятельную организацию отдыха и оздоровления
        /// </summary>
        [EnumMember(Value = "16")] Money = 16,

        /// <summary>
        ///     Сертификат на отдых и оздоровление, 3-7 лет
        /// </summary>
        [EnumMember(Value = "17")] MoneyOn3To7 = 17,

        /// <summary>
        ///     Сертификат на отдых и оздоровление, 7-15 лет
        /// </summary>
        [EnumMember(Value = "18")] MoneyOn7To15 = 18,

        /// <summary>
        ///     Сертификат на совместный отдых и оздоровление для детей-инвалидов, детей с ограниченными возможностями здоровья,
        ///     4-17 лет
        /// </summary>
        [EnumMember(Value = "24")] MoneyOnInvalidOn4To17 = 24,

        /// <summary>
        ///     Сертификат на отдых и оздоровление, молодёжный отдых
        /// </summary>
        [EnumMember(Value = "21")] MoneyOn18 = 21,

        /// <summary>
        ///     Палаточный детский лагерь, 7-15 лет
        /// </summary>
        [EnumMember(Value = "22")] TentChildrenCamp = 22,

        /// <summary>
        ///     Палаточный детский лагерь типа для детей-сирот и детей, оставшихся без попечения родителей, находящихся под опекой,
        ///     попечительством, в том числе в приемной или патронатной семье, 7-17 лет
        /// </summary>
        [EnumMember(Value = "23")] TentChildrenCampOrphan = 23
    }
}
