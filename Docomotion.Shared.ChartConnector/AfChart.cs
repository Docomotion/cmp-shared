using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI.DataVisualization.Charting;
using System.Xml;
using System.Xml.XPath;
using Docomotion.Shared.ComDef;
using Docomotion.Shared.Logger;

namespace Docomotion.Shared.ChartConnector
{
    public class AfChart
    {
        private const int NO_ERRORS = 0;

        private const int ERROR_COMMON = 3; //common error
        private const int ERROR_BUILD_CHART = 4;//XML of FDC is invalid according to "C# chart control"
        private const int ERROR_VALUE_IS_NOT_DOUBLE = 5; //in case graph is not fixed and data type is not string/datetime
        private const int ERROR_VALUE_IS_NOT_DATE_AND_TIME = 6;//in case graph is not fixed and data type is datetime

        private const string XPOINT_LABEL = "AxisLabel";
        private const string XPOINT_VALUE = "XValue";
        private const string XPOINT_LEGEND = "LegendText";

        private const string CHART_TYPE_PIE = "pie";
        private const string CHART_TYPE_STACKED_COLUMN = "StackedColumn";

        private const string CHART_AREA_NAME = "ChartArea";
        private const string BORDER_COLOR = "black";
        private const int FULL_SIZE = 100;
        private const double ADDITION_KOEFFITSIEN = 4.25;

        private readonly int[] m_XForPreviwe = { 1, 2, 5, 10 };
        private readonly int[] m_YForPreviwe = { 10, 20, 30, 40 };

        private string m_DateFormatX = string.Empty;
        private string m_DateFormatY = string.Empty;

        private float m_WidthPx = 0;
        private float m_HeightPx = 0;

        private float m_Resolution = FreeFormDefinitions.MSWORD_DEFAULT_RESOLUTION;

        string m_ValueAddendum = string.Empty;

        string m_PredefinedType = FreeFormDefinitions.FDC.Enums.FDCPredefinedChart.None.ToString();

        private delegate int CheckData(ref string value, string dateFormat);

        static public string GetChartImage(string xmlChartString, int widthPx, int heightPx)
        {
            string imageBase64 = string.Empty;

            FFLog4net.Instance.Debug("GetChartImage Start.");

            Chart chart1 = new Chart();

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(xmlChartString);
            writer.Flush();
            stream.Position = 0;

            MemoryStream imageStream = new MemoryStream();
            try
            {
                FFLog4net.Instance.Debug("chart1.Serializer.");
                chart1.Serializer.Load(stream);

                if (widthPx > 0 && heightPx > 0)
                {
                    using (Bitmap bmp = new Bitmap(widthPx, heightPx))
                    {
                        bmp.SetResolution(FreeFormDefinitions.MSWORD_DEFAULT_RESOLUTION, FreeFormDefinitions.MSWORD_DEFAULT_RESOLUTION);

                        bool drawWithoutDimension = false;
                        try
                        {
                            // THIS WILL NOT WORK IN THE WEB VERSION. USE THE Height & Width PROPERTIES INSTEAD.
                            // Also - XML string cannot contain a Size for the chart, it is unrecognizable in the Web version.
                            //chart1.DrawToBitmap(bmp, new Rectangle(0, 0, widthPx, heightPx));
                            //bmp.Save(imageStream, ImageFormat.Png);

                            chart1.Width = widthPx;
                            chart1.Height = heightPx;

                            FFLog4net.Instance.Debug("GetChartImage:Bitmap.Save.");
                            chart1.SaveImage(imageStream, ChartImageFormat.Png);

                        }
                        catch
                        {
                            drawWithoutDimension = true;
                        }

                        if (drawWithoutDimension)
                        {
                            FFLog4net.Instance.Debug("GetChartImage:Bitmap:drawWithoutDimension=True.");
                            chart1.SaveImage(imageStream, ChartImageFormat.Png);
                        }
                    }
                }
                else
                {
                    FFLog4net.Instance.Debug("GetChartImage:Bitmap:WH=0.");
                    chart1.SaveImage(imageStream, ChartImageFormat.Png);
                }

                if (imageStream != null)
                    imageBase64 = Convert.ToBase64String(imageStream.ToArray());
            }
            catch (Exception ex)
            {
                FFLog4net.Instance.Warn(string.Format("GetChartImage:{0};Error:{1}", ex.Message, ERROR_BUILD_CHART));

                throw ex;
            }
            finally
            {
                FFLog4net.Instance.Debug("GetChartImage Fihish.");
                chart1.Dispose();
                chart1 = null;

                if (stream != null) stream.Dispose();
                if (imageStream != null) imageStream.Dispose();
            }


            return imageBase64;
        }

        private void SetChartSize(XmlDocument xmlProperties, XmlNode chartNode)
        {
            XmlNode dimHeightNode = xmlProperties.SelectSingleNode("//properties/Dimentions/height");
            XmlNode dimWidthNode = xmlProperties.SelectSingleNode("//properties/Dimentions/width");

            if ((dimHeightNode != null) && (dimWidthNode != null))
            {
                //get resolution
                XmlNode resolutionNode = xmlProperties.SelectSingleNode("//formatting/Resolution");
                if (resolutionNode != null)
                {
                    int tmpResolution = 0;
                    if (int.TryParse(resolutionNode.InnerText, out tmpResolution))
                    {
                        m_Resolution = tmpResolution;
                    }
                }

                if (m_PredefinedType.Equals(FreeFormDefinitions.FDC.Enums.FDCPredefinedChart.TzBankStackedColumnHeb.ToString()) || m_PredefinedType.Equals(FreeFormDefinitions.FDC.Enums.FDCPredefinedChart.TzBankStackedColumnEng.ToString()))
                    m_Resolution = 120;

                m_WidthPx = GetPixel(dimWidthNode.InnerText);
                m_HeightPx = GetPixel(dimHeightNode.InnerText);
                if (m_WidthPx > 0 && m_HeightPx > 0)
                {
                    float factor = (m_Resolution / FreeFormDefinitions.MSWORD_DEFAULT_RESOLUTION);
                    m_WidthPx *= factor;
                    m_HeightPx *= factor;

                    if (chartNode.OwnerDocument != null)
                    {
                        XmlAttribute attrChartSize = chartNode.OwnerDocument.CreateAttribute(FreeFormDefinitions.CHART_ATTR_SIZE);
                        attrChartSize.Value = string.Format("{0},{1}", (int)m_WidthPx, (int)m_HeightPx);

                        if (chartNode.Attributes != null) chartNode.Attributes.Append(attrChartSize);
                    }
                }
            }
        }

        private static float GetPixel(string emu)
        {
            float pixels = 0;

            try
            {
                pixels = int.Parse(emu);
                pixels /= FreeFormDefinitions.EMUS_DIVISOR; //in pt
                pixels /= 72F; //in inch
                pixels *= FreeFormDefinitions.MSWORD_DEFAULT_RESOLUTION; //in pixel (resolution 96 DPI) (according to MSWord)
            }
            catch { }

            return pixels;
        }

        private static int CheckStringData(ref string value, string dateFormat)
        {
            return NO_ERRORS;
        }

        private static int CheckDoubleData(ref string value, string dateFormat)
        {
            int error = NO_ERRORS;
            try
            {
                double num;
                NumberStyles style = NumberStyles.Number;
                style -= (NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);

                if (!double.TryParse(value, style, null, out num))
                    error = ERROR_VALUE_IS_NOT_DOUBLE;
            }
            catch
            {
                error = ERROR_VALUE_IS_NOT_DOUBLE;
            }

            return error;
        }

        private static int CheckDateTimeData(ref string value, string dateFormat)
        {
            int error = NO_ERRORS;

            try
            {
                DateTime objDate = DateTime.ParseExact(value, string.IsNullOrEmpty(dateFormat) ? "dd/MM/yyyy" : dateFormat, null);

                double doubleDate = objDate.ToOADate();
                value = doubleDate.ToString();
            }
            catch
            {
                error = ERROR_VALUE_IS_NOT_DATE_AND_TIME;
            }

            return error;
        }

        public string DoChart(string chartParams)
        {
            int error = NO_ERRORS;
            MemoryStream imageStream = null;
            string buffer64 = "=-1";// set invalid Base64 buffer (the first symbol '='). 

            error = BuildChartImage(chartParams, ref imageStream);

            try
            {
                if (error == NO_ERRORS)
                {
                    buffer64 = Convert.ToBase64String(imageStream.ToArray());
                }
                else
                {
                    buffer64 = string.Format("={0}", error);
                }
            }
            catch (Exception ex)
            {
                buffer64 = string.Format("={0}", ERROR_BUILD_CHART);

                FFLog4net.Instance.Warn(string.Format("DoChart.{0};Error:{1}", ex.Message, ERROR_BUILD_CHART));
            }
            finally
            {
                if (imageStream != null) imageStream.Dispose();

                GC.Collect();
                GC.WaitForFullGCComplete();
            }

            return buffer64;
        }

        public string XmlNodesToString(XPathNavigator nav)
        {
            return nav.OuterXml;
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public int GetChartStream(string chartParams, ref MemoryStream imageStream)
        {
            int error = NO_ERRORS;

            XmlDocument xmlChart = new XmlDocument();
            xmlChart.LoadXml(chartParams);
            XmlNodeList seriesList = xmlChart.SelectNodes(".//Series/Series");
            int serieCount = 0;

            foreach (XmlNode serie in seriesList)
            {
                XmlAttributeCollection attr = serie.Attributes;
                attr.RemoveNamedItem("XValueType");
                attr.RemoveNamedItem("YValueType");

                XmlNode pointsNode = serie.SelectSingleNode("./Points");

                XmlAttribute[] pointsAttrArray = null;
                if (pointsNode.Attributes.Count > 0)
                {
                    pointsAttrArray = new XmlAttribute[pointsNode.Attributes.Count];
                    pointsNode.Attributes.CopyTo(pointsAttrArray, 0);
                }

                pointsNode.RemoveAll();

                if (pointsAttrArray != null)
                {
                    foreach (XmlAttribute pointsAttr in pointsAttrArray)
                        pointsNode.Attributes.Append((XmlAttribute)pointsAttr.Clone());
                }

                for (int ind = 0; ind < 4; ind++)
                {
                    XmlNode dataPoint = xmlChart.CreateNode(XmlNodeType.Element, "Point", "");

                    XmlNode xPoint = xmlChart.CreateNode(XmlNodeType.Element, "X", "");
                    xPoint.InnerXml = m_XForPreviwe[ind].ToString();
                    dataPoint.AppendChild(xPoint);

                    XmlNode yPoint = xmlChart.CreateNode(XmlNodeType.Element, "Y", "");
                    yPoint.InnerXml = (m_YForPreviwe[ind] + serieCount).ToString();
                    dataPoint.AppendChild(yPoint);

                    pointsNode.AppendChild(dataPoint);
                }

                serieCount += 5;
            }

            error = BuildChartImage(xmlChart.DocumentElement.OuterXml, ref imageStream);

            return error;

        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private void LoadChartParams(string chartParams, XmlDocument xmlChart)
        {
            XmlDocument xmlProperties = new XmlDocument();
            xmlProperties.LoadXml(chartParams);

            XmlNode propChartNode = xmlProperties.SelectSingleNode("//Chart");

            xmlChart.LoadXml(propChartNode.OuterXml);

            //check if the chart is predefined type
            XmlNode fdcPredefinedTypeNode = xmlProperties.SelectSingleNode("//formatting/FDCPredefinedChart");
            if (fdcPredefinedTypeNode != null) m_PredefinedType = fdcPredefinedTypeNode.InnerText;

            SetChartSize(xmlProperties, xmlChart.DocumentElement);

            XmlNode valueAddendumNode = xmlProperties.SelectSingleNode("//formatting/AppendString");
            if (valueAddendumNode != null) m_ValueAddendum = valueAddendumNode.InnerText;
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private int BuildChartImage(string chartParams, ref MemoryStream imageStream)
        {
            int error = NO_ERRORS;

            FFLog4net.Instance.Debug("BuildChartImage Start ");

            XmlDocument xmlChart = new XmlDocument();

            string xPointName = XPOINT_VALUE;

            try
            {
                LoadChartParams(chartParams, xmlChart);
                XmlNodeList seriesList = xmlChart.SelectNodes(".//Series/Series");

                XmlAttribute formatAttrX = null;
                XmlAttribute formatAttrY = null;

                XmlNode labelNode = xmlChart.SelectSingleNode(".//ChartAreas/ChartArea/AxisX/LabelStyle");
                if (labelNode != null)
                    formatAttrX = labelNode.Attributes["Format"];

                labelNode = xmlChart.SelectSingleNode(".//ChartAreas/ChartArea/AxisY/LabelStyle");
                if (labelNode != null)
                    formatAttrY = labelNode.Attributes["Format"];

                int seriesCount = seriesList.Count;

                if (seriesCount > 0)
                {
                    XmlAttribute charTypeAttr = seriesList[0].Attributes["ChartType"];
                    string chartType = (charTypeAttr != null) ? charTypeAttr.Value : string.Empty;

                    if (seriesCount > 1 && chartType.Equals(CHART_TYPE_PIE, StringComparison.OrdinalIgnoreCase))
                        GetMultiPieXml(ref xmlChart, ref seriesList, seriesCount);

                    CheckData chekDataFunctionX = GetChartProperties(seriesList[0], formatAttrX, chartType, ref xPointName);

                    string yValueType = string.Empty;
                    XmlAttribute yValueTypeAttr = seriesList[0].Attributes["YValueType"];
                    if (yValueTypeAttr != null)
                        yValueType = yValueTypeAttr.Value;

                    CheckData chekDataFunctionY;
                    if (yValueType.Equals("time", StringComparison.OrdinalIgnoreCase) || yValueType.Equals("date", StringComparison.OrdinalIgnoreCase) || yValueType.Equals("datetime", StringComparison.OrdinalIgnoreCase) || yValueType.Equals("datetimeoffset", StringComparison.OrdinalIgnoreCase))
                    {
                        chekDataFunctionY = CheckDateTimeData;
                        m_DateFormatY = formatAttrY == null ? null : formatAttrY.Value;
                    }
                    else
                    {
                        chekDataFunctionY = CheckStringData;
                    }

                    foreach (XmlNode serie in seriesList)
                    {
                        XmlNodeList pointList = serie.SelectNodes("./Points/Point");

                        XmlAttribute[] pointsAttrArray = null;
                        if (pointList.Count > 0)
                        {
                            XmlNode pointsNode = pointList[0].ParentNode;
                            if (pointsNode.Attributes.Count > 0)
                            {
                                pointsAttrArray = new XmlAttribute[pointsNode.Attributes.Count];
                                pointsNode.Attributes.CopyTo(pointsAttrArray, 0);

                                pointsNode.Attributes.RemoveAll();
                            }
                        }

                        foreach (XmlNode pointNode in pointList)
                        {
                            XmlNode dataPoint = xmlChart.CreateNode(XmlNodeType.Element, "DataPoint", "");

                            XmlNode xPoint = pointNode.SelectSingleNode("./X");
                            XmlNode yPoint = pointNode.SelectSingleNode("./Y");

                            if (xPoint != null && !string.IsNullOrEmpty(xPoint.InnerText))
                            {
                                string xPointValue = xPoint.InnerText;
                                error = chekDataFunctionX(ref xPointValue, m_DateFormatX);

                                if (error != NO_ERRORS)
                                    return error;

                                XmlAttribute xPointAttr = xmlChart.CreateAttribute(xPointName);
                                xPointAttr.Value = xPointValue;
                                dataPoint.Attributes.Append(xPointAttr);
                            }

                            if (yPoint != null && !string.IsNullOrEmpty(yPoint.InnerText))
                            {
                                string yPointValue = yPoint.InnerText;
                                chekDataFunctionY(ref yPointValue, m_DateFormatY);

                                XmlAttribute yPointAttr = xmlChart.CreateAttribute("YValues");
                                yPointAttr.Value = yPointValue;
                                dataPoint.Attributes.Append(yPointAttr);
                            }

                            //copy attribute from Points to each Point
                            if (pointsAttrArray != null)
                            {
                                foreach (XmlAttribute pointsAttr in pointsAttrArray)
                                    dataPoint.Attributes.Append((XmlAttribute)pointsAttr.Clone());
                            }

                            pointNode.ParentNode.AppendChild(dataPoint);
                            pointNode.ParentNode.RemoveChild(pointNode);
                        }
                    }

                    FFLog4net.Instance.Debug($"Before Rebuild:PredefinedType-{m_PredefinedType}");

                    if (m_PredefinedType.Equals(FreeFormDefinitions.FDC.Enums.FDCPredefinedChart.TzBankStackedColumn.ToString()))
                    {
                        RebuildToTzBankStackedColumn(xmlChart, seriesList);
                    }
                    else if (m_PredefinedType.Equals(FreeFormDefinitions.FDC.Enums.FDCPredefinedChart.TzBankStackedColumnHeb.ToString()))
                    {
                        RebuildToTzBankStackedColumnBL(xmlChart, seriesList);
                    }
                    else if (m_PredefinedType.Equals(FreeFormDefinitions.FDC.Enums.FDCPredefinedChart.TzBankStackedColumnEng.ToString()))
                    {
                        RebuildToTzBankStackedColumnBL(xmlChart, seriesList, true);
                    }
                    else if (chartType.Equals(CHART_TYPE_STACKED_COLUMN, StringComparison.OrdinalIgnoreCase))
                    {
                        //turn off the legend
                        XmlNodeList legendNodeList = xmlChart.DocumentElement.SelectNodes("./Legends/Legend");
                        foreach (XmlNode legendNode in legendNodeList)
                        {
                            XmlAttribute enableAttr = xmlChart.CreateAttribute("Enabled");
                            enableAttr.Value = "false";
                            legendNode.Attributes.Append(enableAttr);
                        }

                        RebuildSeries4StackedColumn(xmlChart, seriesList);
                    }
                }
                imageStream = new MemoryStream();
                StreamWriter writer = new StreamWriter(imageStream);
                writer.Write(xmlChart.DocumentElement.OuterXml);
                writer.Flush();
                imageStream.Position = 0;
            }
            catch (Exception ex)
            {
                error = ERROR_COMMON;

                FFLog4net.Instance.Warn(string.Format("BuildChartImage.{0};Error:{1}", ex.Message, ERROR_COMMON));
            }

            FFLog4net.Instance.Debug("BuildChartImage Finish.");

            return error;
        }


        /// //////////// DONT works >>>
        //public static string GetEmbeddedResource(string resourceName, Assembly assembly)
        //{
        //    resourceName = FormatResourceName(assembly, resourceName);
        //    using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
        //    {
        //        if (resourceStream == null)
        //            return null;

        //        using (StreamReader reader = new StreamReader(resourceStream))
        //        {
        //            return reader.ReadToEnd();
        //        }
        //    }
        //}

        //private static string FormatResourceName(Assembly assembly, string resourceName)
        //{
        //    return assembly.GetName().Name + "." + resourceName.Replace(" ", "_")
        //                                                       .Replace("\\", ".")
        //                                                       .Replace("/", ".");
        //}

        private float GetMaxValueOfSeries(XmlNodeList seriesList)
        {
            float maxValue = 0;
            foreach (XmlNode series in seriesList)
            {
                float tmpValue = 0;
                XmlNodeList dataValueList = series.SelectNodes("./Points/DataPoint[@YValues]");
                foreach (XmlNode dataValue in dataValueList)
                {
                    float value = 0;
                    if (float.TryParse(dataValue.Attributes["YValues"].Value, out value))
                    {
                        tmpValue += value;
                    }
                }

                if (maxValue < tmpValue) maxValue = tmpValue;

            }

            return maxValue;
        }


        ////////////////// <<<
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private void RebuildToTzBankStackedColumn(XmlDocument xmlChart, XmlNodeList seriesList)
        {
            //get maximum from series
            float maxValue = GetMaxValueOfSeries(seriesList);

            RebuildSeries4StackedColumn(xmlChart, seriesList);

            //separate series to two areas
            XmlNodeList seriesArea1 = xmlChart.SelectNodes("//Series/Series");
            List<XmlNode> seriesArea2 = new List<XmlNode>();
            if (seriesArea1.Count > 0)
            {
                foreach (XmlNode series in seriesArea1)
                {
                    XmlAttribute chartArea1Attr = xmlChart.CreateAttribute("ChartArea");
                    chartArea1Attr.Value = "ChartArea1";
                    series.Attributes.Append(chartArea1Attr);

                    series.Attributes["Legend"].Value = "Default1";

                    seriesArea2.Add(series.Clone());
                }

                foreach (XmlNode series in seriesArea1)
                {
                    XmlNode points = series.SelectSingleNode("Points");
                    if (points != null)
                    {
                        XmlNodeList dataPoints = points.SelectNodes("DataPoint");
                        if (dataPoints.Count > 0)
                        {
                            string legendText = string.Empty;
                            if (dataPoints[0].Attributes["Label"] != null)
                            {
                                dataPoints[0].Attributes["Label"].Value = dataPoints[0].Attributes["Label"].Value.Replace('\n', ' ');
                                legendText = dataPoints[0].Attributes["Label"].Value.Trim();

                                series.Attributes["LegendText"].Value = legendText;

                                XmlAttribute isVisibleInLegendAttr = series.Attributes["IsVisibleInLegend"];
                                if (isVisibleInLegendAttr == null)
                                {
                                    isVisibleInLegendAttr = series.OwnerDocument.CreateAttribute("IsVisibleInLegend");
                                    series.Attributes.Append(isVisibleInLegendAttr);
                                }
                                isVisibleInLegendAttr.Value = "False";

                                dataPoints[0].Attributes["Label"].Value = string.Empty;
                            }
                        }

                        for (int ind = 1; ind < dataPoints.Count; ind++)
                        {
                            points.RemoveChild(dataPoints[ind]);
                        }
                    }
                }

                foreach (XmlNode series2 in seriesArea2)
                {
                    XmlAttribute chartArea2Attr = xmlChart.CreateAttribute("ChartArea");
                    chartArea2Attr.Value = "ChartArea2";
                    series2.Attributes.Append(chartArea2Attr);

                    series2.Attributes["Legend"].Value = "Default2";
                    series2.Attributes["Name"].Value += "_2";

                    XmlNode points = series2.SelectSingleNode("Points");
                    if (points != null)
                    {
                        XmlNodeList dataPoints = points.SelectNodes("DataPoint");
                        if (dataPoints.Count > 1)
                        {
                            string legendText = string.Empty;
                            if (dataPoints[1].Attributes["Label"] != null)
                            {
                                dataPoints[1].Attributes["Label"].Value = dataPoints[1].Attributes["Label"].Value.Replace('\n', ' ');
                                legendText = dataPoints[1].Attributes["Label"].Value.Trim();

                                series2.Attributes["LegendText"].Value = legendText;

                                XmlAttribute isVisibleInLegendAttr = series2.Attributes["IsVisibleInLegend"];
                                if (isVisibleInLegendAttr == null)
                                {
                                    isVisibleInLegendAttr = series2.OwnerDocument.CreateAttribute("IsVisibleInLegend");
                                    series2.Attributes.Append(isVisibleInLegendAttr);
                                }
                                isVisibleInLegendAttr.Value = "False";

                                dataPoints[1].Attributes["Label"].Value = string.Empty;
                            }

                        }

                        points.RemoveChild(dataPoints[0]);
                    }

                    seriesArea1[0].ParentNode.AppendChild(series2);
                }

                ApplyPredefinedTzBankStackedColumn(xmlChart, maxValue);
            }
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static void ApplyPredefinedTzBankStackedColumn(XmlDocument xmlChart, float maxSeriesValue = 0)
        {
            try
            {
                XmlDocument predefinedChart = new XmlDocument();
                predefinedChart.LoadXml(PredefinedChart.TZ_BANKAI_STAKED);

                //update legend
                XmlNode defLegend = predefinedChart.SelectSingleNode("//Legends");
                if (defLegend != null)
                {
                    XmlNode curLegend = xmlChart.SelectSingleNode("//Legends");
                    if (curLegend != null)
                    {
                        XmlNode impotedNode = xmlChart.ImportNode(defLegend, true);

                        curLegend.ParentNode.ReplaceChild(impotedNode, curLegend);
                    }

                    defLegend = xmlChart.SelectSingleNode("//Legends");
                }

                XmlNodeList itemsLegendDefault1 = defLegend.SelectNodes("Legend[@Name='Default1']/CustomItems/LegendItem");
                XmlNode customItemsNode = defLegend.SelectSingleNode("Legend[@Name='Default1']/CustomItems");
                XmlNodeList firstSeriesNodes = xmlChart.SelectNodes("//Chart/Series/Series[@Legend='Default1']");
                for (int i = 0; i < firstSeriesNodes.Count; i++)
                {
                    if (itemsLegendDefault1.Count > i)
                    {
                        XmlNode legendCellNode = null;

                        string color = null;
                        string legendText = firstSeriesNodes[i].Attributes["LegendText"].Value;
                        if (legendText.Contains("אחר"))
                        {
                            legendCellNode = customItemsNode.SelectSingleNode("LegendItem[@Color='245,153,72']");
                            color = "245,153,72";
                        }
                        else if (legendText.Contains("שיקים"))
                        {
                            legendCellNode = customItemsNode.SelectSingleNode("LegendItem[@Color='144,142,75']");
                            color = "144,142,75";
                        }
                        else if (legendText.Contains("העברות"))
                        {
                            legendCellNode = customItemsNode.SelectSingleNode("LegendItem[@Color='152,4,9']");
                            color = "152,4,9";
                        }
                        else if (legendText.Contains("משכורת"))
                        {
                            legendCellNode = customItemsNode.SelectSingleNode("LegendItem[@Color='78,115,174']");
                            color = "78,115,174";
                        }
                        else if (legendText.Contains("כרטיסי חיוב"))
                        {
                            legendCellNode = customItemsNode.SelectSingleNode("LegendItem[@Color='78,115,174']");
                            color = "78,115,174";
                        }
                        else if (legendText.Contains("מזומן"))
                        {
                            legendCellNode = customItemsNode.SelectSingleNode("LegendItem[@Color='30,134,189']");
                            color = "30,134,189";
                        }
                        else if (legendText.Contains("משכנתא"))
                        {
                            legendCellNode = customItemsNode.SelectSingleNode("LegendItem[@Color='99,0,10']");
                            color = "99,0,10";
                        }

                        if (legendCellNode != null)
                        {
                            XmlNode cellNode = legendCellNode.SelectSingleNode("descendant::LegendCell[@Text]");

                            if (cellNode != null)
                            {
                                cellNode.Attributes["Text"].Value = legendText;

                                XmlNode dataPointNode = firstSeriesNodes[i].SelectSingleNode("./Points/DataPoint");
                                if (dataPointNode != null)
                                {
                                    XmlAttribute colorAttr = dataPointNode.Attributes["Color"];
                                    if (colorAttr == null)
                                    {
                                        colorAttr = dataPointNode.OwnerDocument.CreateAttribute("Color");
                                        dataPointNode.Attributes.Append(colorAttr);
                                    }
                                    if (dataPointNode.Attributes["IsEmpty"] == null)
                                    {
                                        colorAttr.Value = color;
                                    }
                                    else
                                    {
                                        cellNode.Attributes["Color"].Value = "0,255,255,255";
                                        cellNode.Attributes["BorderColor"].Value = "0,255,255,255";
                                    }
                                }
                            }
                        }
                    }
                }

                List<XmlNode> delNodes = new List<XmlNode>();
                foreach (XmlNode item in itemsLegendDefault1)
                {
                    XmlNode cellNode = item.SelectSingleNode("descendant::LegendCell[@Text='empty']");
                    if (cellNode != null)
                    {
                        delNodes.Add(item);
                    }
                }

                foreach (XmlNode delNode in delNodes) delNode.ParentNode.RemoveChild(delNode);

                XmlNodeList itemsLegendDefault2 = defLegend.SelectNodes("Legend[@Name='Default2']/CustomItems/LegendItem");
                customItemsNode = defLegend.SelectSingleNode("Legend[@Name='Default2']/CustomItems");
                XmlNodeList secondSeriesNodes = xmlChart.SelectNodes("//Chart/Series/Series[@Legend='Default2']");
                for (int i = 0; i < secondSeriesNodes.Count; i++)
                {
                    if (itemsLegendDefault2.Count > i)
                    {
                        XmlNode legendCellNode = null;
                        string color = null;
                        string legendText = secondSeriesNodes[i].Attributes["LegendText"].Value;
                        if (legendText.Contains("אחר"))
                        {
                            legendCellNode = customItemsNode.SelectSingleNode("LegendItem[@Color='245,153,72']");
                            color = "245,153,72";
                        }
                        else if (legendText.Contains("שיקים"))
                        {
                            legendCellNode = customItemsNode.SelectSingleNode("LegendItem[@Color='144,142,75']");
                            color = "144,142,75";
                        }
                        else if (legendText.Contains("העברות"))
                        {
                            legendCellNode = customItemsNode.SelectSingleNode("LegendItem[@Color='152,4,9']");
                            color = "152,4,9";
                        }
                        else if (legendText.Contains("משכורת"))
                        {
                            legendCellNode = customItemsNode.SelectSingleNode("LegendItem[@Color='78,115,174']");
                            color = "78,115,174";
                        }
                        else if (legendText.Contains("כרטיסי חיוב"))
                        {
                            legendCellNode = customItemsNode.SelectSingleNode("LegendItem[@Color='78,115,174']");
                            color = "78,115,174";
                        }
                        else if (legendText.Contains("מזומן"))
                        {
                            legendCellNode = customItemsNode.SelectSingleNode("LegendItem[@Color='30,134,189']");
                            color = "30,134,189";
                        }
                        else if (legendText.Contains("משכנתא"))
                        {
                            legendCellNode = customItemsNode.SelectSingleNode("LegendItem[@Color='99,0,10']");
                            color = "99,0,10";
                        }

                        if (legendCellNode != null)
                        {
                            XmlNode cellNode = legendCellNode.SelectSingleNode("descendant::LegendCell[@Text]");

                            if (cellNode != null)
                            {
                                cellNode.Attributes["Text"].Value = legendText;

                                XmlNode dataPointNode = secondSeriesNodes[i].SelectSingleNode("./Points/DataPoint");
                                if (dataPointNode != null)
                                {
                                    XmlAttribute colorAttr = dataPointNode.Attributes["Color"];
                                    if (colorAttr == null)
                                    {
                                        colorAttr = dataPointNode.OwnerDocument.CreateAttribute("Color");
                                        dataPointNode.Attributes.Append(colorAttr);
                                    }
                                    if (dataPointNode.Attributes["IsEmpty"] == null)
                                    {
                                        colorAttr.Value = color;
                                    }
                                    else
                                    {
                                        cellNode.Attributes["Color"].Value = "0,255,255,255";
                                        cellNode.Attributes["BorderColor"].Value = "0,255,255,255";
                                    }
                                }
                            }
                        }
                    }
                }

                delNodes.Clear();
                foreach (XmlNode item in itemsLegendDefault2)
                {
                    XmlNode cellNode = item.SelectSingleNode("descendant::LegendCell[@Text='empty']");
                    if (cellNode != null)
                    {
                        delNodes.Add(item);
                    }
                }

                foreach (XmlNode delNode in delNodes) delNode.ParentNode.RemoveChild(delNode);

                //ChartAreas 
                XmlNode defChartAreas = predefinedChart.SelectSingleNode("//ChartAreas");
                if (defChartAreas != null)
                {
                    XmlNode curChartAreas = xmlChart.SelectSingleNode("//ChartAreas");
                    if (curChartAreas != null)
                    {
                        XmlNode impotedNode = xmlChart.ImportNode(defChartAreas, true);

                        curChartAreas.ParentNode.ReplaceChild(impotedNode, curChartAreas);
                    }
                }

                //set max Series Value
                SetMaxValueOfSeries(xmlChart, maxSeriesValue);

                //remove title
                XmlNode titleNode = xmlChart.SelectSingleNode("//Titles");
                if (titleNode != null) titleNode.ParentNode.RemoveChild(titleNode);
            }
            catch (Exception ex)
            {
                FFLog4net.Instance.Warn(string.Format("Chart TzBankStackedColumn:{0}", ex.Message));
            }
        }

        private static void SetMaxValueOfSeries(XmlDocument xmlChart, float maxSeriesValue)
        {
            if (maxSeriesValue > 0)
            {
                maxSeriesValue += maxSeriesValue * 0.1F; //increase in 10%

                XmlNodeList axisesY = xmlChart.SelectNodes("//ChartAreas/ChartArea/AxisY");
                foreach (XmlNode axisY in axisesY)
                {
                    XmlAttribute maxAttr = xmlChart.CreateAttribute("Maximum");
                    maxAttr.Value = maxSeriesValue.ToString();
                    axisY.Attributes.Append(maxAttr);
                }
            }
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private void RebuildSeries4StackedColumn(XmlDocument xmlChart, XmlNodeList seriesList)
        {
            XmlNode seriesParentNode = seriesList[0].ParentNode;

            int numPoints = 0;
            List<XmlNodeList> dataPointLists = new List<XmlNodeList>();
            foreach (XmlNode series in seriesList)
            {
                XmlNodeList dataPointList = series.SelectNodes("./Points/DataPoint");

                if (dataPointList.Count > numPoints) numPoints = dataPointList.Count;

                dataPointLists.Add(dataPointList);
            }

            XmlNode smartLabelStyleNode = seriesParentNode.SelectSingleNode("//SmartLabelStyle");

            seriesParentNode.RemoveAll();

            bool isValueShownAsLabel = false;
            XmlAttribute isValueShownAsLabelAttr = seriesList[0].Attributes["IsValueShownAsLabel"];
            if (isValueShownAsLabelAttr != null) isValueShownAsLabel = Convert.ToBoolean(isValueShownAsLabelAttr.Value);

            for (int i = 0; i < numPoints; i++)
            {
                string curSeriesName = string.Format("StackedSeries{0}", i + 1);
                XmlNode stackSeriesNode = xmlChart.CreateElement("Series");
                XmlNode pointsNode = xmlChart.CreateElement("Points");
                stackSeriesNode.AppendChild(pointsNode);

                if (smartLabelStyleNode != null)
                {
                    stackSeriesNode.PrependChild(smartLabelStyleNode.Clone());
                }

                //add existing attributes
                foreach (XmlAttribute attr in seriesList[0].Attributes)
                {
                    stackSeriesNode.Attributes.Append((XmlAttribute)attr.Clone());
                }

                //add nea attribute
                XmlAttribute attrDrawingStyle = xmlChart.CreateAttribute("CustomProperties");
                attrDrawingStyle.Value = "DrawingStyle=Cylinder";
                stackSeriesNode.Attributes.Append(attrDrawingStyle);

                //change attribute name
                try
                {
                    stackSeriesNode.Attributes["Name"].Value = curSeriesName;
                }
                catch
                {
                    XmlAttribute attrName = xmlChart.CreateAttribute("Name");
                    attrName.Value = curSeriesName;
                    stackSeriesNode.Attributes.Prepend(attrName);
                }

                //remove color
                stackSeriesNode.Attributes.RemoveNamedItem("Color");

                foreach (XmlNodeList dataPointList in dataPointLists)
                {
                    if (dataPointList.Count > i && dataPointList[i].Attributes.Count > 0)
                    {
                        //set LegendText attribute as AxisLabel
                        XmlAttribute legendTextAttr = dataPointList[i].ParentNode.ParentNode.Attributes["LegendText"];

                        pointsNode.AppendChild(dataPointList[i]);

                        if (legendTextAttr != null)
                        {
                            XmlAttribute axisLabelAttr = xmlChart.CreateAttribute("AxisLabel");
                            axisLabelAttr.Value = legendTextAttr.Value;

                            dataPointList[i].Attributes.Append(axisLabelAttr);
                        }

                        string appendValue = string.Empty;
                        if (isValueShownAsLabel)
                            appendValue = dataPointList[i].Attributes["YValues"].Value;

                        dataPointList[i].Attributes["Label"].Value = dataPointList[i].Attributes["Label"].Value.Trim();
                        dataPointList[i].Attributes["Label"].Value = dataPointList[i].Attributes["Label"].Value.Replace(' ', '\n');
                        dataPointList[i].Attributes["Label"].Value = string.Format("{0} {1}{2}", dataPointList[i].Attributes["Label"].Value, appendValue, m_ValueAddendum);

                    }
                    else
                    {
                        //add empty datapoint <DataPoint IsEmpty="true" />
                        XmlNode dataPointEmpty = xmlChart.CreateElement("DataPoint");
                        XmlAttribute dataPointEmptyAttr = xmlChart.CreateAttribute("IsEmpty");
                        dataPointEmptyAttr.Value = "true";
                        dataPointEmpty.Attributes.Append(dataPointEmptyAttr);

                        XmlAttribute labelEmptyAttr = xmlChart.CreateAttribute("Label");
                        labelEmptyAttr.Value = string.Empty;
                        dataPointEmpty.Attributes.Append(labelEmptyAttr);

                        XmlAttribute colorEmptyAttr = xmlChart.CreateAttribute("Color");
                        colorEmptyAttr.Value = "0,255,255,255";
                        dataPointEmpty.Attributes.Append(colorEmptyAttr);

                        pointsNode.AppendChild(dataPointEmpty);
                    }
                }

                seriesParentNode.AppendChild(stackSeriesNode);

                // Name="Series1"  Legend="Default" XValueType="String"  IsXValueIndexed="true" YValueType="Double" ChartType="StackedColumn" ChartArea="Default" IsValueShownAsLabel="True"  CustomProperties="DrawingStyle=Cylinder"
            }
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private CheckData GetChartProperties(XmlNode serieNode, XmlAttribute formatAttr, string chartType, ref string xPointName)
        {
            CheckData chekDataFunction;

            bool isXValueIndexed = false;

            if (chartType.Equals(CHART_TYPE_PIE, StringComparison.OrdinalIgnoreCase))
            {
                xPointName = XPOINT_LEGEND;
                chekDataFunction = CheckStringData;
            }
            else if (chartType.Equals(CHART_TYPE_STACKED_COLUMN, StringComparison.OrdinalIgnoreCase))
            {
                xPointName = "Label";
                chekDataFunction = CheckStringData;
            }
            else
            {

                XmlAttribute isXValueIndexedAttr = serieNode.Attributes["IsXValueIndexed"];
                if (isXValueIndexedAttr != null)
                    isXValueIndexed = Convert.ToBoolean(isXValueIndexedAttr.Value);

                if (isXValueIndexed)
                {
                    chekDataFunction = CheckStringData;
                    xPointName = XPOINT_LABEL;
                }
                else
                {
                    string xValueType = string.Empty;
                    XmlAttribute xValueTypeAttr = serieNode.Attributes["XValueType"];
                    if (xValueTypeAttr != null)
                        xValueType = xValueTypeAttr.Value;

                    if (xValueType.Equals("string", StringComparison.OrdinalIgnoreCase))
                    {
                        chekDataFunction = CheckStringData;
                        xPointName = XPOINT_LABEL;
                    }
                    else
                    {
                        if (xValueType.Equals("time", StringComparison.OrdinalIgnoreCase) || xValueType.Equals("date", StringComparison.OrdinalIgnoreCase) || xValueType.Equals("datetime", StringComparison.OrdinalIgnoreCase) || xValueType.Equals("datetimeoffset", StringComparison.OrdinalIgnoreCase))
                        {

                            chekDataFunction = CheckDateTimeData;
                            m_DateFormatX = formatAttr == null ? null : formatAttr.Value;

                        }
                        else
                        {
                            chekDataFunction = CheckDoubleData;
                        }
                    }
                }
            }
            return chekDataFunction;
        }

        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private void GetMultiPieXml(ref XmlDocument xmlChart, ref XmlNodeList seriesList, int seriesCount)
        {
            double radius = FULL_SIZE / 2 / seriesCount;

            XmlNode chartAreasNode = xmlChart.SelectSingleNode(".//ChartAreas");
            XmlNode areaNode = chartAreasNode.SelectSingleNode("./ChartArea");
            if (areaNode != null)
            {
                chartAreasNode.RemoveChild(areaNode);

                bool is3D = false;
                try
                {
                    string attr3DValue = areaNode.SelectSingleNode("./Area3DStyle").Attributes["Enable3D"].Value;
                    is3D = Convert.ToBoolean(attr3DValue);
                }
                catch { }

                double addition = is3D ? ADDITION_KOEFFITSIEN : 0;

                #region Series
                for (int ind = 0; ind < seriesCount - 1; ind++)
                {
                    SetAttributesToSerie(ref xmlChart, ref seriesList, ind, "Doughnut");

                    XmlAttribute customPropAttr = xmlChart.CreateAttribute("CustomProperties");

                    double dougRadius = FULL_SIZE / (seriesCount - ind) + (seriesCount - 1);

                    customPropAttr.Value = string.Concat("DoughnutRadius=", dougRadius);
                    seriesList[ind].Attributes.Append(customPropAttr);
                }

                SetAttributesToSerie(ref xmlChart, ref seriesList, seriesCount - 1, "Pie");
                #endregion

                #region Areas
                for (int ind = 0; ind < seriesCount; ind++)
                {
                    XmlNode newAreaNode = areaNode.Clone();

                    XmlAttribute backColorAttr = xmlChart.CreateAttribute("BackColor");
                    backColorAttr.Value = "Transparent";
                    newAreaNode.Attributes.Append(backColorAttr);

                    XmlAttribute nameAttr = xmlChart.CreateAttribute("Name");
                    nameAttr.Value = string.Concat(CHART_AREA_NAME, ind);
                    newAreaNode.Attributes.Append(nameAttr);

                    XmlNode position = xmlChart.CreateNode(XmlNodeType.Element, "Position", "");

                    XmlAttribute xAttr = xmlChart.CreateAttribute("X");
                    xAttr.Value = (ind * radius).ToString();
                    position.Attributes.Append(xAttr);

                    XmlAttribute yAttr = xmlChart.CreateAttribute("Y");
                    yAttr.Value = (ind * radius - addition * ind).ToString();
                    position.Attributes.Append(yAttr);

                    XmlAttribute widthAttr = xmlChart.CreateAttribute("Width");
                    double width = FULL_SIZE - 2 * ind * radius;
                    widthAttr.Value = width.ToString();
                    position.Attributes.Append(widthAttr);

                    XmlAttribute heightAttr = xmlChart.CreateAttribute("Height");
                    heightAttr.Value = width.ToString();
                    position.Attributes.Append(heightAttr);

                    XmlAttribute autoAttr = xmlChart.CreateAttribute("Auto");
                    autoAttr.Value = "False";
                    position.Attributes.Append(autoAttr);

                    newAreaNode.AppendChild(position);

                    chartAreasNode.AppendChild(newAreaNode);
                }
            }

            #endregion

            #region Legend

            XmlNode legendNode = xmlChart.SelectSingleNode(".//Legends/Legend");

            if (legendNode != null)
            {
                XmlAttribute enabledAttr = xmlChart.CreateAttribute("Enabled");
                enabledAttr.Value = "False";
                legendNode.Attributes.Append(enabledAttr);
            }
            #endregion

            #region Title

            XmlNode titleNode = xmlChart.SelectSingleNode(".//Titles/Title");

            if (titleNode != null)
            {
                XmlAttribute visibleAttr = xmlChart.CreateAttribute("Visible");
                visibleAttr.Value = "False";
                titleNode.Attributes.Append(visibleAttr);
            }

            #endregion
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static void SetAttributesToSerie(ref XmlDocument xmlChart, ref XmlNodeList seriesList, int ind, string chartType)
        {
            XmlAttribute chartAreaAttr = xmlChart.CreateAttribute("ChartArea");
            chartAreaAttr.Value = string.Concat(CHART_AREA_NAME, ind);
            seriesList[ind].Attributes.Append(chartAreaAttr);

            XmlAttribute borderColorAttr = xmlChart.CreateAttribute("BorderColor");
            borderColorAttr.Value = BORDER_COLOR;
            seriesList[ind].Attributes.Append(borderColorAttr);

            XmlAttribute chartTypeAttr = xmlChart.CreateAttribute("ChartType");
            seriesList[ind].Attributes.Append(chartTypeAttr);
            chartTypeAttr.Value = chartType;
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private void RebuildToTzBankStackedColumnBL(XmlDocument xmlChart, XmlNodeList seriesList, bool english = false)
        {
            float maxValue = GetMaxValueOfSeries(seriesList);

            RebuildSeries4StackedColumn(xmlChart, seriesList);

            //separate series to two areas
            XmlNodeList seriesArea1 = xmlChart.SelectNodes("//Series/Series");
            List<XmlNode> seriesArea2 = new List<XmlNode>();
            if (seriesArea1.Count > 0)
            {
                foreach (XmlNode series in seriesArea1)
                {
                    XmlAttribute chartArea1Attr = xmlChart.CreateAttribute("ChartArea");
                    chartArea1Attr.Value = "ChartArea1";
                    series.Attributes.Append(chartArea1Attr);

                    series.Attributes["Legend"].Value = "Default1";

                    seriesArea2.Add(series.Clone());
                }

                foreach (XmlNode series in seriesArea1)
                {
                    XmlNode points = series.SelectSingleNode("Points");
                    if (points != null)
                    {
                        XmlNodeList dataPoints = points.SelectNodes("DataPoint");
                        if (dataPoints.Count > 0)
                        {
                            string legendText = string.Empty;
                            if (dataPoints[0].Attributes["Label"] != null)
                            {
                                dataPoints[0].Attributes["Label"].Value = dataPoints[0].Attributes["Label"].Value.Replace('\n', ' ');
                                legendText = dataPoints[0].Attributes["Label"].Value.Trim();

                                series.Attributes["LegendText"].Value = legendText;

                                XmlAttribute isVisibleInLegendAttr = series.Attributes["IsVisibleInLegend"];
                                if (isVisibleInLegendAttr == null)
                                {
                                    isVisibleInLegendAttr = series.OwnerDocument.CreateAttribute("IsVisibleInLegend");
                                    series.Attributes.Append(isVisibleInLegendAttr);
                                }
                                isVisibleInLegendAttr.Value = "False";

                                dataPoints[0].Attributes["Label"].Value = string.Empty;
                            }
                        }

                        for (int ind = 1; ind < dataPoints.Count; ind++)
                        {
                            points.RemoveChild(dataPoints[ind]);
                        }
                    }
                }

                foreach (XmlNode series2 in seriesArea2)
                {
                    XmlAttribute chartArea2Attr = xmlChart.CreateAttribute("ChartArea");
                    chartArea2Attr.Value = "ChartArea2";
                    series2.Attributes.Append(chartArea2Attr);

                    series2.Attributes["Legend"].Value = "Default2";
                    series2.Attributes["Name"].Value += "_2";

                    XmlNode points = series2.SelectSingleNode("Points");
                    if (points != null)
                    {
                        XmlNodeList dataPoints = points.SelectNodes("DataPoint");
                        if (dataPoints.Count > 1)
                        {
                            string legendText = string.Empty;
                            if (dataPoints[1].Attributes["Label"] != null)
                            {
                                dataPoints[1].Attributes["Label"].Value = dataPoints[1].Attributes["Label"].Value.Replace('\n', ' ');
                                legendText = dataPoints[1].Attributes["Label"].Value.Trim();

                                series2.Attributes["LegendText"].Value = legendText;

                                XmlAttribute isVisibleInLegendAttr = series2.Attributes["IsVisibleInLegend"];
                                if (isVisibleInLegendAttr == null)
                                {
                                    isVisibleInLegendAttr = series2.OwnerDocument.CreateAttribute("IsVisibleInLegend");
                                    series2.Attributes.Append(isVisibleInLegendAttr);
                                }
                                isVisibleInLegendAttr.Value = "False";

                                dataPoints[1].Attributes["Label"].Value = string.Empty;
                            }

                        }

                        points.RemoveChild(dataPoints[0]);
                    }

                    seriesArea1[0].ParentNode.AppendChild(series2);
                }

                ApplyPredefinedTzBankStackedColumnBL(xmlChart, english, maxValue);
            }
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private void ApplyPredefinedTzBankStackedColumnBL(XmlDocument xmlChart, bool english, float maxSeriesValue)
        {
            /*
             	B	G	R
אחר         	124	76	99
שיקים       	107	190	98
משכורת      	177	123	76
מזומן       	209	171	149
משכנתא      	62	135	225
כרטיסי אשראי	176	152	65
             */

            try
            {
                XmlDocument predefinedChart = new XmlDocument();
                if (english)
                    predefinedChart.LoadXml(PredefinedChart.TZ_BANKAI_STAKED_ENG);
                else
                    predefinedChart.LoadXml(PredefinedChart.TZ_BANKAI_STAKED_HEB);


                m_Resolution = 120;

                XmlNodeList firstSeriesNodes = xmlChart.SelectNodes("//Chart/Series/Series[@Legend='Default1']");
                for (int i = 0; i < firstSeriesNodes.Count; i++)
                {
                    string color = null;
                    string legendText = firstSeriesNodes[i].Attributes["LegendText"].Value;

                    color = GetColorByLegendText(legendText, english);

                    SetDataPointColor(firstSeriesNodes[i], color, english);
                }

                XmlNodeList secondSeriesNodes = xmlChart.SelectNodes("//Chart/Series/Series[@Legend='Default2']");
                for (int i = 0; i < secondSeriesNodes.Count; i++)
                {
                    string color = null;
                    string legendText = secondSeriesNodes[i].Attributes["LegendText"].Value;

                    color = GetColorByLegendText(legendText, english);

                    SetDataPointColor(secondSeriesNodes[i], color, english);
                }

                //ChartAreas 
                XmlNode defChartAreas = predefinedChart.SelectSingleNode("//ChartAreas");
                if (defChartAreas != null)
                {
                    XmlNode curChartAreas = xmlChart.SelectSingleNode("//ChartAreas");
                    if (curChartAreas != null)
                    {
                        XmlNode impotedNode = xmlChart.ImportNode(defChartAreas, true);

                        curChartAreas.ParentNode.ReplaceChild(impotedNode, curChartAreas);
                    }
                }

                //set max Series Value
                SetMaxValueOfSeries(xmlChart, maxSeriesValue);

                //remove title
                XmlNode titleNode = xmlChart.SelectSingleNode("//Titles");
                if (titleNode != null) titleNode.ParentNode.RemoveChild(titleNode);

                //remove legend
                XmlNode defLegend = predefinedChart.SelectSingleNode("//Legends");
                if (defLegend != null) defLegend.ParentNode.RemoveChild(defLegend);
            }
            catch (Exception ex)
            {
                FFLog4net.Instance.Warn(string.Format("Chart TzBankStackedColumnBL:{0}", ex.Message));
            }
        }

        private string GetColorByLegendText(string legendText, bool english)
        {
            string color = string.Empty;
            legendText = legendText.ToLower();
            if (legendText.Contains(english ? "other" : "אחר"))
            {
                color = "99,76,124";
            }
            else if (legendText.Contains(english ? "checks" : "שיקים"))
            {
                color = "98,190,107";
            }
            else if (legendText.Contains(english ? "transfers" : "העברות"))
            {
                color = "152,4,9";
            }
            else if (legendText.Contains(english ? "wages" : "משכורת"))
            {
                color = "76,123,177";
            }
            else if (legendText.Contains(english ? "payment cards" : "כרטיסי חיוב"))
            {
                color = "65,152,176";
            }
            else if (legendText.Contains(english ? "cash" : "מזומן"))
            {
                color = "149,171,209";
            }
            else if (legendText.Contains(english ? "mortgage" : "משכנתא"))
            {
                color = "225,135,62";
            }

            return color;
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private void SetDataPointColor(XmlNode seriesNode, string color, bool english)
        {
            XmlNode dataPointNode = seriesNode.SelectSingleNode("./Points/DataPoint");

            if (dataPointNode != null)
            {
                XmlAttribute colorAttr = dataPointNode.Attributes["Color"];
                if (colorAttr == null)
                {
                    colorAttr = dataPointNode.OwnerDocument.CreateAttribute("Color");
                    dataPointNode.Attributes.Append(colorAttr);
                }
                if (dataPointNode.Attributes["IsEmpty"] == null)
                {
                    colorAttr.Value = color;
                }

                if (english)
                    if (dataPointNode.Attributes["AxisLabel"] != null)
                        if (dataPointNode.Attributes["AxisLabel"].Value == "סך הכנסות שנתי")
                            dataPointNode.Attributes["AxisLabel"].Value = "Total yearly income";
                        else if (dataPointNode.Attributes["AxisLabel"].Value == "סך הוצאות שנתי")
                            dataPointNode.Attributes["AxisLabel"].Value = "Total yearly expenses";

                try
                {
                    XmlAttribute attrFont = dataPointNode.Attributes["Font"];
                    if (attrFont != null) attrFont.Value = "Courier New, 9.5pt";
                }
                catch { }

            }
        }



    }


}
