namespace RestChild.Mobile.DAL.Enum
{
    /// <summary>
    ///     статус
    /// </summary>
    public static class StateEnum
    {
        /// <summary>
        ///     на проверке
        /// </summary>
        public const long Deleted = -1;

        /// <summary>
        ///     статус задания отдыхающего
        /// </summary>
        public static class CamperTask
        {
            /// <summary>
            ///     на проверке
            /// </summary>
            public const long OnApproving = 1;

            /// <summary>
            ///     на выполнении
            /// </summary>
            public const long OnExecution = 2;

            /// <summary>
            ///     отменен
            /// </summary>
            public const long Canceled = 3;

            /// <summary>
            ///     выполнен
            /// </summary>
            public const long Done = 4;

            /// <summary>
            ///     не выполнен
            /// </summary>
            public const long NotDone = 5;

            /// <summary>
            ///     Не назначено
            /// </summary>
            public const long UnAssign = 6;
        }

        /// <summary>
        ///     статус задания отдыхающего
        /// </summary>
        public static class Notification
        {
            /// <summary>
            ///     на проверке
            /// </summary>
            public const long OnApproving = 101;

            /// <summary>
            ///     выполнен
            /// </summary>
            public const long Done = 102;

            /// <summary>
            ///     не выполнен
            /// </summary>
            public const long NotDone = 103;

            /// <summary>
            ///     Есть вопрос
            /// </summary>
            public const long HaveQuestion = 104;

            /// <summary>
            ///     Отвечено
            /// </summary>
            public const long Answered = 105;
        }

        /// <summary>
        ///     заезд
        /// </summary>
        public static class Bout
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 239;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 240;

            /// <summary>
            ///     Подтвержен
            /// </summary>
            public const long Confirmed = 249;

            /// <summary>
            ///     Закрыт
            /// </summary>
            public const long Closed = 259;
        }

        /// <summary>
        ///     отряды
        /// </summary>
        public static class Party
        {
            /// <summary>
            ///     Формирование
            /// </summary>
            public const long Forming = 241;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 242;
        }


        /// <summary>
        ///     Объект отдыха
        /// </summary>
        public static class Camp
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 226;

            /// <summary>
            ///     Отправлен на утверждение
            /// </summary>
            public const long ForApprove = 227;

            /// <summary>
            ///     Утвержден
            /// </summary>
            public const long Approved = 228;

            /// <summary>
            ///     На доработку
            /// </summary>
            public const long ForRework = 229;

            /// <summary>
            ///     На доработке
            /// </summary>
            public const long OnReworking = 230;
        }

        /// <summary>
        ///     Персонал
        /// </summary>
        public static class Personal
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 12031;

            /// <summary>
            ///     Отправлен на утверждение
            /// </summary>
            public const long ForApprove = 12032;

            /// <summary>
            ///     Прошел базовое обучение
            /// </summary>
            public const long Approved = 12033;

            /// <summary>
            ///     На доработку
            /// </summary>
            public const long ForRework = 12034;

            /// <summary>
            ///     На доработке
            /// </summary>
            public const long OnReworking = 12035;

            /// <summary>
            ///     заявка на внос в реестр.
            /// </summary>
            public const long RegistryRequest = 12054;

            /// <summary>
            ///     заявка на внос в реестр (отказано).
            /// </summary>
            public const long RequestDecline = 12055;

            /// <summary>
            ///     Приглашен на собеседование.
            /// </summary>
            public const long InvitedForInterview = 12001;

            /// <summary>
            ///     Прошел собеседование.
            /// </summary>
            public const long PassedInterview = 12002;

            /// <summary>
            ///     Ожидает оплаты.
            /// </summary>
            public const long PendingPayment = 12003;

            /// <summary>
            ///     Проходит обучение.
            /// </summary>
            public const long UndergoingTraining = 12004;

            /// <summary>
            ///     Архив.
            /// </summary>
            public const long Arhive = 12005;

            /// <summary>
            ///     Оплачено.
            /// </summary>
            public const long Payed = 12006;

            /// <summary>
            ///     Отчислен.
            /// </summary>
            public const long Expelled = 12007;
        }

        /// <summary>
        ///     администратор
        /// </summary>
        public static class AdministratorTour
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 20065;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 20066;

            /// <summary>
            ///     Проходит обучение
            /// </summary>
            public const long UndergoingTraining = 20000;

            /// <summary>
            ///     Прошел базовое обучение
            /// </summary>
            public const long CompleteTraining = 20001;

            /// <summary>
            ///     Архив
            /// </summary>
            public const long Archive = 20002;
        }

        /// <summary>
        ///     статус подарка
        /// </summary>
        public static class Gift
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 21001;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 21002;

            /// <summary>
            ///     Архив
            /// </summary>
            public const long Archive = 21000;
        }

        /// <summary>
        ///     статус резервирования подарка
        /// </summary>
        public static class GiftReserved
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Reserved = 22001;

            /// <summary>
            ///     Отклонено
            /// </summary>
            public const long Refusal = 22002;

            /// <summary>
            ///     Выдан
            /// </summary>
            public const long Issued = 22003;

            /// <summary>
            ///     Отмена
            /// </summary>
            public const long Canceled = 22004;
        }

        /// <summary>
        ///     статус задания
        /// </summary>
        public static class Task
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 23001;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 23002;

            /// <summary>
            ///     Архив
            /// </summary>
            public const long Archive = 23000;
        }

        /// <summary>
        ///     статус задания для объекта
        /// </summary>
        public static class CampTask
        {
            /// <summary>
            ///     Связано
            /// </summary>
            public const long Linked = 24000;

            /// <summary>
            ///     Не связано
            /// </summary>
            public const long Unlinked = 24001;
        }

        /// <summary>
        ///     статус задания
        /// </summary>
        public static class BoutTask
        {
            /// <summary>
            ///     Редактирование
            /// </summary>
            public const long Editing = 25001;

            /// <summary>
            ///     Сформирован
            /// </summary>
            public const long Formed = 25002;

            /// <summary>
            ///     Архив
            /// </summary>
            public const long Archive = 25000;
        }
    }
}
