using RestChild.Domain;
using RestChild.Extensions.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models
{
    public class LeisureFacilitiesListModels : CommonPagedList<LeisureFacilities>
    {
        public LeisureFacilitiesListModels()
        {

        }
        public LeisureFacilitiesListModels(IEnumerable<LeisureFacilities> pageItems, int pageNumber, int pageSize, int totalItemCount,LeisureFacilitiesListModels model) : base(pageItems, pageNumber, pageSize, totalItemCount)
        {
            Inn = model?.Inn;
            Name = model?.Name;
            ActiveOnly = model?.ActiveOnly ?? true;
            GroupId = model?.GroupId;
            NotForSelect = model?.NotForSelect ?? false;
            NewPageNumber = model?.NewPageNumber ?? 1;
            Groups = pageItems.ToList();
        }

        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }
        /// <summary>
        ///     наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
		/// только активные
		/// </summary>
		public bool ActiveOnly { get; set; }

        /// <summary>
        /// группа
        /// </summary>
        public long? GroupId { get; set; }

        /// <summary>
        /// Групповой элемент
        /// </summary>
        public bool NotForSelect { get; set; }

        /// <summary>
        /// новый номер страницы
        /// </summary>
        public int NewPageNumber { get; set; }

        /// <summary>
        /// группы
        /// </summary>
        public List<LeisureFacilities> Groups { get; set; }


    }
}
