using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AutoFont.AfUtlFFP;



namespace AutoFont.LC.FFP_LC_FILE
{
    class FFPLcFile
    {
        /// <summary>
        /// the full path to file in lc
        /// </summary>
        protected string m_szLcFullFilePath = null;

        /// <summary>
        /// the Full path to LC folder path
        /// </summary>
        protected string m_szlcLCFullFolderPath = null;


        /// <summary>
        /// the Group Simbol of the project
        /// </summary>
        protected string m_szGroupSimbol = null;

        /// <summary>
        /// the Serial Number of the FFP
        /// </summary>
        protected string m_szSerialNumber = null;


        /// <summary>
        /// the File extension
        /// </summary>
        protected string m_szFileExtension = null;

        /// <summary>
        /// the last error code
        /// </summary>
        protected int m_LastError = ErrorCode.FFP_NO_ERROR;


        /// <summary>
        /// init the calss membres from the file name
        /// </summary>
        /// <param name="a_szLcFullFilePath">the lc full path file</param>
        /// <returns>ture if no error false if error</returns>
        public virtual bool  init(string a_szLcFullFilePath)
        {
            bool bRet = true;

            string l_szLcFileName=null;
           

            do
            {
                m_szLcFullFilePath = a_szLcFullFilePath;

                if (!ExtractPrmtersFromPath(a_szLcFullFilePath, ref l_szLcFileName))
                {
                    bRet = false;
                    break;
                }

                //extcract the Prmters from file name
                if (!ExtractPrmtersFromFileName(l_szLcFileName))
                {
                    bRet = false;
                    break;
                }

            } while (false);

            return bRet;
        }

        /// <summary>
        /// init the class membres from the List 
        /// the list mast be in this order 
        /// 0-LC  Full Folder path
        /// 1-Group Simbol
        /// 2- FFP SN
        /// </summary>
        /// <param name="a_LcFileParameter">the list of the Parametera </param>
        /// <returns>ture if no error false if error</returns>
        public virtual bool init(List<string> a_LcFileParameter)
        {
            bool bRet = true;

            do
            {

                if (a_LcFileParameter == null)
                {
                    m_LastError = ErrorCode.ERR_LC_PARAMETERS_NOT_DEFINE;
                    bRet = false;
                    break;
                }

                if (a_LcFileParameter.Count < 3)
                {
                    m_LastError = ErrorCode.ERR_LC_PARAMETERS_COLLECTIONS_NOT_IN_THE_RIGH_SIZE;
                    bRet = false;
                    break;
                }

                //The LC Full Folder Path
                m_szlcLCFullFolderPath = a_LcFileParameter[0];

                //The  Group Simbol
                m_szGroupSimbol = a_LcFileParameter[1];

                //The SN of the FFP 
                m_szSerialNumber = a_LcFileParameter[2];

                m_szFileExtension = "xml";

                //Build the Lc Full File Path
                //LC Fulder\\Group Simbol\\SN.xml
                m_szLcFullFilePath = string.Format("{0}\\{1}\\{2}.{3}", m_szlcLCFullFolderPath, m_szGroupSimbol, m_szSerialNumber, m_szFileExtension);

            } while (false);

            return bRet;

        }

        /// <summary>
        /// rRead Lc file to buffer
        /// </summary>
        /// <param name="a_pBuffer"></param>
        /// <returns></returns>
        public bool ReadLcFile(ref byte[] a_pBuffer)
        {
            bool bRet = true;
            FileStream l_FileStream = null;

            do
            {
                if (!OpenFile(FileMode.Open, FileAccess.Read, ref l_FileStream, ErrorCode.ERR_OPEN_LC_WRITE_BASE))
                {
                    bRet = false;
                    break;
                }

                a_pBuffer = new byte[l_FileStream.Length];
                l_FileStream.Read(a_pBuffer, 0, a_pBuffer.Length);

                l_FileStream.Close();

            } while (false);

            return bRet;


        }

        public bool WriteLcFile(byte[] a_pBuffer)
        {
            bool bRet = true;
            FileStream l_FileStream = null;

            do
            {

                if (!BuildLcPath())
                {
                    bRet = false;
                    break;
                }
                
                if (!OpenFile(FileMode.Create, FileAccess.Write,ref  l_FileStream, ErrorCode.ERR_OPEN_LC_WRITE_BASE))
                {
                    bRet = false;
                    break;
                }

                l_FileStream.Write(a_pBuffer, 0, a_pBuffer.Length);
                l_FileStream.Flush();
                l_FileStream.Close();
            
            } while (false);

            return bRet;
        }

        protected bool BuildLcPath()
        {
            bool bRet = true;
            do
            {
            
             
            
            } while (false);

            return bRet;
        }


        public int GetLastError()
        {
            return m_LastError;
        }

        /// <summary>
        /// extreact the prmters from the file name 
        /// </summary>
        /// <param name="a_szLcFileName">the lc file name</param>
        /// <returns>ture if no error false if error</returns>
        protected virtual bool ExtractPrmtersFromFileName(string a_szLcFileName)
        {
            bool bRet = true;
            int l_index = 0;


            do
            {
                if (a_szLcFileName.Length == 0)
                {
                    m_LastError = ErrorCode.ERR_LC_FILE_NAME_IS_INVALIED;
                    bRet = false;
                    break;
                }

                //extreat SN 
                l_index = a_szLcFileName.LastIndexOf('\\');
                if (l_index == -1)
                {
                    m_LastError = ErrorCode.ERR_LC_FILE_NAME_IS_INVALIED;
                    bRet = false;
                    break;
                }

                m_szGroupSimbol = a_szLcFileName.Substring(0, l_index);
                m_szFileExtension = "xml";

            } while (false);

            return bRet;
        }

        protected bool ExtractPrmtersFromPath(string a_szLCPath,ref string a_szLcFileName)
        {
            bool bRet = true;

            int l_index = 0;
            do
            {
                //Extract the file name
                l_index = a_szLCPath.LastIndexOf("\\");
                if (l_index == -1)
                {
                    m_LastError = ErrorCode.ERR_LC_FILE_PATH_IS_INVALIED;
                    bRet = false;
                    break;
                }

                a_szLcFileName = a_szLCPath.Substring(l_index + 1);
                a_szLCPath = a_szLCPath.Substring(0, l_index);

                //extreat the Group Simbol
                l_index = a_szLCPath.LastIndexOf("\\");
                if (l_index == -1)
                {
                    m_LastError = ErrorCode.ERR_LC_FILE_PATH_IS_INVALIED;
                    bRet = false;
                    break;
                }

                m_szGroupSimbol = a_szLCPath.Substring(l_index + 1);

                //extract the Lc Folder
                m_szlcLCFullFolderPath = a_szLCPath.Substring(0, l_index + 1);

                if (!ValidateLcFolder())
                {
                    bRet = false;
                    break;
                }

            } while (false);

            return bRet;
        }


        protected bool ValidateLcFolder()
        {

            bool bRest = System.IO.Directory.Exists(m_szlcLCFullFolderPath);

            if (!bRest)
            {
                m_LastError = ErrorCode.ERR_LC_FOLDER_NOT_EXISTS;

            }
            return bRest;
        }

        protected bool OpenFile(FileMode a_FileMode, FileAccess a_FileAccess, ref FileStream a_FileStream, int a_BaseErrorCode)
        {
            try
            {
                a_FileStream = new FileStream(m_szLcFullFilePath, a_FileMode, a_FileAccess);
            }
            catch (ArgumentNullException)
            {
                m_LastError = IoBaseErrorCodes.ERR_IO_BASE_PATH_IS_A_NULL_REFERENCE + a_BaseErrorCode;
                return false;
            }

            catch (ArgumentOutOfRangeException)
            {
                m_LastError = IoBaseErrorCodes.ERR_IO_BASE_ARGUMENT_OUT_OF_RANGE + a_BaseErrorCode;
                return false;
            }
            
            catch (ArgumentException)
            {
                m_LastError = IoBaseErrorCodes.ERR_IO_BASE_PATH_IS_AN_EMPTY_STRING + a_BaseErrorCode;
                return false;
            }
            catch (FileNotFoundException)
            {
                m_LastError = IoBaseErrorCodes.ERR_IO_BASE_THE_FILE_CANNOT_BE_FOUND + a_BaseErrorCode;
                return false;
            }

            catch (PathTooLongException)
            {
                m_LastError = IoBaseErrorCodes.ERR_IO_BASE_PATH_TOO_LONG + a_BaseErrorCode;
                return false;
            }

            catch (DirectoryNotFoundException)
            {
                m_LastError = IoBaseErrorCodes.ERR_IO_BASE_THE_SPECIFIED_PATH_IS_INVALID + a_BaseErrorCode;
                return false;
            }
            
            catch (IOException)
            {
                m_LastError = IoBaseErrorCodes.ERR_IO_BASE_AN_IO_ERROR_OCCURS + a_BaseErrorCode;
                return false;
            }
            catch (System.Security.SecurityException)
            {
                m_LastError = IoBaseErrorCodes.ERR_IO_BASE_THE_CALLER_DOES_NOT_HAVE_THE_REQUIRED_PERMISSION + a_BaseErrorCode;
                return false;
            }
            
            catch (UnauthorizedAccessException)
            {
                m_LastError = IoBaseErrorCodes.ERR_IO_BASE_THE_ACCESS_REQUESTED_IS_NOT_PERMITTED + a_BaseErrorCode;
                return false;
            }
            
            

            
            catch (Exception)
            {
                m_LastError = a_BaseErrorCode;
                return false;
            }

            return true;






        }

        

    }
}
