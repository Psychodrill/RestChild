using System.ServiceModel;
using RestChild.Booking.Logic.Contracts;
using RestChild.Comon.Dto.Booking;

namespace RestChild.Booking.Logic.Services
{
	[ServiceBehavior(Name = "InternalBookingService", ConfigurationName = "InternalBookingService", ConcurrencyMode = ConcurrencyMode.Multiple)]
	public class InternalBookingService : IInternalBookingService
	{
		public ResultSearch GetHotels(BookingSearchRequest request)
		{
			return Booking.GetHotels(request);
		}

		public BookingVariationPlacementResponse VariationPlacement(BookingVariationPlacementRequest request)
		{
			return Booking.VariationPlacement(request);
		}

		public BookingResult MakePreBooking(BookingRequest request)
		{
			return Booking.MakePreBooking(request);
		}

		public BookingResult MakeBookingWithAccount(BookingRequest request, long? accountId)
		{
			return Booking.MakeBooking(request, accountId);
		}

		public BookingResult MakeBooking(BookingRequest request)
		{
			return Booking.MakeBooking(request, null);
		}

		public BookingResult ReleaseBooking(BookingRequest request)
		{
			return Booking.ReleaseBooking(request);
		}

		public BookingResult ReleasePreBooking(BookingRequest request)
		{
			return Booking.ReleasePreBookingPsevdo(request);
		}

		public void AddService(Hotel hotel)
		{
			Booking.AddService(hotel);
		}

		public BookingRequest GetPreBooking(BookingRequest request)
		{
			return Booking.GetPreBooking(request);
		}

		public bool UpdateTour(long tourId)
		{
			return Booking.UpdateTour(tourId);
		}

		/// <summary>
		/// обновляем элемент
		/// </summary>
		public bool UpdateRequest(long tId, long rId)
		{
			var result = false;
			if (tId != 0)
			{
				for (var i = 0; i < Settings.Default.ServersList.Count; i++)
				{
					if (Settings.Default.IndexServer != i)
					{
                  //Security.Logger.SecurityLogger.AddToLog(Domain.SecurityJournalEventType.OutSystemsInteractions, "Взаимодействия с порталом МосРу", $"Запрос по заявлению: {rId}", "", System.ServiceModel.Web.WebOperationContext.Current?.IncomingRequest?.UserAgent);

                  var client = Booking.GetServiceClient(i);
						try
						{
							client.UpdateRequest(0, rId);
						}
						finally
						{
							Booking.CloseClient(client);
						}
					}
					else
					{
						Booking.UpdateRequest(rId);
					}
				}
			}
			else
			{
				result = Booking.UpdateRequest(rId);
			}

			return result;
		}

		public BaseRequest AppendTypeOfRestByRequest(BaseRequest request)
		{
			return Booking.AppendTypeOfRestByRequest(request);
		}
	}
}
