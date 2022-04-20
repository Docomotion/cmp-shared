namespace Docomotion.Shared.JScripts
{
    public partial class JavaScripts
    {
        public abstract class JScriptHTML
        {
            public const string FUNCTION_SHOW_MSG_HTML =
@"function ShowMsg(msg)
{
    alert(msg);
}";
        }

        public class MinimumCharactersHTML : JScriptHTML
        {
            private const string FUNCTION_IS_MIN_LENGTH_HTML =
"function IsMinLength(fieldToCheck, numChars)" +
"{" +
"    var res = true;" +
"    if (fieldToCheck.value.length < numChars)" +
"        res = false;" +
"    return res;" +
"}";

            private const string MAIN_FUNCTION_MINIMUM_CHARACTERS_HTML =
@"function CheckMinLength(fieldToCheck, numChars)
{{
    if(!IsMinLength(fieldToCheck, numChars))
    {{
       ShowMsg(""{0}"");
    }}
}}";

            private const string SCRIPT_HTML = @"CheckMinLength(this, {0})";

            public static string GetScriptByXmlParams(string xmlParams, out string[] functions)
            {
                string scriptBody = string.Empty;
                functions = null;

                MinimumCharactersData scriptData = Deserialize<MinimumCharactersData>(xmlParams);

                if (scriptData != null)
                {
                    scriptBody = string.Format(SCRIPT_HTML, scriptData.minimumCharacters);

                    functions = new string[2];
                    functions[0] = FUNCTION_IS_MIN_LENGTH_HTML;
                    functions[1] = string.Format(MAIN_FUNCTION_MINIMUM_CHARACTERS_HTML, scriptData.message);
                }

                return scriptBody;
            }
        }

        public class MaximumCharactersHTML : JScriptHTML
        {
            private const string FUNCTION_IS_MAX_LENGTH_HTML =
"function IsMaxLength(fieldToCheck, numChars)" +
"{" +
"    var res = true;" +
"    if (fieldToCheck.value.length > numChars)" +
"        res = false;" +
"    return res;" +
"}";

            private const string MAIN_FUNCTION_MAXIMUM_CHARACTERS_HTML =
@"function CheckMaxLength(fieldToCheck, numChars)
{{
    if(!IsMaxLength(fieldToCheck, numChars))
    {{
       ShowMsg(""{0}"");
    }}
}}";

            private const string SCRIPT_HTML = @"CheckMaxLength(this, {0})";

            public static string GetScriptByXmlParams(string xmlParams, out string[] functions)
            {
                string scriptBody = string.Empty;
                functions = null;

                MaximumCharactersData scriptData = Deserialize<MaximumCharactersData>(xmlParams);

                if (scriptData != null)
                {
                    scriptBody = string.Format(SCRIPT_HTML, scriptData.maximumCharacters);

                    functions = new string[2];
                    functions[0] = FUNCTION_IS_MAX_LENGTH_HTML;
                    functions[1] = string.Format(MAIN_FUNCTION_MAXIMUM_CHARACTERS_HTML, scriptData.message);
                }

                return scriptBody;
            }
        }

        public class EmailValidityHTML : JScriptHTML
        {
            private const string FUNCTION_IS_VALID_EMAIL_HTML =
@"function IsValidEmail(fieldToCheck)
{
    var addr = fieldToCheck.value;
    var emailRE = /^([a-zA-Z0-9_\-\.\/]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,5}|[0-9]{1,3})(\]?)$/;
    res = (addr != undefined && addr != """" && addr.match(emailRE) != null);
    return res;
}";

            private const string MAIN_FUNCTION_IS_VALID_EMAIL_HTML =
@"function CheckEmail(fieldToCheck)
{{
    if(!IsValidEmail(fieldToCheck))
    {{
       ShowMsg(""{0}"");
    }}
}}";
            private const string SCRIPT_HTML = @"CheckEmail(this)";

            public static string GetScriptByXmlParams(string xmlParams, out string[] functions)
            {
                string scriptBody = string.Empty;
                functions = null;

                EmailValidityData scriptData = Deserialize<EmailValidityData>(xmlParams);

                if (scriptData != null)
                {
                    scriptBody = SCRIPT_HTML;

                    functions = new string[2];
                    functions[0] = FUNCTION_IS_VALID_EMAIL_HTML;
                    functions[1] = string.Format(MAIN_FUNCTION_IS_VALID_EMAIL_HTML, scriptData.message);
                }

                return scriptBody;
            }
        }

        public class SubmitHTML : JScriptHTML
        {
            public static string GetMsg(string xmlParams)
            {
                string scriptMsg = string.Empty;

                SubmitData scriptData = Deserialize<SubmitData>(xmlParams);

                if (scriptData != null)
                {
                    scriptMsg = scriptData.message;
                }

                return scriptMsg;
            }
        }

        public class SaveHTML : JScriptHTML
        {
            public static string GetMsg(string xmlParams)
            {
                string scriptMsg = string.Empty;

                SaveData scriptData = Deserialize<SaveData>(xmlParams);

                if (scriptData != null)
                {
                    scriptMsg = scriptData.message;
                }

                return scriptMsg;
            }
        }


        public class SaveAndCloseAjaxHTML : JScriptHTML
        {
            public static string GetScriptByXmlParams(string xmlParams, out string[] functions)
            {
                string scriptBody = string.Empty;
                functions = null;

                SaveAndCloseAjaxData scriptData = Deserialize<SaveAndCloseAjaxData>(xmlParams);

                //FUTURE: ...create script.

                return scriptBody;
            }
        }

        public class SaveAjaxHTML : JScriptHTML
        {
            public static string GetScriptByXmlParams(string xmlParams, out string[] functions)
            {
                string scriptBody = string.Empty;
                functions = null;

                SaveAjaxData scriptData = Deserialize<SaveAjaxData>(xmlParams);

                //FUTURE: ...create script.

                return scriptBody;
            }
        }

        public class SubmitAjaxHTML : JScriptHTML
        {
            public static string GetScriptByXmlParams(string xmlParams, out string[] functions)
            {
                string scriptBody = string.Empty;
                functions = null;

                SubmitAjaxData scriptData = Deserialize<SubmitAjaxData>(xmlParams);

                //FUTURE: ...create script.

                return scriptBody;
            }
        }


    }
}
