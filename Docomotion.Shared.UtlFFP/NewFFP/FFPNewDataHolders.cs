using System;
using System.IO;

namespace Docomotion.Shared.UtlFFP.NewFFP
{
    internal class FFPFormat
    {
        protected char[] m_ModuleType = new char[4];
        protected uint m_FileHeaderSize = 0; // package description real size
        protected uint m_FileSize = 0;
        protected byte[] m_FileFormat = new byte[4];
        protected char[] m_ProjectSymbol = new char[4];
        protected UInt16 m_SerialNumber = 0;
        protected UInt16 m_FormsCount = 0;
        protected uint m_FormsHeaderSegmentDirectory = 0;
        protected uint m_FormsHeaderSegment = 0;
        protected uint m_FormFieldsHeaderSegments = 0;
        protected uint m_PackageDescriptionSegment = 0;
        protected uint m_FormFieldsDescriptionsSegments = 0;
        protected uint m_FormsDataSegments = 0;
        protected byte m_CompressionType = Definition.Z7_COMPRESS_TYPE; //Z7 Compression defult

        #region set and get
        static public uint Sizeof()
        {

            uint l_isizeof = (uint)(sizeof(byte) * 4 * 3);
            l_isizeof = l_isizeof + (uint)(sizeof(uint) * 8);
            l_isizeof += (sizeof(UInt16) * 2);

            l_isizeof += 1; //add the Compression Type size
            return l_isizeof;
        }
        public char[] ModuleType
        {
            get
            {
                return m_ModuleType;
            }
            set
            {
                m_ModuleType = value;
            }
        }
        public uint FileHeaderSize
        {
            get
            {
                return m_FileHeaderSize;
            }
            set
            {
                m_FileHeaderSize = value;
            }
        }
        public uint FileSize
        {
            get
            {
                return m_FileSize;
            }
            set
            {
                m_FileSize = value;
            }
        }
        public byte[] FileFormat
        {
            get
            {
                return m_FileFormat;
            }
            set
            {
                m_FileFormat = value;
            }
        }
        public char[] ProjectSymbol
        {
            get
            {
                return m_ProjectSymbol;
            }
            set
            {
                m_ProjectSymbol[0] = value[0];
                m_ProjectSymbol[1] = value[1];
                m_ProjectSymbol[2] = value[2];
                m_ProjectSymbol[3] = '~';
            }
        }
        public UInt16 SerialNumber
        {
            get
            {
                return m_SerialNumber;
            }
            set
            {
                m_SerialNumber = value;
            }
        }
        public UInt16 FormsCount
        {
            get
            {
                return m_FormsCount;
            }
            set
            {
                m_FormsCount = value;
            }
        }
        public uint FormsHeaderSegmentDirectory
        {
            get
            {
                return m_FormsHeaderSegmentDirectory;
            }
            set
            {
                m_FormsHeaderSegmentDirectory = value;
            }
        }
        public uint FormsHeaderSegment
        {
            get
            {
                return m_FormsHeaderSegment;
            }
            set
            {
                m_FormsHeaderSegment = value;
            }
        }
        public uint FormFieldsHeaderSegments
        {
            get
            {
                return m_FormFieldsHeaderSegments;
            }
            set
            {
                m_FormFieldsHeaderSegments = value;
            }
        }
        public uint PackageDescriptionSegment
        {
            get
            {
                return m_PackageDescriptionSegment;
            }
            set
            {
                m_PackageDescriptionSegment = value;
            }
        }
        public uint FormFieldsDescriptionsSegments
        {
            get
            {
                return m_FormFieldsDescriptionsSegments;
            }
            set
            {
                m_FormFieldsDescriptionsSegments = value;
            }
        }
        public uint FormsDataSegments
        {
            get
            {
                return m_FormsDataSegments;
            }
            set
            {
                m_FormsDataSegments = value;
            }
        }

        public byte CompressionType
        {
            get
            {
                return m_CompressionType;
            }
            set
            {
                m_CompressionType = value;
            }
        }


        #endregion set and get

        #region Read and write
        public void ReadFromBuffer(BinaryReader a_BinReader)
        {
            m_ModuleType = a_BinReader.ReadChars(4);
            m_FileHeaderSize = a_BinReader.ReadUInt32();
            m_FileSize = a_BinReader.ReadUInt32();
            m_FileFormat = a_BinReader.ReadBytes(4);
            m_ProjectSymbol = a_BinReader.ReadChars(4);

            m_SerialNumber = a_BinReader.ReadUInt16();
            m_FormsCount = a_BinReader.ReadUInt16();
            m_FormsHeaderSegmentDirectory = a_BinReader.ReadUInt32();
            m_FormsHeaderSegment = a_BinReader.ReadUInt32();
            m_FormFieldsHeaderSegments = a_BinReader.ReadUInt32();
            m_PackageDescriptionSegment = a_BinReader.ReadUInt32();
            m_FormFieldsDescriptionsSegments = a_BinReader.ReadUInt32();
            m_FormsDataSegments = a_BinReader.ReadUInt32();
            //read Compression Type
            m_CompressionType = a_BinReader.ReadByte();
        }

        public void WriteToBuffer(BinaryWriter a_BinWriter)
        {
            a_BinWriter.Write(m_ModuleType);
            a_BinWriter.Write(m_FileHeaderSize);
            a_BinWriter.Write(m_FileSize);
            a_BinWriter.Write(m_FileFormat);
            a_BinWriter.Write(m_ProjectSymbol);
            a_BinWriter.Write(m_SerialNumber);
            a_BinWriter.Write(m_FormsCount);
            a_BinWriter.Write(m_FormsHeaderSegmentDirectory);
            a_BinWriter.Write(m_FormsHeaderSegment);
            a_BinWriter.Write(m_FormFieldsHeaderSegments);
            a_BinWriter.Write(m_PackageDescriptionSegment);
            a_BinWriter.Write(m_FormFieldsDescriptionsSegments);
            a_BinWriter.Write(m_FormsDataSegments);
            //write Compression Type
            a_BinWriter.Write(m_CompressionType);
        }

        #endregion read and write

    }

    class FFPFormsHeaderSegmentDirectory
    {

        // members 
        protected uint m_FormId = 0;
        protected uint m_FormVersion = 0;
        protected ushort m_FormSigniture = 0;
        protected ushort m_ExpirationDate = ushort.MaxValue;

        static public uint Sizeof()
        {
            uint l_iSize = (uint)(sizeof(uint) * 2);
            l_iSize += (uint)(sizeof(ushort) * 2);
            return l_iSize;
        }

        public uint FormId
        {
            get
            {
                return m_FormId;
            }
            set
            {
                m_FormId = value;
            }
        }
        public uint FormVersion
        {
            get
            {
                return m_FormVersion;
            }
            set
            {
                m_FormVersion = value;
            }
        }
        public ushort FormSigniture
        {
            get
            {
                return m_FormSigniture;
            }
            set
            {
                m_FormSigniture = value;
            }
        }
        public ushort ExpirationDate
        {
            get
            {
                return m_ExpirationDate;
            }
            set
            {
                m_ExpirationDate = value;
            }
        }


        // read write
        public void ReadFromBuffer(BinaryReader a_BinReader)
        {
            m_FormId = a_BinReader.ReadUInt32();
            m_FormVersion = a_BinReader.ReadUInt32();
            m_FormSigniture = a_BinReader.ReadUInt16();
            m_ExpirationDate = a_BinReader.ReadUInt16();
        }
        public void WriteToBuffer(BinaryWriter a_BinWriter)
        {
            a_BinWriter.Write(m_FormId);
            a_BinWriter.Write(m_FormVersion);
            a_BinWriter.Write(m_FormSigniture);
            a_BinWriter.Write(m_ExpirationDate);
        }
    }

    class FormsHeaderSegments
    {

        protected uint m_FormdataOffset = 0;
        protected uint m_FormdataLength = 0;
        protected uint m_FormDataRealSize = 0;

        static public uint Sizeof()
        {
            uint l_iSize = (uint)(sizeof(uint) * 3);
            return l_iSize;
        }

        public uint FormdataOffset
        {
            get
            {
                return m_FormdataOffset;
            }
            set
            {
                m_FormdataOffset = value;
            }
        }
        public uint FormdataLength
        {
            get
            {
                return m_FormdataLength;
            }
            set
            {
                m_FormdataLength = value;
            }
        }
        public uint FormDataRealSize
        {
            get
            {
                return m_FormDataRealSize;
            }
            set
            {
                m_FormDataRealSize = value;
            }
        }

        public void ReadFromBuffer(BinaryReader a_BinReader)
        {
            m_FormdataOffset = a_BinReader.ReadUInt32();
            m_FormdataLength = a_BinReader.ReadUInt32();
            m_FormDataRealSize = a_BinReader.ReadUInt32();
        }
        public void WriteToBuffer(BinaryWriter a_BinWriter)
        {
            a_BinWriter.Write(m_FormdataOffset);
            a_BinWriter.Write(m_FormdataLength);
            a_BinWriter.Write(m_FormDataRealSize);
        }
    }

    class FormsHeaderSegmentFormat
    {

        protected uint m_FormFieldsOffset = 0;
        protected uint m_FormFieldsLength = 0;
        protected uint m_FormFieldsRealSize = 0;


        static public uint Sizeof()
        {
            uint l_iSize = (uint)(sizeof(uint) * 3);
            return l_iSize;
        }


        public uint FormFieldsOffset
        {
            get
            {
                return m_FormFieldsOffset;
            }
            set
            {
                m_FormFieldsOffset = value;
            }
        }
        public uint FormFieldsLength
        {
            get
            {
                return m_FormFieldsLength;
            }
            set
            {
                m_FormFieldsLength = value;
            }
        }
        public uint FormFieldsRealSize
        {
            get
            {
                return m_FormFieldsRealSize;
            }
            set
            {
                m_FormFieldsRealSize = value;
            }
        }

        public void ReadFromBuffer(BinaryReader a_BinReader)
        {
            m_FormFieldsOffset = a_BinReader.ReadUInt32();
            m_FormFieldsLength = a_BinReader.ReadUInt32();
            m_FormFieldsRealSize = a_BinReader.ReadUInt32();
        }

        public void WriteToBuffer(BinaryWriter a_BinWriter)
        {
            a_BinWriter.Write(m_FormFieldsOffset);
            a_BinWriter.Write(m_FormFieldsLength);
            a_BinWriter.Write(m_FormFieldsRealSize);
        }
    }

    class FFPSection1
    {
        public FFPFormat m_FFPFormat = new FFPFormat();

        protected FFPFormsHeaderSegmentDirectory[] m_FFPFormsHeaderSegmentDirectory = null;
        protected FormsHeaderSegments[] m_FormsHeaderSegments = null;
        protected FormsHeaderSegmentFormat[] m_FormsHeaderSegmentFormat = null;
        protected int m_CurrentForm = -1;
        protected uint m_FormDataLastOffset = 0;
        protected uint m_FormTagsLastOffset = 0;



        // for wrtie purpose only
        public void init(uint a_iFormsCount)
        {
            m_FFPFormsHeaderSegmentDirectory = new FFPFormsHeaderSegmentDirectory[a_iFormsCount];
            m_FormsHeaderSegments = new FormsHeaderSegments[a_iFormsCount];
            m_FormsHeaderSegmentFormat = new FormsHeaderSegmentFormat[a_iFormsCount];
            // compute sizes 
            // resetting the offsets relative to the start
            m_FFPFormat.FormsCount = Convert.ToUInt16(a_iFormsCount);
            m_FFPFormat.FormsHeaderSegmentDirectory = FFPFormat.Sizeof();
            m_FFPFormat.FormsHeaderSegment = (FFPFormsHeaderSegmentDirectory.Sizeof() * a_iFormsCount) + m_FFPFormat.FormsHeaderSegmentDirectory;
            m_FFPFormat.FormFieldsHeaderSegments = m_FFPFormat.FormsHeaderSegment + (FormsHeaderSegments.Sizeof() * a_iFormsCount);

            m_FFPFormat.PackageDescriptionSegment = m_FFPFormat.FormFieldsHeaderSegments + (FormsHeaderSegmentFormat.Sizeof() * a_iFormsCount);



        }

        public void ReadFromBuffer(BinaryReader a_BinReader)
        {
            m_FFPFormat.ReadFromBuffer(a_BinReader);

            m_FFPFormsHeaderSegmentDirectory = new FFPFormsHeaderSegmentDirectory[m_FFPFormat.FormsCount];


            for (int l_iCounter = 0; l_iCounter < m_FFPFormat.FormsCount; l_iCounter++)
            {
                FFPFormsHeaderSegmentDirectory l_FormsHeaderSegmentDirectory = new FFPFormsHeaderSegmentDirectory();
                l_FormsHeaderSegmentDirectory.ReadFromBuffer(a_BinReader);
                m_FFPFormsHeaderSegmentDirectory[l_iCounter] = l_FormsHeaderSegmentDirectory;

            }

        }

        public void WriteToBuffer(BinaryWriter a_BinWriter)
        {
            m_FFPFormat.WriteToBuffer(a_BinWriter);

            foreach (FFPFormsHeaderSegmentDirectory Obj in m_FFPFormsHeaderSegmentDirectory)
            {
                Obj.WriteToBuffer(a_BinWriter);
            }

            foreach (FormsHeaderSegments Obj in m_FormsHeaderSegments)
            {
                Obj.WriteToBuffer(a_BinWriter);
            }

            foreach (FormsHeaderSegmentFormat Obj in m_FormsHeaderSegmentFormat)
            {
                Obj.WriteToBuffer(a_BinWriter);
            }
        }

        public bool GetFormInfo(uint a_iFormId, ref uint a_FormVesrion, ref ushort a_Formsignature, ref ushort a_FormExpirationDate)
        {
            try
            {
                for (int x = 0; x < m_FFPFormat.FormsCount; x++)
                {
                    if (m_FFPFormsHeaderSegmentDirectory[x].FormId == a_iFormId)
                    {
                        a_FormVesrion = m_FFPFormsHeaderSegmentDirectory[x].FormVersion;
                        a_Formsignature = m_FFPFormsHeaderSegmentDirectory[x].FormSigniture;
                        a_FormExpirationDate = m_FFPFormsHeaderSegmentDirectory[x].ExpirationDate;
                        return true;
                    }

                }

                return false;
            }
            catch (System.Exception)
            {
                return false;
            }


        }


        public bool GetFormDataPropertiesByFormId(uint a_iFormId, BinaryReader a_BinReader, ref uint a_DataOffset, ref uint a_DataLength, ref uint a_DataRealSize)
        {
            int l_iFormIndex = GetFormIDIndexList(a_iFormId);
            if (l_iFormIndex > -1)
            {
                // compute the length to start seeking 
                uint l_iSeek = m_FFPFormat.FormsHeaderSegment;
                l_iSeek += FormsHeaderSegments.Sizeof() * Convert.ToUInt32(l_iFormIndex);

                a_BinReader.BaseStream.Seek(l_iSeek, SeekOrigin.Begin);

                FormsHeaderSegments l_oSegment = new FormsHeaderSegments();
                l_oSegment.ReadFromBuffer(a_BinReader);

                a_DataOffset = l_oSegment.FormdataOffset;
                a_DataLength = l_oSegment.FormdataLength;
                a_DataRealSize = l_oSegment.FormDataRealSize;

                return true;
            }
            else
                return false;
        }

        public bool GetFormTagsPropertiesByFormId(uint a_iFormId, BinaryReader a_BinReader, ref uint a_TagsOffset, ref uint a_TagsLength, ref uint a_TagsRealSize)
        {
            int l_iFormIndex = GetFormIDIndexList(a_iFormId);
            FormsHeaderSegments l_oSegment = new FormsHeaderSegments();

            if (l_iFormIndex == -1)
            {
                return false;
            }
            // compute the length to start seeking 
            uint l_iSeek = m_FFPFormat.FormFieldsHeaderSegments;
            l_iSeek += FormsHeaderSegments.Sizeof() * Convert.ToUInt32(l_iFormIndex);

            a_BinReader.BaseStream.Seek(l_iSeek, SeekOrigin.Begin);

            l_oSegment.ReadFromBuffer(a_BinReader);

            a_TagsOffset = l_oSegment.FormdataOffset;
            a_TagsLength = l_oSegment.FormdataLength;
            a_TagsRealSize = l_oSegment.FormDataRealSize;

            return true;
        }

        protected int GetFormIDIndexList(uint a_iFormId)
        {
            for (int x = 0; x < m_FFPFormat.FormsCount; x++)
            {
                if (m_FFPFormsHeaderSegmentDirectory[x].FormId == a_iFormId)
                    return x;
            }
            return -1;
        }

        // Creats new forms header segment object and forms header segment format object and fill them up 
        public long UpdateDataSection(uint a_lFormID, uint a_lFormVersion, ushort a_iFormSigniture, uint a_lDataLength, uint a_lDataRealSize, uint a_lTagsLength, uint a_lTagsRealSize, ushort a_ExpirationDate)
        {
            m_CurrentForm++;

            m_FFPFormsHeaderSegmentDirectory[m_CurrentForm] = new FFPFormsHeaderSegmentDirectory();
            m_FFPFormsHeaderSegmentDirectory[m_CurrentForm].FormId = a_lFormID;
            m_FFPFormsHeaderSegmentDirectory[m_CurrentForm].FormSigniture = a_iFormSigniture;
            m_FFPFormsHeaderSegmentDirectory[m_CurrentForm].FormVersion = a_lFormVersion;
            m_FFPFormsHeaderSegmentDirectory[m_CurrentForm].ExpirationDate = a_ExpirationDate;

            m_FormsHeaderSegments[m_CurrentForm] = new FormsHeaderSegments();
            m_FormsHeaderSegments[m_CurrentForm].FormdataLength = a_lDataLength;
            m_FormsHeaderSegments[m_CurrentForm].FormdataOffset = m_FormDataLastOffset;
            m_FormDataLastOffset += a_lDataLength;
            m_FormsHeaderSegments[m_CurrentForm].FormDataRealSize = a_lDataRealSize;


            m_FormsHeaderSegmentFormat[m_CurrentForm] = new FormsHeaderSegmentFormat();
            m_FormsHeaderSegmentFormat[m_CurrentForm].FormFieldsLength = a_lTagsLength;
            m_FormsHeaderSegmentFormat[m_CurrentForm].FormFieldsRealSize = a_lTagsRealSize;
            m_FormsHeaderSegmentFormat[m_CurrentForm].FormFieldsOffset = m_FormTagsLastOffset;

            m_FormTagsLastOffset += a_lTagsLength;

            return 0;
        }


        public FFPFormat FFP_HREADER
        {
            get
            {
                return m_FFPFormat;
            }
        }


    }


}
