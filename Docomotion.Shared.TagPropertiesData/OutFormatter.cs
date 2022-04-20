using System;
using System.Globalization;
using System.Text;
using System.Xml;
using Docomotion.Shared.ComDef;

//Autofont
using Docomotion.Shared.NumberUtils;
using Docomotion.Shared.NumberUtils.Enums;
using Docomotion.Shared.TagPropertiesData.Data;
using Docomotion.Shared.TagPropertiesData.Defines;
using Docomotion.Shared.TagPropertiesData.XmlSerializerTagProperties;

namespace Docomotion.Shared.TagPropertiesData
{
    public class OutFormatter
    {
        #region Constants

        public const string CONST_ERROR_STRING = "NaM";
        public const string CONST_AM_STRING = " AM";
        public const string CONST_PM_STRING = " PM";

        #endregion

        #region Members

        public static string[] TIME_DELIMITERS = new string[]
            {
                ".",
                "-",
                "/",
                ":"
            };
       
        public static string[] DATE_DELIMITERS = new string[]
            {
                ".",
                "-",
                "/"
            };
        #endregion

        public static string SetOutputMask(XmlNode properties, string inputString)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(inputString))
            {
                // when running with empty form the runtime insert the "AfFreeFormEmptyFormSpecialString"
                // so if the inputString start with the "AfFreeFormEmptyFormSpecialString" the remove it and return the real value
                if (inputString.StartsWith("AfFreeFormEmptyFormSpecialString"))
                {
                    result = inputString.Replace("AfFreeFormEmptyFormSpecialString", "");
                }
                else
                {
                    result = CONST_ERROR_STRING;
                    DeserializeTagProperties deserializeTagProperties = new DeserializeTagProperties(properties);
                    AfTagProperties.TagProperties prop = deserializeTagProperties.Deserialize();
                    if (prop != null)
                    {
                        result = ApplyTagProperties(prop.outputProperties, inputString);
                    }
                }
            }
            return result;
        }

        public static string SetOutputMask(XmlNode properties, string inputString, FieldPropertiesRTConfiguration fieldPropRTConfig)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(inputString))
            {
                // when running with empty form the runtime insert the "AfFreeFormEmptyFormSpecialString"
                // so if the inputString start with the "AfFreeFormEmptyFormSpecialString" the remove it and return the real value
                if (inputString.StartsWith("AfFreeFormEmptyFormSpecialString"))
                {
                    result = inputString.Replace("AfFreeFormEmptyFormSpecialString", "");
                }
                else
                {
                    result = CONST_ERROR_STRING;
                    DeserializeTagProperties deserializeTagProperties = new DeserializeTagProperties(properties);
                    deserializeTagProperties.FieldPropRTConfig = fieldPropRTConfig;
                    AfTagProperties.TagProperties prop = deserializeTagProperties.Deserialize();
                    if (prop != null)
                    {
                        result = ApplyTagProperties(prop.outputProperties, inputString);
                    }
                }
            }
            return result;
        }

        private static void WrtieToFile(string txt)
        {
            // *** Write to file ***
            string filePath = @"c:\22.xml";

            // Specify file, instructions, and privileges
            System.IO.FileStream file = new System.IO.FileStream(filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write);

            // Create a new stream to write to the file
            System.IO.StreamWriter sw = new System.IO.StreamWriter(file);

            // Write a string to the file
            sw.Write(txt);

            // Close StreamWriter
            sw.Close();

            // Close file
            file.Close();
        } 

        public static bool IsOutputMaskValid(TagOutputProperties outputProp, string inputString)
        {
            string outputString = CONST_ERROR_STRING;

            outputString = ApplyTagProperties(outputProp, inputString);

            return outputString != CONST_ERROR_STRING;            
        }

        private static string ApplyTagProperties(Data.TagOutputProperties tagProps, string testValue)
        {
            if (tagProps.formatting is Data.TextFormatting)
            {
                string retStr = ApplyTextFormatting(
                    (Data.TextFormatting)tagProps.formatting,
                    testValue);

                return retStr;
            }

            return testValue;
        }

        private static string ApplyTextFormatting(Data.TextFormatting formatting, string testValue)
        {
            if (formatting.formatting is Data.NumberFormatting)
                return ApplyNumberFormatting(
                    (Data.NumberFormatting)formatting.formatting,
                    testValue);

            if (formatting.formatting is Data.StringFormatting)
                return ApplyStringFormatting(
                    (Data.StringFormatting)formatting.formatting,
                    testValue);

            if (formatting.formatting is Data.DateFormatting)
                return ApplyStringFormatting(
                    (Data.DateFormatting)formatting.formatting,
                    testValue);

            if (formatting.formatting is Data.TimeFormatting)
                return ApplyStringFormatting(
                    (Data.TimeFormatting)formatting.formatting,
                    testValue);

            if (formatting.formatting is Data.DigitToStringFormating)
                return ApplyStringFormatting(
                    (Data.DigitToStringFormating)formatting.formatting,
                    testValue);

            if (formatting.formatting is Data.DateAndTimeFormatting)
                return ApplyStringFormatting(
                    (Data.DateAndTimeFormatting)formatting.formatting,
                    testValue);

            return testValue;
        }
     
        public static string ApplyNumberFormatting(Data.NumberFormatting formatting, string testValue)
        {
            try
            {
                if (testValue.Length == 0)
                    return CONST_ERROR_STRING;

                bool bLRE = false;
                byte[] LRE_Bytes = { 0xE2, 0x80, 0xAA };

                char[] LRE = UTF8Encoding.UTF8.GetChars(LRE_Bytes);

                char[] tv = testValue.ToCharArray();

                if(tv[0] == LRE[0])
                {
                    bLRE = true;
                }

                bool isNegative = false;

                double number = 0;
                if (testValue.StartsWith("-"))
                {
                    number = double.Parse(testValue.Substring(1, testValue.Length - 1));
                    isNegative = true;
                }
                else
                {
                    number = double.Parse(testValue);
                }

                testValue = "";

                string txtFormatter = formatting.thousandCommaSeperator ?
                    "N" : "F";

                txtFormatter += formatting.precisionPointRightFixed.ToString();

                NumberFormatInfo formatInfo = new NumberFormatInfo
                {
                    NumberDecimalSeparator = GetNumberSeperator(formatting.decimalPointSeperator),
                    NumberGroupSeparator = GetNumberSeperator(formatting.thousandSeperator)
                };

                testValue = number.ToString(txtFormatter, formatInfo);

                if (isNegative)
                {
                    if (formatting.negativeFormatting == NegativeNumberFormatting.MinusSign)
                    {
                        testValue = string.Format("{0}{1}", "-", testValue);
                        testValue = string.Concat(LRE[0], testValue);
                    }
                    else
                        testValue = "(" + testValue + ")";
                }

                if (formatting.PositiveOnly) testValue = testValue.Trim(new char[] { '-', '(', ')', LRE[0] });
            }
            catch (System.Exception e)
            {
                testValue = CONST_ERROR_STRING;
            }


            return testValue;
        }

        private static string GetNumberSeperator(NumberSeparatorFormatting seperatorFormat)
        {
            string separatorString = string.Empty;

            switch (seperatorFormat)
            {
                case NumberSeparatorFormatting.Comma:
                    separatorString = ",";
                    break;
                case NumberSeparatorFormatting.Dot:
                    separatorString = ".";
                    break;
                case NumberSeparatorFormatting.Semicolon:
                    separatorString = ";";
                    break;
                case NumberSeparatorFormatting.Space:
                    separatorString = " ";
                    break;
            }

            return separatorString;
        }

        private static string ApplyStringFormatting(StringFormatting formatting, string testValue)
        {
            switch (formatting.casing)
            {
                case StringCasing.AllLowerCase:
                    return testValue.ToLower();

                case StringCasing.AllUpperCase:
                    return testValue.ToUpper();

                case StringCasing.Name:
                case StringCasing.Title:
                    return CapitalizeAsName(testValue);
            }

            return testValue;
        }

        private static string CapitalizeAsName(string txt)
        {
            string result = "";
            bool capitalizeNextChar = true;

            foreach (char c in txt)
            {
                if (capitalizeNextChar)
                    result += c.ToString().ToUpper();
                else
                    result += c.ToString().ToLower();

                capitalizeNextChar = (c == ' ');
            }

            return result;
        }

        public static string ApplyStringFormatting(Data.DateFormatting formatting, string testValue)
        {
            try
            {
                testValue = GetDateFormatString(formatting, testValue);

                string delimiter = DATE_DELIMITERS[(int)formatting.dateDelimiter];

                testValue = testValue.Replace(":", delimiter);
            }
            catch (System.Exception e)
            {
                testValue = CONST_ERROR_STRING;
            }

            return testValue;
        }

        public static string ApplyStringFormatting(Data.DateAndTimeFormatting formatting, string testValue)
        {
            try
            {
                string delimiter = DATE_DELIMITERS[(int)formatting.dateDelimiter];

                testValue = GetDateAndTimeFormatString(formatting, testValue, delimiter);
            }
            catch (System.Exception e)
            {
                testValue = CONST_ERROR_STRING;
            }

            return testValue;
        }

        public static string GetDateFormatString(Data.DateFormatting dateFormat, string testValue)
        {
            //FUTURE: "ddMMyyyy" is needed to get from the inputDateTime 
            //Support for Saleforce format. Support for format HHmmssddMMyyyy
            DateTime DateTimeTest;
            DateTime retDt;
            if (DateTime.TryParseExact(testValue, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out DateTimeTest))
            {
                testValue = ConvertDateTimeValue(testValue);

                //max length of date and time = 14 symbols (01020317051966) - HHmmssddMMyyyy
                if (testValue.Length < DateAndTimeFormatting.DATA_LEN)
                    testValue = testValue.PadLeft(DateAndTimeFormatting.DATA_LEN, '0');

                retDt = GetDateTimeFormat(testValue, "HHmmssddMMyyyy");
            }
            else
            {
                retDt = GetDateTimeFormat(testValue, "ddMMyyyy");
            }

            if (retDt == DateTime.MinValue)
            {
                testValue = CONST_ERROR_STRING;
            }
            else
            {
                if (dateFormat.calanderLanguage == CalanderLanguage.HEB)
                {
                    switch (dateFormat.calanderType)
                    {
                        case CalanderType.Gregorian:
                            testValue = GetHebrewGeogianDateString(retDt);
                             break;
                        case CalanderType.Lunar:
                             testValue = GetHebrewLounerDateString(retDt, dateFormat.FieldPropRTConfig);
                            break;
                    }

                    return testValue;
                }
               
                switch (dateFormat.dateFormat)
                {
                    case DateFormat.DD_MM_YYYY:
                        testValue = string.Format("{0:D2}", retDt.Day) + ":" + string.Format("{0:D2}", retDt.Month) + ":" + retDt.Year;
                        break;
                    case DateFormat.DD_MM_YY:
                        testValue = string.Format("{0:D2}", retDt.Day) + ":" + string.Format("{0:D2}", retDt.Month) + ":" + retDt.Year.ToString().Substring(2,2);
                        break;
                    case DateFormat.D_M_YYYY:
                        testValue = retDt.Day + ":" + retDt.Month + ":" + retDt.Year;
                        break;
                    case DateFormat.D_M_YY:
                        testValue = retDt.Day + ":" + retDt.Month + ":" + retDt.Year.ToString().Substring(2, 2);
                        break;
                    case DateFormat.MM_DD_YYYY:
                        testValue = string.Format("{0:D2}", retDt.Month) + ":" + string.Format("{0:D2}", retDt.Day) + ":" + retDt.Year;
                        break;
                    case DateFormat.MM_DD_YY:
                        testValue = string.Format("{0:D2}", retDt.Month) + ":" + string.Format("{0:D2}", retDt.Day) + ":" + retDt.Year.ToString().Substring(2, 2);
                        break;
                    case DateFormat.M_D_YYYY:
                        testValue = retDt.Month + ":" + retDt.Day + ":" + retDt.Year;
                        break;
                    case DateFormat.M_D_YY:
                        testValue = retDt.Month + ":" + retDt.Day + ":" + retDt.Year.ToString().Substring(2, 2);
                        break;
                    case DateFormat.Day_DD_Month_YYYY:
                        testValue = GetEnglishGeogianDateString(retDt);
                        break;
                    case DateFormat.DD_MMM_YY:
                        testValue = retDt.ToString("dd:MMM:yy", CultureInfo.CreateSpecificCulture("en-US"));
                        break;
                    case DateFormat.MONTH_DD_COMMA_YYYY:
                        testValue = retDt.ToString("MMMM dd, yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                       break;
                }
            }
            return testValue;
        }

        private static string ConvertDateTimeValue(string dateTimeValue)
        {
            //convert the formated humanable string "2015-05-21 08:52:21" to 08522121052015
            string value4Mask = dateTimeValue;

            char[] sep = { '-', ' ', ':' };
            string[] parts = dateTimeValue.Split(sep);

            if(parts.Length == 6)
                value4Mask = string.Concat(parts[3], parts[4], parts[5], parts[2], parts[1], parts[0]);

            return value4Mask;
        }

        public static string GetDateAndTimeFormatString(Data.DateAndTimeFormatting dateFormat, string testValue, string dateSeperator)
        {
            //Support for Saleforce format. Support for format HHmmssddMMyyyy
            DateTime DateTimeTest;
            if (DateTime.TryParseExact(testValue, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out DateTimeTest))
            {
                testValue = ConvertDateTimeValue(testValue);
            }

            //max length of date and time = 14 symbols (01020317051966) - HHmmssddMMyyyy
            if (testValue.Length < DateAndTimeFormatting.DATA_LEN)
                testValue = testValue.PadLeft(DateAndTimeFormatting.DATA_LEN, '0');
            
            DateTime retDt = GetDateTimeFormat(testValue, "HHmmssddMMyyyy");
            
            if (dateFormat.useTimeZone)
            {
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(dateFormat.timeZoneID);
                retDt = retDt.Add(timeZone.BaseUtcOffset);
            }

            if (retDt == DateTime.MinValue)
            {
                testValue = CONST_ERROR_STRING;
            }
            else
            {
                if (dateFormat.calanderLanguage == CalanderLanguage.HEB)
                {
                    switch (dateFormat.calanderType)
                    {
                        case CalanderType.Gregorian:
                            testValue = GetHebrewGeogianDateString(retDt);
                            break;
                        case CalanderType.Lunar:
                            testValue = GetHebrewLounerDateString(retDt, dateFormat.FieldPropRTConfig);
                            break;
                    }

                    return testValue;
                }

                switch (dateFormat.dateFormat)
                {
                    case DateAndTimeFormat.DD_MM_YYYY:
                        testValue = string.Format("{0:D2}", retDt.Day) + dateSeperator + string.Format("{0:D2}", retDt.Month) + dateSeperator + retDt.Year;
                        break;
                    case DateAndTimeFormat.DD_MM_YY:
                        testValue = string.Format("{0:D2}", retDt.Day) + dateSeperator + string.Format("{0:D2}", retDt.Month) + dateSeperator + retDt.Year.ToString().Substring(2, 2);
                        break;
                    case DateAndTimeFormat.D_M_YYYY:
                        testValue = retDt.Day + dateSeperator + retDt.Month + dateSeperator + retDt.Year;
                        break;
                    case DateAndTimeFormat.D_M_YY:
                        testValue = retDt.Day + dateSeperator + retDt.Month + dateSeperator + retDt.Year.ToString().Substring(2, 2);
                        break;
                    case DateAndTimeFormat.MM_DD_YYYY:
                        testValue = string.Format("{0:D2}", retDt.Month) + dateSeperator + string.Format("{0:D2}", retDt.Day) + dateSeperator + retDt.Year;
                        break;
                    case DateAndTimeFormat.MM_DD_YY:
                        testValue = string.Format("{0:D2}", retDt.Month) + dateSeperator + string.Format("{0:D2}", retDt.Day) + dateSeperator + retDt.Year.ToString().Substring(2, 2);
                        break;
                    case DateAndTimeFormat.M_D_YYYY:
                        testValue = retDt.Month + dateSeperator + retDt.Day + dateSeperator + retDt.Year;
                        break;
                    case DateAndTimeFormat.M_D_YY:
                        testValue = retDt.Month + dateSeperator + retDt.Day + dateSeperator + retDt.Year.ToString().Substring(2, 2);
                        break;
                    case DateAndTimeFormat.Day_DD_Month_YYYY:
                        testValue = GetEnglishGeogianDateString(retDt);
                        break;
                    case DateAndTimeFormat.DD_MMM_YY:
                        testValue = retDt.ToString($"dd{dateSeperator}MMM{dateSeperator}yy", CultureInfo.CreateSpecificCulture("en-US")); ;
                        break;
                    case DateAndTimeFormat.MONTH_DD_COMMA_YYYY:
                        testValue = retDt.ToString($"MMMM dd, yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                        break;

                    case DateAndTimeFormat.DD_MM_YYYY_HH_MM_PM:
                        testValue = retDt.ToString($"dd{dateSeperator}MM{dateSeperator}yyyy hh:mm tt", CultureInfo.CreateSpecificCulture("en-US"));
                        break;
                    case DateAndTimeFormat.DD_MM_YYYY_HH_MM_24:
                        testValue = retDt.ToString($"dd{dateSeperator}MM{dateSeperator}yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-US"));
                        break;

                    case DateAndTimeFormat.MM_DD_YYYY_HH_MM_PM:
                        testValue = retDt.ToString($"MM{dateSeperator}dd{dateSeperator}yyyy hh:mm tt", CultureInfo.CreateSpecificCulture("en-US"));
                        break;

                    case DateAndTimeFormat.MM_DD_YYYY_HH_MM_24:
                        testValue = retDt.ToString($"MM{dateSeperator}dd{dateSeperator}yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-US"));
                        break;

                    case DateAndTimeFormat.DD_MM_YY_HH_MM_PM:
                        testValue = retDt.ToString($"dd{dateSeperator}MM{dateSeperator}yy hh:mm tt", CultureInfo.CreateSpecificCulture("en-US"));
                        break;

                    case DateAndTimeFormat.DD_MM_YY_HH_MM_24:
                        testValue = retDt.ToString($"dd{dateSeperator}MM{dateSeperator}yy HH:mm", CultureInfo.CreateSpecificCulture("en-US"));
                        break;

                    case DateAndTimeFormat.MM_DD_YY_HH_MM_PM:
                        testValue = retDt.ToString($"MM{dateSeperator}dd{dateSeperator}yy hh:mm tt", CultureInfo.CreateSpecificCulture("en-US"));
                        break;

                    case DateAndTimeFormat.MM_DD_YY_HH_MM_24:
                        testValue = retDt.ToString($"MM{dateSeperator}dd{dateSeperator}yy HH:mm", CultureInfo.CreateSpecificCulture("en-US"));
                        break;
                }
            }
            return testValue;
        }

        private static string GetHebrewLounerDateString(DateTime anyDate, FieldPropertiesRTConfiguration fieldPropRTConfig)
        {
            System.Text.StringBuilder hebrewFormatedString = new System.Text.StringBuilder();

            // Create the Hebrew culture to use Hebrew (Jewish) calendar
            CultureInfo jewishCulture = CultureInfo.CreateSpecificCulture("he-IL");
            jewishCulture.DateTimeFormat.Calendar = new HebrewCalendar();

            // Day of the week in the format " "
            // dont in use -> hebrewFormatedString.Append(anyDate.ToString("dddd", jewishCulture) + " ");

            // Day of the month in the format "'"
            hebrewFormatedString.Append(anyDate.ToString("dd", jewishCulture) + " ");

            AddHebrewLounerMonth(anyDate, hebrewFormatedString, jewishCulture, fieldPropRTConfig);

            //year in the format " "
            hebrewFormatedString.Append("" + anyDate.ToString("yyyy", jewishCulture));

            

            return hebrewFormatedString.ToString();
        }

        private static void AddHebrewLounerMonth(DateTime dateTime, StringBuilder hebrewFormatedString, CultureInfo jewishCulture, FieldPropertiesRTConfiguration fieldPropRTConfig)
        {
            try
            {
                bool leapYearMonthAdara = true;
                if (fieldPropRTConfig != null) leapYearMonthAdara = fieldPropRTConfig.LeapYearMonthAdara;

                string monthName = dateTime.ToString("MMM", jewishCulture);
                int curYear = jewishCulture.DateTimeFormat.Calendar.GetYear(dateTime);
                if (jewishCulture.DateTimeFormat.Calendar.IsLeapYear(curYear))
                {
                    if (monthName.Equals("àãø"))
                    {
                        if (leapYearMonthAdara)
                            monthName = "àãø à";
                    }
                }

                hebrewFormatedString.Append(monthName + " ");
            }
            catch { hebrewFormatedString.Append(dateTime.ToString("MMM", jewishCulture) + " "); }
        }

        private static string GetHebrewGeogianDateString(DateTime anyDate)
        {
            System.Text.StringBuilder hebrewFormatedString = new System.Text.StringBuilder();

            // Create the Hebrew culture to use Hebrew (Jewish) calendar
            CultureInfo jewishCulture = CultureInfo.CreateSpecificCulture("he-IL");
            jewishCulture.DateTimeFormat.Calendar = new HebrewCalendar();

            CultureInfo englishCulture = CultureInfo.CreateSpecificCulture("en-US");
            englishCulture.DateTimeFormat.Calendar = new GregorianCalendar();

            CultureInfo englishHebrewCulture = CultureInfo.CreateSpecificCulture("he-IL");
            englishHebrewCulture.DateTimeFormat.Calendar = new GregorianCalendar();

            // Day of the week in the format " "
            // dont in use -> hebrewFormatedString.Append(anyDate.ToString("dddd", jewishCulture) + " ");


            // Day of the month in the format "'"
            hebrewFormatedString.Append(anyDate.ToString("dd", englishCulture) + " ");

            // Month and year in the format " "
            hebrewFormatedString.Append("" + anyDate.ToString("y", englishHebrewCulture));

            return hebrewFormatedString.ToString();
        }

        private static string GetEnglishGeogianDateString(DateTime anyDate)
        {
            System.Text.StringBuilder hebrewFormatedString = new System.Text.StringBuilder();


            CultureInfo englishCulture = CultureInfo.CreateSpecificCulture("en-US");
            englishCulture.DateTimeFormat.Calendar = new GregorianCalendar();


            // Day of the week in the format " "
            hebrewFormatedString.Append(anyDate.ToString("dddd", englishCulture) + " ");


            // Day of the month in the format "'"
            hebrewFormatedString.Append(anyDate.ToString("dd", englishCulture) + " ");

            // Month and year in the format " "
            hebrewFormatedString.Append("" + anyDate.ToString("y", englishCulture));

            return hebrewFormatedString.ToString();
        }

        private static string ApplyStringFormatting(TimeFormatting formatting, string testValue)
        {
            testValue = GetTimeFormatString(formatting.timeFormat, testValue);

            string delimiter = TIME_DELIMITERS[(int)formatting.timeDelimiter];

            testValue = testValue.Replace(":", delimiter);

            return testValue;
            
        }

        public static string GetTimeFormatString(TimeFormat timeFormat, string testValue)
        {
            try
            {
                //FUTURE: "HHmmss" is needed to get from the input
                DateTime retDt = GetDateTimeFormat(testValue, "HHmmss");

                if (retDt == DateTime.MinValue)
                {
                    testValue = CONST_ERROR_STRING;
                }
                else
                {
                    CultureInfo en_us_Culture = new CultureInfo("en-US");
                    switch (timeFormat)
                    {
                        case TimeFormat.HH_MM_SS:
                            testValue = retDt.ToLongTimeString();
                            break;
                        case TimeFormat.H_MM_SS:
                            testValue = retDt.Hour + ":" + string.Format("{0:D2}", retDt.Minute) + ":" + string.Format("{0:D2}", retDt.Second);
                            break;
                        case TimeFormat.HH_MM:
                            testValue = retDt.ToShortTimeString();
                            break;
                        case TimeFormat.H_MM:
                            testValue = retDt.Hour + ":" + string.Format("{0:D2}", retDt.Minute);
                            break;
                        case TimeFormat.hh_mm_ss:
                            testValue = retDt.ToString("T", en_us_Culture);
                            int h = retDt.Hour;
                            if (h > 12)
                            {
                                h = h - 12;
                            }

                            if (testValue.IndexOf("AM") > 0)
                            {
                                testValue = string.Format("{0:D2}", h) + ":" + string.Format("{0:D2}", retDt.Minute) + ":" + string.Format("{0:D2}", retDt.Second) + CONST_AM_STRING;
                            }
                            else
                            {
                                testValue = string.Format("{0:D2}", h) + ":" + string.Format("{0:D2}", retDt.Minute) + ":" + string.Format("{0:D2}", retDt.Second) + CONST_PM_STRING;
                            }
                            break;
                        case TimeFormat.h_mm_ss:
                            testValue = retDt.ToString("T", en_us_Culture);
                            h = retDt.Hour;
                            if (h > 12)
                            {
                                h = h - 12;
                            }
                            if (testValue.IndexOf("AM") > 0)
                            {
                                testValue = h + ":" + string.Format("{0:D2}", retDt.Minute) + ":" + string.Format("{0:D2}", retDt.Second) + CONST_AM_STRING;
                            }
                            else
                            {
                                testValue = h + ":" + string.Format("{0:D2}", retDt.Minute) + ":" + string.Format("{0:D2}", retDt.Second) + CONST_PM_STRING;
                            }
                            break;
                        case TimeFormat.hh_mm:
                            testValue = retDt.ToString("T", en_us_Culture);
                            h = retDt.Hour;
                            if (h > 12)
                            {
                                h = h - 12;
                            }
                            if (testValue.IndexOf("AM") > 0)
                            {
                                testValue = string.Format("{0:D2}", h) + ":" + string.Format("{0:D2}", retDt.Minute) + CONST_AM_STRING;
                            }
                            else
                            {
                                testValue = string.Format("{0:D2}", h) + ":" + string.Format("{0:D2}", retDt.Minute) + CONST_PM_STRING;
                            }
                            break;

                        case TimeFormat.h_mm:
                            testValue = retDt.ToString("T", en_us_Culture);
                            h = retDt.Hour;
                            if (h > 12)
                            {
                                h = h - 12;
                            }
                            if (testValue.IndexOf("AM") > 0)
                            {
                                testValue = h + ":" + string.Format("{0:D2}", retDt.Minute) + CONST_AM_STRING;
                            }
                            else
                            {
                                testValue = h + ":" + string.Format("{0:D2}", retDt.Minute) + CONST_PM_STRING;
                            }
                            break;

                    }

                }
            }
            catch
            {
                testValue = CONST_ERROR_STRING;
            }

            return testValue;
        }
        
        public static DateTime GetDateTimeFormat(string dtStr, string dateTimeFormat)
        {
            DateTime dt = new DateTime();

            try
            {
                System.Globalization.CultureInfo en_us_Culture = new System.Globalization.CultureInfo("en-US");

                DateTime.TryParseExact(dtStr, dateTimeFormat, en_us_Culture, System.Globalization.DateTimeStyles.AdjustToUniversal, out dt);
            }
            catch
            {
            }

            return dt;
        }

        public static string ApplyStringFormatting(Data.DigitToStringFormating formatting, string testValue)
        {
            try
            {
                var number = double.Parse(testValue);
                var currency = (int)formatting.currencyFormat;
                var language = (int)formatting.languageFormat;
                
                return NumberConverter.ToWords(number, (Language)language, (Currency)currency);
            }
            catch
            {
                return CONST_ERROR_STRING;
            }        
        }
    }
}
