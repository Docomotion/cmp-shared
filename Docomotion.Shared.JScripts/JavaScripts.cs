using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Docomotion.Shared.JScripts
{
    public partial class JavaScripts
    {
        public enum scriptType
        {
            ManualScript,
            EmailValidity,
            MinimumCharacters,
            MaximumCharacters,
            Submit,
            GoToPage,
            Save,
            SaveAndCloseAjax,
            SaveAjax,
            SubmitAjax,
            NumberingMask,
            NumberIsNegative,
        }

        public class IAC_ACTION
        {
            public const string SAVE = "1";
            public const string SUBMIT = "2";
        }

        #region scripts

        public abstract class JScript
        {
            public const string FUNCTION_SHOW_MSG_PDF =   
@"function ShowMsg(msg) 
{
  app.alert(msg);
}";
        }

        public class MinimumCharacters : JScript
        {
            /* Example >>>
function IsMinLength(fieldName, numChars)
{
    var res = true;
    if (this.getField(fieldName).value.toString().length < numChars)
        res = false;
return res;
}

if(!IsMinLength("Interactive___InFlSp_Text_Box", 5))
{
    ShowMsg("Minimum 10 characters required");
}
*/

            public const string FUNCTION_IS_MIN_LENGTH_PDF =
@"function IsMinLength(fieldName, numChars)
{
    var res = true;
    if (this.getField(fieldName).value.toString().length < numChars)
        res = false;
    return res;
}";

            private const string INCLUDE_FUNCTIONS_SCRIPT_PDF = 
@"{3}

{4}

if(!IsMinLength(""{0}"", {1}))
{{
   ShowMsg(""{2}"");
}}";

            private const string SCRIPT_PDF =
@"if(!IsMinLength(""{0}"", {1}))
{{
   ShowMsg(""{2}"");
}}";

            public static string GetScript(string fieldName, int charNumber, string message, bool isPdf)
            {
                string scriptBody = string.Empty;

                if (isPdf)
                    scriptBody = string.Format(INCLUDE_FUNCTIONS_SCRIPT_PDF, fieldName, charNumber, message, FUNCTION_IS_MIN_LENGTH_PDF, FUNCTION_SHOW_MSG_PDF);
                else
                    scriptBody = "";

                return scriptBody;
            }

            public static string GetScriptBodyOnly(string fieldName, int charNumber, string message, bool isPdf)
            {
                string scriptBody = string.Empty;

                if (isPdf)
                    scriptBody = string.Format(SCRIPT_PDF, fieldName, charNumber, message);
                else
                    scriptBody = "";

                return scriptBody;
            }

            public static string GetScriptByXmlParams(string fieldName, string xmlParams, bool isPdf)
            {
                string scriptBody = string.Empty;

                MinimumCharactersData scriptData = Deserialize<MinimumCharactersData>(xmlParams);

                if (scriptData != null)
                {
                    scriptBody = GetScriptBodyOnly(fieldName, scriptData.minimumCharacters, scriptData.message, isPdf);
                }

                return scriptBody;
            }
        }

        public class MaximumCharacters : JScript
        {

            /* Example >>>
function IsMaxLength(fieldName, numChars)
{
    var res = true;
    if (this.getField(fieldName).value.toString().length > numChars)
        res = false;
    return res;
}

if(!IsMaxLength("Interactive___InFlSp_Text_Box", 5))
{
    ShowMsg("Maximum 10 characters required");
}
*/

            public const string FUNCTION_IS_MAX_LENGTH_PDF =
@"function IsMaxLength(fieldName, numChars)
{
    var res = true;
    if (this.getField(fieldName).value.toString().length > numChars)
        res = false;
    return res;
}";

            private const string INCLUDE_FUNCTIONS_SCRIPT_PDF =
@"{3}

{4}

if(!IsMaxLength(""{0}"", {1}))
{{
   ShowMsg(""{2}"");
}}";

            private const string SCRIPT_PDF =
@"if(!IsMaxLength(""{0}"", {1}))
{{
   ShowMsg(""{2}"");
}}";

            private const string MAXIMUM_CHARACTERS_HTML = "";

            public static string GetScript(string fieldName, int charNumber, string message, bool isPdf)
            {
                string scriptBody = string.Empty;

                if (isPdf)
                    scriptBody = string.Format(INCLUDE_FUNCTIONS_SCRIPT_PDF, fieldName, charNumber, message, FUNCTION_IS_MAX_LENGTH_PDF, FUNCTION_SHOW_MSG_PDF);
                else
                    scriptBody = MAXIMUM_CHARACTERS_HTML;

                return scriptBody;
            }

            public static string GetScriptBodyOnly(string fieldName, int charNumber, string message, bool isPdf)
            {
                string scriptBody = string.Empty;

                if (isPdf)
                    scriptBody = string.Format(SCRIPT_PDF, fieldName, charNumber, message);
                else
                    scriptBody = MAXIMUM_CHARACTERS_HTML;

                return scriptBody;
            }

            public static string GetScriptByXmlParams(string fieldName, string xmlParams, bool isPdf)
            {
                string scriptBody = string.Empty;

                MaximumCharactersData scriptData = Deserialize<MaximumCharactersData>(xmlParams);

                if (scriptData != null)
                {
                    scriptBody = GetScriptBodyOnly(fieldName, scriptData.maximumCharacters, scriptData.message, isPdf);
                }

                return scriptBody;
            }

        }

        public class EmailValidity : JScript 
        {


            /* Example >>>
function IsValidEmail(fieldName)
{
    var addr = this.getField(fieldName).value;
    var emailRE = /^([a-zA-Z0-9_\-\.\/]+)@((\[[0-9]{{1,3}}\.[0-9]{{1,3}}\.[0-9]{{1,3}}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{{2,5}}|[0-9]{{1,3}})(\]?)$/;
    res = (addr != undefined && addr != "" && addr.match(emailRE) != null);
    return res;
}

if(!IsValidEmail("Interactive___InFlSp_Text_Box"))
{
    ShowMsg("The email address is invalid. Please enter a valid address.");
}
*/

            public const string FUNCTION_IS_VALID_EMAIL_PDF =
@"function IsValidEmail(fieldName)
{
    var addr = this.getField(fieldName).value;
    var emailRE = /^([a-zA-Z0-9_\-\.\/]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,5}|[0-9]{1,3})(\]?)$/;
    res = (addr != undefined && addr != """" && addr.match(emailRE) != null);
    return res;
}";

            private const string INCLUDE_FUNCTIONS_SCRIPT_PDF =
@"{2}

{3}

if(!IsValidEmail(""{0}""))
{{
   ShowMsg(""{1}"");
}}";

            private const string SCRIPT_PDF =
@"if(!IsValidEmail(""{0}""))
{{
   ShowMsg(""{1}"");
}}";

            private const string EMAIL_VALIDITY_HTML = "";

            public static string GetScript(string fieldName, string message, bool isPdf)
            {
                string scriptBody = string.Empty;

                if (isPdf)
                    scriptBody = string.Format(INCLUDE_FUNCTIONS_SCRIPT_PDF, fieldName, message, FUNCTION_IS_VALID_EMAIL_PDF, FUNCTION_SHOW_MSG_PDF);
                else
                    scriptBody = EMAIL_VALIDITY_HTML;

                return scriptBody;
            }


            public static string GetScriptBodyOnly(string fieldName, string message, bool isPdf)
            {
                string scriptBody = string.Empty;

                if (isPdf)
                    scriptBody = string.Format(SCRIPT_PDF, fieldName, message);
                else
                    scriptBody = EMAIL_VALIDITY_HTML;

                return scriptBody;
            }

            public static string GetScriptByXmlParams(string fieldName, string xmlParams, bool isPdf)
            {
                string scriptBody = string.Empty;

                EmailValidityData scriptData = Deserialize<EmailValidityData>(xmlParams);

                if (scriptData != null)
                {
                    scriptBody = GetScriptBodyOnly(fieldName, scriptData.message, isPdf);
                }

                return scriptBody;
            }
        }

        public class GoToPage : JScript
        {
            private const string GOTO_PAGE_PDF = "this.pageNum = [{0}];";
            private const string GOTO_PAGE_HTML = "";

            public static string GetScript(string fieldName, int pageNumber, bool isPdf)
            {
                string scriptBody = string.Empty;

                if (isPdf)
                    scriptBody = string.Format(GOTO_PAGE_PDF, pageNumber-1);
                else
                    scriptBody = GOTO_PAGE_HTML;

                return scriptBody;
            }

            public static string GetScriptByXmlParams(string fieldName, string xmlParams, bool isPdf)
            {
                string scriptBody = string.Empty;

                GoToPageData scriptData = Deserialize<GoToPageData>(xmlParams);

                if (scriptData != null)
                {
                    scriptBody = GetScript(fieldName, scriptData.pageNumber, isPdf);
                }

                return scriptBody;
            }
        }

        public class Submit
        {
            public static string AssemblePDFSubmitScript(string message, List<string> mandatoryFieldList)
            {
                string scriptBody = string.Empty;

                scriptBody = BuildSubmitScript(message, mandatoryFieldList, null, "[IAC_ADDRESS]", IAC_ACTION.SUBMIT);

                return scriptBody;
            }

            public static string AssemblePDFSubmitScriptByXmlParams(string xmlParams, List<string> mandatoryFieldList, List<string> radiobuttonGroups, string address2Submit)
            {
                string scriptBody = string.Empty;

                SubmitData scriptData = Deserialize<SubmitData>(xmlParams);

                if (scriptData != null)
                {
                    scriptBody = BuildSubmitScript(scriptData.message, mandatoryFieldList, radiobuttonGroups, address2Submit, IAC_ACTION.SUBMIT);
                }

                return scriptBody;
            }

        }

        public class Save
        {
            public static string AssemblePDFSaveScript(string message, List<string> mandatoryFieldList)
            {
                string scriptBody = string.Empty;

                scriptBody = BuildSubmitScript(message, mandatoryFieldList, null, "[IAC_ADDRESS]", IAC_ACTION.SAVE);

                return scriptBody;
            }

            public static string AssemblePDFSaveScriptByXmlParams(string xmlParams, List<string> mandatoryFieldList, List<string> radiobuttonGroups, string address2Submit)
            {
                string scriptBody = string.Empty;

                SaveData scriptData = Deserialize<SaveData>(xmlParams);

                if (scriptData != null)
                {
                    scriptBody = BuildSubmitScript(scriptData.message, mandatoryFieldList, radiobuttonGroups, address2Submit, IAC_ACTION.SAVE);
                }

                return scriptBody;
            }
        }

        #endregion

        #region functions

        private static T Deserialize<T>(string inputString)
        {
            T obj = default(T);

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            StringReader strReader = new StringReader(inputString);

            try
            {
                obj = (T)serializer.Deserialize(strReader);
            }
            catch (Exception ex)
            {

            }
            strReader.Close();

            return obj;
        }

        private static string BuildSubmitScript(string msg, List<string> mandatoryFieldList, List<string> radiobuttonGroups, string address2Submit, string action)
            {
                //this.getField("Interactive___InFlSp_Text_Box").value.toString() == ""
                string condition = null;

                string radioButtonCondition = null;
                if (radiobuttonGroups != null && radiobuttonGroups.Count > 0)
                {
                    foreach (string radioGroup in radiobuttonGroups)
                    {
                        if (!string.IsNullOrWhiteSpace(radioGroup))
                        {
                            radioButtonCondition += @"(";

                            radioButtonCondition += string.Format(@"this.getField(""{0}"").value == ""Off""", radioGroup);

                            radioButtonCondition += @")";
                        }

                        if (!radiobuttonGroups.Last().Equals(radioGroup))
                            radioButtonCondition += @" || ";
                    }
                }

                if (mandatoryFieldList.Count > 0)
                {
                    condition = "if(";
                    for (int i = 0; i < mandatoryFieldList.Count; i++)
                    {
                        condition += string.Format(@"this.getField(""{0}"").value.toString() == """"", mandatoryFieldList[i]);

                        if (i < mandatoryFieldList.Count - 1)
                            condition += " || ";
                    }

                    if (!string.IsNullOrWhiteSpace(radioButtonCondition))
                    {
                        condition += string.Format(" || ({0})", radioButtonCondition);
                    }

                    condition += @")
{
";
                    condition += string.Format(@"app.alert(""{0}"");
}}
", msg);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(radioButtonCondition))
                    {
                        condition = string.Format("if({0})", radioButtonCondition);
                        condition += @"{
";
                        condition += string.Format(@"app.alert(""{0}"");
}}
", msg);
                    }
                }

                if (!address2Submit.EndsWith("/")) address2Submit += "/";

                if (condition != null)
                {
                    //old version this.submitForm({cURL: "http://vm-SmallMpS1/IACHandler/handler.ashx?action=2",cSubmitAs: "XML"});
                    //for WebApi this.submitForm({cURL: "http://vm-SmallMpS1/IACHandler/2",cSubmitAs: "XML"});

                    condition += string.Format(@"else
{{
this.submitForm({{cURL: ""{0}{1}"",cSubmitAs: ""XML""}});
}}
", address2Submit, action);
                }
                else
                {
                    condition += string.Format(@"this.submitForm({{cURL: ""{0}{1}"",cSubmitAs: ""XML""}});", address2Submit, action);
                }

                return condition;
            }

        #endregion
    }
}
