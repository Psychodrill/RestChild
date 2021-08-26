using System.ComponentModel.DataAnnotations;

namespace RestChild.Comon.Enumeration
{
    public static class AccessRightEnum
    {
        /// <summary>
        ///     комерческая часть
        /// </summary>
        public const string CommercialPart = "7A0D807F-FF62-4798-A4B7-93BF380BF2C1";

        /// <summary>
        ///     управление пользователями
        /// </summary>
        public const string AccountManage = "03C0A54C-AC10-450D-8016-7F449FCB753E";

        /// <summary>
        ///     управление регионами отдыха
        /// </summary>
        public const string PlaceOfRestManage = "75C2B491-A1B2-4735-A375-93059B22F38E";

        /// <summary>
        ///     управление вожатыми
        /// </summary>
        public const string CounselorsManage = "8C0409F0-98D6-471A-973B-BC3BF6ADC22B";

        /// <summary>
        ///     управление рассылкой проверки в БР
        /// </summary>
        public const string ManageExchangeBaseRegistry = "60CC3642-336C-4E8A-85D1-401A47B57132";

        /// <summary>
        ///     Реестр записи на приём (просмотр)
        /// </summary>
        public const string MosgorturScheduleBookingView = "789BF5C0-D151-4435-824C-79EED359459C";

        /// <summary>
        ///     Реестр записи на приём (анулирование)
        /// </summary>
        public const string MosgorturScheduleBookingCancel = "4FF5F092-DB8A-4C78-B247-E31D31B93C20";

        /// <summary>
        ///     Реестр записи на приём (Создание)
        /// </summary>
        public const string MosgorturScheduleBookingCreate = "4D6BFBC1-16F2-42DB-A02E-54991AA390B3";

        /// <summary>
        ///     Управление рабочими днями (просмотр)
        /// </summary>
        public const string MosgorturWorkingDaysView = "FEEC3B20-8500-41D9-858D-9551BB506EC5";

        /// <summary>
        ///     Управление рабочими днями (редактирование)
        /// </summary>
        public const string MosgorturWorkingDaysEdit = "553B46F8-0F82-4E6E-8D34-BDCD3ED3A0D0";

        /// <summary>
        ///     Управление целями визита (просмотр)
        /// </summary>
        public const string MosgorturBookingTargetsView = "97A388D4-AA84-493A-8B4E-4616EF9EDB1D";

        /// <summary>
        ///     Управление целями визита (редактирование)
        /// </summary>
        public const string MosgorturBookingTargetsEdit = "AEE5CF55-CF11-4333-B3C1-E340FB792563";

        /// <summary>
        ///     Управление интеграцией с ЗАГС
        /// </summary>
        public const string ZAGSIntegration = "ABB812BF-8AA6-470E-903B-52F3DC211E53";

        /// <summary>
        ///     Управление переносом заездов
        /// </summary>
        public const string ChildTransfer = "D904B8AB-1739-45AC-805B-67534E77B579";

        /// <summary>
        ///     Управление интеграцией с ДТСЗН
        /// </summary>
        public const string DTSZNIntegration = "2E4FB7A6-5C87-49E4-A1DB-1D4454F8C964";

        /// <summary>
        ///     управление заездами
        /// </summary>
        public const string ToursManage = "587DB6C0-F67A-413B-A021-75B01A1BF8D5";

        /// <summary>
        ///     просмотр заездов
        /// </summary>
        public const string ToursView = "6853C012-E17A-4B72-819A-8BB64DCB0041";

        /// <summary>
        ///     управление заявлениями
        /// </summary>
        public const string RequestManage = "BD6E2284-C9E9-4A48-934E-7E2DBAF20C30";

        /// <summary>
        ///     просмотр заявления
        /// </summary>
        public const string RequestView = "BF32F0EB-24A7-43DC-9123-F1B84DCC88A9";

        /// <summary>
        ///     Заявление. Подача заявлений на завершенные приемы.
        /// </summary>
        public const string RequestWithoutBookingDate = "39320CB5-1D19-43A1-A436-DA218D982604";

        /// <summary>
        ///     работа с межвед запросами.
        /// </summary>
        public const string InteragencyRequestManage = "03665FC3-6AFB-4E98-AA91-2ADC2211D6D4";

        /// <summary>
        ///     управление справочниками.
        /// </summary>
        public const string VocabularyManage = "A2F9D19E-CAD1-42B3-86A6-8371400161C9";

        /// <summary>
        ///     управление заездами
        /// </summary>
        public const string BoutManage = "4EE49B38-638A-4E66-8417-A4206DBB1EAF";

        /// <summary>
        ///     просмотр городов
        /// </summary>
        public const string CityView = "A1564833-C78E-4ACE-8A9B-5D2ADD316A27";

        /// <summary>
        ///     управление городами
        /// </summary>
        public const string CityManage = "23ECFB6D-3792-48F6-A689-ED24B0C1CBEC";

        /// <summary>
        ///     управление отрядами
        /// </summary>
        public const string PartyManage = "461E7FCC-4053-441A-8247-445FB7FC7BB7";

        /// <summary>
        ///     управление рейсами
        /// </summary>
        public const string DirectoryFlightsManage = "46A17948-EEE2-456C-8B4F-E9157D30278B";

        /// <summary>
        ///     управление транспортом
        /// </summary>
        public const string TransportInfoManage = "E91659DE-E684-486F-A6EA-0B8C4EAC774A";

        /// <summary>
        ///     Редактирование заявления после регистрации
        /// </summary>
        public const string EditAfterRegistration = "961e705d-d786-4f54-92d6-f067dc815b3e";

        /// <summary>
        ///     Повторный запрос в базовом регистре
        /// </summary>
        public const string RetryRequestInBaseRegistry = "4794BDE0-006C-46F2-820E-3BB7D0127AC1";

        /// <summary>
        ///     Заявление. Подача заявления на дополнительные места
        /// </summary>
        public const string AddonRequest = "38881258-c05b-4c43-8ec1-6efaf7391a4d";

        /// <summary>
        ///     Заявление. Услуги
        /// </summary>
        public const string AddonServiceInRequest = "78C2674F-4DA9-4FD2-9386-1382ADA9984A";

        /// <summary>
        ///     Заявление. Исключение ребенка
        /// </summary>
        public const string ExcludeChild = "240A61BB-3372-41E5-A7FD-34FFB10EF792";

        /// <summary>
        ///     Заявление. Удаление черновика
        /// </summary>
        public const string RemoveDraft = "BCA3A988-CC25-4DEE-A4FC-89FAF9402416";

        /// <summary>
        ///     Заявление. Редактирование "Видов нарушений".
        /// </summary>
        public const string RequestEditTypeViolation = "862122F2-1EEB-44F0-B8D8-0B535D6E75E6";

        /// <summary>
        ///     Заявление. Многоэтапная кампания. Отказать по 1075.3.
        /// </summary>
        public const string RequestTo10753 = "B1056F0D-F0F7-43D9-81AE-3EB21826C637";


        /// <summary>
        ///     платежи
        /// </summary>
        public static class Payments
        {
            /// <summary>
            ///     просмотр.
            /// </summary>
            public const string View = "B293E561-59A0-4E0F-ADCA-23B1EB8DD179";

            /// <summary>
            ///     привязывать к начислению
            /// </summary>
            public const string LinkToCalculation = "C9283299-C783-4516-81A8-B3163288DB49";

            /// <summary>
            ///     загрузка платежей
            /// </summary>
            public const string LoadPayments = "1257D7AA-140F-4D20-9B49-89072693C36B";
        }

        /// <summary>
        ///     работа с начислениями
        /// </summary>
        public static class Calculation
        {
            /// <summary>
            ///     просмотр начислений
            /// </summary>
            public const string View = "33744935-59F4-4FCF-9696-65586392FD37";

            /// <summary>
            ///     в статус оплачено
            /// </summary>
            public const string ToPaid = "DA72C29B-E28C-489C-B06A-F86752C016FC";

            /// <summary>
            ///     в статус отменено
            /// </summary>
            public const string ToCancelled = "F7D69594-7D0B-4B97-BB1C-744A5239CE2D";
        }

        /// <summary>
        ///     управление администратором заездов
        /// </summary>
        public static class AdministratorTour
        {
            /// <summary>
            ///     просмотр администраторами смен
            /// </summary>
            public const string View = "E8FAF199-0F15-4F51-AD14-E74121693C66";

            /// <summary>
            ///     управление администраторами смен
            /// </summary>
            public const string Manage = "CFC95BE5-2581-4051-90CD-60AE5950CF49";

            /// <summary>
            ///     создание пользователя
            /// </summary>
            public const string CreateAccount = "76ED018B-2340-43D0-9D1D-27F316BCE720";

            /// <summary>
            ///     В статус Редактирование
            /// </summary>
            public const string ToEdit = "20740DC7-85A0-471F-A7F0-E3E55E7AFAA7";

            /// <summary>
            ///     В статус Сформирован
            /// </summary>
            public const string ToFormed = "F9899AA0-3EEB-4D9B-98E4-455E7B76B718";
        }

        /// <summary>
        ///     доп места
        /// </summary>
        public static class AddonService
        {
            /// <summary>
            ///     Доп услуги просмотр
            /// </summary>
            public const string View = "04569ce2-92e9-4525-a376-c975346b9d6f";

            /// <summary>
            ///     Доп места редактирование
            /// </summary>
            public const string Edit = "a5a28d42-6fae-4630-9690-c0e36874ffde";

            /// <summary>
            ///     Доп места сформировано
            /// </summary>
            public const string ToFormed = "23299388-f1e9-4315-b8e1-500ab88d87fd";

            /// <summary>
            ///     Доп места в формирование
            /// </summary>
            public const string ToForming = "3e9af208-3517-4c53-a0e5-bea20d57c58f";

            /// <summary>
            ///     в архив
            /// </summary>
            public const string ToArchive = "A8643CA0-C557-455E-B340-837363423A1C";
        }

        /// <summary>
        ///     работа с организациями
        /// </summary>
        public static class Organization
        {
            /// <summary>
            ///     редактирование
            /// </summary>
            public const string View = "836e67f7-423e-4a51-968b-44092837a843";

            /// <summary>
            ///     редактирование
            /// </summary>
            public const string Edit = "aa737696-fab9-4d02-a42a-fb465e425886";
        }

        /// <summary>
        ///     работа с лимитами по ОИВ
        /// </summary>
        public static class Limits
        {
            /// <summary>
            ///     Ввод квот по ОИВ
            /// </summary>
            public const string LimitToOiv = "40e8a2f7-ba74-45da-8dda-e7a7dca0580f";

            /// <summary>
            ///     ввод квот по организациям
            /// </summary>
            public const string LimitByOrganization = "3e112db4-f73f-4dfd-ab3c-0ebe80b6cad4";

            /// <summary>
            ///     ввод детей по квотам по организациям
            /// </summary>
            public const string LimitChildInOrganization = "ebd59da5-7f05-4832-a655-7f90b54c2e5d";
        }

        /// <summary>
        ///     Заезд
        /// </summary>
        public static class Bout
        {
            /// <summary>
            ///     Просмотр вожатым или администратором смены
            /// </summary>
            public const string Counselor = "E32E4D1A-4CF6-4106-814F-789A34F1F60B";

            /// <summary>
            ///     Формирование заезда
            /// </summary>
            public const string Forming = "F4902813-01E8-43A4-9CE0-127E50A89FAA";

            /// <summary>
            ///     Изменение признака "Не явился в место отдыха" отдыхающего в любом заезде
            /// </summary>
            public const string NotComeInPlaceOfRestForAllBouts = "65326BE1-E2AB-495D-86C4-26DF79304FDE";

            /// <summary>
            ///     Администратор смены
            /// </summary>
            public const string AdministratorTour = "8DB23638-3ECF-41B4-8A8C-DF57D4E7FCA9";

            /// <summary>
            ///     Редактирование заезда
            /// </summary>
            public const string Edit = "F6735E85-CD1B-447B-A45B-F43E4861245D";

            /// <summary>
            ///     Подтверждение заезда
            /// </summary>
            public static string Confirmed = "4C97D606-C981-45FD-A188-8291786D38B9";

            /// <summary>
            ///     Закрыт
            /// </summary>
            public static string Closed = "2484C519-8942-4CE5-A9F6-64171988CD7E";

            /// <summary>
            ///     Из статуса закрыт в статус подтверждено
            /// </summary>
            public static string FromClosedToConfirmed = "670D2C1D-1DC3-4789-A391-BBE31F4E113D";

            /// <summary>
            ///     Из статуса подтверждено в статус сформировано
            /// </summary>
            public static string FromConfirmedToFormed = "BFB7EFAF-F800-4E2B-864D-30FB1A60A0C9";
        }

        /// <summary>
        ///     Отряд
        /// </summary>
        public static class Party
        {
            /// <summary>
            ///     Формирование отряда
            /// </summary>
            public const string Forming = "C29D5AA2-ACB2-4747-9025-221E107DF1C6";

            /// <summary>
            ///     Редактирование отряда
            /// </summary>
            public const string Edit = "7EABDACE-AAF6-4CF7-8ADF-FE1BF2AA3388";
        }

        /// <summary>
        ///     отчеты
        /// </summary>
        public static class Report
        {
            public const string Report1 = "959d39ea-e7cb-4143-a88a-17f658fc4fea";

            public const string ServiceStatistics = "66FE7FF9-979A-4107-A814-44209012B9BC";

            /// <summary>
            ///     отчет по заездам
            /// </summary>
            public const string TourReport = "e777763d-1a8e-47f2-9cae-e49141cde42b";

            /// <summary>
            ///     отчет по статистика обработки заявлений
            /// </summary>
            public const string StatisticReport = "8174066f-8a64-4022-a3ec-a5e1f117c07b";

            /// <summary>
            ///     отчет по профльным лагерям
            /// </summary>
            public const string SpecializedCampsReport = "9C55D499-CDE2-428D-B94B-C2A27E544499";
        }

        /// <summary>
        ///     Рейсы
        /// </summary>
        public static class DirectoryFlights
        {
            /// <summary>
            ///     Формирование рейса
            /// </summary>
            public const string Form = "318247BC-949D-44FC-B045-7EEC67FDBFA0";

            /// <summary>
            ///     Редактирование рейса
            /// </summary>
            public const string Edit = "9E55B8C4-0662-4D25-B07F-74A63C9693A2";
        }

        /// <summary>
        ///     Транспорт
        /// </summary>
        public static class Transport
        {
            /// <summary>
            ///     просмотр транспорта
            /// </summary>
            public const string View = "AC04EDCE-7142-4C66-96AA-D6B58C9088A8";

            /// <summary>
            ///     Формирование
            /// </summary>
            public const string Form = "ecb868b4-bdaf-4879-adb0-e3681312ebc5";

            /// <summary>
            ///     Редактирование
            /// </summary>
            public const string Edit = "037154b5-3996-4738-8285-478ee148a703";

            /// <summary>
            ///     Указание причины отказа от обратного билета
            /// </summary>
            public const string SetNotNeedTicketReason = "5869E559-77ED-41E0-8ADC-8BF7D2C21F70";

            /// <summary>
            ///     Отказ от билета
            /// </summary>
            public const string SetNotNeedTicket = "6C13E705-DAE5-4C13-8193-CE6F39689EE4";
        }

        /// <summary>
        ///     управление задачами
        /// </summary>
        public static class CounselorTask
        {
            /// <summary>
            ///     право на просмотр.
            /// </summary>
            public const string View = "F722DDEC-8CE4-445D-B79C-E96CDC758DEF";

            /// <summary>
            ///     право на просмотр всех.
            /// </summary>
            public const string ViewAll = "7D7584D2-A454-4513-AB27-14DB6EFECA09";

            /// <summary>
            ///     право на редактирование.
            /// </summary>
            public const string Edit = "E5E8A7F4-A7EE-4A46-92B2-3AA00369CBB2";

            /// <summary>
            ///     право на удаление.
            /// </summary>
            public const string DeleteTask = "7F24B1F5-1B95-46F2-8909-B0FE3025487E";

            /// <summary>
            ///     Редактирование поставленой задачи
            /// </summary>
            public const string EditDeliveredTask = "A594140D-B572-4DC2-B689-0C4E505BC4E3";

            /// <summary>
            ///     в статус задача поставлена
            /// </summary>
            public const string ToDelivered = "9EE06349-D238-4CBB-8445-49B090318DB1";

            /// <summary>
            ///     в статус задача Решена
            /// </summary>
            public const string ToSolved = "C0E674E0-57E2-4843-8E4C-C2206252E873";

            /// <summary>
            ///     в статус задача на согласовании
            /// </summary>
            public const string ToApproved = "64F0FE2E-5B79-4CA6-949C-376E14AE0FEE";

            /// <summary>
            ///     в статус задача на доработку
            /// </summary>
            public const string ToCompletion = "7E7AB824-4B19-43E4-A664-2130B717657C";

            /// <summary>
            ///     в статус прочитано
            /// </summary>
            public const string ToReaded = "D42977EC-CC15-4610-8BDC-C4287B943E84";

            /// <summary>
            ///     в статус не прочитано
            /// </summary>
            public const string ToUnreaded = "FB8711A8-CEDD-4E66-B590-EBDD261BF0F3";
        }

        /// <summary>
        ///     управление тестами
        /// </summary>
        public static class CounselorTest
        {
            /// <summary>
            ///     право на просмотр.
            /// </summary>
            public const string View = "621FD461-D63A-43F4-B0EE-C8AAC34969EB";

            /// <summary>
            ///     право на редактирование.
            /// </summary>
            public const string Edit = "46FDAC6D-BFD5-4EB8-AB0D-DAA98577B755";

            /// <summary>
            ///     в статус редактирование.
            /// </summary>
            public const string ToEdit = "18C54F18-3963-487C-8B1F-576AB307D148";

            /// <summary>
            ///     в статус сформировано.
            /// </summary>
            public const string ToFormed = "57910545-3B77-46F2-A9A8-1F800F50AD1C";
        }

        /// <summary>
        ///     управление тестами
        /// </summary>
        public static class TrainingCounselors
        {
            /// <summary>
            ///     право на просмотр.
            /// </summary>
            public const string View = "C0D2E0CC-613F-43FC-89C3-9878CCB8C0F3";

            /// <summary>
            ///     право на редактирование.
            /// </summary>
            public const string Edit = "ED39A0BA-C9CD-44A9-BEA8-4FF44B4FB916";

            /// <summary>
            ///     в статус редактирование.
            /// </summary>
            public const string ToEdit = "1DA79A01-04CA-4750-80DC-1B569C9BF074";

            /// <summary>
            ///     в статус сформировано.
            /// </summary>
            public const string ToFormed = "BDE9D694-6DC9-48D2-BAF1-731DDF930D1E";

            /// <summary>
            ///     в статус обучение закончено.
            /// </summary>
            public const string ToEducationFinished = "2FFAAAAC-988E-4A2E-905D-1B7445B80E9A";

            /// <summary>
            ///     Установка признака обучение завершено.
            /// </summary>
            public const string SetEducationFinished = "38B14902-1F09-4F97-9151-7FB05B167300";
        }

        /// <summary>
        ///     комерческие путевки
        /// </summary>
        public static class CommercialTour
        {
            /// <summary>
            ///     просмотр заявок
            /// </summary>
            public const string Request = "C87D1407-E4E2-4578-8641-2E8618314B1E";

            /// <summary>
            ///     редактирование
            /// </summary>
            public const string RequestEdit = "F379C0EA-A726-4C92-A548-4D9E88A20F11";

            /// <summary>
            ///     просмотр проектов
            /// </summary>
            public const string ProductView = "FB613813-EFE2-46C8-9F15-C86A3B141BBE";

            /// <summary>
            ///     редактирование продуктов
            /// </summary>
            public const string ProductEdit = "7E302B94-2DF8-454A-AAEE-DD2A66536642";

            /// <summary>
            ///     можно редактировать цену
            /// </summary>
            public const string MayPriceEdit = "B86D11A4-0A00-45B3-8CA7-A401FE30B9F6";

            /// <summary>
            ///     можно редактировать подтверждать
            /// </summary>
            public const string MayApprove = "22CABD61-74CD-4C90-B4B9-465199A96E7E";

            /// <summary>
            ///     Просмотр себестоимости (в заявке и карточке продукта)
            /// </summary>
            public const string InternalPriceView = "6FF0ECE9-C991-4C72-A5A3-944D58BDBB10";
        }

        /// <summary>
        ///     управление педотрядами
        /// </summary>
        public static class PedParty
        {
            public const string PedPartyView = "4FB265E3-74C8-4C92-97EF-FC469D163DE4";
            public const string PedPartyManage = "3401BCB8-5DAF-4D14-91DA-4D14D824AC4A";
            public const string PedPartyToEdit = "09DC9F5D-27A4-456F-937A-7379DB66A33B";
            public const string PedPartyToFormed = "E1E916AA-FB07-42FB-A7C8-EE865BF38139";
        }

        public static class AnalyticReports
        {
            [Display(Name = "Индивидуальный отдых. Распределение по году рождения")]
            public const string BenefitRestChildByAgeAndSex = "77229154-3DDF-48C7-B347-B497464B7B73";

            [Display(Name = "Совместный отдых. Распределение по году рождения")]
            public const string BenefitFamilyRestByAgeAndSex = "39F4CF5A-337B-4A77-9332-CB7339E285FB";

            [Display(Name = "Индивидуальный отдых. Распределение по категориям льгот и округам")]
            public const string BenefitRestChildByCategoryAndDistrict = "F0A218A6-A6A7-43E6-AC23-02F9C04181C2";

            [Display(Name = "Совместный отдых. Распределение по категориям льгот и округам")]
            public const string BenefitFamilyRestByCategoryAndDistrict = "0C9F0497-304A-4F05-81CD-3448B594140B";

            [Display(Name = "Индивидуальный отдых. Недозаезды")]
            public const string BenefitRestChildByBoutCompleteness = "0A587355-178F-4BFD-9CE8-79708E39D808";

            [Display(Name = "Совместный отдых. Недозаезды")]
            public const string BenefitFamilyRestByBoutCompleteness = "2E151C59-105D-47B0-98A2-0C45A08BFD8C";

            [Display(Name = "Оказание транспортных услуг")]
            public const string ByTransportServices = "8318318B-5990-49BF-AA50-98361A30DC6B";

            [Display(Name = "Оказание услуг по проживанию")]
            public const string ByResidenceServices = "60F16E80-23BD-49F4-AC36-B4F50435029B";

            [Display(Name = "Востребованность по ОИВ")]
            public const string SpecializedCampsByVedomstvo = "C0E54EEC-28D0-43F9-94EA-E0483E3AD8BD";

            [Display(Name = "Востребованность по учреждениям")]
            public const string SpecializedCampsByOrganizations = "F7F2033D-96B3-4478-AAE3-862757B059BA";

            [Display(Name = "Профильные лагеря. Распределение по году рождения и регионам")]
            public const string SpecializedCampsByAgeAndRegions = "84A4C70B-C322-4586-8D14-CE9BD7F7B798";

            [Display(Name = "Востребованность номеров в совместном отдыхе")]
            public const string RestWithChildTypeOfRooms = "38B38DC8-B0DB-4A24-8758-A90A7D36BD68";

            [Display(Name = "ЕГИССО")] public const string EGISO = "9F94888C-43F9-4422-A7A2-E013EE35849A";
        }

        /// <summary>
        ///     комерческие заявки
        /// </summary>
        public static class Request
        {
            /// <summary>
            ///     В подтверждение
            /// </summary>
            public const string ToOnApprove = "319E7712-3C9B-472F-B1EE-716FF4722184";

            /// <summary>
            ///     В готово к оплате
            /// </summary>
            public const string ToReadyToPay = "90FE7445-DF82-4C0A-98EA-90239FC20378";


            /// <summary>
            ///     В аннулировано
            /// </summary>
            public const string ToOnWorkig = "138DAF0C-B016-44EC-866B-EA8781724F7D";

            /// <summary>
            ///     В готово к оплате
            /// </summary>
            public const string ToReadyToPayFull = "A7BA2902-7D2B-4DB2-89C3-343B3FD3F78A";

            /// <summary>
            ///     В оплачено.
            /// </summary>
            public const string ToCertificateIssued = "8084EBB5-FA98-47B0-9E27-C29A68E87617";

            /// <summary>
            ///     В редактирование
            /// </summary>
            public const string ToDraft = "01B39852-5C6A-407F-BD0A-E32606DF2871";

            /// <summary>
            ///     В анулировано
            /// </summary>
            public const string ToReject = "2004E68C-1A8F-4910-93A9-EC2A23A5E4A8";

            /// <summary>
            ///     В отказ
            /// </summary>
            public const string ToDenial = "7262E185-CBC8-466B-80F4-C74CFF63B6AB";

            /// <summary>
            ///     В оплачено (аванс)
            /// </summary>
            public const string ToPayed = "8BAC5BF7-AFF9-4846-9FE9-55D88D6568EB";

            /// <summary>
            ///     редактирование людей во всех статусах
            /// </summary>
            public const string EditPersonInOtherState = "18ECA0B0-4106-4F81-8B51-B3CCC2235217";
        }

        /// <summary>
        ///     Профсоюзные списки
        /// </summary>
        public static class TradeUnionList
        {
            /// <summary>
            ///     Просмотр
            /// </summary>
            public const string View = "99564216-196C-4F13-B0DF-3C80FED069B1";

            /// <summary>
            ///     Редактирование
            /// </summary>
            public const string Edit = "8AF574CA-EE21-482C-BB51-039D800E09F0";

            /// <summary>
            ///     В редактирование
            /// </summary>
            public const string ToEdit = "B7BD5C15-7427-443C-8821-37B9BC0D8B2B";

            /// <summary>
            ///     В редактирование (из других статусов)
            /// </summary>
            public const string ToEditFromAll = "2AA0B261-95BA-43DC-A9C2-44CA28955450";

            /// <summary>
            ///     На утверждение
            /// </summary>
            public const string ToOnAproving = "241664AC-937E-418A-8A07-004CB88FC702";

            /// <summary>
            ///     Утверждено
            /// </summary>
            public const string ToApproved = "17C2D40E-B0B3-4ADE-BF7F-AB11752DB237";

            /// <summary>
            ///     Отклонено
            /// </summary>
            public const string ToDeclined = "097CBC9E-7520-4AA6-A5D0-0139A4096205";

            /// <summary>
            ///     Сведения о заехавших внесены
            /// </summary>
            public const string ToFinish = "2E557E4C-8D29-41F0-8C58-BB1E40D15703";
        }

        /// <summary>
        ///     Иинформационная Безопасность (ИБ)
        /// </summary>
        public static class Security
        {
            /// <summary>
            ///     Администратор информационной безопасности
            /// </summary>
            public const string Login = "388fb1f9-c191-4450-b603-a20300a7e480";

            /// <summary>
            ///     Просмотр настроек безопасности
            /// </summary>
            public const string SecuritySettingsView = "be66c69d-da60-499b-959d-f398239357fb";

            /// <summary>
            ///     Сохраненеие настроек безопасности
            /// </summary>
            public const string SecuritySettingsEdit = "240d4489-4c9b-4b57-a6db-66472482aab0";

            /// <summary>
            ///     Просмотр журнала входов в систему
            /// </summary>
            public const string JournalEntrance = "383ba90f-aa6c-4e0c-840f-b55db9a8ca66";

            /// <summary>
            ///     Просмотр журнала процессов и программ
            /// </summary>
            public const string JournalProceses = "b74d4a6d-4e31-4227-b7a5-5a7e02dfd4d8";

            /// <summary>
            ///     Просмотр журнала изменения ролей пользователя
            /// </summary>
            public const string JournalRoles = "e7d5db3d-4579-4383-934c-cee6ff823002";

            /// <summary>
            ///     Просмотре журнала сеансов (сессий)
            /// </summary>
            public const string JournalSessions = "bd75c16a-9ecd-475d-aeba-9333e5ad8168";

            /// <summary>
            ///     Принудительная остановка сессии
            /// </summary>
            public const string StopSessions = "9649d573-e9ca-4b73-90b9-4550e22ca50a";

            /// <summary>
            ///     Журнал уведомлений безопасности
            /// </summary>
            public const string JournalSecurity = "415670ce-abaa-4531-afb3-021d47f85640";

            /// <summary>
            ///     Журнал взаимодействий с ИС
            /// </summary>
            public const string IteractionsWithOutSystems = "70877063-c795-4539-9f3a-feaeee3cfda4";
        }

        /// <summary>
        ///     Сироты
        /// </summary>
        public static class Orphans
        {
            /// <summary>
            ///     Основопологающее право на блок сирот
            /// </summary>
            public const string Main = "33483762-F31A-49F4-8C3D-8DA44AC138EE";

            /// <summary>
            ///     Работа с групами (потребностями) приютов
            /// </summary>
            public const string PupilGroup = "E678D361-CF4D-4734-9C61-CEB3871E3631";

            /// <summary>
            ///     Сформировать группу (потребность) приюта
            /// </summary>
            public const string PupilGroupForm = "B4073DF3-4C13-4B7E-BEBC-87290A4C170A";

            /// <summary>
            ///     Отправить на согласование группу (потребность) приюта
            /// </summary>
            public const string PupilGroupOnAgreement = "9B7729D3-2889-4890-861B-46BA05187F30";

            /// <summary>
            ///     Согласовать группу (потребность) приюта
            /// </summary>
            public const string PupilGroupAgree = "507A377B-0E4B-482F-9924-EB064B0B710B";

            /// <summary>
            ///     Утвердить группу (потребность) приюта
            /// </summary>
            public const string PupilGroupApprove = "D7A2D243-4260-4304-989A-B269CCAF1406";

            /// <summary>
            ///     Редактировать группу (потребность) приюта
            /// </summary>
            public const string PupilGroupEdit = "52E98EFB-3760-4D9B-91E3-AA38EFE9FE1A";

            /// <summary>
            ///     Удалить группу (потребность) приюта
            /// </summary>
            public const string PupilGroupDelete = "C7A263B0-6EE0-49BF-9824-70B18CF7248E";

            /// <summary>
            ///     Работа со списками (группы отправки) приюта
            /// </summary>
            public const string PupilGroupList = "4E5B3F3C-6C1E-43DA-B3DE-3CDA1E9FB97E";

            /// <summary>
            ///     Редактировать список (группу отправки) приюта
            /// </summary>
            public const string PupilGroupListEdit = "FAB8F9B1-D9A0-4FCB-9731-BA4DA840C301";

            /// <summary>
            ///     Сформировать список (группу отправки) приюта
            /// </summary>
            public const string PupilGroupListForm = "0E76CA90-2A66-4503-8741-2F4B353F9F16";

            /// <summary>
            ///     Утвердить список (группу отправки) приюта
            /// </summary>
            public const string PupilGroupListApprove = "A8F5FA31-CC6E-4B1C-B8ED-CC96EE98FC76";

            /// <summary>
            ///     Удалить список (группу отправки) приюта
            /// </summary>
            public const string PupilGroupListDelete = "8C060CA7-87EF-45F1-ACF7-1D8FD6D6794C";
        }


        /// <summary>
        ///     Заезды (мобильное приложение)
        /// </summary>
        public static class NewBout
        {
            /// <summary>
            ///     просмотр заездов в рамках мобильного приложения
            /// </summary>
            public const string View = "B25AA833-DBD7-42A0-9689-72832FE823D7";
        }

        /// <summary>
        ///     Задание
        /// </summary>
        public static class Task
        {
            /// <summary>
            ///     просмотр заданий
            /// </summary>
            public const string View = "17A54463-B26E-4328-8760-39AB42199344";
        }


        /// <summary>
        ///     Подарки
        /// </summary>
        public static class Gift
        {
            /// <summary>
            ///     просмотр
            /// </summary>
            public const string View = "17A54463-B26E-4328-8760-39AB42199344";
        }


        /// <summary>
        ///     Зарезервированные подарки
        /// </summary>
        public static class GiftReserved
        {
            /// <summary>
            ///     просмотр
            /// </summary>
            public const string View = "17A54463-B26E-4328-8760-39AB42199344";
        }

        #region Перевод в статусы

        /// <summary>
        ///     Перевод в статусы заявлений
        /// </summary>
        public static class Status
        {
            #region Общие переходы

            //new Status{Id = 1040, Name = "Подано. Заявка находится на рассмотрении", MpguName = "Подано", MpguDescription = "Подано. Заявка передана в ОИВ и находится на рассмотрении.", MpguComment = "Заявление получено ведомством.", ExternalUid = "1040"},
            public const string ToSend = "3F829F62-9CC5-4098-8890-0FE7D2D07CB2";

            //new Status{Id = 1090, Name = "Отозвано. По инициативе заявителя", MpguName = "Отозвано", MpguDescription = "Отозвано по инициативе Заявителя", MpguComment = "Отозвано по инициативе заявителя.", ExternalUid = "1090"},
            public const string ToCancelByApplicant = "02D4868E-10F7-43CE-BA71-5A8ABA45837D";

            /// <summary>
            ///     В отказ в регистрации
            /// </summary>
            public const string ToRegistrationDecline = "B7A495CC-716B-486B-8870-92C4DDAA3A2C";

            /// <summary>
            ///     В отказ в регистрации (по сопровождению)
            /// </summary>
            public const string ToRegistrationDeclineAttendant = "EBEAD29E-5136-43CE-B7AD-44E3F7F9A49E";

            #endregion

            #region Переходы формы при бронировании услуг

            //new Status{Id = 1055, Name = "Приостановлено. Ожидание прихода заявителя", MpguName = "На исполнении", MpguDescription = "Ожидание прихода Заявителя в ОИВ для подтверждения сведений, указанных в заявлении.", MpguComment = "Ожидание предоставления сведений", ExternalUid = "1055"},
            public const string ToWaitApplicant = "E002AF2F-2D2D-4DA1-AC5E-BC9A16535AC2";

            //new Status{Id = 1080, Name = "Услуга оказана. Отказ в предоставлении услуги", MpguName = "Услуга оказана", MpguDescription = "Отказ в предоставлении услуги", MpguComment = "Отказ в предоставлении путевки", ExternalUid = "1080"},
            public const string ToReject = "F7A257FC-A96C-4FB5-A82B-A5AC56B3F4CA";

            /// <summary>
            ///     проверка оператором (7704 - На исполнении. Сбор сведений)
            /// </summary>
            public const string ToOperatorCheck = "5E8DDA54-7F72-459C-9D13-CBF69054A906";

            /// <summary>
            ///     отказать из ожидания заявителя.
            /// </summary>
            public const string ToRejectNormal = "3D4EC560-5361-4035-8E00-35C85E64C8A6";

            //new Status{Id = 1069, Name = "Приостановлено. Запрос на отзыв по инициативе заявителя", MpguName = "Приостановлено", MpguDescription = "Запрос на отзыв по инициативе Заявителя", MpguComment = "Запрос на отзыв по инициативе Заявителя.", ExternalUid = "1068"},
            public const string ToStoped = "EE995F13-16F4-49FA-862A-B08B15992447";

            /// <summary>
            ///     редактирование в статусе Ожидание прихода заявителя
            /// </summary>
            public const string EditInWaitApplicant = "3e52e086-e7ac-4acf-b19b-247c2c617405";

            /// <summary>
            ///     На исполнении. Формирование результата предоставления услуги.
            /// </summary>
            public const string ApplicantCome = "c8f43c88-fd66-4029-aa61-403a9b82108f";

            /// <summary>
            ///     Услуга оказана. Услуга оказана.
            /// </summary>
            public const string CertificateIssued = "ed3fa5a3-d316-4d54-8715-33ba1e50c56b";

            /// <summary>
            ///     Отказ из услуги оказана.
            /// </summary>
            public const string ToRejectFromCertificateIssued = "0158c875-764a-4fdb-bf12-8826f9a88ebf";

            /// <summary>
            ///     перевод в статус готово к оплате
            /// </summary>
            public const string ToReadyToPay = "8b5b7dc3-4189-4cef-94b5-e8269a57dfca";

            #endregion

            #region Переходы заявлений через двух этапную заявочную кампанию

            /// <summary>
            ///     В ранжирование
            /// </summary>
            public const string FcToRanging = "33039E99-9360-47B2-AF29-6707EC0AFF01";

            /// <summary>
            ///     В включено в список
            /// </summary>
            public const string FcToIncludedInList = "4407104C-99FD-4667-A4EB-D654C264F34E";

            /// <summary>
            ///     В принятие решения
            /// </summary>
            public const string FcToDecisionMaking = "27D4DBFA-0B91-4D94-A2CE-A5F973414DA4";


            /// <summary>
            ///     В принятие решения по Covid
            /// </summary>
            public const string FcToDecisionMakingCovid = "B0BE09D1-FF9D-43B3-86F3-A5FEC002E8DC";

            /// <summary>
            ///     В решение принято
            /// </summary>
            public const string FcToDecisionIsMade = "A2775798-3334-4844-908E-F1DDF50F1086";

            /// <summary>
            ///     В вызов для подтверждения родства
            /// </summary>
            public const string FcToWaitApplicant = "FC81AE6F-A4A5-43AF-A4D4-3AA958326D2C";

            /// <summary>
            ///     В услуга оказана
            /// </summary>
            public const string FcToCertificateIssued = "ACEB10BA-F001-4CC4-82D0-A4D77C299192";

            /// <summary>
            ///     Реквизиты счета подтверждены
            /// </summary>
            public const string FcToMoneyAccountAccepted = "2BC7B63A-FBFF-49B1-9447-AD47645EC6AB";

            /// <summary>
            ///     В вызов для подтверждения данных для платежа
            /// </summary>
            public const string FcToWaitApplicantMoney = "86446190-7D06-4AFC-9CE2-34B105BC1C80";

            /// <summary>
            ///     В отказ
            /// </summary>
            public const string FcToReject = "47F4165A-25F4-4C56-9B32-2834957004E1";

            /// <summary>
            ///     Отзыв с сайта
            /// </summary>
            public const string FcToCancelByRequest = "3AF53C73-1CE6-4228-B76D-25C99F8C7C56";

            /// <summary>
            ///     Завершить обработку заявления
            /// </summary>
            public const string FcFinishWorkWithRequest = "A4DDEFDE-94C0-40A6-8F41-DB630A63B629";

            /// <summary>
            ///     Восстановить заявление
            /// </summary>
            public const string FcRepareRequest = "E8B9EB29-7C52-4567-8BE6-D4869F70418A";

            /// <summary>
            ///     Уведомление заявителю по уточнению выплаты
            /// </summary>
            public const string FcNotComeOnMoney = "215577E7-AF79-463A-A8E7-8423DEB486D9";

            #endregion
        }

        #region квоты по одаренным детям

        public static class Limit
        {
            /// <summary>
            ///     отображение квот для сирот
            /// </summary>
            public const string ViewOrphan = "B3C2EE83-18EB-458F-85C8-D2BD6EEC4590";

            /// <summary>
            ///     отображение квот для профильников
            /// </summary>
            public const string ViewProfile = "4ADF3B32-BD7E-49AE-86C1-4B5ABB7359A7";


            #region статусы для списка организации

            public static class List
            {
                /// <summary>
                ///     Формирование
                /// </summary>
                public const string Formation = "28557651-92ad-4d42-8b05-f6539a23fe81";

                /// <summary>
                ///     Сформирован
                /// </summary>
                public const string Formed = "633f588e-9e4c-49bd-8fb8-8a92a2b64784";

                /// <summary>
                ///     Включен в заезд
                /// </summary>
                public const string IncludedInTour = "9b5c4557-0876-43db-8291-f678837091c2";

                /// <summary>
                ///     Сведения об оплате внесены
                /// </summary>
                public const string IncludedPayment = "27417052-86f7-476d-8ec6-9d76f5800856";

                /// <summary>
                ///     Редактирование в любом статусе
                /// </summary>
                public const string EditInAllStates = "3af8a160-8e73-46ba-8c92-dc1ffacef170";
            }

            #endregion

            #region квота по организации

            public static class Organization
            {
                /// <summary>
                ///     Формирование
                /// </summary>
                public const string Formation = "682366e7-f154-40c8-9971-8db182cb5526";

                /// <summary>
                ///     Доведены до организации
                /// </summary>
                public const string Brought = "4082f02b-ef7f-44ea-8f90-f73d55b14e7c";

                /// <summary>
                ///     Доведены до организации после доработки
                /// </summary>
                public const string BroughtToCompletion = "bf69dd86-4c12-496f-b63a-d9855e5506af";

                /// <summary>
                ///     Отправлена на утверждение в ОИВ
                /// </summary>
                public const string ToApprove = "8c0322ad-1474-45d1-bbd7-7dd3a72ec4a7";

                /// <summary>
                ///     Утверждено
                /// </summary>
                public const string Approved = "bdbeb1be-1f50-4d1a-b50e-b669754e377c";

                /// <summary>
                ///     На доработке
                /// </summary>
                public const string OnCompletion = "6d2758cb-7b2a-45b3-932e-047a85387933";

                /// <summary>
                ///     подтверждено ДКгМ
                /// </summary>
                public const string Confirmed = "6ac55c09-8a0f-4bfc-8b98-733b9f87d54a";

                /// <summary>
                ///     Отправлено на доработку ДКгМ
                /// </summary>
                public const string ApprovedToCompetiotion = "a31d032d-b0b7-4c52-8a6a-696ba86c2fcd";

                /// <summary>
                ///     Вернуть на утверждение в ОИВ
                /// </summary>
                public const string ConfirmedToApproved = "51570B14-66F8-4084-936D-93C2CB5C9B83";
            }

            #endregion

            #region квота по ОИВ

            public static class Oiv
            {
                /// <summary>
                ///     Формирование
                /// </summary>
                public const string Formation = "20364c5e-7718-4602-a25b-d5bb95dd2471";

                /// <summary>
                ///     Доведены до организации
                /// </summary>
                public const string Brought = "188434b7-ed98-4aaf-aae7-d2f508a0efbc";

                /// <summary>
                ///     Доведены до организации после доработки
                /// </summary>
                public const string BroughtToOrganization = "3092d40b-9629-4a92-bca3-fb4ae612754e";

                /// <summary>
                ///     На доработку (не статус)
                /// </summary>
                public const string OnCompletion = "23bc4a9a-05c4-4ca1-b5d2-846c1f3ad6cb";

                /// <summary>
                ///     Сбор потребностей
                /// </summary>
                public const string GatheringRequirements = "A159953F-B491-4FC2-BD65-B3786D483841";
            }

            #endregion

            /// <summary>
            ///     заявки на списки
            /// </summary>
            public static class Request
            {
                /// <summary>
                ///     просмотр
                /// </summary>
                public const string View = "4F0458D8-3457-4734-AD8E-A7CCA8ABBF8A";

                /// <summary>
                ///     Редактирование
                /// </summary>
                public const string Edit = "BAC94130-ADBD-41C5-AD46-549D1F23E08D";

                /// <summary>
                ///     Отправить на утверждение
                /// </summary>
                public const string ToApprove = "625E1D62-CCA2-478B-8E99-A323C9E2FC01";

                /// <summary>
                ///     утвердить
                /// </summary>
                public const string Approve = "ED81AD6F-033E-4963-B321-31768614A2E1";

                /// <summary>
                ///     Отказать
                /// </summary>
                public const string Decline = "EF32C50D-5152-47C4-8CB3-675FB56F3E58";
            }
        }

        #endregion

        /// <summary>
        ///     Блок мест
        /// </summary>
        public static class Tour
        {
            /// <summary>
            ///     работа с услугами
            /// </summary>
            public const string WorkWithServices = "370BDACA-1DD8-42A1-80E2-76978621A5DA";

            /// <summary>
            ///     формирование блока мест
            /// </summary>
            public const string ToTourForm = "470424ec-5f34-4f8e-b725-fabb904c2d4f";

            /// <summary>
            ///     утверждение блока мест
            /// </summary>
            public const string TourForm = "C6DABF8F-669F-4457-B8BC-A512727D8071";

            /// <summary>
            ///     утверждение блока мест
            /// </summary>
            public const string TourEdit = "C6DABF8F-669F-4457-B8BC-A512727D8072";

            /// <summary>
            ///     отметка блока мест как оплаченного
            /// </summary>
            public const string TourPay = "4A777824-EB78-4158-BB5A-4D3FAE1381B2";

            /// <summary>
            ///     в формирование из сформирован
            /// </summary>
            public const string ToFormationFromFormed = "18AC76D9-15F0-42E8-87ED-03864337832B";
        }

        /// <summary>
        ///     Оздоровительная организация.
        /// </summary>
        public static class Hotel
        {
            /// <summary>
            ///     Управление
            /// </summary>
            public const string Manage = "963C0146-A0BC-4442-AB14-CF100D670E13";

            /// <summary>
            ///     Просмотр
            /// </summary>
            public const string View = "D239AD76-D919-41F0-9907-7E700A891AC4";

            /// <summary>
            ///     Отправить на утверждение
            /// </summary>
            public static string ForAprove = "B8AFD3CE-F985-4C74-82D2-969DA838C617";

            /// <summary>
            ///     Утвердить
            /// </summary>
            public static string Approved = "CE3E23D0-6069-4BAC-B709-C0D78292BDC3";

            /// <summary>
            ///     Отправить на доработку
            /// </summary>
            public static string ForRework = "C2EA54F5-EAF5-4345-AEA8-EA1374E2E698";

            /// <summary>
            ///     На доработке
            /// </summary>
            public static string OnReworking = "EA086AB5-B60C-45DA-97DA-FE236CFBE08E";

            /// <summary>
            ///     Редактирование наименования
            /// </summary>
            public static string EditName = "7A25797C-C30A-4BBE-ADD9-42881F69B56A";

            /// <summary>
            ///     Просмотр "матрицы цен"
            /// </summary>
            public static string PricesView = "D381B6DD-27B8-4124-858C-FAC1440217A0";

            /// <summary>
            ///     Редактирование "матрицы цен"
            /// </summary>
            public static string PricesEdit = "A9A93FFF-E1E0-4B26-BCD9-82D0E2F5D48E";
        }

        /// <summary>
        ///     Вожатые
        /// </summary>
        public static class Counselor
        {
            /// <summary>
            ///     Отправить на утверждение
            /// </summary>
            public static string ForAprove = "1D0E6E81-F23F-4551-87E8-A79DE6F511DB";

            /// <summary>
            ///     Утвердить
            /// </summary>
            public static string Approved = "982E54D1-DAFE-4AB1-A7BA-194BD34D1D0E";

            /// <summary>
            ///     Отправить на доработку
            /// </summary>
            public static string ForRework = "735697C9-2D26-4691-B884-FC3379307512";

            /// <summary>
            ///     На доработке
            /// </summary>
            public static string OnReworking = "3F9ECBD5-4628-410B-9C15-BD5E1265C84C";

            /// <summary>
            ///     Отказ в вносе заявки вожатого
            /// </summary>
            public static string RequestDecline = "C395C228-0406-42A7-A211-0C89CC507EF6";

            /// <summary>
            ///     В статус заявки
            /// </summary>
            public static string OnRequest = "6478BFC6-429B-408E-8156-7BBD67982ED2";
        }


        /// <summary>
        ///     Договоры
        /// </summary>
        public static class Contract
        {
            /// <summary>
            ///     управление договорами
            /// </summary>
            public const string Manage = "1A470C49-C52E-4266-8DF3-F44B12F52AA4";

            /// <summary>
            ///     просмотр контрактов
            /// </summary>
            public static string View = "9D5C1BDF-7905-4FB1-B4D2-BC28C2FC5157";

            /// <summary>
            ///     просмотр договоров
            /// </summary>
            public static string ViewCommercial = "DA48B1AD-6AAA-4A8B-8529-34F90BBFB62F";

            /// <summary>
            ///     Зарегистрировать
            /// </summary>
            public static string Register = "42D12F4E-DD36-456C-B321-7AA7496A2B19";

            /// <summary>
            ///     Утвердить
            /// </summary>
            public static string Archive = "F0E1B2E9-B319-4F1B-94B1-BE2EF27D5773";

            /// <summary>
            ///     В редактирование
            /// </summary>
            public static string ToEdit = "368FE4B6-B684-4D1E-B266-C716FD442E5D";
        }

        #endregion

        #region Виды (типы) услуг

        /// <summary>
        ///     3 Экскурсия
        /// </summary>
        public static string TypeOfServiceExcursion = "46DB036C-A4C3-4592-B4F6-460FA41AADDD";

        /// <summary>
        ///     4 Виза
        /// </summary>
        public static string TypeOfServiceVisa = "8550D634-13C7-45A0-A168-10557354218D";

        /// <summary>
        ///     5 Транспорт (Авиа)
        /// </summary>
        public static string TypeOfServiceTransferAero = "8E2D5554-6AF1-4C1F-8F30-82B94271B600";

        /// <summary>
        ///     10 Страховка
        /// </summary>
        public static string TypeOfServiceInsurance = "E1D5FB67-DB2D-4360-BA87-2BBD736BE30A";

        /// <summary>
        ///     11 Дополнительное место к льготной путевке
        /// </summary>
        public static string TypeOfServiceAddonPlace = "D1297F42-A25B-4B99-9DDE-941FF068CECE";

        /// <summary>
        ///     12 Транспорт (ЖД)
        /// </summary>
        public static string TypeOfServiceTransferTrain = "433C4762-07CA-4214-A4C7-69D7B09BD784";

        /// <summary>
        ///     13 Транспорт (Авто)
        /// </summary>
        public static string TypeOfServiceTransferAuto = "AD1F57A1-EEF4-46D4-A820-743F5FED1562";

        /// <summary>
        ///     999 Прочее
        /// </summary>
        public static string TypeOfServiceOther = "768D3C20-E038-4A1E-BAAF-E3A20F8D7FDB";

        #endregion

        #region Мониторинг

        /// <summary>
        ///     Мониторинг
        /// </summary>
        public static class Monitoring
        {
            /// <summary>
            ///     Cведения о численности детей
            /// </summary>
            public static class ChildrenNumberInformation
            {
                /// <summary>
                ///     Просмотр формы
                /// </summary>
                public const string View = "6A366D7E-337C-465C-AE0B-AEA40B7AA524";

                /// <summary>
                ///     Получение уведомлений
                /// </summary>
                public const string EventRecive = "F857FE11-66C2-4D59-A948-2D658D6068DD";

                /// <summary>
                ///     Работа с формой
                /// </summary>
                public const string Edit = "F4314C50-F2FF-4913-A941-46A5F6D92144";

                /// <summary>
                ///     Отправить на согласование
                /// </summary>
                public const string OnAgreement = "FB2E86C6-2FE0-47FC-9C31-BE3068D26C90";

                /// <summary>
                ///     Согласовать
                /// </summary>
                public const string Agree = "0B9941E1-3504-407B-A0B3-2DDA8AF5F9EB";

                /// <summary>
                ///     Утвердить
                /// </summary>
                public const string Approve = "C3AE96B4-86D2-4C78-BF3C-B338FE61275A";

                /// <summary>
                ///     Отправить на доработку
                /// </summary>
                public const string ToEdit = "8B7397DC-2578-488B-BDEC-A9DBDF260AA6";
            }

            /// <summary>
            ///     Cведения о финансировании оздоровительной кампании
            /// </summary>
            public static class FinanceInformation
            {
                /// <summary>
                ///     Просмотр формы
                /// </summary>
                public const string View = "12B0B1A5-1E6D-4FFB-8E0E-D79B8A37C16C";

                /// <summary>
                ///     Получение уведомлений
                /// </summary>
                public const string EventRecive = "511E7EDD-7594-4EAB-8A2B-CFE788B05194";

                /// <summary>
                ///     Работа с формой
                /// </summary>
                public const string Edit = "7E1E444E-7BFA-4F2C-9B6C-CEBDAA9B681F";

                /// <summary>
                ///     Отправить на согласование
                /// </summary>
                public const string OnAgreement = "677681EA-EED2-4BDE-B29D-12F42D1B31C0";

                /// <summary>
                ///     Согласовать
                /// </summary>
                public const string Agree = "C65356A8-1A98-4D8F-9285-CFA674E527A0";

                /// <summary>
                ///     Утвердить
                /// </summary>
                public const string Approve = "CC2C6A3B-B0B0-4CB2-9021-05DDA0106F9D";

                /// <summary>
                ///     Отправить на доработку
                /// </summary>
                public const string ToEdit = "820B86B6-7937-4ED0-8886-31E8152E9D2E";
            }

            /// <summary>
            ///     Сведения о малых формах занятости детей
            /// </summary>
            public static class SmallLeisureInfoData
            {
                /// <summary>
                ///     Просмотр формы
                /// </summary>
                public const string View = "EADA9AA1-EFD4-46FA-878D-878A4404E590";

                /// <summary>
                ///     Получение уведомлений
                /// </summary>
                public const string EventRecive = "0FD0832F-D7C6-41C3-A5B6-230CC55F68E6";

                /// <summary>
                ///     Работа с формой
                /// </summary>
                public const string Edit = "F1A9F1F0-5005-4F2C-9652-57C34EF123CD";

                /// <summary>
                ///     Отправить на утверждение
                /// </summary>
                public const string OnApproving = "0F2F2CFB-E527-4A58-86CC-F4C45C033A9F";

                /// <summary>
                ///     Утвердить
                /// </summary>
                public const string Approve = "AB32B974-3662-488E-BC33-9BD893CCAA10";

                /// <summary>
                ///     Отправить на доработку
                /// </summary>
                public const string ToEdit = "AA135222-1871-44DA-BB13-B04A779C12AC";
            }

            /// <summary>
            ///     Работа с реестрами
            /// </summary>
            public const string ReestrWork = "4F0D675B-ADF2-4755-AF1D-83D286DBCD20";

            /// <summary>
            ///     Отправка уведомлений
            /// </summary>
            public const string EventSent = "D482D5BF-469D-4E57-B2D0-42C47614C145";

            /// <summary>
            ///     Выгрузка сводных форм
            /// </summary>
            public const string CompleteFormDownload = "052E290B-C600-4E14-9A80-713A72CA134C";
        }

        #endregion
    }
}
