using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using AutoFont.AfUtlFFP;
using AutoFont.LC.FFP_LC_FILE;

namespace AutoFont.LC.FFP.Engine
{
    class FFPLcEngine
    {
        /// <summary>
        /// the Full Path to LC Folder
        /// </summary>
        protected string m_szLcFullFolderPath = null;

        /// <summary>
        /// The request Group Simbol
        /// </summary>
        protected string m_szGroupSimbol = null; 
        
        /// <summary>
        /// The request Forms package SN   
        /// </summary>
        protected long m_lSN = -1;

        /// <summary>
        /// the full  path to Distribution Point
        /// </summary>
        protected string m_szDistributionPoint=null;

        /// <summary>
        /// The Full path to Forms Package
        /// </summary>
        protected string m_szFormsPackageFullPath=null;

        /// <summary>
        /// if to save and load the buffes from the LC files system
        /// </summary>
        protected bool m_bUseLcEngine = false;

       /// <summary>
       /// Manger the FFP file
       /// </summary>
        protected FFPLcFile m_FFPLcFile=null;

        /// <summary>
        /// save the last error code
        /// </summary>
        protected int m_LastErrorCode = ErrorCode.FFP_NO_ERROR;

        public bool Init(string a_szFormsPackageFullPath,string a_szLcFullFolderPath, string a_GroupSimbol, long a_SN, bool a_UseLcEngine)
        {
            bool bRet = true;

            do
            {
                if (a_szLcFullFolderPath == null || a_GroupSimbol == null || a_szFormsPackageFullPath==null)
                {
                    m_LastErrorCode = ErrorCode.ERR_LC_INVAILD_ARGUMENT;
                    bRet = false;
                    break;

                }

                if (a_szLcFullFolderPath.Length == 0 || a_GroupSimbol.Length < Definition.MAX_SYMBOL_SIZE || a_szLcFullFolderPath.Length==0)
                {
                    m_LastErrorCode = ErrorCode.ERR_LC_INVAILD_ARGUMENT;
                    bRet = false;
                    break;
                }

                m_bUseLcEngine = a_UseLcEngine;
                m_lSN = a_SN;
                m_szGroupSimbol = a_GroupSimbol;
                m_szLcFullFolderPath = a_szLcFullFolderPath;
            } while (false);

            return bRet;
        }


        public bool RunLcEngine(ref byte [] a_pBuffer)
        {
            bool bRet = true;
            bool l_bUseLcCache = false;
            do
            {
                if (m_lSN == -1)
                {
                  //Define Is Path Specific 
                    if (!DefineIsPathSpecific())
                    {
                        bRet = false;
                        break;
                    }
                }//end of if m_lSN == -1

                if (m_bUseLcEngine == true)
                {
                   //Verify Need To downLoad
                    if (!VerifyNeedToDownLoad(ref l_bUseLcCache))
                    {
                        bRet = false;
                        break;
                    }
                }

                //if need to download form DP Path
                if (l_bUseLcCache == false)
                {
                    //DownLoad From from DP
                    if (!DownLoadFromDP(ref a_pBuffer))
                    {
                        bRet = false;
                        break;
                    }
                }
                else
                {
                  //Load from LC 
                    if (!LoadBufferFromLc(ref a_pBuffer))
                    {
                        if (!DownLoadFromDP(ref a_pBuffer))
                        {
                            bRet = false;
                            break;
                        }
                    }
                }


            } while (false);

            return bRet;
        }

        protected bool DefineIsPathSpecific()
        {
            bool bRet = true;
            do
            {
               

            } while (false);
            return bRet;
        }

        protected virtual bool VerifyNeedToDownLoad(ref bool a_bUseLcCache)
        {
            bool bRet = true;
            do
            {
                a_bUseLcCache = false;

            } while (false);
            return bRet;
        }

        protected  bool DownLoadFromDP(ref byte[] a_pBuffer)
        {
            bool bRet = true;
            do
            {
              //Get the Buffer from DP
               if(!GetBuffer(ref a_pBuffer))
               {
                 bRet=false;
                 break;
               }

               if(m_bUseLcEngine==true)
               {
                if(!SaveBufferToLCFile(a_pBuffer))
                {
                  bRet=false;
                  break;
                }
               }
               
             

            } while (false);
            return bRet;
        }

        protected virtual bool LoadBufferFromLc(ref byte[] a_pBuffer)
        {
            bool bRet = true;
            do
            {
                if (!GetBuffer(ref a_pBuffer))
                {
                    bRet = false;
                    break;
                }

                if (m_bUseLcEngine == false)
                {
                    break;
                }

                //Save the buffer to file

            } while (false);
            return bRet;
        }

        protected virtual bool GetBuffer(ref byte[] a_pBuffer)
        {
            
            bool          bRet = true;

            FFPUlt l_FFPUlt = new FFPUlt();



            do
            {

                try
                {
                    l_FFPUlt.init(m_szFormsPackageFullPath);
                    l_FFPUlt.ReadFFPToBuffer(ref a_pBuffer);
                }
                catch (FFPUtlException ex)
                {
                    m_LastErrorCode = ex.GetErrorCode;
                    bRet = false;
                    break;
                }
                catch (Exception)
                {
                    m_LastErrorCode = ErrorCode.ERR_GET_FFR_FATAL_ERROR;
                    bRet = false;
                    break;
                }
                





            } while (false);

            return bRet;
           
        }

        protected virtual bool SaveBufferToLCFile(byte[] a_pBuffer)
        {
           bool bRet = true;
            List<string> a_Argumant;

            do
            {
             
             //Build the List for the FFP file object 
             a_Argumant=new List<string>(3);
             
             a_Argumant.Add(m_szLcFullFolderPath);
             a_Argumant.Add(m_szGroupSimbol);
             a_Argumant.Add(string.Format("{#####0}",m_lSN));
             
             m_FFPLcFile=new FFPLcFile();

              if(!m_FFPLcFile.init(a_Argumant))
              {
                m_LastErrorCode=m_FFPLcFile.GetLastError();
                bRet=false;
                break;
              }

               m_FFPLcFile.WriteLcFile(a_pBuffer);
            
            }while(false);

            return bRet;
        }

                

    }
}
