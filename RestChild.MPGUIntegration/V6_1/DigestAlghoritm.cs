using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.MPGUIntegration.V61
{
   [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3190.0")]
   [System.SerializableAttribute()]
   [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/")]
   public enum DigestAlghoritm
   {

      /// <remarks/>
      [System.Xml.Serialization.XmlEnumAttribute("GOST3411-2001")]
      GOST34112001,

      /// <remarks/>
      [System.Xml.Serialization.XmlEnumAttribute("GOST3411-2012-256")]
      GOST34112012256,

      /// <remarks/>
      [System.Xml.Serialization.XmlEnumAttribute("GOST3411-2012-512")]
      GOST34112012512,
   }
}
