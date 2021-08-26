using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.Domain;
using RestChild.Admin.Web.Properties;

namespace RestChild.Admin.Web.Controllers.WebApi
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

         bool _new = false;
         var _old_rights = new List<AccessRight>();
			if (loaded == null)
			{
            _new = true;
				entity.AccessRights = accessRights;
				loaded = UnitOfWork.AddEntity(entity);
            RestChild.Security.Logger.SecurityLogger.AddToLog(UnitOfWork, SecurityJournalEventType.RightsAndRoles, "Создана новая роль", string.Format("Роль {0} ({1}) создана", entity.Name, entity.Id), Security.GetCurrentAccountId().Value);
         }
			else
			{
            _old_rights = loaded.AccessRights.ToList();
				loaded.Name = entity.Name;
				UnitOfWork.CopyCollection(accessRights, loaded.AccessRights);
            RestChild.Security.Logger.SecurityLogger.AddToLog(UnitOfWork, SecurityJournalEventType.RightsAndRoles, "Роль изменена", string.Format("Роль {0} ({1}) изменена", entity.Name, entity.Id), Security.GetCurrentAccountId().Value);
         }

			loaded = UnitOfWork.Update(loaded);

         if(_new)
         {
            foreach(var r in loaded.AccessRights)
            {
               RestChild.Security.Logger.SecurityLogger.AddToLog(UnitOfWork, SecurityJournalEventType.RightsAndRoles, "Роль изменена", $"Для роли {entity.Name} ({entity.Id}) изменен состав прав. Добавлено право: {r.Name} ({r.Code}).", Security.GetCurrentAccountId().Value);
            }
         }
         else
         {
            foreach(var r in _old_rights.Where(s => !accessRights.Any(x => s.Id == x.Id)))
            {
               RestChild.Security.Logger.SecurityLogger.AddToLog(UnitOfWork, SecurityJournalEventType.RightsAndRoles, "Роль изменена", $"Для роли {entity.Name} ({entity.Id}) изменен состав прав. Удалено право: {r.Name} ({r.Code}).", Security.GetCurrentAccountId().Value);
            }
            foreach (var r in accessRights.Where(s => !_old_rights.Any(x => s.Id == x.Id)))
            {
               RestChild.Security.Logger.SecurityLogger.AddToLog(UnitOfWork, SecurityJournalEventType.RightsAndRoles, "Роль изменена", $"Для роли {entity.Name} ({entity.Id}) изменен состав прав. Добавлено право: {r.Name} ({r.Code}).", Security.GetCurrentAccountId().Value);
            }

         }

         return loaded;
		}
	}
}
