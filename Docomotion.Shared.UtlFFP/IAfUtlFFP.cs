using System;
using System.Collections.Generic;
using System.Xml;

namespace Docomotion.Shared.UtlFFP
{
    public interface IAfUtlFFP
    {
        int CreatePackage(string  szInputDir,
                          string szOutputDir,
                          string szWorkingDir,
                          bool bOverWrite,
                          ref  CFFPHeader pPackageHeader,
                          string szFFSXmlList,
                          ref string szErrorString);

        int GetFFPHeader(string szFFPFileName,
                         ref CFFPHeader pFFPHeader,
                         ref string szErrorString);

        int FillFRRLinkList(string szFFPFileName,
                            ref LinkedList<CFFRHeader> CFFRHeaderList,
                            ref string szErrorString);

        int GetFFSList(string szFFPFileName,
                       ref XmlDocument FFSList,
                       ref string szErrorString);
    }

    public interface IAfFFPWriteUtils
    {               
        int CreatePackage(string szInputDir,
                          string szOutputDir,
                          string szWorkingDir,
                          bool bOverWrite,
                          ref  CFFPHeader pPackageHeader,
                          string szFFSXmlList,
                          ref string szErrorString,
                          ref string szAdditionalInfo);
        
    }

    public interface IAfFFPReadUtils:IDisposable
    {
        int InitPackageForRead(string szFFPFileName);
        int GetFFPHeader(ref string pFFRPHeaderBuffer);
        int GetFFRData(uint FromID, ref byte[] pFFRFromDataBuffer);
        int GetFFRTags(uint FromID, ref string pFFRFromTagsBuffer);
        int GetPackageSN(ref uint a_PackageSN);
        int GetFormInfo(uint a_FromID, ref uint a_FormVresion, ref ushort a_FormSignature,ref ushort a_FormExpirationDate);
        int IsFormExpired(ushort a_FormExpiredDatas, ref bool a_IsExpired);
        void GetSystemStartTime(ref string a_szSystemStartTime);
        string GetErrorString();
        string GetAdditionalInfo();
       
        
    }


    //public interface IGetFFR
    //{
    //    int GetFFR(string szFFPFileName, uint FromID, ref IntPtr pFFRFromBuffer, ref ulong ulFFRFromBuffer, ref string szErrorString);
    //    void FreeMemory(IntPtr pFFRFromBuffer);
    //}
    
                             
}
