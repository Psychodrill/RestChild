using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using RestChild.Domain;
using RestChild.Web.Models;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    ///     WebApi-контроллер для поиска, получения, добавления и изменения данных реестра регионов отдыха
    /// </summary>
    [Authorize]
    public class WebRestPlaceController : WebGenericRestController<PlaceOfRest>
    {
        /// <summary>
        ///     получить список мест отдыха
        /// </summary>
        [HttpPost]
        public List<PlaceOfRest> GetPlaceOfRests([FromBody] PlaceOfRestsSearchModel model)
        {
            var q = UnitOfWork.GetSet<PlaceOfRest>().AsQueryable();

            if (model?.PresentIds != null && model.PresentIds.Any())
            {
                var group =
                    UnitOfWork.GetSet<PlaceOfRest>()
                        .Where(p => model.PresentIds.Contains(p.Id))
                        .Where(p => p.GroupId.HasValue)
                        .Select(p => p.GroupId)
                        .ToArray();

                q = q.Where(
                    p => !model.PresentIds.Contains(p.Id) && (!group.Contains(p.GroupId) || !p.GroupId.HasValue));
            }

            if (model?.TypeOfRestId != null)
            {
                q = q.Where(v => !v.TypeOfRests.Any() || v.TypeOfRests.Any(t => t.TypeOfRestId == model.TypeOfRestId));
            }

            if (!string.IsNullOrWhiteSpace(model?.Term))
            {
                var t = model.Term.Trim().ToLower();
                q = q.Where(v => v.Name.ToLower().Contains(t));
            }

            var res = q.Where(p => p.IsActive && p.ForMpgu).OrderBy(p => p.Name).ToList()
                .Select(p => new PlaceOfRest(p)).ToList();

            foreach (var item in res)
            {
                if (!string.IsNullOrWhiteSpace(item.PhotoUrl))
                {
                    item.PhotoUrl = string.Format(WebConfigurationManager.AppSettings["UploadPhotoUrl"], item.PhotoUrl);
                }
            }

            return res;
        }

        /// <summary>
        ///     Поиск регионов отдыха
        /// </summary>
        public PlaceOfRestListModel Get(PlaceOfRestListModel model)
        {
            var pageSize = Settings.Default.TablePageSize;
            var page = model?.NewPageNumber ?? 0;
            if (page == 0)
            {
                page = 1;
            }

            var startRecord = (page - 1) * pageSize;

            var name = model?.Name?.ToLower();
            var query = UnitOfWork.GetSet<PlaceOfRest>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(place => place.Name.ToLower().Contains(name));
            }

            if (model?.ActiveOnly ?? true)
            {
                query = query.Where(place => place.IsActive);
            }

            if (model?.NotForSelect ?? true)
            {
                query = query.Where(place => place.NotForSelect);
            }


            if (model?.GroupId.HasValue ?? false)
            {
                query = query.Where(place => place.GroupId == model.GroupId);
            }

            var totalCount = query.Count();
            var entity = query.OrderBy(place => place.Name).Skip(startRecord).Take(pageSize).ToList()
                .Select(p => new PlaceOfRest(p, 2)).ToList();

            return new PlaceOfRestListModel(entity, page, pageSize, totalCount, model);
        }

        /// <summary>
        ///     Получение всех регионов отдыха
        /// </summary>
        public ICollection<PlaceOfRest> Get()
        {
            var query = UnitOfWork.GetSet<PlaceOfRest>().Where(p => p.IsActive);
            var entity = query.OrderBy(place => place.Name).ToList();
            return entity;
        }

        public ICollection<PlaceOfRest> GetActive()
        {
            IQueryable<PlaceOfRest> query = UnitOfWork.GetSet<PlaceOfRest>();
            var entity = query.Where(p => p.IsActive).OrderBy(place => place.Name).ToList();

            return entity;
        }
    }
}
