namespace RestChild.Comon.Dto
{
    /// <summary>
    ///     результат проверки
    /// </summary>
    public class CheckResult
    {
        /// <summary>
        ///     есть ли ошибка
        /// </summary>
        public bool HasError { get; set; }

        /// <summary>
        ///     ошибка
        /// </summary>
        public string Error { get; set; }
    }
}
