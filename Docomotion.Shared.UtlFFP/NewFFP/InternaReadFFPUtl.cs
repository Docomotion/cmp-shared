using System;
using System.IO;
using Docomotion.Shared.ComDef;
using Docomotion.Shared.ErrorHandler;

namespace Docomotion.Shared.UtlFFP.NewFFP
{
    internal class InternaReadFFPUtl:BaseFFPUtl
    {
        //protected Stream m_oFFPFileStream = null;
        protected BinaryReader m_oFFPBinaryReader = null;
        protected FFPFormat m_oFFPFormat = null;
        FFPSection1 m_oFFPSection = null;



        protected void UZ7Buffer(byte[] a_pZipBuffer, ref byte[] a_pUnZipBuffer)
        {
            try
            {
                AutoFont.SevenZ.Af7Z l_oAf7Z = new AutoFont.SevenZ.Af7Z();

                l_oAf7Z.UnCompression7Z(a_pZipBuffer, ref a_pUnZipBuffer);
            }
            catch (Exception ex)
            {
                ExceptionFFEngine.ThrowException(Lapsus.RT.ERR_UNZIP_FFR_BUFFER, m_iSystemErrorCode, Lapsus.ErrorsMsg.STR_ERR_UNZIP_FFR_BUFFER, ex);
            }
        }


        
        
        
        protected void UZipBuffer(byte[] a_pZipBuffer, uint a_uiRealZipBuffer, ref byte[] a_pUnZipBuffer)
        {
            try
            {
                MemoryStream ZipMemStream = new MemoryStream(a_pZipBuffer);

                ICSharpCode.SharpZipLib.Zip.ZipInputStream FFRZipStream = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(ZipMemStream);


                FFRZipStream.GetNextEntry();
                a_pUnZipBuffer = new byte[a_uiRealZipBuffer];


                FFRZipStream.Read(a_pUnZipBuffer, 0, (int)a_uiRealZipBuffer);


                FFRZipStream.Close();
                ZipMemStream.Close();
            }
            catch (Exception ex)
            {
                ExceptionFFEngine.ThrowException(Lapsus.RT.ERR_UNZIP_FFR_BUFFER, m_iSystemErrorCode, Lapsus.ErrorsMsg.STR_ERR_UNZIP_FFR_BUFFER, ex);
            }
        }

        protected void UnCompressionBuffer(byte[] a_pZipBuffer, uint a_uiRealZipBuffer, ref byte[] a_pUnZipBuffer)
        {
            //uzip the tbe buffer to buffer
            switch (m_oFFPFormat.CompressionType)
            {
                case Definition.ZIP_COMPRESS_TYPE: //Zip
                    UZipBuffer(a_pZipBuffer, a_uiRealZipBuffer, ref a_pUnZipBuffer);
                    break;

                case Definition.Z7_COMPRESS_TYPE:
                    UZ7Buffer(a_pZipBuffer, ref a_pUnZipBuffer);
                    break;
                
                default:
                    ExceptionFFEngine.ThrowException(Lapsus.RT.ERR_UNSUPPORT_COMPRESSION_TYPE, m_iSystemErrorCode, Lapsus.ErrorsMsg.STR_ERR_UNSUPPORT_COMPRESSION_TYPE, null);
                    break;

            }
        }

        protected void ValidFileStructure(string a_szFFPFullPath)
        {
            char[] l_szModuleType = m_oFFPFormat.ModuleType;
            uint l_uiFileSize = m_oFFPFormat.FileSize;

            if (!(l_szModuleType[0] == 'F' &&
               l_szModuleType[1] == 'F' &&
               l_szModuleType[2] == 'P' &&
               l_szModuleType[3] == 'S'))
            {
                ExceptionFFEngine.ThrowException(Lapsus.RT.ERR_FFP_INTERNAL_SIGNATURE_CORRUPTED, 0, Lapsus.ErrorsMsg.STR_ERR_FFP_INTERNAL_SIGNATURE_CORRUPTED, null);
               // GenerateException(, m_iErrorCode, , null, string.Format(Lapsus.ErrorsMsg.STR_ERR_FFP_INTERNAL_SIGNATURE_CORRUPTED, a_szFFPFullPath, l_szModuleType));
            }


           FileInfo l_oFileInfo=new FileInfo(a_szFFPFullPath);

           if ((long)l_uiFileSize != l_oFileInfo.Length)
           {
               ExceptionFFEngine.ThrowException(Lapsus.RT.ERR_FFP_INTERNAL_SIGNATURE_CORRUPTED, m_iErrorCode, Lapsus.ErrorsMsg.STR_ERR_FFP_INTERNAL_SIGNATURE_CORRUPTED, null);
           }
            
        }
        
        public void Init(string a_szFFPFullPath)
        {


            //open the FFP file for stream
            OpenFileStream(ref m_FFPFileStream, FileMode.Open, FileAccess.Read, a_szFFPFullPath, Lapsus.RT.ERR_IN_OPEN_FFP_FILE);

            m_oFFPBinaryReader = new BinaryReader(m_FFPFileStream);

            //read the FFP binary header
            m_oFFPSection = new FFPSection1();
            m_oFFPSection.ReadFromBuffer(m_oFFPBinaryReader);
            m_oFFPFormat = m_oFFPSection.FFP_HREADER;

            ValidFileStructure(a_szFFPFullPath);

            FileInfo l_oFileInfo=new FileInfo(a_szFFPFullPath);
            string l_szFileProjectSymbol = null;

            l_szFileProjectSymbol = l_oFileInfo.Name;

            l_szFileProjectSymbol = l_szFileProjectSymbol.Substring(2, 3);
            string l_szInternalProjectSymbol = new string(m_oFFPFormat.ProjectSymbol);
            l_szInternalProjectSymbol=l_szInternalProjectSymbol.Remove(3);

            if (l_szInternalProjectSymbol.ToLower() != l_szFileProjectSymbol.ToLower())
            {


                ExceptionFFEngine.ThrowException(Lapsus.RT.ERR_INTERNAL_PROJECT_SYMBOL_NOT_MACH_TO_FILE_PROJECT_SYMBOL, 0, Lapsus.ErrorsMsg.STR_ERR_INTERNAL_PROJECT_SYMBOL_NOT_MACH_TO_FILE_PROJECT_SYMBOL, null);
            }




        }
        
        public void ReadFFPHeader(ref string pFFRPHeaderBuffer)
        {
            uint l_uiPackageDescriptionOffset=0;
            uint l_uiPackageDescriptionSize=0;
            uint l_uiPackageDescriptionRealsize=0;

            byte[] l_pZipBuffer = null;
            byte[] l_uzipBuffer = null;

            //get the offset of the FFP Header and the size and real size 
            l_uiPackageDescriptionOffset = m_oFFPFormat.PackageDescriptionSegment;
            l_uiPackageDescriptionSize = m_oFFPFormat.FormFieldsDescriptionsSegments - m_oFFPFormat.PackageDescriptionSegment;
            l_uiPackageDescriptionRealsize = m_oFFPFormat.FileHeaderSize;

            //Seek to the to the start 
            m_FFPFileStream.Seek(l_uiPackageDescriptionOffset, SeekOrigin.Begin);

            //read the file to buffer 
            l_pZipBuffer = new byte[l_uiPackageDescriptionSize];
            m_FFPFileStream.Read(l_pZipBuffer, 0, l_pZipBuffer.Length);

            //uzip the tbe buffer to buffer
            UnCompressionBuffer(l_pZipBuffer, l_uiPackageDescriptionRealsize, ref l_uzipBuffer);

            //convet the buffer to utf-8 string 
            pFFRPHeaderBuffer = System.Text.Encoding.UTF8.GetString(l_uzipBuffer);
        }

        public void GetFFRData(uint FromID, ref byte[] pFFRFromDataBuffer)
        {           
           //get the data of the ffr in the ffp 
            
            uint l_uiFormOffset=0;
            uint l_uiFormSize=0;
            uint l_uiFormRealSize=0;

            uint l_uiFFRSeek = 0;

            byte[] l_pZipFFRBuffer = null;


            
            if (!m_oFFPSection.GetFormDataPropertiesByFormId(FromID, m_oFFPBinaryReader, ref l_uiFormOffset, ref l_uiFormSize, ref l_uiFormRealSize))
            {
                ExceptionFFEngine.ThrowException(Lapsus.RT.ERR_CAN_NOT_FIND_FFR_IN_FFP, m_iErrorCode, Lapsus.ErrorsMsg.STR_ERR_CAN_NOT_FOUND_FFR_IN_FFP, null);
            }
            
           

            //seek the buffer 
            l_uiFFRSeek = m_oFFPFormat.FormsDataSegments + l_uiFormOffset;
            m_FFPFileStream.Seek(l_uiFFRSeek, SeekOrigin.Begin);

            //read the ffr 
            l_pZipFFRBuffer = new byte[l_uiFormSize];
            m_FFPFileStream.Read(l_pZipFFRBuffer, 0, l_pZipFFRBuffer.Length);
                       
           
          
            //uzip the buffer 
            
            UnCompressionBuffer(l_pZipFFRBuffer, l_uiFormRealSize, ref pFFRFromDataBuffer);
            
            

        }

        public void GetFFRTags(uint FromID, ref string pszDataTagsBuffer)
        {
            uint l_uiFormOffset = 0;
            uint l_uiFormSize = 0;
            uint l_uiFormRealSize = 0;

            uint l_uiFFRSeek = 0;

            byte[] l_pZipFFRTagBuffer = null;
            byte[] l_punZipFFRTagBuffer = null;

            if (!m_oFFPSection.GetFormTagsPropertiesByFormId(FromID, m_oFFPBinaryReader, ref l_uiFormOffset, ref l_uiFormSize, ref l_uiFormRealSize))
            {
                ExceptionFFEngine.ThrowException(Lapsus.RT.ERR_CAN_NOT_FIND_FFR_IN_FFP, m_iErrorCode, Lapsus.ErrorsMsg.STR_ERR_CAN_NOT_FOUND_FFR_IN_FFP, null);
            }

            if (l_uiFormSize == 0 && l_uiFormRealSize == 0)
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_FORM_DATA_TAG_NOT_FOUND_IN_FFP, m_iSystemErrorCode, Lapsus.ErrorsMsg.STR_ERR_FROM_DATA_TAG_NOT_FOUND_IN_FFP, null);
            }

            //seek the buffer 
            l_uiFFRSeek = m_oFFPFormat.FormFieldsDescriptionsSegments + l_uiFormOffset;
            m_FFPFileStream.Seek(l_uiFFRSeek, SeekOrigin.Begin);

            //read the ffr tag buffer 
            l_pZipFFRTagBuffer = new byte[l_uiFormSize];
            m_FFPFileStream.Read(l_pZipFFRTagBuffer, 0, l_pZipFFRTagBuffer.Length);

            //uzip the buffer 
            UnCompressionBuffer(l_pZipFFRTagBuffer, l_uiFormRealSize, ref l_punZipFFRTagBuffer);

            //convet it to string 
            pszDataTagsBuffer = System.Text.Encoding.UTF8.GetString(l_punZipFFRTagBuffer);
        }

        public void GetFFPSN(ref uint puiFFPSB)
        {
          puiFFPSB=(uint)m_oFFPSection.FFP_HREADER.SerialNumber;
        }

        public void GetFormInfo(uint a_FromID, ref uint a_FormVresion, ref ushort a_FormSignature, ref ushort a_FormExpirationDate)
        {


            if (!m_oFFPSection.GetFormInfo(a_FromID, ref a_FormVresion, ref a_FormSignature, ref a_FormExpirationDate))
            {


                string l_szAdditionalInfo = string.Format(Lapsus.ErrorsMsg.STR_ERR_CAN_NOT_FOUND_FFR_IN_FFP, a_FromID, new string(m_oFFPFormat.ProjectSymbol), m_oFFPFormat.SerialNumber);
                ExceptionFFEngine.ThrowException(Lapsus.RT.ERR_CAN_NOT_FIND_FFR_IN_FFP, m_iErrorCode, l_szAdditionalInfo, null);
            }

            
        }

        public void IsFormExpired(ushort a_FormExpiredDatas)
        {

            int l_month = int.Parse(Definition.START_SYSTEM_TIME.Substring(0, 2));
            int l_Day =int.Parse(Definition.START_SYSTEM_TIME.Substring(3, 2));
            int l_Year = int.Parse(Definition.START_SYSTEM_TIME.Substring(6, 4));

            DateTime l_SystemStartTime = new DateTime(l_Year, l_month, l_Day);
            DateTime l_CurrentTime = DateTime.Now;
            TimeSpan l_TimeSpan ;


            l_TimeSpan = (TimeSpan)(l_CurrentTime - l_SystemStartTime);

            if ((ushort)(l_TimeSpan.Days) > a_FormExpiredDatas)
            {
                l_SystemStartTime = l_SystemStartTime.AddDays(a_FormExpiredDatas);
                if (m_oFFPFormat == null)
                {
                    l_SystemStartTime.AddDays(a_FormExpiredDatas);

                    ExceptionFFEngine.ThrowException(Lapsus.RT.ERR_FORM_IS_EXPIRED, m_iSystemErrorCode, string.Format(Lapsus.ErrorsMsg.STR_ERR_FORM_IS_EXPIRED, l_SystemStartTime.ToShortDateString(), l_CurrentTime.ToShortDateString()), null);
                }
                else
                {
                    string l_szProjectSymbol =new string(m_oFFPFormat.ProjectSymbol);

                    ExceptionFFEngine.ThrowException(Lapsus.RT.ERR_FORM_IS_EXPIRED, m_iSystemErrorCode, string.Format(Lapsus.ErrorsMsg.STR_ERR_FORM_IS_EXPIRED_2, l_SystemStartTime.ToShortDateString(), l_CurrentTime.ToShortDateString(), l_szProjectSymbol, m_oFFPFormat.SerialNumber), null);
                }
            }
        }


        public virtual void  Dispose()
        {
            try
            {
                m_oFFPBinaryReader.Close();
                m_oFFPBinaryReader = null;
            }
            catch(Exception)
            {

            }
        }
       
    }
}
