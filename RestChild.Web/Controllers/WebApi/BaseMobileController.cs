using System.Web.Http;
using RestChild.Comon;
using RestChild.Mobile.DAL;
using RestChild.Web.Logic;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    /// базовый контроллер
    /// </summary>
    public class BaseMobileController : ApiController, ILogic
    {
        /// <summary>
        ///     Unit Of Work
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        ///     Unit Of Work
        /// </summary>
        public IUnitOfWorkMobile MobileUw { get; set; }
    }
}
