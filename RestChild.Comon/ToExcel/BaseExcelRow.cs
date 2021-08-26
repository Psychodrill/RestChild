using System.Drawing;
using System.Runtime.Serialization;

namespace RestChild.Comon.ToExcel
{
    public abstract class BaseExcelRow
    {
        public const double DefaultHeight = 15;

        [DataMember] public double Height { get; set; }

        [DataMember] public bool Bold { get; set; }

        [DataMember] public bool AutoHeight { get; set; }

        [DataMember] public Color? Color { get; set; }

        public abstract string GetClasses();

        public abstract string GetSortKeys();
    }
}
