using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AutoFont.AfUtlFFP.LC
{
    public class LCFile
    {

        protected string m_TempLCFilePath = null;
        protected string m_LCFilePath = null;

        FileWorkMode m_eFileWorkMode = FileWorkMode.eNoAction;

        public LCFile(string a_TempLCFilePath, string a_LCFilePath, FileWorkMode a_eFileWorkMode)
        {
            m_TempLCFilePath = a_TempLCFilePath;
            m_LCFilePath = a_LCFilePath;
            m_eFileWorkMode = a_eFileWorkMode;
        }

      public  void MakeAction()
        {


            if (m_eFileWorkMode == FileWorkMode.eCreateNewLcFile)
            {
               //Copy the File To LC 
                CopyFileToLc();
               
            }

        }

        protected void CopyFileToLc()
        {
           
        }

        public void DeleteFromTemp()
        {
            try
            {
                File.Delete(m_TempLCFilePath);
            }
            catch (Exception)
            {
            }
        }

    }
}
