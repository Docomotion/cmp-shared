using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using Docomotion.Shared.ComDef;
using Docomotion.Shared.ErrorHandler;
using Docomotion.Shared.UtlFFP.NewFFP.Tools;

namespace Docomotion.Shared.UtlFFP.NewFFP
{
    class InternalCreateNewFFP : BaseFFPUtl
    {
        XmlDocument m_MainHeaderDoc = new XmlDocument();
        FFPSection1 m_oFFPFileHeader = new FFPSection1();
        protected byte[] m_DataBuffer = null;
        protected byte[] m_TagsBuffer = null;
        string m_szFFSXmlList = null;
        protected BinaryReader m_oReader = null;
        protected string m_szOutputDirectory = "";
        protected string m_szFFPFileName = "";
        protected string m_szFFPTTempFileName ="";
        CFFPHeader m_FFPHeader = null;

        public long TotatCompressTime = 0;

        Tools.Crc16 m_oCRC16 = new Crc16();

        public int CreatePackage(string szInputDir, string szOutputDir, string szWorkingDir, bool bOverWrite, ref CFFPHeader pPackageHeader, string szFFSXmlList, ref string szErrorString, ref string szAdditionalInfo)
        {

            LogObject.Log("Enter CreatePackage");
            int iErrorCode = Lapsus.FF_NO_ERRORS;
            try
            {
                m_FFPHeader = pPackageHeader;

                init(szInputDir,szOutputDir,szWorkingDir,szFFSXmlList,bOverWrite,pPackageHeader);                 

                // save THe FFS list to the same header
                LogObject.Log("before  CreateFFRScriptlist");
                CreateFFRScriptlist();

                LogObject.Log("before  UpdateDataObjectsAccordingTofiles");
				UpdateDataObjectsAccordingTofiles(szInputDir, szWorkingDir);

                LogObject.Log("before  UpdateProjectInHeaderFile");
                UpdateProjectInHeaderFile(szWorkingDir);


                LogObject.Log("before  CompressData");
                // zip the xml header 
                byte[] l_oZippedheader = null;
                byte[] l_oInXmlHeader = System.Text.Encoding.UTF8.GetBytes(m_MainHeaderDoc.OuterXml);
                CompressDataZ7(ref l_oZippedheader, ref l_oInXmlHeader);

                // update ffpheader attributes 
                m_oFFPFileHeader.m_FFPFormat.FileHeaderSize = Convert.ToUInt32(l_oInXmlHeader.Length);
                m_oFFPFileHeader.m_FFPFormat.FormFieldsDescriptionsSegments = m_oFFPFileHeader.m_FFPFormat.PackageDescriptionSegment + Convert.ToUInt32(l_oZippedheader.Length);

                uint l_idataSegmentoffset = m_oFFPFileHeader.m_FFPFormat.FormFieldsDescriptionsSegments;
                if (m_TagsBuffer != null)
                    l_idataSegmentoffset += Convert.ToUInt32(m_TagsBuffer.Length);                    

                m_oFFPFileHeader.m_FFPFormat.FormsDataSegments = Convert.ToUInt32(l_idataSegmentoffset);

                uint l_TagsLength = 0;
                if (null != m_TagsBuffer)
                    l_TagsLength = Convert.ToUInt32(m_TagsBuffer.Length);

                m_oFFPFileHeader.m_FFPFormat.FileSize = FFPFormat.Sizeof() + 
                    m_oFFPFileHeader.m_FFPFormat.FormsCount * 
                    (FFPFormsHeaderSegmentDirectory.Sizeof() + FormsHeaderSegments.Sizeof() + FormsHeaderSegmentFormat.Sizeof()) + 
                    Convert.ToUInt32(m_DataBuffer.Length) +
                    l_TagsLength +
                    Convert.ToUInt32(l_oZippedheader.Length);

                
                // write to file 

                FileStream l_oOutputFile = new FileStream(string.Format("{0}\\{1}", szWorkingDir, m_szFFPTTempFileName), FileMode.Create, FileAccess.Write);

                BinaryWriter l_oWriter = new BinaryWriter(l_oOutputFile);

                

                m_oFFPFileHeader.WriteToBuffer(l_oWriter);
                l_oWriter.Write(l_oZippedheader);
                if (null != m_TagsBuffer)
                    l_oWriter.Write(m_TagsBuffer);
                l_oWriter.Write(m_DataBuffer);

                l_oOutputFile.Close();

                LogObject.Log("before  MoveFile");
                MoveFile(bOverWrite, szOutputDir, szWorkingDir);

                
                              
            }
            catch (ExceptionFFEngine FFPException)
            {
                LogObject.Log(szErrorString);
                iErrorCode = ExceptionFFEngine.GetException(FFPException, Lapsus.Packager.ERR_PACKAGE_UTIL_COMMON, ref m_szErrorMsg);

            }
            catch (Exception ex)
            {
                ErrorWrapper err = new ErrorWrapper(ex);
                iErrorCode = (int)(((ushort)err.ErrorCode & 0x0000FFFF | (uint)(Lapsus.Packager.ERR_CREATE_FFP_FATAL_ERROR) << 16));
                szErrorString = Lapsus.ErrorsMsg.STR_ERR_CREATE_FFP_FATAL_ERROR;
                szAdditionalInfo = ex.Message;
            }           

            return iErrorCode;           

        }

        public InternalCreateNewFFP()
        {
            char[] l_ModuleType = new char[4];
            l_ModuleType[0] = 'F';
            l_ModuleType[1] = 'F';
            l_ModuleType[2] = 'P';
            l_ModuleType[3] = 'S';

            m_oFFPFileHeader.m_FFPFormat.ModuleType = l_ModuleType;
            m_oFFPFileHeader.m_FFPFormat.FileFormat = new byte[1];
            m_oFFPFileHeader.m_FFPFormat.FileFormat[0] = (byte)1;


        }

        public void init(string szInputDir, string szOutputDir, string szWorkDirectory, string szFFSXmlList, bool bOverWrite, CFFPHeader FFPHeader)
        {
            // check incoming parameters 
            CheckParamters(ref FFPHeader);

            m_szFFSXmlList = szFFSXmlList;
            // save parameters to the FFP header
            SaveFFPparameters(ref FFPHeader);

            // build the file name to write to
            
            if (string.IsNullOrEmpty(szInputDir))
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_INPUT_DIRECTORY_NOT_DEFINE, 0, Lapsus.ErrorsMsg.STR_ERR_INPUT_DIRECTORY_NOT_DEFINE, null);
            }

            if (szInputDir[szInputDir.Length - 1] != '\\')
            {
                szInputDir += "\\";
            }

            //check if out dir is define
            if (string.IsNullOrEmpty(szOutputDir))
            {
                szOutputDir = szInputDir;

            }

            BuildOutDirectoryAndFileName(szOutputDir);

        }

        protected void BuildOutDirectoryAndFileName(string szOutFileName)
        {
            FileAttributes FFPAttributes = FileAttributes.Normal;
            int index = 0;

            try
            {
                FFPAttributes = File.GetAttributes(szOutFileName);

            }
            catch (ArgumentException ex)
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_OUT_DIRECTORY_OR_FILE_NAME_IS_CORRUPTED, 0, string.Format(Lapsus.ErrorsMsg.STR_ERR_OUT_FILE_OR_DIRECTORY_ATTRIBUTE_UNKNOWN, szOutFileName), ex);
            }
            catch (PathTooLongException ex)
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_OUT_DIRECTORY_OR_FILE_NAME_IS_CORRUPTED, 0, szOutFileName, ex);
            }
            catch (NotSupportedException ex)
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_OUT_DIRECTORY_OR_FILE_NAME_IS_CORRUPTED, 0, szOutFileName, ex);
            }
            catch (FileNotFoundException)
            {
                //Get directory  path
                index = szOutFileName.LastIndexOf("\\");
                m_szOutputDirectory = szOutFileName.Substring(0, index + 1);
                //Build The FFP file Name
                m_szFFPFileName = szOutFileName.Substring(index + 1);
                //Build the Temp FFP file name
                m_szFFPTTempFileName = m_szFFPFileName + Definition.FFP_TEMP_EXTSION;
                return;
            }
            catch (DirectoryNotFoundException ex)
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_OUT_DIRECTORY_OR_FILE_NAME_IS_CORRUPTED, 0, szOutFileName, ex);
            }

            if ((FileAttributes.Directory & FFPAttributes) == FileAttributes.Directory)
            {
                m_szOutputDirectory = szOutFileName;
                //build FFP file name 
                BuildFFPFileName();
                //Build the Temp FFP file name
                m_szFFPTTempFileName = m_szFFPFileName + Definition.FFP_TEMP_EXTSION;

            }
            else if ((FileAttributes.Archive & FFPAttributes) == FileAttributes.Archive)
            {
                index = szOutFileName.LastIndexOf("\\");
                m_szOutputDirectory = szOutFileName.Substring(0, index + 1);
                m_szFFPFileName = szOutFileName.Substring(index + 1);
            }
            else
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_OUT_FILE_OR_DIRECTORY_ATTRIBUTE_UNKNOWN, 0, Lapsus.ErrorsMsg.STR_ERR_OUT_FILE_OR_DIRECTORY_ATTRIBUTE_UNKNOWN, null);
            }
        }

        /// <summary>
        /// Build the FFP file name
        /// </summary>
        protected void BuildFFPFileName()
        {
            if (m_FFPHeader.iVersion == 0)
            {
                m_szFFPFileName = string.Format("ff{0}.ffp", m_FFPHeader.szApplicationSymbol);
            }
            else
            {
                m_szFFPFileName = string.Format("ff{0}{1:0000}.ffp", m_FFPHeader.szApplicationSymbol, m_FFPHeader.iVersion);
            }
        }
        public void SaveFFPparameters(ref CFFPHeader pPackageHeader)
        {
            //add Xml Declaration
            XmlDeclaration xmlDeclaration = null;
            XmlElement xmlRoot = null;
            XmlElement xmlHeder = null;


            xmlDeclaration = m_MainHeaderDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");

            m_MainHeaderDoc.AppendChild(xmlDeclaration);

            //add the root Element
            xmlRoot = m_MainHeaderDoc.CreateElement(Definition.FFP_ROOT);

            m_MainHeaderDoc.AppendChild(xmlRoot);



            xmlHeder = m_MainHeaderDoc.CreateElement(Definition.FFP_XML_HEADER);

            //add ApplicationSymbol
            //AddNewElemntToXmlObject(Definition.APP_SYMBOL, pPackageHeader.szApplicationSymbol, ref xmlHeder);

            //add  ApplicationSymbolName
            //AddNewElemntToXmlObject(Definition.APP_SYMBOL_NAME, pPackageHeader.szApplicationSymbolName, ref xmlHeder);

            //add PackageName
            AddNewElemntToXmlObject(Definition.PACKGE_NAME, pPackageHeader.szName, ref xmlHeder);

            //add PackageDescription
            AddNewElemntToXmlObject(Definition.PACKGE_DESCRIPTION, pPackageHeader.szDescription, ref  xmlHeder);

            //add SerialNumber
            AddNewElemntToXmlObject(Definition.SERIAL_NUMBER, pPackageHeader.iVersion.ToString(), ref xmlHeder);

            //add date
            AddNewElemntToXmlObject(Definition.FFP_CREATE_DATE, pPackageHeader.szDate, ref  xmlHeder);

            //add Package Form Version

            AddNewElemntToXmlObject(Definition.FFR_FORM_VERSION, Definition.PACKAGE_VERSION, ref xmlHeder);

            AddNewElemntToXmlObject(Definition.FFR_FILE_COUNT, "0", ref  xmlHeder);

            //create the FFR List Root Object 
            AddNewElemntToXmlObject(Definition.FFR_FILE_LIST, "", ref xmlRoot);

            xmlRoot.AppendChild(xmlHeder);

            m_oFFPFileHeader.m_FFPFormat.ProjectSymbol = pPackageHeader.szApplicationSymbol.ToCharArray();
            m_oFFPFileHeader.m_FFPFormat.SerialNumber = Convert.ToUInt16(pPackageHeader.iVersion);

            if ( pPackageHeader.szFormatVersion.Length < 7) // 7 is the min length of string typed "1.2.3.4"
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_FFR_FILE_MASK, 0, Lapsus.ErrorsMsg.STR_ERR_FFR_FILE_MASK, null);                
            }

            try
            {
            
                string l_sFormatVersion = pPackageHeader.szFormatVersion;
                byte[] l_itemp = new byte[4];
                // compute each number to get the bitwize 
                int l_iLocation = l_sFormatVersion.IndexOf('.');
                int l_iPrevLocation = l_iLocation;

                l_itemp[0] = Convert.ToByte(l_sFormatVersion.Substring(0, l_iLocation));

                l_iLocation = l_sFormatVersion.IndexOf('.', l_iLocation+1);
                l_iPrevLocation++;

                l_itemp[1] = Convert.ToByte(l_sFormatVersion.Substring(l_iPrevLocation, l_iLocation - l_iPrevLocation));

                l_iPrevLocation = l_iLocation;

                l_iPrevLocation++;
                l_iLocation = l_sFormatVersion.IndexOf('.', l_iLocation+1);

                l_itemp[2] = Convert.ToByte(l_sFormatVersion.Substring(l_iPrevLocation, l_iLocation - l_iPrevLocation));

                l_iPrevLocation = l_iLocation;
                l_iPrevLocation++;
                string l_sTemp = l_sFormatVersion.Substring(l_iPrevLocation, l_sFormatVersion.Length - l_iPrevLocation);
                l_itemp[3] = Convert.ToByte(l_sFormatVersion.Substring(l_iPrevLocation, l_sFormatVersion.Length - l_iPrevLocation));

                m_oFFPFileHeader.m_FFPFormat.FileFormat = l_itemp; 

            }
            catch (System.Exception e)
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_FFR_FILE_MASK, 0, string.Format(Lapsus.ErrorsMsg.STR_ERR_FFR_FILE_MASK, pPackageHeader.szFormatVersion), e);
            }
            
           
            



        }
        protected void AddNewElemntToXmlObject(string Name, string value, ref XmlElement Root)
        {
            XmlElement NewElement = null;

            NewElement = m_MainHeaderDoc.CreateElement(Name);

            NewElement.InnerText = value;

            Root.AppendChild(NewElement);
        }

        protected void CheckParamters(ref CFFPHeader FFPHeader)
        {
           
            if (FFPHeader.szDate == null || FFPHeader.szDate.Length == 0)
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_INVALID_PARAMETERS_IN_CREATE_FFP, 0, Lapsus.ErrorsMsg.STR_ERR_INVALID_PARAMETERS_IN_CREATE_FFP, null);
            }
        }

        protected void AddFFRXmlHeaderToMainHeader(string a_sfileName, ref XmlDocument a_oCurrentFile)
        {
            // get the forms header data as string              
           
            try
            {
                a_oCurrentFile.Load(a_sfileName);
            }
            catch (XmlException ex)
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_FILE_IS_NOT_XML, 0, Lapsus.ErrorsMsg.STR_ERR_FILE_IS_NOT_XML, ex);
            }
            catch (Exception ex)
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_FILE_IS_NOT_XML, 0, Lapsus.ErrorsMsg.STR_ERR_FILE_IS_NOT_XML, ex);               
            }


            try
            {

                // remove the project section from the xml 

                
//                 string szXPath = string.Format("/{0}/{1}",Definition.FREE_FORM_PROPERTIES, Definition.FFR_PROJECT);
//                 XmlNode FFPProject = a_oCurrentFile.SelectSingleNode(szXPath);
//                 FFPProject.RemoveAll();

                XmlNode RootNode = a_oCurrentFile.FirstChild;
                string szXPath = string.Format("/{0}/{1}", Definition.FFP_ROOT, Definition.FFR_FILE_LIST);
                XmlNode FFPRoot = m_MainHeaderDoc.SelectSingleNode(szXPath);

                
                RootNode = m_MainHeaderDoc.ImportNode(RootNode, true);

                FFPRoot.AppendChild(RootNode);
            }
            catch (System.Exception ex)
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_FAIL_TO_UPDATE_PROJECT_IN_HEADER_FILE, 0, Lapsus.ErrorsMsg.STR_ERR_FAIL_TO_UPDATE_PROJECT_IN_HEADER_FILE, ex);                   
            }
            
        }

        public void CreateFFRScriptlist()
        {
            XmlDocument mFFSDocument = new XmlDocument();
            XmlNode RootNode = null;
            XmlNode FFPRoot = null;

            //load the FFS xml string to xml Object 

            try
            {
                if (string.IsNullOrEmpty(m_szFFSXmlList))
                {
                    return;
                }

                mFFSDocument.LoadXml(m_szFFSXmlList);
            

                RootNode = mFFSDocument.FirstChild;
                string szXPath = string.Format("/{0}", Definition.FFP_ROOT);
                FFPRoot = m_MainHeaderDoc.SelectSingleNode(szXPath);



                //add the FFS list into the FFP xml object 
                RootNode = m_MainHeaderDoc.ImportNode(RootNode, true);

                FFPRoot.AppendChild(RootNode);


                XmlElement FFPHeaderRoot = null;
                FFPHeaderRoot = (XmlElement)m_MainHeaderDoc.SelectSingleNode(string.Format("/{0}", Definition.FFP_ROOT));

                
            }
            catch (Exception ex)
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_FAIL_TO_LOAD_FFS_TO_HEADER, 0, Lapsus.ErrorsMsg.STR_ERR_FAIL_TO_LOAD_FFS_TO_HEADER, ex);

            }
        }


        public void UpdateDataObjectsAccordingTofiles(string a_szInputDir , string a_szWorkingdirectory)
        {         
			// get all the files in the input directory 
			string l_sSearchInputString = string.Format("FF{0}*.ffr", m_FFPHeader.szApplicationSymbol);

			string [] l_sInputFileList = Directory.GetFiles(a_szInputDir, l_sSearchInputString, SearchOption.TopDirectoryOnly);
            string ffrsTxtFile = Path.Combine(a_szInputDir, "..", "FilesNotToPackage.txt");
            if (File.Exists(ffrsTxtFile))
            {
                ArrayList tempArr = new ArrayList(l_sInputFileList);
                string[] ffrsFromTxt = File.ReadAllLines(ffrsTxtFile);
                foreach(string s in ffrsFromTxt)
                {
                    if (tempArr.Contains(s))
                    {
                        tempArr.Remove(s);
                    }
                }
                if(tempArr.Count != l_sInputFileList.Length)
                {
                    l_sInputFileList = (string[])tempArr.ToArray(typeof(string));
                }
            }
			// for each file get its header file from the working directory
			m_oFFPFileHeader.init(Convert.ToUInt32(l_sInputFileList.Length));    
			foreach (string st in l_sInputFileList)
            {
				// directory \filename + ffheader extension
				string l_sFileName = string.Format("{0}\\{1}{2}", Path.GetFileNameWithoutExtension(st), Path.GetFileNameWithoutExtension(st), Definition.FF_HEADER_FILE_EXT);

				string l_sFileNameInWorkingdirectory = Path.Combine(a_szWorkingdirectory, l_sFileName);

				if (!File.Exists(l_sFileNameInWorkingdirectory))
				{
					//set error 
                    m_iErrorCode = Lapsus.Packager.ERR_FAIL_TO_FIND_FFR_IN_WORKING_DIR;
					return;
				}
                string l_sSearchString;
                ushort l_usCRC = 0;
                //l_sSearchString = string.Format("*{0}", Definition.FF_HEADER_FILE_EXT);
				//string[] l_sFileList = Directory.GetFiles(a_szWorkingdirectory, l_sSearchString, SearchOption.AllDirectories); // form description file 

                                         
//                 foreach (string st in l_sFileList)
//                 {                    
                    XmlDocument l_oCurrentFile = new XmlDocument();

                    // add the FFR xml header to the FFP header
                    LogObject.Log("before AddFFRXmlHeaderToMainHeader");
					AddFFRXmlHeaderToMainHeader(l_sFileNameInWorkingdirectory, ref l_oCurrentFile);
                    


                    // extract data to be updated later with regards to the file
                    uint l_iFormId = 0;
                    uint l_iFormVersion = 0;
                    ushort l_iExpirationDate = 0;


                    LogObject.Log("before ExtractDataFromXmldoc");
                    ExtractDataFromXmldoc(ref l_oCurrentFile, ref l_iFormId, ref l_iFormVersion,ref  l_iExpirationDate);

                    LogObject.Log("after ExtractDataFromXmldoc");
                    // Get the zipped form file 
					string stTemp = l_sFileNameInWorkingdirectory.Replace(Definition.FF_HEADER_FILE_EXT, Definition.FF_DATA_FILE_EXT);

                    uint l_iDataLength = 0;
                    uint l_iRealdataSize = 0;
                    uint l_iRealTagsSize = 0;

                    LogObject.Log("before File.Open::size.txt");//##
                    try
                    {
						FileStream l_oSizeStream = File.Open(Path.Combine(Path.GetDirectoryName(stTemp), "size.txt"), FileMode.Open, FileAccess.Read);

                        StreamReader l_oReader = new StreamReader(l_oSizeStream);

                        string l_sDataRealsize = l_oReader.ReadLine();
                        l_iRealdataSize = Convert.ToUInt32(l_sDataRealsize);
                        string l_sTagsRealsize = l_oReader.ReadLine();
                        l_iRealTagsSize = Convert.ToUInt32(l_sTagsRealsize);
                        string l_sCRCRealsize = l_oReader.ReadLine();
                        l_usCRC = Convert.ToUInt16(l_sCRCRealsize);

                        l_oReader.Close();

                    }
                    catch(Exception ex)
                    {
                        DeleteDirectory(Path.GetDirectoryName(st));
                        m_iErrorCode = Lapsus.RT.ERR_FAIL_TO_FIND_SIZE_FILE;
                        throw ex;
                    }

                    //LogObject.Log("before GetFileLengths");
                    GetFileLengths(stTemp, ref l_iDataLength,ref m_DataBuffer);
                                                
                    // add the info to the 
                    stTemp = stTemp.Replace(Definition.FF_DATA_FILE_EXT, Definition.FF_TAGS_FILE_EXT);
                    uint l_iTagsLength = 0;
                    
                    try
                    {
                        
                        GetFileLengths(stTemp, ref l_iTagsLength, ref m_TagsBuffer);
                    }
                    catch (ExceptionFFEngine ex )
                    {
                       LogObject.Log("Exception GetFileLengths");
                        l_iTagsLength = 0;
                    }
                    
                    //stTemp = stTemp.Substring(stTemp.LastIndexOf('\\') + 1);
                    //stTemp = stTemp.Remove(stTemp.LastIndexOf('.'));
                  
                    //// update data in the header 
                    //LogObject.Log("before UpdateDataSection");
                    m_oFFPFileHeader.UpdateDataSection(l_iFormId, l_iFormVersion, l_usCRC, l_iDataLength, l_iRealdataSize, l_iTagsLength, l_iRealTagsSize, l_iExpirationDate);

                }
            }
           


       

        protected void UpdateProjectInHeaderFile(string a_sDirectory)
        {

            string[] a_sStringArray = Directory.GetFiles(a_sDirectory, "*.ffProject",SearchOption.AllDirectories);
            
            // create the project section in the xml header 
            // 1. load the most recent file 
            try
            {
                XmlDocument l_HeaderXml = new XmlDocument();
                string l_sProjectFileName = a_sStringArray[0];
                l_HeaderXml.Load(l_sProjectFileName);                
                XmlNode l_oProjectNode = l_HeaderXml.FirstChild;

                string l_sXpathFromHeader = string.Format("{0}", Definition.FFP_ROOT);

                XmlNode l_oPlaceToInsert = m_MainHeaderDoc.SelectSingleNode(l_sXpathFromHeader);
                XmlNode l_oPlaceHolder = m_MainHeaderDoc.ImportNode(l_oProjectNode, true);
                l_oPlaceToInsert.AppendChild(l_oPlaceHolder);

                l_sXpathFromHeader = string.Format("{0}/{1}/{2}", Definition.FFP_ROOT, Definition.FFP_XML_HEADER, Definition.FFR_FILE_COUNT);

                
                XmlNode l_oFileCount = m_MainHeaderDoc.SelectSingleNode(l_sXpathFromHeader);
                l_oFileCount.InnerText = Convert.ToString(m_oFFPFileHeader.m_FFPFormat.FormsCount);
//                l_oFileCount.Value =   m_oFFPFileHeader.m_FFPFormat.FormsCount.ToString();
            }
            catch (System.Exception ex)
            {
                LogObject.Log("Exception ERR_FAIL_TO_UPDATE_PROJECT_IN_HEADER_FILE");
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_FAIL_TO_UPDATE_PROJECT_IN_HEADER_FILE, 0, Lapsus.ErrorsMsg.STR_ERR_FAIL_TO_UPDATE_PROJECT_IN_HEADER_FILE, ex);
                
            }
            


        }


        private void DeleteDirectory(string DirectoryToDelete)
        {
            try
            {
                string[] l_sfiles = Directory.GetFiles(DirectoryToDelete);
                foreach (string filePath in l_sfiles)
                {
                    File.Delete(filePath);
                }
                Directory.Delete(DirectoryToDelete);
            }
            catch (Exception ex)
            {

            }

        }

        protected void ExtractDataFromXmldoc(ref XmlDocument a_FFrXml, ref uint a_FormId, ref uint a_FormVersion, ref ushort a_ExpirationData)
        {            

            // extract data from xml file 
            XmlNode l_oIdNode = a_FFrXml.SelectSingleNode("FreeFormProperties/ID");
            if (l_oIdNode == null)
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_FFR_HEADER_IS_CORRUPTED, 0, string.Format(Lapsus.ErrorsMsg.STR_FFR_HEADER_IS_CORRUPTED, "FreeFormProperties/ID"), null);
            }

            a_FormId = Convert.ToUInt32(l_oIdNode.InnerText);

            XmlNode l_FormVersion = a_FFrXml.SelectSingleNode("FreeFormProperties/Version");
            if (l_FormVersion == null)
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_FFR_HEADER_IS_CORRUPTED, 0, string.Format(Lapsus.ErrorsMsg.STR_FFR_HEADER_IS_CORRUPTED, "FreeFormProperties/Version"), null);
            }
            
            XmlNode l_ExpirationData = a_FFrXml.SelectSingleNode("FreeFormProperties/ExpirationDate");
            if (l_FormVersion == null)
            {
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_FFR_HEADER_IS_CORRUPTED, 0, string.Format(Lapsus.ErrorsMsg.STR_FFR_HEADER_IS_CORRUPTED, "FreeFormProperties/ExpirationDate"), null);
            }
            else
            {
                
                LogObject.Log("ExtractDataFromXmldoc deal with Expiration");
                LogObject.Log(string.Format("{0}", l_ExpirationData.InnerText));
                if (l_ExpirationData.InnerText.Length > 0)
                {

                    string l_sDateTime = l_ExpirationData.InnerText;
                    
                    int l_Year = int.Parse(l_sDateTime.Substring(0, 4));
                    
                    int l_Month = int.Parse(l_sDateTime.Substring(4, 2));
                    
                    int l_Day = int.Parse(l_sDateTime.Substring(6, 2));
                    

                    DateTime l_DTExpirationData = new DateTime(l_Year, l_Month, l_Day);

                    int l_SysMonth = int.Parse(Definition.START_SYSTEM_TIME.Substring(0, 2));
                    int l_SysDay = int.Parse(Definition.START_SYSTEM_TIME.Substring(3, 2));
                    int l_SysYear = int.Parse(Definition.START_SYSTEM_TIME.Substring(6, 4));

                    DateTime l_DTStartData = new DateTime(l_SysYear, l_SysMonth, l_SysDay);                                                     
                    
                    TimeSpan l_otimeSpan = (TimeSpan)(l_DTExpirationData - l_DTStartData);
                    

                    a_ExpirationData = (ushort)l_otimeSpan.Days;
                   
                }
                else
                {
                    LogObject.Log("ExpirationData.InnerText.Length=0");
                    a_ExpirationData = ushort.MaxValue;
                }
               
                
            }

            if (l_FormVersion == null)//##
            {
                LogObject.Log("l_FormVersion.InnerText=NULL");
            }
            else
            {
                LogObject.Log(string.Format("l_FormVersion.InnerText:{0}", l_FormVersion.InnerText));
            }


            UInt32.TryParse(l_FormVersion.InnerText, out a_FormVersion);

            //to delete a_FormVersion = Convert.ToUInt32(l_FormVersion.InnerText);

            LogObject.Log(string.Format("ExtractDataFromXmldoc.FormVersion:{0}", a_FormVersion));
        }

        protected void GetFileLengths(string a_sFileName,ref uint a_lLength,ref byte[] a_InnerByteArray)
        {
            byte[] l_oByte = null;

            LoadFileToMem(a_sFileName, ref l_oByte, ref a_lLength);

            //get the CRC of the buffer 
                       
                                                          
            //byte[] l_oZipedByte = null;
            //DateTime l_ostartCompress = DateTime.Now;
            //CompressDataZ7(ref l_oZipedByte, ref l_oByte);
            //DateTime l_oEndCompress = DateTime.Now;

            //TotatCompressTime += l_oEndCompress.Ticks - l_ostartCompress.Ticks;          
            
            long l_lLength = 0;
            byte[] l_iTempByteArray = null;
            if (null != a_InnerByteArray)
            {
                l_lLength = a_InnerByteArray.Length;
                l_iTempByteArray = new byte[l_lLength];
                a_InnerByteArray.CopyTo(l_iTempByteArray, 0);

                
            }
            a_InnerByteArray = new byte[l_lLength + a_lLength];
            if (l_iTempByteArray != null)
                l_iTempByteArray.CopyTo(a_InnerByteArray, 0);

            l_oByte.CopyTo(a_InnerByteArray, l_lLength);                         

        }

        private void CompressDataZ7(ref byte[] a_ZipBuf, ref byte[] A_InData)
        {
            
            AutoFont.SevenZ.Af7Z l_oAf7Z = null;
            

            try
            {
               l_oAf7Z = new AutoFont.SevenZ.Af7Z();
               l_oAf7Z.Compression7Z(A_InData, ref a_ZipBuf);               
            }
            catch (Exception ex)
            {
                LogObject.Log("Exception ERR_FAIL_TO_ZIP_FILE");
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_FAIL_TO_ZIP_FILE, m_iSystemErrorCode, Lapsus.ErrorsMsg.STR_ERR_FAIL_TO_ZIP_FILE, ex);

            }
        }
        
        protected void LoadFileToMem(string a_sFilName, ref byte[] a_sBytearray, ref uint a_lLength)
        {

            if (!File.Exists(a_sFilName))
            {
                DeleteDirectory(Path.GetDirectoryName(a_sFilName));
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_FILE_DOES_NOT_EXIST, 0, string.Format(Lapsus.ErrorsMsg.STR_FILE_DOES_NOT_EXIST, a_sFilName), null);                     
            }
            // read data to mem 

            FileStream l_oFile =  new FileStream(a_sFilName, FileMode.Open, FileAccess.Read);
            BinaryReader l_oReader = new BinaryReader(l_oFile);

            a_lLength = (uint)l_oFile.Length;
            //long l_lFormDataLength = l_oFile.Length;
            byte[] l_oFormDataByteArray = new byte[a_lLength];
            l_oReader.Read(l_oFormDataByteArray, 0, Convert.ToInt32(a_lLength));

            // reset the data holder 

            if (a_sBytearray != null)
            {
                long l_lLength = a_sBytearray.Length;
                byte[] l_TempDatabuffer = new byte[l_lLength];
                l_TempDatabuffer = a_sBytearray;
                a_sBytearray = new byte[l_lLength + l_oFormDataByteArray.Length];
                l_TempDatabuffer.CopyTo(a_sBytearray, 0);
                l_oFormDataByteArray.CopyTo(a_sBytearray, l_lLength);
            }
            else
            {
                a_sBytearray = new byte[l_oFormDataByteArray.Length];
                l_oFormDataByteArray.CopyTo(a_sBytearray, 0);
            }

            l_oFile.Close();
          

        }

       


        public void MoveFile(bool a_bOverWrite , string a_szOutputDir,string a_szWorkingDir)
        {
            string szSourcePath = a_szWorkingDir + '\\' + m_szFFPTTempFileName;
            string szDestination = a_szOutputDir + '\\' + m_szFFPFileName;


            try
            {
                if (File.Exists(szDestination) && a_bOverWrite == true)
                {
                    File.Delete(szDestination);
                }

                File.Move(szSourcePath, szDestination);
            }
            catch (Exception ex)
            {
                LogObject.Log("Exception ERR_CAN_NOT_MOVE_FFP_TEMP_FILE");
                ExceptionFFEngine.ThrowException(Lapsus.Packager.ERR_CAN_NOT_MOVE_FFP_TEMP_FILE, m_iSystemErrorCode, string.Format(Lapsus.ErrorsMsg.STR_ERR_CAN_NOT_MOVE_FFP_TEMP_FILE, szSourcePath, szDestination), ex);
            }
        }

        //public int GetFFR(string szFFPFileName, uint FromID, ref IntPtr pFFRFromBuffer, ref ulong ulFFRFromBuffer, ref string szErrorString)
        //{
        //    FFPUlt l_oUtl = new FFPUlt();
        //    l_oUtl.init(szFFPFileName);


        //    return 0;
        //}
    }
}
