namespace RestChild.Comon.Enumeration
{
    public static class ReportEnum
    {
        public enum ReportSheet
        {
            ServiceStatistics = 2
        }

        public static class ReportTable
        {
            public static class ServiceStatistics
            {
                public static readonly long ApplicantAwaitReport = 2;
                public static readonly long InteragencyAwait = 3;
                public static readonly long MainStatistics = 4;
                public static readonly long BookingsWithoutRequests = 5;
            }
        }

        public static class Heads
        {
            public static class ServiceStatistics
            {
                public static class ApplicantAwaitReport
                {
                    public static readonly long Column1 = 3;
                    public static readonly long Column2 = 4;
                    public static readonly long Column3 = 5;
                    public static readonly long Column4 = 6;
                    public static readonly long Column5 = 7;
                }

                public static class InteragencyAwait
                {
                    public static readonly long Column1 = 8;
                }

                public static class MainStatistics
                {
                    public static readonly long ColumnName = 9;
                    public static readonly long ByHour = 10;
                    public static readonly long ByDay = 11;
                    public static readonly long ByWeek = 12;
                    public static readonly long All = 13;
                }
            }
        }

        public static class Rows
        {
            public static class ServiceStatistics
            {
                public static class ApplicantAwaitReport
                {
                    public static class Row1
                    {
                        public static readonly long Id = 3;

                        public static readonly long Column1 = 3;
                        public static readonly long Column2 = 4;
                        public static readonly long Column3 = 5;
                        public static readonly long Column4 = 6;
                        public static readonly long Column5 = 7;
                    }
                }

                public static class InteragencyAwait
                {
                    public static class Row1
                    {
                        public static readonly long Id = 4;

                        public static readonly long Column1 = 8;
                    }
                }


                public static class MainStatistics
                {
                    /// <summary>
                    ///     Поступило заявлений/отдыхающих
                    /// </summary>
                    public static class Requests
                    {
                        public static readonly long Id = 5;

                        public static readonly long ColumnName = 9;
                        public static readonly long ByHour = 10;
                        public static readonly long ByDay = 11;
                        public static readonly long ByWeek = 12;
                        public static readonly long All = 13;
                    }

                    /// <summary>
                    ///     Выдано сертификатов
                    /// </summary>
                    public static class Sertificates
                    {
                        public static readonly long Id = 6;

                        public static readonly long ColumnName = 14;
                        public static readonly long ByHour = 15;
                        public static readonly long ByDay = 16;
                        public static readonly long ByWeek = 17;
                        public static readonly long All = 18;
                    }

                    /// <summary>
                    ///     Выдано сертификатов
                    /// </summary>
                    public static class SertificatesIssued
                    {
                        public static readonly long Id = 7;

                        public static readonly long ColumnName = 50;
                        public static readonly long ByHour = 51;
                        public static readonly long ByDay = 52;
                        public static readonly long ByWeek = 53;
                        public static readonly long All = 54;
                    }


                    /// <summary>
                    ///     Ожидание прихода заявителя
                    /// </summary>
                    public static class ApplicantAwait
                    {
                        public static readonly long Id = 8;

                        public static readonly long ColumnName = 19;
                        public static readonly long ByHour = 20;
                        public static readonly long ByDay = 21;
                        public static readonly long ByWeek = 22;
                        public static readonly long All = 23;
                    }

                    /// <summary>
                    ///     Отказ в предоставлении услуги
                    /// </summary>
                    public static class ServiceDenied
                    {
                        public static readonly long Id = 9;

                        public static readonly long ColumnName = 24;
                        public static readonly long ByHour = 25;
                        public static readonly long ByDay = 26;
                        public static readonly long ByWeek = 27;
                        public static readonly long All = 28;
                    }

                    /// <summary>
                    ///     Отказ в регистрации заявления
                    /// </summary>
                    public static class ServiceRegisterDenied
                    {
                        public static readonly long Id = 10;

                        public static readonly long ColumnName = 45;
                        public static readonly long ByHour = 46;
                        public static readonly long ByDay = 47;
                        public static readonly long ByWeek = 48;
                        public static readonly long All = 49;
                    }

                    /// <summary>
                    ///     Ожидает ответа из Базового регистра
                    /// </summary>
                    public static class BaseRegisterAwait
                    {
                        public static readonly long Id = 11;

                        public static readonly long ColumnName = 29;
                        public static readonly long ByHour = 30;
                        public static readonly long ByDay = 31;
                        public static readonly long ByWeek = 32;
                        public static readonly long All = 33;
                    }

                    /// <summary>
                    ///     Получены ответы в Базовом регистре
                    /// </summary>
                    public static class BaseRegisterResponsed
                    {
                        public static readonly long Id = 12;

                        public static readonly long ColumnName = 34;
                        public static readonly long ByHour = 35;
                        public static readonly long ByDay = 36;
                        public static readonly long ByWeek = 37;
                        public static readonly long All = 38;
                    }

                    /// <summary>
                    ///     Количество некорректных сообщений от МПГУ
                    /// </summary>
                    public static class MpguErrorMessages
                    {
                        public static readonly long Id = 13;

                        public static readonly long ColumnName = 39;
                        public static readonly long ByHour = 40;
                        public static readonly long ByDay = 41;
                        public static readonly long ByWeek = 42;
                        public static readonly long All = 43;
                    }
                }

                /// <summary>
                ///     Количество бронирований по которым нет заявления
                /// </summary>
                public static class BookingsWithoutRequests
                {
                    public static class Row1
                    {
                        public static readonly long Id = 14;

                        public static readonly long Column1 = 44;
                    }
                }
            }
        }
    }
}
