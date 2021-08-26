namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Краткая модель сотрудника учреждения социальной защиты
    /// </summary>
    public class OrphanageCollaboratorsResultListModel
    {
        /// <summary>
        ///     Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     ФИО
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Должность
        /// </summary>
        public string Position { get; set; }
    }
}
