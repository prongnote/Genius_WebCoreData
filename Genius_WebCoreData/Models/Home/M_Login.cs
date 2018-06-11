using Genius_WebCoreData.App_Class;
using Genius_WebCoreData.FormModels.Home;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Genius_WebCoreData.Models.Home {
    public class M_Login {
        private ClassDB classDB;
        public M_Login() {
            classDB = new ClassDB(); 
        }

        public Object getLoginDisplay() {
            /*
            Dictionary<string, object> jsonReturn = new Dictionary<string, object>();
            List<Dictionary<string, object>> lists = new List<Dictionary<string, object>>();
             foreach (DataRow dataRow in dataTable.Rows) {
                Dictionary<string, object> dataList = new Dictionary<string, object>();   

                dataList.Add("dateModify", dataRow["date_modify"].ToString());
                dataList.Add("createId", dataRow["create_id"]);
                dataList.Add("modifyId", dataRow["modify_id"]);
                dataList.Add("createName", dataRow["create_name"]);

                lists.Add(dataList);
            }
            jsonReturn.Add("dataLists", lists);

            return jsonReturn;



    */

            return null; 

        }

        public bool getLogin(F_Login formModel) {

            string sql = "SELECT * FROM personal WHERE personal_username = '"+ formModel.username + "'  AND personal_password = '" + formModel.password + "'  "; 
            DataTable dataTable = classDB.selectDataTable(sql);
            if (dataTable.Rows.Count == 1 ) {
                setPersonalLogin(dataTable.Rows[0]);
                return true; 
            }

            return false; 
        }

        public void setPersonalLogin(DataRow dataRow) {

            HttpContext.Current.Session["ss_personal_id"] = dataRow["personal_id"];
            HttpContext.Current.Session["ss_personal_username"] = dataRow["personal_username"];
            HttpContext.Current.Session["ss_personal_displayname"] = dataRow["personal_displayname"];
            HttpContext.Current.Session["ss_company_id"] = dataRow["company_id"];


        }

    }
}