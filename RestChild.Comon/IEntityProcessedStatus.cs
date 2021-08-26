using System;

namespace RestChild.Comon
{
    /// <summary>
    ///     Интерфейс для обработки сущностей.
    /// </summary>
    public interface IEntityProcessedStatus : IEntityBase
    {
        long? ProcessedStatusId { get; set; }

        DateTime DateChange { get; set; }
    }
}
