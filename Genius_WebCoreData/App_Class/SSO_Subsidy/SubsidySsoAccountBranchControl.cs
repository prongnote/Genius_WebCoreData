using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genius_WebCoreData.App_Class.SSO_Subsidy {
    public class SubsidySsoAccountBranchControl {


        public SubsidySsoAccountBranchControl() {

        }

        public string ssoAccountBranch { get; set; } //##### 1.
        public string totalBranchWageAmount { get; set; }//##### 2.
        public string totalBranchEmployeeSubsidyAmount { get; set; }//##### 3.
        public string totalBranchEmployerSubsidyAmount { get; set; }//##### 4.
        public string totalBranchSubsidyAmount { get; set; }//##### 5.
        public string totalBranchNumberOfEmployee { get; set; }//##### 6.


        public void setSubsidySsoAccountBranchControl(string stringControl) {
            
            this.ssoAccountBranch = stringControl.Substring(0, 6);                             //##### 0 : 6 = 6
            this.totalBranchWageAmount = stringControl.Substring(6, 16);                       //##### 6 : 16 = 22
            this.totalBranchEmployeeSubsidyAmount = stringControl.Substring(22, 8);            //##### 22 : 8 = 30
            this.totalBranchEmployerSubsidyAmount = stringControl.Substring(30, 8);            //##### 30 : 8 = 38
            this.totalBranchSubsidyAmount = stringControl.Substring(38, 8);                    //##### 38 : 8 = 48
            this.totalBranchNumberOfEmployee = stringControl.Substring(46, 8);                    //##### 46 : 8 = 54

        }


    }


        
 }