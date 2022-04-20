using System.Xml.Serialization;

//Autofont
using System.Xml;
using System;
using Docomotion.Shared.ComDef;
using Docomotion.Shared.TagPropertiesData.Defines;

/// The classes in this file are automatically serialized to xml for saving the document data.
/// If you change a public member, please consider to:
/// 1. Increase the version in AfDocData.FormatVersion
/// 2. Note that previous versions of the format might not be able to load.
/// 


namespace Docomotion.Shared.TagPropertiesData.Data
{

    ///
    /// Base class for all properties
    /// 
    [XmlInclude(typeof(TagOutputProperties))]
    [XmlInclude(typeof(TextFormatting))]
    [XmlInclude(typeof(BarCodeFormatting))]
    [XmlInclude(typeof(PictureFormatting))]
    [XmlInclude(typeof(DateFormatting))]
    [XmlInclude(typeof(NumberFormatting))]
    [XmlInclude(typeof(StringFormatting))]
    [XmlInclude(typeof(TimeFormatting))]
    [XmlInclude(typeof(DigitToStringFormating))]
    [XmlInclude(typeof(InteractiveFieldFormating))]
    [XmlInclude(typeof(IsInteractiveFieldFormating))]
    [XmlInclude(typeof(ChartFormatting))]
    [XmlInclude(typeof(MultimediaFormatting))]
    [XmlInclude(typeof(RTFFormatting))]
    [XmlInclude(typeof(HTMLFormatting))]
    [XmlInclude(typeof(MSWORDFormatting))]
    [XmlInclude(typeof(DateAndTimeFormatting))]
    public abstract class BaseTagProperty
    {
        public abstract BaseTagProperty Clone();
    }


    // -------------------------------------------------------------------- //
    // TagProperties
    // -------------------------------------------------------------------- //

    public class TagOutputProperties : BaseTagProperty
    {
        public TagFormattingType formattingType = TagFormattingType.Text;

        /// Classes for this object:
        ///   - TextFormatting
        ///   - BarCodeFormatting
        public BaseTagProperty formatting = new TextFormatting();
        public int IsHyperlink = 0;

        public TagOutputProperties() { }

        public override BaseTagProperty Clone()
        {
            TagOutputProperties newClone = new TagOutputProperties();
            newClone.IsHyperlink = this.IsHyperlink;
            newClone.formattingType = this.formattingType;
            if (this.formatting != null)
            {
                newClone.formatting = ((BaseTagProperty)(this.formatting)).Clone();
            }
            return newClone;
        }

    }

    // -------------------------------------------------------------------- //
    // TextFormatting
    // -------------------------------------------------------------------- //

    public class TextFormatting : BaseTagProperty
    {
        public TextTagFormattingType formattingType = TextTagFormattingType.None;

        /// Classes for this object:
        ///   - StringFormatting
        ///   - NumberFormatting
        ///   - DateFormatting
        ///   - TimeFormatting
        public object formatting = null;

        public int maxCharacterLenOnEditTime = -1; // -1 means maximum

        public bool firstTableLineOnly = false;

        public TextFormatting() {}

        public override BaseTagProperty Clone()
        {
            TextFormatting newClone = new TextFormatting();
            newClone.maxCharacterLenOnEditTime = this.maxCharacterLenOnEditTime;
            newClone.formattingType = this.formattingType;
            newClone.firstTableLineOnly = this.firstTableLineOnly;
            if (this.formatting != null)
            {
                newClone.formatting = ((BaseTagProperty)(this.formatting)).Clone();
            }
            return newClone;
        }
    }

    // -------------------------------------------------------------------- //
    // DigitToStringFormating
    // -------------------------------------------------------------------- //

    public class DigitToStringFormating : BaseTagProperty
    {
        public CurrencyFormat currencyFormat = CurrencyFormat.NIS;
        public LanguageFormat languageFormat = LanguageFormat.HEBREW;

        public override BaseTagProperty Clone()
        {
            DigitToStringFormating newClone = new DigitToStringFormating();
            newClone.currencyFormat = this.currencyFormat;
            newClone.languageFormat = this.languageFormat;
            return newClone;
        }
    }


    // -------------------------------------------------------------------- //
    // StringFormatting
    // -------------------------------------------------------------------- //

    public class StringFormatting : BaseTagProperty
    {
        public StringCasing casing = StringCasing.AllUpperCase;

        public override BaseTagProperty Clone()
        {
            StringFormatting newClone = new StringFormatting();
            newClone.casing = this.casing;
            return newClone;
        }
    }


    // -------------------------------------------------------------------- //
    // NumberFormatting
    // -------------------------------------------------------------------- //

    public class NumberFormatting : BaseTagProperty
    {
        public int precisionPointRightFixed = 2;
        public bool thousandCommaSeperator = false;
        public NegativeNumberFormatting negativeFormatting = NegativeNumberFormatting.MinusSign;
        public NumberSeparatorFormatting decimalPointSeperator = NumberSeparatorFormatting.Dot;
        public NumberSeparatorFormatting thousandSeperator = NumberSeparatorFormatting.Comma;

        public bool PositiveOnly = false;
        public string ErrorMaskMessage = string.Empty;

        public override BaseTagProperty Clone()
        {
            NumberFormatting newClone = new NumberFormatting();
            newClone.precisionPointRightFixed = this.precisionPointRightFixed;
            newClone.thousandCommaSeperator = this.thousandCommaSeperator;
            newClone.negativeFormatting = this.negativeFormatting;
            newClone.decimalPointSeperator = this.decimalPointSeperator;
            newClone.thousandSeperator = this.thousandSeperator;
            newClone.PositiveOnly = this.PositiveOnly;
            newClone.ErrorMaskMessage = string.Copy(this.ErrorMaskMessage);
            return newClone;
        }
    }


    // -------------------------------------------------------------------- //
    // DateFormatting
    // -------------------------------------------------------------------- //

    public class DateFormatting : BaseTagProperty
    {
        public DateFormat dateFormat = DateFormat.DD_MM_YYYY;
        public TimeDelimiter dateDelimiter = TimeDelimiter.Dot;

        public CalanderType calanderType = CalanderType.Gregorian;
        public CalanderLanguage calanderLanguage = CalanderLanguage.ENG;

        public FieldPropertiesRTConfiguration FieldPropRTConfig { get; set; } = null;

        public override BaseTagProperty Clone()
        {
            DateFormatting newClone = new DateFormatting();
            newClone.dateFormat = this.dateFormat;
            newClone.dateDelimiter = this.dateDelimiter;

            newClone.calanderType = this.calanderType;
            newClone.calanderLanguage = this.calanderLanguage;
            return newClone;
        }
    }


    // -------------------------------------------------------------------- //
    // TimeFormatting
    // -------------------------------------------------------------------- //

    public class TimeFormatting : BaseTagProperty
    {
        public TimeFormat timeFormat = TimeFormat.HH_MM_SS;
        public TimeDelimiter timeDelimiter = TimeDelimiter.Colon;

        public override BaseTagProperty Clone()
        {
            TimeFormatting newClone = new TimeFormatting();
            newClone.timeDelimiter = this.timeDelimiter;
            newClone.timeFormat = this.timeFormat;
            return newClone;
        }
    }

    // -------------------------------------------------------------------- //
    // DateAndTimeFormatting
    // -------------------------------------------------------------------- //

    public class DateAndTimeFormatting : BaseTagProperty
    {
        public const int DATA_LEN = 14; //ssmmHHddmmyyyy
        public DateAndTimeFormat dateFormat = DateAndTimeFormat.DD_MM_YYYY;
        public TimeDelimiter dateDelimiter = TimeDelimiter.Dot;

        public CalanderType calanderType = CalanderType.Gregorian;
        public CalanderLanguage calanderLanguage = CalanderLanguage.ENG;

        public bool useTimeZone = false;
        public string timeZoneID = string.Empty;

        public bool WithTime = false;

        public FieldPropertiesRTConfiguration FieldPropRTConfig { get; set; } = null;

        public override BaseTagProperty Clone()
        {
            DateAndTimeFormatting newClone = new DateAndTimeFormatting();
            newClone.dateFormat = this.dateFormat;
            newClone.dateDelimiter = this.dateDelimiter;

            newClone.calanderType = this.calanderType;
            newClone.calanderLanguage = this.calanderLanguage;

            newClone.useTimeZone = this.useTimeZone;
            newClone.timeZoneID = this.timeZoneID;
            return newClone;
        }
    }


    // -------------------------------------------------------------------- //
    // BarCodeFormatting
    // -------------------------------------------------------------------- //

    public class BarCodeFormatting : BaseTagProperty
    {
        public int standard = (int)BarCodeStandard.Standard_128;
        public bool humanReadable = false;
        public int checkDigit = (int)BarCodeCheckDigit.None;
        public int rotate = 0;

        [XmlIgnore]
        public bool rotationChange = false;

        public override BaseTagProperty Clone()
        {
            BarCodeFormatting newObj = new BarCodeFormatting();
            newObj.standard = this.standard;
            newObj.humanReadable = this.humanReadable;
            newObj.checkDigit = this.checkDigit;
            newObj.rotate = this.rotate;

            return newObj;
        }
    }

    // -------------------------------------------------------------------- //
    // PictureFormatting
    // -------------------------------------------------------------------- //

    public class PictureFormatting : BaseTagProperty
    {
        public int AdjustPictureOptions = (int)PictureDimentions.StretchToDesign;
        public int DefaultResolution = 96;
        public bool OmitPicture = false;

        public override BaseTagProperty Clone()
        {
            PictureFormatting newObj = new PictureFormatting();

            newObj.AdjustPictureOptions = this.AdjustPictureOptions;
            newObj.DefaultResolution = this.DefaultResolution;
            newObj.OmitPicture = this.OmitPicture;

            return newObj;
        }        
    }

    // -------------------------------------------------------------------- //
    // ChartFormatting
    // -------------------------------------------------------------------- //

    public class ChartFormatting : BaseTagProperty
    {
        public XmlDocument GraphObject = null;
        public string AppendString;
        public int Resolution = 96;
        public FreeFormDefinitions.FDC.Enums.FDCPredefinedChart FDCPredefinedChart = FreeFormDefinitions.FDC.Enums.FDCPredefinedChart.None;

        public override BaseTagProperty Clone()
        {
            ChartFormatting newObj = new ChartFormatting();
            newObj.GraphObject = this.GraphObject;
            newObj.AppendString = this.AppendString;
            newObj.Resolution = this.Resolution;
            newObj.FDCPredefinedChart = this.FDCPredefinedChart;

            return newObj;
        }

    }

    

    public class MultimediaFormatting : BaseTagProperty
    {
        

        public override BaseTagProperty Clone()
        {
            MultimediaFormatting newObj = new MultimediaFormatting();          
            return newObj;
        }
    }


    public class RTFFormatting : BaseTagProperty
    {


        public override BaseTagProperty Clone()
        {
            RTFFormatting newObj = new RTFFormatting();
            return newObj;
        }
    }

    public class HTMLFormatting : BaseTagProperty
    {
        public bool FontFamilyByDesigner = false; 
        public bool FontSizeByDesigner = false;
        public bool ForceRtlDirection = false;

        public override BaseTagProperty Clone()
        {
            HTMLFormatting newObj = new HTMLFormatting();
            newObj.FontFamilyByDesigner = this.FontFamilyByDesigner;
            newObj.FontSizeByDesigner = this.FontSizeByDesigner;
            newObj.ForceRtlDirection = this.ForceRtlDirection;
            return newObj;
        }
    }

    public class MSWORDFormatting : BaseTagProperty
    {
        public bool KeepSourceFormatting;
        public bool StartContentFromNewPage;
        public bool TrimExtraEmptyLines = true;
        public bool MergeList;

        public override BaseTagProperty Clone()
        {
            return new MSWORDFormatting
            {
                KeepSourceFormatting = KeepSourceFormatting,
                StartContentFromNewPage = StartContentFromNewPage,
                TrimExtraEmptyLines = TrimExtraEmptyLines,
                MergeList = MergeList
            };
        }
    }



}
