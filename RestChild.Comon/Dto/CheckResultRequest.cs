namespace RestChild.Comon.Dto
{
    /// <summary>
    ///     результат проверки заявления
    /// </summary>
    public class CheckResultRequest
    {
        /// <summary>
        ///     не заверешена проверка
        /// </summary>
        public bool NotFinished { get; set; }

        /// <summary>
        ///     вызов заявителя
        /// </summary>
        public bool CallOfApplicant { get; set; }

        /// <summary>
        ///     подтверждена льгота
        /// </summary>
        public bool BenefitApprove { get; set; }

        /// <summary>
        ///     подтверждена малообеспеченность
        /// </summary>
        public bool LowIncomeApprove { get; set; }

        /// <summary>
        ///     подтвержден СНИЛС
        /// </summary>
        public bool SnilsApprove { get; set; }

        /// <summary>
        ///     подтверждены документы
        /// </summary>
        public bool PassportApprove { get; set; }

    }
}
