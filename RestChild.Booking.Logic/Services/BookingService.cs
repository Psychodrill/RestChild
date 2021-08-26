using System.ServiceModel;
using RestChild.Booking.Logic.Contracts;
using RestChild.Comon.Dto.Booking;

namespace RestChild.Booking.Logic.Services
{
    [ServiceBehavior(Name = "BookingService", ConfigurationName = "BookingService",
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    [LoggingInspector]
    public class BookingService : IBookingService
    {
        /// <summary>
        /// дополнить целью обращения
        /// </summary>
        /// <param name="request"></param>
        private static void AppendTypeOfRestByRequest(BaseRequest request)
        {
            if (string.IsNullOrEmpty(request.DocumentNumber) || request.TypeOfRestId.HasValue)
            {
                return;
            }

            Booking.AppendTypeOfRestByRequest(request);

            if (request.TypeOfRestId.HasValue || 0 == Settings.Default.IndexServer)
            {
                return;
            }

            //Security.Logger.SecurityLogger.AddToLog(Domain.SecurityJournalEventType.OutSystemsInteractions, "Взаимодействия с порталом МосРу", $"Запрос по заявлению (дополнить целью обращения): {request.DocumentNumber}", "", System.ServiceModel.Web.WebOperationContext.Current?.IncomingRequest?.UserAgent);

            var client = Booking.GetServiceClient(0);
            try
            {
                var res = client.AppendTypeOfRestByRequest(request);
                request.TypeOfRestId = res.TypeOfRestId;
            }
            finally
            {
                Booking.CloseClient(client);
            }
        }

        public ResultSearch GetHotels(BookingSearchRequest request)
        {
            AppendTypeOfRestByRequest(request);
            var index = Booking.GetServerIndexClient(request);
            request.WithBookingDate = true;
            if (index != Settings.Default.IndexServer)
            {
                var client = Booking.GetServiceClient(request);
                try
                {
                    return client.GetHotels(request);
                }
                finally
                {
                    Booking.CloseClient(client);
                }
            }

            return Booking.GetHotels(request);
        }

        public BookingVariationPlacementResponse VariationPlacement(BookingVariationPlacementRequest request)
        {
            AppendTypeOfRestByRequest(request);
            var index = Booking.GetServerIndexClient(request);
            request.WithBookingDate = true;
            if (index != Settings.Default.IndexServer)
            {
                var client = Booking.GetServiceClient(request);
                try
                {
                    return client.VariationPlacement(request);
                }
                finally
                {
                    Booking.CloseClient(client);
                }
            }

            return Booking.VariationPlacement(request);
        }

        public BookingResult MakePreBooking(BookingRequest request)
        {
            AppendTypeOfRestByRequest(request);
            var index = Booking.GetServerIndexClient(request);
            if (index != Settings.Default.IndexServer)
            {
                var client = Booking.GetServiceClient(request);
                try
                {
                    return client.MakePreBooking(request);
                }
                finally
                {
                    Booking.CloseClient(client);
                }
            }

            return Booking.MakePreBooking(request);
        }

        public BookingResult MakeBooking(BookingRequest request)
        {
            request.IsFromMPGU = true;
            AppendTypeOfRestByRequest(request);
            var index = Booking.GetServerIndexClient(request);
            if (index != Settings.Default.IndexServer)
            {
                var client = Booking.GetServiceClient(request);
                try
                {
                    return client.MakeBooking(request);
                }
                finally
                {
                    Booking.CloseClient(client);
                }
            }

            return Booking.MakeBooking(request, null);
        }

        public BookingResult ReleaseBooking(BookingRequest request)
        {
            AppendTypeOfRestByRequest(request);
            var index = Booking.GetServerIndexClient(request);
            if (index != Settings.Default.IndexServer)
            {
                var client = Booking.GetServiceClient(request);
                try
                {
                    return client.ReleaseBooking(request);
                }
                finally
                {
                    Booking.CloseClient(client);
                }
            }

            return Booking.ReleaseBooking(request);
        }

        public BookingResult ReleasePreBooking(BookingRequest request)
        {
            AppendTypeOfRestByRequest(request);
            var index = Booking.GetServerIndexClient(request);
            if (index != Settings.Default.IndexServer)
            {
                var client = Booking.GetServiceClient(request);
                try
                {
                    return client.ReleasePreBooking(request);
                }
                finally
                {
                    Booking.CloseClient(client);
                }
            }

            return Booking.ReleasePreBookingPsevdo(request);
        }
    }
}
