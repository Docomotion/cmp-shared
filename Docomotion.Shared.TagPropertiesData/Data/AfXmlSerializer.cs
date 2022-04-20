using System;
using System.IO;
using System.Xml.Serialization;

namespace Docomotion.Shared.TagPropertiesData.Data
{
    public class AfXmlSerializer
    {
        public string Serialize(TagOutputProperties tagProperties)
        {
            string resultString = null;

            XmlSerializer serializer = new XmlSerializer(typeof(TagOutputProperties));

            try
            {
                StringWriter strWriter = new StringWriter();
                serializer.Serialize(strWriter, tagProperties);
                resultString = strWriter.ToString();
                strWriter.Close();
            }
            catch (Exception e)
            {

            }


            return resultString;
        }

        public string Serialize(AfTagProperties.TagProperties tagProperties)
        {
            string resultString = null;

            XmlSerializer serializer = new XmlSerializer(typeof(Data.AfTagProperties.TagProperties));

            try
            {
                StringWriter strWriter = new StringWriter();
                serializer.Serialize(strWriter, tagProperties);
                resultString = strWriter.ToString();
                strWriter.Close();
            }
            catch (Exception e)
            {

            }

            return resultString;
        }

        public void Deserialize(out TagOutputProperties tagProperties, string inputString)
        {
            try
            {
                tagProperties = null;

                XmlSerializer serializer = new XmlSerializer(typeof(TagOutputProperties));

                StringReader strReader = new StringReader(inputString);

                tagProperties = (TagOutputProperties)serializer.Deserialize(strReader);

                strReader.Close();
            }
            catch (Exception e)
            {
                tagProperties = null;
            }
        }

        public void Deserialize(out AfTagProperties.TagProperties tagProperties, string inputString)
        {
            try
            {
                tagProperties = null;

                XmlSerializer serializer = new XmlSerializer(typeof(Data.AfTagProperties.TagProperties));

                StringReader strReader = new StringReader(inputString);

                tagProperties = (Data.AfTagProperties.TagProperties)serializer.Deserialize(strReader);

                strReader.Close();
            }
            catch (Exception e)
            {
                tagProperties = null;
            }
            
        }
    }
}
