using System;
using System.ComponentModel;

namespace RestChild.Comon
{
    [AttributeUsage(AttributeTargets.All)]
    public class CustomDisplayNameAttribute : DisplayNameAttribute
    {
        public CustomDisplayNameAttribute(string displayName)
            : base(displayName)
        {
        }
    }
}
