using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestChild.Comon;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Extensions;

namespace RestChild.Booking.Logic.Extensions
{
	public static class AddonServiceExtensions
	{
		public static string Compare(this AddonServices source, AddonServices target, IUnitOfWork uw)
		{
			var res = new StringBuilder();
			if (source.Name != target.Name)
			{
				res.AppendLine(
					$"<li>Изменено название услуги старое значение:'{source.Name}', новое значение:'{target.Name}'</li>");
			}

			if (source.TypeOfServiceId != target.TypeOfServiceId)
			{
				var serviceName = uw.GetById<TypeOfService>(target.TypeOfServiceId)?.Name;
				res.AppendLine(
					$"<li>Изменен вид услуги старое значение:'{source.TypeOfService?.Name}', новое значение:'{serviceName}'</li>");
			}

			if (source.Requared != target.Requared)
			{
				res.AppendLine(
					$"<li>Изменено признак обязательности услуги старое значение:'{source.Requared.FormatEx()}', новое значение:'{target.Requared.FormatEx()}'</li>");
			}

			if (source.ForForeign != target.ForForeign)
			{
				res.AppendLine(
					$"<li>Изменено признак для зарубежных поездок услуги старое значение:'{source.ForForeign.FormatEx()}', новое значение:'{target.ForForeign.FormatEx()}'</li>");
			}

			if (source.IsActive != target.IsActive)
			{
				res.AppendLine(
					$"<li>Изменено признак активности услуги старое значение:'{source.IsActive.FormatEx()}', новое значение:'{target.IsActive.FormatEx()}'</li>");
			}

			if (source.Description != target.Description)
			{
				res.AppendLine(
					$"<li>Изменено описание услуги старое значение:'{source.Description}', новое значение:'{target.Description}'</li>");
			}

			if (source.ByDefault != target.ByDefault)
			{
				res.AppendLine(
					$"<li>Изменено признак по умолчанию услуги старое значение:'{source.ByDefault.FormatEx()}', новое значение:'{target.ByDefault.FormatEx()}'</li>");
			}

			if (source.OnlyWithRequest != target.OnlyWithRequest)
			{
				res.AppendLine(
					$"<li>Изменено признак только с размещением услуги старое значение:'{source.OnlyWithRequest.FormatEx()}', новое значение:'{target.OnlyWithRequest.FormatEx()}'</li>");
			}

			if (source.DateFrom != target.DateFrom)
			{
				res.AppendLine(
					$"<li>Изменено дата с услуги старое значение:'{source.DateFrom.FormatEx()}', новое значение:'{target.DateFrom.FormatEx()}'</li>");
			}

			if (source.DateTo != target.DateTo)
			{
				res.AppendLine(
					$"<li>Изменено дата по услуги старое значение:'{source.DateTo.FormatEx()}', новое значение:'{target.DateTo.FormatEx()}'</li>");
			}

			if (source.NeedApprove != target.NeedApprove)
			{
				res.AppendLine(
					$"<li>Изменено признак требует подтверждение старое значение:'{source.NeedApprove.FormatEx()}', новое значение:'{target.NeedApprove.FormatEx()}'</li>");
			}

			if (source.DateBookingFrom != target.DateBookingFrom)
			{
				res.AppendLine(
					$"<li>Изменено дата бронирования с старое значение:'{source.DateBookingFrom.FormatEx()}', новое значение:'{target.DateBookingFrom.FormatEx()}'</li>");
			}

			if (source.DateBookingTo != target.DateBookingTo)
			{
				res.AppendLine(
					$"<li>Изменено дата бронирования по старое значение:'{source.DateBookingTo.FormatEx()}', новое значение:'{target.DateBookingTo.FormatEx()}'</li>");
			}

			if (source.AnnouncementEvent != target.AnnouncementEvent)
			{
				res.AppendLine(
					$"<li>Изменено поле анонс мероприятия старое значение:'{source.AnnouncementEvent.FormatEx()}', новое значение:'{target.AnnouncementEvent.FormatEx()}'</li>");
			}

			if (source.ConditionsForAccompanying != target.ConditionsForAccompanying)
			{
				res.AppendLine(
					$"<li>Изменено поле условия для сопровождающих старое значение:'{source.ConditionsForAccompanying.FormatEx()}', новое значение:'{target.ConditionsForAccompanying.FormatEx()}'</li>");
			}

			if (source.SizeMin != target.SizeMin)
			{
				res.AppendLine(
					$"<li>Изменено поле размер группы с старое значение:'{source.SizeMin.FormatEx()}', новое значение:'{target.SizeMin.FormatEx()}'</li>");
			}

			if (source.SizeMax != target.SizeMax)
			{
				res.AppendLine(
					$"<li>Изменено поле размер группы по старое значение:'{source.SizeMax.FormatEx()}', новое значение:'{target.SizeMax.FormatEx()}'</li>");
			}

			if (source.DurationHour != target.DurationHour)
			{
				res.AppendLine(
					$"<li>Изменено поле продолжительность в часах старое значение:'{source.DurationHour.FormatEx()}', новое значение:'{target.DurationHour.FormatEx()}'</li>");
			}

			if (source.DurationDay != target.DurationDay)
			{
				res.AppendLine(
					$"<li>Изменено поле продолжительность в днях старое значение:'{source.DurationDay.FormatEx()}', новое значение:'{target.DurationDay.FormatEx()}'</li>");
			}

			if (source.DurationMonth != target.DurationMonth)
			{
				res.AppendLine(
					$"<li>Изменено поле продолжительность в месяцах старое значение:'{source.DurationMonth.FormatEx()}', новое значение:'{target.DurationMonth.FormatEx()}'</li>");
			}

			if (source.DurationYear != target.DurationYear)
			{
				res.AppendLine(
					$"<li>Изменено поле продолжительность в годах старое значение:'{source.DurationYear.FormatEx()}', новое значение:'{target.DurationYear.FormatEx()}'</li>");
			}

			if (source.IsGroup != target.IsGroup)
			{
				res.AppendLine(
					$"<li>Изменен признак групповая старое значение:'{source.IsGroup.FormatEx()}', новое значение:'{target.IsGroup.FormatEx()}'</li>");
			}

			if (source.Hidden != target.Hidden)
			{
				res.AppendLine(
					$"<li>Изменен признак скрытая старое значение:'{source.Hidden.FormatEx()}', новое значение:'{target.Hidden.FormatEx()}'</li>");
			}

			if (source.NotFixedDate != target.NotFixedDate)
			{
				res.AppendLine(
					$"<li>Изменен признак не фиксированные даты старое значение:'{source.NotFixedDate.FormatEx()}', новое значение:'{target.NotFixedDate.FormatEx()}'</li>");
			}

			if (source.ProcentOver != target.ProcentOver)
			{
				res.AppendLine(
					$"<li>Изменен признак процент надбавки старое значение:'{source.NotFixedDate.FormatEx()}', новое значение:'{target.NotFixedDate.FormatEx()}'</li>");
			}

			if (source.ContractId != target.ContractId)
			{
				var name = uw.GetById<Contract>(target.ContractId);
				res.AppendLine(
					$"<li>Изменен контракт/договор старое значение:'{source.Contract?.SignNumber} - {source.Contract?.SignDate.FormatEx()}', новое значение:'{name?.SignNumber} - {name?.SignDate.FormatEx()}'</li>");
			}

			if (source.PartnerId != target.PartnerId)
			{
				var name = uw.GetById<Organization>(target.PartnerId)?.Name;
				res.AppendLine(
					$"<li>Изменен партнер старое значение:'{source.Partner?.Name}', новое значение:'{name}'</li>");
			}

			if (source.ParentId != target.ParentId)
			{
				var name = uw.GetById<AddonServices>(target.ParentId)?.ToString();
				res.AppendLine(
					$"<li>Изменена родительская услуга старое значение:'{source.Parent}', новое значение:'{name}'</li>");
			}

			if (source.CuratorId != target.CuratorId)
			{
				var name = uw.GetById<Account>(target.CuratorId)?.Name;
				res.AppendLine(
					$"<li>Изменен куратор старое значение:'{source.Curator?.Name}', новое значение:'{name}'</li>");
			}


			if (source.TypeOfRoomsId != target.TypeOfRoomsId)
			{
				var name = uw.GetById<TypeOfRooms>(target.TypeOfRoomsId)?.ToString();
				res.AppendLine(
					$"<li>Изменен вид номера старое значение:'{source.TypeOfRooms}', новое значение:'{name}'</li>");
			}

			if (source.TypePriceCalculationId != target.TypePriceCalculationId)
			{
				var name = uw.GetById<TypePriceCalculation>(target.TypePriceCalculationId)?.Name;
				res.AppendLine(
					$"<li>Изменен вид цены старое значение:'{source.TypePriceCalculation?.Name}', новое значение:'{name}'</li>");
			}

			res.Compare(source, target, "Изменен рейс", v => v.TourTransport?.DirectoryFlightsId,
				v => v.TourTransport?.DirectoryFlights?.GetShortName() ?? uw.GetById<DirectoryFlights>(v.TourTransport?.DirectoryFlightsId)?.GetShortName());
			//res.Compare(source, target, "Изменено город отправления", v => v.TourTransport?.CityOfArrivalId,
			//	v => v.TourTransport?.CityOfArrival?.Name ?? uw.GetById<City>(v.TourTransport?.CityOfArrivalId)?.Name);
			//res.Compare(source, target, "Изменено место отправления", v => v.TourTransport?.PlaceOfArrival,
			//	v => v.TourTransport?.PlaceOfArrival);
			//res.Compare(source, target, "Изменено горо прибытия", v => v.TourTransport?.CityOfDepartureId,
			//	v => v.TourTransport?.CityOfDeparture?.Name ?? uw.GetById<City>(v.TourTransport?.CityOfDepartureId)?.Name);
			//res.Compare(source, target, "Изменено место прибытия", v => v.TourTransport?.PlaceOfDeparture,
			//	v => v.TourTransport?.PlaceOfDeparture);

			//if (source.InRequest != target.InRequest)
			//{
			//}

			//if (source.AddonServicesPaymentTypeId != target.AddonServicesPaymentTypeId)
			//{
			//}

			//if (source.TypeOfRestSubtypeId != target.TypeOfRestSubtypeId)
			//{
			//}

			var diff = (source.Tags?.ToList() ?? new List<Tag>()).Compare((target.Tags?.ToList() ?? new List<Tag>()),
				(tag, u) => tag?.Name ?? u.GetById<Tag>(tag?.Id)?.Name, null, uw);

			if (!string.IsNullOrWhiteSpace(diff))
			{
				res.AppendLine(
					$"<li>Изменены тэги<ul>{diff}</ul></li>");
			}

			var diffPlace = (source.Hotels?.ToList() ?? new List<Hotels>()).Compare((target.Hotels?.ToList() ?? new List<Hotels>()),
				(tag, u) => tag?.Name ?? u.GetById<Hotels>(tag?.Id)?.Name, null, uw);

			if (!string.IsNullOrWhiteSpace(diffPlace))
			{
				res.AppendLine(
					$"<li>Изменены места отдыха<ul>{diffPlace}</ul></li>");
			}

			var diffPhotos = (source.Photos?.ToList() ?? new List<AddonServicesPhoto>()).Compare((target.Photos?.ToList() ?? new List<AddonServicesPhoto>()),
				(tag, u) => tag?.FileName ?? u.GetById<AddonServicesPhoto>(tag?.Id)?.FileName, null, uw);

			if (!string.IsNullOrWhiteSpace(diffPhotos))
			{
				res.AppendLine(
					$"<li>Изменены фотографии <ul>{diffPhotos}</ul></li>");
			}

			var diffPrices = (source.Prices?.ToList() ?? new List<AddonServicesPrice>()).Compare((target.Prices?.ToList() ?? new List<AddonServicesPrice>()),
				(tag, u) => $"Стоимость услуги {tag.Price.FormatEx()} (возраст с {tag.AgeFrom.FormatEx()} по {tag.AgeTo.FormatEx()}" + (tag.DateFrom.HasValue || tag.DateTo.HasValue ? $", дата с {tag?.DateFrom.FormatEx()} по {tag?.DateTo.FormatEx()}" : "") + ")", ((s, t, u)
					=>
				{
					var sb = new StringBuilder();
					if (s?.AgeFrom != t?.AgeFrom)
					{
						sb.AppendLine(
							$"<li>Изменен возраст с старое значение:'{s?.AgeFrom}', новое значение:'{t?.AgeFrom}'</li>");
					}

					if (s?.AgeTo != t?.AgeTo)
					{
						sb.AppendLine(
							$"<li>Изменен возраст по старое значение:'{s?.AgeTo}', новое значение:'{t?.AgeTo}'</li>");
					}

					if (s?.DateFrom != t?.DateFrom)
					{
						sb.AppendLine(
							$"<li>Изменена дата с старое значение:'{s?.DateFrom.FormatEx()}', новое значение:'{t?.DateFrom.FormatEx()}'</li>");
					}

					if (s?.DateTo != t?.DateTo)
					{
						sb.AppendLine(
							$"<li>Изменена дата по старое значение:'{s?.DateTo}', новое значение:'{t?.DateTo}'</li>");
					}

					if (s?.Price != t?.Price)
					{
						sb.AppendLine(
							$"<li>Изменена стоимость значение:'{s?.Price.FormatEx()}', новое значение:'{t?.Price.FormatEx()}'</li>");
					}

					if (s?.PriceInternal != t?.PriceInternal)
					{
						sb.AppendLine(
							$"<li>Изменена себестоимость старое значение:'{s?.PriceInternal.FormatEx()}', новое значение:'{t?.PriceInternal.FormatEx()}'</li>");
					}

					return sb.ToString();
				}), uw);

			if (!string.IsNullOrWhiteSpace(diffPrices))
			{
				res.AppendLine(
					$"<li>Изменены цены услуг<ul>{diffPrices}</ul></li>");
			}

			return res.ToString();
		}
	}
}
