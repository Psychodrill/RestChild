
using RestChild.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RestChild.Web.Properties;
using RestChild.Domain;
using RestChild.Web.Extensions;

namespace RestChild.Web.Controllers.WebApi
{

       
    /// <summary>
	///     Универсальный WEBApi контроллер, реализующий CRUD
	/// </summary>
	/// <typeparam name="T"></typeparam>
    public class WebLeisureFacilitiesController : WebGenericRestController<LeisureFacilities>
    {
        private static readonly string EditEventCode = "Изменение объекта отдыха";
        /// <summary>
		///     Сохранение объекта отдыха
		/// </summary>
		/// <param name="model">Объект отдыха</param>
		/// <param name="userId">Пользователь совершающий действие</param>
		public void SaveFacilities(LeisureFacilities model, long? userId)
        {
            var now = DateTime.Now;

            if (model.Id == 0)
            {
                // data.IsActive = true;

                AddNewLeisureFacilities(model, now);
            }
            else
            {
                Update(model, userId);
            }
        }
        /// <summary>
        ///    Добавление объекта отдыха
        /// </summary>
        private void AddNewLeisureFacilities(LeisureFacilities model, DateTime lastUpdateTick)
        {
            var parties = UnitOfWork.GetSet<LeisureFacilities>();

            model.LastUpdateTick = lastUpdateTick.Ticks;
            model.HistoryLink = this.WriteHistory(model.HistoryLink, EditEventCode, string.Empty);
            model.HistoryLinkId = model.HistoryLink?.Id;
            parties.Add(model);
            UnitOfWork.SaveChanges();
        }
       
        /// <summary>
        ///     Обновление объекта отдыха
        /// </summary>
        public void Update(LeisureFacilities model, long? userId)
        {
            using (var ts = UnitOfWork.GetTransactionScope())
            {
                IQueryable<LeisureFacilities> query = UnitOfWork.GetSet<LeisureFacilities>();
                var entity = query.FirstOrDefault(i => i.Id == model.Id);
                if (entity != null)
                {
                    entity.Abbreviated = model.Abbreviated;
                    entity.ActualAdress = model.ActualAdress;
                    entity.Fullname = model.Fullname;
                    entity.Inn = model.Inn;
                    entity.StateDistrict = model.StateDistrict;
                    entity.StateDistrictId = model.StateDistrictId;
                    entity.LastUpdateTick = DateTime.Now.Ticks;

                    entity.HistoryLink = this.WriteHistory(entity.HistoryLink, EditEventCode, string.Empty);
                    entity.HistoryLinkId = entity.HistoryLink?.Id;
                }

                UnitOfWork.SaveChanges();
                ts.Complete();
            }
        }
        /// <summary>
        ///     Поиск регионов отдыха
        /// </summary>
        public LeisureFacilitiesListModels Get(LeisureFacilitiesListModels model)
        {
            var pageSize = Settings.Default.TablePageSize;
            var page = model?.NewPageNumber ?? 0;
            if (page == 0)
            {
                page = 1;
            }

            var startRecord = (page - 1) * pageSize;

            var name = model?.Name?.ToLower();
            var query = UnitOfWork.GetSet<LeisureFacilities>().AsQueryable();

            
            if (model.Name!=null)
            {
                query = query.Where(place => place.Fullname.Contains(model.Name));
            }
            if (model.Inn != null)
            {
                query = query.Where(place => place.Inn.Contains(model.Inn));
            }
            
            var totalCount = query.Count();
            var entity = query.OrderBy(place => place.Fullname).Skip(startRecord).Take(pageSize).ToList()
                .Select(p => new LeisureFacilities(p, 2)).ToList();

            return new LeisureFacilitiesListModels(entity, page, pageSize, totalCount, model);
        }

    }
}
