using System;
using System.Runtime.InteropServices;
using Docomotion.Shared.ComDef;

namespace Docomotion.Shared.ErrorHandler
{
    public class ExceptionFFEngine : Exception
    {
        private ExceptionFFEngine(int error, int systemError, string errorInfo, Exception exception)
        {
            try
            {
                m_Error = error;

                int sysError = 0;
                string sysErrorMsg = string.Empty;
                if (exception != null)
                {
                    ErrorWrapper errWrapper = new ErrorWrapper(exception);
                    sysError = errWrapper.ErrorCode & 0x0000FFFF;

                    sysErrorMsg = $"\r\nMsg={exception.Message};";
                }
                else
                {
                    sysError = systemError;
                }

                string osErrMsg = string.Empty;
                if (sysError != 0) osErrMsg = $"\r\nOsErr={sysError};";

                m_ErrorMsg = string.Format("Err={0};{1}{2}\r\nObjMsg={3}", (m_Error != Lapsus.FF_NO_ERRORS ? m_Error : (-1)), osErrMsg, sysErrorMsg, errorInfo);
            }
            catch (Exception ex)
            {
                ErrorWrapper errWrapper = new ErrorWrapper(ex);
                m_Error = errWrapper.ErrorCode & 0x0000FFFF;
            }
        }

        private int m_Error = Lapsus.FF_NO_ERRORS;
        public int Error { get { return m_Error; } set { m_Error = value; } }

        private string m_ErrorMsg = null;
        public string ErrorMsg { get { return m_ErrorMsg; } }

        static public void ThrowException(int error, int systemError, string errorInfo, Exception exception)
        {
            if (exception != null)
            {
                if (!exception.GetType().Name.Equals("ExceptionFFEngine"))
                {
                    throw new ExceptionFFEngine(error, systemError, errorInfo, exception);
                }
                else
                {
                    throw exception;
                }
            }
            else
            {
                throw new ExceptionFFEngine(error, systemError, errorInfo, null);
            }
        }

        static public int GetException(Exception exception, int commonError, ref string errMsg)
        {
            int error = commonError;

            if (exception.GetType().Name.Equals("ExceptionFFEngine"))
            {
                if (((ExceptionFFEngine)exception).Error != Lapsus.FF_NO_ERRORS)
                    error = ((ExceptionFFEngine)exception).Error;

                errMsg = ((ExceptionFFEngine)exception).ErrorMsg;
            }
            else
            {
                errMsg = exception.Message;
            }

            Exception inEx = exception.InnerException;
            while (inEx != null)
            {
                errMsg = string.Format("{0};INNER:{1}",errMsg, inEx.Message);

                inEx = inEx.InnerException;
            }

            return error;
        }
    }
}
