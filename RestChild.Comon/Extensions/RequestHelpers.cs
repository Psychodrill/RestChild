using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RestChild.Comon.Extensions
{
    public class RequestHelpers
    {
        /// <summary>
        ///     получить адрес запроса
        /// </summary>
        public static string GetClientIpAddress(HttpRequestBase request)
        {
            try
            {
                var userHostAddress = request.UserHostAddress;
                var res = new List<string> {userHostAddress};
                var allKeys = request.ServerVariables.AllKeys;
                var xRealIpKey = allKeys.FirstOrDefault(a => a.ToUpper() == "X-REAL-IP");
                var xxForwardedForKey = allKeys.FirstOrDefault(a => a.ToUpper() == "X_FORWARDED_FOR");
                var xHttpRealIpKey = allKeys.FirstOrDefault(a => a.ToUpper() == "HTTP_X-REAL-IP");
                var xHttpForwardedForKey = allKeys.FirstOrDefault(a => a.ToUpper() == "HTTP_X_FORWARDED_FOR");

                if (!string.IsNullOrWhiteSpace(xRealIpKey))
                {
                    res.Add(request.ServerVariables[xRealIpKey]);
                }

                if (!string.IsNullOrWhiteSpace(xxForwardedForKey))
                {
                    res.Add(request.ServerVariables[xxForwardedForKey]);
                }

                if (!string.IsNullOrWhiteSpace(xHttpRealIpKey))
                {
                    res.Add(request.ServerVariables[xHttpRealIpKey]);
                }

                if (!string.IsNullOrWhiteSpace(xHttpForwardedForKey))
                {
                    res.Add(request.ServerVariables[xHttpForwardedForKey]);
                }

                return string.Join("/", res);
            }
            catch (Exception)
            {
                // Always return all zeroes for any failure (my calling code expects it)
                return "0.0.0.0";
            }
        }

        /// <summary>
        ///     проверка IP на то что он из локальной подсети
        /// </summary>
        private static bool IsPrivateIpAddress(string ipAddress)
        {
            // http://en.wikipedia.org/wiki/Private_network
            // Private IP Addresses are:
            //  24-bit block: 10.0.0.0 through 10.255.255.255
            //  20-bit block: 172.16.0.0 through 172.31.255.255
            //  16-bit block: 192.168.0.0 through 192.168.255.255
            //  Link-local addresses: 169.254.0.0 through 169.254.255.255 (http://en.wikipedia.org/wiki/Link-local_address)

            var ip = IPAddress.Parse(ipAddress);
            var octets = ip.GetAddressBytes();

            var is24BitBlock = octets[0] == 10;
            if (is24BitBlock)
            {
                return true; // Return to prevent further processing
            }

            var is20BitBlock = octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31;
            if (is20BitBlock)
            {
                return true; // Return to prevent further processing
            }

            var is16BitBlock = octets[0] == 192 && octets[1] == 168;
            if (is16BitBlock)
            {
                return true; // Return to prevent further processing
            }

            var isLinkLocalAddress = octets[0] == 169 && octets[1] == 254;
            return isLinkLocalAddress;
        }
    }
}
