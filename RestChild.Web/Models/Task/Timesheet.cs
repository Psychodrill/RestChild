using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace RestChild.Web.Models.Task
{
    /// <summary>
    ///     расписание заданий
    /// </summary>
    [Serializable]
    [DataContract]
    public class Timesheet
    {
        /// <summary>
        ///     состояние расписания
        /// </summary>
        [DataMember(Name = "state")]
        public TimesheetState State { get; set; } = TimesheetState.Simple;

        /// <summary>
        ///     время начала
        /// </summary>
        [DataMember(Name = "s")]
        public DateTime Start { get; set; }

        /// <summary>
        ///     время окончания
        /// </summary>
        [DataMember(Name = "e")]
        public DateTime End { get; set; }

        /// <summary>
        ///     время
        /// </summary>
        [DataMember(Name = "t")]
        public string Time { get; set; }

        /// <summary>
        ///     Полный день
        /// </summary>
        [DataMember(Name = "fd")]
        public bool FullDay { get; set; }

        /// <summary>
        ///     продолжительность
        /// </summary>
        [DataMember(Name = "d")]
        public int Duration { get; set; }

        /// <summary>
        ///     Доступно за
        /// </summary>
        [DataMember(Name = "ab")]
        public int AvailableBefore { get; set; }

        /// <summary>
        ///     Доступно после
        /// </summary>
        [DataMember(Name = "aa")]
        public int AvailableAfter { get; set; }

        /// <summary>
        ///     Доступно после
        /// </summary>
        [DataMember(Name = "r")]
        public int Refuse { get; set; }

        /// <summary>
        ///     повторять каждый N день
        /// </summary>
        [DataMember(Name = "ed", EmitDefaultValue = false)]
        public int EveryDay { get; set; }

        /// <summary>
        ///     каждый рабочий день
        /// </summary>
        [DataMember(Name = "ew", EmitDefaultValue = false)]
        public bool EveryWorkDay { get; set; }

        /// <summary>
        ///     каждый понедельник
        /// </summary>
        [DataMember(Name = "e1", EmitDefaultValue = false)]
        public bool Every1 { get; set; }

        /// <summary>
        ///     каждый вторник
        /// </summary>
        [DataMember(Name = "e2", EmitDefaultValue = false)]
        public bool Every2 { get; set; }

        /// <summary>
        ///     каждую среду
        /// </summary>
        [DataMember(Name = "e3", EmitDefaultValue = false)]
        public bool Every3 { get; set; }

        /// <summary>
        ///     каждый четверг
        /// </summary>
        [DataMember(Name = "e4", EmitDefaultValue = false)]
        public bool Every4 { get; set; }

        /// <summary>
        ///     каждую пятницу
        /// </summary>
        [DataMember(Name = "e5", EmitDefaultValue = false)]
        public bool Every5 { get; set; }

        /// <summary>
        ///     каждая суббота
        /// </summary>
        [DataMember(Name = "e6", EmitDefaultValue = false)]
        public bool Every6 { get; set; }

        /// <summary>
        ///     каждое воскресенье
        /// </summary>
        [DataMember(Name = "e7", EmitDefaultValue = false)]
        public bool Every7 { get; set; }

        /// <summary>
        ///     описание расписания
        /// </summary>
        public override string ToString()
        {
            var res = new StringBuilder();
            switch (State)
            {
                case TimesheetState.Simple:
                    res.Append("однократно");
                    break;
                case TimesheetState.EveryDay:
                    res.Append(EveryDay > 1 ? $"в каждый {EveryDay}-й день" : "в ежедневно");
                    break;
                case TimesheetState.EveryWorkDay:
                    res.Append("в каждый рабочий день");
                    break;
                case TimesheetState.EveryWeek:
                    res.Append("в еженедельно");

                    var days = new List<string>();

                    if (Every1)
                    {
                        days.Add("понедельник");
                    }

                    if (Every1)
                    {
                        days.Add("вторник");
                    }

                    if (Every1)
                    {
                        days.Add("среда");
                    }

                    if (Every1)
                    {
                        days.Add("четверг");
                    }

                    if (Every1)
                    {
                        days.Add("пятница");
                    }

                    if (Every1)
                    {
                        days.Add("суббота");
                    }

                    if (Every1)
                    {
                        days.Add("воскресенье");
                    }

                    if (days.Any())
                    {
                        res.Append($",{string.Join(", ", days)}");
                    }

                    break;
            }

            res.Append(State == TimesheetState.Simple
                ? $", в {Start:dd.MM.yyyy}"
                : $", с {Start:dd.MM.yyyy} по {End: dd.MM.yyyy}");

            if (!string.IsNullOrEmpty(Time))
            {
                res.Append($" {Time}");
            }

            if (Duration > 0)
            {
                res.Append(", продолжительность");
                FormatTime(res, Duration);
            }

            if (AvailableBefore == 0)
            {
                res.Append(", доступно в момент начала");
            }
            else
            {
                res.Append(", доступно за");
                FormatTime(res, AvailableBefore);
            }

            if (AvailableAfter == 0)
            {
                res.Append("и до начала");
            }
            else
            {
                res.Append(" и после начала на ");
                FormatTime(res, AvailableAfter);
            }


            if (Refuse == 0)
            {
                res.Append(", отказаться нельзя");
            }
            else
            {
                res.Append(", отказаться можно в течении ");
                FormatTime(res, Refuse);
            }

            return res.ToString();
        }

        /// <summary>
        ///     форматирование времени
        /// </summary>
        public string DurationText
        {
            get
            {
                var sb = new StringBuilder();
                FormatTime(sb, Duration);
                return sb.ToString();
            }
        }

        /// <summary>
        ///     форматирование времени
        /// </summary>
        public static string FormatTime(int minutes)
        {
            var sb = new StringBuilder();
            FormatTime(sb, minutes);

            return sb.ToString();
        }

        /// <summary>
        ///     форматирование времени
        /// </summary>
        private static void FormatTime(StringBuilder res, int minutes)
        {
            var time = new TimeSpan(0, minutes, 0);
            if (time.Days > 0)
            {
                res.Append(time.Days == 1 ? " один д." : $" {time.Days} д.");
            }

            if (time.Hours > 0)
            {
                res.Append($" {time.Hours} ч.");
            }

            if (time.Minutes > 0)
            {
                res.Append($" {time.Minutes} мин.");
            }
        }
    }

    /// <summary>
    ///     состояние расписания
    /// </summary>
    public enum TimesheetState
    {
        Simple = 0,

        EveryDay = 1,

        EveryWorkDay = 2,

        EveryWeek = 3
    }
}
