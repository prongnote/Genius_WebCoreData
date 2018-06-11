using Genius_WebCoreData.App_Class;
using Genius_WebCoreData.App_Class.SSO_Subsidy;
using Genius_WebCoreData.FormModels.SSO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using Web_LED.App_Class;

namespace Genius_WebCoreData.Models.SSO {


    public class M_SSO_SendDataForm {

        private SSO_Class ssoClass = new SSO_Class(); 
        private static XmlDocument CreateSoapEnvelope() {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                                            <soapenv:Header/>
                                                <soapenv:Body>
                                                    <tem:getService_SSO_1_10>
                                                        <tem:pathXml>?</tem:pathXml>
                                                    </tem:getService_SSO_1_10>
                                                </soapenv:Body>
                                            </soapenv:Envelope>");
            return soapEnvelopeDocument;
        }

        public bool transferDataXmlToSSO(F_SSO_SendDataForm formModel) {

            return false;
        }
        
        public bool sendDataXmlToSSO(F_SSO_SendDataForm formModel) {
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml(formModel.server_path_xml);

            string url = System.Configuration.ConfigurationManager.AppSettings.Get("GENiusWebServiceCoreData");
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "text/xml; charset=utf-8";
            webRequest.Method = "POST";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
            using (Stream stream = webRequest.GetRequestStream()) {
                soapEnvelopeXml.Save(stream);
            }

            HttpWebResponse webResponse = null;

            webResponse = (HttpWebResponse)webRequest.GetResponse();
            Stream reader = null;
            reader = webResponse.GetResponseStream();
            StreamReader sreader = new StreamReader(reader);
            string result = sreader.ReadToEnd();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result); // suppose that myXmlString contains "<Names>...</Names>"

            //XmlNodeList xmlList = root.SelectNodes("//getReturnFromSSo/GetReturnFromSSo");
            XmlNodeList xmlList = xml.GetElementsByTagName("GetReturnFromSSo");
            Debug.WriteLine(xmlList[0]["DATETIME"].InnerText);
            //Debug.WriteLine(root.GetElementsByTagName("getReturnFromSSo").Count);
            //Debug.WriteLine(xnList.Count);
            /*
            foreach (XmlNode xmlNode in xmlList) {
                Debug.WriteLine(xmlNode["DATETIME"].InnerText);
            }
            */
            return true;


            //XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
            //nsmgr.AddNamespace("tempuri", "http://tempuri.org/"); 
            //XmlNodeList xmlNodeList = xml.SelectNodes("GetReturnFromSSo");
            //var unwrappedResponse = xml.Descendants((XNamespace)"http://schemas.xmlsoap.org/soap/envelope/" + "Body").First().FirstNode();

            //Debug.WriteLine( "----->"+xml.SelectSingleNode("getService_SSO_1_10Response", nsmgr) );
            //Debug.WriteLine(xmlNodeList.Count);


            //XmlDocument xmlTest = new XmlDocument();
            //xmlTest.LoadXml(result); // suppose that myXmlString contains "<Names>...</Names>"
            //XmlElement root = xmlTest.DocumentElement;

            //XmlNamespaceManager nameSpace = new XmlNamespaceManager(xmlTest.NameTable);
            //nameSpace.AddNamespace("", "http://tempuri.org/");



            //xml.SelectSingleNode(); 
            //XmlNode xmlNodeList = xml.SelectSingleNode("/STATUS");

            /*
            foreach (XmlNode node in xmlNodeList) {
                Debug.WriteLine(node["STATUS"].InnerText); 
            }
            Debug.WriteLine(xmlNodeList); 

            XmlNodeList xnList = xml.SelectNodes("/Names/Name");
            foreach (XmlNode xn in xnList) {
                string firstName = xn["FirstName"].InnerText;
                string lastName = xn["LastName"].InnerText;
                Console.WriteLine("Name: {0} {1}", firstName, lastName);
            }
            */

            //return result;


            // return ""; 

            /*
            string url = System.Configuration.ConfigurationManager.AppSettings.Get("GENiusWebServiceCoreData"); 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            try {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream()) {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                return "{error:false}";
            }
            */
        }

        private XmlElement createDocumentHeader(XmlDocument xmlDoc , SubsidyControl subsidyControl) {
            //###########################################   SET HEADER.

            XmlElement documentHeader = xmlDoc.CreateElement(string.Empty, "DocumentHeader", string.Empty);

            XmlElement elementHeaderId = xmlDoc.CreateElement(string.Empty, "ID", string.Empty);
            XmlElement elementHeaderIssueDateTime = xmlDoc.CreateElement(string.Empty, "IssueDateTime", string.Empty);
            XmlElement elementHeaderRegisterId = xmlDoc.CreateElement(string.Empty, "RegistrationID", string.Empty);

            //FIX FOR TEST.
            DateTime dateTimeNow = DateTime.Now;
            //subsidyControl.referenceNumber = SSO_Configs.SSO_REFERENCE_PROFILE+ "000000005";  
            subsidyControl.referenceNumber = ssoClass.getSsoRefNo(); 
            subsidyControl.issueDateTime = String.Format("{0:s}", dateTimeNow);
            subsidyControl.senderRegistrationId = SSO_Configs.SSO_REGISTER_ID;

            XmlText dataHeaderId = xmlDoc.CreateTextNode(subsidyControl.referenceNumber);
            XmlText dataHeaderIssueDateTime = xmlDoc.CreateTextNode(subsidyControl.issueDateTime);
            XmlText dataHeaderRegisterId = xmlDoc.CreateTextNode(subsidyControl.senderRegistrationId); 
            // XmlText dataHeaderRegisterId = xmlDoc.CreateTextNode("TH0030308067580000000000005T1"); //34.Sender Register ID **********

            elementHeaderId.AppendChild(dataHeaderId);
            elementHeaderIssueDateTime.AppendChild(dataHeaderIssueDateTime);
            elementHeaderRegisterId.AppendChild(dataHeaderRegisterId);


            documentHeader.AppendChild(elementHeaderId);
            documentHeader.AppendChild(elementHeaderIssueDateTime);
            documentHeader.AppendChild(elementHeaderRegisterId);

            //###########################################   END SET HEADER.

            return documentHeader;
        }

        private XmlElement createDocumentPartyInformation(XmlDocument xmlDoc, SubsidyControl subsidyControl) {

            XmlElement documentHeader = xmlDoc.CreateElement(string.Empty, "PartyInformation", string.Empty);
            //#################################################################################################   SET TraderParty.

            TraderParty traderParty = new TraderParty();
            //FIX FOR TEST.
            subsidyControl.companyBranch = SSO_Configs.SSO_BRANCH_NUMBER;
            subsidyControl.companyTaxNumber = SSO_Configs.SSO_TAX_NUMBER;
            traderParty.traderPartyTaxInformation = new TraderPartyTaxInformation {
                TaxNumber = subsidyControl.companyTaxNumber,
                BranchCode = subsidyControl.companyBranch,
                BranchName = subsidyControl.companyBranchName
            };
 


            traderParty.traderPartyName = new TraderPartyName { Thai = subsidyControl.companyName, English = subsidyControl.companyEnglishName };

            traderParty.traderPartyAddress = new TraderPartyAddress {
                StreetAndNumber = subsidyControl.streetAndNumber,
                //District = subsidyControl.district,
                District = SSO_Configs.SSO_ADDRESS_DISTRICT,
                SubProvince = subsidyControl.subProvince,
                Province = subsidyControl.province,
                Postcode = subsidyControl.postcode,
                CountryCode = subsidyControl.countryCode
            };

            ssoClass.SSO_ACCOUNT_ID = subsidyControl.ssoAccount;
            traderParty.SSOAccountID = subsidyControl.ssoAccount ; 
            traderParty.traderContractDetail = new TraderContractDetail {
                NamePrefix = "*****", //NamePrefix
                Name = "*****", //Name
                PhoneNumber = subsidyControl.phoneNumber ,
                MobileNumber = "*****", //MobileNumber 
                FaxNumber = subsidyControl.faxNumber ,
                EmailAddress = "*****", //EmailAddress
                Other = "*****",  //Other
            };

            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("", "");

            
            XmlDocument xmlDocTraderParty = new XmlDocument();
            XmlSerializer xmlSerializerTraderParty = new XmlSerializer(typeof(TraderParty));
            using (XmlWriter writer = xmlDocTraderParty.CreateNavigator().AppendChild()) {
                xmlSerializerTraderParty.Serialize(writer, traderParty, xmlSerializerNamespaces);
            }
            //XmlNode nodeSwitch = xmlDocTraderParty.SelectSingleNode("/TraderParty");



            documentHeader.AppendChild(documentHeader.OwnerDocument.ImportNode(xmlDocTraderParty.DocumentElement, true));

            //#################################################################################################   SET AccountParty.
            /*
            XmlSerializer xmlSerializerAccountParty = new XmlSerializer(typeof(AccountParty));
            AccountParty accountParty = new AccountParty();
            accountParty.accountPartyTaxInformation = new AccountPartyTaxInformation { TaxNumber = subsidyControl.accountingOfficeTaxNumber , BranchCode = subsidyControl.accountingOfficeBranch};
            XmlDocument xmlDocAccount = new XmlDocument();
            using (XmlWriter writer = xmlDocAccount.CreateNavigator().AppendChild()) {
                xmlSerializerAccountParty.Serialize(writer, accountParty, xmlSerializerNamespaces);
            }
            documentHeader.AppendChild(documentHeader.OwnerDocument.ImportNode(xmlDocAccount.DocumentElement, true));
            */

            //documentHeader.AppendChild(documentAccountParty); 
            return documentHeader;


        }

        private XmlElement createDocumentEmployerInformation(SubsidyControl subsidyControl) {

            EmployerInformation employerInformation = new EmployerInformation {
                EmployerID = subsidyControl.employerIdCard,
                EmployerName = subsidyControl.employerName,
                EmployerPosition = subsidyControl.employerPosition
            }; 
            
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("", "");

            XmlDocument xmlDocument = new XmlDocument();
            XmlSerializer xmlSerializer= new XmlSerializer(typeof(EmployerInformation));
            using (XmlWriter writer = xmlDocument.CreateNavigator().AppendChild()) {

                xmlSerializer.Serialize(writer, employerInformation, xmlSerializerNamespaces);
            }
            //xmlDoc.AppendChild(xmlDoc.OwnerDocument.ImportNode(xmlDocument.DocumentElement, true));

            return xmlDocument.DocumentElement; 
        }

        private XmlElement createDocumentPaymentInformation(SubsidyControl subsidyControl) {
            /*
            PaymentInformation paymentInformation = new PaymentInformation {
                BankCode = subsidyControl.bankCode,
                BankBranchCode = subsidyControl.bankBranchCode,
                AccountNumber = subsidyControl.bankAccountNumber,
                PaymentAmount = new PaymentAmount { paymentAmount = subsidyControl.paymentAmount, currencyID = "THB" }
            };
            */
            PaymentInformation paymentInformation = new PaymentInformation {
                BankCode = SSO_Configs.SSO_BANKCODE,
                BankBranchCode = SSO_Configs.SSO_BANK_BRANCH_CODE,
                AccountNumber = SSO_Configs.SSO_BANK_ACCOUNT_NUMBER,
                PaymentAmount = new PaymentAmount { paymentAmount = subsidyControl.paymentAmount, currencyID = "THB" }
            };

            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("", "");

            XmlDocument xmlDocument = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PaymentInformation));
            using (XmlWriter writer = xmlDocument.CreateNavigator().AppendChild()) {
                xmlSerializer.Serialize(writer, paymentInformation, xmlSerializerNamespaces);
            }

            return xmlDocument.DocumentElement;
        }
        
        private XmlElement createDocumentSubsidyInformation(SubsidyControl subsidyControl , SubsidySsoAccountBranchControl accountBranchControl ,string[] lines) {

            SubsidyInformation subsidyInformation = new SubsidyInformation();
            subsidyInformation.SubsidyRate = subsidyControl.subsidyRate;
            subsidyInformation.SubsidyMonth = "--" + subsidyControl.subsidyMonth+ "--";
            subsidyInformation.SubsidyYear = subsidyControl.subsidyYear;
            //subsidyInformation.TotalWageAmount = subsidyControl.totalWageAmount;
            subsidyInformation.TotalWageAmount = new TotalWageAmount { totalWageAmount = subsidyControl.totalWageAmount, currencyID = "THB" };
            subsidyInformation.TotalEmployeeSubsidyAmount = new TotalEmployeeSubsidyAmount { totalEmployeeSubsidyAmount = subsidyControl.totalEmployeeSubsidyAmount, currencyID = "THB" };
            subsidyInformation.TotalEmployerSubsidyAmount = new TotalEmployerSubsidyAmount { totalEmployerSubsidyAmount = subsidyControl.totalEmployerSubsidyAmount, currencyID = "THB" };
            subsidyInformation.TotalSubsidyAmount = new TotalSubsidyAmount { totalSubsidyAmount = subsidyControl.totalSubsidyAmount, currencyID = "THB" };
            subsidyInformation.TotalEmployeeNumber = subsidyControl.totalNumberOfEmployee;

            //########################  Set Data SsoClass.
            ssoClass.SUBSIDY_RATE = subsidyControl.subsidyRate;
            ssoClass.SUBSIDY_MONTH = subsidyControl.subsidyMonth ;
            ssoClass.SUBSIDY_YEAR = subsidyControl.subsidyYear;
            ssoClass.TOTAL_WAGE_AMOUNT = subsidyControl.totalWageAmount ;
            ssoClass.TOTAL_EMPLOYEE_AMOUNT = subsidyControl.totalEmployeeSubsidyAmount;
            ssoClass.TOTAL_EMPLOYER_AMOUNT = subsidyControl.totalEmployerSubsidyAmount;
            ssoClass.TOTAL_EMPLOYER_NO = subsidyControl.totalNumberOfEmployee ; 
            //########################  BranchInformation.
            BranchInformation branchInformation = new BranchInformation();
            branchInformation.branchItem = new BranchItem();
            branchInformation.branchItem.ItemNumber = "1";
            branchInformation.branchItem.SSOBranchCode = accountBranchControl.ssoAccountBranch;
            branchInformation.branchItem.TotalBranchWageAmount = new TotalBranchWageAmount { totalBranchWageAmount = accountBranchControl.totalBranchWageAmount, currencyID = "THB" };
            branchInformation.branchItem.TotalBranchEmployeeSubsidyAmount = new TotalBranchEmployeeSubsidyAmount { totalBranchEmployeeSubsidyAmount = accountBranchControl.totalBranchEmployeeSubsidyAmount, currencyID = "THB" };
            branchInformation.branchItem.TotalBranchEmployerSubsidyAmount = new TotalBranchEmployerSubsidyAmount { totalBranchEmployerSubsidyAmount = accountBranchControl.totalBranchEmployerSubsidyAmount, currencyID = "THB" };
            branchInformation.branchItem.TotalBranchSubsidyAmount = new TotalBranchSubsidyAmount { totalBranchSubsidyAmount = accountBranchControl.totalBranchSubsidyAmount, currencyID = "THB" };
            branchInformation.branchItem.TotalBranchEmployeeNumber = accountBranchControl.totalBranchNumberOfEmployee;
            //branchInformation.branchItem.TotalBranchEmployeeNumber = "1"; 
            //########################  ItemInformation
            ItemInformation itemInformation = new ItemInformation();
           
            int countLine = 1;
            List<SubsidyItem> subsidySsoDetails = new List<SubsidyItem>(); 
            foreach (string line in lines) {

                SubsidySsoDetail subsidySsoDetail = new SubsidySsoDetail();
                subsidySsoDetail.setSubsidySsoDetail(line); 

                SubsidyItem subsidyItem = new SubsidyItem();
                subsidyItem.ItemNumber = countLine.ToString() ;
                subsidyItem.WageAmount = new WageAmount { wageAmount = subsidySsoDetail.wageAmount, currencyID = "THB" };
                subsidyItem.SubsidyAmount = new SubsidyAmount { subsidyAmount = subsidySsoDetail.employeeSubsidyAmount, currencyID = "THB" };
                //subsidyItem.WageAmount = new WageAmount { wageAmount = "20000", currencyID = "THB" };
               // subsidyItem.SubsidyAmount = new SubsidyAmount { subsidyAmount = "750", currencyID = "THB" };
                subsidyItem.LastEntry = subsidySsoDetail.lastEntry;

                subsidyItem.employeeInformation = new EmployeeInformation();
                subsidyItem.employeeInformation.EmployeeID = subsidySsoDetail.employeeIdCard;
                subsidyItem.employeeInformation.EmployeeTitle = subsidySsoDetail.employeeTitle;
                subsidyItem.employeeInformation.EmployeeName = subsidySsoDetail.employeeName;
                subsidyItem.employeeInformation.EmployeeLastName = subsidySsoDetail.employeeLastName;
                
                subsidySsoDetails.Add(subsidyItem);
                countLine++;

            }
            itemInformation.subsidyItem = subsidySsoDetails ;

            subsidyInformation.branchInformation = branchInformation;
            subsidyInformation.branchInformation.branchItem.itemInformation = itemInformation; 
            //subsidyInformation.branchInformation = new BranchInformation();



            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("", "");

            XmlDocument xmlDocument = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SubsidyInformation));
            using (XmlWriter writer = xmlDocument.CreateNavigator().AppendChild()) {
                xmlSerializer.Serialize(writer, subsidyInformation, xmlSerializerNamespaces);
            }
            return xmlDocument.DocumentElement;
        }
        
        public bool convertDataToXml(F_SSO_SendDataForm formModel) {

            try {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = xmlDoc.DocumentElement;

                xmlDoc.InsertBefore(xmlDeclaration, root);

                XmlElement elementBody = xmlDoc.CreateElement(string.Empty, "SSOMessageSubsidy", string.Empty);

                xmlDoc.AppendChild(elementBody);
                elementBody.SetAttribute("xmlns", "urn:th:sso:xsd:subsidy:1");
                elementBody.SetAttribute("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");


                string[] lines = File.ReadAllLines(formModel.server_path_decode);
                string firstLine = lines[0]; // gets the first line from file. //#####   FOR CONTROL.
                string secondLine = lines[1]; // gets the second line from file. //#####   FOR ACCOUNT BRANCH CONTROL.

                lines = lines.Skip(2).ToArray();

                //########################################################  SET Control.
                SubsidyControl subsidyControl = new SubsidyControl();
                subsidyControl.setSubsidyControl(firstLine);

                SubsidySsoAccountBranchControl accountBranchControl = new SubsidySsoAccountBranchControl();
                accountBranchControl.setSubsidySsoAccountBranchControl(secondLine);

                XmlElement documentHeader = this.createDocumentHeader(xmlDoc, subsidyControl);  // SET HEADER.
                XmlElement documentDetail = xmlDoc.CreateElement(string.Empty, "DocumentDetail", string.Empty);
                XmlElement documentPartyInformation = this.createDocumentPartyInformation(xmlDoc, subsidyControl);  // SET PartyInformation.           
                XmlElement documentEmployerInformation = this.createDocumentEmployerInformation(subsidyControl);  // SET EmployerInformation.
                XmlElement documentSubsidyInformation = this.createDocumentSubsidyInformation(subsidyControl, accountBranchControl, lines);  // SET SubsidyInformation.
                XmlElement documentPaymentInformation = this.createDocumentPaymentInformation(subsidyControl);  // SET PaymentInformation.


                documentDetail.AppendChild(documentPartyInformation);
                documentDetail.AppendChild(documentDetail.OwnerDocument.ImportNode(documentEmployerInformation, true));
                documentDetail.AppendChild(documentDetail.OwnerDocument.ImportNode(documentSubsidyInformation, true));
                documentDetail.AppendChild(documentDetail.OwnerDocument.ImportNode(documentPaymentInformation, true));

                elementBody.AppendChild(documentHeader);
                elementBody.AppendChild(documentDetail);

                //########################################################  END SET Control.

                string pathSaveXml = formModel.server_path_xml;
                try {
                    //string xmlFileName = pathSaveXml + "/" + formModel.server_file_name.Split('.')[0] + ".xml";
                    //string xmlFileName = formModel.server_file_name.Split('.')[0] + ".xml";

                    //######################################    SET SsoClass.

                    ssoClass.SEND_STATUS = "0";
                    ssoClass.SEND_RESPONSE = "";

                    string inv_no = ssoClass.getSsoInvNo();
                    ssoClass.XML_PATH = "XML_SEND/1/" + DateTime.Now.ToString("yyyy_MM") ;
                    ssoClass.XML_NAME = ssoClass.generateXmlFileName() ; 
                    ssoClass.insertDataSsoDataSend();
                    //ssoClass.XML_NAME = xmlFileName;

                    string xmlFilePathToSso = formModel.server_path_plugins + "/"+ ssoClass.XML_PATH;
                   
                    if (!Directory.Exists(xmlFilePathToSso)) {
                        Directory.CreateDirectory(xmlFilePathToSso);
                    }
                    

                    xmlDoc.Save(formModel.server_path_xml+"/"+ ssoClass.XML_NAME);
                    xmlDoc.Save(xmlFilePathToSso + "/"+ ssoClass.XML_NAME);
                    
                    string appPath = formModel.server_path_send_exe ;
                    //ProcessStartInfo processInfo = new ProcessStartInfo(appPath);
                    //Process process = Process.Start(processInfo);

                    string arr = ssoClass.REF_NO_TEXT + "| " + inv_no + "| " + ssoClass.XML_PATH + "/" + ssoClass.XML_NAME;
                    const string argsSeparator = "|";
                    string args = string.Join(argsSeparator, arr);

                    Process.Start(appPath, args.Replace(" | ", " "));
                    //string url = WebConfigurationManager.AppSettings["GENiusWebServiceCoreData"];  

                    //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url+ "/sendDataToSSO_Subsidy_1_10");
                    //Debug.WriteLine("xxxxxxxxxx"); 
                }
                catch (Exception ex) {                  
                    Debug.WriteLine(ex);
                    return false;
                }             
                return true;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                return false; 
            }
        }
       
        public Object getDecodeString(F_SSO_SendDataForm formModel) {

            Dictionary<string, object> jsonReturn = new Dictionary<string, object>();


            // string path = @"D:\TestFiles\test_text_007.DATII";
            string path = formModel.server_path;
            // This text is added only once to the file.
            if (File.Exists(path)) {

                string readTextEncode = File.ReadAllText(path);

                var base64EncodedBytes = System.Convert.FromBase64String(readTextEncode);
                string textDecode = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                string pathSaveDecode = formModel.server_path_decode;

                try {
                    File.WriteAllText(pathSaveDecode, textDecode, Encoding.UTF8);
                    Debug.WriteLine(readTextEncode);
                    Debug.WriteLine(textDecode);

                    jsonReturn.Add("textEncode", readTextEncode);
                    jsonReturn.Add("textDecode", textDecode);
                    jsonReturn.Add("pathSaveDecode", pathSaveDecode);
                    jsonReturn.Add("textXmlFileName", formModel.server_file_name);
                    jsonReturn.Add("progressStatus", "1");
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex);
                    jsonReturn.Add("Failure", ex);
                }

            }

            return jsonReturn;
        }

    }


}