using System;
using System.IO;
using Docomotion.Shared.ComDef;
using Docomotion.Shared.ErrorHandler;
using Docomotion.Shared.UtlFFP.LC.LCFile;

namespace Docomotion.Shared.UtlFFP.LC.LCEngine
{
    class FormLcEngine:BaseLcEngine
    {
        protected uint m_uiFormId=0;
        protected uint m_uiFormVresion = 0;
        protected ushort m_sFromCRC = 0;
        protected ushort m_FormExpirationDate = ushort.MaxValue;

        public  bool Init(string a_szDPPath, string a_szGroupSimbol, uint a_ulFormId, string a_szLcPath, int m_aTimeToVailed)
        {
           
            bool bRet = true;

            do
            {
                if (!base.Init(a_szDPPath, a_szGroupSimbol, a_szLcPath, m_aTimeToVailed))
                {
                    bRet = false;
                    break;
                }

                m_uiFormId = a_ulFormId;
                m_uiFormVresion = 0;
                m_sFromCRC = 0;

            
            
            } while (false);

            return bRet;


        }


        protected override string BuildLCSerachString()
        {
            return string.Format("{0}_????_{1:00000}_????_?????_?????.ffr", LcDeines.FROM_START_FILE_NAME,
                                                                    m_uiFormId);
        }

        protected override string BuildLCFileName()
        {

            
            
            return string.Format("{0}{1}\\{2}_{3:0000}_{4:00000}_{5:0000}_{6:00000}_{7:00000}.ffr" , m_szLCFolder,
                                                                                        m_szProjectSymbol,
                                                                                        LcDeines.FROM_START_FILE_NAME,
                                                                                        m_uiPackageSerialNamber,
                                                                                        m_uiFormId,
                                                                                        m_uiFormVresion,
                                                                                        m_sFromCRC,
                                                                                        m_FormExpirationDate);

            
        }

        
        protected override bool VerifyNeedToDownload(ref bool a_bNeedToDownload, LcFileObject a_LcFileObject)
        {
            bool bRet = true;
            string l_szSerachPath = null;
            string l_szSerachDirtory = null;
            string[] l_szResult = null;
            string l_szLCFullPathFile = null;

            do
            {

                //build the serach path 
                l_szSerachPath = string.Format("{0}{1}", m_szLCFolder, m_szProjectSymbol);
                l_szSerachDirtory = BuildLCSerachString();


                //serach the file in the LC path 
                try
                {
					if(Directory.Exists(l_szSerachPath))
						l_szResult = Directory.GetFiles(l_szSerachPath, l_szSerachDirtory, SearchOption.TopDirectoryOnly);
                }
                catch (Exception)
                {
                    l_szResult = null;
                }

                if (l_szResult == null || l_szResult.Length == 0)
                {
                    a_LcFileObject.OperationOnLc = FileWorkMode.eCreateNewLcFile;
                    a_bNeedToDownload = true;
                    break;
                }

                l_szLCFullPathFile = l_szResult[0];


                //vrefide id the Form expried
                if (!ValidateFormExpired(l_szLCFullPathFile))
                {
                    a_LcFileObject.OperationOnLc = FileWorkMode.eReplaceLcFile;
                    a_LcFileObject.OldLcFullPath = l_szLCFullPathFile;
                    a_bNeedToDownload = true;
                    break;
                }

                //vrefide the time of the file 
                if (!VerifyLCFileTime(l_szLCFullPathFile))
                {
                    a_LcFileObject.OperationOnLc = FileWorkMode.eReplaceLcFile;
                    a_LcFileObject.OldLcFullPath = l_szLCFullPathFile;
                    a_bNeedToDownload = true;
                    break;

                }
                
                
                //vrefide the SN of the file
                if (ValidateSN(l_szLCFullPathFile, ref a_bNeedToDownload, a_LcFileObject))
                {
                    break;
                }

               


                

                //validate  the Signature 
                if (!ValidateSignature(l_szLCFullPathFile, ref a_bNeedToDownload, a_LcFileObject))
                {
                    bRet = false;
                    break;
                }


            } while (false);

            return bRet;
        }


        protected bool ValidateSN(string a_szLCFullPathFile, ref bool a_bNeedToDownload, LcFileObject a_LcFileObject)
        {
            string l_szFileName = null;
            int l_index = 0;
            uint l_ulLcFileSN = 0;
            bool bRet = true;

            do
            {
                //get the file name
                l_index = a_szLCFullPathFile.LastIndexOf('\\');
                l_szFileName = a_szLCFullPathFile.Substring(l_index + 1);

                try
                {

                    l_ulLcFileSN = uint.Parse(l_szFileName.Substring(LcDeines.PACKAGE_SN_INDEX, LcDeines.PACKAGE_SN_SIZE));
                    if (m_uiPackageSerialNamber == l_ulLcFileSN)
                    {

                        a_LcFileObject.LcTragetFullPath = a_szLCFullPathFile;
                        if (!a_LcFileObject.ReadLcFileToBuffer())
                        {
                            //read form LC fild do we need to download from DP
                            a_LcFileObject.OperationOnLc = FileWorkMode.eNoAction;
                            a_bNeedToDownload = true;
                            bRet = true;
                            break;
                        }

                        //we read the file from LC  
                        a_LcFileObject.OperationOnLc = FileWorkMode.eNoAction;
                        a_bNeedToDownload = false;
                        bRet = true;
                        break;

                    }


                    //the SN not match
                    bRet = false;
                    break;
                   


                }
                catch (Exception)
                {
                    a_LcFileObject.OperationOnLc = FileWorkMode.eReplaceLcFile;
                    a_LcFileObject.OldLcFullPath = a_szLCFullPathFile;
                    a_bNeedToDownload = true;
                    bRet = false;
                    break;
                }
            
            
            } while (false);

            return bRet;

        }
        
        protected bool ValidateSignature(string a_szLCFullPathFile, ref bool a_bNeedToDownload, LcFileObject a_LcFileObject)
        {
            bool bRet = true;

            uint l_uiLCFileFromVersion = 0;
            uint l_uiLCFileFromCRC = 0;
            string l_szFileName = null;
            int l_index = 0;

            do
            {
                //get the file name 
                //get the file name
                l_index = a_szLCFullPathFile.LastIndexOf('\\');
                l_szFileName = a_szLCFullPathFile.Substring(l_index + 1);

                try
                {
                    //extact the form ver and the from crc from the LC file nam
                    l_uiLCFileFromVersion = uint.Parse(l_szFileName.Substring(LcDeines.FORM_VERSION_INDEX, LcDeines.FORM_VERSION_SIZE));
                    l_uiLCFileFromCRC = uint.Parse(l_szFileName.Substring(LcDeines.FORM_SRC_INDEX, LcDeines.FORM_SRC_SIZE));
                }
                catch (Exception)
                {
                    a_LcFileObject.OperationOnLc = FileWorkMode.eReplaceLcFile;
                    a_LcFileObject.OldLcFullPath = a_szLCFullPathFile;
                    a_bNeedToDownload = true;
                    break;
                }


                //Get the SRC and the FROM Ver from the FFP file in the Distribution Point
                if (m_sFromCRC == 0 && m_uiFormVresion == 0)
                {
                    if (m_FFPUtilsIntrface == null)
                    {
                        if (!InitPackageInterface())
                        {
                            bRet = false;
                            break;
                        }
                    }

                    m_iLastErrorCode = m_FFPUtilsIntrface.GetFormInfo(m_uiFormId, ref m_uiFormVresion, ref m_sFromCRC, ref m_FormExpirationDate);
                    if (m_iLastErrorCode != Lapsus.FF_NO_ERRORS)
                    {

                        ExceptionFFEngine.ThrowException(m_iLastErrorCode, 0, m_FFPUtilsIntrface.GetErrorString(), null);
                        bRet = false;
                        break;
                    }


                }//end of get  SRC and the FROM Ver from the FFP file in the Distribution Point

                //compare  the scr and the ver 
                if (l_uiLCFileFromCRC != m_sFromCRC)
                {
                    a_LcFileObject.OperationOnLc = FileWorkMode.eReplaceLcFile;
                    a_LcFileObject.OldLcFullPath = a_szLCFullPathFile;
                    a_bNeedToDownload = true;
                    break;
                }

                //read the LC file to buffer
                a_LcFileObject.LcTragetFullPath = a_szLCFullPathFile;
                if (!a_LcFileObject.ReadLcFileToBuffer())
                {
                    m_iLastErrorCode = a_LcFileObject.LastErrorCode;
                    a_LcFileObject.OperationOnLc = FileWorkMode.eNoAction;
                    bRet = true;
                    a_bNeedToDownload = true;
                    break;

                }

                //reanme the LC file
                a_LcFileObject.OperationOnLc = FileWorkMode.eReNameLcFile;
                a_LcFileObject.OldLcFullPath = a_szLCFullPathFile;
                a_LcFileObject.LcTragetFullPath = BuildLCFileName();
                a_bNeedToDownload = false;

               




            } while (false);

            return bRet;
        }

        protected override bool DownloadFromDistributionPoint(LcFileObject a_LcFileObject)
        {
            bool bRet = true;
            byte[] l_pFormBuffer = null;
           
           

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
                //get the form info
                m_iLastErrorCode = m_FFPUtilsIntrface.GetFormInfo(m_uiFormId, ref m_uiFormVresion, ref m_sFromCRC,ref m_FormExpirationDate);

                if (m_iLastErrorCode != Lapsus.FF_NO_ERRORS)
                {
                    
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, 0, m_FFPUtilsIntrface.GetErrorString(), null);
                    break;
                }
                
                
                //read the Form buffer 
                m_iLastErrorCode = m_FFPUtilsIntrface.GetFFRData(m_uiFormId, ref l_pFormBuffer);

                if (m_iLastErrorCode != Lapsus.FF_NO_ERRORS)
                {
                    
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, 0, m_FFPUtilsIntrface.GetErrorString(), null);
                    break;
                }

                //save the buffer in the object
                a_LcFileObject.SetLcBuffer(l_pFormBuffer);
                
            
            } while (false);

            return bRet;
        }

        protected override bool GetPackageSN()
        {
            bool bRet = true;

            do
            {

                if (!base.GetPackageSN())
                {
                    bRet = false;
                    break;
                }

                m_iLastErrorCode = m_FFPUtilsIntrface.GetFormInfo(m_uiFormId, ref m_uiFormVresion, ref m_sFromCRC,ref m_FormExpirationDate);
                if (m_iLastErrorCode != Lapsus.FF_NO_ERRORS)
                {

                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, 0, m_FFPUtilsIntrface.GetErrorString(), null);
                    bRet = false;
                    break;
                }

            } while (false);

            return bRet;
        }

        protected bool ValidateFormExpired(string a_szFormPath)
        {
            bool bRet = true;
            ushort l_FormExpiredDays = 0;
            int l_ErrorCode = 0;
            bool l_IsFormExpired=false;
            int l_index = 0;
            string l_szFileName = null;

            do
            {
                try
                {

                    //get the file name
                    l_index = a_szFormPath.LastIndexOf('\\');
                    l_szFileName = a_szFormPath.Substring(l_index + 1);

                    if (!InitPackageInterface())
                    {
                        bRet = false;
                        break;
                    }

                    l_FormExpiredDays = ushort.Parse(l_szFileName.Substring(LcDeines.FORM_EXPIRED_INDEX, LcDeines.FORM_EXPIRED_SIZE));
                    l_ErrorCode = m_FFPUtilsIntrface.IsFormExpired(l_FormExpiredDays, ref l_IsFormExpired);

                    if (l_ErrorCode != Lapsus.FF_NO_ERRORS)
                    {
                        bRet = false;
                        ExceptionFFEngine.ThrowException(l_ErrorCode, 0, m_FFPUtilsIntrface.GetErrorString(), null);
                        break;
                    }
                    bRet = l_IsFormExpired;
                }
                catch (Exception)
                {
                    bRet = false;
                    break;
                }

            
            } while (false);

            return bRet;
        }
    }
}
