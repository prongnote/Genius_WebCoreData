using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genius_WebCoreData.FormModels.Administrator {
    public class F_RegisterCompany {

        public string company_id { get; set;  }
        public string company_name_thai { get; set;  }
        public string company_name_eng { get; set; }
        public string company_short_name { get; set; }
        public string company_register_id { get; set; }
        public string company_address { get; set; }

        public string action_type { get; set; }

       


    }
}