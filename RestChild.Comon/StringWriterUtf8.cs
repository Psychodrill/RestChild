using System.IO;
using System.Text;

namespace RestChild.Comon
{
    public class StringWriterUtf8 : StringWriter
    {
        public StringWriterUtf8(StringBuilder sb)
            : base(sb)
        {
        }

        public override Encoding Encoding => Encoding.UTF8;
    }
}
