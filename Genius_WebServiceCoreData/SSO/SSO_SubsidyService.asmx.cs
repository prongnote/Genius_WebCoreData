using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Genius_WebServiceCoreData.SSO {
    /// <summary>
    /// Summary description for SSO_SubsidyService
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    public class MainDataReturn {
        public List<GetReturnFromSSo> getReturnFromSSo { get; set; }
    }

    public class GetReturnFromSSo {
        public string STATUS { get; set; }
        public string DATETIME { get; set; }
        public string CODE { get; set; }
        public int randomInt { get; set; }
    }


    public class SSO_SubsidyService : System.Web.Services.WebService {

        [WebMethod]
        [SoapDocumentMethod]
        public MainDataReturn getService_SSO_1_10(string pathXml) {

            XmlDocument xmlDoc = new XmlDocument();
            pathXml = @"d:\documents\visual studio 2015\Projects\Genius_WebCoreData\Genius_WebCoreData\Uploads\TestFileXml\xxx.xml";
            XmlElement element = null;

            XmlElement elementReturnBody = null;

            //TEST.
            List<GetReturnFromSSo> getReturnFromSSos = new List<GetReturnFromSSo>();
            GetReturnFromSSo getReturnFromSSo = new GetReturnFromSSo();
            Random rnd = new Random();
            getReturnFromSSo.STATUS = "true";
            getReturnFromSSo.DATETIME = DateTime.Now.ToString();
            getReturnFromSSo.CODE = "ACdiii___1023";

            getReturnFromSSo.randomInt = rnd.Next(0, 50000);
            getReturnFromSSos.Add(getReturnFromSSo);
            //getReturnFromSSo.randomInt = rnd.Next(0, 50000);
            //getReturnFromSSos.Add(getReturnFromSSo);
            //getReturnFromSSo.randomInt = rnd.Next(0, 50000);
           // getReturnFromSSos.Add(getReturnFromSSo);
            //XmlSerializer xmlSerializer = new XmlSerializer(getReturnFromSSo.GetType());

            MainDataReturn mainDataReturn = new MainDataReturn();
            mainDataReturn.getReturnFromSSo = getReturnFromSSos;
            return mainDataReturn;
            /*  
             using (StringWriter textWriter = new StringWriter()){

                  xmlSerializer.Serialize(textWriter, getReturnFromSSo);

                  return textWriter;
                  /*
                  StringReader stringWriter = new StringReader(textWriter);
                  Object _copy = xmlSerializer.Deserialize(textWriter);
                  //return _copy;
                  //XmlWriter writer = new XmlTextWriter(textWriter);
                  Debug.WriteLine( xmlSerializer.ToString());
                 return writer; 
                
        }

            
                    try { 
                       xmlDoc.Load(pathXml);
                       element = xmlDoc.DocumentElement;

                       elementReturnBody = xmlDoc.CreateElement(string.Empty, "RETURN_DATA", string.Empty);
                       XmlElement elementReturnData = xmlDoc.CreateElement(string.Empty, "STATUS", string.Empty);

                       XmlText returnDataText = xmlDoc.CreateTextNode("true");
                       elementReturnData.AppendChild(returnDataText);
                       elementReturnBody.AppendChild(elementReturnData);

                   }
                   catch (Exception ex) {
                       Debug.WriteLine(ex); 
                   }
            
            return elementReturnBody;

            // return xmlDoc.OuterXml; 
            //  StreamReader sreader = new StreamReader(xmlDoc.OuterXml.ToString());
            //  string result = sreader.ReadToEnd();
            //  return HttpUtility.HtmlDecode(result);  ; 
            */

        }

        private static XmlDocument CreateSoapEnvelope() {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://www.webserviceX.NET"">
                                            <soapenv:Body >
                                                  <web:GetCitiesByCountry>
                                                     <web:CountryName>THAILAND</web:CountryName>
                                                  </web:GetCitiesByCountry>
                                               </soapenv:Body>
                                            </soapenv:Envelope> ");
            return soapEnvelopeDocument;
        }

        [WebMethod]
        public string HelloWorld() {
            return "Hello World";
        }

        [WebMethod]
        public XmlDocument sendDataToSSO_Subsidy_1_10() {
            
            string appPath = Path.Combine(Server.MapPath("~/Plugins/SSO_SendData"), "SSO_Subsidy_SendData.exe");  ;
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement documentHeader = xmlDoc.CreateElement(string.Empty, "documentHeader", string.Empty);
            SendDataSsoXml sendDataSsoXml = new SendDataSsoXml();
            sendDataSsoXml.ID = "TH0001055410115490000000001T6";
            sendDataSsoXml.IssueDateTime = DateTime.Now.ToString();
            sendDataSsoXml.RegistrationId = "0291052967";

            try {
                ProcessStartInfo processInfo = new ProcessStartInfo(appPath);
                Process process = Process.Start(processInfo);                
                sendDataSsoXml.DetailResponse = new DetailResponse { ResponseCode = "SUCCESS", ResponseNumber = "00000123", ResponseText = "TEST JA." };
            }
            catch (Exception ex) {
                sendDataSsoXml.DetailResponse = new DetailResponse { ResponseCode = "ERROR", ResponseNumber = "00000123", ResponseText = "TEST ERROR JA."+ex };
                Debug.WriteLine(ex);
            }
            finally {
                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add("", "");
                XmlDocument xmlDocSendDataSsoXml = new XmlDocument();
                XmlSerializer xmlSerializerTraderParty = new XmlSerializer(typeof(SendDataSsoXml));
                using (XmlWriter writer = xmlDocSendDataSsoXml.CreateNavigator().AppendChild()) {
                    xmlSerializerTraderParty.Serialize(writer, sendDataSsoXml, xmlSerializerNamespaces);
                }
                xmlDoc.AppendChild(documentHeader.OwnerDocument.ImportNode(xmlDocSendDataSsoXml.DocumentElement, true));
            }

            return xmlDoc ;
        }


        [WebMethod]
        [SoapDocumentMethod]
        public string getResult_CitiesByCountry(string countryName) {
            string url = "http://www.webservicex.com/globalweather.asmx";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.ContentType = "text/xml; charset=utf-8";
            webRequest.Method = "POST";
            //webRequest.ContentLength = 255;
            webRequest.Headers.Add("SOAPAction", "http://www.webserviceX.NET/GetCitiesByCountry");
            webRequest.Accept = "text/xml";
            webRequest.Host = "www.webservicex.com";


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


            return HttpUtility.HtmlDecode(result);
        }
    }
}
