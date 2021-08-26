using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestChild.Domain
{
    public partial class Pupil
    {
        /// <summary>
        /// Воспитанник отчислен
        /// </summary>
        [NotMapped]
        public bool PupilIsOut
        {
            get
            {
                return DateOut.HasValue;
            }
        }
    }
}
