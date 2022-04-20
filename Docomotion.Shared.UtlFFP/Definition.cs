namespace Docomotion.Shared.UtlFFP
{
    public enum AccessType
    {
        READ = 4,
        WRITE = 2,
        READ_WRITE = 6
    }

    internal struct FFRFile
    {
        public string szFilePath; //ffp file parh
        public ulong dwBufferSeek; //ofset zip buffer in ffr 
    };


    internal class Definition
    {
        public const int APP_SYMBOL_SIZE = 11;
        public const int NAME_STRING_SIZE = 261;
        public const int DATE_STRING_SIZE = 41;
        public const int NUMBER_STRING_SIZE = 6;

        public const int FFR_HEADER = 50;  //the  size of the  header  befor ffr file buffer in ffp file
        public const int FFR_FOOTER = 50; //the size of the ffp footer in the end of ffp file

        public const string FFS_ID_ATTRIBUTES = "id";
        public const string FFS_VERSION_ATTRIBUTES = "version";

        public const string XPATH_FFS = "/FFPRoot/FFS_LIST";

        public const string FFP_TEMP_EXTSION = ".temp"; //the temp file extension

        public const int MAX_SYMBOL_SIZE = 3;

        public const int ZIP_COMPRESS_TYPE = 0;
        public const int Z7_COMPRESS_TYPE =  1;


        /*****************************************************************************
  XML_TAG
*****************************************************************************/

        public const string FFP_XML_HEADER = "FreeFormPackageHeader";
        public const string APP_SYMBOL = "ApplicationSymbol";
        public const string APP_SYMBOL_NAME = "ApplicationSymbolName";
        public const string PACKGE_NAME = "PackageName";
        public const string PACKGE_DESCRIPTION = "PackageDescription";
        public const string SERIAL_NUMBER = "SerialNumber";
        public const string COMPRESSION_TYPE = "CompressionType";
        public const string FFR_FILE_LIST = "FFRFileList";
        public const string FREE_FORM_PROPERTIES = "FreeFormProperties";
        public const string FRR_Lengh = "FFRLength";
        public const string FFR_OFF_SET = "FFROffset";
        public const string FFP_ROOT = "FFPRoot";
        public const string FFR_REAL_FILE_SIZE = "FFRRealSize";
        public const string FFR_FILE_COUNT = "FFRFileCount";
        public const string FFP_CREATE_DATE = "PackageCreateDate";
        public const string FFP_VERSION = "PackageFormVersion";
        public const string FFR_PROJECT = "FreeFormProject";
        public const string FFR_FROM_ID = "ID";
        public const string FFR_NAME = "Name";
        public const string FFR_DESCRIPTION = "Description";
        public const string FFR_VERSION = "Version";
        public const string FFR_DATE = "Date";
        public const string FFR_FORM_VERSION = "FormatVersion";
        public const string FFS_LIST = "FFS_LIST";
        public const string FFS = "FFS";
        public const string FFR_FILELIST_START = "<FFRFileList>";
        public const string FFR_FILELIST_END = "</FFRFileList>";




        public const string FFR_FILE_DAT = "file.dat";

        public const string PACKAGE_VERSION = "1.3.0.0"; //the ffp format version
        public const string FF_DATA_FILE_EXT = ".ffData";
        public const string FF_TAGS_FILE_EXT = ".ffTags";
        public const string FF_HEADER_FILE_EXT = ".ffHeader";
        public const string START_SYSTEM_TIME = "12/31/2008"; //format time MM/DD/YYYY

    }
}
