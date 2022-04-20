using Docomotion.Shared.ComDef;
using Docomotion.Shared.ErrorHandler;
using Docomotion.Shared.UtlFFP.LC.LCFile;

namespace Docomotion.Shared.UtlFFP.LC.LCEngine
{
    class FromDataTagLcEngine:FormLcEngine
    {
        protected override bool DownloadFromDistributionPoint(LcFileObject a_LcFileObject)
        {
            bool bRet = true;
            string l_pFormDataTagBuffer = null;
            string l_szTempFolderPath = null;

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

                //read the Form buffer 
                m_iLastErrorCode = m_FFPUtilsIntrface.GetFFRTags(m_uiFormId, ref l_pFormDataTagBuffer);

                if (m_iLastErrorCode != Lapsus.FF_NO_ERRORS)
                {
                    bRet = false;
                    ExceptionFFEngine.ThrowException(m_iLastErrorCode, 0, m_FFPUtilsIntrface.GetErrorString(), null);
                    break;
                }

                //save the Heder in the LC file object 
                a_LcFileObject.SetLcBuffer(l_pFormDataTagBuffer);

            } while (false);

            return bRet;
        }

        protected override string BuildLCFileName()
        {
            return string.Format("{0}{1}\\{2}_{3:0000}_{4:00000}_{5:0000}_{6:00000}_{7:00000}.xml", m_szLCFolder,
                                                                                        m_szProjectSymbol,
                                                                                        LcDeines.DATA_TAG_START_FILE_NAME,
                                                                                        m_uiPackageSerialNamber,
                                                                                        m_uiFormId,
                                                                                        m_uiFormVresion,
                                                                                        m_sFromCRC,
                                                                                        m_FormExpirationDate);
        }

        protected override string BuildLCSerachString()
        {
            return string.Format("{0}_????_{1:00000}_????_?????_?????.xml", LcDeines.DATA_TAG_START_FILE_NAME,
                                                                  m_uiFormId);
        }
    }
}
