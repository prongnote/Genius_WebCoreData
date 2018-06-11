using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genius_WebCoreData.FormModels.SSO {
    public class F_SSO_SendDataForm {
        public string upload_sso_file_encode { get; set; }
        public string upload_sso_file_decode { get; set; }
        public string text_sso_decode { get; set; }
        public string text_sso_encode { get; set; }


        public string server_path { get; set; }
        public string server_path_decode { get; set; }
        public string server_file_name { get; set; }
        public string server_path_xml { get; set;  }
        public string server_path_plugins { get; set; }
        public string server_path_send_exe { get; set; }

        // public string[] files { get; set; }
        public HttpPostedFileBase files { get; set; }
    }
}