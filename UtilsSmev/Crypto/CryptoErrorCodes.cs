using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsSmev.Crypto
{
   public static class CryptoErrorCodes
   {
      public static class Certificate
      {
         /// <summary>
         /// Электронная подпись сертификата не верна
         /// </summary>
         public const uint SignNotValid = 0x11100001;

         /// <summary>
         /// Срок действия сертификата ещё не наступил
         /// </summary>
         public const uint NotStarted = 0x11100002;

         /// <summary>
         /// Срок действия сертификата уже просрочен
         /// </summary>
         public const uint Expired = 0x11100004;

         /// <summary>
         /// Ошибка формата сертификата x.509
         /// </summary>
         public const uint FormatError = 0x11100010;

         /// <summary>
         /// Неизвестный алгоритм электронной подписи сертификата
         /// </summary>
         public const uint SignUnknownAlgorithm = 0x11100011;

         /// <summary>
         /// Отсутствуют данные открытого ключа для проверки электронной подписи сертификата
         /// </summary>
         public const uint OpenKeyNotFound = 0x11100012;

         /// <summary>
         /// Сертификат отозван
         /// </summary>
         public const uint Revoked = 0x11100018;

         /// <summary>
         /// Ошибка формирования цепочки сертификатов
         /// </summary>
         public const uint ChainError = 0x11100020;

         /// <summary>
         /// Отсутствуют действительные списки отзыва сертификата
         /// </summary>
         public const uint SosNotFound = 0x11100030;
      }

      public static class Tsp
      {
         /// <summary>
         /// Ошибка выполнения HTTP запроса по протоколу TSP
         /// </summary>
         public const uint HttpError = 0x11200100;

         /// <summary>
         /// Не задан URL адрес службы обработки TSP запросов
         /// </summary>
         public const uint UrlNotFound = 0x11200101;

         /// <summary>
         /// Ошибка формата ответных данных TimeStampResp
         /// </summary>
         public const uint AnswerFormatError = 0x11200200;

         /// <summary>
         /// Версия time-stamp token не поддерживается
         /// </summary>
         public const uint TimestampVersionNotSupported = 0x11200300;

         /// <summary>
         /// Хеш значения time-stamped данных не совпадают
         /// </summary>
         public const uint HashError = 0x11200400;

         /// <summary>
         /// Поле nonce не соответствует указанному в запросе
         /// </summary>
         public const uint NonceError = 0x11200500;

         /// <summary>
         /// Некорректный статус ответа на запрос по протоколу TSP
         /// </summary>
         public const uint AnswerStatusError = 0x11200600;
      }

      public static class Ocsp
      {
         /// <summary>
         /// Не задан URL адрес службы обработки OCSP запросов
         /// </summary>
         public const uint UrlDataNotFound = 0x11300001;

         /// <summary>
         /// Ошибка выполнения HTTP запроса по протоколу OCSP(Online Certificate Status Protocol)
         /// </summary>
         public const uint HttpError = 0x11300002;

         /// <summary>
         /// Ошибка времени отклика службы
         /// </summary>
         public const uint TimeoutError = 0x11300010;

         /// <summary>
         /// Ошибка обработки данных отклика службы
         /// </summary>
         public const uint AnswerError = 0x11300020;
      }

      public static class License
      {
         /// <summary>
         /// Ошибка инициализации лицензионного файла
         /// </summary>
         public const uint InitError = 0x11400001;

         /// <summary>
         /// Версия программного продукта не соответствует лицензируемой версии
         /// </summary>
         public const uint VersionError = 0x11400002;

         /// <summary>
         /// Лицензионный срок использования программного продукта ещё не наступил
         /// </summary>
         public const uint NotStarted = 0x11400003;

         /// <summary>
         /// Лицензионный срок использования программного продукта уже истек
         /// </summary>
         public const uint Expired = 0x11400004;

         /// <summary>
         /// Операция не доступна в соответствии с условиями лицензирования программного продукта
         /// </summary>
         public const uint MethodNotAllowed = 0x11400005;
      }

      public static class Common
      {
         /// <summary>
         /// Отсутствует криптопровайдер с поддержкой ГОСТ криптографии
         /// </summary>
         public const uint GostCryptoProviderNotFound = 0x11F00001;

         /// <summary>
         /// Не найден соответствующий ключевой контейнер
         /// </summary>
         public const uint ContainerNotFound = 0x11F00002;

         /// <summary>
         /// Не поддерживаемый тип исходных подписанных данных
         /// </summary>
         public const uint SignedDataTypeError = 0x11F00003;

         /// <summary>
         /// Исходные подписанные данные должны быть в формате CAdES-BES
         /// </summary>
         public const uint SignedDataCadesbesTypeRequired = 0x11F00004;

         /// <summary>
         /// Не найден сертификат подписчика
         /// </summary>
         public const uint SignCertificateNotFound = 0x11F00006;

         /// <summary>
         /// Исходные подписанные данные уже содержат атрибут штампа времени
         /// </summary>
         public const uint SignedDataAlreadyHasTimestamp = 0x11F00007;

         /// <summary>
         /// Не найден URL адрес OCSP службы в сертификате подписчика
         /// </summary>
         public const uint SignCertificateOcspUrlNotFound = 0x11F00008;

         /// <summary>
         /// Данные для присоединения уже содержатся в заданных подписанных данных
         /// </summary>
         public const uint SignedDataAlreadyIncludeDataToAttach = 0x11F00009;

         /// <summary>
         /// Не найден сертификат издателя (Issuer)
         /// </summary>
         public const uint IssuerCertificateNotFound = 0x11F00010;

         /// <summary>
         /// Данные отсутствуют в заданных подписанных данных
         /// </summary>
         public const uint UnsignedDataNotFound = 0x11F00011;

         /// <summary>
         /// На ключевом носителе уже есть контейнеры с идентификаторами '1' и '99'
         /// </summary>
         public const uint ContainerAlreadyHasIdsToAdd = 0x11F01000;
      }

      public static class WinApi
      {
         /// <summary>
         /// Не найден криптоалгоритм (отсутствует соответствующий криптопровайдер)
         /// </summary>
         public const uint CryptEUnknownAlgo = 0x80091002;
      }
   }
}
