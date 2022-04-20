using System;

namespace Docomotion.Shared.UtlFFP.LC
{
    public  interface ILcEngine
    {
        int ActivateLcEngine(string a_szDPPath, string a_szGroupSymbol, uint a_uiFormID, string a_szLcPath, int a_TimeToVailed, LCFilesTypes a_eRequestType, ref IntPtr a_pBuffer, ref ulong a_pBufferSize,ref string a_szErrorMsg);
		int ActivateLcEngine(string a_szDPPath, string a_szGroupSymbol, uint a_uiFormID, string a_szLcPath, int a_TimeToVailed, LCFilesTypes a_eRequestType, ref uint a_uiFormVersion, ref ushort a_uiForamSignature, ref IntPtr a_pBuffer, ref ulong a_pBufferSize, ref uint a_uiPackageSN, ref string a_szErrorMsg, ref string a_sAdditionalInfo);
		int InnerActivateLcEngine(string a_szDPPath, string a_szGroupSymbol, uint a_uiFormID, string a_szLcPath, int a_TimeToVailed, LCFilesTypes a_eRequestType, ref uint a_uiFormVersion, ref ushort a_uiForamSignature, ref IntPtr a_pBuffer, ref ulong a_pBufferSize, ref uint a_uiPackageSN, ref string a_szErrorMsg, ref string a_sAdditionalInfo);	
        void FreeMemory(IntPtr a_pBuffer);
    }
}
