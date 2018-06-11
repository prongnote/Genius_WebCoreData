using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Genius_WebCoreData.App_Class.SSO_Subsidy {


    [XmlRoot("PaymentInformation")]
    public class PaymentInformation {
        
        public string BankCode { get; set; }
        public string BankBranchCode { get; set; }
        public string AccountNumber { get; set; }
        public PaymentAmount PaymentAmount { get; set; }


    }

    public class PaymentAmount {
        [XmlAttribute]
        public string currencyID { get; set; }
        [XmlText]
        public string paymentAmount { get; set; }
    }
}