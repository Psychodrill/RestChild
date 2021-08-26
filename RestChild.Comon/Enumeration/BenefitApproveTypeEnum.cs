namespace RestChild.Comon.Enumeration
{
    public enum BenefitApproveTypeEnum
    {
        /// <summary>
        ///     Льгота не подтверждена в базовом регистре
        /// </summary>
        NotApproved = 1,

        /// <summary>
        ///     Направлен запрос в Базовый регистр
        /// </summary>
        NotApprovedSendToBr = 2,

        /// <summary>
        ///     Льгота подтверждена в Базовом регистре
        /// </summary>
        ApprovedByBr = 3,

        /// <summary>
        ///     Льгота подтверждена по документам Заявителя
        /// </summary>
        ApprovedByApplicant = 4,

        /// <summary>
        ///     Льгота подтверждена по межведомственному запросу
        /// </summary>
        ApprovedByOiv = 5,

        /// <summary>
        ///     Льгота не подтверждена по межведомственному запросу
        /// </summary>
        NotApprovedByOiv = 6
    }
}
