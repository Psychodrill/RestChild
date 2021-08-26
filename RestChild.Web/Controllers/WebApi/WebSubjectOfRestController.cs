using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebSubjectOfRestController : WebGenericRestController<SubjectOfRest>
	{
		/// <summary>
		///     Поиск тематик смены
		/// </summary>
		/// <param name="name">Название</param>
		/// <param name="classId">классификация</param>
		/// <param name="pageNumber">Номер страницы (начиная с 1)</param>
		public CommonPagedList<SubjectOfRest> Get(string name, long? classId, int pageNumber)
		{
			int pageSize = Settings.Default.TablePageSize;
			int startRecord = (pageNumber - 1)*pageSize;
			name = name?.ToLower();
			IQueryable<SubjectOfRest> query = UnitOfWork.GetSet<SubjectOfRest>().Where(place => place.Name.ToLower().Contains(name));
			if (classId > 0)
			{
				query = query.Where(q => q.SubjectOfRestClassificationId == classId);
			}

			int totalCount = query.Count();
			List<SubjectOfRest> entity = query.OrderBy(place => place.Name).Skip(startRecord).Take(pageSize).ToList();

			return new CommonPagedList<SubjectOfRest>(entity, pageNumber, pageSize, totalCount);
		}

		public ICollection<SubjectOfRest> Get()
		{
			return UnitOfWork.GetSet<SubjectOfRest>().Where(s=>s.IsActive).ToList();
		}

		public override SubjectOfRest Put(long id, SubjectOfRest data)
		{
			var entity = UnitOfWork.GetById<SubjectOfRest>(id);

			entity.DescriptionHtml = data.DescriptionHtml;
			entity.Description = data.Description;
			entity.Name = data.Name;
			entity.PhotoUrl = data.PhotoUrl;
			entity.IsActive = data.IsActive;
			entity.ViewOnSite = data.ViewOnSite;
			entity.ViewOnMpgu = data.ViewOnMpgu;
			entity.SubjectOfRestClassificationId = data.SubjectOfRestClassificationId;
			entity.CreateUserId = Security.GetCurrentAccountId();
			entity.LastUpdateTick = DateTime.Now.Ticks;
			entity = UnitOfWork.Update(entity);

			if (entity.LinkToFile == null || entity.LinkToFile.Id == 0)
			{
				entity.LinkToFile = UnitOfWork.AddEntity(new LinkToFile());
				entity.LinkToFileId = entity.LinkToFile.Id;
			}

			var fileToExclude = entity.LinkToFile.Files.Where(fl => !data.LinkToFile.Files.Select(f => f.Id).Contains(fl.Id));
			foreach (var file in data?.LinkToFile?.Files?.Where(f=>f.Id == 0) ?? new List<FileOrLink>())
			{
				file.LinkId = entity.LinkToFileId;
				UnitOfWork.AddEntity(file);
			}

			foreach (var file in fileToExclude)
			{
				UnitOfWork.Delete(file);
			}

			return entity;
		}
	}
}
