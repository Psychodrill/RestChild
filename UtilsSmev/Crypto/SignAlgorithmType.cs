using System.ComponentModel;

namespace UtilsSmev.Crypto
{
   /// <summary>
   ///   Алгоритм формирования подписи
   /// </summary>
   public enum SignAlgorithmType
   {
      [AlgorithmProperties]
      [Description("CAdESBES")]
      CAdESBES = 1,

      [AlgorithmProperties]
      [Description("CMS")]
      CMS = 2,

      [AlgorithmProperties(AlgorithmProperties.NotUsed)]
      [Description("PAdES")]
      PAdES = 3,

      [AlgorithmProperties(AlgorithmProperties.NotUsed)]
      [Description("CAdESX")]
      CAdESX = 93
   }
}
