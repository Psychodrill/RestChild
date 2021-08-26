using System.IO;
using System.IO.Compression;
using System.Text;

namespace RestChild.Comon
{
    /// <summary>
    ///     сжатие строк
    /// </summary>
    public static class Compression
    {
        /// <summary>
        ///     запаковка
        /// </summary>
        public static byte[] CompressString(string value)
        {
            var byteArray = new byte[0];
            if (!string.IsNullOrEmpty(value))
            {
                byteArray = Encoding.UTF8.GetBytes(value);
                using (var stream = new MemoryStream())
                {
                    using (var zip = new GZipStream(stream, CompressionMode.Compress))
                    {
                        zip.Write(byteArray, 0, byteArray.Length);
                    }

                    byteArray = stream.ToArray();
                }
            }

            return byteArray;
        }

        /// <summary>
        ///     распаковка
        /// </summary>
        public static string DecompressString(byte[] value)
        {
            var resultString = string.Empty;
            if (value != null && value.Length > 0)
            {
                using (var stream = new MemoryStream(value))
                using (var zip = new GZipStream(stream, CompressionMode.Decompress))
                using (var reader = new StreamReader(zip))
                {
                    resultString = reader.ReadToEnd();
                }
            }

            return resultString;
        }
    }
}
