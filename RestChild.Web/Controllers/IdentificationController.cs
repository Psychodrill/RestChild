using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Web.IdentificationService;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
    public class IdentificationController : BaseController
    {
        // GET: Identification
        public ActionResult Index(IdentificationModel model)
        {
			//"990-002-269 87"
			model = model ?? new IdentificationModel();
		    var identService = new IdentificationService.IdentificationServiceClient();

			model.DocumentTypes = identService.DocumentType_SelectAll(null)?.ToDictionary(d=>d.Id, d=>d) ?? new Dictionary<int, DocumentType>();

	        if (!string.IsNullOrWhiteSpace(model.Snils))
	        {
		        model.Declarant = new DeclarantSearchResult
		        {
			        Declarants = new[] {identService.Declarant_SelectBySnils(model.Snils, null)}
		        };
	        }
	        else
	        {
				var filter = new FilterExpression();
				var cond = new List<ConditionExpressionOfDeclarantAttribute>();
				try
				{
			        if (!string.IsNullOrWhiteSpace(model.FirstName) && !string.IsNullOrWhiteSpace(model.LastName))
			        {
				        if (!string.IsNullOrWhiteSpace(model.LastName))
				        {
					        cond.Add(new ConditionExpressionOfDeclarantAttribute
					        {
						        Attribute = DeclarantAttribute.LastName,
						        Operator = ConditionOperator.Equal,
						        Values = new[] {model.LastName}
					        });
				        }
				        if (!string.IsNullOrWhiteSpace(model.FirstName))
				        {
					        cond.Add(new ConditionExpressionOfDeclarantAttribute
					        {
						        Attribute = DeclarantAttribute.FirstName,
						        Operator = ConditionOperator.Equal,
						        Values = new[] {model.FirstName}
					        });
				        }

				        if (!string.IsNullOrWhiteSpace(model.DocumentNumber))
				        {
					        cond.Add(new ConditionExpressionOfDeclarantAttribute
					        {
						        Attribute = DeclarantAttribute.DocumentNumber,
						        Operator = ConditionOperator.Equal,
						        Values = new[] {model.DocumentNumber}
					        });
				        }

				        if (!string.IsNullOrWhiteSpace(model.MiddleName))
				        {
					        cond.Add(new ConditionExpressionOfDeclarantAttribute
					        {
						        Attribute = DeclarantAttribute.Patronymic,
						        Operator = ConditionOperator.Equal,
						        Values = new[] {model.MiddleName}
					        });
				        }

				        if (model.BithDate.HasValue)
				        {
					        cond.Add(new ConditionExpressionOfDeclarantAttribute
					        {
						        Attribute = DeclarantAttribute.BirthDate,
						        Operator = ConditionOperator.Equal,
						        Values = new[] {model.BithDate.DateTimeToXml()}
					        });
				        }


				        filter.Conditions = cond.ToArray();
				        model.Declarant =
					        identService.Declarant_Search(filter,
						        new[]
						        {new DeclarantOrder {Attribute = DeclarantAttribute.LastName, OrderType = OrderType.Ascending}},
						        new PagingInfo {PageNumber = 1, RowsPerPageCount = 300}, null);

			        }
		        }
		        catch
		        {
					cond.Add(new ConditionExpressionOfDeclarantAttribute
					{
						Attribute = DeclarantAttribute.Gender,
						Operator = ConditionOperator.Equal,
						Values = new[] { model.Male ? "Male" : "Female" }
					});
					filter.Conditions = cond.ToArray();
					try
			        {
				        model.Declarant =
					        identService.Declarant_Search(filter,
						        new[]
						        {new DeclarantOrder {Attribute = DeclarantAttribute.LastName, OrderType = OrderType.Ascending}},
						        new PagingInfo {PageNumber = 1, RowsPerPageCount = 300}, null);
			        }
			        catch (Exception ex2)
			        {
						model.Error = ex2.Message;
					}
				}
	        }

	        if (model.Declarant?.Declarants != null && model.Declarant.Declarants.Any())
		    {
				model.Documents = new Dictionary<Guid, DeclarantDocument[]>();

				foreach (var declarant in model.Declarant.Declarants)
			    {
				    if (!model.Documents.ContainsKey(declarant.Id))
				    {
					    model.Documents.Add(declarant.Id, identService.DeclarantDocument_SelectByDeclarantID(declarant.Id, null));
				    }
			    }
			}

			return View("Index", "_Layout", model);
        }
	}
}
