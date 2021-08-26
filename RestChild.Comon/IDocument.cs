namespace RestChild.Comon
{
    /// <summary>
    ///     Документ
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        ///     тело документа
        /// </summary>
        byte[] FileBody { get; set; }

        /// <summary>
        ///     Название файла
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        ///     тип файла (расширение)
        /// </summary>
        string MimeTypeShort { get; set; }

        /// <summary>
        ///     тип файла
        /// </summary>
        string MimeType { get; set; }

        /// <summary>
        ///     Тип документа к заявлению
        /// </summary>
        long? RequestFileTypeId { get; set; }
    }
}
