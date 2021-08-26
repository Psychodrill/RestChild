using System;
using System.Collections.Generic;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Booking.Logic.Logic
{
	/// <summary>
	/// класс по созданию доп заявки
	/// </summary>
	public static class CreateAddonRequestService
	{
		/// <summary>
		/// создание заявки на доп услуги и места.
		/// </summary>
		public static Request CreateAddonRequestForList(this IUnitOfWork unitOfWork, Child c)
		{
			var childs = new List<Child>
			{
				new Child(c)
				{
					RequestId = null,
					YearOfCompany = null,
					Key = null,
					KeyOther = null,
					IntervalStart = null,
					IntervalEnd = null,
					TypeOfGroupCheckId = null,
					EntityId = c.Id,
					ChildListId = null,
					BoutId = null,
					Payed = false,
					PartyId = null
				}
			};

			var attendants = new List<Applicant>();

			var tour = c.ChildList?.Tour ?? c.ChildList?.LimitOnOrganization?.Tour;

			var requaredOrDefaultService =
				(tour?.Services ?? new List<AddonServices>()).Where(s => s.ByDefault || s.Requared == true).ToList();

			var addonServices =
				requaredOrDefaultService.Where(s => !s.LinkServiceId.HasValue)
										.Where(s => s.IsActive)
										.Select(
											s =>
											new RequestService
												{
													AddonServicesId = s.Id,
													AddonServices = s,
													DirectoryFlightsId = s?.TourTransport?.DirectoryFlightsId,
													DateFrom = s.DateFrom,
													DateTo = s.DateTo
												})
										.ToList();

			var persitsted = unitOfWork.AddEntity(new Request
			{
				ParentListOfChildId = c.ChildListId,
				TypeOfRestId = (long)TypeOfRestEnum.CommercicalAddonServiceList,
				StatusId = (long)StatusEnum.Draft,
				IsLast = true,
				IsDraft = true,
				YearOfRestId = tour?.YearOfRestId,
				TourId = tour?.Id,
				HotelsId = tour?.HotelsId,
				UpdateDate = DateTime.Now,
				TimeOfRestId = tour?.TimeOfRestId,
				PlaceOfRestId = tour?.Hotels?.PlaceOfRestId,
				SubjectOfRestId = tour?.SubjectOfRestId,
				Child = childs,
				Attendant = attendants,
				DateIncome = tour?.DateIncome,
				DateOutcome = tour?.DateOutcome,
				ProcentPrepaid = tour?.MinPrepaymentAmount ?? 100,
				RequestServices = addonServices
			});

			// добавляем дочернии обязательные услуги
			requaredOrDefaultService =
				requaredOrDefaultService.Where(s => !addonServices.Select(a => a.AddonServicesId).Contains(s.Id)).ToList();

			while (requaredOrDefaultService.Any(s => addonServices.Select(a => (long?)a.Id).Contains(s.LinkServiceId)))
			{
				var appendService =
					requaredOrDefaultService.Where(s => addonServices.Select(a => (long?)a.Id).Contains(s.LinkServiceId)).ToList();

				foreach (var service in appendService)
				{
					var parentService = persitsted.RequestServices.Where(s => s.AddonServicesId == service.LinkServiceId).ToList();
					foreach (var p in parentService)
					{
						var s = unitOfWork.AddEntity(new RequestService
						{
							AddonServicesId = service.Id,
							AddonServices = service,
							DirectoryFlightsId = service?.TourTransport?.DirectoryFlightsId,
							DateFrom = service.DateFrom,
							DateTo = service.DateTo,
							ParentId = p.Id,
							Parent = p
						});

						p.Childs.Add(s);
						persitsted.RequestServices.Add(s);
					}
				}

				requaredOrDefaultService =
					requaredOrDefaultService.Where(s => !appendService.Select(a => a.Id).Contains(s.Id)).ToList();
			}

			return persitsted;
		}

		/// <summary>
		/// создание заявки на доп услуги и места.
		/// </summary>
		public static Request CreateAddonRequest(this IUnitOfWork unitOfWork, Request request)
		{
			var childs =
				request.Child.Where(c => !c.IsDeleted)
						.Select(
							c =>
							new Child(c)
								{
									Id = 0,
									RequestId = null,
									YearOfCompany = null,
									Key = null,
									KeyOther = null,
									IntervalStart = null,
									IntervalEnd = null,
									TypeOfGroupCheckId = null,
									EntityId = c.Id,
									BoutId = null,
									Payed = false,
									PartyId = null
							})
						.ToList();

			var attendants = new List<Applicant>
								{
									new Applicant(request.Applicant)
										{
											RequestId = null,
											Key = null,
											KeyOther = null,
											IntervalStart = null,
											IntervalEnd = null,
											EntityId = request.ApplicantId,
											IsApplicant = true,
											DocumentTypeId =
												request.Applicant.DocumentTypeId > 10000 && request.Applicant.DocumentTypeId / 10000 != 5
													? request.Applicant.DocumentTypeId % 10000 + 50000
													: request.Applicant.DocumentTypeId,
											BoutId = null,
											Payed = false
										}
								};

			var typeOfRest = request.TypeOfRest;

			while (typeOfRest.Parent != null)
			{
				typeOfRest = typeOfRest.Parent;
			}

			if (request.TypeOfRest?.NeedAttendant ?? false)
			{
				attendants.AddRange(request.Attendant.Where(a => a.IsAccomp)
					.Select(a => new Applicant(a)
				{
					RequestId = null,
					Request = null,
					Key = null,
					Payed = false,
					BoutId = null,
					KeyOther = null,
					IntervalStart = null,
					IntervalEnd = null,
					EntityId = a.Id,
					IsApplicant = false,
					IsAccomp = true,
					Id = 0,
					DocumentTypeId =
						a.DocumentTypeId > 10000 && a.DocumentTypeId/10000 != 5
							? a.DocumentTypeId%10000 + 50000
							: a.DocumentTypeId
				}));
			}

			var requaredOrDefaultService =
				(request?.Tour?.Services ?? new List<AddonServices>()).Where(s => s.ByDefault || s.Requared == true).ToList();

			var addonServices =
				requaredOrDefaultService.Where(s => !s.LinkServiceId.HasValue && s.IsActive)
										.Select(
											s =>
											new RequestService
												{
													AddonServicesId = s.Id,
													AddonServices = s,
													DirectoryFlightsId = s?.TourTransport?.DirectoryFlightsId,
													DateFrom = s.DateFrom,
													DateTo = s.DateTo
												})
										.ToList();

			var persitsted =
				unitOfWork.AddEntity(
					new Request
						{
							ParentRequestId = request.Id,
							TypeOfRestId =
								request.TypeOfRest == null || request.TypeOfRest.NeedPlacment ? (long)TypeOfRestEnum.CommercicalAddonRequest : (long)TypeOfRestEnum.CommercicalAddonService,
							StatusId = (long)StatusEnum.Draft,
							IsLast = true,
							IsDraft = true,
							YearOfRestId = request.YearOfRestId,
							TourId = request.TourId,
							HotelsId = request.HotelsId,
							UpdateDate = DateTime.Now,
							TimeOfRestId = request.TimeOfRestId,
							PlaceOfRestId = request.PlaceOfRestId,
							SubjectOfRestId = request.SubjectOfRestId,
							Child = childs,
							Attendant = attendants,
							DateIncome = request.DateIncome ?? request.Tour?.DateIncome,
							DateOutcome = request.DateOutcome ?? request.Tour?.DateOutcome,
							ProcentPrepaid = request.Tour?.MinPrepaymentAmount ?? 100,
							RequestServices = addonServices
						});

			// добавляем дочернии обязательные услуги
			requaredOrDefaultService =
				requaredOrDefaultService.Where(s => !addonServices.Select(a => a.AddonServicesId).Contains(s.Id)).ToList();

			while (requaredOrDefaultService.Any(s => addonServices.Select(a => (long?)a.Id).Contains(s.LinkServiceId)))
			{
				var appendService =
					requaredOrDefaultService.Where(s => addonServices.Select(a => (long?)a.Id).Contains(s.LinkServiceId)).ToList();

				foreach (var service in appendService)
				{
					var parentService = persitsted.RequestServices.Where(s => s.AddonServicesId == service.LinkServiceId).ToList();
					foreach (var p in parentService)
					{
						var s = unitOfWork.AddEntity(new RequestService
						{
							AddonServicesId = service.Id,
							AddonServices = service,
							DirectoryFlightsId = service?.TourTransport?.DirectoryFlightsId,
							DateFrom = service.DateFrom,
							DateTo = service.DateTo,
							ParentId = p.Id,
							Parent = p
						});

						p.Childs.Add(s);
						persitsted.RequestServices.Add(s);
					}
				}

				requaredOrDefaultService =
					requaredOrDefaultService.Where(s => !appendService.Select(a => a.Id).Contains(s.Id)).ToList();
			}

			return persitsted;
		}
	}
}
