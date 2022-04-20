using System;
using System.Xml;
using Docomotion.Shared.ComDef;
using Docomotion.Shared.UtlFFP.NewFFP;

namespace Docomotion.Shared.UtlFFP
{


    public class AfPackageUtl
    {

        #region IAfUtlFFP Members

        private long GetFullFFPHeader(string szFFPFileName, ref XmlDocument pFFPFullHeader, ref string szErrorString, ref string szAdditionalInfo)
        {
            try
            {
                long Ret = Lapsus.FF_NO_ERRORS;

                IAfFFPReadUtils l_oAfFFPReadUtils = null;
                string l_szFFPHeaderXmlBuffer = null;

                do
                {
                    Ret = CreateFFPReadUtilsInstance(szFFPFileName, ref l_oAfFFPReadUtils, ref szErrorString, ref szAdditionalInfo);

                    if (Ret != Lapsus.FF_NO_ERRORS)
                    {
                        break;
                    }

                    Ret = (long)l_oAfFFPReadUtils.GetFFPHeader(ref l_szFFPHeaderXmlBuffer);

                    if (Ret != Lapsus.FF_NO_ERRORS)
                    {
                        szErrorString = l_oAfFFPReadUtils.GetErrorString();
                        break;
                    }

                    XmlDocument l_oFullHeader = new XmlDocument();
                    if (pFFPFullHeader == null)
                        pFFPFullHeader = new XmlDocument();

                    pFFPFullHeader.LoadXml(l_szFFPHeaderXmlBuffer);

                } while (false);

                return Ret;
            }
            catch (Exception ex)
            {
                szErrorString = ex.Message;
                szAdditionalInfo = ex.Message;
                return Lapsus.Packager.ERR_FFP_HEADER_FATAL_ERROR;
            }

        }

        public long GetFFPHeader(string szFFPFileName, ref XmlDocument pFFPHeader, ref string szErrorString, ref string szAdditionalInfo)
        {
            long Ret = Lapsus.FF_NO_ERRORS;

            try
            {
                do
                {
                    XmlDocument l_oFullHeader = new XmlDocument();

                    string l_sError = null;

                    Ret = GetFullFFPHeader(szFFPFileName, ref l_oFullHeader, ref l_sError, ref szAdditionalInfo);

                    //Ret = GetFullFFPHeader(szFFPFileName, ref l_oFullHeader, ref szErrorString, ref szAdditionalInfo);


                    if (Ret != Lapsus.FF_NO_ERRORS)
                    {
                        break;
                    }

                    XmlNode l_pNode = l_oFullHeader.SelectSingleNode("/FFPRoot/FreeFormPackageHeader");

                    string l_sXml = l_pNode.OuterXml;
                    if (null == pFFPHeader)
                        pFFPHeader = new XmlDocument();

                    pFFPHeader.LoadXml(l_sXml);


                } while (false);
            }
            catch (Exception ex)
            {
                szErrorString = ex.Message;
                szAdditionalInfo = ex.Message;
                return Lapsus.Packager.ERR_FFP_HEADER_FATAL_ERROR;                
            }
                    
            return Ret;
        }

        public long FillFFRLinkList(string szFFPFileName, ref XmlDocument CFFRHeaderList, ref string szErrorString, ref string szAdditionalInfo)
        {
            long Ret = Lapsus.FF_NO_ERRORS;

            try
            {

                do
                {
                    XmlDocument l_oFullHeader = new XmlDocument();

                    Ret = GetFullFFPHeader(szFFPFileName, ref l_oFullHeader, ref szErrorString, ref szAdditionalInfo);


                    if (Ret != Lapsus.FF_NO_ERRORS)
                    {
                        break;
                    }

                    XmlNode l_pNode = l_oFullHeader.SelectSingleNode("/FFPRoot/FFRFileList");

                    string l_sXml = l_pNode.OuterXml;
                    if (null == CFFRHeaderList)
                        CFFRHeaderList = new XmlDocument();

                    CFFRHeaderList.LoadXml(l_sXml);


                } while (false);
            }
            catch (Exception ex)
            {
                szErrorString = ex.Message;
                szAdditionalInfo = ex.Message;
                return Lapsus.Packager.ERR_FFP_HEADER_FATAL_ERROR;
               
            }

            return Ret;
            
        }

        public long GetFFSList(string szFFPFileName, ref XmlDocument FFSList, ref string szErrorString, ref string szAdditionalInfo)
        {

            long Ret = Lapsus.FF_NO_ERRORS;

            try
            {

                do
                {
                    XmlDocument l_oFullHeader = new XmlDocument();

                    Ret = GetFullFFPHeader(szFFPFileName, ref l_oFullHeader, ref szErrorString, ref szAdditionalInfo);


                    if (Ret != Lapsus.FF_NO_ERRORS)
                    {
                        break;
                    }

                    XmlNode l_pNode = l_oFullHeader.SelectSingleNode("/FFPRoot/FFRFileList/");

                    string l_sXml = l_pNode.OuterXml;
                    if (null == FFSList)
                        FFSList = new XmlDocument();

                    FFSList.LoadXml(l_sXml);

                } while (false);
            }
            catch (Exception ex)
            {
                szErrorString = ex.Message;
                szAdditionalInfo = ex.Message;
                return Lapsus.Packager.ERR_FFP_HEADER_FATAL_ERROR;

            }
            return Ret;           
        }

        public long GetProjectHeader(string a_sFullFileName, ref XmlDocument szProjectHeader, ref string szErrorString, ref string szAdditionalInfo)
        {
            long Ret = Lapsus.FF_NO_ERRORS;

            try
            {

                do
                {
                    XmlDocument l_oFullHeader = new XmlDocument();

                    Ret = GetFullFFPHeader(a_sFullFileName, ref l_oFullHeader, ref szErrorString, ref szAdditionalInfo);


                    if (Ret != Lapsus.FF_NO_ERRORS)
                    {
                        break;
                    }

                    XmlNode l_pNode = l_oFullHeader.SelectSingleNode("/FFPRoot/FreeFormProject");

                    string l_sXml = l_pNode.OuterXml;
                    if (null == szProjectHeader)
                        szProjectHeader = new XmlDocument();

                    szProjectHeader.LoadXml(l_sXml);


                } while (false);
            }
            catch (Exception ex)
            {
                szErrorString = ex.Message;
                szAdditionalInfo = ex.Message;
                return Lapsus.Packager.ERR_FFP_HEADER_FATAL_ERROR;

            }

            return Ret;

         }
        

        #endregion


        protected long CreateFFPReadUtilsInstance(string szFFPFileName, ref IAfFFPReadUtils pAfFFPReadUtils, ref  string szErrorString, ref string szAdditionalInfo)
        {
            long Ret = Lapsus.FF_NO_ERRORS;
            
            pAfFFPReadUtils = new FFPReadUtils();


            Ret = pAfFFPReadUtils.InitPackageForRead(szFFPFileName);

            if (Ret != Lapsus.FF_NO_ERRORS)
            {
                szErrorString = pAfFFPReadUtils.GetErrorString();
                szAdditionalInfo = pAfFFPReadUtils.GetAdditionalInfo();
            }

            return Ret;
            
        }

        protected long ReadFFPHeader(string a_szFFPXmlHeader, ref CFFPHeader pFFPHeader, ref  string szErrorString)
        {

            XmlDocument l_oXmlDocument = new XmlDocument();
            long l_Ret = Lapsus.FF_NO_ERRORS;

            do
            {
               
            
            } while (false);

            return l_Ret;
        }


        

      
     
   
    }
     
}
