namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     Результат проверки заявления
    /// </summary>
    public class CheckStateCheckRequestResult
    {
        /// <summary>
        ///     Проверка заявки не завершена
        /// </summary>
        public bool NotFinished { get; set; }

        /// <summary>
        ///     ребёнок подтверждена
        /// </summary>
        public bool? ApprovedChild { get; set; }

        /// <summary>
        ///     льгота подтверждена
        /// </summary>
        public bool? ApprovedBenefit { get; set; }

        /// <summary>
        ///     льгота подтверждена без ограничений по неподтверждению льготы
        /// </summary>
        public bool? ApprovedBenefitInAllTime { get; set; }

        /// <summary>
        ///     льгота подтверждена
        /// </summary>
        public bool ApprovedBenefitSetted { get; set; }

        /// <summary>
        ///     СНИЛС подвтержден
        /// </summary>
        public bool? ApprovedSnils { get; set; }

        /// <summary>
        ///     Платежи подтверждены
        /// </summary>
        public bool? ApprovedPayment { get; set; }

        /// <summary>
        ///     Родство подтверждено
        /// </summary>
        public bool? ApprovedRelationship { get; set; }

        /// <summary>
        ///     Данные по родству загружены
        /// </summary>
        public bool DataRelationshipLoaded { get; set; }
    }
}
