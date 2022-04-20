using System;

namespace Docomotion.Shared.ComDef
{
    /// <summary>
    /// Class for a form holding as byte array
    /// </summary>
    [Serializable] 
    public class FormBinaryHolder
    {
        public byte[] FormData = null; //The form content
        public byte[] FormSchema = null; //The schema of this form
        public byte[] FormMetaData = null; //The metadata of the form 
       
        public FormBinaryHolder[] Subforms = null; //The list of subforms

        public FormIdentification FormIdentifier = null; //The identification data of this form
    }
}
