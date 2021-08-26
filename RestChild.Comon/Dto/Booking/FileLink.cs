using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    /// <summary>
    ///     ссылка на файлы.
    /// </summary>
    [DataContract(Name = "fileLink")]
    public class FileLink
    {
        public FileLink()
        {
        }

        public FileLink(FileLink source)
        {
            Url = source.Url;
            Title = source.Title;
        }

        /// <summary>
        ///     ссылка на файл.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        ///     наименование файла.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }
    }
}
