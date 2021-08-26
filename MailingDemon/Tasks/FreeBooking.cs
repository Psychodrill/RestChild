using System;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Dto.Booking;
using RestChild.DAL;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
	/// <summary>
	///     освобождение бронирование
	/// </summary>
	[Task]
	public class FreeBooking : BaseTask
	{
		protected override void Execute()
		{
			Logger.Info("FreeBooking started");
			using(var unitOfWork = new UnitOfWork())
			{
				var date = DateTime.Now.Date.AddDays(-2);

				var bookings = unitOfWork.GetSet<Booking>()
					.Where(b => !b.RequestId.HasValue && b.BookingDate < date && !b.Canceled)
					.OrderBy(b => b.Id)
					.Take(700)
					.ToList();

				foreach (var booking in bookings)
				{
					try
					{
						var releaseRequest = new BookingRequest
						{
							TypeOfRestId = booking.TypeOfRestId ?? 0,
							BookingGuid = booking.Code
						};

						var client = RestChild.Booking.Logic.Booking.GetServiceClient(releaseRequest);
						try
						{
							var res = client.ReleaseBooking(releaseRequest);
							if (res.IsError)
							{
								Logger.ErrorFormat("FreeBooking Не произошло снятие бронирования. BookingGuid={0}, BookingId={1}, Error={2}",
									booking.Code, booking.Id, res.ErrorMessage);
							}
						}
						finally
						{
							RestChild.Booking.Logic.Booking.CloseClient(client);
						}
					}
					catch (Exception ex)
					{
						Logger.Error(
							$"FreeBooking Не произошло снятие бронирования. BookingGuid={booking.Code}, bookingId={booking.Id}", ex);
					}
				}
			}

         Logger.Info("FreeBooking finish");
		}
	}
}
