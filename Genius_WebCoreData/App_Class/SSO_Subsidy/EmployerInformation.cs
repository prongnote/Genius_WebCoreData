using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Genius_WebCoreData.App_Class.SSO_Subsidy {

    [XmlRoot("EmployerInformation")]
    public class EmployerInformation {

        [XmlElement("EmployerID")]
        public string EmployerID { get; set; }

        [XmlElement("EmployerName")]
        public string EmployerName { get; set; }

        [XmlElement("EmployerPosition")]
        public string EmployerPosition { get; set; }

    }
}