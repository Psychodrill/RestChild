using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     Конфиг заполнения таблицы интеграции с ЕРЛ
    /// </summary>
    public class ERLStatusTableFillConfig : BaseConfig
    {
        /// <summary>
        ///     Ограничение кол-ва передаваемых детей
        /// </summary>
        public int? ChildCount { get; set; }

        /// <summary>
        ///     Минимальный идентификатор заявления
        /// </summary>
        public long? RequestIdMin { get; set; }

        /// <summary>
        ///     Максимальный идентификатор заявления
        /// </summary>
        public long? RequestIdMax { get; set; }
    }
}
