using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace RestChild.Web.Controllers
{
    /// <summary>
    /// Работа с ррестром записей на приём в ГАУК МОСГОРТУ
    /// </summary>
    [Authorize]
    public partial class BookingMosgorturReestrController : BaseController
    {
        public WebBookingMosgorturReestrController ApiController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiController.SetUnitOfWorkInRefClass(unitOfWork);
        }

        #region Working days

        /// <summary>
        ///     Рабочии дни
        /// </summary>
        [Route("BookingMosgorturReestr/WorkingDays/{Year}/{Month}/{DepartmentId}")]
        public ActionResult WorkingDays(int Year = 0, int Month = 0, int Page = 1, int DepartmentId = 2)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturWorkingDaysView))
            {
                return RedirectToAvalibleAction();
            }

            var filter = new BookingMosgorturReestrWorkingDaysFilterModel();
            if (Month > 0 && Month <= 12 && Year > 0)
            {
                filter.Date = new DateTime(Year, Month, 1);
            }
            filter.DepartmentId = DepartmentId;
            filter.PageNumber = Page;
            filter.Result = ApiController.GetDays(filter);
            ViewBag.Departments = ApiController.GetDepartments();
            return View(filter);
        }

        /// <summary>
        ///     Изменить рабочий день
        /// </summary>
        [HttpGet]
        public ActionResult DayManage(long id = 0, long DepartmentId = 2)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturWorkingDaysEdit))
            {
                return RedirectToAvalibleAction();
            }

            ViewBag.Targets = ApiController.GetDayTargets(DepartmentId);
            ViewBag.Departments = ApiController.GetDepartments();
            var result = ApiController.GetModel(id, DepartmentId);
            return View(result);
        }

        /// <summary>
        ///     Удалить рабочий день
        /// </summary>
        /// <param name="Id">Идентификатор рабочего дня</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteDay(long Id = 0)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturWorkingDaysEdit))
            {
                return RedirectToAvalibleAction();
            }
            var result = ApiController.GetModel(Id);

            if (ApiController.CheckMgtDayExistsAndIsBussy(result.Date))
            {
                result.ErrorMessage = "<ul><li>Рабочий день не может быть удален. На данный день уже есть записи на прием</li></ul>";
                ViewBag.Targets = ApiController.GetDayTargets((long)result.DepartmentId);
                return View("DayManage", result);
            }

            result.IsDeleted = true;
            var id = ApiController.SetModel(result);

            return RedirectToAction(nameof(DayManage), new { Id });
        }

        /// <summary>
        ///     Сохранить рабочий день
        /// </summary>
        [HttpPost]
        public ActionResult SaveDay(Models.MGTWorkingDayModel result)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturWorkingDaysEdit))
            {
                return RedirectToAvalibleAction();
            }

            bool haserror = false;

            if (result.Id > 0)
            {

            }
            //if (ModelState.IsValid)
            if (true)
            {
                if (result.Id < 1 && ApiController.CheckMgtDayExists(result.Date,(long)result.DepartmentId))
                {
                    result.ErrorMessage = "<ul><li>На данный день режим работы уже заведён</li></ul>";
                    haserror = true;
                }

                if (result.Windows.All(ss => ss.IsDeleted))
                {
                    ModelState.AddModelError(nameof(result.Windows), "Окна не заданы");
                    haserror = true;
                }

                if (result.Windows.Where(ss => !ss.IsDeleted).GroupBy(ss => ss.WindowNumber).Any(ss => ss.Count() > 1))
                {
                    result.ErrorMessage += "<ul><li>У нескольких окон указан одинаковый номер</li></ul>";
                    haserror = true;
                }

                for (int i = 0; i < result.Windows.Count(); i++)
                {
                    if (result.Windows[i].IsDeleted)
                        continue;

                    if (result.Windows[i].TimeIntervals.All(ss => ss.IsDeleted))
                    {
                        ModelState.AddModelError(string.Format("Windows[{0}]", i), "Окно задано не верно");
                        haserror = true;
                        continue;
                    }

                    MGTWorkingDayWindowTimeIntervalModel period = null;
                    for (int j = 0; j < result.Windows[i].TimeIntervals.Count(); j++)
                    {
                        if (result.Windows[i].TimeIntervals[j].IsDeleted)
                            continue;

                        if (string.IsNullOrWhiteSpace(result.Windows[i].TimeIntervals[j].TimeFromString) || string.IsNullOrWhiteSpace(result.Windows[i].TimeIntervals[j].TimeToString))
                        {
                            ModelState.AddModelError(string.Format("Windows[{0}]", i), "Расписание окна задано не верно");
                            haserror = true;
                            break;
                        }

                        var curentIntervalTimeFrom = result.Windows[i].TimeIntervals[j].TimeFromString.Split(':');
                        var curentIntervalTimeTo = result.Windows[i].TimeIntervals[j].TimeToString.Split(':');

                        if (curentIntervalTimeFrom.Length != 2 || curentIntervalTimeTo.Length != 2 || !int.TryParse(curentIntervalTimeFrom[0], out int tf1) || !int.TryParse(curentIntervalTimeFrom[1], out int tf2) || !int.TryParse(curentIntervalTimeTo[0], out int tt1) || !int.TryParse(curentIntervalTimeTo[1], out int tt2))
                        {
                            ModelState.AddModelError(string.Format("Windows[{0}]", i), "Расписание окна задано не верно");
                            haserror = true;
                            break;
                        }

                        var ndf = result.Date.Date.AddHours(tf1).AddMinutes(tf2);
                        var ndt = result.Date.Date.AddHours(tt1).AddMinutes(tt2);

                        if (ndf >= ndt)
                        {
                            ModelState.AddModelError(string.Format("Windows[{0}]", i), "Интервалы окна заданы не верно");
                            haserror = true;
                            break;
                        }

                        if (period != null)
                        {
                            var previosIntervalTimeTo = period.TimeToString.Split(':');
                            var ld = result.Date.Date.AddHours(Convert.ToInt32(previosIntervalTimeTo[0])).AddMinutes(Convert.ToInt32(previosIntervalTimeTo[1]));

                            if (ld > ndf)
                            {
                                ModelState.AddModelError(string.Format("Windows[{0}]", i), "Интервалы окна заданы не верно");
                                haserror = true;
                                break;
                            }
                        }

                        period = result.Windows[i].TimeIntervals[j];
                    }
                }

                if (!haserror)
                {
                    var id = ApiController.SetModel(result);
                    return RedirectToAction(nameof(DayManage), new { @Id = id });
                }
            }


            var tgs = ApiController.GetDayTargets((long)result.DepartmentId);
            ViewBag.Targets = tgs;
            UnitOfWork.SaveChanges();
            return View("DayManage", result);
        }

        /// <summary>
        ///     Добавить окно
        /// </summary>
        [HttpPost]
        public ActionResult AddWindow(int index, long Id)
        {
            var newWindow = new Models.MGTWorkingDayWindowModel();
            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Windows[{0}]", index);
            ViewBag.Benefits = ApiController.GetChildrenBenefits();

            var tgs = ApiController.GetDayTargets(Id);
            ViewBag.Targets = tgs;
            //newWindow.SelectedTargets = tgs.Select(ss => ss.Id).ToList();

            return PartialView("~/Views/BookingMosgorturReestr/EditorTemplates/Window.cshtml", newWindow);
        }

        /// <summary>
        ///     Добавить интервал
        /// </summary>
        [HttpPost]
        public ActionResult AddTimeInterval(int windowId, int intervalId)
        {
            var newTimeInterval = new Models.MGTWorkingDayWindowTimeIntervalModel() { TimeFromString = "08:00", TimeToString = "20:00", IsDeleted = false };
            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Windows[{1}].TimeIntervals[{0}]", intervalId, windowId);
            ViewBag.Benefits = ApiController.GetChildrenBenefits();
            return PartialView("~/Views/BookingMosgorturReestr/EditorTemplates/TimeInterval.cshtml", newTimeInterval);
        }

        /// <summary>
        ///     История
        /// </summary>
        [HttpGet]
        public ActionResult GetObjectHistory(long DayId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturScheduleBookingView))
            {
                return RedirectToAvalibleAction();
            }

            int i = 1;
            var res = UnitOfWork.GetSet<Domain.MGTWorkingDaysHistory>().Where(ss => ss.WorkingDayId == DayId).OrderByDescending(ss => ss.EventDate).Select(ss => new { ss.EventName, ss.EventDescription, ss.EventDate, Author = ss.Author.Login + " (" + ss.Author.Id.ToString() + ")" }).ToList().Select(ss => $"<tr><td>{i++}</td><td>{ss.EventName}</td><td>{ss.EventDescription}</td><td>{string.Format("{0:dd.MM.yyyy HH:mm}", ss.EventDate)}</td><td>{ss.Author}</td></tr>");
            return Content(string.Join(string.Empty, res));
        }

        /// <summary>
        ///     Скопировать день
        /// </summary>
        [HttpPost]
        public ActionResult TransferDay(long DayId, DateTime tdate)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturScheduleBookingView))
            {
                return RedirectToAvalibleAction();
            }

            var result = ApiController.GetModel(DayId);

            result.Date = tdate.Date;
            result.IsDeleted = false;

            if (ApiController.CheckMgtDayExists(result.Date,(long)result.DepartmentId))
            {
                result.ErrorMessage = "<ul><li>На данный день режим работы уже заведён</li></ul>";

                ViewBag.Targets = ApiController.GetDayTargets((long)result.DepartmentId);
                ViewBag.Benefits = ApiController.GetChildrenBenefits();

                return View(nameof(DayManage), result);
            }

            result.Id = -1;

            var id = ApiController.SetModel(result);
            return RedirectToAction(nameof(DayManage), new { @Id = id });
        }

        #endregion

        #region Bookings

        /// <summary>
        /// Поиск записей на приём в МГТ
        /// </summary>
        public ActionResult Search(BookingMosgorturReestrFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturScheduleBookingView))
            {
                return RedirectToAvalibleAction();
            }

            filter = filter ?? new BookingMosgorturReestrFilterModel()
            {
                DateFrom = DateTime.Now.Date,
                DateTo = DateTime.Now.Date.AddMonths(1),
            };
            filter.Result = ApiController.Get(filter);
            ViewBag.Targets = ApiController.GetTargets();
            ViewBag.Statuses = ApiController.GetStatuses();
            ViewBag.Departments = ApiController.GetDepartments();

            return View("BookingMosgorturReestrList", filter);
        }

        /// <summary>
        /// Вывод view добавления записи на приём
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertBooking(long DepartmentId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturScheduleBookingCreate))
            {
                return RedirectToAvalibleAction();
            }

            var booking = new Models.BookingMosgorturReestrBooking();
            booking.DepartmentId = DepartmentId;
            ViewBag.Targets = ApiController.GetDayTargets(DepartmentId);
            ViewBag.Benefits = ApiController.GetChildrenBenefits();
            return View("BookingManage", booking);
        }

        /// <summary>
        /// Добавить partial view (ребенок) для записи на прием
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddChild(int index)
        {
            var newChild = new Models.BookingMosgorturReestrBooking.Child();
            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Children[{0}]", index);
            ViewBag.Benefits = ApiController.GetChildrenBenefits();
            return PartialView("~/Views/Shared/EditorTemplates/Child.cshtml", newChild);
        }

        /// <summary>
        /// Сохранение записи на приём
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public ActionResult SaveBooking(Models.BookingMosgorturReestrBooking booking)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturScheduleBookingCreate))
            {
                return RedirectToAvalibleAction();
            }

            ViewBag.Targets = ApiController.GetDayTargets();
            ViewBag.Benefits = ApiController.GetChildrenBenefits();

            for (int i = 0; i < booking.Children.Count(); i++)
            {
                if (booking.Children[i].IsDeleted)
                    continue;
                if (string.IsNullOrWhiteSpace(booking.Children[i].FirstName) && string.IsNullOrWhiteSpace(booking.Children[i].LastName) && string.IsNullOrWhiteSpace(booking.Children[i].MiddleName) && string.IsNullOrWhiteSpace(booking.Children[i].BenefitType))
                {
                    booking.Children[i].IsDeleted = true;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(booking.Children[i].FirstName))
                {
                    Expression<Func<Models.BookingMosgorturReestrBooking, string>> expression = x => x.Children[i].FirstName;
                    string key = ExpressionHelper.GetExpressionText(expression);
                    ModelState.AddModelError(key, "Необходимо ввести имя");
                }
                if (string.IsNullOrWhiteSpace(booking.Children[i].LastName))
                {
                    Expression<Func<Models.BookingMosgorturReestrBooking, string>> expression = x => x.Children[i].LastName;
                    string key = ExpressionHelper.GetExpressionText(expression);
                    ModelState.AddModelError(key, "Необходимо ввести фамилию");
                }
                if (string.IsNullOrWhiteSpace(booking.Children[i].MiddleName) && !booking.Children[i].NoMiddleName)
                {
                    Expression<Func<Models.BookingMosgorturReestrBooking, string>> expression = x => x.Children[i].MiddleName;
                    string key = ExpressionHelper.GetExpressionText(expression);
                    ModelState.AddModelError(key, "Необходимо ввести отчество");
                }
                if (string.IsNullOrWhiteSpace(booking.Children[i].BenefitType))
                {
                    Expression<Func<Models.BookingMosgorturReestrBooking, string>> expression = x => x.Children[i].BenefitType;
                    string key = ExpressionHelper.GetExpressionText(expression);
                    ModelState.AddModelError(key, "Необходимо задать категорию льготы");
                }
                if (!booking.Children[i].DateOfBirth.HasValue || booking.Children[i].DateOfBirth.Value.Year < 1950)
                {
                    Expression<Func<Models.BookingMosgorturReestrBooking, DateTime?>> expression = x => x.Children[i].DateOfBirth;
                    string key = ExpressionHelper.GetExpressionText(expression);
                    ModelState.AddModelError(key, "Необходимо задать дату рождения ребёнка");
                }
            }

            if (!booking.NoMiddleName && string.IsNullOrWhiteSpace(booking.MiddleName))
            {
                ModelState.AddModelError(nameof(booking.MiddleName), "Необходимо ввести отчество");
            }

            if (ModelState.IsValid)
            {
                var error = ApiController.ValidateBookingDay(booking.Date, booking.Time, booking.SelectedTarget, booking.SlotsCount);
                if (!string.IsNullOrWhiteSpace(error))
                {
                    booking.ErrorMessage = error;
                }
                else
                {
                    try
                    {
                        var res = ApiController.SetModel(booking, false);
                        return RedirectToAction("Update", new { @Id = res });
                    }
                    catch (Exception ex)
                    {
                        booking.ErrorMessage = ex.Message;
                    }
                }
            }
            return View("BookingManage", booking);
        }

        /// <summary>
        /// Вывод view обновления записи на приём
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Update(long Id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturScheduleBookingView))
            {
                return RedirectToAvalibleAction();
            }

            var booking = ApiController.GetBooking(Id);
            ViewBag.Targets = ApiController.GetDayTargets();
            ViewBag.Benefits = ApiController.GetChildrenBenefits();
            return View("BookingManage", booking);
        }

        /// <summary>
        ///     Отменить бронь
        /// </summary>
        public ActionResult CancelBooking(long Id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturScheduleBookingCancel))
            {
                return RedirectToAvalibleAction();
            }
            ApiController.CancelBooking(Id);
            return RedirectToAction("Update", new { Id });
        }

        /// <summary>
        ///     Посетитель явился
        /// </summary>
        public ActionResult BookingVisited(long Id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturScheduleBookingCreate))
            {
                return RedirectToAvalibleAction();
            }
            ApiController.BookingVisited(Id);
            return RedirectToAction("Update", new { Id });
        }

        /// <summary>
        ///     Выбрать время
        /// </summary>
        [HttpPost]
        public ActionResult GetGrid(Models.VisitQueue.BookingVisitGridViewFilter model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturScheduleBookingCreate))
            {
                return RedirectToAvalibleAction();
            }

            var br = new Models.VisitQueue.BookingRepository(UnitOfWork, null);
            model.Times = br.GetVisitGrid(
                new Models.VisitQueue.BookingVisitGridFilter { DateFrom = model.Date, DateTo = model.Date, VisitSlotsCount = model.SlotsCount, VisitTargetId = model.SelectedTarget })
                .Where(ss => ss.Date == model.Date && model.Date < DateTime.Now.AddDays(15d))
                .SelectMany(s => s.Cells)
                .ToDictionary(x => x.TimeOfDay, x => string.Format("{0:HH:mm}", x));

            return PartialView("Partials/VisitGrid", model);
        }

        #endregion
    }
}
