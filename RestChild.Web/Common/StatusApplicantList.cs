using System.Collections.Generic;
using System.Web.Mvc;

namespace RestChild.Web.Common
{
    /// <summary>
    ///     Статус подающего заявление
    /// </summary>
    public static class StatusApplicantList
    {
        /// <summary>
        ///     Коллекция статусов подающего заявления. (Оригинальный справочник ведется в МПГУ)
        /// </summary>
        public static List<SelectListItem> Data = new List<SelectListItem>
        {
            new SelectListItem {Value = "1", Text = "Отец"},
            new SelectListItem {Value = "2", Text = "Мать"},
            new SelectListItem {Value = "3", Text = "Законный представитель"},
            new SelectListItem {Value = "4", Text = "Доверенное лицо"},
            new SelectListItem {Value = "5", Text = "Лицо из числа детей-сирот и детей, оставшихся без попечения родителей"}
        };
    }
}
