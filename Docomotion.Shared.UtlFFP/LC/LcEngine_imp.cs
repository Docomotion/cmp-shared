using System;
using Docomotion.Shared.ComDef;
using Docomotion.Shared.ErrorHandler;
using Docomotion.Shared.UtlFFP.LC.LCEngine;

namespace Docomotion.Shared.UtlFFP.LC
{
    /// <summary>
    /// LC Engine 
    /// </summary>
    public class LcEngine_imp :BaseFFPUtl,ILcEngine
    {

        protected bool m_bInit = false;
        

        protected int m_iLastErrorCode = 0;

        protected bool Init()
        {
            bool bRet = true;
            do
            {
                
                m_bInit = true;

            } while (false);

            return bRet;
        }

        public int ActivateLcEngine(string a_szDPPath, 
                                    string a_szGroupSymbol, 
                                    uint a_uiFormID, 
                                    string a_szLcPath, 
                                    int a_TimeToVailed, 
                                    LCFilesTypes a_eRequestType, 
                                    ref IntPtr a_pBuffer, 
                                    ref ulong a_pBufferSize, 
                                    ref string a_szErrorMsg)
        {
            BaseLcEngine l_BaseLcEngine = null;

            string l_sTemp = "";

            return InvokLcEngine(ref l_BaseLcEngine, a_szDPPath, a_szGroupSymbol, a_uiFormID, a_szLcPath, a_TimeToVailed, a_eRequestType, ref a_pBuffer, ref a_pBufferSize, ref a_szErrorMsg, ref l_sTemp);
            
        }


		public int InnerActivateLcEngine(string a_szDPPath, string a_szGroupSymbol, uint a_uiFormID, string a_szLcPath, int a_TimeToVailed, LCFilesTypes a_eRequestType, ref uint a_uiFormVersion, ref ushort a_uiForamSignature, ref IntPtr a_pBuffer, ref ulong a_pBufferSize, ref uint a_uiPackageSN, ref string a_szErrorMsg, ref string a_sAdditionalInfo)
        {
			try
			{								
				return ActivateLcEngine(a_szDPPath, a_szGroupSymbol, a_uiFormID, a_szLcPath, a_TimeToVailed, a_eRequestType, ref  a_uiFormVersion, ref  a_uiForamSignature, ref a_pBuffer, ref  a_pBufferSize, ref  a_uiPackageSN, ref  a_szErrorMsg, ref  a_sAdditionalInfo);
			}
			catch (System.Exception)
			{
				return -1;
			}
			
        }


		public int ActivateLcEngine(string a_szDPPath, string a_szGroupSymbol, uint a_uiFormID, string a_szLcPath, int a_TimeToVailed, LCFilesTypes a_eRequestType, ref uint a_uiFormVersion, ref ushort a_uiForamSignature, ref IntPtr a_pBuffer, ref ulong a_pBufferSize, ref uint a_uiPackageSN, ref string a_szErrorMsg, ref string a_sAdditionalInfo)
		{
			BaseLcEngine l_BaseLcEngine = null;
            int bRet = Lapsus.FF_NO_ERRORS;
			ushort l_FormExpirationDate = 0;
			a_uiPackageSN = 0;

			do
			{

				bRet = InvokLcEngine(ref l_BaseLcEngine, a_szDPPath, a_szGroupSymbol, a_uiFormID, a_szLcPath, a_TimeToVailed, a_eRequestType, ref a_pBuffer, ref a_pBufferSize, ref a_szErrorMsg, ref a_sAdditionalInfo);

                if (bRet != Lapsus.FF_NO_ERRORS)
				{
					break;
				}

				if (l_BaseLcEngine.GetFFPReadUtils != null)
				{
					bRet = l_BaseLcEngine.GetFFPReadUtils.GetFormInfo(a_uiFormID, ref a_uiFormVersion, ref a_uiForamSignature, ref l_FormExpirationDate);

                    if (bRet != Lapsus.FF_NO_ERRORS)
					{
						a_szErrorMsg = l_BaseLcEngine.GetFFPReadUtils.GetErrorString();
						a_sAdditionalInfo = l_BaseLcEngine.GetFFPReadUtils.GetAdditionalInfo();
						break;
					}
					l_BaseLcEngine.GetFFPReadUtils.GetPackageSN(ref a_uiPackageSN);

				}

			} while (false);

			return bRet;

		}


        public void FreeMemory(IntPtr a_Prt)
        {
            if (a_Prt != IntPtr.Zero)
            {
                System.Runtime.InteropServices.Marshal.FreeHGlobal(a_Prt);
            }
        }



        private int InvokLcEngine(ref BaseLcEngine    a_BaseLcEngine,
                                 string          a_szDPPath,
                                 string          a_szGroupSymbol,
                                 uint            a_uiFormID,
                                 string          a_szLcPath,
                                 int             a_TimeToVailed,
                                 LCFilesTypes    a_eRequestType,
                                 ref IntPtr      a_pBuffer,
                                 ref ulong       a_pBufferSize,
                                 ref string      a_szErrorMsg,
                                 ref string       a_sAdditionalInfo)
        {

            bool bRet = true;


            string additionalInfo = "";
            do
            {

                try
                {
                    additionalInfo = $"Start InvokLcEngine. DPPath={a_szDPPath};GroupSymbol={a_szGroupSymbol};FormID={a_uiFormID};LcPath={a_szLcPath}";
                    //reset the object membres
                    if (m_bInit == false)
                    {

                        if (!Init())
                        {
                            bRet = false;
                            break;
                        }
                    }
                    m_iLastErrorCode = Lapsus.FF_NO_ERRORS;

                    if (a_szGroupSymbol.Length != LcDeines.GROUP_SYMBOL_SIZE)
                    {
                        m_iLastErrorCode = Lapsus.RT.ERR_PROJECT_SYMBOL_SIZE_IS_INVALID;
                        bRet = false;
                        ExceptionFFEngine.ThrowException(m_iLastErrorCode, 0, Lapsus.ErrorsMsg.STR_ERR_PROJECT_SYMBOL_SIZE_IS_INVALID, null);
                        break;
                    }

                    additionalInfo = $"{additionalInfo}\nFileType={a_eRequestType}";
                    //define the request  file type
                    switch (a_eRequestType)
                    {
                        case LCFilesTypes.eForm:
                            a_BaseLcEngine = new FormLcEngine();
                            bRet = ((FormLcEngine)a_BaseLcEngine).Init(a_szDPPath, a_szGroupSymbol, a_uiFormID, a_szLcPath, a_TimeToVailed);
                            break;

                        case LCFilesTypes.eFormDataTag:
                            a_BaseLcEngine = new FromDataTagLcEngine();
                            bRet = ((FromDataTagLcEngine)a_BaseLcEngine).Init(a_szDPPath, a_szGroupSymbol, a_uiFormID, a_szLcPath, a_TimeToVailed);

                            break;

                        case LCFilesTypes.ePackageHeader:
                            a_BaseLcEngine = new PackageHederLcEngine();
                            bRet = a_BaseLcEngine.Init(a_szDPPath, a_szGroupSymbol, a_szLcPath, a_TimeToVailed);
                            break;
                        default:
                            m_iLastErrorCode = Lapsus.RT.ERR_UNKNOWN_REQUEST_TYPE;
                            bRet = false;
                            ExceptionFFEngine.ThrowException(m_iLastErrorCode, 0, string.Format(Lapsus.ErrorsMsg.STR_ERR_UNKNOWN_REQUEST_TYPE, a_eRequestType),null);
                            break;

                    }

                    //init the object 
                    if (bRet == false)
                    {
                        m_iLastErrorCode = a_BaseLcEngine.LastErrorCode;
                        additionalInfo = $"{additionalInfo}\nError Init LcEngine. Res={m_iLastErrorCode}";
                        break;
                    }

                    additionalInfo = $"{additionalInfo}\nAfter Init LcEngine. Res={m_iLastErrorCode}";

                    //run the LC Engine
                    if (!a_BaseLcEngine.RunLCEngine(ref a_pBuffer, ref a_pBufferSize))
                    {
                        m_iLastErrorCode = a_BaseLcEngine.LastErrorCode;
                        bRet = false;
                        a_szErrorMsg = $"{additionalInfo}\nError Run LcEngine. Res={m_iLastErrorCode};ErrMsg={a_BaseLcEngine.ErrorString};AddInfo={ a_BaseLcEngine.AdditionalInfo}";
                        break;
                    }

                    additionalInfo = $"{additionalInfo}\nAfter Run LcEngine. Res={m_iLastErrorCode}";


                    if (a_BaseLcEngine != null) a_BaseLcEngine.Dispose();

                    additionalInfo = $"{additionalInfo}\nAfter Dispose LcEngine.";
                }
                catch (ExceptionFFEngine ex)
                {
                    m_iLastErrorCode = ExceptionFFEngine.GetException(ex, Lapsus.Packager.ERR_PACKAGE_UTIL_COMMON, ref m_szErrorMsg);

                    a_szErrorMsg = $"{m_szErrorMsg}\n\r[{additionalInfo}]";

                    if(a_BaseLcEngine != null)  a_BaseLcEngine.Dispose();
                }
                catch (Exception ex)
                {

                    m_iLastErrorCode = Lapsus.RT.ERR_LC_FATAL_ERROR;
                    a_szErrorMsg = $"{ex.Message}\n\r{Lapsus.ErrorsMsg.STR_ERR_LC_FATAL_ERROR}\n\r[{additionalInfo}]";

                    if (a_BaseLcEngine != null) a_BaseLcEngine.Dispose();
                    break;
                }
            } while (false);

            //if it waring do not return error code 
            if (m_iLastErrorCode >= Lapsus.Packager.WARN_START_NUMBER && m_iLastErrorCode <= Lapsus.Packager.WARN_END_NUMBER)
            {
                a_szErrorMsg = null;
                m_iLastErrorCode = Lapsus.FF_NO_ERRORS;
            }
            return m_iLastErrorCode;
        }



        
    }

}
