using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Extensions;
using RestChild.Web.Models;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
	[Authorize]
	public class SubjectOfRestController : BaseController
	{
		public WebSubjectOfRestController ApiController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		public ActionResult Index()
		{
			return RedirectToAction("Search");
		}

		public ActionResult Search(string name = "", long? classId = 0, int pageNumber = 1)
		{
			SetUnitOfWorkInRefClass();
			if (!Security.HasRight(AccessRightEnum.VocabularyManage))
			{
				RedirectToAvalibleAction();
			}

			var restPlaces = ApiController.Get(name, classId, pageNumber);
			ViewBag.name = name;
			ViewBag.Classifications = GetClassifications();
            return View("SubjectOfRestList", restPlaces);
		}

		public ActionResult Insert()
		{
			SetUnitOfWorkInRefClass();
			if (!Security.HasRight(AccessRightEnum.VocabularyManage))
			{
				RedirectToAvalibleAction();
			}

			var newPlace = new SubjectOfRestModel
			{
				Data =
				{
					IsActive = true,
					ViewOnSite = true,
					ViewOnMpgu = true
				},
				Classifications = GetClassifications()
		};

			return View("SubjectOfRestEdit", newPlace);
		}

		/// <summary>
		/// получить классификацию
		/// </summary>
		/// <returns></returns>
		private List<SubjectOfRestClassification> GetClassifications()
		{
			return UnitOfWork.GetSet<SubjectOfRestClassification>().Where(c=>!c.IsArchive).ToList().Select(s=>new SubjectOfRestClassification(s)).OrderBy(s=>s.Name).ToList();
		}

		public ActionResult Update(long id)
		{
			SetUnitOfWorkInRefClass();
			if (!Security.HasRight(AccessRightEnum.VocabularyManage))
			{
				RedirectToAvalibleAction();
			}

			var subjectOfRest = ApiController.Get(id);
			if (subjectOfRest.PhotoUrl != null)
			{
				ViewBag.ImgUrl = Settings.Default.RestPlaceImagesVirtualPath + "/" + subjectOfRest.PhotoUrl;
			}
			else
			{
				ViewBag.ImgUrl = string.Empty;
			}

			var model = new SubjectOfRestModel(subjectOfRest)
			{
				Classifications = GetClassifications()
			};

			return View("SubjectOfRestEdit", model);
		}

		[HttpPost]
		public ActionResult Save(SubjectOfRestModel model)
		{
			SetUnitOfWorkInRefClass();
			if (!Security.HasRight(AccessRightEnum.VocabularyManage))
			{
				RedirectToAvalibleAction();
			}
			var subjectOfRest = model.BuildData();

            if (!ModelState.IsValid)
            {
	            model.Classifications = GetClassifications();
				return View("SubjectOfRestEdit", model);
			}

			if (subjectOfRest.SubjectOfRestClassificationId <= 0)
			{
				subjectOfRest.SubjectOfRestClassificationId = null;
			}

			if (subjectOfRest.Id == 0)
			{
				subjectOfRest.LastUpdateTick = DateTime.Now.Ticks;
				subjectOfRest = ApiController.Post(subjectOfRest);
				subjectOfRest.HistoryLink = this.WriteHistory(subjectOfRest.HistoryLink, "Создание новой тематики смены", String.Empty);
				subjectOfRest.HistoryLinkId = subjectOfRest.HistoryLink?.Id;
				UnitOfWork.SaveChanges();
			}
			else
			{
				if (UnitOfWork.GetSet<SubjectOfRest>().FirstOrDefault(s => s.Id == subjectOfRest.Id)?.LastUpdateTick != subjectOfRest.LastUpdateTick)
				{
					SetRedicted();
				}
				else
				{
					subjectOfRest = ApiController.Put(subjectOfRest.Id, subjectOfRest);
					subjectOfRest.HistoryLink = this.WriteHistory(subjectOfRest.HistoryLink, "Изменение тематики смены", String.Empty);
					subjectOfRest.HistoryLinkId = subjectOfRest.HistoryLink?.Id;
					UnitOfWork.SaveChanges();
				}
			}

			return RedirectToAction("Update", new {id = subjectOfRest.Id});
		}

		/// <summary>
		/// поиск элемента
		/// </summary>
		public ActionResult ClassificatorList(SubjectOfRestClassificationFilterModel model)
		{
			if (!Security.HasRight(AccessRightEnum.VocabularyManage))
			{
				RedirectToAvalibleAction();
			}

			model = model ?? new SubjectOfRestClassificationFilterModel(new List<SubjectOfRestClassification>(), 0, 10, 0);
			var items = UnitOfWork.GetSet<SubjectOfRestClassification>().AsQueryable();

			if (!string.IsNullOrWhiteSpace(model.SearchString))
			{
				var ss = model.SearchString.ToLower();
				items = items.Where(i => i.Name.ToLower().Contains(ss));
			}

			if (!model.ViewArchive)
			{
				items = items.Where(i => !i.IsArchive);
			}

			var totalCount = items.Count();

			var pageSize = Settings.Default.TablePageSize;
			var pageNumber = model.PageNumberEx ?? model.PageNumber;
			var startRecord = (pageNumber - 1) * pageSize;

			var entity =
				items.OrderBy(d => d.Name).ThenByDescending(b => b.Id).Skip(startRecord).Take(pageSize).ToList();

			return View(new SubjectOfRestClassificationFilterModel(entity, pageNumber, model.PageSize, totalCount)
			{
				SearchString = model.SearchString,
				PageNumberEx = model.PageNumberEx
			});
		}

		/// <summary>
		/// поиск элемента
		/// </summary>
		public ActionResult ClassificatorEdit(long? id)
		{
			SetUnitOfWorkInRefClass();
			if (!Security.HasRight(AccessRightEnum.VocabularyManage))
			{
				RedirectToAvalibleAction();
			}

			var entity = UnitOfWork.GetById<SubjectOfRestClassification>(id) ?? new SubjectOfRestClassification();
			entity.FileOrLink = entity.FileOrLink ?? new FileOrLink();
			return View(entity);
		}

		/// <summary>
		/// поиск элемента
		/// </summary>
		public ActionResult SaveClassificator(SubjectOfRestClassification model)
		{
			SetUnitOfWorkInRefClass();
			if (!Security.HasRight(AccessRightEnum.VocabularyManage))
			{
				RedirectToAvalibleAction();
			}
			if (string.IsNullOrWhiteSpace(model.FileOrLink.FileUrl))
			{
				model.FileOrLink = null;
				model.FileOrLinkId = null;
			}
			else
			{
				model.FileOrLink.IsPhoto = true;
				model.FileOrLink.FileName = "Фотография" + Path.GetExtension(model.FileOrLink.FileUrl);
				model.FileOrLink.LastUpdateTick = DateTime.Now.Ticks;
			}

			if (model.Id == 0)
			{
				model.LastUpdateTick = DateTime.Now.Ticks;
				model.HistoryLink = this.WriteHistory(model.HistoryLink, "Добавление новой записи классификатора тематик смены",
					$"Наименование: '{model.Name}', видимость на сайте: {model.ViewOnSite.FormatEx()}");
				model.HistoryLinkId = model.HistoryLink?.Id;
				model = UnitOfWork.AddEntity(model);
			}
			else
			{
				var entity = UnitOfWork.GetById<SubjectOfRestClassification>(model.Id);
				if (entity.LastUpdateTick != model.LastUpdateTick)
				{
					SetRedicted();
				}
				else
				{
					entity.HistoryLink = this.WriteHistory(entity.HistoryLink, "Изменение записи классификатора тематик смены",
						$"Наименование: '{model.Name}', видимость на сайте: {model.ViewOnSite.FormatEx()}");
					entity.HistoryLinkId = entity.HistoryLink?.Id;
					entity.Name = model.Name;
					entity.LastUpdateTick = DateTime.Now.Ticks;
					entity.ViewOnSite = model.ViewOnSite;
					entity.IsArchive = model.IsArchive;
					if (entity.FileOrLink?.FileUrl != model.FileOrLink?.FileUrl)
					{
						if (entity.FileOrLink != null)
						{
							var fl = entity.FileOrLink;
							fl.FileName = $"SubjectOfRestClassification.Id={entity.Id};fileName='{fl.FileName}';Tiks='{fl.LastUpdateTick}'";
							fl.LastUpdateTick = DateTime.Now.Ticks;
							entity.FileOrLink = null;
							entity.FileOrLinkId = null;
						}

						if (model.FileOrLink != null && !string.IsNullOrWhiteSpace(model.FileOrLink?.FileUrl))
						{
							entity.FileOrLink = UnitOfWork.AddEntity(model.FileOrLink);
							entity.FileOrLinkId = entity.FileOrLink?.Id;
						}
					}

					model = UnitOfWork.Update(entity);
					UnitOfWork.SaveChanges();
				}
			}

			return RedirectToAction("ClassificatorEdit", new {id = model.Id});
		}


	}
}
