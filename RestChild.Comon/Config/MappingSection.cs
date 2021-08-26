using System.Configuration;

namespace RestChild.Comon.Config
{
    /// <summary>
    ///     секция настроек по мапингу
    /// </summary>
    public class MappingSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public MappingElements Items
        {
            get => (MappingElements) this[""];
            set => this[""] = value;
        }
    }

    /// <summary>
    ///     элементы
    /// </summary>
    public class MappingElements : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Mapping();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var e = element as Mapping;
            if (e == null)
            {
                return string.Empty;
            }

            return $"{e.SubUrl}-{e.FieldName}-{e.TypeName}";
        }
    }

    /// <summary>
    ///     мапинг настроек
    /// </summary>
    public class Mapping : ConfigurationElement
    {
        [ConfigurationProperty("subUrl", IsRequired = true)]
        public string SubUrl
        {
            get => (string) base["subUrl"];
            set => base["subUrl"] = value;
        }

        [ConfigurationProperty("subUploadUrl", IsRequired = false)]
        public string SubUploadUrl
        {
            get => (string) base["subUploadUrl"];
            set => base["subUploadUrl"] = value;
        }


        [ConfigurationProperty("fieldName", IsKey = true, IsRequired = true)]
        public string FieldName
        {
            get => (string) base["fieldName"];
            set => base["fieldName"] = value;
        }

        [ConfigurationProperty("type", IsKey = true, IsRequired = true)]
        public string TypeName
        {
            get => (string) base["type"];
            set => base["type"] = value;
        }
    }
}
