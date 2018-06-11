using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Genius_WebCoreData.App_Class.SSO_Subsidy {

    [XmlRoot("AccountParty")]
    public class AccountParty {
        [XmlElement("TaxInformation")]
        public AccountPartyTaxInformation accountPartyTaxInformation; 
    }

    [XmlRoot("TaxInformation")]
    public class AccountPartyTaxInformation {
        [XmlElement("TaxNumber")]
        public string TaxNumber { get; set; }

        [XmlElement("BranchCode")]
        public string BranchCode { get; set; }
        

    }

    
}