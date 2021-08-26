namespace RestChild.Comon.Enumeration
{
    public static class DocumentGenerationEnum
    {
        /// <summary>
        ///     Сертификат / путевка
        /// </summary>
        public static string SaveCertificateToRequest = "SaveCertificateToRequest";

        /// <summary>
        ///     Уведомление о регистрации заявления
        /// </summary>
        public static string NotificationRegistration = "NotificationRegistration";

        /// <summary>
        ///     Уведомление о рассмотрении заявления (отказ)
        /// </summary>
        public static string NotificationRefuse = "NotificationRefuse";

        /// <summary>
        ///     Уведомление о приостановлении рассмотрения заявления
        /// </summary>
        public static string NotificationWaitApplicant = "NotificationWaitApplicant";

        /// <summary>
        ///     Уведомление о необходимости выбора организации отдыха и оздоровления
        /// </summary>
        public static string NotificationOfNeedToChoose = "NotificationOfNeedToChoose";

        /// <summary>
        ///     Уведомление о предоставлении сертификата
        /// </summary>
        public static string NotificationAboutDecision = "NotificationAboutDecision";
    }
}
