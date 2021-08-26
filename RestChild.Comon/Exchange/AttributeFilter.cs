using System;
using System.Collections.Generic;
using System.Linq;

namespace RestChild.Comon.Exchange
{
    public class AttributeFiller
    {
        public AttributeFiller(string prefix)
        {
            Attributes = new List<NSIDogm.Attribute>();
            Prefix = prefix;
        }

        private string Prefix { get; }
        internal IList<NSIDogm.Attribute> Attributes { get; set; }

        /// <summary>
        ///     добавить элемент
        /// </summary>
        public void Add(string field, string type, object value)
        {
            Attributes.Add(new NSIDogm.Attribute
            {
                name = string.IsNullOrEmpty(Prefix) ? field : Prefix + "/" + field,
                type = type,
                value = value != null ? new[] {new NSIDogm.AttributeValue {Value = value.ToString()}} : null
            });
        }

        /// <summary>
        ///     добавить группу
        /// </summary>
        public void Add(string field, AttributeFiller[] attrs)
        {
            Attributes.Add(new NSIDogm.Attribute
            {
                name = string.IsNullOrEmpty(Prefix) ? field : Prefix + "/" + field,
                type = "GROUPING",
                groupValue = attrs.Select(a => new NSIDogm.GroupValue
                {
                    occurrence = 0,
                    occurrenceSpecified = true,
                    attribute = a.Array()
                }).ToArray()
            });
        }

        public void AddId(object value, DateTime? activateDate)
        {
            Attributes.Add(
                new NSIDogm.Attribute
                {
                    name = string.IsNullOrEmpty(Prefix)
                        ? "Идентификация в системах КИС"
                        : Prefix + "/Идентификация в системах КИС",
                    type = "GROUPING",
                    groupValue = new[]
                    {
                        new NSIDogm.GroupValue
                        {
                            occurrence = 0,
                            occurrenceSpecified = true,
                            attribute = new[]
                            {
                                new NSIDogm.Attribute
                                {
                                    name = "Идентификатор в системе",
                                    type = "STRING",
                                    value = value != null
                                        ? new[] {new NSIDogm.AttributeValue {Value = value.ToString()}}
                                        : null
                                },
                                new NSIDogm.Attribute
                                {
                                    name = "Дата активации",
                                    type = "DATE",
                                    value = activateDate != null
                                        ? new[]
                                        {
                                            new NSIDogm.AttributeValue
                                            {
                                                Value =
                                                    $"{activateDate:yyyy-MM-dd}T00:00:00"
                                            }
                                        }
                                        : null
                                }
                            }
                        }
                    }
                });
        }

        public NSIDogm.Attribute[] Array()
        {
            return Attributes.ToArray();
        }
    }
}
