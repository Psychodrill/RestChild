using System.ServiceModel;
using RestChild.Comon.Dto.Booking;

namespace RestChild.Booking.Logic.Contracts
{
	/// <summary>
	/// сервис бронирования
	/// </summary>
	[ServiceContract]
	public interface IInternalBookingService : IBookingService
	{
		[OperationContract]
		void AddService(Hotel hotel);

		/// <summary>
		/// бронирование с пользователем
		/// </summary>
		[OperationContract]
		BookingResult MakeBookingWithAccount(BookingRequest request, long? accountId);

		/// <summary>
		/// получение пре бронирования.
		/// </summary>
		[OperationContract]
		BookingRequest GetPreBooking(BookingRequest request);

		/// <summary>
		/// обновить заезд
		/// </summary>
		/// <param name="tourId"></param>
		[OperationContract]
		bool UpdateTour(long tourId);

		/// <summary>
		/// обновить заявление
		/// </summary>
		[OperationContract]
		bool UpdateRequest(long tId, long rId);

		/// <summary>
		/// получить вид отдыха по заявлению
		/// </summary>
		[OperationContract]
		BaseRequest AppendTypeOfRestByRequest(BaseRequest request);
	}
}
