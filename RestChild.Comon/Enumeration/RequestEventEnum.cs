using System;

namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     события заявления
    /// </summary>
    public static class RequestEventEnum
    {
        /// <summary>
        ///     отправляем что сертификат выдан по заявлению
        /// </summary>
        public static readonly Guid SendCertificateIssued = new Guid("4FFB2187-02AE-472A-BAC8-732491F236E8");

        /// <summary>
        ///     отправляем что сертификат выдан по родительскому заявлению
        /// </summary>
        public static readonly Guid SendCertificateIssuedByParent = new Guid("1E9983D7-475E-4094-8AD8-4F0423D8CC7F");

        /// <summary>
        ///     начато ранжирование
        /// </summary>
        public static readonly Guid Ranging = new Guid("07215969-05D8-4C65-877C-A62AEEDA8413");

        /// <summary>
        ///     Список сформирован
        /// </summary>
        public static readonly Guid IncludeInList = new Guid("DC94EBAF-03B8-4F1D-8B5F-3F87CC18D0DA");

        /// <summary>
        ///     начат сбор данных для отдыха
        /// </summary>
        public static readonly Guid StartSecondTour = new Guid("131DC4D5-E635-4499-951F-EF08B33B51CF");

        /// <summary>
        ///     Отказ в аннулировании
        /// </summary>
        public static readonly Guid DeclineCancel = new Guid("6EEA0451-713C-4E7C-B461-60B6019315D5");

        /// <summary>
        ///     формирование результата 1052
        /// </summary>
        public static readonly Guid ResultMaking = new Guid("DEBB3EEC-5467-4D9B-AA0B-514F177CB0C9");

        /// <summary>
        ///     Отказ от заявления (не уважительно) 7708.3
        /// </summary>
        public static readonly Guid RequestDeclineNotApproved = new Guid("40AEB7A2-BF01-42D7-B204-A36E64ACE4BE");

        /// <summary>
        ///     7704.1 Получение документов из Базового Регистра (проверка льготы и получения ежемесячного пособия на ребенка в
        ///     ДТСЗН).
        /// </summary>
        public static readonly Guid SendRequestInBenefit = new Guid("35350A98-54D9-45AC-A8A9-DA4B91B3FDE5");

        /// <summary>
        ///     7705.1 Документы из Базового Регистра получены (проверка льготы в ДТСЗН).
        /// </summary>
        public static readonly Guid GetResponseInBenefit = new Guid("A5131E58-F0FA-4431-AFFC-BD0BFCACB405");

        /// <summary>
        ///     7704.2 Получение документов из Базового Регистра (проверка родства в УЗАГС, ФЗАГС).
        /// </summary>
        public static readonly Guid SendRequestForRelatives = new Guid("22D7EDDC-DA57-41EE-8C23-000454A4BCB8");

        /// <summary>
        ///     7705.2 Документы из Базового Регистра получены (проверка родства в ЗАГС).
        /// </summary>
        public static readonly Guid GetResponseForRelatives = new Guid("56B730B6-D37D-40E4-B1EF-82DE7A8C447E");

        /// <summary>
        ///     7704.3 Получение документов из Базового Регистра Пенсионного фонда РФ (проверка СНИЛС).
        /// </summary>
        public static readonly Guid SendRequestForSnils = new Guid("F8A439A6-B26E-4B1A-A9D9-C44FCDE83110");

        /// <summary>
        ///     7705.3 Документы из Базового Регистра Пенсионного фонда РФ получены (проверка СНИЛС).
        /// </summary>
        public static readonly Guid GetResponseForSnils = new Guid("2B4FD618-DCF3-462E-8084-53A0FE88D6F4");

        /// <summary>
        ///     7704.4 Получение документов из АИС «ЦПМПК» (проверка льготы в ЦПМПК)
        /// </summary>
        public static readonly Guid SendRequestInCPMPK = new Guid("5FF9C2A3-08F3-44A3-BD6C-F59B5B249276");

        /// <summary>
        ///     7705.4 Документы из Базового Регистра получены в АИС «ЦПМПК» (проверка льготы в ЦПМПК).
        /// </summary>
        public static readonly Guid GetResponseForCPMPK = new Guid("99B85AB4-40A0-450C-9DFC-1432CA54D41B");

        /// <summary>
        ///     7704.5 Получение документов из Базового Регистра (проверка законного представительства ребенка льготной категории
        ///     дети - сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе
        ///     приемной или патронатной семье в ДТСЗН).
        /// </summary>
        public static readonly Guid SendRequestInBRDTSZN = new Guid("4E42B896-AA9B-4052-9A82-2AFAAA3D771A");

        /// <summary>
        ///     7705.5 Документы из Базового Регистра получены (проверка законного представительства в ДТСЗН).
        /// </summary>
        public static readonly Guid GetResponseForBRDTSZN = new Guid("2C1857BB-8C7F-4FB3-BE2B-473D6B21D366");

        /// <summary>
        ///     7704.6 Получение документов из Базового Регистра АС УР (проверка паспорта в МВД).
        /// </summary>
        public static readonly Guid SendRequestInBRASUR = new Guid("82477449-519F-4193-B71A-D8AFAA6002D3");

        /// <summary>
        ///     7705.6 Документы из Базового Регистра АС УР получены (проверка паспорта в МВД).
        /// </summary>
        public static readonly Guid GetResponseForBRASUR = new Guid("979B69BD-92D9-4C1A-A5A6-8B6E81BB081D");

        /// <summary>
        ///     7704.7 Получение документов из Базового Регистра АС УР (проверка адреса регистрации в МВД детей всех льготных
        ///     категорий и лиц из числа детей-сирот и детей, оставшихся без попечения родителей).
        /// </summary>
        public static readonly Guid SendRequestForRegistrationByPassport =
            new Guid("A5501A0B-80EA-4C96-B8F6-A7E9BA0C01BF");

        /// <summary>
        ///     7705.7 Документы из Базового Регистра АС УР получены (проверка адреса регистрации в МВД детей всех льготных
        ///     категорий и лиц из числа детей-сирот и детей, оставшихся без попечения родителей).
        /// </summary>
        public static readonly Guid GetResponseFoRegistrationByPassport =
            new Guid("98087B63-E659-4235-B3A4-C6E92C18CB7F");
    }
}
