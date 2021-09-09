namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     документы к заявлению
    /// </summary>
    public enum RequestFileTypeEnum
    {
        /// <summary>
        ///     Заявление
        /// </summary>
        Request = 10,

        /// <summary>
        ///     документ, удостоверяющий личность заявителя
        /// </summary>
        Applicant = 20,

        /// <summary>
        ///     документ, подтверждающий полномочия законного представителя
        /// </summary>
        RightApplicant = 30,

        /// <summary>
        ///     Документ, подтверждающий представление интересов законного представителя
        /// </summary>
        RightAgent = 35,

        /// <summary>
        ///     документ, удостоверяющий личность ребенка
        /// </summary>
        Child = 40,

        /// <summary>
        ///     документ, подтверждающий место жительства ребенка в городе Москве
        /// </summary>
        ChildRegistration = 50,

        /// <summary>
        ///     документы, подтверждающие льготную категорию ребенка
        /// </summary>
        ChildBenefit = 60,

        /// <summary>
        ///     документы, подтверждающие уважительные причины неиспользования ранее выданной путевки
        /// </summary>
        ChildReason = 70,

        /// <summary>
        ///     документы подтверждающие отдых
        /// </summary>
        RestChildApprove = 80,

        /// <summary>
        ///     Сертификат на отдых
        /// </summary>
        CertificateOnRest = 90,

        /// <summary>
        ///     Сертификат на субсидию
        /// </summary>
        CertificateOnPayment = 100,

        /// <summary>
        ///     Уведомление об отказе в предоставлении услуги
        /// </summary>
        NotificationRefuse = 110,

        /// <summary>
        ///     Уведомления
        /// </summary>
        Notifications = 120,

        /// <summary>
        ///     Подтверждающие представление интересов заявителя
        /// </summary>
        ConfirmRepresentationApplicantsInterests = 130,

        /// <summary>
        ///     Банковские реквизиты
        /// </summary>
        BankCredentials = 140,

        /// <summary>
        ///     СНИЛС заявителя
        /// </summary>
        ApplicantSnils = 150,

        /// <summary>
        ///     Подтверждающие льготную категорию лица из числа детей-сирот и детей, оставшихся без попечения родителей
        /// </summary>
        PersonsConfirmingPrivilegedCategoryOrphans = 160,

        /// <summary>
        ///     Подтверждающие место жительства лица из числа детей-сирот и детей, оставшихся без попечения родителей
        /// </summary>
        PersonsConfirmingAddressOrphans = 170,

        /// <summary>
        ///     Удостоверяющие личность сопровождающего лица
        /// </summary>
        AttendantIdentity = 180,

        /// <summary>
        ///     СНИЛС сопровождающего лица
        /// </summary>
        AttendantSnils = 190,

        /// <summary>
        ///     Подтверждающие полномочия доверенного лица на сопровождение
        /// </summary>
        AttendantConfirmRepresentationInterests = 200,

        /// <summary>
        ///     СНИЛС ребенка
        /// </summary>
        ChildSnils = 210,

        /// <summary>
        ///     Файлы от менеджеров
        /// </summary>
        ManagersFiles = 1000,

        /// <summary>
        ///     Файлы от клиента
        /// </summary>
        ClientFiles = 2000,
    }
}
