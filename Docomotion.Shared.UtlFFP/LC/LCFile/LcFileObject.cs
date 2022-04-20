using System;
using System.IO;
using Docomotion.Shared.ComDef;

namespace Docomotion.Shared.UtlFFP.LC.LCFile
{
    public class LcFileObject
    {
        

        protected string m_szLcTragetFullPath = null;

        protected string m_szOldLcFullPath = null;

        protected FileWorkMode m_eOperationOnLc = FileWorkMode.eNoAction;

        protected int m_iErrorCode = Lapsus.FF_NO_ERRORS;

        protected byte[] m_pLCBufeer = null;


        protected bool SetBufferToLC()
        {
            
            bool bRet = true;

            string l_szFolderPath = null;
            
            int l_index = 0;
            FileStream l_oFileStream = null;

            do
            {
               //split the file name and the folder 
                l_index = m_szLcTragetFullPath.LastIndexOf('\\');
                l_szFolderPath = m_szLcTragetFullPath.Substring(0,l_index);

              //vrified if the folder found 
                if (!Directory.Exists(l_szFolderPath))
                {
                     //build the projet simbol path
                    try
                    {
                        Directory.CreateDirectory(l_szFolderPath);
                    }
                    catch (Exception)
                    {
                        m_iErrorCode = Lapsus.Packager.WARN_CAN_NOT_CREATE_LC_PROJECT_DIRECTORY; // ISRAEL - try to set different errors 
                        bRet = false;
                        break;
                    }
                }//end of create folder 
           
             //copy the file from temp to lc folder 
                try
                {
                    using (l_oFileStream = new FileStream(m_szLcTragetFullPath, FileMode.Create, FileAccess.Write))
                    {
                        l_oFileStream.Write(m_pLCBufeer, 0, m_pLCBufeer.Length);
                        l_oFileStream.Close();
                    }
                }
                catch (Exception)
                {
                    m_iErrorCode = Lapsus.Packager.WARN_CAN_NOT_CREATE_LC_FILE;
                    bRet = false;
                    break;
                }
            
            
            } while (false);

            return bRet;
        }

        protected bool ReplaceLCFile()
        {
            
            bool bRet = true;

            do
            {
                try
                {
                    File.Delete(m_szOldLcFullPath);
                
                }
                catch (Exception)
                {
                    m_iErrorCode = Lapsus.Packager.WARN_CAN_NOT_DELETE_LC_FILE;
                    bRet = false;
                    break;
                }

                if (!SetBufferToLC())
                {
                    bRet = false;
                    break;
                }
            
            
            } while (false);

            return bRet;
        }

        protected bool ReNameLcFile()
        {
            bool bRet = true;

            do
            {
                try
                {
                    File.Move(m_szOldLcFullPath, m_szLcTragetFullPath);
                }
                catch (Exception)
                {
                    m_iErrorCode = Lapsus.Packager.WARN_CAN_NOT_RENAME_LC_FILE;
                    bRet = false;
                    break;
                }
            
            
            } while (false);

            return bRet;
        }
        
        
        public void SetLcBuffer(byte[] a_pBuffer)
        {
            m_pLCBufeer = a_pBuffer;
           
        }

        public void SetLcBuffer(string a_szXmlBuffer)
        {

            m_pLCBufeer = System.Text.Encoding.UTF8.GetBytes(a_szXmlBuffer);

            
        }

        public bool InvokeLcperation()
        {
           

            bool bRet = true;
            
            switch (m_eOperationOnLc)
            {
                case FileWorkMode.eCreateNewLcFile:
                    {
                        //copy the file from m_szTempLcFullPath to m_szLcTragetFullPath
                        bRet = SetBufferToLC();
                        break;
                    }
                case FileWorkMode.eReNameLcFile:
                    {
                        //rename the file m_szOldLcFullPath to m_szLcTragetFullPath
                        bRet = ReNameLcFile();
                        break;
                    }
                case FileWorkMode.eReplaceLcFile:
                    {
                        //delete the m_szOldLcFullPath and copy the m_szTempLcFullPath to m_szLcTragetFullPath
                        bRet = ReplaceLCFile();
                        break;
                    }
                case FileWorkMode.eNoAction:
                    break;
               
            }//end of switch

            

            return bRet;
        }


        

        public string LcTragetFullPath
        {
            set
            {
                m_szLcTragetFullPath = value;
            }
        }

        public string OldLcFullPath
        {
            set
            {
                m_szOldLcFullPath = value;
            }
        }

        public FileWorkMode OperationOnLc
        {
            set
            {
                m_eOperationOnLc = value;
            }
            get
            {
                return m_eOperationOnLc;
            }
        }

        public int LastErrorCode
        {
            get
            {
                return m_iErrorCode;
            }
        }

        public bool ReadLcFileToBuffer()
        {
            FileStream l_oFileStream = null;
            bool bRet =true;;

            do
            {
                try
                {
                    using (l_oFileStream = new FileStream(m_szLcTragetFullPath, FileMode.Open, FileAccess.Read))
                    {
                        m_pLCBufeer = new byte[l_oFileStream.Length];
                        l_oFileStream.Read(m_pLCBufeer, 0, m_pLCBufeer.Length);
                        l_oFileStream.Close();
                    }
                
                
                }
                catch (Exception)
                {
                    m_iErrorCode = Lapsus.Packager.WARN_CAN_NOT_READ_READ_LC_FILE;
                    bRet = false;
                    break;
                }
            
            
            } while (false);

            return bRet;
        }

        public byte[] LCBuffer
        {
            get
            {
                return m_pLCBufeer;
            }
        }


    }
}
