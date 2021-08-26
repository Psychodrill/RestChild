using System;
using System.Collections.Generic;
using PagedList;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class CommonPagedList<T> : BasePagedList<T>
    {
        public CommonPagedList() : base(1, 10, 0)
        {
        }

        public CommonPagedList(IEnumerable<T> pageItems, int pageNumber, int pageSize, int totalItemCount)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "Страница не может быть меньше чем 1.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException("pageSize", pageSize,
                    "Размер страницы не может быть меньше чем 1.");
            }

            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            PageCount = TotalItemCount > 0 ? (int) Math.Ceiling(TotalItemCount / (double) PageSize) : 0;
            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < PageCount;
            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber >= PageCount;
            FirstItemOnPage = (PageNumber - 1) * PageSize + 1;
            var num = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = num > TotalItemCount ? TotalItemCount : num;
            if (pageItems != null)
            {
                Subset.AddRange(pageItems);
            }
        }
    }

    public class OrganizationPagedList
    {
        public string Name { get; set; }
        public int OrganizationType { get; set; }
        public long? StateDistrictId { get; set; }
        public long? OivId { get; set; }

        /// <summary>
        ///     регионы
        /// </summary>
        public List<StateDistrict> StateDistricts { get; set; }

        /// <summary>
        ///     регионы
        /// </summary>
        public List<Organization> Oivs { get; set; }

        /// <summary>
        ///     тип организации изменился
        /// </summary>
        public bool ChangeOrgType { get; set; }

        public CommonPagedList<Organization> CommonPagedList { get; set; }
    }
}
