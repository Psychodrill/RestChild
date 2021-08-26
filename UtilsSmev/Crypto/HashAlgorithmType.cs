using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsSmev.Crypto
{
   /// <summary>
   ///   Алгоритм расчёта хеша
   /// </summary>
   public enum HashAlgorithmType
   {
      // Номера не менять - сохраняются в БД.

      [AlgorithmProperties(AlgorithmProperties.NotUsed)]
      [Description("SHA1")]
      SHA1 = 0,

      [AlgorithmProperties]
      [Description("ГОСТ Р 34.11 2001")]
      GOST3411 = 1,

      [AlgorithmProperties]
      [Description("ГОСТ Р 34.11 2012")]
      GOST3411_2012 = 2,

      [AlgorithmProperties]
      [Description("ГОСТ Р 34.11 2012 усиленный")]
      GOST3411_2012_STRONG = 3,

      [AlgorithmProperties(AlgorithmProperties.Optional)]
      [Description("СТБ 34.101.01")]
      STB_34_101_01 = 4
   }
}
