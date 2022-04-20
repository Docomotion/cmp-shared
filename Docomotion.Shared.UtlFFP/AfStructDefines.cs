using System;
using System.Runtime.InteropServices;
using System.Xml;
using Docomotion.Shared.ComDef;

namespace Docomotion.Shared.UtlFFP
{
    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 2)]
    [Serializable]
    public class CFFBaseHeader
    {
        public string szApplicationSymbol = null;
        public string szApplicationSymbolName = null;
        public string szDate = null;
        public string szName = null;
        public string szDescription = null;
        public string szFormatVersion = null;
        public int iVersion;
        public int iFileType;  // 0 for ffr 1 for dll
        public int iBackCompatibility;  // 1 for FFR1 

        public int FFRType = FreeFormDefinitions.COMPILED_FRR_TYPE.FFR2;

        public CFFBaseHeader()
        {
            
        }

    }


    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 2)]
    [Serializable]
    public class CFFPHeader : CFFBaseHeader
    {        
        //public uint iVersion;        
        public CFFPHeader()
        {
        }

        public long ConvertFromXml(XmlDocument a_Header)
        {

            long bRet = Lapsus.FF_NO_ERRORS;

            szName = GetValueFromXml("/FreeFormPackageHeader/PackageName",ref a_Header);
            szDate = GetValueFromXml("/FreeFormPackageHeader/PackageCreateDate", ref a_Header);
            szDescription = GetValueFromXml("/FreeFormPackageHeader/PackageDescription", ref a_Header);
            iVersion = Convert.ToInt32(GetValueFromXml("/FreeFormPackageHeader/SerialNumber", ref a_Header));
            szFormatVersion = GetValueFromXml("/FreeFormPackageHeader/FormatVersion", ref a_Header);
            iFileType = Convert.ToInt32(GetValueFromXml("/FreeFormPackageHeader/FileType", ref a_Header));
            szApplicationSymbol = GetValueFromXml("/FreeFormPackageHeader/ApplicationSymbol",ref a_Header);
            szApplicationSymbolName = GetValueFromXml("/FreeFormPackageHeader/ApplicationSymbolName", ref a_Header);

            string ffrTypeStr = GetValueFromXml("/FreeFormPackageHeader/FFRType", ref a_Header);
            int ffrTypeNum = -1;
            if (!string.IsNullOrEmpty(ffrTypeStr))
                if (int.TryParse(ffrTypeStr, out ffrTypeNum))
                    FFRType = ffrTypeNum;

            iBackCompatibility = Convert.ToInt32(GetValueFromXml("/FreeFormPackageHeader/BackCompatibility", ref a_Header));

            return bRet;

        }

        public string GetValueFromXml(string a_XPath, ref XmlDocument a_XmlDoc)
        {
            try
            {
                return a_XmlDoc.SelectSingleNode(a_XPath).InnerText;
            }
            catch (Exception)
            {
                return "";
            }

        }
    }

    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 2)]

    [Serializable]
    public class CFFBaseSingleFilesHeader : CFFBaseHeader
    {
        public int iID;
        public CFFBaseSingleFilesHeader ()
        {
        }
    }

    [Serializable]
    public class CFFRHeader : CFFBaseSingleFilesHeader
    {        
        public int iCompressionType;  //

        public CFFRHeader()
        {
        }
    }

    [Serializable]
    public class CFFSHeader : CFFBaseSingleFilesHeader
    {       
        public CFFSHeader()
        {

        }    
    }




    [Serializable]
    public class FFSInformation
    {
        private int m_iID;
        private int m_iVersion;

        public FFSInformation()
        {
        }
       public FFSInformation(int iID, int iVersion)
        {
            m_iID = iID;
            m_iVersion = iVersion;
        }
        
        public int ID
        {
            set
            {
                m_iID = value;
            }
            get
            {
                return m_iID;
            }
        }
        public int Version
        {
            set
            {
                m_iVersion = value;
            }
            get
            {
                return m_iVersion;
            }
        }
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 2)]
    internal class CFFRProp
    {
        public byte[] pBuffer = null;
        public ulong ulBufferLength = 0;
        public uint iFromID = 0;

        public CFFRProp()
        {
        }

    }
    
}
