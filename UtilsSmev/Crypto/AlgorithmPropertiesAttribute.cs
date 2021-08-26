using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsSmev.Crypto
{
   public class AlgorithmPropertiesAttribute : Attribute
   {
      internal AlgorithmPropertiesAttribute()
      {
         Properties = AlgorithmProperties.Default;
      }

      internal AlgorithmPropertiesAttribute(AlgorithmProperties options)
      {
         Properties = options;
      }

      public AlgorithmProperties Properties { get; }
   }

   [Flags]
   public enum AlgorithmProperties
   {
      Default = 0,
      Optional = 1,
      NotUsed = 2
   }
}
