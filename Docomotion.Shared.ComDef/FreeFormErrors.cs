namespace Docomotion.Shared.ComDef
{
    public class Lapsus
    {
        public const int FF_NO_ERRORS = 0;

        public class Compiler
        {
            public const int ERR_WML2XSL_COMMON_ERROR = 9101;
            public const int ERR_WML2XSL_WML_FILE_NOT_FOUND = 9102;
            public const int ERR_WML2XSL_CANNOT_LOAD_WML_DOC = 9105;
            public const int ERR_WML2XSL_IMPOT_WML_ROOT = 9106;
            public const int ERR_WML2XSL_ADD_XML_DECL = 9107;
            public const int ERR_WML2XSL_ADD_XSL_ROOT = 9108;
            public const int ERR_WML2XSL_CANNOT_ADD_XSL_OUTPUT_TAG = 9109;
            public const int ERR_WML2XSL_ADD_XSL_MAIN_TEMPLATE = 9110;
            public const int ERR_WML2XSL_ADD_XSL_PROC_INSTR = 9111;
            public const int ERR_WML2XSL_GET_SCHEMA_INFO = 9112;
            public const int ERR_WML2XSL_GET_SCHEMA_TAGLIST = 9113;
            public const int ERR_WML2XSL_ADD_APPLYTEMPLATE_TAG = 9114;
            public const int ERR_WML2XSL_CREATE_THE_APPLYTEMPLATE = 9115;
            public const int ERR_WML2XSL_GETFORMMAP = 9116;
            public const int ERR_WML2XSL_CREATEXSLDOC = 9117;
            public const int ERR_WML2XSL_SCHEMANOTMATCHED = 9118;
            public const int ERR_WML2XSL_REMOVE_SCHEMA_ROOT = 9121;
            public const int ERR_WML2XSL_ADD_SHEMA_ROOT = 9122;
            public const int ERR_WML2XSL_TREAT_FLDCHAR = 9123;
            public const int ERR_WML2XSL_ADD_SHAPETYPES = 9124;
            public const int ERR_WML2XSL_CANNOT_SAVE_XSL_DOC = 9125;

            public const int ERR_SUBFORM_COMMON_ERROR = 9900;
            public const int ERR_SUBFORM_NO_CONTAINER = 9901;
            public const int ERR_SUBFORM_NO_FILE = 9902;
        }

        public class RT
        {
            public const int ERR_FAIL_TO_LOAD_RENDER = 7051;

            public const int ERR_FFRSEND_ENGINE_EXCEPTION = 8010;
            
            public const int ERR_EXCEPTION_OCCURRED = 1001; //if a exception in program was happened
            public const int ERR_CAN_NOT_LOAD_ANALYZER_SPEC_LIBRARY = 1003; //can not load Spacel engine dll
            public const int ERR_CAN_NOT_GET_PROC_ADDRESS = 1004;//can not get the ProcAddress

            public const int ERR_NO_STARTING_POINT_FOUND = 1130; //not found the tag StartingPoint in the xml maping buffer  	

            public const int ERR_CONFIGURE_INI_IS_MISSING_IN_API_DIR = 7400; // case FreeFormRuntime.config is missing 
            public const int ERR_CONFIGURE_INI_GET_ERROR = 7499; // get FreeFormRuntime.config  error

            public const int ERR_INPUT_BUFFER_IS_NULL = 7101;
            public const int ERR_INPUT_BUFFER_IS_CORRUPTED = 7103;

            public const int ERR_FORM_ID_MISSING_IN_HEADER = 7208;

            public const int ERR_DATA_AFTER_HEADER_END_IS_MISSING = 7211;  //  case no  data after the FreeFrom header

            public const int ERR_OBJ_XML_READER_FAILED_TO_READ = 7301; 	// case Xml header syntax is mistaken or corrupt
            public const int ERR_PROJECT_SYMBOL_MISSING_IN_HEADER = 7304; 	// case project_symbol is missing in Xml header & can't read from In
            public const int ERR_PROJECT_SYMBOL_LENGTH_IS_NOT_AS_REQUIRED = 7305;	// case project_symbol length is not as required by ffp format name

            public const int ERR_TECHNOLOGY_IS_MISSING = 7404; 	// case technology is missing in Xml header Input & Ini file
            public const int ERR_TECHNOLOGY_VALUE_IS_ILLEGAL = 7405; 	// case technology from Xml header or Ini file contain illegal value

            public const int ERR_INTER_FORM_WITH_NO_INTERACTIVE_FORMAT = 7330;

            public const int ERR_TASK_XML_BUFFER_CORRUPTED = 7460;// The Xml of the Task is been corrupted

            public const int ERR_CODE_PAGE_NOT_NUMBER = 7418;	// case code_page contain character that is not numbers in Xml header Input or Ini file
            public const int ERR_INCOMPATIBLE_CODE_PAGE = 7420;   // case mapping code page and ffi code page not matched

            public const int ERR_TECHNOLOGY_IS_VIEW_BUT_DESTINATION_MISSING = 7428;


            public const int ERR_TECHNOLOGY_IS_PRINT_BUT_DESTINATION_MISSING = 7407;	// case destination missing in Xml header Input & Ini file
            public const int ERR_PRINT_DESTINATION_VALUE_IS_ILLEGAL = 7409;	// case Print Destination from Xml header contain illegal value
            public const int ERR_DESTINATION_IS_FILE_BUT_FILE_NAME_MISSING = 7410;// case file_name missing in Xml header Input & Ini file
            public const int ERR_TECHNOLOGY_IS_PRINT_BUT_QUEUE_NAME_MISSING = 7406;	// case queue_name missing in Xml header Input & Ini file
            public const int ERR_TECHNOLOGY_IS_PRINT_BUT_SPOOL_JOB_NAME_MISSING = 7408; 	// case spool_job_name missing in Xml header Input & Ini file


            public const int ERR_FORMAT_VALUE_IS_ILLEGAL = 7415;	// case format in Xml header Input or Ini file contain illegal value

            public const int ERR_DIR_IN_VIEW_FILE_NAME_IS_NOT_EXIST = 7413;
            public const int ERR_TECHNOLOGY_IS_VIEW_BUT_FORMAT_MISSING = 7414; 	// case format missing in Xml header Input & Ini file

            public const int ERR_DIR_IN_PRINT_FILE_NAME_IS_NOT_EXIST = 7411;	// case directory in file_name in Xml header Input or Ini file is not exist


            public const int ERR_VIEW_DESTINATION_VALUE_IS_ILLEGAL = 7425;	// case the thchnology is view but destination is missing or empty

            public const int ERR_TECHNOLOGY_IS_VIEW_BUT_FILE_NAME_MISSING = 7412;	// case file_name missing in Xml header Input & Ini file

            public const int ERR_FILE_NAME_IN_VIEW_FILE_NAME_IS_NOT_EXIST = 7417;

            public const int ERR_PATH_IN_VIEW_FILE_NAME_CONTAIN_ILLEGAL_CHAR = 7422;	// case path at file_name in Xml header Input or Ini file contain illegal char
            public const int ERR_PATH_IN_PRINT_FILE_NAME_CONTAIN_ILLEGAL_CHAR = 7421;	// case path at file_name in Xml header Input or Ini file contain illegal char


            public const int ERR_NO_TECHNOLOGY_IN_TASK = 7461; // The technology tag not found in the task xml buffer

            public const int ERR_TASK_THCHNOLOGY_VALUE_INVALID = 7463;

            public const int ERR_FORM_ID_IS_NOT_NUMBER = 7210;	// case form_id contain character that is not numbers

            public const int ERR_FORMS_PATH_IS_MISSING = 7401;

            public const int ERR_VIEW_FILE_NAME_EXIST_BUT_OVERWRITE_IS_0 = 7598; 	// case view file name exist but Overwrite flag is false
            public const int ERR_PRINT_FILE_NAME_EXIST_BUT_OVERWRITE_IS_0 = 7597; 	// case print file name exist but Overwrite flag is false



            public const int ERR_CREATE_PROCESS_TO_RUN_AFTER = 7901;

            public const int ERR_SCALING_PERCENT_VALUE_MISSING = 7601;
            public const int ERR_SCALING_PERCENT_VALUE_INVALID = 7602;
            public const int ERR_SCALING_STANDARD_PAGE_VALUE_MISSING = 7603;
            public const int ERR_SCALING_STANDARD_PAGE_VALUE_INVALID = 7604;
            public const int ERR_SCALING_CUSTOM_SIZE_VALUE_MISSING = 7605;
            public const int ERR_SCALING_CUSTOM_SIZE_VALUE_INVALID = 7606;

            public const int ERR_MANDATORY_TAG_VALUE_MISSING = 1133;

            public const int ERR_OBJECT_ID_LENGTH_IS_NOT_AS_REQUIRED = 8502;
            public const int ERR_OBJECT_ID_IS_NOT_NUMBER = 8503;

            public const int ERR_XMV_COMMON_ERROR = 10000;
            public const int ERR_XMV_OUT_OF_MEMORY = 10001;
            public const int ERR_XMV_XSLT_LOAD_FORM = 10002;
            public const int ERR_XMV_LOAD_FORM = 10003;
            public const int ERR_XMV_LOAD_XML_DATA = 10004;
            public const int ERR_XMV_FFR_TRANSFORMATION = 10005;
            public const int ERR_XMV_LOAD_OBJECTS_PARAM_BUFFER = 10006;

            public const int ERR_RENDER_COMMON_ERROR_MODULE_FILE = 3050;

            public const int ERR_RENDER_MEMORY_FOP_BUFFER = 3059;
            public const int ERR_RENDER_CREATE_OUTPUT_FILE = 3055;

            public const int ERR_RENDER_PRINTING_ERROR = 3105;
            public const int ERR_RENDER_OPEN_PRINTER = 3151;
            public const int ERR_RENDER_START_DOC = 3152;
            public const int ERR_RENDER_WRITE_TO_PRINTER = 3153;
            public const int ERR_RENDER_END_DOC = 3154;
            public const int ERR_RENDER_CLOSE_PRINTER = 3155;

            public const int ERR_FAIL_UPDATE_INTERACTIVE_FFI = 9299;

            public const int ERR_CODE_PAGE_NO_INSTALL = 1132;

            //run time errors of package
            //eroro code Rang 5000-5299
            public const int ERR_PROJECT_SYMBOL_SIZE_IS_INVALID = 5001; // undoc //the project symbol size in to 3 chars
            public const int ERR_UNKNOWN_REQUEST_TYPE = 5002; // undoc //the request id is unknown
            public const int ERR_LC_FATAL_ERROR = 5003; // undoc //Exception hppand in lc Engine
            public const int ERR_PROJECT_SYMBOL_IN_PACKAGE_FILE_NAME_NOT_MATCH_TO_INPUT_ARGUMENT = 5504; // undoc //the FFP Project Symbol  not mach to Project Symbol from input 
            public const int ERR_SEARCH_FILES_PATH_IS_A_FILE_NAME = 5505;// undoc //path is a file name.
            public const int ERR_SEARCH_FILES_THE_CALLER_DOES_NOT_HAVE_THE_REQUIRED_PERMISSION = 5506;// doc//The caller does not have the required permission
            public const int ERR_SEARCH_FILES_SEARCH_PATTERN_DOES_NOT_CONTAIN_A_VALID_PATTERN = 5507;// undoc //path is a zero-length string, contains only white space, or contains one or more invalid characters as defined by InvalidPathChars. -or- searchPattern does not contain a valid pattern
            public const int ERR_SEARCH_FILES_PATH_OR_SEARCH_PATTERN_IS_A_NULL_REFERENCE = 5508;// undoc //path or searchpattern is a null reference
            public const int ERR_SEARCH_FILES_THE_SPECIFIED_PATH_FILE_NAME_OR_BOTH_EXCEED_THE_SYSTEM_DEFINED_MAXIMUM_LENGTH = 5009;// doc //The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters
            public const int ERR_SEARCH_FILES_THE_SPECIFIED_PATH_IS_INVALID = 5010;// doc //The specified path is invalid (for example, it is on an unmapped drive).
            public const int ERR_CAN_NOT_FOUND_PACKAGE_FILE_IN_DISTRIBUTION_POINT = 5011;// doc //can not find mach file in the Distribution Point
            public const int ERR_PACKAGE_FILE_NAME_DOES_NOT_HAVE_THE_MATCH_LENGTH = 5512;// doc //the size of the ffp file name in case of vresion not have the righ size (12 chars)
            public const int ERR_PACKAGE_FILE_NAME_HAS_CORRUPTED_FILE_NAME = 5013;// doc //the file name not have number in the file name but is in the righ format
            public const int ERR_INIT_PACKAGE_FOR_READ_FATAL_ERROR = 5014;// undoc 
            public const int ERR_IS_FORM_EXPIRED_FATAL_ERROR = 5015;// undoc
            public const int ERR_FORM_IS_EXPIRED = 5016;// doc //the form expired  days is small then the time from system start antill toady
            public const int ERR_GET_FORM_INFO_FATAL_ERROR = 5017; //undoc
            public const int ERR_CAN_NOT_FIND_FFR_IN_FFP = 5018;// doc //can not found the ffr id in the ffp file
            public const int ERR_OBJECT_NOT_INIT = 5019; // undoc //the init was not call
            public const int ERR_GET_FFR_DATA_FATAL_ERROR = 5020; // undoc
            public const int ERR_UNSUPPORT_COMPRESSION_TYPE = 5021; // undoc //the Compression Type in unnon
            public const int ERR_UNZIP_FFR_BUFFER = 5022;// undoc //The Uzip Filed
            public const int ERR_IN_OPEN_FFP_FILE = 5023; // doc //error in open ffp file 
            public const int ERR_FFP_INTERNAL_SIGNATURE_CORRUPTED = 5024; // undoc //one of the internal signature values is corrupted
            public const int ERR_FFP_FILE_NAME_SIZE_IS_TO_SHORT = 5025; //undoc -new from 08-02-2010 //when the ffp file name is less then 9
            public const int ERR_INTERNAL_PROJECT_SYMBOL_NOT_MACH_TO_FILE_PROJECT_SYMBOL = 5026; ////undoc -new from 09-03-2010  //when the file Project Symbol not mach to the internal Project Symbol
            public const int ERR_FAIL_TO_FIND_SIZE_FILE = 5027; // undoc new from 11/06/2010


            public const int ERR_ATTACHMENT_FORMAT_NOT_HAVE_VALUE = 8920; //the attachment format not exist or not have value
            public const int ERR_ATTACHMENT_FILE_NAME_NOT_HAVE_VALUE = 8921; //the attachment file name not exist or not have value 
            public const int ERR_SERVER_ADDRESS_NOT_HAVE_VALUE = 8922; //the Server address not exist or not have value

            public const int ERR_MAIL_IS_ILLEGAL = 8925; //the mail address is not in the current format

            public const int ERR_FROM_MAIL_NOT_HAVE_VALUE = 8927; //the from mail  not exist or not have value
            public const int ERR_TO_NOT_HAVE_VALUE = 8928;//the to mail address not exist or not have value

            public const int ERR_ONE_TO_MAIL_ADDRESS_ILLEGAL = 8929;//one of the mails in the to 	address	is 	illegal
            public const int ERR_ONE_CC_MAIL_ADDRESS_ILLEGAL = 8930; //one of the mails in the cc 	address	is 	illegal
            public const int ERR_ONE_BCC_MAIL_ADDRESS_ILLEGAL = 8931; //one of the mails in the bcc address	is 	illegal	

            public const int ERR_MAIL_EXTERNAL_FILE_GENERIC_EXCEPTION = 8942;	//An unspecified error occurred
            public const int ERR_MAIL_EXTERNAL_FILE_NOT_FOUND = 8943;	//The file could not be located

            public const int ERR_MAIL_EXTERNAL_FILE_VALUE_EMPTY = 8956;  // the source is exteranl_file but no valuefor file path

            public const int ERR_ATTACHMENT_FORMAT_IS_NOT_VALID_HTML = 8957;  // the source is html_body but the value is not HTML or HTML4S
            public const int ERR_ATTACHMENT_FORMAT_IS_NOT_VALID_TXT = 8958;  // the source is txt_body but the value is not txt

            public const int ERR_SMTP_EXCEPTION = 2513;
            public const int ERR_SMTP_FAILED_RECIPIENTS_EXCEPTION = 2514;
            public const int ERR_SEND_EMAIL_FATAL_ERROR = 2515;
            public const int ERR_ERROR_CONVERT_ATTACHED_FROM_BASE64 = 2516;

            public const int ERR_FORM_PATH_CONTAIN_ILLEGAL_CHAR = 7402;	//
            public const int ERR_LOAD_FFP_FORM = 8021;
            public const int ERR_LOAD_FORM = 8022;
            public const int ERR_GET_FORM = 8023;
            public const int ERR_LOAD_FIRST_FORM = 8024; //first formin task has error
            public const int ERR_LOAD_FORM_INTERNAL = 8025;

            public const int ERR_OVERLAY_HEADER_IS_MISSING = 7151;
            public const int ERR_OVERLAY_INPUT_BUFFER_IS_CORRUPTED = 7152;
            public const int ERR_OVERLAY_DATA_AFTER_HEADER_END_IS_MISSING = 7153;

            public const int ERR_CLOUD_FORM_LOAD = 8000;
            public const int ERR_CLOUD_FORM_INVALID = 8001;

            public const int ERR_COMMON_ERROR_BATCH_ACTION_FAILED = 8044;

            public const int ERR_CERTIFICATE_PROCESS = 7700; //common error that occurs on signature process
            public const int ERR_CERTIFICATE_PASSWORD_EMPTY = 7701; //password is empty 
            public const int ERR_CERTIFICATE_PASSWORD_INCORRECT = 7702;//invalid password or certificate file is corrupted 
            public const int ERR_CERTIFICATE_FILE_PATH_INVALID = 7703; //certificate file is not found

            public const int ERR_DATA_TAG_VALUE = 9002;
            public const int ERR_DATA_TAG_BARCODE = 9120;
            public const int ERR_DATA_TAG_CHART = 9150;

            public const int ERR_INPUT_RTF_COMMON = 9400; //common error that occurs on RTF converting process
            public const int ERR_INPUT_RTF_INIT = 9401;  // error that occurs on initialisation RTF converter
            public const int ERR_INPUT_RTF_INVALID_FORMAT = 9402; //Input data is not RTF format
            public const int ERR_INPUT_RTF_SOURCE_NOT_FOUND = 9403; //Error on getting the RTF data from URL

            public const int ERR_INPUT_HTML_COMMON = 9450; //common error that occurs on HTML converting process
            public const int ERR_INPUT_HTML_INIT = 9451;  // error that occurs on initialisation RTF converter
            public const int ERR_INPUT_HTML_INVALID_FORMAT = 9452; //Input data is not HTML format

            public const int ERR_INPUT_MSWORD_COMMON = 9460;
            public const int ERR_INPUT_MSWORD_INIT = 9461;  
            public const int ERR_INPUT_MSWORD_INVALID_FORMAT = 9462;
            public const int ERR_INPUT_MSWORD_SOURCE_NOT_FOUND = 9463; //Error on getting the MSWORD data from URL
            public const int ERR_INPUT_MSWORD_APPLY_FAILED = 9464; // Error for the case of error during applying of content

            //Col Row (1200-1299)
            public const int ERR_MISSING_INPUT_PARAMETERS = 1200; //one of the input parameter have missing
            public const int ERR_CONVERT_INPUT_BUFFER_TO_UNICODE = 1201; //error in convert the input buffer to unicode code page
            public const int ERR_NO_MARKER_ID_ATTRIBUTE = 1202; // no Attribute id in the marker Element
            public const int ERR_NO_SEARCH_STRING_ATTRIBUTE_IN_MARKER = 1203; //no search_string attribute in the marker Element
            public const int ERR_DEFINITIONS_VALUES_MISSING = 1204; //one of the  definitions values in the input mapping not found.
            public const int ERR_NOT_FOUND_MANDATORY_MARKER = 1205; //can not found all the mandatory markers that found in the  input mapping
            public const int ERR_NOT_COMPATIBLE_DATA = 1206; //the input data buffer not match to the input mapping info
            public const int ERR_CONVERT_BUFFER_TO_BASE64 = 1207; //error in convert line to base 64
            public const int ERR_CAN_NOT_FIND_TABLE_END_MARKER = 1208; //can not find the table end marker
            public const int ERR_MAPPING_NOT_IN_CORRECT_FORMAT = 1210; //one ore more element or attribute not found in the mapping xml buffer
            public const int ERR_FATAL_BUILD_XML = 1211; // undoc
            public const int ERR_FATAL_BUILD_SINGLE_TAG = 1212; // undoc
            public const int ERR_FATAL_BUILD_TABLE_ELEMENT = 1213; // undoc
            public const int ERR_FATAL_BUILD_TABLE_ROWS = 1214; // undoc
        }

        public class WarningsDefines
        {
            #region //XMV objects warnings 10600 - 10699 -> return 9013

            public const int WARN_XMV_BASE_NUMBER_10600 = 10600;
            public const int WARN_XMV_END_NUMBER_10600 = 10699;
            //--------------------------------------------------------------
            public const int WARN_XMV_COMMON_IMPORT_FORM = 10601;//unexpected error
            public const int WARN_XMV_COMMON_ANALYSE_FORM = 10602;//unexpected error
            public const int WARN_XMV_COMMON_CONVERT_FORM = 10603;//unexpected error
            //--------------------------------------------------------------
            public const int WARN_XMV_WML_SETTINGS = 10604;//the form setting
            //--------------------------------------------------------------
            public const int WARN_XMV_WML_STYLES = 10605;//the list of styles
            public const int WARN_XMV_STYLE_PROPERTIES = 10606;//one of style's properteis 
            //--------------------------------------------------------------
            public const int WARN_XMV_WML_NUMBERING = 10607;//the numbering definition list 
            //--------------------------------------------------------------
            public const int WARN_XMV_WML_DRAWING = 10608; //the picture object
            //--------------------------------------------------------------
            public const int WARN_XMV_WML_THEME_COLOR = 10609;//the theme color object
            public const int WARN_XMV_WML_THEME_FONT = 10610;//the them font object
            //--------------------------------------------------------------
            public const int WARN_XMV_SECTION = 10611;//the section object
            public const int WARN_XMV_SECTION_PROPERTIES = 10612;//one of section's properties
            //--------------------------------------------------------------         
            public const int WARN_XMV_DIFFERENT_HEADER_FOOTER = 10613;//the settings of the header/footer
            //--------------------------------------------------------------         
            public const int WARN_XMV_BLOCK_CONTENT = 10614;//the paragraph content
            //--------------------------------------------------------------         
            public const int WARN_XMV_HEADER = 10615;//the header object
            public const int WARN_XMV_FOOTER = 10616;//the footer object
            public const int WARN_XMV_HEADER_FOOTER_COLLECTION = 10617;//the header/footer definition list 
            //--------------------------------------------------------------         
            public const int WARN_XMV_FOOTNOTES = 10618;
            public const int WARN_XMV_ENDNOTES = 10619;
            public const int WARN_XMV_FOOTNOTES_ENDNOTES_COLLECTION = 10620;
            public const int WARN_XMV_FOOTNOTE_SEPARATOR = 10621;
            //--------------------------------------------------------------         
            public const int WARN_XMV_WML_PARAGRAPH_WITH_FLOATING_OBJECTS = 10622;//the paragraph with the float shape
            public const int WARN_XMV_WML_FLOATING_SHAPE = 10623;//the shape not inline wrapped       
            //--------------------------------------------------------------         
            public const int WARN_XMV_TABLE_CONTENT = 10624;//unexected table content
            public const int WARN_XMV_TABLE_ROW_CONTENT = 10625;
            public const int WARN_XMV_TABLE_CELL_CONTENT = 10626;
            public const int WARN_XMV_TABLE_PROPERTIES = 10627;
            public const int WARN_XMV_TABLE_BORDERS = 10628;
            public const int WARN_XMV_TABLE_ROW_PROPERTIES = 10629;
            public const int WARN_XMV_TABLE_CELL_PROPERTIES = 10630;
            //--------------------------------------------------------------         
            public const int WARN_XMV_PARAGRAPH = 10631;
            public const int WARN_XMV_PARAGRAPH_STYLE = 10632;
            public const int WARN_XMV_PARAGRAPH_PROPERTIES = 10633;
            public const int WARN_XMV_PARAGRAPH_CONTENT = 10634;
            public const int WARN_XMV_PARAGRAPH_NUMBERING = 10635;
            public const int WARN_XMV_PARAGRAPH_LINES = 10636;//the text line of the paragraph
            public const int WARN_XMV_PARAGRAPH_SPACING = 10637;
            public const int WARN_XMV_PARAGRAPH_INDENTATION = 10638;
            //--------------------------------------------------------------         
            public const int WARN_XMV_SIMPLE_FIELD = 10639;
            //--------------------------------------------------------------         
            public const int WARN_XMV_RUN_CONTENT = 10640;//unexpected run content
            public const int WARN_XMV_RUN_TAB_CHARACTER = 10641;
            public const int WARN_XMV_RUN_RIGHT_TAB_CHARACTER = 10642;
            public const int WARN_XMV_RUN_CENTER_TAB_CHARACTER = 10643;
            public const int WARN_XMV_RUN_PROPERTIES = 10644;
            public const int WARN_XMV_RUN_NUMBERING_LEVEL_PROPERTIES = 10645;
            public const int WARN_XMV_RUN_NUMBERING_LEVEL_BY_STYLE = 10646;
            public const int WARN_XMV_RUN_NUMBERING_LABEL = 10647;
            public const int WARN_XMV_RUN_NUMBERING_LABEL_TEXT = 10648;
            public const int WARN_XMV_RUN_TEXT = 10649;
            public const int WARN_XMV_RUN_SHAPE = 10650;
            public const int WARN_XMV_RUN_BREAK = 10651;
            public const int WARN_XMV_RUN_WML_FIELD = 10652;
            public const int WARN_XMV_RUN_SYMBOL_CHARACTER = 10653;
            public const int WARN_XMV_RUN_DRAWING = 10654;
            //--------------------------------------------------------------         
            public const int WARN_WML_OBJECT = 10655; //the attached objects
            public const int WARN_WML_OBJECT_PAGE_DEFINITION = 10656;
            public const int WARN_WML_OBJECT_NUMBERING_STYLES = 10657;
            public const int WARN_WML_OBJECT_NUMBERING_PARAGRAPHS = 10658;
            public const int WARN_WML_OBJECT_NUMBERING_DEFINITION = 10659;
            public const int WARN_WML_OBJECT_FOOTNOTES = 10660;
            public const int WARN_WML_OBJECT_ENDNOTES = 10661;
            //-------------------------------------------------------------- 
            public const int WARN_XMV_INTERACTIVE_FIELD = 10670;
            //--------------------------------------------------------------  
            public const int WARN_XMV_CUSTOM_TAG_DRAWING = 10680;
            //--------------------------------------------------------------         


            #endregion

            //the data warnings 10500 - 10549 -> return 9002
            public const int WARN_XMV_DATA_TAG_MIN_NUMBER = 10500;
            public const int WARN_XMV_DATA_TAG_MAX_NUMBER = 10549;

            public const int WARN_XMV_XSLT_NOT_NUMBER = 10501; // Not Anumber
            public const int WARN_XMV_XSLT_INFINITY = 10502; //divided by zero
            public const int WARN_XMV_XSLT_NAM = 10503;       //  Not applicable mask
            public const int WARN_XMV_XSLT_NAF = 10504;       //  Not applicable function

            //external function engine warnings 10550 - 10599
            public const int WARN_XMV_BASE_NUMBER_EXT_FUNC = 10550;
            //////COMMON_ERROR = 1;
            //////ERROR_CACHE_ENTRY_NOT_FOUND = 2;
            //////ERROR_CONNECT_FAILURE = 3;
            //////ERROR_CONNECTION_CLOSED = 4;
            //////ERROR_KEEP_ALIVE_FAILURE = 5;
            //////ERROR_MESSAGE_LENGTH_LIMIT_EXCEEDED = 6;
            //////ERROR_NAME_RESOLUTION_FAILURE = 7;
            //////ERROR_PENDING = 8;
            //////ERROR_PIPELINE_FAILURE = 9;
            //////ERROR_PROTOCOL_ERROR = 10;
            //////ERROR_PROXY_NAME_RESOLUTION_FAILURE = 11;
            //////ERROR_RECEIVE_FAILURE = 12;
            //////ERROR_REQUEST_CANCELED = 13;
            //////ERROR_REQUEST_PROHIBITED_BY_CACHE_POLICY = 14;
            //////ERROR_REQUEST_PROHIBITED_BY_PROXY = 15;
            //////ERROR_SECURE_CHANNEL_FAILURE = 16;
            //////ERROR_SEND_FAILURE = 17;
            //////ERROR_SERVER_PROTOCOL_VIOLATION = 18;
            //////ERROR_TIMEOUT = 19;
            //////ERROR_TRUST_FAILURE = 20;


            //Barcode genarator warnings 10900 - 10949 -> return 9120


            public const int WARN_XMV_BARCODE_MIN_NUMBER = 10900;
            public const int WARN_XMV_BARCODE_MAX_NUMBER = 10949;
            public const int WARN_XMV_BC_OS_ERROR = 10901; //launching of the barcode engine failed
            //warnings = WARN_XMV_BC_BASE + %Barcode genarator errors%
            //Barcode genarator errors
            //////WARN_INVALID_OPTION = 2; //one of barcode settings is invalid
            //////ERROR_TOO_LONG = 5; //the barcode value is too long for the current symbology
            //////ERROR_INVALID_DATA = 6; //Invalid characters in data
            //////ERROR_INVALID_CHECK = 7; //invalid check digit(s)
            //////ERROR_INVALID_OPTION = 8;// one of the barcode's setting is incorrect
            //////ERROR_ENCODING_PROBLEM = 9;//barcode encoding process problem
            //////ERROR_FILE_ACCESS = 10;//access denied to uotput file
            //////ERROR_MEMORY = 11;//allocation memory error
            //////ERROR_INVALID_SYMBOLOGY_ID = 12; //unknown symbology
            //////ERROR_EMPTY_DATA = 13; //No input data

            //Charting genarator warnings 10950 - 10999 -> return 9150
            public const int WARN_XMV_CHART_MIN_NUMBER = 10950;
            public const int WARN_XMV_CHART_MAX_NUMBER = 10999;
            public const int WARN_XMV_CHART_OS_ERROR = 10951; //launching of the chart engine failed
            //warnings = WARN_XMV_BASE + %charting genarator errors%
            //Charting genarator errors
            //private const int ERROR_MISSING_CHART_DLLS = 2;//missing DataVisualisation dlls(C# chart control)
            //private const int ERROR_COMMON = 3; //common error
            //private const int ERROR_BUILD_CHART = 4;//XML of FDC is invalid according to "C# chart control"
            //private const int ERROR_VALUE_IS_NOT_DOUBLE = 5; //in case graph is not fixed and data type is not string/datetime
            //private const int ERROR_VALUE_IS_NOT_DATE_AND_TIME = 6;//in case graph is not fixed and data type is datetime


        }

  
        public class Packager
        {
            public const int ERR_NO_VALID_FFR_FILES = 9500;     // valid ffr files was not found (probably ffr expired date has arrived) 
            //public const int ERR_FILE_LOADING_FAILED        = 9501;     // NOT ACTIVE fail to load file 
            public const int ERR_SYSTEM_EXCEPTION = 9502;     // system exception was thrwan
            //public const int ERR_FAIL_TO_CREATE_FFS_HEADER  = 9503;     // fail to create FFS Header(delete) by liran on 08-02-2010
            public const int ERR_FILE_DONT_EXIST = 9504;     // file dont exists
            //public const int ERR_FAIL_TO_GET_FFS_COMPONENTS = 9505;     // fail to get FFS inner components(delete)
            public const int ERR_FAIL_TO_FIND_FFS_ID = 9506;     // fail to find ffs id in xml
            //public const int ERR_XML_CORRUPTED               =9507;     // xml is corrupted(delete) by liran on 08-02-2010 --replase by to new error code 
            public const int ERR_SCRIPT_NOT_FOUND = 9508;     // script not found
            public const int ERR_NO_FFS_IN_LIST = 9509;     // no fss list 
            //public const int ERR_NO_FFR_FILES_FOUND          =9510;     // no ffr files where found in input directory
            public const int ERR_CAN_NOT_FOUND_MAPPING_IN_FFR_XML = 9511; //can not found the maping xml (new by liran 08-02-2010) replace the ERR_XML_CORRUPTED error 
            public const int ERR_CAN_NOT_FOUND_FREE_FORM_PROJECT_TAG_IN_FFR_XML = 9512; //can not found the FreeFormProject  (new by liran 08-02-2010) replace the ERR_XML_CORRUPTED error
            public const int ERR_CAN_NOT_FOUND_PROJECT_SCRIPT_CALL_TAG_IN_FFR_XML = 9513;//can not found the projectScriptCall (new by liran 08-02-2010) replace the ERR_XML_CORRUPTED error
            public const int ERR_FAIL_TO_CREATE_PROJECT_SYMBOL_DIR = 9520; // NEW - fail to create inter folder in working directory         
            public const int ERR_FAIL_TO_DELETE_FILES_FROM_WORKING_DIR = 9522; // NEW  
            public const int ERR_FAIL_TO_GET_VALID_FFS = 9523;
            public const int ERR_CORRUPTED_WORKING_DIR = 9524;
            public const int ERR_DUPLICATE_FORMS = 9525;
            public const int ERR_SUBFORMS_VALIDATION_EXCEPTION = 9526;
            public const int ERR_LOAD_DATA_TOXML_BEFORE_SUBFORMS_ASSEMBLER_EXCEPTION = 9527;
            public const int ERR_FFS_HANDELING_EXCEPTION = 9528;
            public const int ERR_CREATEDATA_EXCEPTION = 9529;
            public const int ERR_SUBFORM_DOES_NOT_EXISTS = 9530;
            public const int ERR_CREATEFFRDATAFILE_EXCEPTON = 9531;
            public const int ERR_CREATEHEADEFILE_EXCEPTON = 9532;

            public const int ERR_XSL_COMPILER_FAIL = 9022; //the XslCompiler filed to Compiled the xlst buffer
            public const int ERR_XSL_COMPILER_FATAL_ERROR = 9023; //Exception hppends  in Compiled of the xlast buffer 
            public const int ERR_CAN_NOT_CREATE_TEMP_DATA_FILE = 9024; //can not create the temp data file
            public const int ERR_CAN_NOT_OPEN_COMPILED_FROM_FILE = 9025; //can nnot open the compiled form dll for read
           
            public const int ERR_PACKAGE_UTIL_COMMON = 5300; //the data tag of the from not exist in the ffp 

            public const int ERR_FORM_DATA_TAG_NOT_FOUND_IN_FFP = 5301; //the data tag of the from not exist in the ffp 
            public const int ERR_FFP_HEADER_FATAL_ERROR = 5302;
            public const int ERR_GET_FFR_TAGS_FATAL_ERROR = 5303;
            public const int ERR_GET_PACKAGE_SN_FATAL_ERROR = 5304;
            public const int ERR_FILE_DOES_NOT_EXIST = 5305;// trying to locate a non existed file 
            public const int ERR_FILE_IS_NOT_XML = 5306;// file is not xml file  
            public const int ERR_FAIL_TO_LOAD_FFS_TO_HEADER = 5307;// ffs could not be loaded to the header
            public const int ERR_FAIL_TO_ZIP_FILE = 5308;// zipp process failed
            public const int ERR_FAIL_TO_UPDATE_PROJECT_IN_HEADER_FILE = 5309;
            public const int ERR_INPUT_DIRECTORY_NOT_DEFINE = 5310;   //the inpur directory is null
            public const int ERR_INVALID_PARAMETERS_IN_CREATE_FFP = 5311;   //one or more of the ffp parameter  is missing
            public const int ERR_FFR_FILE_MASK = 5312;   //the version in ffr file is missing
            public const int ERR_OUT_FILE_OR_DIRECTORY_ATTRIBUTE_UNKNOWN = 5313;   //The Out path is not normal file or directory
            public const int ERR_OUT_DIRECTORY_OR_FILE_NAME_IS_CORRUPTED = 5314;   //the out DIRECTORY is not a dir or file
            public const int ERR_CAN_NOT_MOVE_FFP_TEMP_FILE = 5315;   //can move to temp ffp file to out dir
            public const int ERR_CREATE_FFP_FATAL_ERROR = 5316;   //fatal errro in  create ffp
            public const int ERR_FFR_HEADER_IS_CORRUPTED = 5317;    //no FFR files found in the input directory
            public const int ERR_FAIL_TO_FIND_FFR_IN_WORKING_DIR = 5318;


            //LC warning Range 5700-5799
            public const int WARN_CAN_NOT_CREATE_LC_PROJECT_DIRECTORY = 5700;//the create of the sub Directory of the project filed
            public const int WARN_CAN_NOT_CREATE_LC_FILE = 5701;//Create the LC file was filed
            public const int WARN_CAN_NOT_DELETE_LC_FILE = 5702;//the delete of the file in the lc filed
            public const int WARN_CAN_NOT_RENAME_LC_FILE = 5703;//can not reane the Lc file 
            public const int WARN_CAN_NOT_READ_READ_LC_FILE = 5704;//can not read the lc file into buffer
            public const int WARN_START_NUMBER = 5700;
            public const int WARN_END_NUMBER = 5799;
  
        }

        public class ErrorsMsg
        {
            /////////////////
            public const string STR_ERR_OUT_FILE_OR_DIRECTORY_ATTRIBUTE_UNKNOWN = "The file attribute is not supoort.";
            public const string STR_ERR_PACKAGE_FILE_NAME_DOES_NOT_HAVE_THE_MATCH_LENGTH = "The packager file name {0} need to have {1} character";
            public const string STR_ERR_CAN_NOT_FOUND_FFR_IN_FFP = "The FreeForm Packager can not find the form ID in package file";
/// <summary>
/// //////////////
/// </summary>
            
            
            public const string STR_ERR_PROJECT_SYMBOL_SIZE_IS_INVALID = "The project symbols size need to be 3 latters letter";
            public const string STR_ERR_UNKNOWN_REQUEST_TYPE = "The request type {0} is unknown";
            public const string STR_ERR_LC_FATAL_ERROR = "A fatal error occured during Lc run";
            public const string STR_ERR_PROJECT_SYMBOL_IN_PACKAGE_FILE_NAME_NOT_MATCH_TO_INPUT_ARGUMENT = "The project symbols {0} not mach to the packager file name {1}";
            public const string STR_ERR_SEARCH_FILES_PATH_IS_A_FILE_NAME = "The search files string {0} is file name";
            public const string STR_ERR_SEARCH_FILES_THE_CALLER_DOES_NOT_HAVE_THE_REQUIRED_PERMISSION = "The user {0} does not have the required permission to the folder {1}";
            public const string STR_ERR_SEARCH_FILES_SEARCH_PATTERN_DOES_NOT_CONTAIN_A_VALID_PATTERN = "The search Pattern does not contain a valid pattern";
            public const string STR_ERR_SEARCH_FILES_PATH_OR_SEARCH_PATTERN_IS_A_NULL_REFERENCE = "The path or search pattern is a null reference";
            public const string STR_ERR_SEARCH_FILES_THE_SPECIFIED_PATH_FILE_NAME_OR_BOTH_EXCEED_THE_SYSTEM_DEFINED_MAXIMUM_LENGTH = "The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters";
            public const string STR_ERR_SEARCH_FILES_THE_SPECIFIED_PATH_IS_INVALID = "The directory {0} not found";
            public const string STR_ERR_CAN_NOT_FOUND_PACKAGE_FILE_IN_DISTRIBUTION_POINT = "Cannot find forms collection file (FFP) in specified folder";
            public const string STR_ERR_PACKAGE_FILE_NAME_HAS_CORRUPTED_FILE_NAME = "The SN of the packager file name {0} is not number";
            public const string STR_ERR_INIT_PACKAGE_FOR_READ_FATAL_ERROR = "The forms collection file have been corrpted";
            public const string STR_ERR_IS_FORM_EXPIRED_FATAL_ERROR = "A fatal error occured during chake form expirion";
            public const string STR_ERR_GET_FORM_INFO_FATAL_ERROR = "A fatal error occured during get form info";
            public const string STR_ERR_OBJECT_NOT_INIT = "The intrenal freeform packager file have not initialization";
            public const string STR_ERR_FFP_HEADER_FATAL_ERROR = "A fatal error occured during get the packager file header";
            public const string STR_ERR_GET_FFR_DATA_FATAL_ERROR = "A fatal error occured during get the form data";
            public const string STR_ERR_GET_FFR_TAGS_FATAL_ERROR = "A fatal error occured during get form tags";
            public const string STR_ERR_GET_PACKAGE_SN_FATAL_ERROR = "A fatal error occured during get packager file serial number";
            public const string STR_ERR_FFP_FILE_NAME_SIZE_IS_TO_SHORT = "The packager file name {0} need to be  at least {1} character";
            public const string STR_ERR_INTERNAL_PROJECT_SYMBOL_NOT_MACH_TO_FILE_PROJECT_SYMBOL = "The project symbol of the file name not mach to the internal project symbol";








            //all the strings need to be chake to spelling
            public const string STR_ERR_NO_VALID_FFR_FILES = "Can not find any valid Form file";
            public const string STR_ERR_SYSTEM_EXCEPTION = "A fatal error occured during execution of {0} ,system message {1}";
            public const string STR_ERR_FILE_DONT_EXIST = "Fail to find FFS file {0}";
            public const string STR_ERR_FAIL_TO_FIND_FFS_ID = "Fail to find FFS id";
            public const string STR_ERR_CAN_NOT_FOUND_MAPPING_IN_FFR_XML = "Can not found the maping in ffr {0}";
            public const string STR_ERR_CAN_NOT_FOUND_FREE_FORM_PROJECT_TAG_IN_FFR_XML = "Can not found the FreeFormProject tag in ffr file {0}";
            public const string STR_ERR_CAN_NOT_FOUND_PROJECT_SCRIPT_CALL_TAG_IN_FFR_XML = "Can not found the projectScriptCall tag in ffr file {0}";
            public const string STR_ERR_NO_FFS_IN_LIST = "The Script list was empty";
            public const string STR_ERR_SCRIPT_NOT_FOUND = "Fail to find FFS file {0}";
            public const string STR_ERR_NO_FFR_FILES_FOUND = "Can not found any ffr files in {0}";
            public const string STR_ERR_CANT_OPEN_FILE = "Can not open The file {0} System description {1}";
            public const string STR_ERR_FILE_EMPTY = "The File {0} is empy";
            public const string STR_ERR_CANT_READ_FILE = "Can not read the file {0} System description {1}";
            public const string STR_ERR_INCORRECT_PARAM = "Input Params are not valid in function {0}";
            public const string STR_ERR_CANT_CREATE_TMP = "Can not Create File {0} system description {1}";
            public const string STR_ERR_CANT_WRITE_TMP = "Can not write temp File {0} system description {1}";
            public const string STR_ERR_XSL_COMPILER_FAIL = "Error occured during compilation of the form {0}";
            public const string STR_ERR_XSL_COMPILER_FATAL_ERROR = "A fatal error occured during compilation of the form {0} system description {1}";
            public const string STR_ERR_CAN_NOT_CREATE_TEMP_DATA_FILE = "Can not create the Temp file {0} system description {1}";
            public const string STR_ERR_CAN_NOT_OPEN_COMPILED_FROM_FILE = "Can not open file {0} system description {1}";

            public const string STR_ERR_INVALID_PARAMETERS_IN_CREATE_FFP = "There are missing parameters in package header.";
            public const string STR_ERR_INPUT_DIRECTORY_NOT_DEFINE = "The input directory is not defined";
            public const string STR_ERR_FFR_FILE_MASK = "The form's version number is missing.";
            public const string STR_ERR_DUPLICATE_FFR_ID = "The FreeForm Packager detected in the input directory \n an identical ID for two different Forms.";
            public const string STR_ERR_FFR_FILE_IS_CORRUPTED_IN_GET_CREATE_FFP = "The From File is corrupted."; //need to chek by zlikg
            public const string STR_ERR_FFR_XML_CORRUPTED = "The XML form file is corrupted";
            public const string STR_ERR_FFR_MISSING_PROPERTY = "The header of the form file is missing.";
            public const string STR_ERR_FFR_FILE_IS_EMPTY_IN_CREATE_FFP = "The From File is empty."; //need to chek by zlikg
            public const string STR_ERR_FFP_FILE_IS_CORRUPTED = "FreeForm Packager file is corrupted.";  //need to chek by zlikg
            public const string STR_ERR_FFP_FILE_PATH_INVALID = "The path parameter is not define or empty.";
            public const string STR_ERR_FFP_FILE_IS_EMPTY = "The FreeForm Packager file is empty."; ////need to chek by zlikg
            public const string STR_ERR_CAN_NOT_PARSE_FFP_FILE = "The package XML is corrupted";
            public const string STR_ERR_FFP_XML_IS_CORRUPTED = "The package XML is corrupted.";
            public const string STR_ERR_UNZIP_FFR_BUFFER = "The FreeForm Packager failed to un-zip the form";
            public const string STR_ERR_GET_FFR_FATAL_ERROR = "A fatal error occured while opening form";
            public const string STR_ERR_FFS_ATTRIBUTES_NOT_EXIST = "The script attribute value does not exist.";
            public const string STR_ERR_CREATE_FFP_FATAL_ERROR = "A fatal error occured during package creating process";
            public const string STR_ERR_GET_FFS_LIST_FATAL_ERROR = "a fatal error occured while get the script list.";
            public const string STR_ERR_GET_FFR_LIST_FATAL_ERROR = "a fatal error occured while get the script list.";
            public const string STR_ERR_GET_FFP_HEADER_FATAL_ERROR = "a fatal error occured while get FreeForm Packager header";//need to chek by zlikg
            public const string STR_ERR_FFR_CORRUPTED = "The From File id not mach to file name id";
            public const string STR_ERR_NO_FFE_FILE_FOUND = "The FreeForm Packager not found any From File.";
            public const string STR_ERR_MEMORY_ALLOCATION = "The memory allocation filed.";

            public const string STR_FFR_HEADER_IS_CORRUPTED = "The FFR header file *.ffh is corrupted";

            //new msg un cake from 13-10-2009 
            public const string STR_ERR_UNSUPPORT_COMPRESSION_TYPE = "The Compression Type value is not support in the current vresion.";

            public const string STR_FILE_DOES_NOT_EXIST = "File does not exist";
            public const string STR_ERR_FILE_IS_NOT_XML = "Failed to load a file that is not XML file";
            public const string STR_ERR_FAIL_TO_LOAD_FFS_TO_HEADER = "Fail to load FFS to header FFS might be corrupted";
            public const string STR_ERR_FAIL_TO_ZIP_FILE = "Fail to compress file";
            public const string STR_ERR_FAIL_TO_UPDATE_PROJECT_IN_HEADER_FILE = "Fail to update the header file with project properties";
            public const string STR_ERR_FROM_DATA_TAG_NOT_FOUND_IN_FFP = "Can not found the from-data-tag in the ffp";
            public const string STR_ERR_FORM_IS_EXPIRED = "The Form expired time have been past";
            public const string STR_ERR_FFP_INTERNAL_SIGNATURE_CORRUPTED = "One of the internal signature values is corrupted";


            public const string STR_ERR_FORM_IS_EXPIRED_2 = "Expired date {0} Current date {1} Group symbol {2}{3}";
          //  public const string STR_ERR_FROM_DATA_TAG_NOT_FOUND_IN_FFP = STR_ERR_CAN_NOT_FOUND_FFR_IN_FFP;
            public const string STR_ERR_CAN_NOT_MOVE_FFP_TEMP_FILE = "Source path {0} Target path {1}";
        }

        public class Permissions
        {
            public const int ERR_INIT_PERMISSION_BY_DECRYPT_PERMIT_CODE = 7599;
            public const int ERR_MODULE_IS_UNAUTHORIZED_START = 7500;
            public const int ERR_MODULE_IS_UNAUTHORIZED_END = 7563;
        }

        public class PreAnalyzerRT
        {
            public const int ERR_PRERT_COMMON = 10000;
            public const int ERR_PRERT_GET_PARAMS = 10001;
            public const int ERR_PRERT_INVALID_PARAM = 10002;
            public const int ERR_PRERT_GET_FORMS = 10003;
            public const int ERR_PRERT_JOB_HEADER_MISSING = 10004;
            public const int ERR_PRERT_MANDATORY_JH_PARAM_MISSING = 10005;
            public const int ERR_PRERT_SAVE_FFI = 10005;

        }
    }
}
