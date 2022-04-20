using System.Collections.Generic;
using System.Xml;
using Docomotion.Shared.ComDef;

namespace Docomotion.Shared.SchemeManipulation
{
    public class SchemaManipulation
    {
        private const string XPATH_SCHEMA_MANIPULATION = "/FreeFormMapping/MAPPING_ROOT/xml/structure/type";

        public void ApplySchemaManipulation(XmlDocument mappingDoc, XmlDocument xmlDocData)
        {
            try
            {
                if (mappingDoc != null)
                {
                    XmlNode manipulationTypeNode = GetManipulations(mappingDoc);

                    if (manipulationTypeNode != null)
                    {
                        if (manipulationTypeNode.Attributes["action"].Value.Equals(FreeFormDefinitions.MANIPULATION_TYPE_CONCATENATE))
                        {
                            ApplyConcatenate(manipulationTypeNode, xmlDocData);
                        }
                        else if (manipulationTypeNode.Attributes["action"].Value.Equals(FreeFormDefinitions.MANIPULATION_TYPE_MAKE_PARENT))
                        {
                            ApplyMakeParent(manipulationTypeNode, xmlDocData);
                        }
                        else if (manipulationTypeNode.Attributes["action"].Value.Equals(FreeFormDefinitions.MANIPULATION_TYPE_ATTRIBUTE_VALUES))
                        {
                            ApplyAttributeValues(manipulationTypeNode, xmlDocData);
                        }
                    }
                }
            }
            catch { }
        }

        private XmlNode GetManipulations(XmlDocument mappingDoc)
        {
            XmlNode manipulationNode = mappingDoc.DocumentElement.SelectSingleNode(XPATH_SCHEMA_MANIPULATION);

            return manipulationNode;
        }

        private void ApplyConcatenate(XmlNode manipulationTypeNode, XmlDocument xmlDocData)
        {
// <structure>
//        <type action="Concatenate">
//          <delimiter />
//          <tag_name>
//            <operation>2</operation>
//            <name>abc</name>
//            <attr>
//              <name>aaa</name>
//            </attr>
//          </tag_name>
//        </type>
//      </structure>

            int method = 1;//default
            int iValue = 0;
            XmlNodeList tagList = manipulationTypeNode.SelectNodes("./tag_name");
            foreach (XmlNode tag in tagList)
            {
                XmlNode methodNode = tag.SelectSingleNode("./operation");
                if (methodNode != null)
                    if (int.TryParse(methodNode.InnerText, out iValue)) method = iValue;

                if (method == 1)
                {
                    ApplyConcatenateM1(manipulationTypeNode, tag, xmlDocData);
                }
                else if (method == 2)
                {
                    ApplyConcatenateM2(tag, xmlDocData);
                }
            }

        }

        private void ApplyConcatenateM2(XmlNode tag, XmlDocument xmlDocData)
        {
            string tagName = null;
            XmlNode nameNode = tag.SelectSingleNode("./name");
            if (nameNode != null) tagName = nameNode.InnerText;

            List<string> attrsName = new List<string>();
            XmlNodeList attrNameNodes = tag.SelectNodes("./attr/name");
            foreach (XmlNode attrNameNode in attrNameNodes)
                attrsName.Add(attrNameNode.InnerText);

            //apply manipulation to data
            if (!string.IsNullOrWhiteSpace(tagName))
            {
                XmlNodeList inputNodes = xmlDocData.SelectNodes(string.Format("//{0}", tagName));
                foreach (XmlNode inputNode in inputNodes)
                {
                    foreach (string attrName in attrsName)
                    {
                        if (!string.IsNullOrWhiteSpace(attrName))
                        {
                            if (inputNode.Attributes[attrName] != null)
                            {
                                 XmlNode childNode = xmlDocData.CreateElement(attrName);
                                 childNode.InnerText = inputNode.Attributes[attrName].Value;
                                 inputNode.AppendChild(childNode);
                            }
                        }
                    }

                    inputNode.Attributes.RemoveAll();
                }
            }
        }

        private void ApplyConcatenateM1(XmlNode manipulationTypeNode, XmlNode tag, XmlDocument xmlDocData)
        {
            #region example
            /*
  <MAPPING_ROOT>
    <row_col />
     <xml>
       <definitions />
       <structure>
         <type action="Concatenate">
           <delimiter>_</delimiter>
           <tag_name>
             <name>psft_rowset</name>
             <attr>
               <name>group</name>
             </attr>
           </tag_name>
         </type>
       </structure>
     </xml>
     <data_repository_tags>
     <ROOT>
     *DATA 
     ...
               */
            #endregion

            string delimiterStr = string.Empty;
            XmlNode delimiterNode = manipulationTypeNode.SelectSingleNode("./delimiter");
            if (delimiterNode != null) delimiterStr = delimiterNode.InnerText;

            string tagName = null;
            string concatNodeName = null;
            XmlNode nameNode = tag.SelectSingleNode("./name");
            if (nameNode != null) tagName = nameNode.InnerText;

            List<string> attrsName = new List<string>();
            XmlNodeList attrNameNodes = tag.SelectNodes("./attr/name");
            foreach (XmlNode attrNameNode in attrNameNodes)
                attrsName.Add(attrNameNode.InnerText);

            //apply manipulation to data
            if (!string.IsNullOrWhiteSpace(tagName))
            {
                XmlNodeList inputNodes = xmlDocData.SelectNodes(string.Format("//{0}", tagName));
                foreach (XmlNode inputNode in inputNodes)
                {
                    concatNodeName = tagName;

                    string concatAttrName = string.Empty;
                    foreach (string attrName in attrsName)
                    {
                        if (!string.IsNullOrWhiteSpace(attrName))
                        {
                            if (inputNode.Attributes[attrName] != null)
                                concatAttrName += string.Format("{2}{0}{2}{1}", attrName, inputNode.Attributes[attrName].Value, delimiterStr);
                        }
                    }

                    concatAttrName = concatAttrName.Replace(' ', '_');

                    concatNodeName += string.Format("{0}", concatAttrName);

                    XmlNode concatNode = xmlDocData.CreateElement(concatNodeName);
                    concatNode.InnerXml = inputNode.InnerXml;

                    inputNode.ParentNode.ReplaceChild(concatNode, inputNode);
                }
            }
        }

        private void ApplyMakeParent(XmlNode manipulationTypeNode, XmlDocument xmlDocData)
        {
            #region example
            /*
<type action="make_parent">
  <delimiter />
  <new_parent>
    <method>2</method>
    <name>P_P016DATA</name>
    <path />
    <tag_name>
      <name>P016DATA</name>
    </tag_name>
  </new_parent>
  <new_parent>
    <method>2</method>
    <name>AGUDA_016</name>
    <path />
    <tag_name>
      <name>P_P016DATA</name>
    </tag_name>
    <tag_name>
      <name>P016LAK</name>
    </tag_name>
  </new_parent>
</type>
             */
            #endregion

            List<XmlNode> parentNodesM3 = new List<XmlNode>();
            XmlNodeList parentNodes = manipulationTypeNode.SelectNodes("./new_parent");
            foreach(XmlNode parentNode in parentNodes)
            {
                int method = 2;//default
                int iValue = 0;
                XmlNode methodNode = parentNode.SelectSingleNode("./method");
                if (methodNode != null)
                    if (int.TryParse(methodNode.InnerText, out iValue)) method = iValue;

                if (method == 2)
                {
                    ApplyMakeParentM2(parentNode, xmlDocData);
                }
                else if(method == 1)
                {
                    ApplyMakeParentM1(parentNode, xmlDocData);
                }
                else if (method == 3)
                {
                    parentNodesM3.Add(parentNode);
                }
            }

            foreach (XmlNode parentNodeM3 in parentNodesM3)
            {
                ApplyMakeParentM3(parentNodeM3, xmlDocData);
            }
        }

        private void ApplyMakeParentM3(XmlNode parentNode, XmlDocument xmlDocData)
        {
            string newParentStr = null;
            List<string> childDataNames = new List<string>();
            XmlNode insertPoint = null;

            if (GetInfoAboutParentTag(parentNode, childDataNames, out newParentStr))
            {
                Queue<XmlNode> childrenNodes = null;
                int prevInd = -1;
                foreach (XmlNode childNode in xmlDocData.DocumentElement.ChildNodes)
                {
                    int ind = childDataNames.FindIndex(e => e.Equals(childNode.Name));
                    if (ind != (-1))
                    {
                        if (childrenNodes == null)
                        {
                            insertPoint = childNode.PreviousSibling;

                            childrenNodes = new Queue<XmlNode>();

                            childrenNodes.Enqueue(childNode);
                        }
                        else
                        {
                            if (prevInd > ind)
                            {
                                AddParent(ref childrenNodes, newParentStr, insertPoint, xmlDocData);

                                insertPoint = childNode.PreviousSibling;

                                childrenNodes = new Queue<XmlNode>();

                                childrenNodes.Enqueue(childNode);
                            }
                            else
                            {
                                childrenNodes.Enqueue(childNode);
                            }
                        }
                    }
                    else
                    {
                        AddParent(ref childrenNodes, newParentStr, insertPoint, xmlDocData);
                    }

                    prevInd = ind;
                }

                AddParent(ref childrenNodes, newParentStr, insertPoint, xmlDocData);
            }
        }

        private void ApplyMakeParentM2(XmlNode parentNode, XmlDocument xmlDocData)
        {
            string newParentStr = null;
            List<string> childDataNames = new List<string>();
            XmlNode insertPoint = null;

            if (GetInfoAboutParentTag(parentNode, childDataNames, out newParentStr))
            {
                Queue<XmlNode> childrenNodes = null;

                foreach (XmlNode childNode in xmlDocData.DocumentElement.ChildNodes)
                {
                    if (childDataNames.Exists(e => e.Equals(childNode.Name)))
                    {
                        if (childrenNodes == null)
                        {
                            insertPoint = childNode.PreviousSibling;

                            childrenNodes = new Queue<XmlNode>();
                        }
                        childrenNodes.Enqueue(childNode);
                    }
                    else
                    {
                        AddParent(ref childrenNodes, newParentStr, insertPoint, xmlDocData);
                    }
                }

                AddParent(ref childrenNodes, newParentStr, insertPoint, xmlDocData);
            }
        }

        private void ApplyMakeParentM1(XmlNode parentNode, XmlDocument xmlDocData)
        {
            string newParentStr = null;
            List<string> childDataNames = new List<string>();
            XmlNode insertPoint = null;

            if (GetInfoAboutParentTag(parentNode, childDataNames, out newParentStr))
            {
                Queue<XmlNode> childrenNodes = null;

                foreach (XmlNode childNode in xmlDocData.DocumentElement.ChildNodes)
                {
                    if (childDataNames.Exists(e => e.Equals(childNode.Name)))
                    {
                        if (childrenNodes == null)
                        {
                            insertPoint = childNode.PreviousSibling;

                            childrenNodes = new Queue<XmlNode>();
                        }

                        childrenNodes.Enqueue(childNode);
                    }
                }

                AddParent(ref childrenNodes, newParentStr, insertPoint, xmlDocData);
            }
        }

        private void AddParent(ref Queue<XmlNode> childrenNodes, string newParentStr, XmlNode insertPoint, XmlDocument xmlDocData)
        {
            if (childrenNodes != null)
            {
                XmlNode newParentNode = xmlDocData.CreateElement(newParentStr);

                while (childrenNodes.Count > 0)
                {
                    newParentNode.AppendChild(childrenNodes.Dequeue());
                }

                if (insertPoint != null)
                    xmlDocData.DocumentElement.InsertAfter(newParentNode, insertPoint);
                else
                    xmlDocData.DocumentElement.PrependChild(newParentNode);

                childrenNodes = null;
            }
        }

        private bool GetInfoAboutParentTag(XmlNode parentNode, List<string> childDataNames,  out string newParentStr)
        {
            bool res = false;

            newParentStr = null;

            XmlNode newParentNode = parentNode.SelectSingleNode("./name");
            if (newParentNode != null) newParentStr = newParentNode.InnerText;
            if (!string.IsNullOrWhiteSpace(newParentStr))
            {
                XmlNodeList childDataNodes = parentNode.SelectNodes("./tag_name");
                if (childDataNodes.Count > 0)
                {
                    res = true;

                    foreach (XmlNode childDataNode in childDataNodes)
                        childDataNames.Add(childDataNode.InnerText);
                }
            }

            return res;
        }

        private void ApplyAttributeValues(XmlNode manipulationTypeNode, XmlDocument xmlDocData)
        {
            string delimiterStr = "_";
            XmlNode delimiterNode = manipulationTypeNode.SelectSingleNode("./delimiter");
            if (delimiterNode != null) delimiterStr = delimiterNode.InnerText;

            XmlNodeList tagList = xmlDocData.DocumentElement.SelectNodes("/descendant::*[not(descendant::*)]");

            foreach (XmlNode tag in tagList)
            {
                if (tag.Attributes.Count > 0)
                {
                    foreach (XmlAttribute attr in tag.Attributes)
                    {
                        string newNodeName = string.Concat(tag.Name, delimiterStr, attr.Name);

                        XmlNode newNode = xmlDocData.CreateElement(newNodeName);

                        tag.ParentNode.InsertAfter(newNode, tag);

                        newNode.InnerText = attr.Value;
                    }
                }
            }
        }
    }
}
