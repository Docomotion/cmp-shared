using System;
using System.Xml;
using Docomotion.Shared.ComDef;
using Docomotion.Shared.TagPropertiesData.Data;
using Docomotion.Shared.TagPropertiesData.Defines;

namespace Docomotion.Shared.TagPropertiesData.XmlSerializerTagProperties
{

    interface IXmlFormatting
    {
        AfTagProperties.TagProperties GetTagProperties();
        
    }

    public abstract class XmlFormatting : IXmlFormatting
    {
        public XmlNode xmlFormmatting = null;
        public AfTagProperties.TagProperties prop = new AfTagProperties.TagProperties();

        public FieldPropertiesRTConfiguration FieldPropRTConfig { get; protected set; } = null;

        public void Init()
        {            
            prop.outputProperties = new TagOutputProperties();
        }

        public virtual AfTagProperties.TagProperties GetTagProperties()
        {
            return prop;
        }
    }

    public class XmlFormattingNone : XmlFormatting
    {
        public XmlFormattingNone(XmlNode xmlFormmatting)
        {
            this.xmlFormmatting = xmlFormmatting;
        }

        public override AfTagProperties.TagProperties GetTagProperties()
        {
            return prop;
        }
    }

    public class XmlFormattingString : XmlFormatting
    {
        public XmlFormattingString(XmlNode xmlFormmatting)
        {
            this.xmlFormmatting = xmlFormmatting;
        }

        public override AfTagProperties.TagProperties GetTagProperties()
        {
            ((TextFormatting)prop.outputProperties.formatting).formatting = new StringFormatting();

            foreach (XmlNode child in xmlFormmatting)
            {
                if (child.Name == XmlFormattingDefines.casing)
                {
                    switch (child.InnerText)
                    {
                        case XmlFormattingDefines.allUpperCase:
                            ((StringFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).casing = StringCasing.AllUpperCase;
                            break;
                        case XmlFormattingDefines.allLowerCase:
                            ((StringFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).casing = StringCasing.AllLowerCase;
                            break;
                        case XmlFormattingDefines.name:
                            ((StringFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).casing = StringCasing.Name;
                            break;
                        case XmlFormattingDefines.title:
                            ((StringFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).casing = StringCasing.Title;
                            break;
                    }
                }
            }

            return prop;
        }
    }

    public class XmlFormattingNumber : XmlFormatting
    {
        public XmlFormattingNumber(XmlNode xmlFormmatting)
        {
            this.xmlFormmatting = xmlFormmatting;
        }

        public override AfTagProperties.TagProperties GetTagProperties()
        {
            ((TextFormatting)prop.outputProperties.formatting).formatting = new NumberFormatting();

            foreach (XmlNode child in xmlFormmatting)
            {
                if (child.Name == XmlFormattingDefines.precisionPointRightFixed)
                {
                    try
                    {
                        ((NumberFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).precisionPointRightFixed = int.Parse(child.InnerText);                        
                    }
                    catch { }
                }
                else if (child.Name == XmlFormattingDefines.thousandCommaSeperator)
                {
                    ((NumberFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).thousandCommaSeperator = child.InnerText == "true";
                }
                else if (child.Name == XmlFormattingDefines.negativeFormatting)
                {
                    if (child.InnerText == XmlFormattingDefines.minusSign)
                    {
                        ((NumberFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).negativeFormatting = NegativeNumberFormatting.MinusSign;
                    }
                    else
                    {
                        ((NumberFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).negativeFormatting = NegativeNumberFormatting.Parentheses;
                    }
                }
                else if (child.Name == "decimalPointSeperator")
                {
                    try
                    {
                        ((NumberFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).decimalPointSeperator = (NumberSeparatorFormatting)Enum.Parse(typeof(NumberSeparatorFormatting), child.InnerText, true);
                    }
                    catch { }
                }
                else if (child.Name == "thousandSeperator")
                {
                    try
                    {
                        ((NumberFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).thousandSeperator = (NumberSeparatorFormatting)Enum.Parse(typeof(NumberSeparatorFormatting), child.InnerText, true);

                    }
                    catch { }
                }
            }

            return prop;
        }
    }

    public class XmlFormattingDate : XmlFormatting
    {
        public XmlFormattingDate(XmlNode xmlFormmatting, FieldPropertiesRTConfiguration fieldPropRTConfig)
        {
            this.xmlFormmatting = xmlFormmatting;
            this.FieldPropRTConfig = fieldPropRTConfig;
        }

        public override AfTagProperties.TagProperties GetTagProperties()
        {
            ((TextFormatting)prop.outputProperties.formatting).formatting = new DateFormatting();

            if (this.FieldPropRTConfig != null)
                ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).FieldPropRTConfig = FieldPropRTConfig;


            foreach (XmlNode child in xmlFormmatting)
            {
                if (child.Name == XmlFormattingDefines.dateDelimiter)
                {
                    switch (child.InnerText)
                    {
                        case XmlFormattingDefines.Dot:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateDelimiter = TimeDelimiter.Dot;
                            break;
                        case XmlFormattingDefines.Minus:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateDelimiter = TimeDelimiter.Minus;
                            break;
                        case XmlFormattingDefines.Colon:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateDelimiter = TimeDelimiter.Colon;
                            break;
                        case XmlFormattingDefines.Slash:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateDelimiter = TimeDelimiter.Slash;
                            break;
                    }
                }
                else if (child.Name == XmlFormattingDefines.dateFormat)
                {
                    switch (child.InnerText)
                    {
                        case XmlFormattingDefines.D_M_YY:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateFormat.D_M_YY;
                            break;
                        case XmlFormattingDefines.D_M_YYYY:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateFormat.D_M_YYYY;
                            break;
                        case XmlFormattingDefines.DD_MM_YY:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateFormat.DD_MM_YY;
                            break;
                        case XmlFormattingDefines.DD_MM_YYYY:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateFormat.DD_MM_YYYY;
                            break;
                        case XmlFormattingDefines.M_D_YY:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateFormat.M_D_YY;
                            break;
                        case XmlFormattingDefines.M_D_YYYY:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateFormat.M_D_YYYY;
                            break;
                        case XmlFormattingDefines.MM_DD_YY:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateFormat.MM_DD_YY;
                            break;
                        case XmlFormattingDefines.MM_DD_YYYY:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateFormat.MM_DD_YYYY;
                            break;
                        case XmlFormattingDefines.Day_DD_Month_YYYY:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateFormat.Day_DD_Month_YYYY;
                            break;
                        case XmlFormattingDefines.DD_MMM_YY:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateFormat.DD_MMM_YY;
                            break;
                        case XmlFormattingDefines.MONTH_DD_COMMA_YYYY:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateFormat.MONTH_DD_COMMA_YYYY;
                            break;
                    }
                }
                else if (child.Name == XmlFormattingDefines.calanderLanguage)
                {
                    switch (child.InnerText)
                    {
                        case XmlFormattingDefines.calanderLanguageHEB:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).calanderLanguage = CalanderLanguage.HEB;
                            break;
                        case XmlFormattingDefines.calanderLanguageENG:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).calanderLanguage = CalanderLanguage.ENG;
                            break;
                    }
                }
                else if (child.Name == XmlFormattingDefines.calanderType)
                {
                    switch (child.InnerText)
                    {
                        case XmlFormattingDefines.calanderTypeGeogian:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).calanderType = CalanderType.Gregorian;
                            break;
                        case XmlFormattingDefines.calanderTypeLunar:
                            ((DateFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).calanderType = CalanderType.Lunar;
                            break;
                    }
                }
            }

            return prop;
        }
    }

    public class XmlFormattingDateAndTime : XmlFormatting
    {
        public XmlFormattingDateAndTime(XmlNode xmlFormmatting, FieldPropertiesRTConfiguration fieldPropRTConfig)
        {
            this.xmlFormmatting = xmlFormmatting;
            this.FieldPropRTConfig = fieldPropRTConfig;
        }

        public override AfTagProperties.TagProperties GetTagProperties()
        {
            ((TextFormatting)prop.outputProperties.formatting).formatting = new DateAndTimeFormatting();

            if (this.FieldPropRTConfig != null)
                ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).FieldPropRTConfig = FieldPropRTConfig;

            foreach (XmlNode child in xmlFormmatting)
            {
                if (child.Name == XmlFormattingDefines.dateDelimiter)
                {
                    switch (child.InnerText)
                    {
                        case XmlFormattingDefines.Dot:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateDelimiter = TimeDelimiter.Dot;
                            break;
                        case XmlFormattingDefines.Minus:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateDelimiter = TimeDelimiter.Minus;
                            break;
                        case XmlFormattingDefines.Colon:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateDelimiter = TimeDelimiter.Colon;
                            break;
                        case XmlFormattingDefines.Slash:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateDelimiter = TimeDelimiter.Slash;
                            break;
                    }
                }
                else if (child.Name == XmlFormattingDefines.dateFormat)
                {
                    switch (child.InnerText)
                    {
                        #region Date only

                        case XmlFormattingDefines.D_M_YY:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateAndTimeFormat.D_M_YY;
                            break;
                        case XmlFormattingDefines.D_M_YYYY:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateAndTimeFormat.D_M_YYYY;
                            break;
                        case XmlFormattingDefines.DD_MM_YY:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateAndTimeFormat.DD_MM_YY;
                            break;
                        case XmlFormattingDefines.DD_MM_YYYY:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateAndTimeFormat.DD_MM_YYYY;
                            break;
                        case XmlFormattingDefines.M_D_YY:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateAndTimeFormat.M_D_YY;
                            break;
                        case XmlFormattingDefines.M_D_YYYY:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateAndTimeFormat.M_D_YYYY;
                            break;
                        case XmlFormattingDefines.MM_DD_YY:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateAndTimeFormat.MM_DD_YY;
                            break;
                        case XmlFormattingDefines.MM_DD_YYYY:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateAndTimeFormat.MM_DD_YYYY;
                            break;
                        case XmlFormattingDefines.Day_DD_Month_YYYY:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateAndTimeFormat.Day_DD_Month_YYYY;
                            break;
                        case XmlFormattingDefines.DD_MMM_YY:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateAndTimeFormat.DD_MMM_YY;
                            break;
                        case XmlFormattingDefines.MONTH_DD_COMMA_YYYY:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).dateFormat = DateAndTimeFormat.MONTH_DD_COMMA_YYYY;
                            break;

                        #endregion

                        #region Date and time

                        default:
                            GetDateAndTimeMask(child.InnerText, ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting));
                            break;
                        
                        #endregion
                    }
                }
                else if (child.Name == XmlFormattingDefines.calanderLanguage)
                {
                    switch (child.InnerText)
                    {
                        case XmlFormattingDefines.calanderLanguageHEB:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).calanderLanguage = CalanderLanguage.HEB;
                            break;
                        case XmlFormattingDefines.calanderLanguageENG:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).calanderLanguage = CalanderLanguage.ENG;
                            break;
                    }
                }
                else if (child.Name == XmlFormattingDefines.calanderType)
                {
                    switch (child.InnerText)
                    {
                        case XmlFormattingDefines.calanderTypeGeogian:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).calanderType = CalanderType.Gregorian;
                            break;
                        case XmlFormattingDefines.calanderTypeLunar:
                            ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).calanderType = CalanderType.Lunar;
                            break;
                    }
                }
                else if (child.Name == XmlFormattingDefines.UseTimeZone)
                {
                    ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).useTimeZone = child.InnerText == "true" ? true : false;
                    //< useTimeZone > false </ useTimeZone >
               }
                else if (child.Name == XmlFormattingDefines.TimeZoneID)
                {
                    ((DateAndTimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).timeZoneID = child.InnerText;
                }
            }

            return prop;
        }

        private void GetDateAndTimeMask(string xmlFormating, DateAndTimeFormatting dateAndTimeFormatting)
        {
            dateAndTimeFormatting.WithTime = true;

            switch (xmlFormating)
            {
                case XmlFormattingDefines.DD_MM_YYYY_HH_MM_PM:
                    dateAndTimeFormatting.dateFormat = DateAndTimeFormat.DD_MM_YYYY_HH_MM_PM;
                    break;
                case XmlFormattingDefines.DD_MM_YYYY_HH_MM_24:
                    dateAndTimeFormatting.dateFormat = DateAndTimeFormat.DD_MM_YYYY_HH_MM_24;
                    break;
                case XmlFormattingDefines.MM_DD_YYYY_HH_MM_PM:
                    dateAndTimeFormatting.dateFormat = DateAndTimeFormat.MM_DD_YYYY_HH_MM_PM;
                    break;
                case XmlFormattingDefines.MM_DD_YYYY_HH_MM_24:
                    dateAndTimeFormatting.dateFormat = DateAndTimeFormat.MM_DD_YYYY_HH_MM_24;
                    break;
                case XmlFormattingDefines.DD_MM_YY_HH_MM_PM:
                    dateAndTimeFormatting.dateFormat = DateAndTimeFormat.DD_MM_YY_HH_MM_PM;
                    break;
                case XmlFormattingDefines.DD_MM_YY_HH_MM_24:
                    dateAndTimeFormatting.dateFormat = DateAndTimeFormat.DD_MM_YY_HH_MM_24;
                    break;
                case XmlFormattingDefines.MM_DD_YY_HH_MM_PM:
                    dateAndTimeFormatting.dateFormat = DateAndTimeFormat.MM_DD_YY_HH_MM_PM;
                    break;
                case XmlFormattingDefines.MM_DD_YY_HH_MM_24:
                    dateAndTimeFormatting.dateFormat = DateAndTimeFormat.MM_DD_YY_HH_MM_24;
                    break;
            }
        }
    }

    public class XmlFormattingTime : XmlFormatting
    {
        public XmlFormattingTime(XmlNode xmlFormmatting)
        {
            this.xmlFormmatting = xmlFormmatting;
        }

        public override AfTagProperties.TagProperties GetTagProperties()
        {
            ((TextFormatting)prop.outputProperties.formatting).formatting = new TimeFormatting();

            foreach (XmlNode child in xmlFormmatting)
            {
                if (child.Name == XmlFormattingDefines.timeDelimiter)
                {
                    switch (child.InnerText)
                    {
                        case XmlFormattingDefines.Dot:
                            ((TimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).timeDelimiter = TimeDelimiter.Dot;
                            break;
                        case XmlFormattingDefines.Minus:
                            ((TimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).timeDelimiter = TimeDelimiter.Minus;
                            break;
                        case XmlFormattingDefines.Colon:
                            ((TimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).timeDelimiter = TimeDelimiter.Colon;
                            break;
                        case XmlFormattingDefines.Slash:
                            ((TimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).timeDelimiter = TimeDelimiter.Slash;
                            break;
                    }
                }
                else if (child.Name == XmlFormattingDefines.timeFormat)
                {
                    switch (child.InnerText)
                    {
                        case XmlFormattingDefines.h_mm:
                            ((TimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).timeFormat = TimeFormat.h_mm;
                            break;
                        case XmlFormattingDefines.H_MM:
                            ((TimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).timeFormat = TimeFormat.H_MM;
                            break;
                        case XmlFormattingDefines.h_mm_ss:
                            ((TimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).timeFormat = TimeFormat.h_mm_ss;
                            break;
                        case XmlFormattingDefines.H_MM_SS:
                            ((TimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).timeFormat = TimeFormat.H_MM_SS;
                            break;
                        case XmlFormattingDefines.hh_mm:
                            ((TimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).timeFormat = TimeFormat.hh_mm;
                            break;
                        case XmlFormattingDefines.HH_MM:
                            ((TimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).timeFormat = TimeFormat.HH_MM;
                            break;
                        case XmlFormattingDefines.hh_mm_ss:
                            ((TimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).timeFormat = TimeFormat.hh_mm_ss;
                            break;
                        case XmlFormattingDefines.HH_MM_SS:
                            ((TimeFormatting)((TextFormatting)prop.outputProperties.formatting).formatting).timeFormat = TimeFormat.HH_MM_SS;
                            break;
                    }
                }
            }

            return prop;
        }
    }

    public class XmlFormattingDigitToString : XmlFormatting
    {
        public XmlFormattingDigitToString(XmlNode xmlFormmatting)
        {
            this.xmlFormmatting = xmlFormmatting;
        }

        public override AfTagProperties.TagProperties GetTagProperties()
        {
            ((TextFormatting)prop.outputProperties.formatting).formatting = new DigitToStringFormating();

            foreach (XmlNode child in xmlFormmatting)
            {
                if (child.Name == XmlFormattingDefines.currencyFormat)
                {
                    switch (child.InnerText)
                    {
                        case XmlFormattingDefines.EURO:
                            ((DigitToStringFormating)((TextFormatting)prop.outputProperties.formatting).formatting).currencyFormat = CurrencyFormat.EURO;
                            break;
                        case XmlFormattingDefines.NIS:
                            ((DigitToStringFormating)((TextFormatting)prop.outputProperties.formatting).formatting).currencyFormat = CurrencyFormat.NIS;
                            break;
                        case XmlFormattingDefines.US:
                            ((DigitToStringFormating)((TextFormatting)prop.outputProperties.formatting).formatting).currencyFormat = CurrencyFormat.US;
                            break;
                        case XmlFormattingDefines.NONE:
                            ((DigitToStringFormating)((TextFormatting)prop.outputProperties.formatting).formatting).currencyFormat = CurrencyFormat.NONE;
                            break;
                    }
                }
                else if (child.Name == XmlFormattingDefines.languageFormat)
                {
                    if (child.InnerText == XmlFormattingDefines.ENGLISH)
                    {
                        ((DigitToStringFormating)((TextFormatting)prop.outputProperties.formatting).formatting).languageFormat = LanguageFormat.ENGLISH;
                    }
                    else if (child.InnerText == XmlFormattingDefines.HEBREW)
                    {
                        ((DigitToStringFormating)((TextFormatting)prop.outputProperties.formatting).formatting).languageFormat = LanguageFormat.HEBREW;
                    }
                }
            }

            return prop;
        }
    }   
}
