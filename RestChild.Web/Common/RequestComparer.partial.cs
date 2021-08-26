using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestChild.Comon;
using RestChild.Domain;

namespace RestChild.Web.Common
{
	public static partial class RequestComparer
	{
		public static List<RequestDiff> GetDiffForRequest(Request oldRequest, Request newRequest, IUnitOfWork unitOfWork)
		{
			var result = new List<RequestDiff>();

			if (oldRequest != null || newRequest != null)
			{
				result.Add(CompareRequestInternal(oldRequest, newRequest, unitOfWork));
				result.Add(CompareApplicantInternal(oldRequest?.Applicant, newRequest?.Applicant, unitOfWork));
				result.Add(CompareAgentInternal(oldRequest?.Agent, newRequest?.Agent));
				result.AddRange(ApplicantCollectionDiff(oldRequest, newRequest, unitOfWork));
				result.AddRange(ChildCollectionDiff(oldRequest, newRequest, unitOfWork));
				result.AddRange(AddonServiceCollectionDiff(oldRequest, newRequest));
                result.AddRange(InformationVouchersCollectionDiff(oldRequest, newRequest, unitOfWork));
            }

			return result.Where(r => r != null && (r.Items != null && r.Items.Any() || r.Messages != null && r.Messages.Any())).ToList();
		}
	}
}
