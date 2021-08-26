namespace RestChild.Comon.SMS
{
    /// <summary>
    ///     Сообщение для отправки
    /// </summary>
    public interface IMessage : IEntityBase
    {
        /// <summary>
        ///     Номер телефона
        /// </summary>
        string Phone { get; set; }

        /// <summary>
        ///     Сообщение
        /// </summary>
        string SmsMessage { get; set; }
    }
}
