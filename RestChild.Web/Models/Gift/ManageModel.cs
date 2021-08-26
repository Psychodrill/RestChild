using System;
using System.Collections.Generic;
using System.Linq;
using RestChild.Mobile.Domain;

namespace RestChild.Web.Models.Gift
{
    /// <summary>
    ///     модель для подарка
    /// </summary>
    public class ManageModel : ViewModelBase<Mobile.Domain.Gift>
    {
        /// <summary>
        ///     конструктор
        /// </summary>
        public ManageModel() : base(new Mobile.Domain.Gift())
        {
            Params = new Dictionary<string, GiftParameter>();
            Counts = new Dictionary<long, long>();
        }

        /// <summary>
        ///     конструктор
        /// </summary>
        public ManageModel(Mobile.Domain.Gift data) : base(data)
        {
            Params = data.GiftParameters?.ToDictionary(d => d.Id.ToString()) ?? new Dictionary<string, GiftParameter>();
            var file = data.Link?.Files?.FirstOrDefault();
            if (file != null)
            {
                PhotoShowUrl = $"/DownloadImage.ashx/{file.FileUrl}";
            }

            Counts = new Dictionary<long, long>();
        }


        /// <summary>
        ///     количество остатков
        /// </summary>
        public Dictionary<long, long> Counts { get; set; }

        /// <summary>
        ///     активная закладка
        /// </summary>
        public string ActiveTab { get; set; }

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
        ///     параметры подарка
        /// </summary>
        public Dictionary<string, GiftParameter> Params { get; set; }

        /// <summary>
        ///     подготовка данных
        /// </summary>
        public override Mobile.Domain.Gift BuildData()
        {
            var data = base.BuildData();
            data.GiftParameters = Params?.Values.ToList() ?? new List<GiftParameter>();
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

            return data;
        }
    }
}
