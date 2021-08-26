using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using DocumentFormat.OpenXml.Presentation;
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

            this.client.DefaultRequestHeaders.Add("x-ext-emp-token",
                System.Configuration.ConfigurationManager.AppSettings["SMSServiceToken"]);
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
        private ResponseMessage SendMessage(OutputMessage message)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, BuildRequest())
            {
                Content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json")
            };

            var response = client.SendAsync(request).Result;

            using (var responseStream = response.Content.ReadAsStreamAsync().Result)
            {
                using (var reader = new StreamReader(responseStream))
                {
                    var text = reader.ReadToEnd();

                    response.EnsureSuccessStatusCode();

                    var sr = JsonConvert.DeserializeObject<ResponseMessage>(text);

                    return sr;
                }
            }
        }

        /// <summary>
        ///     Отправка смс сообщения
        /// </summary>
        public ResponseMessage SendMessage(IMessage msg)
        {
            return SendMessage(new OutputMessage
            {
                Source = source,
                Destination = msg.Phone,
                Message = msg.SmsMessage,
                ExtMessageId = msg.Id.ToString()
            });
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
