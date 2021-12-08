using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using DocumentFormat.OpenXml.Presentation;
using log4net;
using Newtonsoft.Json;

namespace RestChild.Comon.SMS
{
    /// <summary>
    ///     Клиент сервиса отправки СМС
    /// </summary>
    public class SMSSendClient : IDisposable
    {
        private readonly HttpClient client;

        private readonly string source = "MOSGORTUR";

        public SMSSendClient() : this(null, null)
        {
        }

        public SMSSendClient(string source = null) : this(null, source)
        {
        }

        public SMSSendClient(HttpClient client = null, string source = null)
        {
            this.client = client ?? new HttpClient();
            if (!string.IsNullOrWhiteSpace(source))
            {
                this.source = source;
            }
        }

        /// <summary>
        ///     Построить запрос
        /// </summary>
        private static string BuildRequest()
        {
            var builder = new UriBuilder(System.Configuration.ConfigurationManager.AppSettings["SMSServiceAddress"] ??
                                         "http://uat.emp.mos.ru/api/v2.0/communication/sms/output");
            var query = HttpUtility.ParseQueryString(builder.Query);
            builder.Query = query.ToString();

            return builder.ToString();
        }

        /// <summary>
        ///     Непосредственная отправка сообщения
        /// </summary>
        private ResponseMessage SendMessage(OutputMessage message, ILog logger = null)
        {
            var reqAddress = BuildRequest();
            var cont = JsonConvert.SerializeObject(message);

            var request = new HttpRequestMessage(HttpMethod.Post, reqAddress)
            {
                Content = new StringContent(cont, Encoding.UTF8, "application/json"),
            };
            request.Headers.Add("x-ext-emp-token", System.Configuration.ConfigurationManager.AppSettings["SMSServiceToken"]);

            logger?.Debug($"Mobile sms sending address: {reqAddress}");
            logger?.Debug($"Mobile sms sending content: {cont}");
            logger?.Debug($"Mobile sms sending request: {request}");

            var response = client.SendAsync(request).Result;

            using (var responseStream = response.Content.ReadAsStreamAsync().Result)
            {
                using (var reader = new StreamReader(responseStream))
                {
                    var text = reader.ReadToEnd();

                    response.EnsureSuccessStatusCode();

                    logger?.Debug($"Mobile sms sending response: {text}");

                    var sr = JsonConvert.DeserializeObject<ResponseMessage>(text);

                    return sr;
                }
            }
        }

        /// <summary>
        ///     Отправка смс сообщения
        /// </summary>
        public ResponseMessage SendMessage(IMessage msg, ILog logger = null)
        {
            return SendMessage(new OutputMessage
            {
                Source = source,
                Destination = msg.Phone.Replace("+", string.Empty),
                Message = msg.SmsMessage,
                ExtMessageId = msg.Id.ToString()
            }, logger);
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
