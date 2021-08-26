// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Converter.cs" company="Itopcase">
//   Copyright(C)2013
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;

namespace RestChild.Comon.Services
{
    public class NullableQueryStringConverter : QueryStringConverter
    {
        public override bool CanConvert(Type type)
        {
            var valueType = Nullable.GetUnderlyingType(type);

            var isCollType = typeof(IEnumerable).IsAssignableFrom(type);

            return valueType != null && base.CanConvert(valueType) || isCollType || base.CanConvert(type);
        }

        public override object ConvertStringToValue(string parameter, Type parameterType)
        {
            var valueType = Nullable.GetUnderlyingType(parameterType);

            // Handle nullable types
            if (valueType != null)
            {
                // Define a null value as being an empty or missing (null) string passed as the query parameter value
                return string.IsNullOrEmpty(parameter) ? null : base.ConvertStringToValue(parameter, valueType);
            }

            var isCollType = typeof(IEnumerable).IsAssignableFrom(parameterType) && parameterType != typeof(string);

            if (isCollType)
            {
                var elType = parameterType.GetGenericArguments().First();
                var res = (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(elType));

                if (string.IsNullOrWhiteSpace(parameter))
                {
                    return null;
                }

                foreach (var str in parameter.Split(','))
                {
                    res.Add(ConvertStringToValue(str, elType));
                }

                return res;
            }

            return base.ConvertStringToValue(parameter, parameterType);
        }
    }
}
