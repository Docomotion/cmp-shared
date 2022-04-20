using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;


namespace Docomotion.Shared.TagPropertiesData.Defines
{
    // -------------------------------------------------------------------- //
    // Tag Properties
    // -------------------------------------------------------------------- //

    public enum TagFormattingType
    {
        Text,
        BarCode,
        Picture,
        PictureBinary,
        Interactive,
        Chart,
        Multimedia,
        InputRTF,
        InputHTML,
        IsInteractive,
        InputMSWORD
    }

    public enum TextTagFormattingType
    {
        None,
        String,
        Number,
        Date,
        Time,
        DigitToString,
        DateAndTime
    }

    public enum StringCasing
    {
        AllUpperCase,
        AllLowerCase,
        Name,
        Title
    }

    public enum PictureDimentions
    {
        StretchToDesign,
        StretchToMax,
        RealDimensions
    }

    public enum CurrencyFormat
    {
        NIS,
        US,
        EURO,
        NONE
    }

    public enum LanguageFormat
    {
        HEBREW,
        ENGLISH,
    }

    // indexes must match definition of enum ENUM_STRINGS_NegativeNumberFormatting
    public enum NegativeNumberFormatting
    {
        MinusSign,
        Parentheses
    }

    public enum NumberSeparatorFormatting
    {
        Comma,
        Dot,
        Semicolon,
        Space
    }


    public enum DateFormat
    {
        DD_MM_YYYY,
        DD_MM_YY,
        D_M_YYYY,
        D_M_YY,
        MM_DD_YYYY,
        MM_DD_YY,
        M_D_YYYY,
        M_D_YY,
        Day_DD_Month_YYYY,
        DD_MMM_YY,
        MONTH_DD_COMMA_YYYY
    }

    public enum DateAndTimeFormat
    {
        DD_MM_YYYY,
        DD_MM_YY,
        D_M_YYYY,
        D_M_YY,
        MM_DD_YYYY,
        MM_DD_YY,
        M_D_YYYY,
        M_D_YY,
        Day_DD_Month_YYYY,
        DD_MMM_YY,
        MONTH_DD_COMMA_YYYY,
        DD_MM_YYYY_HH_MM_PM,
        DD_MM_YYYY_HH_MM_24,
        MM_DD_YYYY_HH_MM_PM,
        MM_DD_YYYY_HH_MM_24,
        DD_MM_YY_HH_MM_PM,
        DD_MM_YY_HH_MM_24,
        MM_DD_YY_HH_MM_PM,
        MM_DD_YY_HH_MM_24
    }

    public enum TimeDelimiter
    {
        Dot,
        Minus,
        Slash,
        Colon
    }

    public enum CalanderLanguage
    {
        ENG,
        HEB
    }

    public enum CalanderType
    {
        Gregorian,
        Lunar,
    }

    public enum TimeFormat
    {
        HH_MM_SS,
        H_MM_SS,
        HH_MM,
        H_MM,
        hh_mm_ss,
        h_mm_ss,
        hh_mm,
        h_mm
    }

    public enum BarCodeStandard
    {
        UPC_A = 34,
        UPC_E = 37,
        EAN = 13,
        Code_93 = 25,
        Standard_128 = 20,
        Code_IATA = 4,
        Code_Industrial = 7,
        Code_Matrix = 2,
        Code_Interleaved = 3,
        Codabar = 18,
        Code_39_ISO_16388 = 8,
        Code_39_ISO_Extended = 9,
        MSI_Plessey = 47,
        UCC_EAN_128 = 16,

        // 2D
        Standard_pdf_417 = 55,
        Data_Matrix_ISO_16022 = 71,
        QR = 58
    }
    

    public enum BarCodeCheckDigit
    {
        None = 0,
        Mod_43 = 1,
        Mod_10 = 1,
        Mod_10_And_Mod_10 = 2,
        Mod_11 = 3,
        Mod_11_And_Mod_10 = 4,
    }

    public enum Rotation
    {
        Angle_90,
        Angle_180,
        Angle_270,
        None
    }

    public enum InteractiveFieldType
    {
        editfield,
        radiobutton,
        button,
        combobox,
        checkbox,
        listbox,
        signaturepad,
        uploadfilebutton,
        datepicker,
        datetimepicker,
        uploadembeddedimage,
        IsTextBox, //=>editfield
        IsRadioButton, //radiobutton,
        IsPickList, //combobox,
        IsMultiPickList, //listbox,
        IsCheckBox, //checkbox,
        IsSignaturePad, //signaturepad,
        IsUploadFile,//uploadfilebutton
        IsDatePicker,//datepicker
        IsDatetimePicker,//datetimepicker
        IsUploadEmbeddedImage,//uploadembeddedimage
        IsButton //button
    }

    public enum InteractiveShapeStyle
    {
        check,
        circle,
        cross,
        diamond,
        square,
        star
    }

    public enum InteractiveFormField
    {
        [XmlEnum("0")]
        visible = 0,
        [XmlEnum("1")]
        hidden = 1,
        [XmlEnum("2")]
        vissble_but_doesnt_print = 2,
        [XmlEnum("3")]
        hidden_but_printable = 3 
    }

    public enum InteractiveOrientation
    {
        [XmlEnum("0")]
        orientation_0 = 0,
        [XmlEnum("90")]
        orientation_90 = 90,
        [XmlEnum("180")]
        orientation_180 = 180,
        [XmlEnum("270")]
        orientation_270 = 270
    }

    [Flags]
    public enum InteractiveFileUploadButtonFileTypes
    {
        [XmlEnum("image")]
        Image = 1,
        [XmlEnum("pdf")]
        PDF = 2
    }

    public enum InteractiveLineThickness
    {
        none,
        thin,
        medium,
        thick
    }

    public enum InteractiveLineStyle
    {
        none,
        solid,
        dashed,
        beveled,
        inset,
        underline
    }

    public enum InteractiveTextDirection
    {
        [XmlEnum("0")]
        auto = 0,
        [XmlEnum("1")]
        ltr = 1,
        [XmlEnum("2")]
        rtl = 2
    }

    public enum InteractiveAlignment
    {
        [XmlEnum("0")]
        left = 0,
        [XmlEnum("1")]
        center = 1,
        [XmlEnum("2")]
        right = 2
    }

    public enum InteractivePosition
    {
        [XmlEnum("0")]
        bottomLeft = 0,
        [XmlEnum("1")]
        bottomCenter = 1,
        [XmlEnum("2")]
        bottomRight = 2
    }

    public enum InteractiveDefaultValueType
    {
        [XmlEnum("0")]
        Const = 0,
        [XmlEnum("1")]
        Applicative = 1,
        [XmlEnum("2")]
        ThisTag = 2
    }

    public enum InteractiveButtonLayout
    {
        [XmlEnum("0")]
        label = 0,
        [XmlEnum("1")]
        icon = 1,
        [XmlEnum("2")]
        icon_top_label_bottom = 2,
        [XmlEnum("3")]
        label_top_icon_botom = 3,
        [XmlEnum("4")]
        icon_left_label_right = 4,
        [XmlEnum("5")]
        label_left_icon_right = 5,
        [XmlEnum("6")]
        label_over_icon = 6
    }


    public enum ChartType
    {
        Column,
        Line,
        Pie,
        StackedColumn,
        Bank1,
        Bank2,
        Bank1BL,
        Bank2BL,
        Bank3BL,
        Bank4BL
    }
}

