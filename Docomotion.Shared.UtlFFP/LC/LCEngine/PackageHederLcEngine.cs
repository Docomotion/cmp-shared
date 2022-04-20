using System;
using System.IO;
using Docomotion.Shared.ComDef;
using Docomotion.Shared.ErrorHandler;
using Docomotion.Shared.UtlFFP.LC.LCFile;

namespace Docomotion.Shared.UtlFFP.LC.LCEngine
{
    class PackageHederLcEngine:BaseLcEngine
    {



        protected override string BuildLCFileName()
        {
            return string.Format("{0}{1}\\{2}_{3:0000}.xml", m_szLCFolder,
                                                             m_szProjectSymbol,
                                                             LcDeines.PACKAGE_HREAD_START_FILE_NAME,
                                                              m_uiPackageSerialNamber);
        }

        protected override string BuildLCSerachString()
        {
            return string.Format("{0}_????.xml", LcDeines.PACKAGE_HREAD_START_FILE_NAME);
        }

        protected override bool VerifyNeedToDownload(ref bool a_bNeedToDownload, LcFileObject a_LcFileObject)
        {
            bool bRet = true;
            string l_szSerachPath = null;
            string l_szSerachDirtory=null;
            string[] l_szResult = null;
            
            string l_szLCFullPathFile = null;
            int l_index = 0;
            uint l_ulLcFileSN = 0;

            string l_szFileName = null;

            do
            {
              
               //build the serach path in the LC folder 
                l_szSerachDirtory=string.Format("{0}{1}",m_szLCFolder,m_szProjectSymbol);

                l_szSerachPath = BuildLCSerachString();

              //chake if file found 
                try
                {
                    l_szResult = Directory.GetFiles(l_szSerachDirtory, l_szSerachPath, SearchOption.TopDirectoryOnly);
                }
                catch (Exception)
                {
                    l_szResult = null; 
                }

                //if mach files found in the LC dirtory 
                if (l_szResult == null || l_szResult.Length == 0)
                {
                    a_LcFileObject.OperationOnLc = FileWorkMode.eCreateNewLcFile;
                    a_bNeedToDownload=true;
                    break;
                }

                l_szLCFullPathFile = l_szResult[0];

                //get the file name
                l_index = l_szLCFullPathFile.LastIndexOf('\\');
                l_szFileName = l_szLCFullPathFile.Substring(l_index + 1);

                //get the SN from the LC file 
                try
                {
                   
                    l_ulLcFileSN = uint.Parse(l_szFileName.Substring(LcDeines.PACKAGE_HREAD_SN_INDEX,LcDeines.PACKAGE_HREAD_SN_SIZE));
                    if (l_ulLcFileSN != m_uiPackageSerialNamber)
                    {
                        l_ulLcFileSN = uint.MaxValue;
                    }
                }
                catch (Exception)
                {
                    l_ulLcFileSN = uint.MaxValue;
                }

                if (l_ulLcFileSN == uint.MaxValue)
                {
                    a_LcFileObject.OldLcFullPath = l_szLCFullPathFile;
                    a_LcFileObject.OperationOnLc = FileWorkMode.eReplaceLcFile;
                    a_bNeedToDownload = true;
                    break;
                }
                
                
                //virfiled if the file is legal
                if (!VerifyLCFileTime(l_szLCFullPathFile))
                {
                    //time out
                    a_LcFileObject.OldLcFullPath = l_szLCFullPathFile;
                    a_LcFileObject.OperationOnLc = FileWorkMode.eReplaceLcFile;
                    a_bNeedToDownload = true;
                    break;

                }
                
                  

                //copy the file from the LC to buffer 
                a_LcFileObject.LcTragetFullPath = l_szLCFullPathFile;
                if (!a_LcFileObject.ReadLcFileToBuffer())
                {
                    a_LcFileObject.OperationOnLc = FileWorkMode.eNoAction;
                    a_bNeedToDownload = true;
                    break;

                }
                
                a_bNeedToDownload = false;
                a_LcFileObject.OperationOnLc = FileWorkMode.eNoAction;


                    
               

            
            
            
            } while (false);

            return bRet;
        }

        protected override bool DownloadFromDistributionPoint(LcFileObject a_LcFileObject)
        {
            bool bRet = true;
            string l_szPackageHeder=null;
           

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

                //Get the Package Heder
                m_iLastErrorCode = m_FFPUtilsIntrface.GetFFPHeader(ref l_szPackageHeder);

                if (m_iLastErrorCode != Lapsus.FF_NO_ERRORS)
                {
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, 0, m_FFPUtilsIntrface.GetErrorString(), null);
                    break;
                }

                //save the Heder in the LC file object 
                a_LcFileObject.SetLcBuffer(l_szPackageHeder);


            
            
            
            } while (false);

            return bRet;
        }

        

    }
}
