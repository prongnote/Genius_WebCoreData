using Genius_WebCoreData.App_Class;
using Genius_WebCoreData.FormModels.Administrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Genius_WebCoreData.Models.Administrator {
    public class M_RegisterCompany {

        private ClassDB classDB = new ClassDB();


        public Object loadDataList() {
            string sql = "SELECT * FROM company ORDER BY company_id";
            Dictionary<string, object> jsonReturn = new Dictionary<string, object>();
            List<Dictionary<string, object>> lists = new List<Dictionary<string, object>>();

            DataTable dataTable = classDB.getDataTable(sql.ToString());

            foreach (DataRow dataRow in dataTable.Rows) {


                Dictionary<string, object> dataList = new Dictionary<string, object>();
                
                dataList.Add("company_id", dataRow["company_id"]);
                dataList.Add("company_name_thai", dataRow["company_name_thai"]);
                dataList.Add("company_name_eng", dataRow["company_name_eng"]);
                dataList.Add("company_short_name", dataRow["company_short_name"]);
                dataList.Add("company_address", dataRow["company_address"]);
                dataList.Add("company_register_id", dataRow["company_register_id"]);

                lists.Add(dataList);

            }

            jsonReturn.Add("dataLists", lists);

            return jsonReturn;


        }


        public bool saveData(F_RegisterCompany formModel) {

            Dictionary<string, string> dataFields = new Dictionary<string, string>();

            dataFields.Add("company_name_thai", formModel.company_name_thai);
            dataFields.Add("company_name_eng", formModel.company_name_eng);
            dataFields.Add("company_register_id", formModel.company_register_id);
            dataFields.Add("company_short_name", formModel.company_short_name);
            dataFields.Add("company_address", formModel.company_address);

            try {
                if (formModel.action_type.Equals("ADD")) {
                    classDB.insertData(dataFields, "company");
                }
                else if (formModel.action_type.Equals("EDIT")) {
                    classDB.updateData(dataFields, "company" , " company_id = "+formModel.company_id); 
                }                
                return true;
            }
            catch (Exception ex) {
                return false; 
            }

        }


    }
}