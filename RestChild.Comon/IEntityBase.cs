using System;

namespace RestChild.Comon
{
    public interface IEntityBase
    {
        long Id { get; set; }

        /// <summary>
        ///     Последнее сохранение
        /// </summary>
        long LastUpdateTick { get; set; }

        /// <summary>
        ///     Внешний ключ
        /// </summary>
        long? Eid { get; set; }

        /// <summary>
        ///     Статус обмена по сущности
        /// </summary>
        long? EidSendStatus { get; set; }

        /// <summary>
        ///     Дата синхронизации
        /// </summary>
        DateTime? EidSyncDate { get; set; }
    }
}
