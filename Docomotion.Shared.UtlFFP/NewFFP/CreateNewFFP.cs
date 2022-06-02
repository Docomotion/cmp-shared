using System;
using System.IO;
using Docomotion.Shared.ComDef;
using Docomotion.Shared.ErrorHandler;

namespace Docomotion.Shared.UtlFFP.NewFFP
{

    public class LogObject
    {
        protected static FileStream m_FileStream = null;
        protected static StreamWriter m_StreamWriter = null;
        protected static bool m_Flag = false;

        public static void init(string a_sfullLogPath, string a_sFileMode)
        {
            if (m_FileStream == null && m_StreamWriter == null)
            {

                if (!a_sFileMode.Equals("Append"))
                {
                    m_FileStream = new FileStream(a_sfullLogPath, FileMode.OpenOrCreate, FileAccess.Write);
                }
                else
                {
                    if (File.Exists(a_sfullLogPath))
                        m_FileStream = new FileStream(a_sfullLogPath, FileMode.Append, FileAccess.Write);
                    else
                        m_FileStream = new FileStream(a_sfullLogPath, FileMode.Create, FileAccess.Write);

                }
                m_StreamWriter = new StreamWriter(m_FileStream);

                m_Flag = true;
            }
        }
        public static void Finalize()
        {
            if (m_Flag)
            {
                m_StreamWriter.Flush();
                m_FileStream.Close();
                m_Flag = false;
            }

        }
        public static void Log(string a_sStringToLog)
        {
            if (m_Flag)
            {
                DateTime l_oTime = DateTime.Now;
                m_StreamWriter.WriteLine(string.Format("{0:00}/{1:00}/{2:0000} - {3}:{4:000} -> {5}", l_oTime.Day, l_oTime.Month, l_oTime.Year, l_oTime.ToLongTimeString(), l_oTime.Millisecond, a_sStringToLog));
                m_StreamWriter.Flush();
            }
        }
    }

    public class FFPReadUtils : BaseFFPUtl, IAfFFPReadUtils, IDisposable
    {
        private InternaReadFFPUtl m_oInternaReadFFPUtl = null;
        protected string m_szErrorString = null;
        bool m_bIsInit = false;





        #region IAfFFPUtils Members

        public int InitPackageForRead(string szFFPFileName)
        {
            int bRet = 0;

            do
            {

                try
                {
                    m_oInternaReadFFPUtl = new InternaReadFFPUtl();
                    m_oInternaReadFFPUtl.Init(szFFPFileName);
                    m_bIsInit = true;

                    

                }
                catch (ExceptionFFEngine ex)
                {
                    bRet = ExceptionFFEngine.GetException(ex, Lapsus.Packager.ERR_PACKAGE_UTIL_COMMON, ref m_szErrorMsg);

                    break;


                }
                catch (Exception ex)
                {
                    bRet = Lapsus.RT.ERR_INIT_PACKAGE_FOR_READ_FATAL_ERROR;
                    m_szErrorMsg = Lapsus.ErrorsMsg.STR_ERR_INIT_PACKAGE_FOR_READ_FATAL_ERROR;
                    m_szAdditionalInfo = ex.Message;
                    break;
                }



            } while (false);

            return bRet;
        }

        public int GetFFPHeader(ref string pFFRPHeaderBuffer)
        {
            int bRet = 0;

            do
            {
                try
                {
                    if (m_bIsInit == false)
                    {
                        bRet = Lapsus.RT.ERR_OBJECT_NOT_INIT;
                        ExceptionFFEngine.ThrowException(bRet, 0, Lapsus.ErrorsMsg.STR_ERR_OBJECT_NOT_INIT, null);
                        break;
                    }

                    m_oInternaReadFFPUtl.ReadFFPHeader(ref pFFRPHeaderBuffer);

                }
                catch (ExceptionFFEngine ex)
                {
                    bRet = ExceptionFFEngine.GetException(ex, Lapsus.Packager.ERR_PACKAGE_UTIL_COMMON, ref m_szErrorMsg);
                    break;
                }
                catch (Exception ex)
                {

                    bRet = Lapsus.Packager.ERR_FFP_HEADER_FATAL_ERROR;
                    m_szErrorMsg = Lapsus.ErrorsMsg.STR_ERR_FFP_HEADER_FATAL_ERROR;
                    m_szAdditionalInfo = ex.Message;
                    break;
                }



            } while (false);

            return bRet;
        }

        public int GetFFRData(uint FromID, ref byte[] pFFRFromDataBuffer)
        {
            int bRet = 0;
            uint l_FormVersion = 0;
            ushort l_FormCRC = 0;
            ushort l_FormExpired = 0;



            do
            {
                try
                {

                    if (m_bIsInit == false)
                    {
                        bRet = Lapsus.RT.ERR_OBJECT_NOT_INIT;
                        ExceptionFFEngine.ThrowException(bRet, 0, Lapsus.ErrorsMsg.STR_ERR_OBJECT_NOT_INIT, null);
                        break;
                    }
                    m_oInternaReadFFPUtl.GetFormInfo(FromID, ref l_FormVersion, ref l_FormCRC, ref l_FormExpired);


                    m_oInternaReadFFPUtl.IsFormExpired(l_FormExpired);


                    m_oInternaReadFFPUtl.GetFFRData(FromID, ref pFFRFromDataBuffer);



                }
                catch (ExceptionFFEngine ex)
                {
                    bRet = ExceptionFFEngine.GetException(ex, Lapsus.Packager.ERR_PACKAGE_UTIL_COMMON, ref m_szErrorMsg);
                    break;
                }
                catch (Exception ex)
                {

                    bRet = Lapsus.RT.ERR_GET_FFR_DATA_FATAL_ERROR;
                    m_szErrorMsg = Lapsus.ErrorsMsg.STR_ERR_GET_FFR_DATA_FATAL_ERROR;
                    m_szAdditionalInfo = ex.Message;
                    break;
                }



            } while (false);

            return bRet;
        }

        public int GetFFRTags(uint FromID, ref string pFFRFromTagsBuffer)
        {
            int bRet = 0;
            uint l_FormVersion = 0;
            ushort l_FormCRC = 0;
            ushort l_FormExpired = 0;


            do
            {
                try
                {
                    if (m_bIsInit == false)
                    {
                        bRet = Lapsus.RT.ERR_OBJECT_NOT_INIT;
                        ExceptionFFEngine.ThrowException(bRet, 0, Lapsus.ErrorsMsg.STR_ERR_OBJECT_NOT_INIT, null);
                        break;
                    }

                    m_oInternaReadFFPUtl.GetFormInfo(FromID, ref l_FormVersion, ref l_FormCRC, ref l_FormExpired);

                    m_oInternaReadFFPUtl.IsFormExpired(l_FormExpired);

                    m_oInternaReadFFPUtl.GetFFRTags(FromID, ref pFFRFromTagsBuffer);

                }
                catch (ExceptionFFEngine ex)
                {
                    bRet = ExceptionFFEngine.GetException(ex, Lapsus.Packager.ERR_PACKAGE_UTIL_COMMON, ref m_szErrorMsg);
                    break;
                }
                catch (Exception ex)
                {

                    bRet = Lapsus.Packager.ERR_GET_FFR_TAGS_FATAL_ERROR;
                    m_szErrorMsg = Lapsus.ErrorsMsg.STR_ERR_GET_FFR_TAGS_FATAL_ERROR;
                    m_szAdditionalInfo=ex.Message;
                    break;
                }



            } while (false);

            return bRet;
        }

        public int GetPackageSN(ref uint a_PackageSN)
        {
            int bRet = 0;

            do
            {

                try
                {
                    if (m_bIsInit == false)
                    {
                        bRet = Lapsus.RT.ERR_OBJECT_NOT_INIT;
                        ExceptionFFEngine.ThrowException(bRet, 0, Lapsus.ErrorsMsg.STR_ERR_OBJECT_NOT_INIT, null);
                        break;
                    }
                    m_oInternaReadFFPUtl.GetFFPSN(ref a_PackageSN);
                }
                catch (ExceptionFFEngine ex)
                {
                    bRet = ExceptionFFEngine.GetException(ex, Lapsus.Packager.ERR_PACKAGE_UTIL_COMMON, ref m_szErrorMsg);
                    break;
                }
                catch (Exception ex)
                {

                    bRet = Lapsus.Packager.ERR_GET_PACKAGE_SN_FATAL_ERROR;
                    m_szErrorMsg = Lapsus.ErrorsMsg.STR_ERR_GET_PACKAGE_SN_FATAL_ERROR;
                    m_szAdditionalInfo = ex.Message;
                    break;
                }
            } while (false);

            return bRet;
        }

        public int GetFormInfo(uint a_FromID, ref uint a_FormVresion, ref ushort a_FormSignature, ref ushort a_FormExpirationDate)
        {
            int bRet = 0;
            do
            {
                try
                {
                    m_oInternaReadFFPUtl.GetFormInfo(a_FromID, ref a_FormVresion, ref a_FormSignature, ref a_FormExpirationDate);
                }
                catch (ExceptionFFEngine ex)
                {
                    bRet = ExceptionFFEngine.GetException(ex, Lapsus.Packager.ERR_PACKAGE_UTIL_COMMON, ref m_szErrorMsg);
                    break;

                }
                catch (Exception ex)
                {

                    bRet = Lapsus.RT.ERR_GET_FORM_INFO_FATAL_ERROR;
                    m_szErrorMsg = Lapsus.ErrorsMsg.STR_ERR_GET_FORM_INFO_FATAL_ERROR;
                    m_szAdditionalInfo = ex.Message;
                    break;
                }

            } while (false);

            return bRet;
        }

        public int IsFormExpired(ushort a_FormExpiredDatas, ref bool a_IsExpired)
        {

            int Ret = Lapsus.FF_NO_ERRORS;
            bool bInitObject = false;

            try
            {
                if (m_oInternaReadFFPUtl == null)
                {
                    m_oInternaReadFFPUtl = new InternaReadFFPUtl();
                    bInitObject = true;

                }


                m_oInternaReadFFPUtl.IsFormExpired(a_FormExpiredDatas);
                a_IsExpired = true;

            }
            catch (ExceptionFFEngine ex)
            {
                Ret = ExceptionFFEngine.GetException(ex, Lapsus.Packager.ERR_PACKAGE_UTIL_COMMON, ref m_szErrorMsg);
                a_IsExpired = false;
            }
            catch (Exception ex)
            {

                Ret = Lapsus.RT.ERR_IS_FORM_EXPIRED_FATAL_ERROR;
                a_IsExpired = false;
                m_szErrorMsg = Lapsus.ErrorsMsg.STR_ERR_IS_FORM_EXPIRED_FATAL_ERROR;
                m_szAdditionalInfo = ex.Message;

            }

            if (bInitObject == true)
            {
                m_oInternaReadFFPUtl = null;
            }


            return Ret;

        }

        public void GetSystemStartTime(ref string a_szSystemStartTime)
        {
            a_szSystemStartTime = Definition.START_SYSTEM_TIME;
        }

        public string GetErrorString()
        {
            return m_szErrorMsg;
        }


        #endregion

        private void init()
        {
            LogObject.init("c:\\package2.txt", "append");
        }

        #region IDisposable Members

        public override void Dispose()
        {
            base.Dispose();

            if (m_oInternaReadFFPUtl != null)
            {
                m_oInternaReadFFPUtl.Dispose();
            }
        }

        #endregion

        #region IAfFFPReadUtils Members


        public string GetAdditionalInfo()
        {
            return m_szAdditionalInfo;
        }

        #endregion
    }


    public class CreateNewFFP : BaseFFPUtl, IAfFFPWriteUtils
    {

        #region IAfFFPWriteUtils Members        

        public int CreatePackage(string szInputDir, string szOutputDir, string szWorkingDir, bool bOverWrite, ref CFFPHeader pPackageHeader, string szFFSXmlList, ref string szErrorString,ref string szAdditionalInfo)
        {
            int bRet = Lapsus.FF_NO_ERRORS;
            InternalCreateNewFFP m_oCreateFFP = new InternalCreateNewFFP();
            bRet = m_oCreateFFP.CreatePackage(szInputDir, szOutputDir, szWorkingDir, bOverWrite, ref  pPackageHeader, szFFSXmlList, ref  szErrorString, ref szAdditionalInfo);
          
            return bRet;
        }
        #endregion

    }

        
}


   


