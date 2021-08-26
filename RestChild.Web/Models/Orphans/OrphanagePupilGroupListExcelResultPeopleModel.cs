using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///  Модель людей для расширенной модели списка/группы отправки
    /// </summary>
    public class OrphanagePupilGroupListExcelResultPeopleModel
    {
        /// <summary>
        ///     ФИО
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Билеты туда
        /// </summary>
        public bool TicketsTo { get; set; }

        /// <summary>
        ///     Билеты оттуда
        /// </summary>
        public bool TicketsFrom { get; set; }

        /// <summary>
        ///     Наркотики
        /// </summary>
        public string[] Drugs { get; set; }

        /// <summary>
        ///     Передвигается с помощью кресла каталки
        /// </summary>
        public bool Pandicapped { get; set; }

        /// <summary>
        ///     Особенности питания
        /// </summary>
        public string[] FoodFeatures { get; set; }
    }
}
