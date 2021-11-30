using System.ComponentModel.DataAnnotations.Schema;

namespace RestChild.Domain
{
    /// <summary>
    ///     расширение к адресу
    /// </summary>
    public partial class Address
    {
        /// <summary>
        ///     округ
        /// </summary>
        [NotMapped]
        public string District { get; set; }

        /// <summary>
        ///     район
        /// </summary>
        [NotMapped]
        public string Region { get; set; }

        /// <summary>
        ///     форматирование адреса как строку
        /// </summary>
        public override string ToString()
        {
            var res = string.Empty;
            if (!string.IsNullOrWhiteSpace(FiasId))
            {
                res += Name;
            }
            else if (BtiAddress != null)
            {
                var streetName = BtiAddress.BtiStreet != null ? BtiAddress.BtiStreet.Name : string.Empty;
                res += "Город Москва, " + streetName;
                res += (!string.IsNullOrWhiteSpace(streetName) ? ", " : "") + BtiAddress.ShortAddress;
            }
            else
            {
                res += "Город Москва, ";

                res += (!string.IsNullOrWhiteSpace(res) && !string.IsNullOrWhiteSpace(Street) ? " Улица " : "") + Street;

                if (!string.IsNullOrWhiteSpace(House))
                {
                    res += " Дом " + House;
                }

                if (!string.IsNullOrWhiteSpace(Corpus))
                {
                    res += " Корпус " + Corpus;
                }

                if (!string.IsNullOrWhiteSpace(Stroenie))
                {
                    res += " Строение " + Stroenie;
                }

                if (!string.IsNullOrWhiteSpace(Vladenie))
                {
                    res += " Владение " + Vladenie;
                }
            }

            if (!string.IsNullOrWhiteSpace(Appartment))
            {
                res += ", кв. " + Appartment;
            }

            return res;
        }
    }
}
