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
                res += "г. Москва, " + streetName;
                res += (!string.IsNullOrWhiteSpace(streetName) ? ", " : "") + BtiAddress.ShortAddress;
            }
            else
            {
                if (BtiRegion != null)
                {
                    res += BtiRegion.Name;
                }

                res += (!string.IsNullOrWhiteSpace(res) && !string.IsNullOrWhiteSpace(Street) ? ", " : "") + Street;

                if (!string.IsNullOrWhiteSpace(House))
                {
                    res += ", д. " + House;
                }

                if (!string.IsNullOrWhiteSpace(Corpus))
                {
                    res += ", корп. " + Corpus;
                }

                if (!string.IsNullOrWhiteSpace(Stroenie))
                {
                    res += ", стр. " + Stroenie;
                }

                if (!string.IsNullOrWhiteSpace(Vladenie))
                {
                    res += ", влад. " + Vladenie;
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
