using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Security.Logger;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;
using RestChild.Web.Models.Business;
using RestChild.Web.Properties;


namespace RestChild.Web.Controllers
{
    [Authorize]
    public class LeisureFacilitiesController : BaseController
    {
        public LeisureFacilitiesController()
        {

        }
        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiController.SetUnitOfWorkInRefClass(unitOfWork);
        }
        /// <summary>
        ///     Gets or sets the api controller.
        /// </summary>
        public WebLeisureFacilitiesController ApiController { get; set; }
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
        ///     Поиск объектов отдыха
        /// </summary>
        /// <returns>
        ///     The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Search(LeisureFacilitiesListModels model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (model.GroupId == 0)
            {
                model.GroupId = null;
            }
            var restPlaces = ApiController.Get(model);
            return View("LeisureFacilitiesList", restPlaces); 
        }

        public ActionResult Insert()
        {
            var newPlace = new LeisureFacilltiesViewModel(new LeisureFacilities())
            {
                StateDistricts = UnitOfWork.GetSet<StateDistrict>().OrderBy(s => s.Name).ToList()
            };
           
            return View("LeisureFacilltiesEdit", newPlace);
        }

        /// <summary>
        ///     Обновление объекта отдыха
        /// </summary>
        public ActionResult Update(long id)
        {
            var entity = ApiController.Get(id);
            return View("LeisureFacilltiesEdit", CreateModel(entity));//, place);
        }
        /// <summary>
        ///     Удаление объекта отдыха
        /// </summary>
        public ActionResult Delete(long id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var place = ApiController.Get(id);
            UnitOfWork.Delete(place);
            UnitOfWork.SaveChanges();
            return RedirectToAction(nameof(Search));
        }
        /// <summary>
        ///     сохранение объекта отдыха
        /// </summary>
        [HttpPost]
        public ActionResult Save(LeisureFacilltiesViewModel place)
        {
            var data = place.BuildData();
            var currentAccountId = Security.GetCurrentAccountId();
            if ((!ModelState.IsValid || !ValidateModel(data)))
            {
                return View("LeisureFacilltiesEdit", CreateModel(data));
            }

            SetUnitOfWorkInRefClass(UnitOfWork);
            ApiController.SaveFacilities(data, currentAccountId);
            return RedirectToAction(nameof(Search)); 
        }

        /// <summary>
        ///     выгрузка в excel
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>       
        public ActionResult ExportFacilltiesToExcel(LeisureFacilitiesListModels place)
        {
            var restPlaces = ApiController.Get(place);
            var count = restPlaces.Groups.Count();
            
            if (count <= 1000)
            {
                var file = LeisureFacilitiesListExcelExport.GenerateFile(restPlaces.Groups);
                if (!string.IsNullOrEmpty(file))
                {
                    return FileAndDeleteOnClose(file,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Объекты отдыха.xlsx");
                }

                return null;
            }
            return RedirectToAction(nameof(Search));
        }
        
        private LeisureFacilltiesViewModel CreateModel(LeisureFacilities entity)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            

            var model = new LeisureFacilltiesViewModel(entity)
            {
                StateDistricts = UnitOfWork.GetSet<StateDistrict>().OrderBy(s => s.Name).ToList()
            };
            return model;
        }
        private bool ValidateModel(LeisureFacilities leisureFacilities)
        {
            var isOk = true;
            if (leisureFacilities == null)
            {
                ModelState.AddModelError(string.Empty, "Ошибка получения данных");
                return false;
            }
            if (string.IsNullOrWhiteSpace(leisureFacilities.Abbreviated))
            {
                ModelState.AddModelError("Data.Abbreviated", "Необходимо указать сокращённое название");
                isOk = false;
            }
            if (string.IsNullOrWhiteSpace(leisureFacilities.Fullname))
            {
                ModelState.AddModelError("Data.Fullname", "Необходимо указать полное название");
                isOk = false;
            }
            if (string.IsNullOrWhiteSpace(leisureFacilities.ActualAdress))
            {
                ModelState.AddModelError("Data.ActualAdress", "Необходимо указать физический адрес");
                isOk = false;
            }
            if (string.IsNullOrWhiteSpace(leisureFacilities.Inn))
            {
             
                ModelState.AddModelError("Data.Inn", "Необходимо указать ИНН");
                isOk = false;
                
            }
            else
            {
                if (leisureFacilities.Inn.Length != 10)
                {
                    ModelState.AddModelError("Data.Inn", "Необходимо указать ИНН полностью");
                    isOk = false;
                }
            }
            if (string.IsNullOrWhiteSpace(leisureFacilities.StateDistrictId.ToString()))
            {
                ModelState.AddModelError("Data.StateDistrictId", "Необходимо указать регион");
                isOk = false;
            }
            return isOk;
        }

    }
}
