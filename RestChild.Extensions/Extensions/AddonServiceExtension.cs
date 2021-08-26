using System;
using System.Collections.Generic;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Extensions.Extensions
{
    public static class AddonServiceExtension
    {
        /// <summary>
        ///     получение наименование услуги
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static string GetServiceName(this AddonServices a)
        {
            if (a == null)
            {
                return null;
            }

            return $"{a.Name}{(a.TypeOfService != null ? " (" + a.TypeOfService?.Name + ")" : string.Empty)}";
        }

        /// <summary>
        ///     получить стоимость
        /// </summary>
        /// <param name="service"></param>
        /// <param name="dateBirth"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public static AddonServicesPrice GetPrice(this AddonServices service, DateTime? dateBirth, DateTime? dateFrom,
            DateTime? dateTo)
        {
            if (service == null)
            {
                return null;
            }

            var age = dateBirth.HasValue ? dateBirth.GetAgeInYears() : 18;
            return service.GetPrice(age, dateFrom, dateTo);
        }

        /// <summary>
        ///     получить стоимость
        /// </summary>
        /// <param name="service"></param>
        /// <param name="age"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public static AddonServicesPrice GetPrice(this AddonServices service, int? age, DateTime? dateFrom,
            DateTime? dateTo)
        {
            if (service == null)
            {
                return null;
            }

            AddonServicesPrice res;
            var prices = service?.Parent?.Prices?.ToList() ?? new List<AddonServicesPrice>();
            prices.AddRange(service?.Prices ?? new List<AddonServicesPrice>());

            var pricesQ = prices.Where(a => (!a.AgeFrom.HasValue || a.AgeFrom <= age) &&
                                            (!a.AgeTo.HasValue || a.AgeTo >= age) && a.Price > 0);

            var typePriceCalculationId = service.TypePriceCalculationId ?? service?.Parent?.TypePriceCalculationId;
            if (dateFrom.HasValue && dateTo.HasValue &&
                (typePriceCalculationId == (long) TypePriceCalculationEnum.ByDay ||
                 typePriceCalculationId == (long) TypePriceCalculationEnum.ByNight))
            {
                pricesQ = pricesQ.Where(p => !p.DateFrom.HasValue || p.DateFrom <= dateTo)
                    .Where(p => !p.DateTo.HasValue || p.DateTo >= dateFrom).ToArray();

                var curentDay = dateFrom.Value;
                AddonServicesPrice prevPrice = null;
                decimal priceCalc = 0;
                decimal priceInternalCalc = 0;

                while (curentDay <= dateTo)
                {
                    var price = pricesQ.Where(p => !p.DateFrom.HasValue || p.DateFrom <= curentDay)
                        .FirstOrDefault(p => !p.DateTo.HasValue || p.DateTo >= curentDay);

                    if (price == null)
                    {
                        return null;
                    }

                    if (typePriceCalculationId == (long) TypePriceCalculationEnum.ByDay)
                    {
                        priceCalc += price.Price;
                        priceInternalCalc += price.PriceInternal;
                    }
                    else if (prevPrice != null)
                    {
                        priceCalc += price.Price;
                        priceInternalCalc += price.PriceInternal;
                    }

                    prevPrice = price;
                    curentDay = curentDay.AddDays(1);
                }

                return new AddonServicesPrice
                    {Price = priceCalc, PriceInternal = priceInternalCalc, AddonServicesId = service.Id, AgeFrom = age};
            }

            if (dateFrom.HasValue)
            {
                res = pricesQ
                    .Where(p => !p.DateFrom.HasValue || p.DateFrom <= dateFrom)
                    .FirstOrDefault(p => !p.DateTo.HasValue || p.DateTo >= dateFrom);
                return res == null ? null : new AddonServicesPrice(res);
            }

            res = pricesQ.FirstOrDefault();
            return res == null ? null : new AddonServicesPrice(res);
        }
    }
}
