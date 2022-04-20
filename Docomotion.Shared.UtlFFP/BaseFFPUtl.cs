using System;
using System.IO;
using System.Xml;
using Docomotion.Shared.ComDef;
using Docomotion.Shared.ErrorHandler;

namespace Docomotion.Shared.UtlFFP
{
    public class BaseFFPUtl:IDisposable
    {

        #region  Members
        /// <summary>
        /// the ffp Xml object
        /// </summary>
        protected XmlDocument m_FFPXmlDocument = null;

        

        /// <summary>
        /// save the System Error Code 
        /// </summary>
        protected int m_iSystemErrorCode = Lapsus.FF_NO_ERRORS;

        /// <summary>
        /// save the poject Error Code 
        /// </summary>
        protected int m_iErrorCode = Lapsus.FF_NO_ERRORS;


        /// <summary>
        /// save the error description
        /// </summary>
        protected string m_szErrorMsg = null;

        /// <summary>
        /// save the Additional Info
        /// </summary>
        protected string m_szAdditionalInfo = null;

        /// <summary>
        /// FFP File Stream
        /// </summary>
        protected Stream m_FFPFileStream = null;

        #endregion

        #region Metods

  

        /// <summary>
        /// open Stream Object 
        /// </summary>
        /// <param name="StreamObject">the Stream Object </param>
        /// <param name="OpenMode">Open Mode </param>
        /// <param name="AccessMode">Access Mode</param>
        /// <param name="szPath">the Path to open the Stream</param>
        /// <param name="iBaseError">the Base error code that will be if the Open will be filed</param>
        protected void OpenFileStream(ref Stream StreamObject, FileMode OpenMode, FileAccess AccessMode, string szPath, int iBaseError)
        {
            try
            {
                StreamObject = new FileStream(szPath, OpenMode, AccessMode);
            }
            catch (Exception ex)
            {
                ExceptionFFEngine.ThrowException(iBaseError, m_iSystemErrorCode, string.Format("{0}", szPath), ex);
               
            }
        }


        /// <summary>
        /// Check if  the user have  acess rigth to the path 
        /// </summary>
        /// <param name="szPath">the path what need to Check</param>
        /// <param name="eAccessType">acess rigth</param>
        /// <param name="iBaseCode">eror code that will hppend if we have no acess</param>
        protected void CheckAuthorization(string szPath, AccessType eAccessType, int iBaseCode)
        {
            

        }


        #endregion


        public int SystemError
        {
            get
            {
                return m_iSystemErrorCode;
            }
        }

        public string ErrorString
        {
            get
            {
                return m_szErrorMsg;
            }
        }

        public string AdditionalInfo
        {
            get
            {
                return m_szAdditionalInfo;
            }
        }



        #region IDisposable Members

        public virtual void  Dispose()
        {
            try
            {
                if (m_FFPFileStream != null)
                {
                    m_FFPFileStream.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}
