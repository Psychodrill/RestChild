using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
    public class TagController : BaseController
    {
		/// <summary>
		/// поиск элемента
		/// </summary>
		public ActionResult List(TagFilterModel model)
		{
			if (!Security.HasRight(AccessRightEnum.VocabularyManage))
			{
				RedirectToAvalibleAction();
			}

			model = model ?? new TagFilterModel(new List<Tag>(), 0, 10, 0);
			var items = UnitOfWork.GetSet<Tag>().AsQueryable();

			if (!string.IsNullOrWhiteSpace(model.SearchString))
			{
				var ss = model.SearchString.ToLower();
				items = items.Where(i => i.Name.ToLower().Contains(ss));
			}

			if (!model.ViewArchive)
			{
				items = items.Where(i => !i.IsDeleted);
			}

			var totalCount = items.Count();

			var pageSize = Settings.Default.TablePageSize;
			var pageNumber = model.PageNumberEx ?? model.PageNumber;
			var startRecord = (pageNumber - 1) * pageSize;

			var entity =
				items.OrderBy(d => d.Name).ThenByDescending(b => b.Id).Skip(startRecord).Take(pageSize).ToList();

			return View(new TagFilterModel(entity, pageNumber, model.PageSize, totalCount)
			{
				SearchString = model.SearchString,
				PageNumberEx = model.PageNumberEx
			});
		}

		/// <summary>
		/// поиск элемента
		/// </summary>
		public ActionResult Edit(long? id)
		{
			SetUnitOfWorkInRefClass();
			if (!Security.HasRight(AccessRightEnum.VocabularyManage))
			{
				RedirectToAvalibleAction();
			}

			var entity = UnitOfWork.GetById<Tag>(id) ?? new Tag();
			return View(entity);
		}

		/// <summary>
		/// поиск элемента
		/// </summary>
		public ActionResult Save(Tag model)
		{
			SetUnitOfWorkInRefClass();
			if (!Security.HasRight(AccessRightEnum.VocabularyManage))
			{
				RedirectToAvalibleAction();
			}


			if (model.Id == 0)
			{
				model.LastUpdateTick = DateTime.Now.Ticks;
				model = UnitOfWork.AddEntity(model);
			}
			else
			{
				var entity = UnitOfWork.GetById<Tag>(model.Id);
				if (entity.LastUpdateTick != model.LastUpdateTick)
				{
					SetRedicted();
				}
				else
				{
					entity.Name = model.Name;
					entity.LastUpdateTick = DateTime.Now.Ticks;
					entity.Description = model.Description;
					entity.IsDeleted = model.IsDeleted;
					model = UnitOfWork.Update(entity);
					UnitOfWork.SaveChanges();
				}
			}

			return RedirectToAction("Edit", new { id = model.Id });
		}

	}
}
