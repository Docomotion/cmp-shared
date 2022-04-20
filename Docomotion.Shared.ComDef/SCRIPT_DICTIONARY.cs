
namespace Docomotion.Shared.ComDef
{
    public static class SCRIPT_DICTIONARY
    {
        #region Script Procedures
        public const string PROC_SET_VALUE = "SET VALUE";
        public const string PROC_SET_SYMBOL = "SET SYMBOL";
        public const string PROC_HIDE_CONTAINER = "HIDE CONTAINER";
        public const string PROC_SHOW_CONTAINER = "SHOW CONTAINER";
        public const string PROC_LOCK_CONTAINER = "LOCK CONTAINER";
        public const string PROC_SET_FONT_COLOR = "SET FONT COLOR";
        public const string PROC_SET_FONT_WEIGHT = "SET FONT WEIGHT";
        public const string PROC_SET_FONT_UNDERLINE = "SET FONT UNDERLINE";
        public const string PROC_SET_FONT_SLANT = "SET FONT SLANT";
        public const string PROC_SET_FONT_SIZE = "SET FONT SIZE";
        public const string PROC_SET_FONT_TYPEFACE = "SET FONT TYPEFACE";
        public const string PROC_SET_FONT_ATTRIBUTES = "SET FONT ATTRIBUTES";
        public const string PROC_INSERT_PICTURE = "INSERT PICTURE";
        public const string PROC_INSERT_PICTURE4 = "INSERT PICTURE";
        public const string PROC_INSERT_PICTURE_EXT = "INSERT PICTURE";
        public const string PROC_INSERT_RTF = "INSERT RTF";
        public const string PROC_INSERT_HTML = "INSERT HTML";
        public const string PROC_INSERT_MSWORD = "INSERT MSWORD";

        public const string PROC_INSERT_PAGE_BREAK_IN = "INSERT PAGE BREAK";
        public const string PROC_INSERT_PAGE_BREAK_OUT = "TABLE PAGE BREAK";

        public const string PROC_INSERT_PAGE_BREAK_AFTER_IN = "INSERT PAGE BREAK AFTER";
        public const string PROC_INSERT_PAGE_BREAK_AFTER_OUT = "TABLE PAGE BREAK AFTER";

        public const string PROC_INSERT_PAGE_BREAK_BEFORE_IN = "INSERT PAGE BREAK BEFORE";
        public const string PROC_INSERT_PAGE_BREAK_BEFORE_OUT = "TABLE PAGE BREAK BEFORE";

        public const string PROC_SET_ADDRESS = "SET ADDRESS";
        public const string PROC_SET_CHART_DATA = "SET CHART DATA";

        public const string PROC_INSERT_MEDIA_SOURCE = "INSERT MEDIA SOURCE";

        public const string PROC_HIDE_ROW = "HIDE ROW";
        public const string PROC_SHOW_ROW = "SHOW ROW";

        public const string PROC_SET_ROWS_RANGE = "SET ROWS RANGE";

        public const string PROC_HIDE_GRAPHICAL_ROW = "HIDE GRAPHICAL ROW";
        public const string PROC_SHOW_GRAPHICAL_ROW = "SHOW GRAPHICAL ROW";

        public const string PROC_SORT_TABLE = "SORT TABLE";

        public const string ELEMENT_PARAMS = "PARAMS"; //list of parameters

        public struct PROC_SORT_TABLE_PARAM_TYPE
        {
            public const string TEXT = "TEXT";
            public const string NUMBER = "NUMBER";
            public const string DATE = "DATE";
            public const string DATETIME = "DATE & TIME";
        }

        public struct PROC_SORT_TABLE_PARAM_ORDER
        {
            public const string ASCENDING = "ASC";
            public const string DESCENDING = "DES";
        }

        public struct PROC_SORT_TABLE_PARAM_DATE_FORMAT
        {
            public const string dd_MM_yyyy = "dd/MM/yyyy";
            public const string MM_dd_yyyy = "MM/dd/yyyy";
            public const string dd_MM_yy = "dd/MM/yy";
            public const string yyyy_MM_dd = "yyyy/MM/dd";
            public const string ddMMyyyy = "ddMMyyyy";
            public const string MMddyyyy = "MMddyyyy";
        }

        public struct PROC_SORT_TABLE_PARAM_DATE_TIME_FORMAT
        {
            public const string yyyy_MM_dd_HH_mm_ss = "yyyy-MM-dd HH:mm:ss";
        }

        #endregion

            #region Script Functions
            public const string ISL_FUNC_STRING_LENGTH = "STRING LENGTH";
        public const string XSLT_FUNC_STRING_LENGTH = "Func_String_Length";

        public const string ISL_FUNC_APPEND_STRING = "APPEND STRING";
        public const string XSLT_FUNC_APPEND_STRING = "Func_Append_String";

        public const string ISL_FUNC_SUB_STRING = "SUB STRING";
        public const string XSLT_FUNC_SUB_STRING = "Func_Sub_String";

        public const string ISL_FUNC_PAD_STRING = "PAD STRING";
        public const string XSLT_FUNC_PAD_STRING = "Func_Pad_String";

        public const string ISL_FUNC_REPLACE_STRING = "REPLACE STRING";
        public const string XSLT_FUNC_REPLACE_STRING = "Func_Replace_String";

        public const string ISL_FUNC_REMOVE_STRING = "REMOVE STRING";
        public const string XSLT_FUNC_REMOVE_STRING = "Func_Remove_String";

        public const string ISL_FUNC_TRIM_CHARACTER = "TRIM CHARACTER";
        public const string XSLT_FUNC_TRIM_CHARACTER = "Func_Trim_Character";

        public const string ISL_FUNC_COMPRESS_CHARACTER = "COMPRESS CHARACTER";
        public const string XSLT_FUNC_COMPRESS_CHARACTER = "Func_Compress_Character";

        public const string ISL_FUNC_MAX_STRING_LENGTH = "MAX STRING LENGTH";
        public const string XSLT_FUNC_MAX_STRING_LENGTH = "Func_Max_String_Length";

        public const string ISL_FUNC_TRUNCATE_STRING = "TRUNCATE STRING";
        public const string XSLT_FUNC_TRUNCATE_STRING = "Func_Truncate_String";

        public const string ISL_FUNC_STRING_POSITION = "STRING POSITION";
        public const string XSLT_FUNC_STRING_POSITION = "Func_String_Position";

        public const string ISL_FUNC_ABSOLUTE = "ABSOLUTE";
        public const string XSLT_FUNC_ABSOLUTE = "Func_Absolute";

        public const string ISL_FUNC_INTEGER = "INTEGER";
        public const string XSLT_FUNC_INTEGER = "Func_Integer";

        public const string ISL_FUNC_MANTISSA = "MANTISSA";
        public const string XSLT_FUNC_MANTISSA = "Func_Mantissa";

        public const string ISL_FUNC_POWER = "POWER";
        public const string XSLT_FUNC_POWER = "Func_Power";

        public const string ISL_FUNC_ROUND = "ROUND";
        public const string XSLT_FUNC_ROUND = "Func_Round";

        public const string ISL_FUNC_EXTERNAL_USE_DB = "External_USE_DB";
        public const string XSLT_FUNC_EXTERNAL_USE_DB = "Func_External_USE_DB";

        public const string ISL_FUNC_TABLE_POSITION = "TABLE ROW POSITION";
        public const string XSLT_FUNC_TABLE_POSITION = "Func_Table_Row_Position";

        public const string ISL_FUNC_TABLE_COUNT = "TABLE ROW COUNT";
        public const string XSLT_FUNC_TABLE_COUNT = "Func_Table_Row_Count";

        public const string ISL_FUNC_TABLE_SUM = "TABLE COLUMN SUM";
        public const string XSLT_FUNC_TABLE_SUM = "Func_Table_Column_Sum";

        public const string ISL_FUNC_TABLE_AVERAGE = "TABLE COLUMN AVERAGE";
        public const string XSLT_FUNC_TABLE_AVERAGE = "Func_Table_Column_Average";
        #endregion

        public const string ELEMENT_ROOT = "SCRIPT";
        public const string ELEMENT_IF = "IF";
        public const string ELEMENT_COND = "COND";
        public const string ELEMENT_EXPRESSION = "EXPR";
        public const string ELEMENT_FUNCTION_CALL = "FUNC";
        public const string ELEMENT_PROCEDURE_CALL = "PROC";
        public const string ELEMENT_THEN = "THEN";
        public const string ELEMENT_TAG = "TAG";

        public const string ELEMENT_OPERATOR = "OPERATOR";
        public const string OPERATOR_ADD = "ADD";
        public const string OPERATOR_SUB = "SUB";
        public const string OPERATOR_MULTIPLY = "MULT";
        public const string OPERATOR_DIV = "DIV";
        public const string OPERATOR_MOD = "MOD";


        public const string ELEMENT_LOGICAL = "LOGICAL";
        public const string LOGICAL_AND = "AND";
        public const string LOGICAL_OR = "OR";



        public const string ELEMENT_COMPARISSION = "COMPARISSION";
        public const string COMPARISSION_EQUAL = "EQUAL";
        public const string COMPARISSION_NOTEQUAL = "NOTEQUAL";
        public const string COMPARISSION_GREATER = "GREATER";
        public const string COMPARISSION_LESSTHAN = "LESS";
        public const string COMPARISSION_GREATEROREQUAL = "GREATEROREQUAL";
        public const string COMPARISSION_LESSTHANOREQUAL = "LESSOREQUAL";


        public const string ELEMENT_CONST = "CONST";

        public const string ELEMENT_ELSE = "ELSE";
        public const string ELEMENT_ELSE_IF = "ELSEIF";

        public const string FUNCTION_GET_INPUT_VALUE = "GetInputExprValue";//function for SET VALUE

        public const string GT_SCRIPT_ATTR_NAME = "gt";
    }
}
