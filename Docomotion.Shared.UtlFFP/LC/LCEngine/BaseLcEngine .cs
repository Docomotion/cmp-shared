using System;
using System.IO;
using Docomotion.Shared.ComDef;
using Docomotion.Shared.ErrorHandler;
using Docomotion.Shared.UtlFFP.LC.LCFile;
using Docomotion.Shared.UtlFFP.NewFFP;

namespace Docomotion.Shared.UtlFFP.LC.LCEngine
{
    abstract class BaseLcEngine:BaseFFPUtl,IDisposable
    {

        
        protected IAfFFPReadUtils m_FFPUtilsIntrface = null;
        
        
        protected string m_szDistributionPoint = null;
        protected string m_szLCFolder = null;
        protected string m_szProjectSymbol = null;
        protected int m_MaxDayIvalied = 0;

        protected uint m_uiPackageSerialNamber = 0;



        protected string m_szFullPathToPackageFile = null;

       

        protected int m_iLastErrorCode = 0;

        //to save the LC fils path befor update the LC file object
        
        protected string m_szFullPathToLCFile=null;
        protected string m_szFullPathToOldLCFile=null;
       
        
       
        protected abstract bool DownloadFromDistributionPoint(LcFileObject a_LcFileObject);
        protected abstract string BuildLCFileName();
        protected abstract string BuildLCSerachString();


        public int LastErrorCode
        {
            get
            {
                return m_iLastErrorCode;
            }
        }

        public IAfFFPReadUtils GetFFPReadUtils
        {
            get
            {
                return m_FFPUtilsIntrface;
            }
        }

        public bool Init(string a_szDPPath, string a_szGroupSymbol, string a_szLcPath,int m_aTimeToVailed)
        {
            bool bRet = true;
           

            do
            {
                m_szDistributionPoint = a_szDPPath;  
                m_szProjectSymbol = a_szGroupSymbol;
                m_szLCFolder = a_szLcPath;

                


                if (m_szLCFolder==null ||
                    m_szLCFolder.Length == 0)
                {

                    m_szLCFolder = null;
                }
                else
                {
                   
                    if (m_szLCFolder[m_szLCFolder.Length - 1] != '\\')
                    {
                        m_szLCFolder += '\\';
                    }
                }



               

                m_MaxDayIvalied = m_aTimeToVailed;
               


            } while (false);

            return bRet;
        }



        public bool RunLCEngine(ref IntPtr a_pBuffer, ref ulong a_pBufferSize)
        {
            bool bRet = true;
            bool l_IsTheDistributionPointIsSpecificPath = false;
            bool l_bNeedToDownload = false;
            LcFileObject l_LcFileObject = new LcFileObject();
            

            do
            {
                if (!AnalysisDistributionPointPath(ref l_IsTheDistributionPointIsSpecificPath))
                {
                    bRet = false;
                    break;
                }

                //if the the path is specific and thir is no SN
                if (l_IsTheDistributionPointIsSpecificPath == false)
                {
                    //get the Last Package SN in the Distribution Point
                    if (!GetLastSNPackageDistributionPoint())
                    {
                        bRet = false;
                        break;
                    }
                }

                //verify Need to Download 
                if (!VerifyNeedToDownload(ref l_bNeedToDownload, l_LcFileObject))
                {
                    bRet = false;
                    break;
                }

                if (l_bNeedToDownload == true)
                {
                    if (!DownloadFromDistributionPoint(l_LcFileObject))
                    {
                        bRet = false;
                        break;
                    }
                }

                if (l_LcFileObject.OperationOnLc == FileWorkMode.eReplaceLcFile ||
                   l_LcFileObject.OperationOnLc == FileWorkMode.eCreateNewLcFile)
                {                   
                    l_LcFileObject.LcTragetFullPath = BuildLCFileName();
                }

               

               

                //return the result buffer 
                byte[] l_pLCBuffer = l_LcFileObject.LCBuffer;

                 a_pBuffer=System.Runtime.InteropServices.Marshal.AllocHGlobal(l_pLCBuffer.Length);
                 System.Runtime.InteropServices.Marshal.Copy(l_pLCBuffer, 0, a_pBuffer, l_pLCBuffer.Length);
                 a_pBufferSize = (ulong)l_pLCBuffer.Length;

                 
                if (m_szLCFolder != null)
                 {
                    
                    if (!l_LcFileObject.InvokeLcperation())
                     {
                          
                        m_iLastErrorCode = l_LcFileObject.LastErrorCode;
                         bRet = false;
                         break;
                     }

                      
                }




            } while (false);

            return bRet;
        }

        protected bool GetSNFromPackageFileName(string a_szPackageFileName, ref uint a_PackageFileSN)
        {
            bool bRet = true;

            do
            {
                if (a_szPackageFileName.Length != LcDeines.MIN_FFP_FILE_LENGTH + LcDeines.SN_NUMBER_SIZE)
                {
                    m_iLastErrorCode = Lapsus.RT.ERR_PACKAGE_FILE_NAME_DOES_NOT_HAVE_THE_MATCH_LENGTH;
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, 0, string.Format(Lapsus.ErrorsMsg.STR_ERR_PACKAGE_FILE_NAME_DOES_NOT_HAVE_THE_MATCH_LENGTH, a_szPackageFileName, LcDeines.MIN_FFP_FILE_LENGTH + LcDeines.SN_NUMBER_SIZE), null);
                    break;
                }

                //read the SN nember 
                try
                {
                    a_PackageFileSN = uint.Parse(a_szPackageFileName.Substring(5, LcDeines.SN_NUMBER_SIZE));
                }
                catch (Exception)
                {
                    m_iLastErrorCode = Lapsus.RT.ERR_PACKAGE_FILE_NAME_HAS_CORRUPTED_FILE_NAME;
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, m_iSystemErrorCode, string.Format(Lapsus.ErrorsMsg.STR_ERR_PACKAGE_FILE_NAME_HAS_CORRUPTED_FILE_NAME, a_szPackageFileName), null);
                    break;
                }



            } while (false);

            return bRet;
        }

        protected bool AnalysisDistributionPointPath(ref bool a_IsTheDistributionPointIsSpecificPath)
        {
            bool bRet = true;
            string l_szPackageFileName = null;
            string l_szPackageProjectSymbol = null;
            int l_index = 0;

            do
            {
                //if the Distribution Point path have extension .ffp
                if (m_szDistributionPoint.LastIndexOf(LcDeines.FFP_EXTENSION) == -1)
                {
                    a_IsTheDistributionPointIsSpecificPath = false;
                    if (m_szDistributionPoint[m_szDistributionPoint.Length - 1] != '\\')
                    {
                        m_szDistributionPoint += '\\';
                    }
                    break;
                }

                a_IsTheDistributionPointIsSpecificPath = true;

                //Get the Package file name
                l_index = m_szDistributionPoint.LastIndexOf('\\');
                if (l_index != -1)
                {
                    l_szPackageFileName = m_szDistributionPoint.Substring(l_index + 1);
                }
                else
                {
                    l_szPackageFileName = m_szDistributionPoint;
                }
                //get the Project Symbol from the file name
                l_szPackageProjectSymbol = l_szPackageFileName.Substring(2, 3);

                //if the file name is legal
                if (l_szPackageFileName.Length < LcDeines.MIN_FFP_FILE_LENGTH)
                {
                    m_iLastErrorCode = Lapsus.RT.ERR_FFP_FILE_NAME_SIZE_IS_TO_SHORT;
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, 0, string.Format(Lapsus.ErrorsMsg.STR_ERR_FFP_FILE_NAME_SIZE_IS_TO_SHORT, l_szPackageFileName, LcDeines.MIN_FFP_FILE_LENGTH), null);
                    break;
                }

                //compare  the Package Project Symbol with the input Project Symbol
                if (l_szPackageProjectSymbol.ToLower() != m_szProjectSymbol.ToLower())
                {
                    m_iLastErrorCode = Lapsus.RT.ERR_PROJECT_SYMBOL_IN_PACKAGE_FILE_NAME_NOT_MATCH_TO_INPUT_ARGUMENT;
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, 0, string.Format(Lapsus.ErrorsMsg.STR_ERR_PROJECT_SYMBOL_IN_PACKAGE_FILE_NAME_NOT_MATCH_TO_INPUT_ARGUMENT, l_szPackageFileName, m_szProjectSymbol), null);
                    break;
                }

                //if the File have no in file name  SN
                if (l_szPackageFileName.Length == LcDeines.MIN_FFP_FILE_LENGTH)
                {
                   
                    m_szFullPathToPackageFile = m_szDistributionPoint;
                    //get the SN 
                    if (!GetPackageSN())
                    {
                        bRet = false;
                        break;
                    }
                    break;
                }


                if (!GetSNFromPackageFileName(l_szPackageFileName, ref m_uiPackageSerialNamber))
                {
                    bRet = false;
                    break;
                }

                m_szFullPathToPackageFile = m_szDistributionPoint;


            } while (false);

            return bRet;


        }

        protected bool GetLastSNPackageDistributionPoint()
        {
            bool bRet = true;

            string l_szPackagFileName = null;
            string l_szSearchPackagFileNames = null;
            string[] l_ResultsPackagFileNames = null;
            string l_szTempFileName = null;
            int l_iMaxSNIndex = 0;
            int l_index = 0;
            uint l_CurrentSN = 0;
            uint l_MaxSN = 0;

            do
            {

                //build the non vresion project simbol file name 
                l_szPackagFileName = string.Format("{0}FF{1}{2}", m_szDistributionPoint, m_szProjectSymbol, LcDeines.FFP_EXTENSION);

                //if file Exists
                if (File.Exists(l_szPackagFileName))
                {
                    //set the SN to zero
                    m_uiPackageSerialNamber = 0;
                    m_szFullPathToPackageFile = l_szPackagFileName;
                    break;
                }

                //start to search string for find mach  Packages files
                l_szSearchPackagFileNames = string.Format("FF{0}*{1}", m_szProjectSymbol, LcDeines.FFP_EXTENSION);


                try
                {
                    l_ResultsPackagFileNames = Directory.GetFiles(m_szDistributionPoint, l_szSearchPackagFileNames, SearchOption.TopDirectoryOnly);
                }
                catch (PathTooLongException ex)
                {
                    m_iLastErrorCode = Lapsus.RT.ERR_SEARCH_FILES_THE_SPECIFIED_PATH_FILE_NAME_OR_BOTH_EXCEED_THE_SYSTEM_DEFINED_MAXIMUM_LENGTH;
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, m_iSystemErrorCode, Lapsus.ErrorsMsg.STR_ERR_SEARCH_FILES_THE_SPECIFIED_PATH_FILE_NAME_OR_BOTH_EXCEED_THE_SYSTEM_DEFINED_MAXIMUM_LENGTH, ex);
                    break;
                }
                catch (ArgumentNullException ex)
                {
                    m_iLastErrorCode = Lapsus.RT.ERR_SEARCH_FILES_PATH_OR_SEARCH_PATTERN_IS_A_NULL_REFERENCE;
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, m_iSystemErrorCode, Lapsus.ErrorsMsg.STR_ERR_SEARCH_FILES_PATH_OR_SEARCH_PATTERN_IS_A_NULL_REFERENCE, ex);
                    break;
                }
                catch (DirectoryNotFoundException ex)
                {
                    m_iLastErrorCode = Lapsus.RT.ERR_SEARCH_FILES_THE_SPECIFIED_PATH_IS_INVALID;
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, m_iSystemErrorCode, string.Format(Lapsus.ErrorsMsg.STR_ERR_SEARCH_FILES_THE_SPECIFIED_PATH_IS_INVALID, m_szDistributionPoint), ex);
                    break;
                }
                catch (IOException ex)
                {
                    m_iLastErrorCode = Lapsus.RT.ERR_SEARCH_FILES_PATH_IS_A_FILE_NAME;
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, m_iSystemErrorCode, string.Format(Lapsus.ErrorsMsg.STR_ERR_SEARCH_FILES_PATH_IS_A_FILE_NAME, m_szDistributionPoint + l_szSearchPackagFileNames), ex);
                    break;
                }
                catch (UnauthorizedAccessException ex)
                {
                    m_iLastErrorCode = Lapsus.RT.ERR_SEARCH_FILES_THE_CALLER_DOES_NOT_HAVE_THE_REQUIRED_PERMISSION;
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, m_iSystemErrorCode, string.Format(Lapsus.ErrorsMsg.STR_ERR_SEARCH_FILES_THE_CALLER_DOES_NOT_HAVE_THE_REQUIRED_PERMISSION, System.Environment.UserName, m_szDistributionPoint), ex);
                    
                    break;
                }
                catch (ArgumentException ex)
                {
                    m_iLastErrorCode = Lapsus.RT.ERR_SEARCH_FILES_SEARCH_PATTERN_DOES_NOT_CONTAIN_A_VALID_PATTERN;
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, m_iSystemErrorCode, Lapsus.ErrorsMsg.STR_ERR_SEARCH_FILES_SEARCH_PATTERN_DOES_NOT_CONTAIN_A_VALID_PATTERN, ex);
                    break;
                }
                catch (Exception ex)
                {
                    m_iLastErrorCode = Lapsus.RT.ERR_CAN_NOT_FOUND_PACKAGE_FILE_IN_DISTRIBUTION_POINT;
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, m_iSystemErrorCode, Lapsus.ErrorsMsg.STR_ERR_CAN_NOT_FOUND_PACKAGE_FILE_IN_DISTRIBUTION_POINT, ex);
                    break;
                }

                if (l_ResultsPackagFileNames == null || l_ResultsPackagFileNames.Length == 0)
                {
                    m_iLastErrorCode = Lapsus.RT.ERR_CAN_NOT_FOUND_PACKAGE_FILE_IN_DISTRIBUTION_POINT;
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, 0, string.Format(Lapsus.ErrorsMsg.STR_ERR_CAN_NOT_FOUND_PACKAGE_FILE_IN_DISTRIBUTION_POINT, m_szDistributionPoint, m_szProjectSymbol), null);
                    break;
                }

                //Start to read the Max SN in this files
                for (int i = 0; i < l_ResultsPackagFileNames.Length; ++i)
                {
                    l_szTempFileName = l_ResultsPackagFileNames[i];

                    //get the file name 
                    l_index = l_szTempFileName.LastIndexOf('\\');
                    l_szTempFileName = l_szTempFileName.Substring(l_index + 1);

                    try
                    {
                       GetSNFromPackageFileName(l_szTempFileName, ref l_CurrentSN);
                       bRet = true;
                    }
                    catch (ExceptionFFEngine ex)
                    {
                        bRet = false;

                        m_iErrorCode = ExceptionFFEngine.GetException(ex, Lapsus.Packager.ERR_PACKAGE_UTIL_COMMON, ref m_szErrorMsg);
                    }
                    

                    if (l_CurrentSN > l_MaxSN)
                    {
                        l_MaxSN = l_CurrentSN;
                        l_iMaxSNIndex = i;
                    }

                }//end of for

                if (l_MaxSN == 0)
                {
                    bRet = false;
                }
                else
                {
                    m_iErrorCode = Lapsus.FF_NO_ERRORS;
                    m_szErrorMsg = null;
                    m_szAdditionalInfo = null;
                    bRet = true;

                }
                
                if (bRet == false)
                {
                    break;
                }

                //set the SN and the PackageFile path
                m_uiPackageSerialNamber = l_MaxSN;
                m_szFullPathToPackageFile = l_ResultsPackagFileNames[l_iMaxSNIndex];


            } while (false);

            return bRet;
        }

        protected virtual bool VerifyNeedToDownload(ref bool a_bNeedToDownload, LcFileObject a_LcFileObject)
        {
            //for  skip the LC real Engine set the mode to flase do i alwas need to do Download
            //and the file mode set to no acsion for not copy the temp file into the LC path

            a_bNeedToDownload = true;
            a_LcFileObject.OperationOnLc = FileWorkMode.eNoAction;
            return true;
        }

        protected bool InitPackageInterface()
        {
            m_FFPUtilsIntrface = new FFPReadUtils();

            m_iLastErrorCode = m_FFPUtilsIntrface.InitPackageForRead(m_szFullPathToPackageFile);

            if (m_iLastErrorCode != Lapsus.FF_NO_ERRORS)
            {
                m_szAdditionalInfo = m_FFPUtilsIntrface.GetAdditionalInfo();
                m_szErrorMsg = m_FFPUtilsIntrface.GetErrorString();
                m_FFPUtilsIntrface = null;
                //GenerateException(m_iLastErrorCode, 0, m_FFPUtilsIntrface.GetErrorString(), null, m_FFPUtilsIntrface.GetAdditionalInfo());
               
               

                return false;
            }

            return true;
        }

        protected virtual bool GetPackageSN()
        {
            bool bRet = true;

            do
            {
                if (m_FFPUtilsIntrface == null)
                {
                    if (!InitPackageInterface())
                    {
                        bRet = false;
                        break;
                    }
                }

                //get the SN of the Package
               m_iLastErrorCode=m_FFPUtilsIntrface.GetPackageSN(ref m_uiPackageSerialNamber);

               if (m_iLastErrorCode != Lapsus.FF_NO_ERRORS)
               {
                   ExceptionFFEngine.ThrowException(m_iLastErrorCode, 0, m_FFPUtilsIntrface.GetErrorString(), null);
                   
                   bRet = false;
                   break;
               }
            
            } while (false);

            return bRet;
        }

        protected bool VerifyLCFileTime(string a_szLCFullPathFile)
        {
            bool bRet = false;
            DateTime l_CurretTime;
            DateTime l_FileCreateTime;
            TimeSpan l_TimeSpan;
           

            do
            {
                try
                {

                    //get the curret tile 
                    l_CurretTime = DateTime.Now;

                    //get the file create time
                    l_FileCreateTime = File.GetCreationTime(a_szLCFullPathFile);

                    //copmter the times
                    l_TimeSpan =(TimeSpan)(l_CurretTime - l_FileCreateTime);

                    if (l_TimeSpan.Days > m_MaxDayIvalied)
                    {
                        bRet = false;
                    }
                    else
                    {
                        bRet = true;
                    }

                }
                catch (Exception)
                {
                    bRet = false;
                    break;
                }
            
            
            } while (false);

            return bRet;
        }



        #region IDisposable Members

        public override void Dispose()
        {

            base.Dispose();

            if (m_FFPUtilsIntrface != null)
            {
                m_FFPUtilsIntrface.Dispose();
            }
        }

        #endregion
    }
}
