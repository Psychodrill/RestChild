using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestChild.Domain;
using RestChild.Web.Models.TradeUnion;

namespace RestChild.Web.Models.TradeUnionCashback
{
    /// <summary>
    ///    модель списка претендентов на кэшбэк
    /// </summary>
    public class TradeUnionCashbackModel : TradeUnionModel
    {
        public TradeUnionCashbackModel()
        {
        }

        public TradeUnionCashbackModel(TradeUnionList data) : base(data)
        {
            var items = data?.Campers?.OrderBy(c => c.Child?.LastName)
                            .ThenBy(c => c.Child?.FirstName)
                            .Select(c => new TradeUnionCashbackCamperModel(c)).ToList() ??
                        new List<TradeUnionCashbackCamperModel>();

            Campers = items.ToDictionary(c => c.Id.ToString(), c => c);
            CampersJson = items.ToDictionary(c => c.Id.ToString(), JsonConvert.SerializeObject);
        }

        /// <summary>
        ///    Отдыхающие
        /// </summary>
        public new Dictionary<string, TradeUnionCashbackCamperModel> Campers { get; set; }

        /// <summary>
        ///    Организации
        /// </summary>
        public List<Organization> Organizations { get; set; }

        /// <summary>
        ///     Степени участия льготы в стоимости
        /// </summary>
        public ICollection<TradeUnionCamperPrivilegePart> PrivilegeParts { get; set; }

        /// <summary>
        ///    Вид документа
        /// </summary>
        public List<DocumentType> DocumentTypesForParent { get; set; }
    }
}
