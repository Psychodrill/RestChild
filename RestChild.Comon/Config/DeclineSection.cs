using System.Configuration;

namespace RestChild.Comon.Config
{
    public class DeclineSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public DeclineElements Items
        {
            get => (DeclineElements) this[""];
            set => this[""] = value;
        }
    }

    /// <summary>
    ///     элементы
    /// </summary>
    public class DeclineElements : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Decline();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var e = element as Decline;
            if (e == null)
            {
                return string.Empty;
            }

            return $"{e.DeclineCode}-{e.TypeOfRest}-{e.DeclineId}";
        }
    }

    /// <summary>
    ///     мапинг настроек
    /// </summary>
    public class Decline : ConfigurationElement
    {
        [ConfigurationProperty("code", IsRequired = true)]
        public string DeclineCode
        {
            get => (string) base["code"];
            set => base["code"] = value;
        }

        [ConfigurationProperty("declineId", IsKey = true, IsRequired = true)]
        public long? DeclineId
        {
            get
            {
                var item = base["declineId"];
                if (item is string)
                {
                    return ((string) base["declineId"]).LongParse();
                }

                return item as long?;
            }
            set => base["declineId"] = value;
        }

        [ConfigurationProperty("typeOfRest", IsKey = true, IsRequired = true)]
        public long? TypeOfRest
        {
            get
            {
                var item = base["typeOfRest"];
                if (item is string)
                {
                    return ((string) base["typeOfRest"]).LongParse();
                }

                return item as long?;
            }
            set => base["typeOfRest"] = value;
        }
    }
}
