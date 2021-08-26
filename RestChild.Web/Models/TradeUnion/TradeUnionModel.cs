using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestChild.Domain;
using RestChild.Web.Properties;

namespace RestChild.Web.Models.TradeUnion
{
    /// <summary>
    ///    модель списка.
    /// </summary>
    public class TradeUnionModel : ViewModelBase<TradeUnionList>
    {
        public TradeUnionModel() : base(new TradeUnionList())
        {
            Campers = new Dictionary<string, TradeUnionCamperModel>();
            CampersJson = new Dictionary<string, string>();
        }

        public TradeUnionModel(TradeUnionList data) : base(data)
        {
            var items = data?.Campers?.OrderBy(c => c.Child?.LastName)
                           .ThenBy(c => c.Child?.FirstName)
                           .Select(c => new TradeUnionCamperModel(c)).ToList() ??
                        new List<TradeUnionCamperModel>();
            Campers =
               items
                  .ToDictionary(c => c.Id.ToString(), c => c);
            CampersJson = items.ToDictionary(c => c.Id.ToString(), JsonConvert.SerializeObject);
        }

        public Dictionary<string, string> CampersJson { get; set; }
        public Dictionary<string, TradeUnionCamperModel> Campers { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public ViewModelState State { get; set; }

        /// <summary>
        ///     Действие
        /// </summary>
        public string StateMachineActionString { get; set; }

        /// <summary>
        ///     Активная вкладка
        /// </summary>
        public string ActiveTab { get; set; }

        /// <summary>
        ///     Возможность редактирования
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        ///     Входящее
        /// </summary>
        public bool IsIncomeFlag { get; set; }

        /// <summary>
        ///    Только один лагерь
        /// </summary>
        public bool OnlyOneCamp { get; set; }

        /// <summary>
        ///    Только один профсоюз
        /// </summary>
        public bool OnlyOneTradeUnion { get; set; }

        /// <summary>
        ///     Периоды отдыха
        /// </summary>
        public List<GroupedTimeOfRest> TimeOfRests { get; set; }

        /// <summary>
        ///     Года отдыха
        /// </summary>
        public List<YearOfRest> YearOfRests { get; set; }

        /// <summary>
        ///    Вид документа
        /// </summary>
        public List<DocumentType> DocumentTypes { get; set; }

        /// <summary>
        ///    Статус по отношению к ребенку
        /// </summary>
        public List<TradeUnionStatusByChild> StatusByChild { get; set; }

        /// <summary>
        ///    Комментарий при переводе Профсоюзного списка в статус "Отклонено".
        /// </summary>
        public string CommentToDeclined { get; set; }

        /// <summary>
        ///     Дубли детей в других списках в рамках года
        /// </summary>
        public ICollection<Person> DoubleChildren { get; set; }

        /// <summary>
        ///    Получение данных
        /// </summary>
        /// <returns></returns>
        public override TradeUnionList BuildData()
        {
            if (Data.Id > 0)
            {
                Data.Campers = (Campers?.Values.ToList()
                                    .Select(c => c?.BuildEntity())
                                    .Where(o => o != null)
                                    .ToList() ?? Data.Campers ?? new List<TradeUnionCamper>());
            }
            else
            {
                Data.Campers = (CampersJson?.Values.ToList()
                    .Select(c => JsonConvert.DeserializeObject<TradeUnionCamperModel>(c)?.BuildEntity())
                    .Where(o => o != null)
                    .ToList() ?? Data.Campers ?? new List<TradeUnionCamper>());
            }

            return base.BuildData();
        }
    }
}
