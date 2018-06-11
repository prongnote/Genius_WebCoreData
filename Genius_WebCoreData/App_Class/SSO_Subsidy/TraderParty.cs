using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Genius_WebCoreData.App_Class.SSO_Subsidy {

    [XmlRoot("TraderParty")]
    public class TraderParty {

        [XmlElement(ElementName = "TaxInformation", Order = 1)]
        public TraderPartyTaxInformation traderPartyTaxInformation;

        [XmlElement(ElementName = "Name", Order = 2)]
        public TraderPartyName traderPartyName;

        [XmlElement(ElementName = "Address", Order = 3)]
       // [XmlElement(Order = 3), XmlElement("Address")]
        public TraderPartyAddress traderPartyAddress;


        [XmlElement(ElementName = "SSOAccountID", Order = 4)]
        //[XmlElement(Order = 4), XmlElement("SSOAccountID")]
        public string SSOAccountID { get; set; }

        [XmlElement(ElementName = "ContractDetail", Order = 5)]
        //[XmlElement(Order = 5), XmlElement("ContractDetail")]
        public TraderContractDetail traderContractDetail; 

        
    }

    [XmlRoot("Name")]
    public class TraderPartyName {
        [XmlElement("Thai")]
        public string Thai { get; set; }

        [XmlElement("English")]
        public string English { get; set; }        

    }

    [XmlRoot("TaxInformation")]
    public class TraderPartyTaxInformation {
        [XmlElement("TaxNumber")]
        public string TaxNumber { get; set; }

        [XmlElement("BranchCode")]
        public string BranchCode { get; set; }

        [XmlElement("BranchName")]
        public string BranchName { get; set; }


    }
    [XmlRoot("ContractDetail")]
    public class TraderContractDetail {

        [XmlElement("NamePrefix")]
        public string NamePrefix { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [XmlElement("MobileNumber")]
        public string MobileNumber { get; set; }
        [XmlElement("FaxNumber")]
        public string FaxNumber { get; set; }
        [XmlElement("EmailAddress")]
        public string EmailAddress { get; set; }
        [XmlElement("Other")]
        public string Other { get; set; }

    }

    public class TraderPartyAddress {

        [XmlElement("StreetAndNumber")]
        public string StreetAndNumber { get; set; }
        [XmlElement("District")]
        public string District { get; set; }
        [XmlElement("SubProvince")]
        public string SubProvince { get; set; }
        [XmlElement("Province")]
        public string Province { get; set; }
        [XmlElement("Postcode")]
        public string Postcode { get; set; }
        [XmlElement("CountryCode")]
        public string CountryCode { get; set; }

    }


}