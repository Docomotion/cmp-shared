namespace Docomotion.Shared.EngineUtils
{
    public struct PrefixNS
    {
        public const string PKG = "http://schemas.microsoft.com/office/2006/xmlPackage";
        public const string VE = "http://schemas.openxmlformats.org/markup-compatibility/2006";
        public const string V = "urn:schemas-microsoft-com:vml";
        public const string W = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
        public const string O = "urn:schemas-microsoft-com:office:office";
        public const string R = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
        public const string M = "http://schemas.openxmlformats.org/officeDocument/2006/math";
        public const string WP = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing";
        public const string W10 = "urn:schemas-microsoft-com:office:word";
        public const string WNE = "http://schemas.microsoft.com/office/word/2006/wordml";
        public const string A = "http://schemas.openxmlformats.org/drawingml/2006/main";
        public const string PIC = "http://schemas.openxmlformats.org/drawingml/2006/picture";
        public const string XSI = "http://www.w3.org/2001/XMLSchema-instance";
        public const string XSL = "http://www.w3.org/1999/XSL/Transform";
        public const string DC = "http://purl.org/dc/elements/1.1/";
        public const string CP = "http://schemas.openxmlformats.org/package/2006/metadata/core-properties";
        public const string WPS = "http://schemas.microsoft.com/office/word/2010/wordprocessingShape";
        public const string MC = "http://schemas.openxmlformats.org/markup-compatibility/2006";
        public const string WPC = "http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas";
        public const string WP14 = "http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing";
        public const string W14 = "http://schemas.microsoft.com/office/word/2010/wordml";
        public const string W15 = "http://schemas.microsoft.com/office/word/2012/wordml";
        public const string WPG = "http://schemas.microsoft.com/office/word/2010/wordprocessingGroup";
        public const string WPI = "http://schemas.microsoft.com/office/word/2010/wordprocessingInk";
    }

    public struct TagName
    {
        public const string PKG_PACKAGE = "pkg:package";
        public const string PKG_PART = "pkg:part";
        public const string PKG_XMLDATA = "pkg:xmlData";
        public const string PKG_BINARYDATA = "pkg:binaryData";

        //------------------------------------------------------
        public const string RELATIONSHIPS = "Relationships";
        public const string RELATIONSHIP = "Relationship";

        //------------------------------------------------------
        public const string DC_TITLE = "dc:title";
        public const string DC_SUBJECT = "dc:subject";
        public const string DC_CREATOR = "dc:creator";
        public const string DC_DESCRIPTION = "dc:description";

        //------------------------------------------------------
        public const string CP_CATEGORY = "cp:category";
        public const string CP_CONTENTSTATUS = "cp:contentStatus";
        public const string COREPROPERTIES = "cp:coreProperties";

        //------------------------------------------------------
        public const string W_SETTINGS = "w:settings";
        public const string W_EVENANDODDHEADERS = "w:evenAndOddHeaders";
        public const string W_DEFAULTTABSTOP = "w:defaultTabStop";
        public const string W_THEMEFONTLANG = "w:themeFontLang";
        public const string W_DOCUMENT = "w:document";
        public const string W_BODY = "w:body";
        public const string W_STYLES = "w:styles";
        public const string W_STYLE = "w:style";
        public const string W_DOCDEFAULTS = "w:docDefaults";

        public const string W_RPRDEFAULT = "w:rPrDefault";
        public const string W_RPR = "w:rPr";
        public const string W_PPRDEFAULT = "w:pPrDefault";
        public const string W_PPR = "w:pPr";

        public const string W_STRIKE = "w:strike";
        public const string W_CAPS = "w:caps";
        public const string W_B = "w:b";
        public const string W_BCS = "w:bCs";
        public const string W_COLOR = "w:color";
        public const string W_SZ = "w:sz";
        public const string W_SZCS = "w:szCs";
        public const string W_RTL = "w:rtl";
        public const string W_I = "w:i";
        public const string W_ICS = "w:iCs";
        public const string W_RFONTS = "w:rFonts";
        public const string W_RSTYLE = "w:rStyle";
        public const string W_U = "w:u";
        public const string W_VERTALIGN = "w:vertAlign";
        public const string W_HIGHLIGHT = "w:highlight";
        public const string W_SHD = "w:shd";
        public const string W_SPACING = "w:spacing";

        public const string W_NAME = "w:name";
        public const string W_BASEDON = "w:basedOn";

        public const string W_BIDI = "w:bidi";
        public const string W_IND = "w:ind";
        public const string W_JC = "w:jc";
        public const string W_KEEPLINES = "w:keepLines";
        public const string W_KEEPNEXT = "w:keepNext";
        public const string W_NUMPR = "w:numPr";
        public const string W_PSTYLE = "w:pStyle";
        public const string W_TABS = "w:tabs";
        public const string W_TAB = "w:tab";
        public const string W_CONTEXTUALSPACING = "w:contextualSpacing";
        public const string W_WIDOWCONTROL = "w:widowControl";

        public const string W_ILVL = "w:ilvl";
        public const string W_NUMID = "w:numId";

        public const string W_TBLPR = "w:tblPr";
        public const string W_BIDIVISUAL = "w:bidiVisual";
        public const string W_TBLCELLMAR = "w:tblCellMar";
        public const string W_TBLSTYLE = "w:tblStyle";
        public const string W_TBLLAYOUT = "w:tblLayout";
        public const string W_TBLBORDERS = "w:tblBorders";
        public const string W_TBLW = "w:tblW";
        public const string W_TBLIND = "w:tblInd";
        public const string W_TBLPPR = "w:tblpPr";
        public const string W_TBLLOOK = "w:tblLook";
        public const string W_TBLSTYLEROWBANDSIZE = "w:tblStyleRowBandSize";
        public const string W_TBLSTYLECOLBANDSIZE = "w:tblStyleColBandSize";

        public const string W_TOP = "w:top";
        public const string W_BOTTOM = "w:bottom";
        public const string W_LEFT = "w:left";
        public const string W_RIGHT = "w:right";
        public const string W_INSIDEH = "w:insideH";
        public const string W_INSIDEV = "w:insideV";
        public const string W_TL2BR = "w:tl2br";
        public const string W_TR2BL = "w:tr2bl";
    
        //------------------------------------------------------

        public const string A_THEME = "a:theme";
        public const string A_THEMEELEMENTS = "a:themeElements";
        public const string A_FONTSCHEME = "a:fontScheme";
        public const string A_MAJORFONT = "a:majorFont";
        public const string A_MINORFONT = "a:minorFont";
        public const string A_LATIN = "a:latin";
        public const string A_EA = "a:ea";
        public const string A_CS = "a:cs";
        public const string A_FONT = "a:font";

    }

    public struct AttrName
    {
        public const string PKG_NAME = "pkg:name";
        public const string PKG_CONTENTTYPE = "pkg:contentType";
        public const string PKG_PADDING = "pkg:padding";

        //------------------------------------------------------
        public const string XMLNS = "xmlns";
        public const string ID = "Id";
        public const string TYPE = "Type";
        public const string TARGET = "Target";
        public const string NAME = "name";
        public const string TYPEFACE = "typeface";
        public const string SCRIPT = "script";


        //------------------------------------------------------
        public const string W_VAL = "w:val";
        public const string W_BIDI = "w:bidi";
        public const string W_EASTASIA = "w:eastAsia";
        public const string W_THEMECOLOR = "w:themeColor";
        public const string W_COLOR = "w:color";
        public const string W_THEMEFILL = "w:themeFill";
        public const string W_FILL = "w:fill";
        public const string W_ASCII = "w:ascii";
        public const string W_CS = "w:cs";
        public const string W_HANSI = "w:hAnsi";
        public const string W_ASCIITHEME = "w:asciiTheme";
        public const string W_CSTHEME = "w:cstheme";
        public const string W_EASTASIATHEME = "w:eastAsiaTheme";
        public const string W_HANSITHEME = "w:hAnsiTheme";
        public const string W_LEFT = "w:left";
        public const string W_RIGHT = "w:right";
        public const string W_HANGING = "w:hanging";
        public const string W_FIRSTLINE = "w:firstLine";
        public const string W_HINT = "w:hint";

        public const string W_BEFORE = "w:before";
        public const string W_BEFORELINES = "w:beforeLines";
        public const string W_BEFOREAUTOSPACING = "w:beforeAutospacing";
        public const string W_AFTER = "w:after";
        public const string W_AFTERLINES = "w:afterLines";
        public const string W_AFTERAUTOSPACING = "w:afterAutospacing";
        public const string W_LINE = "w:line";
        public const string W_LINERULE = "w:lineRule";

        public const string W_TYPE = "w:type";
        public const string W_DEFAULT = "w:default";
        public const string W_STYLEID = "w:styleId";

        public const string W_POS = "w:pos";
        public const string W_W = "w:w";

        public const string W_SZ = "w:sz";

    }
}
