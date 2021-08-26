using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RestChild.ERL.V202003
{
    public partial class person_benefit
    {
        [XmlIgnore]
        public bool citizen_pkSpecified;
    }
}
