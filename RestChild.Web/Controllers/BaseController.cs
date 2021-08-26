using Castle.Core.Logging;
using RestChild.Comon;
using RestChild.Web.Common;
using RestChild.Web.Logic;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace RestChild.Web.Controllers
{
    public abstract class BaseController : Controller, ILogic
    {
        protected const string errorMessage = "ErrorMessage";

        public const string RedirectedKey = "CD7DB986-90C7-420D-8A7C-6918DFA063D2";

        public const string ErrorsKey = "BD45D0FD-ADA3-47CA-BF12-957F8B8C9F4D";

        public const string MessageKey = "FE1D47EF-C9C2-486A-AFE3-6306A06C3AB6";

        /// <summary>
        ///     Unit Of Work
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        public ILogger Logger { get; set; }

        protected void SetUnitOfWorkInRefClass()
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
        }

        /// <summary>
        /// получить временный файл
        /// </summary>
        /// <returns></returns>
        public static string GetTempFileName()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        }

        protected internal virtual FileStreamDeleteOnCloseResult FileAndDeleteOnClose(string fileName, string contentType, string fileDownloadName)
        {
            FileStreamDeleteOnCloseResult fileStreamResult = new FileStreamDeleteOnCloseResult(fileName, contentType)
            {
                FileDownloadName = fileDownloadName
            };
            return fileStreamResult;
        }

        /// <summary>
        ///     заполнить во всех дочерних классах
        /// </summary>
        public virtual void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        ///     возврат на страницу по умолчанию
        /// </summary>
        /// <returns></returns>
        public ActionResult RedirectToAvalibleAction()
        {
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// установка признака что была перезагрузка данных.
        /// </summary>
        public void SetRedicted()
        {
            TempData[RedirectedKey] = true;
        }

        /// <summary>
        /// установка признака что была перезагрузка данных не было.
        /// </summary>
        public void ClearRedicted()
        {
            TempData[RedirectedKey] = null;
        }

        public void SetErrors(ICollection<string> errors)
        {
            TempData[ErrorsKey] = errors?.ToList();
        }

        public void SetMessages(ICollection<string> message)
        {
            TempData[MessageKey] = message?.ToList();
        }
    }
}
