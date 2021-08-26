using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.Domain;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebRoleController : WebGenericRestController<Role>
	{
		/// <summary>
		///     Поиск ролей
		/// </summary>
		/// <param name="name">Имя роли</param>
		/// <param name="pageNumber">Номер страницы (начиная с 1)</param>
		/// <returns>Список найденных ролей</returns>
		public CommonPagedList<Role> Get(string name, int pageNumber)
		{
			IQueryable<Role> query = UnitOfWork.GetSet<Role>().Where(place => place.Name.Contains(name));

			if (!Security.HasRight(AccessRightEnum.CommercialPart))
			{
				query = query.Where(r => r.AccessRights.All(a => a.GroupCode != AccessRightEnum.CommercialPart));
			}

			int pageSize = Settings.Default.TablePageSize;
			int startRecord = (pageNumber - 1)*pageSize;
			int totalCount = query.Count();
			List<Role> entity = query.OrderBy(place => place.Name).Skip(startRecord).Take(pageSize).ToList();

			return new CommonPagedList<Role>(entity, pageNumber, pageSize, totalCount);
		}

		public IEnumerable<Role> Get()
		{
			return UnitOfWork.GetSet<Role>().ToList();
		}

		public override Role Put(long id, Role entity)
		{
			return Save(entity);
		}

		public override Role Post(Role entity)
		{
			return Save(entity);
		}

		private Role Save(Role entity)
		{
			if (entity == null)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			entity.AccessRights = entity.AccessRights ?? new List<AccessRight>();
			var arsIds = entity.AccessRights?.Select(a => a.Id) ?? new List<long>();
			var accessRights = UnitOfWork.GetSet<AccessRight>().Where(a => arsIds.Contains(a.Id)).ToList();
			Role loaded = UnitOfWork.GetSet<Role>().FirstOrDefault(x => x.Id == entity.Id);


			if (loaded == null)
			{
				entity.AccessRights = accessRights;
				loaded = UnitOfWork.AddEntity(entity);
			}
			else
			{
				loaded.Name = entity.Name;
				UnitOfWork.CopyCollection(accessRights, loaded.AccessRights);
			}

			loaded = UnitOfWork.Update(loaded);
			return loaded;
		}
	}
}
