using System.Xml;
using Docomotion.Shared.ComDef;

//Autofont
using Docomotion.Shared.TagPropertiesData.Data;
using Docomotion.Shared.TagPropertiesData.Defines;

namespace Docomotion.Shared.TagPropertiesData.XmlSerializerTagProperties
{
    public class DeserializeTagProperties
    {
        private XmlNode properties = null;

        public FieldPropertiesRTConfiguration FieldPropRTConfig { set; get; } = null;

        public DeserializeTagProperties(XmlNode properties)
        {
            this.properties = properties;
        }

        public AfTagProperties.TagProperties Deserialize()
        {
            bool isTextFormattingType = false;
            XmlNode formatting = null;

            foreach (XmlNode childNode in properties.ChildNodes[0].ChildNodes)
            {
                if (childNode.Name == XmlFormattingDefines.formattingType)
                {
                    if (childNode.InnerText == XmlFormattingDefines.textType)
                        isTextFormattingType = true;
                }
                else if (childNode.Name == XmlFormattingDefines.formatting)
                {
                    formatting = childNode;
                }
            }

            if (isTextFormattingType)
                return Process(formatting);

            return null;
        }

        private AfTagProperties.TagProperties Process(XmlNode formatting)
        {
            AfTagProperties.TagProperties prop = null;       
            string formattingType = string.Empty;
            XmlFormatting xmlFormatting = null;

            foreach (XmlNode childNode in formatting.ChildNodes)
            {
                if (childNode.Name == XmlFormattingDefines.formattingType)
                {
                    formattingType = childNode.InnerText;                    
                }
                else if (childNode.Name == XmlFormattingDefines.formatting)
                {
                    switch (formattingType)
                    {
                        case XmlFormattingDefines.stringFormatting:                           
                            xmlFormatting = new XmlFormattingString(childNode);                            
                            break;
                        case XmlFormattingDefines.numberFormatting:
                            xmlFormatting = new XmlFormattingNumber(childNode);
                            break;
                        case XmlFormattingDefines.dateFormatting:
                            xmlFormatting = new XmlFormattingDate(childNode, FieldPropRTConfig);
                            break;
                        case XmlFormattingDefines.dateAndTimeFormatting:
                            xmlFormatting = new XmlFormattingDateAndTime(childNode, FieldPropRTConfig);
                            break;
                        case XmlFormattingDefines.timeFormatting:
                            xmlFormatting = new XmlFormattingTime(childNode);
                            break;
                        case XmlFormattingDefines.digitToStringFormatting:
                            xmlFormatting = new XmlFormattingDigitToString(childNode);
                            break;
                    }
                }
            }

            if (formattingType == XmlFormattingDefines.noneFormatting)
            {
                xmlFormatting = new XmlFormattingNone(null);
            }

            if (xmlFormatting != null)
            {
                xmlFormatting.Init();
                prop = xmlFormatting.GetTagProperties();
            }

            return prop;
        }
    }
}
