using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genius_WebCoreData.App_Class {
    public class ClassPersonal {

        public string personalId { get; set;  }
        public string personalUsername { get; set; }
        public string personalDisplayname { get; set; }
        public string companyId { get; set; }
        public string companyName { get; set; }
        public string companyNameEng { get; set; }
        public string companyShortName { get; set; }
        public string companyTax { get; set; }


        public ClassPersonal() {
            setPersonal();
        }

        public void setPersonal() {
            this.personalId = "1";
            this.personalUsername = "test_1";
            this.personalDisplayname = "test_1";
            this.companyId = "1";
            this.companyName = "กกกกก";
            this.companyNameEng = "AAAAA";
            this.companyShortName = "AAA";
            this.companyTax = "0105541011549";
        }
        


    }
}