﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using RestChild.Extensions.Filter;
using RestChild.DAL;
using RestChild.Domain;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebHotelPlacementController : WebGenericRestController<HotelPlacement>
    {
		/// <summary>
		///     Поиск размещения отеля
		/// </summary>
		/// <param name="name">Название</param>
		/// <param name="pageNumber">Номер страницы (начиная с 1)</param>
		public CommonPagedList<HotelPlacement> Get(string name, int pageNumber)
		{
			int pageSize = Settings.Default.TablePageSize;
			int startRecord = (pageNumber - 1) * pageSize;
			var query = UnitOfWork.GetSet<HotelPlacement>().Where(place => place.Name.Contains(name));
			int totalCount = query.Count();
			var entity = query.OrderBy(place => place.Name).Skip(startRecord).Take(pageSize).ToList();

			return new CommonPagedList<HotelPlacement>(entity, pageNumber, pageSize, totalCount);
		}

		public List<HotelPlacement> Get()
		{
			return UnitOfWork.GetSet<HotelPlacement>().ToList();
		}
    }
}
