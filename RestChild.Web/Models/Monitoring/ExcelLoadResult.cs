using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using RestChild.Comon;
using RestChild.Domain;

namespace RestChild.Web.Models.Monitoring
{
    /// <summary>
    ///     результат загрузки из Excel
    /// </summary>
    [DataContract]
    public class ExcelLoadResult
    {
        /// <summary>
        ///     конструктор
        /// </summary>
        public ExcelLoadResult()
        {
            Data = new Dictionary<long, IList<MonitoringSmallLeisureInfoData>>();
            Gbu = new List<string>();
        }

        /// <summary>
        ///     имя файла
        /// </summary>
        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        /// <summary>
        ///     текст результата
        /// </summary>
        [DataMember(Name = "resultLoad")]
        public string ResultLoad { get; set; }

        /// <summary>
        ///     успешно ли загружен
        /// </summary>
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        /// <summary>
        ///     список ГБУ
        /// </summary>
        [DataMember(Name = "gbu")]
        public IList<string> Gbu { get; set; }

        /// <summary>
        ///     данные
        /// </summary>
        [IgnoreDataMember]
        public Dictionary<long, IList<MonitoringSmallLeisureInfoData>> Data { get; set; }

        /// <summary>
        ///     добавление данных
        /// </summary>
        public void AppendData(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key) || value == null)
            {
                return;
            }

            var keys = key.Split('_');
            //$"MoneyOutcome_{type.Id}_{subtype.Id}_{gbu.Id}_{gbu.GBUId}"
            //    0             1           2           3        4

            if (keys.Length != 5)
            {
                return;
            }

            var gbuId = keys[4].LongParse();

            if (!gbuId.HasValue)
            {
                return;
            }

            if (!Data.ContainsKey(gbuId.Value))
            {
                var data = new List<MonitoringSmallLeisureInfoData>();
                Data.Add(gbuId.Value, data);
            }

            var item = Data[gbuId.Value];

            var typeId = keys[1].LongParse();
            var subTypeId = keys[2].LongParse();

            var valueToSave =
                item.FirstOrDefault(v => v.SmallLeisureTypeId == typeId && v.SmallLeisureSubtypeId == subTypeId);

            if (valueToSave == null)
            {
                valueToSave = new MonitoringSmallLeisureInfoData
                {
                    SmallLeisureTypeId = typeId,
                    SmallLeisureSubtypeId = subTypeId
                };

                item.Add(valueToSave);
            }

            if (keys[0] == "MoneyOutcome")
            {
                valueToSave.MoneyOutcome = value.ToString().Replace(",", ".").DecimalParse();
            }

            if (keys[0] == "ChildernCountCovered")
            {
                valueToSave.ChildernCountCovered = value.ToString().IntParse();
            }

            if (keys[0] == "ChildrenCountPost")
            {
                valueToSave.ChildrenCountPost = value.ToString().IntParse();
            }

            if (keys[0] == "NoteOne")
            {
                valueToSave.NoteOne = value.ToString();
            }

            if (keys[0] == "NoteThree")
            {
                valueToSave.NoteThree = value.ToString();
            }

            if (keys[0] == "NoteTwo")
            {
                valueToSave.NoteTwo = value.ToString();
            }
        }

        /// <summary>
        ///     удалить пустые данные
        /// </summary>
        public void ClearEmpty()
        {
            foreach (var key in Data.Keys.ToList())
            {
                var data = Data[key];

                if (data.All(d =>
                    string.IsNullOrWhiteSpace(d.NoteOne) &&
                    string.IsNullOrWhiteSpace(d.NoteTwo) &&
                    string.IsNullOrWhiteSpace(d.NoteThree) &&
                    !d.MoneyOutcome.HasValue &&
                    !d.ChildrenCountPost.HasValue &&
                    !d.ChildernCountCovered.HasValue
                ))
                {
                    Data.Remove(key);
                }
            }
        }
    }
}
