using System;
using System.Xml;

namespace Docomotion.Shared.Utils
{
    public partial class FLib
    {
        public static XmlNode FindXmlChildCaseInsensitive(XmlNode xmlTag, string tagName)
        {
            XmlNode foundNode = null;

            foreach (XmlNode childNode in xmlTag.ChildNodes)
            {
                if (childNode.Name.Equals(tagName, StringComparison.InvariantCultureIgnoreCase))
                {
                    foundNode = childNode;
                    break;
                }
            }

            return foundNode;
        }

        public static XmlNode FindXmlChildByIndexCaseInsensitive(XmlNode xmlTag, string tagName, int index)
        {
            XmlNode foundNode = null;

            if (index < 0) index = 0;

            int i = 0;

            foreach (XmlNode childNode in xmlTag.ChildNodes)
            {
                if (childNode.Name.Equals(tagName, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (i == index)
                    {
                        foundNode = childNode;
                        break;
                    }

                    i++;
                }
            }

            return foundNode;
        }

        public static string GetNodeText(XmlNode paramsNode, string xpath)
        {
            string property = string.Empty;

            XmlNode propertyNode = paramsNode.SelectSingleNode(xpath);

            if (propertyNode != null)
            {
                property = propertyNode.InnerText;
            }

            return property;
        }

        public static string GetStringProperty(XmlNode paramsNode, string xpath, string defaultValue = "")
        {
            string res = GetNodeText(paramsNode, xpath);

            if (string.IsNullOrEmpty(res)) res = defaultValue;

            return res;
        }

        public static int GetIntegerProperty(XmlNode paramsNode, string xpath, int defaultValue = 0)
        {
            string property = GetNodeText(paramsNode, xpath);

            int res = 0;

            if (!int.TryParse(property, out res))
                res = defaultValue;

            return res;
        }

        public static bool GetBooleanProperty(XmlNode paramsNode, string xpath, bool defaultValue = false)
        {
            string property = GetNodeText(paramsNode, xpath);

            bool res = false;
            if (!bool.TryParse(property, out res))
                res = defaultValue;
            return res;
        }

        public static float GetFloatProperty(XmlNode paramsNode, string xpath, float defaultValue = 0F)
        {
            string property = GetNodeText(paramsNode, xpath);

            float res = 0F;
            if (!float.TryParse(property, out res)) res = defaultValue;

            return res;
        }
    }
}