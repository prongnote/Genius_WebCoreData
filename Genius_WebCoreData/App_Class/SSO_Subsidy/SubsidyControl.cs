using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genius_WebCoreData.App_Class.SSO_Subsidy {
  


public class SubsidyControl {

        public string referenceNumber { get; set; } //##### 1.
        public string issueDateTime { get; set; }//##### 2.
        public string companyTaxNumber { get; set; }//##### 3.
        public string companyBranch { get; set; }//##### 4.
        public string companyBranchName { get; set; }//##### 5.
        public string companyName { get; set; }//##### 6.
        public string companyEnglishName { get; set; }//##### 7.
        public string streetAndNumber { get; set; }//##### 8.
        public string district { get; set; }//##### 9.
        public string subProvince { get; set; }//##### 10.
        public string province { get; set; }//##### 11.
        public string postcode { get; set; }//##### 12.
        public string countryCode { get; set; }//##### 13.
        public string phoneNumber { get; set; }//##### 14.
        public string faxNumber { get; set; }//##### 15.
        public string accountingOfficeTaxNumber { get; set; }//##### 16.
        public string accountingOfficeBranch { get; set; }//##### 17.
        public string ssoAccount { get; set; }//##### 18.
        public string subsidyRate { get; set; }//##### 19.
        public string subsidyMonth { get; set; }//##### 20.
        public string subsidyYear { get; set; }//##### 21.
        public string totalWageAmount { get; set; }//##### 22.
        public string totalEmployeeSubsidyAmount { get; set; }//##### 23.
        public string totalEmployerSubsidyAmount { get; set; }//##### 24.
        public string totalSubsidyAmount { get; set; }//##### 25.
        public string totalNumberOfEmployee { get; set; }//##### 26.
        public string employerIdCard { get; set; }//##### 27.
        public string employerName { get; set; }//##### 28.
        public string employerPosition { get; set; }//##### 29.
        public string bankCode { get; set; }//##### 30.
        public string bankBranchCode { get; set; }//##### 31.
        public string bankAccountNumber { get; set; }//##### 32.
        public string paymentAmount { get; set; }//##### 33.
        public string senderRegistrationId { get; set; }//##### 34.


        public SubsidyControl() {


        }

        public void setSubsidyControl(string stringControl) {

            this.referenceNumber = stringControl.Substring(0, 13).Trim();                      //##### 0 : 13 = 13
            this.issueDateTime = stringControl.Substring(13, 19).Trim();                     //##### 13 : 19 = 32
            this.companyTaxNumber = stringControl.Substring(32, 17).Trim();                    //##### 32 : 17 = 49
            this.companyBranch = stringControl.Substring(49, 6).Trim();                        //##### 49 : 6 = 55
            this.companyBranchName = stringControl.Substring(55, 20).Trim();                   //##### 55 : 20 = 75
            this.companyName = stringControl.Substring(75, 120).Trim();                        //##### 75 : 120 = 195
            this.companyEnglishName = stringControl.Substring(195, 70).Trim();                 //##### 195 : 70 = 265
            this.streetAndNumber = stringControl.Substring(265, 70).Trim();                    //##### 265 : 70 = 335
            this.district = stringControl.Substring(335, 35).Trim();                           //##### 335 : 35 = 370
            this.subProvince = stringControl.Substring(370, 35).Trim();                        //##### 370 : 35 = 405
            this.province = stringControl.Substring(405, 35).Trim();                           //##### 405 : 35 = 440
            this.postcode = stringControl.Substring(440, 9).Trim();                            //##### 440 : 9 = 449
            this.countryCode = stringControl.Substring(449, 2).Trim();                         //##### 449 : 2 = 451
            this.phoneNumber = stringControl.Substring(451, 35).Trim();                        //##### 451 : 35 = 486
            this.faxNumber = stringControl.Substring(486, 35).Trim();                          //##### 486 : 35 = 521
            this.accountingOfficeTaxNumber = stringControl.Substring(521, 17).Trim();          //##### 521 : 17 = 538
            this.accountingOfficeBranch = stringControl.Substring(538, 6).Trim();              //##### 538 : 6 = 544
            this.ssoAccount = stringControl.Substring(544, 10).Trim();                         //##### 544 : 10 = 554
            this.subsidyRate = stringControl.Substring(554, 5).Trim();                         //##### 554 : 5 = 559
            this.subsidyMonth = stringControl.Substring(559, 2).Trim();                        //##### 559 : 2 = 561
            this.subsidyYear = stringControl.Substring(561, 4).Trim();                         //##### 561 : 4 = 565
            this.totalWageAmount = stringControl.Substring(565, 16).Trim();                    //##### 565 : 16 = 581
            this.totalEmployeeSubsidyAmount = stringControl.Substring(581, 8).Trim();          //##### 581 : 8 = 589
            this.totalEmployerSubsidyAmount = stringControl.Substring(589, 8).Trim();          //##### 589 : 8 = 597
            this.totalSubsidyAmount = stringControl.Substring(597, 8).Trim();                  //##### 597 : 8 = 605
            this.totalNumberOfEmployee = stringControl.Substring(605, 8).Trim();               //##### 605 : 8 = 613
            this.employerIdCard = stringControl.Substring(613, 17).Trim();                     //##### 613 : 17 = 630
            this.employerName = stringControl.Substring(630, 70).Trim();                       //##### 630 : 70 = 700
            this.employerPosition = stringControl.Substring(700, 35).Trim();                   //##### 700 : 35 = 735
            this.bankCode = stringControl.Substring(735, 3).Trim();                            //##### 735 : 3 = 738
            this.bankBranchCode = stringControl.Substring(738, 6).Trim();                      //##### 738 : 6 = 744
            this.bankAccountNumber = stringControl.Substring(744, 20).Trim();                  //##### 744 : 20 = 764
            this.paymentAmount = stringControl.Substring(764, 8).Trim();                       //##### 764 : 8 = 772
            this.senderRegistrationId = stringControl.Substring(772, (stringControl.Length-772)).Trim();                  //##### 772 : 35 = 807




        }


    }

    


}