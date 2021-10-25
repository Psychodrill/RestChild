using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
    [Authorize]
    public class RestPlacesController : BaseController
    {
        /// <summary>
        ///     Gets or sets the api controller.
        /// </summary>
        public WebRestPlaceController ApiController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiController.SetUnitOfWorkInRefClass(unitOfWork);
        }

        /// <summary>
        ///     The index.
        /// </summary>
        /// <returns>
        ///     The <see cref="ActionResult" />.
        /// </returns>
        public override ActionResult Index()
        {
            return RedirectToAction(nameof(Search));
        }

        /// <summary>
        ///     Поиск направлений отдыха
        /// </summary>
        /// <returns>
        ///     The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Search(PlaceOfRestListModel model)
        {
            if (!Security.HasAnyRights(new[] {AccessRightEnum.PlaceOfRestManage}))
            {
                return RedirectToAvalibleAction();
            }

            SetUnitOfWorkInRefClass(UnitOfWork);
            if (model.GroupId == 0)
            {
                model.GroupId = null;
            }

            var restPlaces = ApiController.Get(model);
            restPlaces.Groups = UnitOfWork.GetSet<PlaceOfRest>().Where(p => p.IsActive && p.NotForSelect).ToList();
            return View("PlaceOfRestList", restPlaces);
        }

        public ActionResult Insert()
        {
            var newPlace = new PlaceOfRest();
            PrepareView();
            newPlace.IsActive = true;
            return View("PlaceOfRestEdit", newPlace);
        }

        private void PrepareView()
        {
            ViewBag.Groups = UnitOfWork.GetSet<PlaceOfRest>().Where(p => p.IsActive && p.NotForSelect).ToList();
            var trs = UnitOfWork.GetSet<TypeOfRest>().Select(t => (long?) t.ParentId).Where(t => t.HasValue);
            ViewBag.TypeOfRests = UnitOfWork.GetSet<TypeOfRest>()
                .Where(p => p.IsActive && !p.Commercial && !trs.Contains(p.Id))
                .OrderBy(t => t.Name).ToList();
        }

        public ActionResult Update(long id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var place = ApiController.Get(id);
            PrepareView();
            if (place.PhotoUrl != null)
            {
                ViewBag.ImgUrl = Settings.Default.RestPlaceImagesVirtualPath + "/" + place.PhotoUrl;
            }
            else
            {
                ViewBag.ImgUrl = string.Empty;
            }

            place.TypeOfRestIds = place.TypeOfRests?.Select(t => t.TypeOfRestId).ToArray() ?? new long?[0];

            return View("PlaceOfRestEdit", place);
        }

        /// <summary>
        ///     сохранение направления отдыха
        /// </summary>
        [HttpPost]
        public ActionResult Save(PlaceOfRest place)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            if (place.GroupId == 0)
            {
                place.GroupId = null;
            }

            if (!ModelState.IsValid)
            {
                return View("PlaceOfRestEdit", place);
            }

            if (place.Id == 0)
            {
                if (place.TypeOfRestIds != null && place.TypeOfRestIds.Any())
                {
                    place.TypeOfRests = place.TypeOfRestIds.Select(t => new PlaceOfRestTypeOfRest {TypeOfRestId = t})
                        .ToList();
                }

                ApiController.Post(place);
            }
            else
            {
                var typeOfRestIds = place.TypeOfRestIds?.ToList() ?? new List<long?>();
                ApiController.Put(place.Id, place);

                var typesOfRest = UnitOfWork.GetSet<PlaceOfRestTypeOfRest>().Where(p => p.PlaceOfRestId == place.Id)
                    .ToList();
                var newIds = typeOfRestIds.Where(t => typesOfRest.All(v => v.TypeOfRestId != t)).Distinct().ToList();
                if (newIds.Any())
                {
                    foreach (var id in newIds)
                    {
                        UnitOfWork.AddEntity(new PlaceOfRestTypeOfRest {TypeOfRestId = id, PlaceOfRestId = place.Id});
                    }
                }

                var toDelete = typesOfRest.Where(t => typeOfRestIds.All(v => v != t.TypeOfRestId)).ToList();
                foreach (var item in toDelete)
                {
                    UnitOfWork.Delete(item);
                }
            }

            UnitOfWork.SaveChanges();

            return RedirectToAction("Update", new {id = place.Id});
        }

    }
}
