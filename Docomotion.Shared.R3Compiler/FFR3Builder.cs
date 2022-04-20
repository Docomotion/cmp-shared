using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Docomotion.Shared.EngineUtils;
using Docomotion.Shared.RIICompiler;

namespace Docomotion.Shared.R3Compiler
{
    public class FFR3Builder
    {
        class FFRPart
        {
            public FFRPart(int sPos, bool isXsl)
            {
                SPos = sPos;
                IsXSL = isXsl;
            }
            public int SPos = 0;
            public int EPos = 0;
            public int Length { get { return (EPos - SPos + 1); } }
            public bool IsXSL = true;
        }

        #region FFR to FFR3
        // is used in Packager
        public static string ConvertFFRToFFR3 (string ffr, ref string ffr3)
        {
            string ffr2Descriptor = null;

            byte[] ffrBytes = Encoding.UTF8.GetBytes(ffr);
            byte[] xslPart3 = null;
            byte[] wmlPart3 = null;
            byte[] wmlPart = null;
            ffr2Descriptor = ConvertFFRToFFR3(ffrBytes, ref xslPart3, ref wmlPart3, ref wmlPart);

            byte[] fullFFR3 = new byte[xslPart3.Length + wmlPart3.Length + wmlPart.Length];
            Array.Copy(xslPart3, fullFFR3, xslPart3.Length);
            Array.Copy(wmlPart3, 0, fullFFR3, xslPart3.Length, wmlPart3.Length);
            Array.Copy(wmlPart, 0, fullFFR3, xslPart3.Length + wmlPart3.Length, wmlPart.Length);

            ffr3 = Encoding.UTF8.GetString(fullFFR3);

            return ffr2Descriptor;
        }

        // is used in RT
        public static string ConvertFFRToFFR3(byte[] ffr, ref byte[] xslPart3, ref byte[] wmlPart3, ref byte[] wmlPart, bool isConvertRT = false)
        {
            string ffr2Descriptor = null;

            XmlDocument ffrDoc = new XmlDocument();
            using (MemoryStream ffrStream = new MemoryStream(ffr))
            {
                ffrDoc.Load(ffrStream);
            }

            FFRIIBuilder.PrepareFFRToFFRIIXml(ffrDoc, ref wmlPart);

            SeparateFFRIIToFFR3(ffrDoc, ref xslPart3, ref wmlPart3, isConvertRT);

            int xslSize = xslPart3.Length;
            int wml3Size = wmlPart3.Length;

            return ffr2Descriptor = $"<ffr_desc type=\"3\" xslSize=\"{xslSize}\" wml3Size=\"{wml3Size}\" />";
        }

        private static void SeparateFFRIIToFFR3(XmlDocument ffrDoc, ref byte[] xslPart3, ref byte[] wmlPart3, bool isConvertRT)
        {
            XmlNodeList xslTextList = ffrDoc.DocumentElement.SelectNodes("//xsl:text", WMLReader.MngrWml);
            foreach (XmlNode xslText in xslTextList)
            {
                if (xslText.ParentNode != null)
                {
                    XmlText textNode = ffrDoc.CreateTextNode(xslText.InnerText);
                    xslText.ParentNode.ReplaceChild(textNode, xslText);
                }
            }

            XmlNodeList pureWmlNodes = ffrDoc.DocumentElement.SelectNodes("//ancestor-or-self::*[namespace-uri()!='http://www.w3.org/1999/XSL/Transform'" +
                                                                          " and not(descendant::*[namespace-uri()='http://www.w3.org/1999/XSL/Transform'])" +
                                                                          " and parent::*[descendant-or-self::*[namespace-uri()='http://www.w3.org/1999/XSL/Transform']]]");

            int wmlId = 1;
            int wmlPartOffset = 0;

            using (MemoryStream msWmlPart3 = new MemoryStream())
            {
                using (MemoryStream msWmlDescriptor = new MemoryStream())
                {
                    foreach (XmlNode pureWmlNode in pureWmlNodes)
                    {
                        //check if this is NOT chart:<xsl:variable name="chart-properties">
                        XmlNode xslAncestor = pureWmlNode.SelectSingleNode("ancestor::xsl:variable[@name='chart-properties']", WMLReader.MngrWml);
                        if (xslAncestor != null) continue;

                        if (isConvertRT)
                        {
                            //dont replace the attribute properties of interactive tag because defualt value '{ROOT\branch\....}'
                            if (pureWmlNode.Name.Equals("w:attr"))
                            {
                                string attrName = WMLReader.ST_StringValueAsString(pureWmlNode, "w:name");
                                if (attrName == "tagProperties")
                                {
                                    string propValAttr = WMLReader.ST_StringValueAsString(pureWmlNode, "w:val");
                                    if (!string.IsNullOrWhiteSpace(propValAttr))
                                        if (propValAttr.Contains("Interactive"))
                                            continue;
                                }
                            }
                        }

                        string aliasWml = $"ml_{wmlId}";
                        XmlElement mlNode = ffrDoc.CreateElement(aliasWml);
                        pureWmlNode.ParentNode.ReplaceChild(mlNode, pureWmlNode);

                        //WML descriptor
                        byte[] pureWmlBytes = Encoding.UTF8.GetBytes(pureWmlNode.OuterXml);
                        string wmlDesc = $"_{wmlId}_{wmlPartOffset}_{pureWmlBytes.Length}";
                        byte[] bWmlDesc = Encoding.UTF8.GetBytes(wmlDesc);
                        msWmlDescriptor.Write(bWmlDesc, 0, bWmlDesc.Length);
                        wmlPartOffset += pureWmlBytes.Length;

                        //wml content
                        msWmlPart3.Write(pureWmlBytes, 0, pureWmlBytes.Length);

                        wmlId++;
                    }

                    //count of items in descriptor
                    int wmlPartsCount = pureWmlNodes.Count;
                    byte[] wmlDescriptor = null;

                    int descLength = (int)msWmlDescriptor.Length + wmlPartsCount.ToString().Length + 1;
                    byte startByte = (byte)(65 + descLength.ToString().Length); //start from A. A=>1, B=>2...

                    byte[] bPrefixWmlDescriptor = Encoding.UTF8.GetBytes($"{(char)startByte}{descLength}_{wmlPartsCount}");
                    wmlDescriptor = new byte[bPrefixWmlDescriptor.Length + msWmlDescriptor.Length];

                    byte[] tmpBytes = msWmlDescriptor.ToArray();
                    Array.Copy(bPrefixWmlDescriptor, wmlDescriptor, bPrefixWmlDescriptor.Length);
                    Array.Copy(tmpBytes, 0, wmlDescriptor, bPrefixWmlDescriptor.Length, tmpBytes.Length);

                    wmlPart3 = new byte[wmlDescriptor.Length + msWmlPart3.Length];
                    tmpBytes = msWmlPart3.ToArray();
                    Array.Copy(wmlDescriptor, wmlPart3, wmlDescriptor.Length);
                    Array.Copy(tmpBytes, 0, wmlPart3, wmlDescriptor.Length, tmpBytes.Length);
                }
            }

            xslPart3 = Encoding.UTF8.GetBytes(ffrDoc.OuterXml);
        }
        #endregion

        #region FFR2 to FFR3

        //used in RT
        public static string SeparateFFRIIToFFR3(byte[] xslFormContent, ref byte[] xslPart3, ref byte[] wmlPart3, bool isConvertRT = false)
        {
            string ffr2Descriptor = null;
            
            XmlDocument ffrDoc = new XmlDocument();
            using (MemoryStream ffrStream = new MemoryStream(xslFormContent))
            {
                ffrDoc.Load(ffrStream);
            }

            SeparateFFRIIToFFR3(ffrDoc, ref xslPart3, ref wmlPart3, isConvertRT);

            int xslSize = xslPart3.Length;
            int wml3Size = wmlPart3.Length;

            return ffr2Descriptor = $"<ffr_desc type=\"3\" xslSize=\"{xslSize}\" wml3Size=\"{wml3Size}\" />";
        }

        #endregion

        #region return to WML
        public static void FFR3FormToWml(byte[] ffr3Form, byte[] ffr3WmlParts, ref byte[] wmlForm)
        {
            List<FFRPart> ffr3FormParts = new List<FFRPart>();
            ParseFFR3Form(ffr3Form, ffr3FormParts);

            FillForm3Wml(ffr3Form, ffr3WmlParts, ffr3FormParts, ref wmlForm);
        }

        private static void ParseFFR3Form(byte[] xslPart, List<FFRPart> ffrParts)
        {
            //skip xml declaration
            int i = 0;
            if (xslPart[i] == '<' && xslPart[i + 1] == '?')
            {
                i = 2;
                while (xslPart[i++] != '?' && xslPart[++i] != '>') ;
                FFRPart xmlDeclPart = new FFRPart(0, false);
                xmlDeclPart.EPos = i;
                ffrParts.Add(xmlDeclPart);
            }

            FFRPart curFfrPart = null;
            bool xslFound = false;
            for (++i; i < xslPart.Length; i++)
            {
                if (xslPart[i] == '<')
                {
                    xslFound = CheckWMLAlias(i, xslPart);
                    if (xslFound)
                    {
                        if (curFfrPart == null)
                        {
                            curFfrPart = new FFRPart(i, true);
                        }
                        else
                        {
                            if (!curFfrPart.IsXSL)
                            {
                                curFfrPart = ChangeFFRPart(ffrParts, curFfrPart, i, true);
                            }
                        }

                        for (++i; i < xslPart.Length; i++)
                        {
                            if (xslPart[i] == '>')
                            {
                                curFfrPart.EPos = i;
                                ffrParts.Add(curFfrPart);

                                curFfrPart = null;
                                xslFound = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        //wml part
                        if (curFfrPart == null)
                        {
                            curFfrPart = new FFRPart(i, false);
                        }
                        else
                        {
                            if (curFfrPart.IsXSL)
                            {
                                curFfrPart = ChangeFFRPart(ffrParts, curFfrPart, i, false);
                            }
                        }
                    }
                }
                else
                {
                    if (!xslFound)
                    {
                        if (curFfrPart == null)
                        {
                            curFfrPart = new FFRPart(i, false);
                        }
                        else
                        {
                            if (curFfrPart.IsXSL)
                            {
                                curFfrPart = ChangeFFRPart(ffrParts, curFfrPart, i, false);
                            }
                        }
                    }
                }
            }

            if (curFfrPart != null)
            {
                curFfrPart.EPos = xslPart.Length - 1;
                ffrParts.Add(curFfrPart);
            }
        }

        private static bool CheckWMLAlias(int ind, byte[] xslPart)
        {
            return (xslPart[ind + 1] == 'm' &&
                    xslPart[ind + 2] == 'l' &&
                    xslPart[ind + 3] == '_');
        }

        private static FFRPart ChangeFFRPart(List<FFRPart> ffrParts, FFRPart curFfrPart, int startNewPos, bool isXsl)
        {
            curFfrPart.EPos = startNewPos - 1;
            ffrParts.Add(curFfrPart);
            FFRPart newFfrPart = new FFRPart(startNewPos, isXsl);

            return newFfrPart;
        }

        private static void FillForm3Wml(byte[] ffr3Form, byte[] ffr3WmlParts, List<FFRPart> ffr3FormParts, ref byte[] wmlForm)
        {
            Dictionary<string, Tuple<int, int>> wmlDescriptor = new Dictionary<string, Tuple<int, int>>();
            int startWml = GetWmlDescriptor(wmlDescriptor, ffr3WmlParts);

            using (MemoryStream msWmlForm = new MemoryStream())
            {
                for (int p = 0; p < ffr3FormParts.Count; p++)
                {
                    if (ffr3FormParts[p].IsXSL)
                    {
                        byte[] orgWmlPart = GetOrgWml(ffr3Form, ffr3WmlParts, ffr3FormParts[p], wmlDescriptor, startWml);
                        if (orgWmlPart != null) msWmlForm.Write(orgWmlPart, 0, orgWmlPart.Length);
                    }
                    else
                    {
                        msWmlForm.Write(ffr3Form, ffr3FormParts[p].SPos, ffr3FormParts[p].Length);
                    }
                }

                wmlForm = msWmlForm.ToArray();
            }
        }

        private static int GetWmlDescriptor(Dictionary<string, Tuple<int, int>> wmlDescriptor, byte[] ffr3WmlParts)
        {
            if (ffr3WmlParts == null || ffr3WmlParts.Length == 0)
            {
                return 0;
            }

            //get descriptor length by start byte
            int lenOfDescNumber = ffr3WmlParts[0] - 'A';
            byte[] descLengthBytes = new byte[lenOfDescNumber];
            Array.Copy(ffr3WmlParts, 1, descLengthBytes, 0, lenOfDescNumber);
            string descLengthStr = Encoding.UTF8.GetString(descLengthBytes);
            int descLength = 0;
            int.TryParse(descLengthStr, out descLength);

            byte[] descTableBytes = new byte[descLength];
            Array.Copy(ffr3WmlParts, lenOfDescNumber + 1, descTableBytes, 0, descLength);

            string descTableStr = Encoding.UTF8.GetString(descTableBytes);

            char[] sep = { '_' };
            string[] descTableItems = descTableStr.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            string descTableItemsNumber = descTableItems[0];

            Tuple<int, int> descTableItem = null;
            string item1 = null; //ID
            string item2 = null;//offset


            for (int i = 1; i < descTableItems.Count(); i++)
            {
                if (i % 3 == 1)
                {
                    item1 = descTableItems[i];
                }
                else if (i % 3 == 2)
                {
                    item2 = descTableItems[i];
                }
                else if (i % 3 == 0)
                {
                    int offset = 0; int len = 0;
                    int.TryParse(item2, out offset);
                    int.TryParse(descTableItems[i], out len);//length
                    descTableItem = new Tuple<int, int>(offset, len);

                    wmlDescriptor.Add(item1, descTableItem);
                }
            }

            return descLength + lenOfDescNumber + 1;
        }

        private static byte[] GetOrgWml(byte[] ffr3Form, byte[] ffr3WmlParts, FFRPart wmlPart, Dictionary<string, Tuple<int, int>> wmlDescriptor, int startWml)
        {
            byte[] orgWml = null;
            int offsetOrgWml = startWml;

            byte[] wmlPartTag = new byte[wmlPart.Length];
            Array.Copy(ffr3Form, wmlPart.SPos, wmlPartTag, 0, wmlPart.Length);
            string wmlPartTagStr = Encoding.UTF8.GetString(wmlPartTag);
            int sPos = wmlPartTagStr.IndexOf('_') + 1;
            int ePos = sPos;
            for (; ePos < wmlPartTagStr.Length; ePos++)
            {
                if (!char.IsDigit(wmlPartTagStr[ePos]))
                    break;
            }

            string id = null;
            if (sPos != 1 && ePos != -1) id = wmlPartTagStr.Substring(sPos, ePos - sPos);

            if (id != null)
            {
                Tuple<int, int> wmlDesc = null;
                if (wmlDescriptor.TryGetValue(id, out wmlDesc))
                {
                    orgWml = new byte[wmlDesc.Item2];
                    Array.Copy(ffr3WmlParts, offsetOrgWml + wmlDesc.Item1, orgWml, 0, wmlDesc.Item2);

                }
            }

            return orgWml;
        }

        #endregion


        //old version not in use >>>
        //const string ML_ROOT_END = "</ml_root>";
        //const string PKG_START = "<pkg:package";
        //const string PKG_END = "</pkg:package>";
        //const string XSL_ATTR_START = "<xsl:attribute";
        //const string ML_ROOT_START = "<ml_root>";

        //        public static string SeparateFFRIIToFFr3(byte[] xslPart2, ref byte[] xslPart3, ref byte[] wmlPart3)
        //        { 
        //            string ffr2Descriptor = string.Empty;

        //            List<FFRPart> ffrParts = new List<FFRPart>();
        //            ParseFFR(xslPart2, ffrParts);

        //            CheckAttributeXsltScript(xslPart2, ffrParts);

        //            RebuildXslPart(xslPart2, ffrParts, ref xslPart3, ref wmlPart3);

        //            int xslSize = xslPart3.Length;
        //            int wml3Size = wmlPart3.Length;
        //            ffr2Descriptor = $"<ffr_desc type=\"3\" xslSize=\"{xslSize}\" wml3Size=\"{wml3Size}\" />";

        //            return ffr2Descriptor;
        //        }

        //        public static string SeparateFFRToFFR3(byte[] ffr, ref byte[] xslPart3, ref byte[] wmlPart3, ref byte[] wmlPart)
        //        {
        //            string ffr2Descriptor = string.Empty;
        //            byte[] xslPart = null;

        //            Stopwatch sw = new Stopwatch();
        //            sw.Start();
        //            List<FFRPart> FFRParts = new List<FFRPart>();
        //            FFRIIBuilder.PrepareFFRToFFRII(ffr, ref xslPart, ref wmlPart);
        //            sw.Stop();

        //            Stopwatch sw2 = new Stopwatch();
        //            sw2.Start();
        //            List<FFRPart> ffrParts = new List<FFRPart>();
        //            ParseFFR(xslPart, ffrParts);
        //            sw2.Stop();

        //            CheckAttributeXsltScript(xslPart, ffrParts);

        //            Stopwatch sw3 = new Stopwatch();
        //            sw3.Start();
        //            RebuildXslPart(xslPart, ffrParts, ref xslPart3, ref wmlPart3);
        //            sw3.Stop();

        //            int xslSize = xslPart3.Length;
        //            int wml3Size = wmlPart3.Length;
        //            ffr2Descriptor = $"<ffr_desc type=\"3\" xslSize=\"{xslSize}\" wml3Size=\"{wml3Size}\" />";

        //            return ffr2Descriptor;
        //        }

        //        public static string ConvertFFRToFFR3(byte[] ffr, ref byte[] ffr3)
        //        {
        //            string ffr2Descriptor = string.Empty;
        //            byte[] wmlPart = null;
        //            byte[] xslPart3 = null;
        //            byte[] wmlPart3 = null;

        //            ffr2Descriptor = SeparateFFRToFFR3(ffr, ref xslPart3, ref wmlPart3, ref wmlPart);

        //            ffr3 = new byte[xslPart3.Length + wmlPart3.Length + wmlPart.Length];

        //            Stopwatch sw4 = new Stopwatch();
        //            sw4.Start();
        //            Array.Copy(xslPart3, ffr3, xslPart3.Length);
        //            Array.Copy(wmlPart3, 0, ffr3, xslPart3.Length, wmlPart3.Length);
        //            Array.Copy(wmlPart, 0, ffr3, wmlPart3.Length + xslPart3.Length, wmlPart.Length);
        //            sw4.Stop();

        //        private static bool CheckXslNs(int ind, byte[] xslPart)
        //        {
        //            return (xslPart[ind + 1] == 'x' &&
        //                    xslPart[ind + 2] == 's' &&
        //                    xslPart[ind + 3] == 'l' &&
        //                    xslPart[ind + 4] == ':');
        //        }

        //        private static void ParseFFR(byte[] xslPart, List<FFRPart> ffrParts)
        //        {
        //            //skip xml declaration
        //            int i = 0;
        //            if (xslPart[i] == '<' && xslPart[i + 1] == '?')
        //            {
        //                i = 2;
        //                while (xslPart[i++] != '?' && xslPart[++i] != '>');
        //                FFRPart xmlDeclPart = new FFRPart(0, true);
        //                xmlDeclPart.EPos = i;
        //                ffrParts.Add(xmlDeclPart);
        //            }

        //            FFRPart curFfrPart = null;
        //            for (++i; i < xslPart.Length; i++)
        //            {
        //                if (xslPart[i] == '<')
        //                {
        //                    bool xslFound = CheckXslNs(i, xslPart);
        //                    if (xslFound)
        //                    {
        //                        //xsl part
        //                        if (curFfrPart == null)
        //                        {
        //                            curFfrPart = new FFRPart(i, true);
        //                        }
        //                        else
        //                        {
        //                            if(!curFfrPart.IsXSL)
        //                            {
        //                                curFfrPart = ChangeFFRPart(ffrParts, curFfrPart, i, true);
        //                            }
        //                        }
        //                    }
        //                    else if(xslPart[i + 1] == '/')
        //                    {
        //                        if (curFfrPart != null)
        //                        {
        //                            xslFound = CheckXslNs(i + 1, xslPart);

        //                            if (curFfrPart.IsXSL)
        //                            {
        //                                if (!xslFound)
        //                                {
        //                                    curFfrPart = ChangeFFRPart(ffrParts, curFfrPart, i, false);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                if(xslFound)
        //                                {
        //                                    curFfrPart = ChangeFFRPart(ffrParts, curFfrPart, i, true);
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //wml part
        //                        if (curFfrPart == null)
        //                        {
        //                            curFfrPart = new FFRPart(i, false);
        //                        }
        //                        else
        //                        {
        //                            if (curFfrPart.IsXSL)
        //                            {
        //                                curFfrPart = ChangeFFRPart(ffrParts, curFfrPart, i, false);
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            if(curFfrPart != null)
        //            {
        //                curFfrPart.EPos = xslPart.Length - 1;
        //                ffrParts.Add(curFfrPart);
        //            }
        //        }

        //        private static bool CompareWithSequence(byte[] xslPart, FFRPart wmlPart, string toCompare, bool fromEnd)
        //        {
        //            //check from end
        //            bool res = false;
        //            byte[] toCompareBytes = Encoding.UTF8.GetBytes(toCompare);

        //            if (wmlPart.Length >= toCompareBytes.Length)
        //            {
        //                byte[] sequenceOfPart = new byte[toCompareBytes.Length];
        //                if (fromEnd)
        //                    Array.Copy(xslPart, wmlPart.EPos - toCompareBytes.Length + 1, sequenceOfPart, 0, toCompareBytes.Length);
        //                else
        //                    Array.Copy(xslPart, wmlPart.SPos, sequenceOfPart, 0, toCompareBytes.Length);
        //#if DEB_UG
        //                string s = Encoding.UTF8.GetString(sequenceOfPart);
        //                Debug.WriteLine(s);
        //#endif
        //                res = toCompareBytes.SequenceEqual(sequenceOfPart);
        //            }

        //            return res;
        //        }

        //        private static void CheckAttributeXsltScript(byte[] xslPart, List<FFRPart> ffrParts)
        //        {
        //            for (int i = 2; i < ffrParts.Count - 1; i++)
        //            {
        //                if(ffrParts[i].IsXSL)
        //                {
        //                    if(CompareWithSequence(xslPart, ffrParts[i], XSL_ATTR_START, false))
        //                    {
        //                        if(!ffrParts[i - 1].IsXSL && !ffrParts[i + 1].IsXSL)
        //                        {
        //                            ffrParts[i - 1].LeaveWml = true;
        //                            ffrParts[i + 1].LeaveWml = true;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        private static void RebuildXslPart(byte[] xslPart, List<FFRPart> ffrParts,ref byte[] xslPart3, ref byte[] wmlPart3)
        //        {
        //            byte[] wmlDescriptor = null;
        //            int wmlPartsCount = 0;
        //            int wmlPartOffset = 0;
        //            using (MemoryStream msXslPart3 = new MemoryStream())
        //            {
        //                using (MemoryStream msWmlPart3 = new MemoryStream())
        //                {
        //                    using (MemoryStream msWmlDescriptor = new MemoryStream())
        //                    {
        //                        bool searchStartOfPackage = true;
        //                        bool searchEndOfPackage = false;
        //                        int wmlId = 0;
        //                        msXslPart3.Write(xslPart, ffrParts[0].SPos, ffrParts[0].Length);
        //                        msXslPart3.Write(xslPart, ffrParts[1].SPos, ffrParts[1].Length);

        //                        for (int i = 2; i < ffrParts.Count - 1; i++)
        //                        {
        //                            if (ffrParts[i].IsXSL)
        //                            {
        //                                msXslPart3.Write(xslPart, ffrParts[i].SPos, ffrParts[i].Length);
        //                            }
        //                            else
        //                            {
        //                                if(searchStartOfPackage)
        //                                {
        //                                    searchStartOfPackage = !CompareWithSequence(xslPart, ffrParts[i], PKG_START, false);
        //                                    if (!searchStartOfPackage)
        //                                    {
        //                                        byte[] mlRootStart = Encoding.UTF8.GetBytes(ML_ROOT_START);
        //                                        msXslPart3.Write(mlRootStart, 0, mlRootStart.Length);

        //                                        searchEndOfPackage = true;
        //                                    }
        //                                }

        //                                if (ffrParts[i].LeaveWml)
        //                                {
        //                                    msXslPart3.Write(xslPart, ffrParts[i].SPos, ffrParts[i].Length);
        //                                    continue;
        //                                }

        //                                wmlPartsCount++;
        //                                wmlId++;
        //                                string aliasWml = $"<ml_{wmlId}/>";
        //                                byte[] bAliasWml = Encoding.UTF8.GetBytes(aliasWml);
        //                                msXslPart3.Write(bAliasWml, 0, bAliasWml.Length);

        //                                //WML descriptor
        //                                string wmlDesc = $"_{wmlId}_{wmlPartOffset}_{ffrParts[i].Length}";
        //                                byte[] bWmlDesc = Encoding.UTF8.GetBytes(wmlDesc);
        //                                msWmlDescriptor.Write(bWmlDesc, 0, bWmlDesc.Length);
        //                                wmlPartOffset += ffrParts[i].Length;

        //                                //wml content
        //                                msWmlPart3.Write(xslPart, ffrParts[i].SPos, ffrParts[i].Length);

        //                                if (searchEndOfPackage)
        //                                {
        //                                    searchEndOfPackage = !CompareWithSequence(xslPart, ffrParts[i], PKG_END, true);
        //#if DEB_UG
        //                                    byte[] tmp = new byte[ffrParts[i].Length];
        //                                    Array.Copy(xslPart, ffrParts[i].SPos,tmp,0 ,ffrParts[i].Length);
        //                                    string ss = Encoding.UTF8.GetString(tmp);
        //#endif
        //                                    if (!searchEndOfPackage)
        //                                    {
        //                                        byte[] mlRootEnd = Encoding.UTF8.GetBytes(ML_ROOT_END);
        //                                        msXslPart3.Write(mlRootEnd, 0, mlRootEnd.Length);
        //                                    }
        //                                }
        //                            }
        //                        }

        //                        //count of items in descriptor
        //                        int descLength = (int)msWmlDescriptor.Length + wmlPartsCount.ToString().Length + 1;
        //                        byte startByte = (byte)(65 + descLength.ToString().Length); //start from A. A=>1, B=>2...

        //                        byte[] bPrefixWmlDescriptor = Encoding.UTF8.GetBytes($"{(char)startByte}{descLength}_{wmlPartsCount}");
        //                        wmlDescriptor = new byte[bPrefixWmlDescriptor.Length + msWmlDescriptor.Length];

        //                        byte[] tmpBytes = msWmlDescriptor.ToArray();
        //                        Array.Copy(bPrefixWmlDescriptor, wmlDescriptor, bPrefixWmlDescriptor.Length);
        //                        Array.Copy(tmpBytes, 0, wmlDescriptor, bPrefixWmlDescriptor.Length, tmpBytes.Length);

        //                        wmlPart3 = new byte[wmlDescriptor.Length + msWmlPart3.Length];
        //                        tmpBytes = msWmlPart3.ToArray();
        //                        Array.Copy(wmlDescriptor, wmlPart3, wmlDescriptor.Length);
        //                        Array.Copy(tmpBytes, 0, wmlPart3, wmlDescriptor.Length, tmpBytes.Length);
        //                    }

        //                    msXslPart3.Write(xslPart, ffrParts[ffrParts.Count - 1].SPos, ffrParts[ffrParts.Count - 1].Length);
        //                }

        //                xslPart3 = msXslPart3.ToArray();

        //            }


        //        }
        //<<<

    }
}

