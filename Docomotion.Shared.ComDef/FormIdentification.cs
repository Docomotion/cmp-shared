using System;

namespace Docomotion.Shared.ComDef
{
    /// <summary>
    /// Class of identification data of the form
    /// </summary>
    [Serializable] 
    public class FormIdentification
    {
        private string m_FormId = string.Empty;
        public string FormId { get { return m_FormId; } set { m_FormId = value; } }

        private string m_FormVersion = string.Empty;
        public string FormVersion { get { return m_FormVersion; } set { m_FormVersion = value; } }

        private string m_FormName = string.Empty;
        public string FormName { get { return m_FormName; } set { m_FormName = value; } }

        private string m_FormDescription = string.Empty;
        public string FormDescription { get { return m_FormDescription; } set { m_FormDescription = value; } }

        private string m_FormGroupSymbols = string.Empty;
        public string FormGroupSymbols { get { return m_FormGroupSymbols; } set { m_FormGroupSymbols = value; } }

        public FormIdentification Clone()
        {
            FormIdentification newInstance = new FormIdentification();

            newInstance.m_FormId = string.Copy(this.m_FormId);
            newInstance.m_FormVersion = string.Copy(this.m_FormVersion);
            newInstance.m_FormName = string.Copy(this.m_FormName);
            newInstance.m_FormDescription = string.Copy(this.m_FormDescription);
            newInstance.m_FormGroupSymbols = string.Copy(this.m_FormGroupSymbols);

            return newInstance;
        }
    }
}
