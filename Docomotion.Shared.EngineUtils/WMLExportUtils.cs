using System;
using System.Xml;
using Docomotion.Shared.ComDef;

namespace Docomotion.Shared.EngineUtils
{
    public sealed class WMLWriter
    {
        private static WMLWriter instance = null;
        private static object m_LockObj = new object();
        private static WMLWriter Instance
        {
            get
            {
                if (instance == null)
                    lock (m_LockObj)
                        if (instance == null)
                            instance = new WMLWriter();

                return instance;
            }
        }

        static public XmlElement Append2Parent(XmlNode parentNode, string elementName, string elementNS)
        {
            XmlElement xmlElement = null;

            if (parentNode != null)
            {
                xmlElement = parentNode.OwnerDocument.CreateElement(elementName, elementNS);
                parentNode.AppendChild(xmlElement);
            }

            return xmlElement;
        }

        static public XmlElement Append2Parent(XmlNode parentNode, string elementName)
        {
            XmlElement xmlElement = parentNode.OwnerDocument.CreateElement(elementName);
            parentNode.AppendChild(xmlElement);

            return xmlElement;
        }

        static public XmlElement Append2Parent(XmlNode parentNode, string elementName, string elementNS, string elementValue)
        {
            XmlElement xmlElement = parentNode.OwnerDocument.CreateElement(elementName, elementNS);
            xmlElement.InnerText = elementValue;
            parentNode.AppendChild(xmlElement);

            return xmlElement;
        }

        static public XmlElement InsertAfterNodeOrPrepend2Parent(XmlNode parentNode, string elementName, string elementNS, string elementName2Search)
        {
            XmlElement xmlElement = null;

            if (parentNode != null)
            {
                xmlElement = parentNode.OwnerDocument.CreateElement(elementName, elementNS);
                
                XmlNode element2Search = null;
                if (elementName2Search != null)
                    element2Search = parentNode.SelectSingleNode(elementName2Search, WMLReader.MngrWml);
                
                if (element2Search == null)
                {
                    parentNode.PrependChild(xmlElement);
                }
                else
                {
                    parentNode.InsertAfter(xmlElement, element2Search);
                }
            }

            return xmlElement;
        }

        static public void AddAttribute(XmlNode parentNode, string attrName, string attrNS, string attrValue)
        {
            XmlAttribute attrNode = parentNode.OwnerDocument.CreateAttribute(attrName, attrNS);
            attrNode.Value = attrValue;
            parentNode.Attributes.Append(attrNode);
        }

        static public void AddAttribute(XmlNode parentNode, string attrName, string attrValue)
        {
            XmlAttribute attrNode = parentNode.OwnerDocument.CreateAttribute(attrName);
            attrNode.Value = attrValue;
            parentNode.Attributes.Append(attrNode);
        }

        static public void ST_OnOffAddValue(XmlNode parentNode, NullableBoolValue value, string wmlTag)
        {
            if (value.HasValue)
            {
                XmlNode propNode = WMLWriter.Append2Parent(parentNode, wmlTag, PrefixNS.W);
                {
                    if (!value.Get) WMLWriter.AddAttribute(propNode, AttrName.W_VAL, PrefixNS.W, ST_OnOff.ON_OFF_VAL_FALSE);
                }
            }
        }

        static public void ST_OnOffAttributeValue(XmlNode parentNode, NullableBoolValue value, string attr)
        {
            if (value.HasValue)
            {
                string attrValue = (value.Get ? ST_OnOff.ON_OFF_VAL_1 : ST_OnOff.ON_OFF_VAL_0);

                WMLWriter.AddAttribute(parentNode, attr, PrefixNS.W, attrValue);
            }
        }

        static public void ST_StringAddValue(XmlNode parentNode, NullableStringValue value, string wmlTag, string attr)
        {
            if (value.HasValue)
            {
                XmlNode propNode = WMLWriter.Append2Parent(parentNode, wmlTag, PrefixNS.W);
                {
                    WMLWriter.AddAttribute(propNode, attr, PrefixNS.W, value.Get);
                }
            }
        }

        static public void ST_StringAttributeValue(XmlNode parentNode, NullableStringValue value, string attr)
        {
            if (value.HasValue)
            {
                WMLWriter.AddAttribute(parentNode, attr, PrefixNS.W, value.Get);

            }
        }

        static public void ST_HpsMeasureAddValue(XmlNode parentNode, NullableFloatValue value, string wmlTag, string attr)
        {
            if (value.HasValue)
            {
                XmlNode propNode = WMLWriter.Append2Parent(parentNode, wmlTag, PrefixNS.W);
                {
                    WMLWriter.AddAttribute(propNode, attr, PrefixNS.W, (value.Get * 2).ToString());
                }
            }
        }

        static public void ST_TwipsMeasureAddValue(XmlNode parentNode, NullableFloatValue value, string wmlTag, string attr)
        {
            if (value.HasValue)
            {
                XmlNode propNode = WMLWriter.Append2Parent(parentNode, wmlTag, PrefixNS.W);
                {
                    WMLWriter.AddAttribute(propNode, attr, PrefixNS.W, value.Get.ToString());
                }
            }
        }

        static public void ST_TwipsMeasureAttributeValue(XmlNode parentNode, NullableFloatValue value, string attr)
        {
            if (value.HasValue)
            {
                WMLWriter.AddAttribute(parentNode, attr, PrefixNS.W, value.Get.ToString());
            }
        }

        static public void ST_DecimalNumberAddValue(XmlNode parentNode, NullableIntValue value, string wmlTag, string attr)
        {
            if (value.HasValue)
            {
                XmlNode propNode = WMLWriter.Append2Parent(parentNode, wmlTag, PrefixNS.W);
                {
                    WMLWriter.AddAttribute(propNode, attr, PrefixNS.W, value.Get.ToString());
                }
            }
        }

        static public void ST_DecimalNumberAttributeValue(XmlNode parentNode, NullableIntValue value, string attr)
        {
            if (value.HasValue)
            {
                WMLWriter.AddAttribute(parentNode, attr, PrefixNS.W, value.Get.ToString());
            }
        }

        static public void ST_EighthPointMeasureAttributeValue(XmlNode parentNode, NullableFloatValue value, string attr)
        {
            if (value.HasValue)
            {
                float wmlValue = value.Get;
                wmlValue /= 20F;
                wmlValue *= 8F;

                WMLWriter.AddAttribute(parentNode, attr, PrefixNS.W, wmlValue.ToString());
            }
        }

        static public void ST_ShortHexNumberAttributeValue(XmlNode parentNode, NullableIntValue value, string attr)
        {
            if (value.HasValue)
            {
                WMLWriter.AddAttribute(parentNode, attr, PrefixNS.W, value.Get.ToString("X"));
            }
        }

        static public int Twips2EMUS(float twipsValue)
        {
            return Convert.ToInt32(twipsValue / 20F *  FreeFormDefinitions.EMUS_DIVISOR);
        }

        static public int Degrees2AngleMeasure(float degrees)
        {
            return Convert.ToInt32(degrees * 60000F); 
        }
    }
}
