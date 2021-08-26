using System.ServiceModel;
using RestChild.Comon.Dto.Booking;

namespace RestChild.Booking.Logic.Contracts
{
	[ServiceContract(Namespace = "http://aisdo.itopcase.ru/Booking")]
	public interface IBookingService
	{
		[OperationContract]
		ResultSearch GetHotels(BookingSearchRequest request);

		[OperationContract]
		BookingVariationPlacementResponse VariationPlacement(BookingVariationPlacementRequest request);

		[OperationContract]
		BookingResult MakePreBooking(BookingRequest request);

		[OperationContract]
		BookingResult MakeBooking(BookingRequest request);

		[OperationContract]
		BookingResult ReleaseBooking(BookingRequest request);

		[OperationContract]
		BookingResult ReleasePreBooking(BookingRequest request);
	}
}