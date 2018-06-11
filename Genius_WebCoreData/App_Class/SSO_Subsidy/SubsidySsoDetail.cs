using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genius_WebCoreData.App_Class.SSO_Subsidy {
    public class SubsidySsoDetail {

        public string itemNumber { get; set; } //##### 1.
        public string ssoAccountBranch { get; set; }//##### 2.
        public string ssoAccountBranchItemNumber { get; set; }//##### 3.
        public string employeeIdCard { get; set; }//##### 4.
        public string employeeTitle { get; set; }//##### 5.
        public string employeeName { get; set; }//##### 6.
        public string employeeLastName { get; set; } //##### 8.
        public string wageAmount { get; set; } //##### 9.
        public string employeeSubsidyAmount { get; set; } //##### 10.
        public string lastEntry { get; set; } //##### 11.


        public SubsidySsoDetail() {

        }

        public void setSubsidySsoDetail(string stringControl) {

            this.itemNumber = stringControl.Substring(0, 5).Trim();                        //##### 0 : 5 = 5
            this.ssoAccountBranch = stringControl.Substring(5,6).Trim();                   //##### 5 : 6 = 11
            this.ssoAccountBranchItemNumber = stringControl.Substring(11, 5).Trim();       //##### 11 : 5 = 16
            this.employeeIdCard = stringControl.Substring(16, 17).Trim();                  //##### 16 : 17 = 33
            this.employeeTitle = stringControl.Substring(33, 35).Trim();                   //##### 33 : 35 = 68
            this.employeeName = stringControl.Substring(68, 35).Trim();                    //##### 68 : 35 = 103
            this.employeeLastName = stringControl.Substring(103, 35).Trim();               //##### 103 : 35 = 138
            this.wageAmount = stringControl.Substring(138, 16).Trim();                     //##### 138 : 16 = 154
            this.employeeSubsidyAmount = stringControl.Substring(154, 8).Trim();           //##### 154 : 8 = 162
            this.lastEntry = stringControl.Substring(162, 1).Trim();                       //##### 162 : 1 = 163
        }



    }
}