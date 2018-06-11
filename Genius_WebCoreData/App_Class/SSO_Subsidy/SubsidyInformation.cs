using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Genius_WebCoreData.App_Class.SSO_Subsidy {

    [XmlRoot("SubsidyInformation")]
    public class SubsidyInformation {
        [XmlElement(Order = 1)]
        public string SubsidyRate { get; set; }
        [XmlElement(Order = 2)]
        public string SubsidyMonth { get; set; }
        [XmlElement(Order = 3)]
        public string SubsidyYear { get; set; }
        [XmlElement(Order = 4)]
        public TotalWageAmount TotalWageAmount { get; set; }
        [XmlElement(Order = 5)]
        public TotalEmployeeSubsidyAmount TotalEmployeeSubsidyAmount { get; set; }
        [XmlElement(Order = 6)]
        public TotalEmployerSubsidyAmount TotalEmployerSubsidyAmount { get; set; }
        [XmlElement(Order = 7)]
        public TotalSubsidyAmount TotalSubsidyAmount { get; set; }
        [XmlElement(Order = 8)]
        public string TotalEmployeeNumber { get; set; }

        [XmlElement(ElementName = "BranchInformation", Order = 9)]
        //[XmlElement("BranchInformation")]
        public BranchInformation branchInformation;

    }


    public class TotalWageAmount {
        [XmlAttribute]
        public string currencyID { get; set; }
        [XmlText]
        public string totalWageAmount { get; set; }
    }
    public class TotalEmployeeSubsidyAmount {
        [XmlAttribute]
        public string currencyID { get; set; }
        [XmlText]
        public string totalEmployeeSubsidyAmount { get; set; }
    }
    public class TotalEmployerSubsidyAmount {
        [XmlAttribute]
        public string currencyID { get; set; }
        [XmlText]
        public string totalEmployerSubsidyAmount { get; set; }
    }
    public class TotalSubsidyAmount {
        [XmlAttribute]
        public string currencyID { get; set; }
        [XmlText]
        public string totalSubsidyAmount { get; set; }
    }

    
    

    [XmlRoot("BranchInformation")]
    public class BranchInformation {
        [XmlElement("BranchItem")]
        public BranchItem branchItem;
    }

    [XmlRoot("BranchItem")]
    public class BranchItem {
        [XmlElement(Order = 1)]
        public string ItemNumber { get; set; }
        [XmlElement(Order = 2)]
        public string SSOBranchCode { get; set; }
        [XmlElement(Order = 3)]
        public TotalBranchWageAmount TotalBranchWageAmount { get; set; }
        [XmlElement(Order = 4)]
        public TotalBranchEmployeeSubsidyAmount TotalBranchEmployeeSubsidyAmount { get; set; }
        [XmlElement(Order = 5)]
        public TotalBranchEmployerSubsidyAmount TotalBranchEmployerSubsidyAmount { get; set; }
        [XmlElement(Order = 6)]
        public TotalBranchSubsidyAmount TotalBranchSubsidyAmount { get; set; }
        [XmlElement(Order = 7)]
        public string TotalBranchEmployeeNumber { get; set; }
        [XmlElement(ElementName = "ItemInformation", Order = 8)]
        //[XmlElement("ItemInformation")]
        public ItemInformation itemInformation;
    }

    public class TotalBranchWageAmount {
        [XmlAttribute]
        public string currencyID { get; set; }
        [XmlText]
        public string totalBranchWageAmount { get; set; }
    }
    public class TotalBranchEmployeeSubsidyAmount {
        [XmlAttribute]
        public string currencyID { get; set; }
        [XmlText]
        public string totalBranchEmployeeSubsidyAmount { get; set; }
    }
    public class TotalBranchEmployerSubsidyAmount {
        [XmlAttribute]
        public string currencyID { get; set; }
        [XmlText]
        public string totalBranchEmployerSubsidyAmount { get; set; }
    }
    public class TotalBranchSubsidyAmount {
        [XmlAttribute]
        public string currencyID { get; set; }
        [XmlText]
        public string totalBranchSubsidyAmount { get; set; }
    }





    [XmlRoot("ItemInformation")]
    public class ItemInformation {
        [XmlElement("SubsidyItem")]
        public List<SubsidyItem> subsidyItem;
    }


    [XmlRoot("SubsidyItem")]
    public class SubsidyItem {
        [XmlElement(Order = 1)]
        public string ItemNumber { get; set; }
        [XmlElement(ElementName = "EmployeeInformation", Order = 2)]
        //[XmlElement("EmployeeInformation")]
        public EmployeeInformation employeeInformation;
        [XmlElement(Order = 3)]
        public WageAmount WageAmount { get; set; }
        [XmlElement(Order = 4)]
        public SubsidyAmount SubsidyAmount { get; set; }
        [XmlElement(Order = 5)]
        public string LastEntry { get; set; }


        
    }
    public class WageAmount {
        [XmlAttribute]
        public string currencyID { get; set; }
        [XmlText]
        public string wageAmount { get; set; }
    }
    public class SubsidyAmount {
        [XmlAttribute]
        public string currencyID { get; set; }
        [XmlText]
        public string subsidyAmount { get; set; }
    }

    [XmlRoot("EmployeeInformation")]
    public class EmployeeInformation {
        public string EmployeeID { get; set; }
        public string EmployeeTitle { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }

    }

}