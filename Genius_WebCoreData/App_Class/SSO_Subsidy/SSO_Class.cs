using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_LED.App_Class;

namespace Genius_WebCoreData.App_Class.SSO_Subsidy {
    public class SSO_Class {

        private ClassPersonal classPersonal = new ClassPersonal();
        private ClassDB classDB = new ClassDB();

        public string REF_NO  { get;set; }
        public string REF_NO_TEXT { get; set; }
        public string INV_NO { get; set; }
        public string INV_NO_TEXT { get; set; }
        public string SSO_ACCOUNT_ID { get; set; }
        public string SEND_STATUS { get; set; }
        public string SEND_RESPONSE { get; set; }
        public string SUBSIDY_RATE { get; set; }
        public string SUBSIDY_MONTH { get; set; }
        public string SUBSIDY_YEAR { get; set; }
        public string TOTAL_WAGE_AMOUNT { get; set; }
        public string TOTAL_EMPLOYEE_AMOUNT { get; set; }
        public string TOTAL_EMPLOYER_AMOUNT { get; set; }
        public string TOTAL_EMPLOYER_NO { get; set; }


        public string YEAR_MONTH = DateTime.Now.ToString("yyyyMM");
        public string XML_NAME { get; set; }
        public string XML_PATH { get; set; }

        public string getSsoRefNo() {

            this.REF_NO = classDB.selectOnceData("sso_ref_no", "ref_no" , "ref_no_year_month='" + this.YEAR_MONTH + "' AND ref_no_status='0' AND company_id=" + classPersonal.companyId );
            if (this.REF_NO.Equals("")) {
                this.REF_NO = classDB.nextNumber("ref_no", "sso_ref_no", "")  ;
                this.REF_NO_TEXT = SSO_Configs.SSO_REFERENCE_PROFILE + String.Format("{0:D9}", Int32.Parse(this.REF_NO));
                insertDataSsoRefNo(); 
            }
            this.REF_NO_TEXT = SSO_Configs.SSO_REFERENCE_PROFILE + String.Format("{0:D9}", Int32.Parse(this.REF_NO));
            return this.REF_NO_TEXT; 
        }

        public string getSsoInvNo() {
            //this.INV_NO = classDB.selectOnceData("sso_data_send", "inv_no_running", "ref_no = '"+this.REF_NO_TEXT+"'");
            //if (this.INV_NO.Equals("")) {
            this.INV_NO = classDB.nextNumber("inv_no_running", "sso_data_send", "ref_no = '" + this.REF_NO_TEXT + "'");
                
            //}           
            //classPersonal.companyId + "-" + this.YEAR_MONTH + "-" + this.INV_NO            
            this.INV_NO_TEXT = classPersonal.companyShortName + "-" + this.YEAR_MONTH + "-" + String.Format("{0:D2}", Int32.Parse(this.INV_NO));
            
            return this.INV_NO_TEXT; 
        }

        public void insertDataSsoDataSend() {

            Dictionary<string, string> insertData = new Dictionary<string, string>();

            insertData.Add("ref_no", this.REF_NO_TEXT);
            insertData.Add("inv_no", this.INV_NO_TEXT);
            insertData.Add("inv_no_year_month", this.YEAR_MONTH );
            insertData.Add("inv_no_running", this.INV_NO);
            insertData.Add("doc_type", "1");
            insertData.Add("datetime_create", SystemClass.getDateNow());
            insertData.Add("datetime_modify", SystemClass.getDateNow());
            insertData.Add("personal_create", classPersonal.personalId);
            insertData.Add("personal_modify", classPersonal.personalId);
            insertData.Add("xml_path", this.XML_PATH);
            insertData.Add("xml_name", this.XML_NAME);
            insertData.Add("company_id", classPersonal.companyId);
            insertData.Add("company_tax", "0105541011549");
            insertData.Add("company_name", classPersonal.companyName);
            insertData.Add("company_name_eng", classPersonal.companyNameEng);
            insertData.Add("sso_account_id", this.SSO_ACCOUNT_ID);

            insertData.Add("send_status", this.SEND_STATUS);
            insertData.Add("send_response", this.SEND_RESPONSE);
            insertData.Add("subsidy_rate", this.SUBSIDY_RATE);
            insertData.Add("subsidy_month", this.SUBSIDY_MONTH);
            insertData.Add("subsidy_year", this.SUBSIDY_YEAR);
            insertData.Add("total_wage_amount", this.TOTAL_WAGE_AMOUNT);
            insertData.Add("total_employee_amount", this.TOTAL_EMPLOYEE_AMOUNT);
            insertData.Add("total_employer_amount", this.TOTAL_EMPLOYER_AMOUNT);
            insertData.Add("total_employee_no", this.TOTAL_EMPLOYER_NO);


            classDB.insertData(insertData, "sso_data_send");
        }

        private void insertDataSsoRefNo() {
            
            Dictionary<string, string> insertData = new Dictionary<string, string>();

            insertData.Add("ref_no", this.REF_NO);
            insertData.Add("ref_no_text", this.REF_NO_TEXT); 
            insertData.Add("ref_no_year_month", this.YEAR_MONTH);
            insertData.Add("ref_no_year", DateTime.Now.Year.ToString());         
            insertData.Add("ref_no_month", DateTime.Now.ToString("MM"));
            insertData.Add("company_id", classPersonal.companyId);
            insertData.Add("ref_no_status", "0");

            classDB.insertData(insertData, "sso_ref_no");

        }

        public string generateXmlFileName() {
            return this.REF_NO_TEXT+"_"+ this.INV_NO_TEXT+"_" +DateTime.Now.ToString("yyyyMMdd") + ".xml";
        }

    }
}