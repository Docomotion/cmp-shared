using System;
using System.Collections.Generic;
using System.Text;

using AutoFont.LC.FFP.Engine;
using AutoFont.AfUtlFFP;

namespace AutoFont.LC.FORM_LC_FILE
{
    class FormLcEngine:FFPLcEngine
    {

        protected string m_szFormID = null;
        protected string m_FormVersion = null;
        protected string m_FormCRC = null;


        public bool init(string a_szFormsPackageFullPath, string a_szLcFullFolderPath, string a_GroupSimbol, long a_SN, long a_FormId, long a_FromVresion, long a_FormCRC, bool a_UseLcEngine)
        {

            bool bRet = false;

            do
            {
                if (!base.Init(a_szFormsPackageFullPath, a_szLcFullFolderPath, a_GroupSimbol, a_FormCRC, a_UseLcEngine))
                {
                    bRet = false;
                    break;
                }

                m_szFormID = string.Format("{####0}", a_FormId);
                m_FormVersion = string.Format("{####0}", a_FromVresion);
                m_FormCRC = string.Format("{0}", a_FormCRC);

            } while (false);

            return bRet;
        }

        protected override bool GetBuffer(ref byte[] a_pBuffer)
        {
            AfUtlFFP_imp l_AfUtlFFP_imp = null;
            bool bRet = true;
            IntPtr l_Buffer=IntPtr.Zero;
            ulong l_BufferSize=0;
            string l_szErrorString=null;



            do
            {
                l_AfUtlFFP_imp = new AfUtlFFP_imp();
               m_LastErrorCode=l_AfUtlFFP_imp.GetFFR(m_szFormsPackageFullPath, uint.Parse(m_szFormID), ref l_Buffer, ref l_BufferSize, ref l_szErrorString);

               if (m_LastErrorCode != ErrorCode.FFP_NO_ERROR)
               {
                   bRet = false;
                   break;
               }

               a_pBuffer = new byte[l_BufferSize];
               System.Runtime.InteropServices.Marshal.Copy(l_Buffer, a_pBuffer, 0, a_pBuffer.Length);
            
            } while (false);


            if (l_Buffer != IntPtr.Zero)
            {
                l_AfUtlFFP_imp.FreeMemory(l_Buffer);
            }

            return bRet;
        }

        protected override bool SaveBufferToLCFile(byte[] a_pBuffer)
        {
            bool bRet = true;
            do
            {
            } while (false);

            return bRet;
        }

        protected override bool LoadBufferFromLc(ref byte[] a_pBuffer)
        {
            bool bRet = true;
            do
            {
            } while (false);

            return bRet;
        }

        protected override bool VerifyNeedToDownLoad(ref bool a_bUseLcCache)
        {
            bool bRet = true;
            do
            {
            } while (false);

            return bRet;
        }
    }
}
