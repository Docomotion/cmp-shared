using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Xml;

namespace Docomotion.Shared.EngineUtils
{
    public sealed class WMLReader
    {
        #region XPATH definition

        public const string XPATH_DOCUMENT_W_BODY = "//pkg:package/pkg:part[@pkg:name='/word/document.xml']/pkg:xmlData/w:document/w:body";

        public const string XPATH_XSL_DOCUMENT_W_BODY = ".//pkg:package/pkg:part[@pkg:name='/word/document.xml']/pkg:xmlData/w:document/w:body";

        public const string XPATH_XSL_NUMBERING = ".//pkg:package/pkg:part[@pkg:name='/word/numbering.xml']/pkg:xmlData/w:numbering";

        public const string XPATH_XSL_NUMBERING_PKG_PART = ".//pkg:package/pkg:part[@pkg:name='/word/numbering.xml']";

        public const string XPATH_XSL_DEFAULT_STYLES_PARA = ".//w:styles/w:style[@w:type='paragraph' and @w:default='1']";

        public const string XPATH_XSL_DEFAULT_STYLES_TABLE = ".//w:styles/w:style[@w:type='table' and @w:default='1']";

        public const string XPATH_XSL_DEFAULT_STYLES_CHAR = ".//w:styles/w:style[@w:type='character' and @w:default='1']";

        public const string  XPATH_STYLES = "//pkg:package/pkg:part[@pkg:name='/word/styles.xml']/pkg:xmlData/w:styles";

        public const string XPATH_STYLES_WITH_EFFECTS = "//pkg:package/pkg:part[@pkg:name='/word/stylesWithEffects.xml']/pkg:xmlData/w:styles";

        #endregion

        private XmlNamespaceManager m_mngrWml = null;

        private void AddNameSpaces()
        {
            m_mngrWml = new XmlNamespaceManager(new NameTable());

            m_mngrWml.AddNamespace("pkg", PrefixNS.PKG);
            m_mngrWml.AddNamespace("ve", PrefixNS.VE);
            m_mngrWml.AddNamespace("v", PrefixNS.V);
            m_mngrWml.AddNamespace("w", PrefixNS.W);
            m_mngrWml.AddNamespace("o", PrefixNS.O);
            m_mngrWml.AddNamespace("r", PrefixNS.R);
            m_mngrWml.AddNamespace("m", PrefixNS.M);
            m_mngrWml.AddNamespace("wp", PrefixNS.WP);
            m_mngrWml.AddNamespace("w10", PrefixNS.W10);
            m_mngrWml.AddNamespace("wne", PrefixNS.WNE);
            m_mngrWml.AddNamespace("a", PrefixNS.A);
            m_mngrWml.AddNamespace("pic", PrefixNS.PIC);
            m_mngrWml.AddNamespace("xsi", PrefixNS.XSI);
            m_mngrWml.AddNamespace("xsl", PrefixNS.XSL);
            m_mngrWml.AddNamespace("dc", PrefixNS.DC);
            m_mngrWml.AddNamespace("cp", PrefixNS.CP);
            m_mngrWml.AddNamespace("wps", PrefixNS.WPS);
            m_mngrWml.AddNamespace("wpg", PrefixNS.WPG);
            m_mngrWml.AddNamespace("AfMasking", "AutofontMasking");
            m_mngrWml.AddNamespace("AfUseDB", "AutofontUseDB");

        }


        private WMLReader()
        {
            AddNameSpaces();
        }
        
        private static WMLReader instance = null;
        private static object m_LockObj = new object();
        private static WMLReader Instance
        {
            get
            {
                if (instance == null)
                    lock (m_LockObj)
                        if (instance == null)
                            instance = new WMLReader();

                return instance;
            }
        }

        static public XmlNamespaceManager MngrWml
        {
            get
            {
                return Instance.m_mngrWml;
            }
        }

        static private string GetAttrValue(XmlNode node, string attrName)
        {
            string result = null;

            if (node != null)
            {
                XmlAttribute attr = (XmlAttribute)node.Attributes.GetNamedItem(attrName);
                if (attr != null) result = string.Copy(attr.Value);
            }

            return result;
        }

        static public bool ST_StringValue(XmlNode node, string attrName, NullableStringValue value)
        {
            value.Set = GetAttrValue(node, attrName);

            return value.HasValue;
        }

        static public string ST_StringValueAsString(XmlNode node, string attrName)
        {
            return GetAttrValue(node, attrName);
        }

        static public bool ST_TwipsMeasureValue(XmlNode node, string attrName, NullableFloatValue value)
        {
            string attrValue = GetAttrValue(node, attrName);

            if (!string.IsNullOrEmpty(attrValue))
            {
                value.Set = Convert.ToSingle(attrValue);
            }
            return value.HasValue;
        }

        static public bool ST_OnOffValue(XmlNode node, NullableBoolValue bValue)
        {
            if (node != null)
            {
                string sValue = GetAttrValue(node, "w:val");
                bValue.Set = true;
                GetOnOffValue(sValue, bValue);
            }
            return bValue.HasValue;
        }

        static public bool ST_OnOffAttributeValue(XmlNode node, string attrName, NullableBoolValue bValue)
        {
            string attrValue = GetAttrValue(node, attrName);

            if (!string.IsNullOrEmpty(attrValue))
            {
                GetOnOffValue(attrValue, bValue);
            }
            return bValue.HasValue;
        }

        static private void GetOnOffValue(string sValue, NullableBoolValue bValue)
        {
            if (sValue != null)
            {
                if (sValue == ST_OnOff.ON_OFF_VAL_FALSE || sValue == ST_OnOff.ON_OFF_VAL_OFF || sValue == ST_OnOff.ON_OFF_VAL_0)
                {
                    bValue.Set = false;
                }
                else
                {
                    bValue.Set = true;
                }
            }
        }

        static public bool ST_DecimalNumberValue(XmlNode node, string attrName, NullableIntValue value)
        {
            string attrValue = GetAttrValue(node, attrName);

            if (!string.IsNullOrEmpty(attrValue))
            {
                value.Set = Convert.ToInt32(attrValue);
            }
            return value.HasValue;
        }

        static public bool ST_Coordinate32MeasureValue(XmlNode node, NullableIntValue value)
        {
            if (node != null)
            {
                int twips = ConvertEMUS2Twips(node.InnerText);
                value.Set = twips;
            }

            return value.HasValue;
        }

        static public bool ST_ShortHexNumberValue(XmlNode node, string attrName, NullableIntValue value)
        {
            string attrValue = GetAttrValue(node, attrName);

            if (!string.IsNullOrEmpty(attrValue))
            {
                value.Set = Convert.ToInt32(attrValue, 16);
            }
            return value.HasValue;
        }

        static public bool ST_HpsMeasureValue(XmlNode node, string attrName, NullableFloatValue value)
        {
            string attrValue = GetAttrValue(node, attrName);

            if (!string.IsNullOrEmpty(attrValue))
            {
                value.Set = Convert.ToSingle(attrValue) / 2F;
            }
            return value.HasValue;
        }

        static public bool ST_EighthPointMeasureValue(XmlNode node, string attrName, NullableFloatValue value)
        {
            string attrValue = GetAttrValue(node, attrName);

            if (!string.IsNullOrEmpty(attrValue))
            {
                float fValue = Convert.ToSingle(attrValue);
                fValue = fValue / 8F * 20F; //convert to twips

                value.Set = fValue;
            }
            return value.HasValue;
        }

        static private int ConvertEMUS2Twips(string value)
        {
            float fPointValue = Convert.ToInt64(value) / 12700F; //convert from EMUS
            return Convert.ToInt32((fPointValue * 20F));
        }

        static public bool ST_Coordinate32MeasureValue(XmlNode node, string attrName, NullableIntValue value)
        {
            string attrValue = GetAttrValue(node, attrName);

            if (!string.IsNullOrEmpty(attrValue))
            {
                int twips = ConvertEMUS2Twips(attrValue);
                value.Set = twips;
                //FUTURE: refactor for supporting the Measure_Units: <xsd:pattern value="-?[0-9]+(\.[0-9]+)?(mm|cm|in|pt|pc|pi)"/>
            }

            return value.HasValue;
        }

        static public bool ST_PositiveCoordinateMeasureValue(XmlNode node, string attrName, NullableIntValue value)
        {
            string attrValue = GetAttrValue(node, attrName);

            if (!string.IsNullOrEmpty(attrValue))
            {
                value.Set = ConvertEMUS2Twips(attrValue);
            }

            return value.HasValue;
        }

        static public bool ST_AngleMeasureValue(XmlNode node, string attrName, NullableFloatValue value)
        {//60,000ths of a degree.
            string attrValue = GetAttrValue(node, attrName);

            if (!string.IsNullOrEmpty(attrValue))
            {
                value.Set = Convert.ToSingle(attrValue) / 60000F;
            }

            return value.HasValue;
        }

        static public bool ST_PercentageMeasureValue(XmlNode node, string attrName, NullableFloatValue value)
        {
            string attrValue = GetAttrValue(node, attrName);

            if (!string.IsNullOrEmpty(attrValue))
            {
                if (attrValue.EndsWith("%"))
                {
                    value.Set = Convert.ToSingle(attrValue.Substring(0, attrValue.Length - 1));
                }
                else
                {
                    value.Set = Convert.ToSingle(attrValue) / 1000F;

                }
            }

            return value.HasValue;
        }

        static public float Twips2Point(float twipsVal)
        {
            return (twipsVal / 20F);
        }

        static public void GetFirstChilds(XmlNode parentNode, ref List<XmlNode> childNodes, string[] searchedNodeName)
        {
            string path = "";
            foreach (string curChildNodeName in searchedNodeName)
            {
                path = string.Format("{0}descendant::{1}|", path, curChildNodeName);
            }
            path = path.Remove(path.Length - 1);
            XmlNodeList AllChilds = parentNode.SelectNodes(path, Instance.m_mngrWml);

            XmlNode curChild = null;
            foreach (XmlNode childToAdd in AllChilds)
            {
                curChild = childToAdd.ParentNode; ;
                bool TakeIt = true;
                while (curChild != parentNode)
                {
                    foreach (string child in searchedNodeName)
                    {
                        if (curChild.Name == child)
                        {
                            TakeIt = false;
                            break;
                        }
                    }
                    if (TakeIt)
                        curChild = curChild.ParentNode;
                    else
                        break;
                }
                if (TakeIt)
                {
                    if (childNodes == null)
                        childNodes = new List<XmlNode>();
                    if (!childNodes.Contains(childToAdd))
                        childNodes.Add(childToAdd);
                }
            }
        }

        static public string GetParentPartName(XmlNode wmlObjNode)
        {
            string xpath = string.Format("ancestor::pkg:part[1]");
            string containerName = "document.xml";
            try
            {
                XmlNode pkgNode = wmlObjNode.SelectSingleNode(xpath, MngrWml);
                string pkgName = ST_StringValueAsString(pkgNode, "pkg:name");

                if (!string.IsNullOrWhiteSpace(pkgName))
                {
                    int firstInd = pkgName.LastIndexOf('/') + 1;
                    containerName = pkgName.Substring(firstInd, pkgName.Length - firstInd);
                }
            }
            catch { }

            return containerName;
        }
    }

    public class NullableBoolValue : ICloneable
    {
        public NullableBoolValue(bool defaultValue) { m_Default = defaultValue; }
        public NullableBoolValue() { }

        private bool m_Default = false;

        private Nullable<bool> m_Value = null;

        public Nullable<bool> Set { set { if (value != null) m_Value = value; } }
        public bool Get { get { return m_Value.GetValueOrDefault(m_Default); } }

        public bool HasValue { get { return (m_Value.HasValue); } }

        public void DeleteValue() { m_Value = null; }

        public object Clone()
        {
            NullableBoolValue NullableBoolValue = new NullableBoolValue();
            NullableBoolValue.m_Default = this.m_Default;
            NullableBoolValue.m_Value = this.m_Value;

            return NullableBoolValue;
        }
    }

    public class NullableFloatValue : ICloneable
    {
        public NullableFloatValue(float defaultValue) { m_Default = defaultValue; }
        public NullableFloatValue() { }

        private float m_Default = 0F;
        public float DefaultValue { set { m_Default = value; } }

        private Nullable<float> m_Value = null;

        public Nullable<float> Set { set { if (value != null) m_Value = value; } }

        public float Get { get { return m_Value.GetValueOrDefault(m_Default); } }

        public bool HasValue { get { return (m_Value.HasValue); } }

        public void DeleteValue() { m_Value = null; }

        public object Clone()
        {
            NullableFloatValue NewNullableFloatValue = new NullableFloatValue();
            NewNullableFloatValue.m_Default = this.m_Default;
            NewNullableFloatValue.m_Value = this.m_Value;

            return NewNullableFloatValue;
        }
    }

    public class NullableIntValue : ICloneable
    {
        public NullableIntValue(int defaultValue) { m_Default = defaultValue; }
        public NullableIntValue() { }

        private int m_Default = 0;

        private Nullable<int> m_Value = null;

        public Nullable<int> Set { set { if (value != null) m_Value = value; } }

        public bool HasValue { get { return (m_Value.HasValue); } }

        public int Get { get { return m_Value.GetValueOrDefault(m_Default); } }

        public void DeleteValue() { m_Value = null; }

        public object Clone()
        {
            NullableIntValue NewNullableIntValue = new NullableIntValue();
            NewNullableIntValue.m_Default = this.m_Default;
            NewNullableIntValue.m_Value = this.m_Value;

            return NewNullableIntValue;
        }
    }

    public class NullableStringValue : ICloneable
    {
        public NullableStringValue(string defaultValue) { m_Default = defaultValue; }
        public NullableStringValue() { }

        private string m_Default = string.Empty;
        private string m_Value = null;

        public string Set { set
            {
                if (value != null)
                    m_Value = string.Copy(value);
                else
                    m_Value = null;
            } }

        public bool HasValue { get { return (m_Value != null); } }

        public string Get { get { return (m_Value != null) ? m_Value : m_Default; } }

        public object Clone()
        {
            NullableStringValue NewNullableStringValue = new NullableStringValue();
            if (m_Default != null) NewNullableStringValue.m_Default = (string)this.m_Default.Clone();
            if (m_Value != null) NewNullableStringValue.m_Value = (string)this.m_Value.Clone();

            return NewNullableStringValue;
        }
    }

    public sealed class ColorConverterUtilities
    {
        private static bool decreaseRedFlag = false;
        public static string GetLuminanceRGB(string baseColorHex, float lumMod, float lumOffSet)
        {
            Color baseColorString = Color.FromArgb(Int32.Parse(baseColorHex, NumberStyles.HexNumber));

            if (lumOffSet == 0F)
                decreaseRedFlag = true;

            Color modifiedColor = HSL.ModifyBrightness(baseColorString, lumMod, lumOffSet);

            return GetHexStringFromColor(modifiedColor);
        }

        private static string GetHexStringFromColor(System.Drawing.Color color)
        {
            return decreaseRedFlag ? (color.R - 1).ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2") :
                                     color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        internal class HSL
        {
            #region Fields And Properties
            private double m_H = 0;
            private double m_S = 0;
            private double m_L = 0;

            public double H
            {
                get { return m_H; }
                set
                {
                    m_H = value;
                    m_H = m_H > 1 ? 1 : m_H < 0 ? 0 : m_H;
                }
            }

            public double S
            {
                get { return m_S; }
                set
                {
                    m_S = value;
                    m_S = m_S > 1 ? 1 : m_S < 0 ? 0 : m_S;
                }
            }

            public double L
            {
                get { return m_L; }
                set
                {
                    m_L = value;
                    m_L = m_L > 1 ? 1 : m_L < 0 ? 0 : m_L;
                }
            }

            #endregion

            /// <summary>
            /// Sets the absolute brightness of a color
            /// </summary>
            /// <param name="baseColor">Original color</param>
            /// <param name="brightness">The luminance level to impose</param>
            /// <returns>an adjusted color</returns>
            private static Color SetBrightness(Color baseColor, double brightness)
            {
                HSL hsl = RgbToHsl(baseColor);
                hsl.L = brightness;
                return HslToRgb(hsl);

            }

            /// <summary>
            /// Modifies an existing brightness level
            /// </summary>
            /// <remarks>
            /// To reduce brightness use a number smaller than 1. To increase brightness use a number larger tnan 1
            /// </remarks>
            /// <param name="baseColor">The original color</param>
            /// <param name="brightness">The luminance delta</param>
            /// <returns>An adjusted color</returns>
            public static Color ModifyBrightness(Color baseColor, double lumMod, double lumOffSet)
            {
                HSL hsl = RgbToHsl(baseColor);
                hsl.L *= (lumMod / 100);
                hsl.L += (lumOffSet / 100);
                return HslToRgb(hsl);
            }

            /// <summary>
            /// Sets the absolute saturation level
            /// </summary>
            /// <remarks>Accepted values 0-1</remarks>
            /// <param name="baseColor">An original color</param>
            /// <param name="Saturation">The saturation value to impose</param>
            /// <returns>An adjusted color</returns>
            private static Color SetSaturation(Color baseColor, double Saturation)
            {
                HSL hsl = RgbToHsl(baseColor);
                hsl.S = Saturation;
                return HslToRgb(hsl);
            }

            /// <summary>
            /// Modifies an existing Saturation level
            /// </summary>
            /// <remarks>
            /// To reduce Saturation use a number smaller than 1. To increase Saturation use a number larger tnan 1
            /// </remarks>
            /// <param name="baseColor">The original color</param>
            /// <param name="satMod">The saturation delta</param>
            /// <returns>An adjusted color</returns>
            public static Color ModifySaturation(Color baseColor, double satMod, double satOffSet)
            {
                HSL hsl = RgbToHsl(baseColor);
                hsl.S *= satMod;
                hsl.S += satOffSet;
                return HslToRgb(hsl);
            }

            /// <summary>
            /// Sets the absolute Hue level
            /// </summary>
            /// <remarks>Accepted values 0-1</remarks>
            /// <param name="baseColor">An original color</param>
            /// <param name="Hue">The Hue value to impose</param>
            /// <returns>An adjusted color</returns>
            private static Color SetHue(Color baseColor, double Hue)
            {
                HSL hsl = RgbToHsl(baseColor);
                hsl.H = Hue;
                return HslToRgb(hsl);
            }

            /// <summary>
            /// Modifies an existing Hue level
            /// </summary>
            /// <remarks>
            /// To reduce Hue use a number smaller than 1. To increase Hue use a number larger tnan 1
            /// </remarks>
            /// <param name="baseColor">The original color</param>
            /// <param name="Hue">The Hue delta</param>
            /// <returns>An adjusted color</returns>
            public static Color ModifyHue(Color baseColor, double Hue)
            {
                HSL hsl = RgbToHsl(baseColor);
                hsl.H *= Hue;
                return HslToRgb(hsl);
            }

            /// <summary>
            /// Converts a color from HSL to RGB
            /// </summary>
            /// <remarks>Adapted from the algoritm in Foley and Van-Dam</remarks>
            /// <param name="hsl">The HSL value</param>
            /// <returns>A Color structure containing the equivalent RGB values</returns>

            public static Color HslToRgb(HSL hsl)
            {
                double r = 0, g = 0, b = 0;
                double temp1, temp2;

                if (hsl.L == 0)
                {
                    r = g = b = 0;
                }
                else
                {
                    if (hsl.S == 0)
                    {
                        r = g = b = hsl.L;
                    }
                    else
                    {
                        temp2 = ((hsl.L <= 0.5) ? hsl.L * (1.0 + hsl.S) : hsl.L + hsl.S - (hsl.L * hsl.S));
                        temp1 = 2.0 * hsl.L - temp2;

                        double[] t3 = new double[] { hsl.H + 1.0 / 3.0, hsl.H, hsl.H - 1.0 / 3.0 };
                        double[] clr = new double[] { 0, 0, 0 };
                        for (int i = 0; i < 3; i++)
                        {
                            if (t3[i] < 0)
                                t3[i] += 1.0;
                            if (t3[i] > 1)
                                t3[i] -= 1.0;

                            if (6.0 * t3[i] < 1.0)
                                clr[i] = temp1 + (temp2 - temp1) * t3[i] * 6.0;
                            else if (2.0 * t3[i] < 1.0)
                                clr[i] = temp2;
                            else if (3.0 * t3[i] < 2.0)
                                clr[i] = (temp1 + (temp2 - temp1) * ((2.0 / 3.0) - t3[i]) * 6.0);
                            else
                                clr[i] = temp1;
                        }
                        r = clr[0];
                        g = clr[1];
                        b = clr[2];
                    }
                }

                return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));

            }



            /// <summary>
            /// Converts RGB to HSL
            /// </summary>
            /// <remarks>Takes advantage of whats already built in to .NET by using the Color.GetHue, Color.GetSaturation and Color.GetBrightness methods</remarks>
            /// <param name="baseColor">A Color to convert</param>
            /// <returns>An HSL value</returns>
            internal static HSL RgbToHsl(Color baseColor)
            {
                HSL hsl = new HSL();

                hsl.H = baseColor.GetHue() / 360.0; // we store hue as 0-1 as opposed to 0-360
                hsl.L = baseColor.GetBrightness();
                hsl.S = baseColor.GetSaturation();

                return hsl;
            }
        }

        static public string ColorName2Hex(string colorName)
        {
            string szValue = "000000"; //default

            switch (colorName)
            {
                case "black":
                    szValue = "000000";
                    break;
                case "blue":
                    szValue = "0000FF";
                    break;
                case "cyan":
                    szValue = "00FFFF";
                    break;
                case "darkBlue":
                    szValue = "000080";
                    break;
                case "darkCyan":
                    szValue = "008080";
                    break;
                case "darkGray":
                    szValue = "808080";
                    break;
                case "darkGreen":
                    szValue = "008000";
                    break;
                case "darkMagenta":
                    szValue = "800080";
                    break;
                case "darkRed":
                    szValue = "800000";
                    break;
                case "darkYellow":
                    szValue = "808000";
                    break;
                case "green":
                    szValue = "00FF00";
                    break;
                case "lightGray":
                    szValue = "C0C0C0";
                    break;
                case "magenta":
                    szValue = "FF00FF";
                    break;
                case "red":
                    szValue = "FF0000";
                    break;
                case "white":
                    szValue = "FFFFFF";
                    break;
                case "yellow":
                    szValue = "FFFF00";
                    break;
                case "orange":
                    szValue = "FFA500";
                    break;
                case "pink":
                    szValue = "FFC0CB";
                    break;
            }//end of switch

            return szValue;
        }

        static public string HexColor2Name(string hexColor)
        {
            string szValue = "black"; //default

            switch (hexColor)
            {
                case "000000":
                    szValue = "black";
                    break;
                case "0000FF":
                    szValue = "blue";
                    break;
                case "00FFFF":
                    szValue = "cyan";
                    break;
                case "000080":
                    szValue = "darkBlue";
                    break;
                case "008080":
                    szValue = "darkCyan";
                    break;
                case "808080":
                    szValue = "darkGray";
                    break;
                case "008000":
                    szValue = "darkGreen";
                    break;
                case "800080":
                    szValue = "darkMagenta";
                    break;
                case "800000":
                    szValue = "darkRed";
                    break;
                case "808000":
                    szValue = "darkYellow";
                    break;
                case "00FF00":
                    szValue = "green";
                    break;
                case "C0C0C0":
                    szValue = "lightGray";
                    break;
                case "FF00FF":
                    szValue = "magenta";
                    break;
                case "FF0000":
                    szValue = "red";
                    break;
                case "FFFFFF":
                    szValue = "white";
                    break;
                case "FFFF00":
                    szValue = "yellow";
                    break;
                case "FFA500":
                    szValue = "orange";
                    break;
                case "FFC0CB":
                    szValue = "pink";
                    break;
            }//end of switch

            return szValue;
        }

        static public string ColorNameInteractiveField2Hex(string color)
        {
            string szValue = "000000"; //default
            switch (color)
            {
                case "black":
                    szValue = "000000";
                    break;
                case "blue":
                    szValue = "0000FF";
                    break;
                case "cyan":
                    szValue = "00FFFF";
                    break;
                case "dark_gray":
                case "gray":
                    szValue = "808080";
                    break;
                case "green":
                    szValue = "00FF00";
                    break;
                case "light_gray":
                    szValue = "C0C0C0";
                    break;
                case "magenta":
                    szValue = "FF00FF";
                    break;
                case "orange":
                    szValue = "FFA500";
                    break;
                case "pink":
                    szValue = "FFC0CB";
                    break;
                case "red":
                    szValue = "FF0000";
                    break;
                case "white":
                    szValue = "FFFFFF";
                    break;
                case "yellow":
                    szValue = "FFFF00";
                    break;
                default:
                    break;
            }

            return szValue;
        }

    }
}
