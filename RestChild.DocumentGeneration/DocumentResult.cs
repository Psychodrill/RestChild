using RestChild.Comon;

namespace RestChild.DocumentGeneration
{
    /// <summary>
    ///     результат генерации документа
    /// </summary>
    public struct DocumentResult : IDocument
    {
        /// <summary>
        ///     документ
        /// </summary>
        public byte[] FileBody { get; set; }

        /// <summary>
        ///     имя
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///     тип
        /// </summary>
        public string MimeTypeShort { get; set; }

        /// <summary>
        ///     тип
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        ///     вид документа
        /// </summary>
        public long? RequestFileTypeId { get; set; }
    }
}
