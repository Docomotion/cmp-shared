using System;
using System.Collections.Generic;
using System.Text;

using AutoFont.AfUtlFFP;

namespace AutoFont.AfUtlFFP.LC
{
    public class RunTimeLcEngine_imp : LcEngine_imp, IRunTimeLcEngine
    {
        protected uint m_iFromId = 0;
        protected int m_FormVersion = 0;
        protected int m_FormCRC = 0;


        protected bool Init(string a_szDPPath, string a_szGroupSimbol, string a_szLcPath, string a_szTempLcPath,string a_szTimeToValid, uint a_FromID)
        {
            bool bRet = true;

            do
            {
                if (!base.init(a_szDPPath, a_szGroupSimbol, a_szTempLcPath, a_szLcPath, a_szTimeToValid))
                {
                    bRet = false;
                    break;
                }

                m_iFromId = a_FromID;
                m_FormVersion = 0;
                m_FormCRC = 0;


            } while (false);

            return bRet;
        }

        protected override bool DefineValiedFileName()
        {
            m_FormCRC = 13433;
            m_FormVersion = 50;

            m_eFileWorkMode = FileWorkMode.eCreateNewLcFile;
            return false;
        }


        protected bool FindFileInLC()
        {
            bool bRet = true;

            string m_szSerahString = null;

            do
            {
               
            
            
            } while (false);

            return bRet;
        }
        
        protected override bool DownLoaFromDP()
        {
            bool bRet = true;
            AfUtlFFP_imp l_AfUtlFFP_imp = new AfUtlFFP_imp();
            IntPtr l_pBuffer = IntPtr.Zero;
            ulong l_pBufferSize = 0;
            string l_szErrorMsg = null;

            do
            {
                m_LastErrorCode = l_AfUtlFFP_imp.GetFFR(m_szFullFFPFileName, m_iFromId, ref l_pBuffer, ref l_pBufferSize, ref l_szErrorMsg);

                if (m_LastErrorCode != ErrorCode.FFP_NO_ERROR)
                {
                    bRet = false;
                    break;
                }

                m_pBuffer = new byte[l_pBufferSize];
                System.Runtime.InteropServices.Marshal.Copy(l_pBuffer, m_pBuffer, 0, m_pBuffer.Length);
                l_AfUtlFFP_imp.FreeMemory(l_pBuffer);

            } while (false);

            return bRet;
        }

        protected override string BuildLcFileName(string a_szPath)
        {
            return string.Format("{0}\\{1:D4}_{2:04}_{3:D4}_{4}.ffe", a_szPath, m_iFFPSerialNumber, m_iFromId, m_FormVersion, m_FormCRC);
        }

        public int ActivateLcEngine(string a_szDPPath, string a_szGroupSimbol, string a_szLcPath, string a_szTempLcPath, uint a_FromID, string m_aTimeToVailed, ref IntPtr a_psTempLcFilePath)
        {
            bool bRet = true;

            do
            {
                if (!Init(a_szDPPath, a_szGroupSimbol, a_szLcPath, a_szTempLcPath, m_aTimeToVailed, a_FromID))
                {
                    bRet = false;
                    break;
                }

                if (!RunLcEngine())
                {
                    bRet = false;
                    break;
                }

                a_psTempLcFilePath = GetTempLCFilePath();

                if (a_psTempLcFilePath == IntPtr.Zero)
                {
                    m_LastErrorCode = ErrorCode.ERR_INT_GENERATE_LV_FILE_NAME;
                    bRet = false;
                    break;
                }


            } while (false);

            return m_LastErrorCode;
        }


    }
}
