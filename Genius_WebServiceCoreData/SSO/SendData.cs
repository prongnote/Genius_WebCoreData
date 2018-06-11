using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Genius_WebServiceCoreData.SSO {


    [XmlRoot("DocumentHeder")]
    public class SendDataSsoXml {
            
            public string ID { get; set; }
            public string IssueDateTime { get; set; }
            public string RegistrationId { get; set; }

            public DetailResponse DetailResponse { get; set; }

    }
    public class DetailResponse {
        public string ResponseNumber { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseText { get; set; }

    }
}