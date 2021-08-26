using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     Каникулярные периоды
    /// </summary>
    public enum OrphanagePupilGroupVacationPeriodEnum : long
    {
        /// <summary>
        ///     Весенние каникулы
        /// </summary>
        SpringVacation = 1,

        /// <summary>
        ///     Летние каникулы
        /// </summary>
        SummerVacation = 2,

        /// <summary>
        ///     Осенние каникулы
        /// </summary>
        AutumnVacation = 3,

        /// <summary>
        ///     Зимние каникулы
        /// </summary>
        WinterVacation = 4

    }
}
