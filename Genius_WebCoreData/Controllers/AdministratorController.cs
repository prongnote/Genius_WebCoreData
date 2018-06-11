using Genius_WebCoreData.FormModels.Administrator;
using Genius_WebCoreData.Models.Administrator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Genius_WebCoreData.Controllers
{
    public class AdministratorController : Controller
    {
        M_RegisterCompany model = new M_RegisterCompany();
        // GET: Administrator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult V_RegisterCompany() {
            
            ViewBag.jsonData = JsonConvert.SerializeObject(Json(model.loadDataList()));
            return View(); 

        }

        public ActionResult actionRegisterCompany(F_RegisterCompany formModel) {

            

            if (!string.IsNullOrEmpty(Request["saveData"]).Equals("true")) {
                return Json(model.saveData(formModel));                 
            }

            return null;

        }
    }
}