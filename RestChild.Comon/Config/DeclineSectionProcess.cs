using System.Configuration;
using System.Linq;

namespace RestChild.Comon.Config
{
    public static class DeclineSectionProcess
    {
        /// <summary>
        ///     получение причиный отказа
        /// </summary>
        public static long? GetDeclineReason(string declineCode, long? typeOfRestId)
        {
            var decline =
                ((ConfigurationManager.GetSection("declineMapping") as DeclineSection)?.Items ?? new DeclineElements())
                .OfType<Decline>().ToList();

            return decline.FirstOrDefault(d => d.DeclineCode == declineCode && d.TypeOfRest == typeOfRestId)?.DeclineId;
        }
    }
}
