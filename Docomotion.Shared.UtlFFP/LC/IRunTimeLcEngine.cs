using System;
using System.Collections.Generic;
using System.Text;

namespace AutoFont.AfUtlFFP.LC
{
    interface IRunTimeLcEngine
    {
        int ActivateLcEngine(string a_szDPPath, string a_szGroupSimbol, string a_szLcPath, string a_szTempLcPath, uint a_FromID, string m_aTimeToVailed, ref IntPtr a_psTempLcFilePath);
         void UpdateLC();
        void FreeMemory(IntPtr a_Prt);

    }
}
