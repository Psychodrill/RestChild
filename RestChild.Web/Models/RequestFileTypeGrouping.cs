using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models
{
    /// <summary>
    ///     Группировка документов по блокам
    /// </summary>
    public enum RequestFileTypeGrouping : int
    {
        /// <summary>
        ///     Заявитель
        /// </summary>
        Applicant = 1,
        /// <summary>
        ///     Сопровождающий
        /// </summary>
        Attendant = 2,
        /// <summary>
        ///     Дети/ребёнок
        /// </summary>
        Child = 3,
        /// <summary>
        ///     Общее
        /// </summary>
        Other = 4
    }
}
