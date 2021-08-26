using System;

namespace RestChild.Comon
{
    public abstract class CustomFormatAttribute : Attribute
    {
        public abstract string Format(object value);
    }
}
