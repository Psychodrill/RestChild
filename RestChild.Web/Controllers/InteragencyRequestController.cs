using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OfficeOpenXml.Style;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.OpenXmlExtensions;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
    [Authorize]
    public class InteragencyRequestController : BaseController
    {
        public WebInteragencyRequestController ApiController { get; set; }
        public WebApi.OrganizationController ApiOrganizationController { get; set; }
        public WebBtiDistrictsController ApiDistrictsController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiController.SetUnitOfWorkInRefClass(unitOfWork);
        }

        private void PrepareModel(InteragencyRequestViewModel model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            model.Status = ApiController.GetStatuses();
            model.StatusResults =
                ApiController.GetStatusResults().ToList()
                    .InsertAt(new StatusResult {Id = 0, Name = "-- Не выбрано --"});

            if (model.Data != null && model.Data.StatusInteragencyRequest == null &&
                model.Data.StatusInteragencyRequestId.HasValue)
            {
                model.Data.StatusInteragencyRequest =
                    model.Status.FirstOrDefault(s => s.Id == model.Data.StatusInteragencyRequestId);
            }
        }

        public ActionResult GetSpreadsheet(long id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var model = GetInteragencyRequestViewModel(id);

            if (model.Data.Id == 0)
            {
                return RedirectToAction("List");
            }

            var excelTable = new ExcelTable<InteragencyRequestResult>(new List<ExcelColumn<InteragencyRequestResult>>
            {
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req => req.NullSafe(r => r.Child.Request.RequestNumber).FormatEx(),
                    Title = "Номер заявления",
                    Width = 29.86,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req => req.NullSafe(r => r.Child.Request.RequestNumberMpgu).FormatEx(),
                    Title = "Номер МПГУ",
                    Width = 12.29,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req => req.NullSafe(r => r.Child.Request.DateRequest).FormatEx(),
                    Title = "Дата заявления",
                    Width = 27.71,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req =>
                        $"{req.NullSafe(r => r.Child.Request.Applicant.LastName)} {req.NullSafe(r => r.Child.Request.Applicant.FirstName)} {req.NullSafe(r => r.Child.Request.Applicant.MiddleName)}"
                            .FormatEx(),
                    Title = "Заявитель",
                    Width = 33,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req => req.NullSafe(r => r.Child.Request.Applicant.DocumentType.Name).FormatEx(),
                    Title = "Вид документа заявителя",
                    Width = 27.57,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req => req.NullSafe(r => r.Child.Request.Applicant.DocumentSeria).FormatEx(),
                    Title = "Серия",
                    Width = 8.43,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req => req.NullSafe(r => r.Child.Request.Applicant.DocumentNumber).FormatEx(),
                    Title = "Номер",
                    Width = 8.43,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req =>
                        $"{req.NullSafe(r => r.Child.LastName)} {req.NullSafe(r => r.Child.FirstName)} {req.NullSafe(r => r.Child.MiddleName)}"
                            .FormatEx(),
                    Title = "Ребенок",
                    Width = 33.57,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req => req.NullSafe(r => r.Child.DocumentType.Name).FormatEx(),
                    Title = "Вид документа ребенка",
                    Width = 48.71,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req => req.NullSafe(r => r.Child.DocumentSeria).FormatEx(),
                    Title = "Серия",
                    Width = 8.43,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req => req.NullSafe(r => r.Child.DocumentNumber).FormatEx(),
                    Title = "Номер",
                    Width = 8.43,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req => req.NullSafe(r => r.Child.DateOfBirth).FormatEx(),
                    Title = "Дата рождения",
                    Width = 26.57,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req => req.NullSafe(r => r.Child.PlaceOfBirth).FormatEx(),
                    Title = "Место рождения",
                    Width = 23.57,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req =>
                        (req.Child?.Address?.ToString())
                        .FormatEx(),
                    Title = "Адрес регистрации",
                    Width = 36.71,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req => req.NullSafe(r => r.Child.Male ? "Мужской" : "Женский").FormatEx(),
                    Title = "Пол",
                    Width = 9,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req => req.NullSafe(r => r.Child.BenefitType.Name).FormatEx(),
                    Title = "Льгота",
                    Width = 36.71,
                    WordWrap = true
                },
                new ExcelColumn<InteragencyRequestResult>
                {
                    Func = req => req.NullSafe(r => r.InteragencyRequest.Organization.Name).FormatEx(),
                    Title = "Организация",
                    Width = 36.71,
                    WordWrap = true
                }
            });

            excelTable.DataBind("Межведомственный запрос",
                model.Results.Select(r => new ExcelRow<InteragencyRequestResult> {Data = r.BuildData(), Height = 30})
                    .ToList(), ExcelBorderStyle.Thin);
            var excelStream = excelTable.CreateExcel();

            return File(excelStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Межведомственный запрос.xlsx");
        }

        public ActionResult PrintDocument(long id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var model = GetInteragencyRequestViewModel(id);

            if (model.Data.Id == 0)
            {
                return RedirectToAction("List");
            }

            using (var ms = new MemoryStream())
            {
                using (
                    var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());
                    mainPart.Document = doc;

                    var table = new Table();

                    var tblProp = new TableProperties();

                    var tableMainStyle = new TableStyle {Val = "TableGrid"};
                    var tableMainWidth = new TableWidth {Width = "13745", Type = TableWidthUnitValues.Dxa};
                    tblProp.Append(tableMainStyle, tableMainWidth);

                    table.AppendChild(tblProp);

                    var tg = new TableGrid(
                        new GridColumn {Width = "1382"},
                        new GridColumn {Width = "1382"},
                        new GridColumn {Width = "1382"},
                        new GridColumn {Width = "1382"},
                        new GridColumn {Width = "1382"},
                        new GridColumn {Width = "1382"},
                        new GridColumn {Width = "1382"}
                    );
                    table.AppendChild(tg);

                    var tr = new TableRow();
                    tr.Text("Фамилия", new TableCellProperties().Borders(new TableCellBorders().AllBorder()),
                        new ParagraphProperties().CenterAlign(), new RunProperties().SetFont().SetFontSize().Bold());
                    tr.Text("Имя", new TableCellProperties().Borders(new TableCellBorders().AllBorder()),
                        new ParagraphProperties().CenterAlign(), new RunProperties().SetFont().SetFontSize().Bold());
                    tr.Text("Отчество", new TableCellProperties().Borders(new TableCellBorders().AllBorder()),
                        new ParagraphProperties().CenterAlign(), new RunProperties().SetFont().SetFontSize().Bold());
                    tr.Text("Дата рождения", new TableCellProperties().Borders(new TableCellBorders().AllBorder()),
                        new ParagraphProperties().CenterAlign(), new RunProperties().SetFont().SetFontSize().Bold());
                    tr.Text("Документ удостоверяющий личность",
                        new TableCellProperties().Borders(new TableCellBorders().AllBorder()),
                        new ParagraphProperties().CenterAlign(), new RunProperties().SetFont().SetFontSize().Bold());
                    tr.Text("Вид льготы", new TableCellProperties().Borders(new TableCellBorders().AllBorder()),
                        new ParagraphProperties().CenterAlign(), new RunProperties().SetFont().SetFontSize().Bold());
                    tr.Text("Дата возникновения льготы",
                        new TableCellProperties().Borders(new TableCellBorders().AllBorder()),
                        new ParagraphProperties().CenterAlign(), new RunProperties().SetFont().SetFontSize().Bold());
                    table.AppendChild(tr);

                    foreach (var result in model.Results)
                    {
                        var tableRow = new TableRow();

                        tableRow.Text(result.NullSafe(r => r.Child.LastName).FormatEx("-", false),
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()),
                            new ParagraphProperties().LeftAlign(), new RunProperties().SetFont().SetFontSize());
                        tableRow.Text(result.NullSafe(r => r.Child.FirstName).FormatEx("-", false),
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()),
                            new ParagraphProperties().LeftAlign(), new RunProperties().SetFont().SetFontSize());
                        tableRow.Text(result.NullSafe(r => r.Child.MiddleName).FormatEx("-", false),
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()),
                            new ParagraphProperties().LeftAlign(), new RunProperties().SetFont().SetFontSize());
                        tableRow.Text(result.NullSafe(r => r.Child.DateOfBirth).FormatEx("dd.MM.yyyy"),
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()),
                            new ParagraphProperties().LeftAlign(), new RunProperties().SetFont().SetFontSize());
                        tableRow.Text(
                            $"{result.NullSafe(r => r.Child.DocumentType.Name).FormatEx()}, {result.NullSafe(r => r.Child.DocumentSeria).FormatEx()} {result.NullSafe(r => r.Child.DocumentNumber).FormatEx()}",
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()),
                            new ParagraphProperties().LeftAlign(), new RunProperties().SetFont().SetFontSize());
                        tableRow.Text(result.NullSafe(r => r.Child.BenefitType.Name).FormatEx(),
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()),
                            new ParagraphProperties().LeftAlign(), new RunProperties().SetFont().SetFontSize());
                        tableRow.Text(result.NullSafe(r => r.Child.BenefitDate).FormatEx("dd.MM.yyyy"),
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()),
                            new ParagraphProperties().LeftAlign(), new RunProperties().SetFont().SetFontSize());
                        table.AppendChild(tableRow);
                    }

                    var sectionProperties = new SectionProperties();
                    sectionProperties.AppendChild(new PageSize
                        {Orient = PageOrientationValues.Landscape, Width = 15840, Height = 12240});
                    doc.Body.AppendChild(table);
                    doc.Body.AppendChild(sectionProperties);
                }

                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    id + ".docx");
            }
        }

        public ActionResult Edit(long? id, bool needValidation = false, bool createSecondaryRequest = false)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var model = GetInteragencyRequestViewModel(id, createSecondaryRequest);

            if (model.Data.StatusInteragencyRequestId == (long)StatusInteragencyRequestEnum.Deleted)
            {
                return RedirectToAction(nameof(List));
            }

            ViewBag.Title = !model.Data.IsSecondaryRequest
                ? "Межведомственный запрос"
                : "Повторный межведомственный запрос";

            if (needValidation)
            {
                model.CheckModel("checkAll");
            }

            return View(model);
        }

        private InteragencyRequestViewModel GetInteragencyRequestViewModel(long? id,
            bool createSecondaryRequest = false)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var req = (id.HasValue ? ApiController.Get(id.Value) : null) ??
                      new InteragencyRequest
                      {
                          StatusInteragencyRequestId = (long) StatusInteragencyRequestEnum.Draft,
                          IsSecondaryRequest = createSecondaryRequest
                      };
            InteragencyRequestViewModel model;

            if (req.Id == 0)
            {
                var allChildren = ApiController.GetAllChildren(createSecondaryRequest);

                var result = allChildren.ToList();

                var benefitTypes =
                    result.GroupBy(c => c.BenefitType.Name)
                        .Select(
                            x =>
                                new CheckBoxViewModel<BenefitType>
                                {
                                    Data = x.Select(e => e.BenefitType).FirstOrDefault(), IsChecked = true,
                                    Description = $"(детей: {x.Count()})"
                                });

                var regions =
                    result.Select(
                            c => new
                            {
                                c.BenefitType,
                                Region = c.NullSafe(child => child.Address.BtiRegion) ??
                                         c.NullSafe(child => child.Address.BtiAddress.BtiRegion)
                            })
                        .GroupBy(r => r.Region).Select(g => new InteragencyRequestRegionModel
                            {Data = g.Key, ChildsCount = g.Count()}).ToList();

                model = new InteragencyRequestViewModel(req, allChildren, benefitTypes, regions)
                {
                    Data = {ForAllRegion = true}
                };
            }
            else
            {
                var assignedChildren = ApiController.GetRequestResults(req.Id);
                var assignedChildrenBenefitIds =
                    assignedChildren.Select(c => c.NullSafe(cc => cc.Child.BenefitTypeId)).ToList();
                var assignedBenefitTypes =
                    UnitOfWork.GetSet<BenefitType>()
                        .Where(x => assignedChildrenBenefitIds.Contains(x.Id))
                        .GroupBy(x => x.Name, b => b, (k, b) => new {BenefiType = b.FirstOrDefault()})
                        .ToList()
                        .Select(
                            x =>
                                new CheckBoxViewModel<BenefitType>
                                {
                                    Data = x.BenefiType,
                                    IsChecked =
                                        UnitOfWork.GetSet<InteragencyRequestBenefitType>()
                                            .Any(y => y.BenefitTypeId == x.BenefiType.Id &&
                                                      y.InteragencyRequestId == req.Id),
                                    Description =
                                        $"(детей: {assignedChildren.Count(c => c.NullSafe(cc => cc.Child.BenefitType.Name) == x.NullSafe(xx => xx.BenefiType.Name))})"
                                })
                        .ToList();

                var regions = new List<InteragencyRequestRegionModel>();
                if (req.BtiRegion != null)
                {
                    regions.Add(new InteragencyRequestRegionModel
                        {Data = req.BtiRegion, ChildsCount = assignedChildren.Count});
                }

                model = new InteragencyRequestViewModel(req, assignedChildren,
                    assignedBenefitTypes, regions);
            }

            PrepareModel(model);
            return model;
        }

        public ActionResult Save(InteragencyRequestViewModel request)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (Security.HasRight(AccessRightEnum.InteragencyRequestManage))
            {
                if (request == null || request.Data == null)
                {
                    return RedirectToAction("Edit", "InteragencyRequest");
                }

                var model = ApiController.Save(request);
                if (model == null)
                {
                    return RedirectToAction("Edit", "InteragencyRequest");
                }

                return RedirectToAction("Edit", "InteragencyRequest",
                    new {id = model.Id, needValidation = !request.IsValid});
            }

            return Content("Недостаточно прав для выполнения операции", "text/plain", Encoding.UTF8);
        }

        public ActionResult List(InteragencyRequestListViewModel model)
        {
            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.RequestView}))
            {
                return RedirectToAvalibleAction();
            }

            SetUnitOfWorkInRefClass(UnitOfWork);
            return View(ApiController.List(model ?? new InteragencyRequestListViewModel(null, 1, 15, 0)));
        }
    }
}
