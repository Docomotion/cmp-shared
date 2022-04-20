namespace Docomotion.Shared.UtlFFP.LC
{

    public enum LCFilesTypes
    {
        ePackageHeader =0,
        eForm =1,
        eFormDataTag=2
    }
    
    
    public enum FileWorkMode
    {
        eReplaceLcFile = 0,
        eCreateNewLcFile = 1,
        eReNameLcFile = 2,
        eNoAction = 3
    }

    public class LcDeines
    {
        public const string FFP_EXTENSION = ".ffp";
        public const string PACKAGE_HEADER_EXTENSION = ".xml";
        public const string FORM_EXTENSION = ".ffr";
        public const string FORM_DATA_TAG_EXTENSION = ".xml";
        public const int GROUP_SYMBOL_SIZE = 3;
        public const int MIN_FFP_FILE_LENGTH = 9;
        public const int SN_NUMBER_SIZE = 4;

        public const string PACKAGE_HREAD_START_FILE_NAME = "FFP_HEADER";
        public const string FROM_START_FILE_NAME =     "FORM_DATA";
        public const string DATA_TAG_START_FILE_NAME = "TAGS_DATA";

        public const int PACKAGE_SN_INDEX = 10;
        public const int PACKAGE_SN_SIZE = 4;

        public const int FORM_VERSION_INDEX = 21;
        public const int FORM_VERSION_SIZE = 4;

        public const int FORM_SRC_INDEX = 26;
        public const int FORM_SRC_SIZE = 5;

        public const int FORM_EXPIRED_INDEX = 32;
        public const int FORM_EXPIRED_SIZE = 5;

        public const int PACKAGE_HREAD_SN_INDEX = 11;
        public const int PACKAGE_HREAD_SN_SIZE = 4;

       
    }


}

