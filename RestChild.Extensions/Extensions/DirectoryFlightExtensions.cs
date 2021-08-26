using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Extensions.Extensions
{
    public static class DirectoryFlightExtensions
    {
        /// <summary>
        ///     наименование рейса
        /// </summary>
        /// <param name="directoryFlights"></param>
        /// <returns></returns>
        public static string GetName(this DirectoryFlights directoryFlights)
        {
            if (directoryFlights == null)
            {
                return string.Empty;
            }

            return
                $"{directoryFlights.TypeOfTransport?.Name} - {directoryFlights.FilightNumber} ({directoryFlights.Departure?.Name} - {directoryFlights.Arrival?.Name} {directoryFlights.TimeOfDeparture?.ToString("в HH:mm")})";
        }

        /// <summary>
        ///     наименование рейса
        /// </summary>
        /// <param name="directoryFlights"></param>
        /// <returns></returns>
        public static string GetShortName(this DirectoryFlights directoryFlights)
        {
            if (directoryFlights == null)
            {
                return string.Empty;
            }

            return
                $"{directoryFlights.FilightNumber} ({directoryFlights.TimeOfDeparture?.ToString("в HH:mm")})";
        }

        public static int GetRestManCount(this DirectoryFlights directoryFlight)
        {
            if (directoryFlight.LinkToPeoples != null)
            {
                var linksToPeople = directoryFlight.LinkToPeoples
                    .Where(i => i.NeedTicket || !i.NotNeedTicketReasonId.HasValue).ToArray();

                var childs = linksToPeople
                    .Where(i => i.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child &&
                                !i.NotNeedTicketReasonId.HasValue)
                    .Select(i => i.Child)
                    .Count(i => !i.IsDeleted);

                var councelors = linksToPeople.Where(i => i.NeedTicket).Where(i =>
                        i.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor
                        || i.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor
                        || i.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor)
                    .Count(i => i.CounselorsId.HasValue);

                var attendants = linksToPeople.Where(i =>
                        i.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant &&
                        !i.NotNeedTicketReasonId.HasValue)
                    .Count(i => i.Applicant.IsAccomp);

                var administrators = linksToPeople.Where(i => i.NeedTicket).Where(i =>
                        i.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator)
                    .Count(i => i.AdministratorTourId.HasValue);

                return childs + councelors + attendants + administrators;
            }

            return 0;
        }
    }
}
