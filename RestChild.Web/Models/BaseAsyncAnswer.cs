namespace RestChild.Web.Models
{
    /// <summary>
    ///     Модель ответа для асинхронных запросов
    /// </summary>
    public class BaseAsyncAnswer
    {
        /// <summary>
        ///     Текст ошибки
        /// </summary>
        public string ErrorText { get; set; }

        /// <summary>
        ///     Наличие ошибки
        /// </summary>
        public bool IsError => !string.IsNullOrWhiteSpace(ErrorText);
    }
}
