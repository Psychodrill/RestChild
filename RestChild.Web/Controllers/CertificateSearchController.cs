using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using RestChild.Booking.Logic.Extensions;
using RestChild.Booking.Logic.Services;
using RestChild.Comon;
using RestChild.Comon.Config;
using RestChild.Comon.Dto;
using RestChild.Comon.Dto.Booking;
using RestChild.Comon.Dto.SearchRestChild;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Extensions;
using RestChild.Extensions.Filter;
using RestChild.Security.Logger;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Extensions;
using RestChild.Web.Models;
using RestChild.Web.Models.Business.Export;
using RestChild.Web.Properties;
using TimeOfRest = RestChild.Domain.TimeOfRest;

namespace RestChild.Web.Controllers
{
  

    /// <summary>
    ///     управление заявочной кампанией
    /// </summary>
    [System.Web.Mvc.Authorize]
    public partial class CertificateSearchController : BaseController
    {
        private readonly string DefaultOptionValue = "-- Не выбрано --";
        public WebCertificateSearchController ApiController { get; set; }
        public WebVocabularyController VocController { get; set; }
        public WebBtiDistrictsController DistrictController { get; set; }
        public WebHotelsController HotelController { get; set; }
        public WebContractController ContractController { get; set; }

        public ActionResult CertificateList(CertificateFilterModel model)
        {
            if (!Security.HasAnyRightsForSomeOrganization(new[] { AccessRightEnum.RequestView }))
            {
                return RedirectToAvalibleAction();
            }

            SetUnitOfWorkInRefClass(UnitOfWork);

            PrepareVocabulary(null, model);
            model = model ?? new CertificateFilterModel();

            if (model.PageSize == 0 || model.PageNumber == 0)
            {
                model.PageSize = 10;
                model.PageNumber = 1;
            }

            if (!model.HotelsId.IsNullOrEmpty())
            {
                model.HotelName = UnitOfWork.GetById<Hotels>(model.HotelsId.Value).Name;
            }

            return View(ApiController.CertificateList(model));
        }

        /// <summary>
        ///     заполнение справочников во ViewBag
        /// </summary>
        /// <param name="request"></param>
        /// <param name="filter"></param>
        private void PrepareVocabulary(Certificate certificate = null, CertificateSearchFilterModel filter = null)
        {
            ViewBag.Hotels =
                HotelController.Get().OrderBy(p => p.Name)
                .InsertAt(new Hotels { Id = 0, Name = DefaultOptionValue }, 0);

            ViewBag.PlacesOfRest =
                VocController.GetPlacesOfRestInternal(true).OrderBy(p => p.Id)
                    .InsertAt(new PlaceOfRest { Id = 0, Name = DefaultOptionValue });

            ViewBag.Contracts =
                UnitOfWork.GetAll<Contract>().Where(c => c.OrganizationId.HasValue && c.OnRest && c.StateId != null && c.StateId != StateMachineStateEnum.Deleted).OrderBy(p => p.Id)
                .InsertAt(new Contract { Id = 0, SignNumber = DefaultOptionValue },0);

            ViewBag.StatusOfRest =
                VocController.GetStatusOfRest()
                    .Where(s => s.Id != (long)StatusEnum.ErrorRequest && s.ForPreferential && s.IsFinal)
                    .OrderBy(p => p.Id == (long)StatusEnum.Draft ? 0 : 1)
                    .ThenBy(p => p.Name)
                    .ToList()
                    .InsertAt(new Status {Id = 0, Name = DefaultOptionValue },0);            

            ViewBag.Organizations = UnitOfWork.GetAll<Organization>().Where(c => !c.Id.IsNullOrEmpty() && !c.IsDeleted /*&& c.IsHotel*/).OrderBy(p => p.Name).ToList().InsertAt(new Organization { Id = 0, Name = DefaultOptionValue }, 0);
        }

        /// <summary>
        /// Поиск сертификата
        /// </summary>
        public ActionResult CertificateView(long id)
            {
                if (!Security.HasAnyRightsForSomeOrganization(new[] { AccessRightEnum.RequestView }))
                {
                    return RedirectToAvalibleAction();
                }
                var certificate = UnitOfWork.GetById<Certificate>(id);
                return View(certificate);
            }

    }
}
