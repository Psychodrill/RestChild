namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     статусы машины состояний
    /// </summary>
    public static class StateMachineStateEnum
    {
        /// <summary>
        ///     удалена
        /// </summary>
        public const long Deleted = -1;

        #region квоты по одаренным детям

        public static class Limit
        {
            #region статусы для списка организации

            public static class List
            {
                /// <summary>
                ///     Формирование
                /// </summary>
                public const long Formation = 1;

                /// <summary>
                ///     Сформирован
                /// </summary>
                public const long Formed = 2;

                /// <summary>
                ///     Включен в заезд
                /// </summary>
                public const long IncludedInTour = 3;

                /// <summary>
                ///     Внесены сведения об оплате
                /// </summary>
                public const long IncludedPayment = 4;
            }

            #endregion

            #region квота по организации

            public static class Organization
            {
                /// <summary>
                ///     Формирование
                /// </summary>
                public const long Formation = 5;

                /// <summary>
                ///     Доведены до организации
                /// </summary>
                public const long Brought = 6;

                /// <summary>
                ///     Доведены до организации (для доработки, не статус)
                /// </summary>
                public const long BroughtToCompetiotion = 7;

                /// <summary>
                ///     Отправлена на утверждение в ОИВ
                /// </summary>
                public const long ToApprove = 8;

                /// <summary>
                ///     Утверждено
                /// </summary>
                public const long Approved = 9;

                /// <summary>
                ///     На доработке
                /// </summary>
                public const long OnCompletion = 10;

                /// <summary>
                ///     Подтверждено ДКгМ
                /// </summary>
                public const long Confirmed = 11;

                /// <summary>
                ///     Из утверждено в на доработку(не статус)
                /// </summary>
                public const long ApprovedToCompetiotion = 12;

                /// <summary>
                ///     Из "Подтверждено ДКгМ" в "На утверждении ДКгМ"(не статус)
                /// </summary>
                public const long ConfirmedToApproved = 17;
            }

            #endregion

            #region квота по ОИВ

            public static class Oiv
            {
                /// <summary>
                ///     Формирование
                /// </summary>
                public const long Formation = 13;

                /// <summary>
                ///     Доведены до Oiv
                /// </summary>
                public const long Brought = 14;

                /// <summary>
                ///     Доведены до Организаций
                /// </summary>
                public const long BroughtToOrganization = 15;

                /// <summary>
                ///     На доработку (не статус)
                /// </summary>
                public const long OnCompletion = 16;

                /// <summary>
                ///     Сбор потребностей
                /// </summary>
                public const long GatheringRequirements = 18;
            }

            #endregion
        }

        #endregion


        public static class Tour
        {
            /// <summary>
            ///     Формирование
            /// </summary>
            public const long Formation = 23;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long ToFormed = 22;

            /// <summary>
            ///     Утвержден
            /// </summary>
            public const long Formed = 24;

            /// <summary>
            ///     Оплачен
            /// </summary>
            public const long ToFormationFromFormed = 25;
        }

        public static class Hotel
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 26;

            /// <summary>
            ///     Отправлен на утверждение
            /// </summary>
            public const long ForApprove = 27;

            /// <summary>
            ///     Утвержден
            /// </summary>
            public const long Approved = 28;

            /// <summary>
            ///     На доработку
            /// </summary>
            public const long ForRework = 29;

            /// <summary>
            ///     На доработке
            /// </summary>
            public const long OnReworking = 30;
        }

        public static class Counselor
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 31;

            /// <summary>
            ///     Отправлен на утверждение
            /// </summary>
            public const long ForApprove = 32;

            /// <summary>
            ///     Утвержден
            /// </summary>
            public const long Approved = 33;

            /// <summary>
            ///     На доработку
            /// </summary>
            public const long ForRework = 34;

            /// <summary>
            ///     На доработке
            /// </summary>
            public const long OnReworking = 35;

            /// <summary>
            ///     заявка на внос в реестр.
            /// </summary>
            public const long Request = 54;

            /// <summary>
            ///     заявка на внос в реестр (отказано).
            /// </summary>
            public const long RequestDecline = 55;
        }

        public static class Contract
        {
            /// <summary>
            ///     Новый
            /// </summary>
            public const long New = 36;

            /// <summary>
            ///     Действующий
            /// </summary>
            public const long Active = 37;

            /// <summary>
            ///     Архивный
            /// </summary>
            public const long Archive = 38;
        }

        public static class Bout
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 39;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 40;

            /// <summary>
            ///     Подтвержен
            /// </summary>
            public const long Confirmed = 49;

            /// <summary>
            ///     Закрыт
            /// </summary>
            public const long Closed = 59;

            /// <summary>
            ///     Не статус
            /// </summary>
            public const long FromClosedToConfirmed = 91;

            /// <summary>
            ///     Не статус
            /// </summary>
            public const long FromConfirmedToFormed = 92;
        }

        public static class Party
        {
            /// <summary>
            ///     Формирование
            /// </summary>
            public const long Forming = 41;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 42;
        }


        /// <summary>
        ///     Рейсы
        /// </summary>
        public static class DirectoryFlights
        {
            /// <summary>
            ///     Формирование
            /// </summary>
            public const long Forming = 43;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 44;
        }

        /// <summary>
        ///     Транспорт
        /// </summary>
        public static class Transport
        {
            /// <summary>
            ///     Формирование
            /// </summary>
            public const long Forming = 45;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 46;
        }

        /// <summary>
        ///     доп услуги.
        /// </summary>
        public static class AddonService
        {
            /// <summary>
            ///     Формирование
            /// </summary>
            public const long Forming = 57;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 58;

            /// <summary>
            ///     Архивный статус.
            /// </summary>
            public const long Archive = 56;
        }

        /// <summary>
        ///     Начисление
        /// </summary>
        public static class Calculation
        {
            /// <summary>
            ///     Не оплачено
            /// </summary>
            public const long Unpaid = 47;

            /// <summary>
            ///     Оплачено
            /// </summary>
            public const long Paid = 48;

            /// <summary>
            ///     Отменено
            /// </summary>
            public const long Cancelled = 20;
        }

        /// <summary>
        ///     статусы связей услуг.
        /// </summary>
        public static class AddonServiceLink
        {
            /// <summary>
            ///     Черновик
            /// </summary>
            public const long Draft = 50;

            /// <summary>
            ///     Выставлено
            /// </summary>
            public const long Offered = 51;

            /// <summary>
            ///     Сформировано
            /// </summary>
            public const long Formed = 52;

            /// <summary>
            ///     Аннулировано
            /// </summary>
            public const long Canceled = 53;
        }

        /// <summary>
        ///     статусы задачь
        /// </summary>
        public static class CounselorTask
        {
            /// <summary>
            ///     задача поставлена
            /// </summary>
            public const long Delivered = 60;

            /// <summary>
            ///     задача Решена
            /// </summary>
            public const long Solved = 61;

            /// <summary>
            ///     на согласовании
            /// </summary>
            public const long Approved = 62;

            /// <summary>
            ///     на доработку
            /// </summary>
            public const long Completion = 63;

            /// <summary>
            ///     Не прочитано
            /// </summary>
            public const long Sended = 64;

            /// <summary>
            ///     Прочитано
            /// </summary>
            public const long Readed = 67;
        }

        public static class AdministratorTour
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 65;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 66;
        }

        /// <summary>
        ///     статусы задачь
        /// </summary>
        public static class CounselorTest
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 68;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 69;
        }

        /// <summary>
        ///     статусы задачь
        /// </summary>
        public static class TrainingCounselors
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 70;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 71;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long EducationFinished = 72;
        }

        /// <summary>
        ///     статусы педотрядов для вожатых
        /// </summary>
        public static class PedParty
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 73;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 74;
        }

        /// <summary>
        ///     Профильники. Заявки на списки
        /// </summary>
        public static class LimitRequest
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 75;

            /// <summary>
            ///     На утверждении
            /// </summary>
            public const long OnAprove = 76;

            /// <summary>
            ///     Утверждена
            /// </summary>
            public const long Approved = 77;

            /// <summary>
            ///     Отклонена
            /// </summary>
            public const long Declined = 78;
        }

        /// <summary>
        ///     платежи
        /// </summary>
        public static class Payment
        {
            /// <summary>
            ///     не привязаный
            /// </summary>
            public const long Unlinked = 79;

            /// <summary>
            ///     привязанный
            /// </summary>
            public const long Linked = 80;

            /// <summary>
            ///     Отмененный платеж
            /// </summary>
            public const long Anuled = 87;
        }

        /// <summary>
        ///     комерческие заявки
        /// </summary>
        public static class Request
        {
            /// <summary>
            ///     черновик
            /// </summary>
            public const long Draft = (long) StatusEnum.Draft;

            /// <summary>
            ///     В работе
            /// </summary>
            public const long OnWorking = (long) StatusEnum.OnWorking;

            /// <summary>
            ///     Готово к оплате
            /// </summary>
            public const long ReadyToPay = (long) StatusEnum.ReadyToPay;

            /// <summary>
            ///     Заявка оплачена
            /// </summary>
            public const long CertificateIssued = (long) StatusEnum.CertificateIssued;

            /// <summary>
            ///     Аннулировано
            /// </summary>
            public const long Reject = (long) StatusEnum.Reject;

            /// <summary>
            ///     Запрошено
            /// </summary>
            public const long OnApprove = (long) StatusEnum.OnApprove;

            /// <summary>
            ///     Готово к доплате
            /// </summary>
            public const long ReadyToPayFull = (long) StatusEnum.ReadyToPayFull;

            /// <summary>
            ///     Отказ
            /// </summary>
            public const long Denial = (long) StatusEnum.Denial;

            /// <summary>
            ///     Оплачен аванс
            /// </summary>
            public const long Payed = (long) StatusEnum.Payed;
        }

        /// <summary>
        ///     списки профсоюзов
        /// </summary>
        public static class TradeUnion
        {
            /// <summary>
            ///     редактирование (не статус)
            /// </summary>
            public const long EditForAll = 81;

            /// <summary>
            ///     редактирование
            /// </summary>
            public const long Edit = 82;

            /// <summary>
            ///     сведения о заехавших внесены
            /// </summary>
            public const long Finish = 83;

            /// <summary>
            ///     на утверждение
            /// </summary>
            public const long OnAproving = 84;

            /// <summary>
            ///     отклонено
            /// </summary>
            public const long Declined = 85;

            /// <summary>
            ///     утверждено
            /// </summary>
            public const long Approved = 86;
        }

        /// <summary>
        ///     Группы (потребности) приютов
        /// </summary>
        public static class PupilGroup
        {
            /// <summary>
            ///     Редактирование (не статус)
            /// </summary>
            public const long EditForAll = 120;

            /// <summary>
            ///     Формирование
            /// </summary>
            public const long Formation = 121;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 122;

            /// <summary>
            ///     Согласование ДТСЗН
            /// </summary>
            public const long OnAgreement = 123;

            /// <summary>
            ///     Согласована
            /// </summary>
            public const long Agreed = 124;

            /// <summary>
            ///     Утверждена
            /// </summary>
            public const long Approved = 125;

            /// <summary>
            ///     Удалена
            /// </summary>
            public const long Deleted = 126;

            /// <summary>
            ///     Редактирование (из утверждённого) 
            /// </summary>
            public const long EditForMGT = 154;
        }

        /// <summary>
        ///     Список (группа отправки) приюта
        /// </summary>
        public static class PupilGroupList
        {
            /// <summary>
            ///     Редактирование (не статус)
            /// </summary>
            public const long EditForAll = 127;

            /// <summary>
            ///     Формирование
            /// </summary>
            public const long Formation = 128;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 129;

            /// <summary>
            ///     Утверждена
            /// </summary>
            public const long Approved = 130;

            /// <summary>
            ///     Удалена
            /// </summary>
            public const long Deleted = 131;
        }

        /// <summary>
        ///     Сертификаты
        /// </summary>
        public static class Certificate
        {
            /// <summary>
            ///     Редактирование (не статус)
            /// </summary>
            public const long EditForAll = 2701;

            /// <summary>
            ///     Использован
            /// </summary>
            public const long PaidOff = 2702;

            /// <summary>
            ///     Оплачен
            /// </summary>
            public const long Paid = 2703;

            /// <summary>
            ///     Сертификат. Отчетность сдана
            /// </summary>
            public const long ReportingSubmitted = 2704;

            /// <summary>
            ///     Сертификат. Отчетность принята
            /// </summary>
            public const long ReportingAccepted = 2705;
        }

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
                ///     Редактирование (не статус)
                /// </summary>
                public const long EditForAll = 140;

                /// <summary>
                ///     Формирование
                /// </summary>
                public const long Formation = 141;

                /// <summary>
                ///     Согласование
                /// </summary>
                public const long OnAgreement = 142;

                /// <summary>
                ///     Согласована
                /// </summary>
                public const long Agreed = 143;

                /// <summary>
                ///     Утверждена
                /// </summary>
                public const long Approved = 144;
            }

            /// <summary>
            ///     Сведения о финансировании оздоровительной кампании
            /// </summary>
            public static class FinanceInformation
            {
                /// <summary>
                ///     Редактирование (не статус)
                /// </summary>
                public const long EditForAll = 145;

                /// <summary>
                ///     Формирование
                /// </summary>
                public const long Formation = 146;

                /// <summary>
                ///     Согласование
                /// </summary>
                public const long OnAgreement = 147;

                /// <summary>
                ///     Согласована
                /// </summary>
                public const long Agreed = 148;

                /// <summary>
                ///     Утверждена
                /// </summary>
                public const long Approved = 149;
            }

            /// <summary>
            ///     Сведения о финансировании оздоровительной кампании
            /// </summary>
            public static class SmallLeisureInfoData
            {
                /// <summary>
                ///     Редактирование (не статус)
                /// </summary>
                public const long EditForAll = 150;

                /// <summary>
                ///     Формирование
                /// </summary>
                public const long Formation = 151;

                /// <summary>
                ///     Утверждение
                /// </summary>
                public const long OnApproving = 152;

                /// <summary>
                ///     Утверждена
                /// </summary>
                public const long Approved = 153;
            }

            // последний индекс 154
        }
    }
}
