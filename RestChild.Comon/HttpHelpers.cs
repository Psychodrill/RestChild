using System.IO;
using System.Net.Http;
using System.Text;

namespace RestChild.Comon
{
    public static class HttpHelpers
    {
        /// <summary>
        ///     запись файла потоково
        /// </summary>
        public static Stream HttpUploadFile(string url, string file, long length, int lengthbuffer, int startPos,
            byte[] data)
        {
            HttpContent bytesContent = new ByteArrayContent(data);
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Headers.ContentType.MediaType = "multipart/form-data";
                formData.Headers.Add("X-FileName", file);
                formData.Headers.Add("Content-Range", $"bytes {startPos}-{startPos + lengthbuffer - 1}/{length}");
                var body = new StringContent("", Encoding.UTF8, "application/json");
                formData.Add(body);
                formData.Add(bytesContent, "file", "file");
                var response = client.PostAsync(url, formData).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return response.Content.ReadAsStreamAsync().Result;
            }
        }
    }
}
