using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using AutoFont.AfUtlFFP;
using AutoFont.LC.FFP_LC_FILE;

namespace AutoFont.LC.FORM_LC_FILE
{
    class FromLcFile:FFPLcFile
    {
        /// <summary>
        /// The Form ID 
        /// </summary>
        protected string m_szFormID=null;
        
        /// <summary>
        /// the Form Version
        /// </summary>
        protected string m_szFormVresion=null;
        
        /// <summary>
        /// The Form CRC
        /// </summary>
        protected string m_szFormCRC=null;

        /// <summary>
        /// init the calss membres from the file name
        /// </summary>
        /// <param name="a_szLcFullFilePath">the lc full path file</param>
        /// <returns>ture if no error false if error</returns>
        public override bool init(string a_szLcFullFilePath)
        {
            bool bRet = true;
            string l_szLcFileName=null;

            do
            {
                m_szLcFullFilePath=a_szLcFullFilePath;

                if (!ExtractPrmtersFromPath(a_szLcFullFilePath, ref l_szLcFileName))
                {
                    bRet = false;
                    break;
                }

                if (!ExtractPrmtersFromFileName(l_szLcFileName))
                {
                    bRet = false;
                    break;
                }

                m_szFileExtension = "ffe";

            
            
            } while (false);

            return bRet;
 

        }


        /// <summary>
        /// init the class membres from the List 
        /// the list mast be in this order 
        /// 0-LC  Full Folder path
        /// 1-Group Simbol
        /// 2- FFP SN
        /// 3-FORM ID
        /// 4-FORM Version
        /// 5-Form CRC
        /// </summary>
        /// <param name="a_LcFileParameter">the list of the Parametera </param>
        /// <returns>ture if no error false if error</returns>
        public override bool init(List<string> a_LcFileParameter)
        {
            bool bRest = true;
            do
            {
                if (a_LcFileParameter == null)
                {
                    m_LastError = ErrorCode.ERR_LC_PARAMETERS_NOT_DEFINE;
                    bRest = false;
                    break;
                }

                if (a_LcFileParameter.Count < 6)
                {
                    m_LastError = ErrorCode.ERR_LC_PARAMETERS_COLLECTIONS_NOT_IN_THE_RIGH_SIZE;
                    bRest = false;
                    break;
                }

                //The LC Full Folder Path
                m_szlcLCFullFolderPath = a_LcFileParameter[0];

                //The  Group Simbol
                m_szGroupSimbol = a_LcFileParameter[1];

                //The SN of the FFP 
                m_szSerialNumber = a_LcFileParameter[2];

                //The Form ID 
                m_szFormID = a_LcFileParameter[3];

                //The Form Version
                m_szFormVresion = a_LcFileParameter[4];
                
                //Form CRC
                m_szFormCRC = a_LcFileParameter[5];

                m_szFileExtension = "ffe";

                //Build The LC File Path
                m_szLcFullFilePath = string.Format("{0}\\{1}\\{2}_{3}_{4}_{5}_{6}.{7}", m_szlcLCFullFolderPath, m_szGroupSimbol, m_szSerialNumber, m_szFormID, m_szFormVresion, m_szFormCRC, m_szFileExtension);

            } while (false);

            return bRest;
        }

        protected override bool ExtractPrmtersFromFileName(string a_szLcFileName)
        {
            bool bRet = true;
            int l_index = 0;

            do
            {
               //Get SN
                l_index = a_szLcFileName.IndexOf('_');
                if (l_index == -1)
                {
                    m_LastError = ErrorCode.ERR_LC_FILE_PATH_IS_INVALIED;
                    bRet = false;
                    break;
                }
                m_szSerialNumber = a_szLcFileName.Substring(0, l_index);
                a_szLcFileName = a_szLcFileName.Substring(l_index + 1);

                //Get From ID
                l_index = a_szLcFileName.IndexOf('_');
                if (l_index == -1)
                {
                    m_LastError = ErrorCode.ERR_LC_FILE_PATH_IS_INVALIED;
                    bRet = false;
                    break;
                }

                m_szFormID = a_szLcFileName.Substring(0, l_index);
                a_szLcFileName = a_szLcFileName.Substring(l_index + 1);

                //Get form version
                l_index = a_szLcFileName.IndexOf('_');
                if (l_index == -1)
                {
                    m_LastError = ErrorCode.ERR_LC_FILE_PATH_IS_INVALIED;
                    bRet = false;
                    break;
                }
                m_szFormVresion = a_szLcFileName.Substring(0, l_index);
                a_szLcFileName = a_szLcFileName.Substring(l_index + 1);

                //Get CRC
                l_index = a_szLcFileName.IndexOf('.');
                if (l_index == -1)
                {
                    m_LastError = ErrorCode.ERR_LC_FILE_PATH_IS_INVALIED;
                    bRet = false;
                    break;
                }

                m_szFormCRC = a_szLcFileName.Substring(0, l_index);
               



            
            } while (false);

            return bRet;
        }
    }
}
