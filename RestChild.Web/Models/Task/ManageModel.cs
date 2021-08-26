using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestChild.Mobile.Domain;

namespace RestChild.Web.Models.Task
{
    /// <summary>
    ///     класс для работы с заданиями
    /// </summary>
    public class ManageModel : ViewModelBase<BoutTask>
    {
        /// <summary>
        ///     конструктор
        /// </summary>
        public ManageModel() : base(new BoutTask())
        {
            Timesheet = new Timesheet();
        }

        /// <summary>
        ///     конструктор
        /// </summary>
        public ManageModel(BoutTask data) : base(data)
        {
            var file = data.Link?.Files?.FirstOrDefault();
            if (file != null)
            {
                PhotoShowUrl = $"/DownloadImage.ashx/{file.FileUrl}";
            }

            Timesheet = !string.IsNullOrWhiteSpace(data.Timesheet)
                ? JsonConvert.DeserializeObject<Timesheet>(data.Timesheet)
                : new Timesheet
                {
                    Start = data.Bout.DateIncome,
                    End = data.Bout.DateOutcome,
                    EveryDay = 1
                };
        }

        /// <summary>
        ///     активная закладка
        /// </summary>
        public string ActiveTab { get; set; }

        /// <summary>
        ///     Начало
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        ///     продолжительность
        /// </summary>
        public double? Period { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public ViewModelState State { get; set; }

        /// <summary>
        ///     Действие
        /// </summary>
        public string StateMachineActionString { get; set; }

        /// <summary>
        ///     можно редактировать
        /// </summary>
        public bool CanEdit { get; set; }

        /// <summary>
        ///     Ссылка на файл
        /// </summary>
        public string PhotoShowUrl { get; set; }

        /// <summary>
        ///     Ссылка на файл
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        ///     Ссылка на файл
        /// </summary>
        public string PhotoName { get; set; }

        /// <summary>
        ///     задания на лагеря
        /// </summary>
        public Dictionary<string, CampTask> Tasks { get; set; }

        /// <summary>
        ///     расписание
        /// </summary>
        public Timesheet Timesheet { get; set; }

        /// <summary>
        ///     подготовка данных
        /// </summary>
        public override BoutTask BuildData()
        {
            var data = base.BuildData();
            if (!string.IsNullOrWhiteSpace(PhotoUrl))
            {
                data.Link = new Link
                {
                    Files = new List<FileItem>
                    {
                        new FileItem
                        {
                            FileUrl = PhotoUrl,
                            FileName = PhotoUrl.EndsWith("png") ? $"{PhotoName}.png" : $"{PhotoName}.jpg",
                            DateCreate = DateTime.Now
                        }
                    }
                };
            }

            if (Timesheet != null)
            {
                data.StartDate = Timesheet.Start;
                data.FinishDate = Timesheet.End;
                data.Timesheet = JsonConvert.SerializeObject(Timesheet);
            }

            return data;
        }
    }
}
