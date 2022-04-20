using System.Collections.Generic;

namespace Docomotion.Shared.ComDef
{
    sealed public partial class FreeFormDefinitions
    {
        public class FDC
        {
            public class RepositoryPropertiesMappers
            {
                public static Dictionary<string, string> RepositoryFileColorsMapper = new Dictionary<string, string>()
                    {
                        {"black", "Black"},
                        {"blue", "Blue"},
                        {"cyan", "Cyan"},
                        {"dark_gray", "DarkGray"},
                        {"gray", "Gray"},
                        {"green", "Green"},
                        {"light_gray", "LightGray"},
                        {"magenta", "Magenta"},
                        {"orange", "Orange"},
                        {"pink", "Pink"},
                        {"red", "Red"},
                        {"white", "White"},
                        {"yellow", "Yellow"},
                        {"none", "None"}
                    };

                public static Dictionary<string, string> RepositoryObjectColorsMapper = new Dictionary<string, string>()
                    {
                        {"Black", "black"},       
                        {"Blue", "blue"},        
                        {"Cyan", "cyan"},        
                        {"DarkGray", "dark_gray"},
                        {"Gray", "gray"},        
                        {"Green", "green"},       
                        {"LightGray", "light_gray"},
                        {"Magenta", "magenta"},
                        {"Orange", "orange"},      
                        {"Pink", "pink"},        
                        {"Red", "red"},         
                        {"White", "white"},       
                        {"Yellow", "yellow"},      
                        {"None", "none"}       
                    };

                public static Dictionary<string, string> RepositoryFileBorderLineMapper = new Dictionary<string, string>()
                    {
                        {"none", "None"},
                        {"thin", "Thin"},
                        {"medium", "Medium"},
                        {"thick", "Thick"}
                    };

                public static Dictionary<string, string> RepositoryObjectBorderLineMapper = new Dictionary<string, string>()
                    {
                        {"None", "none"},
                        {"Thin", "thin"},
                        {"Medium", "medium"},
                        {"Thick", "thick"}
                    };

                public static Dictionary<string, string> RepositoryFileBorderStyleMapper = new Dictionary<string, string>()
                    {
                        {"none", "None"},
                        {"solid", "Solid"},
                        {"dashed", "Dashed"},
                        {"beveled", "Beveled"},
                        {"inset", "Inset"},
                        {"underline", "Underline"}
                    };

                public static Dictionary<string, string> RepositoryObjectBorderStyleMapper = new Dictionary<string, string>()
                    {
                        {"None", "none"},
                        {"Solid", "solid"},
                        {"Dashed", "dashed"},
                        {"Beveled", "beveled"},
                        {"Inset", "inset"},
                        {"Underline", "underline"}
                    };

                public static Dictionary<string, string> RepositoryFileScriptEventMapper = new Dictionary<string, string>()
                    {
                        {"OnSetFocus", "FocusSet"},
                        {"OnLostFocus", "FocusLost"},
                        {"OnMouseEnter", "MouseEnter"},
                        {"OnMouseExit", "MouseExit"},
                        {"OnMouseUp", "MouseUp"},
                        {"OnMouseDown", "MouseDown"}
                    };

                public static Dictionary<string, string> RepositoryObjectScriptEventMapper = new Dictionary<string, string>()
                    {
                        {"FocusSet", "OnSetFocus"}, 
                        {"FocusLost", "OnLostFocus"},
                        {"MouseEnter", "OnMouseEnter"},
                        {"MouseExit", "OnMouseExit"},
                        {"MouseUp", "OnMouseUp"},
                        {"MouseDown", "OnMouseDown"}
                    };


                public static Dictionary<string, string> RepositoryObjectUploadFileTypesMapper = new Dictionary<string, string>()
                    {
                        {"image", "Image"},
                        {"pdf", "PDF"},
                    };

                public static Dictionary<string, string> RepositoryFileUploadFileTypesMapper = new Dictionary<string, string>()
                    {
                        {"Image", "image"},
                        {"PDF", "pdf"},
                    };

                public static Dictionary<string, string> RepositoryFileButtonPositionMapper = new Dictionary<string, string>()
                    {
                        {"bottomLeft", "BottomLeft"},
                        {"bottomRight", "BottomRight"},
                        {"bottomCenter", "BottomMiddle"},

                    };

                public static Dictionary<string, string> RepositoryObjectButtonPositionMapper = new Dictionary<string, string>()
                    {
                        {"BottomLeft", "bottomLeft"},
                        {"BottomRight", "bottomRight"},
                        {"BottomMiddle", "bottomCenter"},

                    };

            }
            public class Enums
            {
                public enum RepositoryType
                {
                    NotSelected = 0,
                    Organization,
                    Project,
                    Form,
                    None,
                    Size,    // not a repository type, only of indicate the size of the enum
                }

                public enum VisualOrder
                {
                    Logical,
                    Visual,
                    RLO,
                    LRE,
                    RLE
                }

                public enum Trim
                {
                    Automatic = -1,
                    No,
                    Right,
                    Left,
                    Both
                }

                public enum LineBreak
                {
                    Automatic = -1,
                    No,
                    Yes
                }

                public enum FDCPredefinedChart
                {
                    None,
                    TzBankColumn, //BL type
                    TzBankStackedColumn,//BL type
                    TzBankColumnHeb, //FIBI type
                    TzBankColumnEng, //FIBI type
                    TzBankStackedColumnHeb,//FIBI type
                    TzBankStackedColumnEng//FIBI type
                }

                public enum ScriptEvents
                {
                    OnSetFocus,
                    OnLostFocus,
                    OnMouseEnter,
                    OnMouseExit,
                    OnMouseUp,
                    OnMouseDown,
                    onafterprint,   // Script to be run after the document is printed
                    onbeforeprint,  // Script to be run before the document is printed
                    onbeforeunload, // Script to be run when the document is about to be unloaded
                    onerror, //  Script to be run when an error occurs
                    onhashchange, //  Script to be run when there has been changes to the anchor part of the a URL
                    onload, // Fires after the page is finished loading
                    onmessage, //  Script to be run when the message is triggered
                    onoffline, // Script to be run when the browser starts to work offline
                    ononline, //  Script to be run when the browser starts to work online
                    onpagehide, // Script to be run when a user navigates away from a page
                    onpageshow, // Script to be run when a user navigates to a page
                    onpopstate, //  Script to be run when the window's history changes
                    onresize, //  Fires when the browser window is resized
                    onstorage, // Script to be run when a Web Storage area is updated
                    onunload, // Fires once a page has unloaded(or the browser window has been closed)
                }
            }
        }
    }
}
