namespace Docomotion.Shared.ComDef
{
    sealed public partial class FreeFormDefinitions
    {
        public const int FORM_NAME_GROUP_SYMBOL_POS = 2;
        public const int FORM_NAME_GROUP_SYMBOL_LENGTH = 3;

        public const string FORM_NAME_VERSION_PREFIX = "v";

        public const string XSD_ATTR_LF = "lineBreak";
        public const string XSD_ATTR_SCHEMA_ROOT = "schemaRoot";

        public const string SUFFIX_BINARY_PICTURE_TEMPLATE = "_binary_picture";

        public const string SUFFIX_BARCODE_PICTURE_TEMPLATE = "_barcode_picture";

        public const string SUFFIX_CHART_PICTURE_TEMPLATE = "_chart_picture";

        public const string XMV_ATTR_LF = "w:LF";

        public const string XMV_ATTR_CUSTOM_TAG_TEXT = "w:customXmlText";

        public const string XMV_ATTR_CUSTOM_NEW_SCRIPT = "forward";

        public const string XMV_ATTR_EXTERNAL_TEXT_WITH_PREFIX = "w:externalText";
        public const string XMV_ATTR_EXTERNAL_TEXT = "externalText"; //like table block separator
        public const string XMV_ATTR_EXTERNAL_TEXT_STYLE_PREV_TEXT = "prev_text"; //default

        public const string XMV_ATTR_RT_DISALLOW = "rt_disallow";
        public const string XMV_ATTR_RT_FLATNESS = "rt_flatness";
        public const string XMV_ATTR_RT_HIDDEN = "rt_hidden";
        public const string XMV_ATTR_RT_PREFIX = "rt_";

        public const string CUSTOM_XML_TAG_TYPE_VIDEO = "multimedia";
        public const string CUSTOM_XML_TAG_TYPE_RTF = "inputrtf";
        public const string CUSTOM_XML_TAG_TYPE_RTF_WITH_PREFIX = "w:inputRtf";

        public const string CUSTOM_XML_TAG_TYPE_HTML = "inputhtml";
        public const string CUSTOM_XML_TAG_TYPE_HTML_WITH_PREFIX = "w:inputHtml";

        public const string CUSTOM_XML_TAG_TYPE_MSWORD = "inputmsword";
        public const string CUSTOM_XML_TAG_TYPE_MSWORD_WITH_PREFIX = "w:inputMsword";

        public const string CUSTOM_XML_TAG_NAME_OF_ATTR_VIDEO = "videoLink";

        public const string NAME_RTF_FIELD_TEMPLATE = "rtf_field";
        public const string NAME_HTML_FIELD_TEMPLATE = "html_field";
        public const string NAME_MSWORD_FIELD_TEMPLATE = "msword_field";

        public const float DEGREE_AS_RADIAN = 0.0174532925F;

        public const float EMUS_DIVISOR = 12700F;

        public const string FF_U_ROOT = "FF_U_ROOT";
        public const string SINGLE_SCHEMA_ROOT = "ROOT";
        public const string PSEUDO_TAG_NAME = "AfPseudoTag";

        public const string TAG_SCHEMA_PATH = "schemaPath";
        
        public const string TAG_INTERACTIVE_SCHEMA_PATH = "interactivePath";
        public const string ROOT_INTERACTIVE_FIELDS = "ROOT_Interactive_Fields";
        public const string NAME_INTERACTIVE_TAG_VALUE = "interactive_value";
        public const string NAME_DISPLAY_NAME_TAG = "displayName";
        public const string INTERACTIVE_FIELD_NAME_SEPARATOR = "___InFlSp_";
        public const string INTERACTIVE_IAC_URL_INI_PARAM = "iac_handler_url";
        public const string INTERACTIVE_IAC_URL_SCRIPT_HOLDER = "$FFIACHANDLER";
        public const string INTERACTIVE_FIELD_NAME_DOTS_ALIAS = "_doottt_";
        public const string NAME_INTERACTIVE_TAG_DEFAULT_VALUE = "interactive_default_value";

        public const string ITAG_EXT_SCRIPTS_COMMON = "common";
        public const string ITAG_EXT_SCRIPTS_SIGNATUREPAD = XMVInteractiveFieldDefinitions.SIGNATURE_PAD;
        public const string ITAG_EXT_SCRIPTS_DATETIMEPICKER = XMVInteractiveFieldDefinitions.DATETIME_PICKER;
        public const string ITAG_EXT_SCRIPTS_DATEPICKER = XMVInteractiveFieldDefinitions.DATE_PICKER;
        public const string ITAG_EXT_SCRIPTS_UPLOADEMBEDDEDIMAGE = XMVInteractiveFieldDefinitions.UPLOAD_EMBEDDED_IMAGE;

        public const string ITAG_EXT_SCRIPTS_TEXTFIELD = XMVInteractiveFieldDefinitions.TEXTFIELD;
        public const string ITAG_EXT_SCRIPTS_RADIOBUTTON = XMVInteractiveFieldDefinitions.RADIOBUTTON;
        public const string ITAG_EXT_SCRIPTS_PUSHBUTTON = XMVInteractiveFieldDefinitions.PUSHBUTTON;
        public const string ITAG_EXT_SCRIPTS_DROPDOWNLIST = XMVInteractiveFieldDefinitions.DROPDOWNLIST;
        public const string ITAG_EXT_SCRIPTS_CHECKBOX = XMVInteractiveFieldDefinitions.CHECKBOX;
        public const string ITAG_EXT_SCRIPTS_LISTBOX = XMVInteractiveFieldDefinitions.LISTBOX;
        public const string ITAG_EXT_SCRIPTS_UPLOAD_FILE_BUTTON = XMVInteractiveFieldDefinitions.UPLOAD_FILE_BUTTON;

        public const string INTERACTIVE_SUBMIT_SCRIPT_MESSAGE = "Please fill in all mandatory fields before submitting";

        public const string CHART_ATTR_SIZE = "Size";

        public const int MSWORD_DEFAULT_RESOLUTION = 96;

        public class InteractiveDefaultValueType
        {
            public const int CONSTANT = 0;
            public const int APPLICATIVE_TAG = 1;
            public const int THIS_TAG = 2;
        }

        public enum IFIELD_ACTION_ON_SUBMIT
        {//"0 - hide, 1 - show as symbol"
            HIDE, //
            SHOW_SYMBOL
        }

        //deleted public const string XSLT_ERROR_VAL_INFINITY = "Infinity";
        public const string XSLT_ERROR_VAL_NAN = "NaN";
        public const string XSLT_ERROR_VAL_NAM = "NaM";
        public const string XSLT_ERROR_VAL_NAF = "NaF";

        public const int RT_FORM_CACHE_SIZE = 100;
        public const int RT_CORES_NUMBER = 1;
        public const int RT_MEMORY_USAGE = 85;
        public const int RT_LC_VALID_DAYS = 90;
        public const int RT_FORM_CACHE_EXPIRATION = 60; //in minutes

        public const int RT_RUN_GC_AFTER_RUNS = 100;

        public const string SIMPLEX_PRINTING = "1";
        public const string DUPLEX_PRINTING = "2";

        public const string FFI_JOB_TYPE_TASK = "Task";
        public const string FFI_JOB_TYPE_SINGLE = "Single";
        public const string FFI_JOB_TYPE_BATCH = "Batch";
        public const string FFI_JOB_TYPE_TASK_SEPARETED = "Task Separated";

        public const string MANIPULATION_TYPE_CONCATENATE = "Concatenate";
        public const string MANIPULATION_TYPE_MAKE_PARENT = "make_parent";
        public const string MANIPULATION_TYPE_ATTRIBUTE_VALUES = "AttributeValues";

        public const string PROJECT_SYMBOLS_OF_DEFAULT_PROJECT = "PRJ";

        public const int INSERT_MISSING_TAGS_BEHAVIOR_WO_TABLE = 0;
        public const int INSERT_MISSING_TAGS_BEHAVIOR_WITH_TABLE = 1;//default

        public class COMPILED_FRR_TYPE
        {
            public const int FFR1 = 1;
            public const int FFR2 = 2;
            public const int FFR3 = 3;
        }

        public class PAPER_DIMENSIONS
        {
            //in pt
            public const float A3_HEIGHT = 1190.7F;
            public const float A3_WIDTH = 841.95F;

            public const float A4_HEIGHT = 841.95F;
            public const float A4_WIDTH = 595.35F;

            public const float A5_HEIGHT = 595.35F;
            public const float A5_WIDTH = 419.55F;

            public const float LETTER_HEIGHT = 792F;
            public const float LETTER_WIDTH = 612F;

            public const float LEGAL_HEIGHT = 1008F;
            public const float LEGAL_WIDTH = 612F;

            public const float EXECUTIVE_HEIGHT = 756F;
            public const float EXECUTIVE_WIDTH = 522F;
        }

        public const float FACTOR_CONVERT_MM_TO_TWIPS = 56.7F; //1 mm = 56.7 Twips.

        public const int CODE_PAGE_HEBREW_ISO_VISUAL = 28598;
        public const int UTF8 = 65001;

        public class JavaScriptXml
        {
            public const string JC_ACTION_PATH = ".//actions/action";
            public const string JC_EVENT_NAME_PATH = "./event_name";
            public const string JC_VALUE_PATH = "./script/value";
            public const string JC_PREDEFINED_TYPE_PATH = "./script/predefined";
            public const string JC_SCRIPT_TYPE_PATH = "./script/scriptType";
            public const string JC_SCRIPT_DATA_PATH = "./script/scriptData";
        }

        public class JavaScriptHTMLEventsBody
        {
            public const string ON_AFTERPRINT = "onafterprint";   // Script to be run after the document is printed
            public const string ON_BEFOREPRINT = "onbeforeprint";  // Script to be run before the document is printed
            public const string ON_BEFOREUNLOAD = "onbeforeunload"; // Script to be run when the document is about to be unloaded
            public const string ON_ERROR = "onerror"; //  Script to be run when an error occurs
            public const string ON_HASHCHANGE = "onhashchange"; //  Script to be run when there has been changes to the anchor part of the a URL
            public const string ON_LOAD = "onload";// Fires after the page is finished loading
            public const string ON_MESSAGE = "onmessage";//  Script to be run when the message is triggered
            public const string ON_OFFLINE = "onoffline";// Script to be run when the browser starts to work offline
            public const string ON_ONLINE = "ononline"; //  Script to be run when the browser starts to work online
            public const string ON_PAGEHIDE = "onpagehide";// Script to be run when a user navigates away from a page
            public const string ON_PAGESHOW = "onpageshow";// Script to be run when a user navigates to a page
            public const string ON_POPSTATE = "onpopstate";//  Script to be run when the window's history changes
            public const string ON_RESIZE = "onresize";//  Fires when the browser window is resized
            public const string ON_STORAGE = "onstorage";// Script to be run when a Web Storage area is updated
            public const string ON_UNLOAD = "onunload"; // Fires once a page has unloaded(or the browser window has been closed)
        }

        public class XMVInteractiveFieldDefinitions
        {
            #region params defines
            public const string TEXTFIELD = "editfield";
            public const string RADIOBUTTON = "radiobutton";
            public const string PUSHBUTTON = "button";
            public const string DROPDOWNLIST = "combobox";
            public const string CHECKBOX = "checkbox";
            public const string LISTBOX = "listbox";
            public const string SIGNATURE_PAD = "signaturepad";
            public const string UPLOAD_FILE_BUTTON = "uploadfilebutton";
            public const string DATETIME_PICKER = "datetimepicker";
            public const string DATE_PICKER = "datepicker";
            public const string UPLOAD_EMBEDDED_IMAGE = "uploadembeddedimage";

            public const string ISTEXTBOX = "istextbox";
            public const string ISSIGNATUREPAD = "issignaturepad";
            public const string ISUPLOADFILE = "isuploadfile";
            public const string ISCHECKBOX = "ischeckbox";
            public const string ISMULTIPICKLIST = "ismultipicklist";
            public const string ISPICKLIST = "ispicklist";
            public const string ISDATEPICKER = "isdatepicker";
            public const string ISDATETIMEPICKER = "isdatetimepicker";
            public const string ISUPLOADEMBEDDEDIMAGE = "isuploadembeddedimage";
            public const string ISBUTTON = "isbutton";
            public const string ISRADIOBUTTON = "isradiobutton";

            public const int PARAGRAPH_DIRECTION = 0;
            public const int LTR = 1;
            public const int RTL = 2;

            public const int LAYOUT_BUTTON_LABEL_ONLY = 0;
            public const int LAYOUT_BUTTON_ICON_ONLY = 1;
            public const int LAYOUT_BUTTON_ICON_TOP_LABEL_BOTTOM = 2;
            public const int LAYOUT_BUTTON_LABEL_TOP_ICON_BOTTOM = 3;
            public const int LAYOUT_BUTTON_ICON_LEFT_LABEL_RIGHT = 4;
            public const int LAYOUT_BUTTON_LABEL_LEFT_ICON_RIGHT = 5;
            public const int LAYOUT_BUTTON_LABEL_OVER_ICON = 6;

            public const string BORDER_WEIGHT_NONE = "none";
            public const string BORDER_WEIGHT_THIN = "thin";
            public const string BORDER_WEIGHT_MEDIUM = "medium";
            public const string BORDER_WEIGHT_THICK = "thick";

            public const string BORDER_STYLE_NONE = "none";
            public const string BORDER_STYLE_SOLID = "solid";
            public const string BORDER_STYLE_DASHED = "dashed";
            public const string BORDER_STYLE_BEVELED = "beveled";
            public const string BORDER_STYLE_INSET = "inset";
            public const string BORDER_STYLE_UNDERLINE = "underline";

            public const int ALIGNMENT_LEFT = 0;
            public const int ALIGNMENT_CENTER = 1;
            public const int ALIGNMENT_RIGHT = 2;

            public const string CHECK = "check";
            public const string CIRCLE = "circle";
            public const string CROSS = "cross";
            public const string DIAMOND = "diamond";
            public const string SQUARE = "square";
            public const string STAR = "star";

            public const string ON_SET_FOCUS = "OnSetFocus";
            public const string ON_LOST_FOCUS = "OnLostFocus";
            public const string ON_MOUSE_ENTER = "OnMouseEnter";
            public const string ON_MOUSE_EXIT = "OnMouseExit";
            public const string ON_MOUSE_UP = "OnMouseUp";
            public const string ON_MOUSE_DOWN = "OnMouseDown";

            public const int FIELD_VISIBLE = 0;
            public const int FIELD_HIDDEN = 1;
            public const int FIELD_VISIBLE_NOT_PRINT = 2;
            public const int FIELD_HIDDEN_BUT_PRINTABLE = 3;

            public const int FIELD_ROTATION_0 = 0;
            public const int FIELD_ROTATION_90 = 90;
            public const int FIELD_ROTATION_180 = 180;
            public const int FIELD_ROTATION_270 = 270;

            public const string BUTTON_CHECKED_VALUE = "on";
            #endregion
        }

        public const string WML_SECT_PROP_ATTR_ID = "w:rsidSect";
        public const string HTML_OUTPUT_SECT_PROP_ATTR_ID = "ff_sect_id";
        public const string HTML_OUTPUT_PARA_TOC = "ff_is_toc";
        public const string HTML_OUTPUT_IS_BOOKMARK_START = "ff_is_bookmarkstart";
        public const string HTML_OUTPUT_IS_TAB = "ff_is_tab";
        public const string HTML_OUTPUT_HAS_PARA_STYLE = "ff_has_style";
        public const string HTML_OUTPUT_PARA_PROP = "ff_para_prop";
        public const string HTML_OUTPUT_PAGE_BREAK = "ff_page_break";
        public const string HTML_OUTPUT_PAGE_PART = "ff_page_part";
        public const string HTML_OUTPUT_PAGE_FOOTER_VALUE = "footer";
        public const string HTML_OUTPUT_PAGE_HEADER_VALUE = "header";
        public const string HTML_OUTPUT_PAGE_END_LINE_ID = "ff_page_end_line";
        public const string HTML_OUTPUT_OPEN_CONTAINER = "ff_container_open";
        public const string HTML_OUTPUT_CLOSE_CONTAINER = "ff_container_close";
    }
}
