using System.Text.RegularExpressions;

namespace RestChild.Web.Common
{
    public static class DocumentTypeHelper
    {
        /// <summary>
        ///     Является ли документ свидетельством о рождении
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public static bool IsBirthCert(long documentId)
        {
            return documentId == 22;
        }

        /// <summary>
        ///     Является ли документ свидетельством о рождении иностранного образца
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public static bool IsForeignBirthCert(long? documentId)
        {
            return documentId == 23;
        }

        /// <summary>
        ///     Является ли документ паспортом гражданина РФ
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public static bool IsPassport(long documentId)
        {
            return documentId % 100 == 1;
        }

        /// <summary>
        ///     Является ли документ заграничным паспортом
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public static bool IsForeignPassport(long documentId)
        {
            var lastDigit = documentId % 100;
            return lastDigit == 2 || lastDigit == 3 || lastDigit == 4;
        }

        /// <summary>
        ///     Является ли документ паспортом иностранного образца
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public static bool IsPassportOfForeignCountry(long? documentId)
        {
            if (!documentId.HasValue)
                return false;
            var lastDigit = documentId.Value % 100;
            return lastDigit == 5 || lastDigit == 7 || lastDigit == 10;
        }

        /// <summary>
        ///     Проверка серии пасспорта
        /// </summary>
        /// <param name="series"></param>
        /// <returns></returns>
        public static bool IsPassportSeriesValid(string series)
        {
            var regex = new Regex(@"^\d{4}$");
            return regex.IsMatch(series);
        }

        /// <summary>
        ///     Проверка номера пасспорта
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPassportNumberValid(string number)
        {
            var regex = new Regex(@"^\d{6}$");
            return regex.IsMatch(number);
        }

        /// <summary>
        ///     Проверка серии свидетельства о рождении
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsBirthCertSeriesValid(string series)
        {
            var regex = new Regex(@"^[a-zA-Z]{1,7}-[а-яА-Я]{2}$");
            return regex.IsMatch(series);
        }

        /// <summary>
        ///     Проверка номера свидетельства о рождении
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsBirthCertNumberValid(string number)
        {
            var regex = new Regex(@"^\d{6}$");
            return regex.IsMatch(number);
        }

        /// <summary>
        ///     Валидация серии паспорта
        /// </summary>
        public static bool IsDocumentSeriaValid(long documentTypeId, string series)
        {
            if (IsPassport(documentTypeId))
            {
                return IsPassportSeriesValid(series);
            }
            if (IsBirthCert(documentTypeId))
            {
                return IsBirthCertSeriesValid(series);
            }

            return true;
        }

        /// <summary>
        ///     Валидация номер документа
        /// </summary>
        public static bool IsDocumentNumberValid(long documentTypeId, string number)
        {
            if (IsPassport(documentTypeId))
            {
                return IsPassportNumberValid(number);
            }
            if (IsBirthCert(documentTypeId))
            {
                return IsBirthCertNumberValid(number);
            }

            return true;
        }

        /// <summary>
        ///     Валидация документа (в целом)
        /// </summary>
        public static bool IsDocumentValid(long documentTypeId, string series, string number)
        {
            if (IsPassport(documentTypeId))
            {
                return IsPassportSeriesValid(series) && IsPassportNumberValid(number);
            }
            if (IsBirthCert(documentTypeId))
            {
                return IsBirthCertSeriesValid(series) && IsBirthCertNumberValid(number);
            }

            return true;
        }
    }
}
