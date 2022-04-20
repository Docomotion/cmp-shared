using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Docomotion.Shared.EngineUtils;

namespace Docomotion.Shared.RIICompiler
{
    public class FFRIIBuilder
    {
        public static string ConvertFFRToFFRII(string ffr, ref string ffr2)
        {
            string ffr2Descriptor = string.Empty;

            XmlDocument ffrDoc = new XmlDocument();
            ffrDoc.LoadXml(ffr);

            XDocument wmlPartsXDoc = GetOutWmlParts(ffrDoc);

            string wmlParts = wmlPartsXDoc.ToString();

            string xslStr = ffrDoc.OuterXml;
            ffr2 = string.Format("{0}{1}", xslStr, wmlParts);

            int xslSize = Encoding.UTF8.GetBytes(xslStr).Length;
            ffr2Descriptor = SetFFRDesc(xslSize);

            return ffr2Descriptor;
        }

        public static void PrepareFFRToFFRII(byte[] ffr, ref byte[] xslPart, ref byte[] wmlPart)
        {
            XmlDocument ffrDoc = new XmlDocument();
            XDocument wmlPartsXDoc = null;
            using (MemoryStream ffrStream = new MemoryStream(ffr))
            {
                ffrDoc.Load(ffrStream);

                wmlPartsXDoc = GetOutWmlParts(ffrDoc);

                SetAllNS(ffrDoc);

                xslPart = Encoding.UTF8.GetBytes(ffrDoc.OuterXml);
            }

            wmlPart = Encoding.UTF8.GetBytes(wmlPartsXDoc.ToString());
        }

        public static void PrepareFFRToFFRIIXml(XmlDocument ffrDoc, ref byte[] wmlPart)
        {
            XDocument wmlPartsXDoc = null;
            wmlPartsXDoc = GetOutWmlParts(ffrDoc);
            SetAllNS(ffrDoc);

            wmlPart = Encoding.UTF8.GetBytes(wmlPartsXDoc.ToString());
        }

        static private void SetAllNS(XmlDocument ffrDoc)

        {
            Dictionary<string, string> allNS = new Dictionary<string, string>();
            allNS.Add("xmlns:a" ,PrefixNS.A);
            allNS.Add("xmlns:cp", PrefixNS.CP);
            allNS.Add("xmlns:dc" ,PrefixNS.DC);
            allNS.Add("xmlns:m" ,PrefixNS.M);
                allNS.Add("xmlns:mc" ,PrefixNS.MC);
                allNS.Add("xmlns:o" ,PrefixNS.O);
                allNS.Add("xmlns:pic" ,PrefixNS.PIC);
            allNS.Add("xmlns:pkg" ,PrefixNS.PKG);
            allNS.Add("xmlns:r" ,PrefixNS.R);
            allNS.Add("xmlns:v" ,PrefixNS.V);
            allNS.Add("xmlns:ve" ,PrefixNS.VE);
            allNS.Add("xmlns:w" ,PrefixNS.W);
            allNS.Add("xmlns:w10" ,PrefixNS.W10);
            allNS.Add("xmlns:w14" ,PrefixNS.W14);
            allNS.Add("xmlns:w15" ,PrefixNS.W15);
            allNS.Add("xmlns:wne" ,PrefixNS.WNE);
            allNS.Add("xmlns:wp" ,PrefixNS.WP);
            allNS.Add("xmlns:wp14" ,PrefixNS.WP14);
            allNS.Add("xmlns:wpc" ,PrefixNS.WPC);
            allNS.Add("xmlns:wpg" ,PrefixNS.WPG);
                allNS.Add("xmlns:wpi" ,PrefixNS.WPI);
            allNS.Add("xmlns:wps" ,PrefixNS.WPS);
            allNS.Add("xmlns:xsi" ,PrefixNS.XSI);

            foreach(KeyValuePair<string,string> pair in allNS)
            {
                WMLWriter.AddAttribute(ffrDoc.DocumentElement, pair.Key, pair.Value);

                //ffrDoc.DocumentElement.Attributes.Append( .SetNamedItem()
            }
        }

        static private XDocument GetOutWmlParts(XmlDocument ffrDoc)
        {
            //styles
            string styleStr = GetPartsXml("//pkg:package/pkg:part[@pkg:name='/word/styles.xml']", ffrDoc);
            //stylesWithEffects
            string styleWhithEfectStr = GetPartsXml("//pkg:package/pkg:part[@pkg:name='/word/stylesWithEffects.xml']", ffrDoc);
            //numbering
            string numberingStr = GetPartsXml("//pkg:package/pkg:part[@pkg:name='/word/numbering.xml']", ffrDoc);
            //settings
            string settingsStr = GetPartsXml("//pkg:package/pkg:part[@pkg:name='/word/settings.xml']", ffrDoc);
            //theme
            string themeStr = GetPartsXml("//pkg:package/pkg:part[@pkg:name='/word/theme/theme1.xml']", ffrDoc);

            //images
            List<string> imageParts = new List<string>();
            GetImages(ffrDoc, imageParts);

            //webSettings
            string webSettingsStr = GetPartsXml("//pkg:package/pkg:part[@pkg:name='/word/webSettings.xml']", ffrDoc);
            //docProps/app
            string docPropsAppStr = GetPartsXml("//pkg:package/pkg:part[@pkg:name='/docProps/app.xml']", ffrDoc);
            //docProps/core
            string docPropsCoreStr = GetPartsXml("//pkg:package/pkg:part[@pkg:name='/docProps/core.xml']", ffrDoc);
            //word/fontTable
            string fontTableStr = GetPartsXml("//pkg:package/pkg:part[@pkg:name='/word/fontTable.xml']", ffrDoc);

            XDocument wmlPartsXDoc = new XDocument(new XElement("wml"));
            if (!string.IsNullOrEmpty(styleStr)) wmlPartsXDoc.Root.Add(XElement.Parse(styleStr));
            if (!string.IsNullOrEmpty(styleWhithEfectStr)) wmlPartsXDoc.Root.Add(XElement.Parse(styleWhithEfectStr));
            if (!string.IsNullOrEmpty(numberingStr)) wmlPartsXDoc.Root.Add(XElement.Parse(numberingStr));
            if (!string.IsNullOrEmpty(settingsStr)) wmlPartsXDoc.Root.Add(XElement.Parse(settingsStr));
            if (!string.IsNullOrEmpty(themeStr)) wmlPartsXDoc.Root.Add(XElement.Parse(themeStr));
            if (!string.IsNullOrEmpty(webSettingsStr)) wmlPartsXDoc.Root.Add(XElement.Parse(webSettingsStr));
            if (!string.IsNullOrEmpty(docPropsAppStr)) wmlPartsXDoc.Root.Add(XElement.Parse(docPropsAppStr));
            if (!string.IsNullOrEmpty(docPropsCoreStr)) wmlPartsXDoc.Root.Add(XElement.Parse(docPropsCoreStr));
            if (!string.IsNullOrEmpty(fontTableStr)) wmlPartsXDoc.Root.Add(XElement.Parse(fontTableStr));

            foreach (string imagePart in imageParts)
            {
                if (!string.IsNullOrEmpty(imagePart)) wmlPartsXDoc.Root.Add(XElement.Parse(imagePart));
            }

            return wmlPartsXDoc;
        }

        static public string SetFFRDesc(int xslSize)
        {
            return string.Format("<ffr_desc type=\"2\" xslSize=\"{0}\" />", xslSize);
        }

        static private string GetPartsXml(string xpath, XmlDocument xslDoc)
        {
            XmlNode partNode = xslDoc.SelectSingleNode(xpath, WMLReader.MngrWml);
            string partStr = string.Empty;
            if (partNode != null)
            {
                partStr = partNode.OuterXml;
                partNode.ParentNode.RemoveChild(partNode);
            }

            return partStr;
        }

        static private void GetImages(XmlDocument xslDoc, List<string> imageParts)
        {
            try
            {
                XmlNodeList imageNodes = xslDoc.DocumentElement.SelectNodes("//pkg:part[starts-with(@pkg:contentType,'image')]", WMLReader.MngrWml);

                foreach (XmlNode imageNode in imageNodes)
                {
                    XmlNode xslNode = imageNode.SelectSingleNode("./descendant::*[starts-with(name(),'xsl')]", WMLReader.MngrWml);
                    if (xslNode == null)
                    {
                        imageParts.Add(imageNode.OuterXml);

                        imageNode.ParentNode.RemoveChild(imageNode);
                    }
                }
            }
            catch { }
        }
    }
}
