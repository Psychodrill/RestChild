using System.Web.Mvc;

namespace RestChild.Admin.Web.Controllers
{
   /// <summary>
   /// контроллер базовой страницы
   /// </summary>
   [Authorize]
   public class HomeController : BaseController
   {
      /// <summary>
      /// пустая страница
      /// </summary>
      /// <returns></returns>
      public ActionResult Index()
      {
         return View();
      }
   }
}
