using System.Web.Mvc;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     контроллер для отображения карточки ребёнка
    /// </summary>
    public class UniqueCardController : BaseController
    {
        /// <summary>
        ///     получить информацию о ребёнке
        /// </summary>
        public ActionResult Child(long id)
        {
            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.RequestView}))
            {
                return RedirectToAvalibleAction();
            }
            var child = UnitOfWork.GetById<ChildUniqe>(id);
            return View(child);
        }

        /// <summary>
        ///     получить информацию о родственнике
        /// </summary>
        public ActionResult Relative(long id)
        {
            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.RequestView}))
            {
                return RedirectToAvalibleAction();
            }

            var relative = UnitOfWork.GetById<RelativeUniqe>(id);
            return View(relative);
        }
    }
}
