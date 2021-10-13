using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     МСП в ИС Социум
    /// </summary>
    public enum TypeOfRestERLEnum
    {
        /// <summary>
        ///     Бесплатная путевка для отдыха и оздоровления (индивидуальный выездной отдых)
        /// </summary>
        FreeRestIndividual = 1,

        /// <summary>
        ///     Бесплатная путевка для отдыха и оздоровления (совместный выездной отдых)
        /// </summary>
        FreeRestGroup = 2,

        /// <summary>
        ///     Бесплатная путевка для отдыха и оздоровления (Молодежный отдых)
        /// </summary>
        FreeRestCertYouth = 3,

        /// <summary>
        ///     Сертификат на отдых и оздоровление ребенка
        /// </summary>
        FreeRestChild = 4,

        /// <summary>
        ///     Компенсация за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления
        /// </summary>
        Compensation = 5,

        /// <summary>
        ///     Компенсация за путевку лицу из числа детей-сирот и детей, оставшихся без попечения родителей
        /// </summary>
        CompensationYouthRest = 6,

        /// <summary>
        ///     Сертификат на отдых и оздоровление ребенка и сопровождающего лица
        /// </summary>
        FreeRestChildAndApplicant = 7
    }
}
