namespace RestChild.Comon
{
    /// <summary>
    ///     Документ для ЦСХЕД
    /// </summary>
    public interface ICshedDocument : IDocument
    {
        /// <summary>
        ///     Идентификатор ССО
        /// </summary>
        string SsoId { get; set; }

        /// <summary>
        ///     Код ЦСХЕД
        /// </summary>
        string CodeChed { get; set; }

        /// <summary>
        ///     Код АСГУФ
        /// </summary>
        string CodeAsGuf { get; set; }

        /// <summary>
        ///     Идентификатор заявления
        /// </summary>
        long RequestId { get; set; }
    }
}
