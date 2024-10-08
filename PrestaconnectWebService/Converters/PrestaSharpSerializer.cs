using RestSharp.Extensions;
using RestSharp.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrestaconnectWebService.Converters
{
    public class PrestaSharpSerializer : XmlSerializer
    {
        public PrestaSharpSerializer()
        {
        }

        public PrestaSharpSerializer(string @namespace)
            : base(@namespace)
        {
        }

        public string PrestaSharpSerialize(object obj)
        {
            XDocument xDocument = new XDocument();
            Type type = obj.GetType();
            string text = type.Name;
            SerializeAsAttribute attribute = type.GetAttribute<SerializeAsAttribute>();
            if (attribute != null)
            {
                text = attribute.TransformName(attribute.Name ?? text);
            }

            XElement xElement = new XElement(text.AsNamespaced(base.Namespace));
            if (obj is IList)
            {
                string text2 = "";
                foreach (object item in (IList)obj)
                {
                    Type type2 = item.GetType();
                    SerializeAsAttribute attribute2 = type2.GetAttribute<SerializeAsAttribute>();
                    if (attribute2 != null)
                    {
                        text2 = attribute2.TransformName(attribute2.Name ?? text);
                    }

                    if (text2 == "")
                    {
                        text2 = type2.Name;
                    }

                    XElement xElement2 = new XElement(text2);
                    Map(xElement2, item);
                    xElement.Add(xElement2);
                }
            }
            else
            {
                Map(xElement, obj);
            }

            if (base.RootElement.HasValue())
            {
                XElement content = new XElement(base.RootElement.AsNamespaced(base.Namespace), xElement);
                xDocument.Add(content);
            }
            else
            {
                xDocument.Add(xElement);
            }

            return xDocument.ToString();
        }

        private void Map(XElement root, object obj)
        {
            Type type = obj.GetType();
            IEnumerable<PropertyInfo> enumerable = from p in type.GetProperties()
                                                   let indexAttribute = p.GetAttribute<SerializeAsAttribute>()
                                                   where p.CanRead && p.CanWrite
                                                   orderby (indexAttribute != null) ? indexAttribute.Index : int.MaxValue
                                                   select p;
            SerializeAsAttribute attribute = type.GetAttribute<SerializeAsAttribute>();
            foreach (PropertyInfo item in enumerable)
            {
                string text = item.Name;
                object value = item.GetValue(obj, null);
                if (obj.GetType().FullName.Equals("Bukimedia.PrestaSharp.Entities.AuxEntities.language") && root.Name.LocalName.Equals("language") && text.Equals("id"))
                {
                    root.Add(new XAttribute(XName.Get("id"), value));
                }
                else if (obj.GetType().FullName.Equals("Bukimedia.PrestaSharp.Entities.AuxEntities.language") && root.Name.LocalName.Equals("language") && text.Equals("Value"))
                {
                    XText content = new XText((value == null) ? "" : value.ToString());
                    root.Add(content);
                }
                else
                {
                    if (value == null)
                    {
                        continue;
                    }

                    string serializedValue = GetSerializedValue(value);
                    Type propertyType = item.PropertyType;
                    bool flag = false;
                    SerializeAsAttribute attribute2 = item.GetAttribute<SerializeAsAttribute>();
                    if (attribute2 != null)
                    {
                        text = (attribute2.Name.HasValue() ? attribute2.Name : text);
                        flag = attribute2.Attribute;
                    }

                    SerializeAsAttribute attribute3 = item.GetAttribute<SerializeAsAttribute>();
                    if (attribute3 != null)
                    {
                        text = attribute3.TransformName(text);
                    }
                    else if (attribute != null)
                    {
                        text = attribute.TransformName(text);
                    }

                    XElement xElement = new XElement(text.AsNamespaced(base.Namespace));
                    if (propertyType.IsPrimitive || propertyType.IsValueType || propertyType == typeof(string))
                    {
                        if (flag)
                        {
                            root.Add(new XAttribute(text, serializedValue));
                            continue;
                        }

                        xElement.Value = serializedValue;
                    }
                    else if (value is IList)
                    {
                        string text2 = "";
                        foreach (object item2 in (IList)value)
                        {
                            if (text2 == "")
                            {
                                Type type2 = item2.GetType();
                                SerializeAsAttribute attribute4 = type2.GetAttribute<SerializeAsAttribute>();
                                text2 = ((attribute4 != null && attribute4.Name.HasValue()) ? attribute4.Name : type2.Name);
                            }

                            XElement xElement2 = new XElement(text2);
                            Map(xElement2, item2);
                            xElement.Add(xElement2);
                        }
                    }
                    else
                    {
                        Map(xElement, value);
                    }

                    root.Add(xElement);
                }
            }
        }

        private string GetSerializedValue(object obj)
        {
            object obj2 = obj;
            if (obj is DateTime && base.DateFormat.HasValue())
            {
                obj2 = ((DateTime)obj).ToString(base.DateFormat);
            }
            else if (obj is bool)
            {
                obj2 = obj.ToString().ToLowerInvariant();
            }
            else if (obj is decimal)
            {
                obj2 = obj.ToString().Replace(",", ".");
            }

            return obj2.ToString();
        }
    }
}
