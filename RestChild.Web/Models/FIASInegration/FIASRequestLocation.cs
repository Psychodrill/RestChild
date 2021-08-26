using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestChild.Web.Models.FIASInegration
{
    /// <summary>
    /// Список ограничений / Приоритетный город поиска
    /// </summary>
    [DataContract]
    public class FIASRequestLocation
    {
        [DataMember(Name = "region")]
        public string Region { get; set; }
    }
}
