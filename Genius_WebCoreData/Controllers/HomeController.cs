using Genius_WebCoreData.FormModels.Home;
using Genius_WebCoreData.Models.Home;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_LED.App_Class;


namespace Genius_WebCoreData.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult V_DataCenter() {
            return View();
        }
        
        public ActionResult getLoginDisplay() {



            return null; 
        }

        public ActionResult isLogin(F_Login formModel) {
            M_Login model = new M_Login();

            if (formModel.requestLogin == "isLogin") {
                if (model.getLogin(formModel)) {
                    return Json(SystemClass.returnResultJsonSuccess());
                }
                else {
                    return Json(SystemClass.returnResultJsonFailure());
                }
            }
            else {
                return Json(SystemClass.returnResultJsonFailure());  
            }
        }


        public ActionResult V_Login() {
            
            
            return View();
        }


        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}