using System.Xml.Serialization;

namespace Docomotion.Shared.JScripts
{
    //public enum scriptType
    //{
    //    ManualScript,
    //    EmailValidity,
    //    MinimumCharacters,
    //    MaximumCharacters,
    //    Submit,
    //    //Print,
    //    GoToPage,
    //    MandatoryToFill
    //}

    [XmlInclude(typeof(MaximumCharactersData))]
    [XmlInclude(typeof(MinimumCharactersData))]
    [XmlInclude(typeof(EmailValidityData))]
    [XmlInclude(typeof(SubmitData))]
    [XmlInclude(typeof(SaveData))]
    [XmlInclude(typeof(GoToPageData))]
    [XmlInclude(typeof(SaveAndCloseAjaxData))]
    [XmlInclude(typeof(SaveAjaxData))]
    [XmlInclude(typeof(SubmitAjaxData))]
    public abstract class InteractiveScriptData { }

    [XmlRoot("scriptData")]
    public class EmailValidityData : InteractiveScriptData
    {
        public string message;
        public bool isCustomMessage;
    }

    [XmlRoot("scriptData")]
    public class GoToPageData : InteractiveScriptData
    {
        public int pageNumber;

    }

    [XmlRoot("scriptData")]
    public class MaximumCharactersData : InteractiveScriptData
    {
        public string message;
        public int maximumCharacters;
        public bool isCustomMessage;
    }

    [XmlRoot("scriptData")]
    public class MinimumCharactersData : InteractiveScriptData
    {
        public string message;
        public int minimumCharacters;
        public bool isCustomMessage;
    }

    [XmlRoot("scriptData")]
    public class SubmitData : InteractiveScriptData
    {
        public string message;
        public bool isCustomMessage;
    }

    [XmlRoot("scriptData")]
    public class SaveData : InteractiveScriptData
    {
        public string message;
        public bool isCustomMessage;
    }

    [XmlRoot("scriptData")]
    public class SaveAndCloseAjaxData : InteractiveScriptData
    {
        public string FunctionName;
        public string message;
        public bool isCustomMessage;
    }

    [XmlRoot("scriptData")]
    public class SaveAjaxData : InteractiveScriptData
    {
        public string FunctionName;
        public string message;
        public bool isCustomMessage;
    }

    [XmlRoot("scriptData")]
    public class SubmitAjaxData : InteractiveScriptData
    {
        public string FunctionName;
        public string message;
        public bool isCustomMessage;
    }
}
