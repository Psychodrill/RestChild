using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Admin.Web.Common;
using RestChild.Comon;
using RestChild.Mobile.DAL;

namespace RestChild.Admin.Web.Controllers
{
    /// <summary>
    ///     контроллер для мобильного блока
    /// </summary>
    public class BaseMobileController : Controller, ILogic
    {
        /// <summary>
        ///     Unit Of Work
        /// </summary>
        public IUnitOfWorkMobile MobileUw { get; set; }

        /// <summary>
        ///     Unit Of Work
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        ///     возврат на страницу по умолчанию
        /// </summary>
        public ActionResult RedirectToAvailableAction()
        {
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        ///     установить ошибки
        /// </summary>
        public void SetErrors(ICollection<string> errors)
        {
            TempData[BaseController.ErrorsKey] = errors?.ToList();
        }

        /// <summary>
        ///     установка признака что была перезагрузка данных.
        /// </summary>
        public void SetRedirected()
        {
            TempData[BaseController.RedirectedKey] = true;
        }
    }
}
