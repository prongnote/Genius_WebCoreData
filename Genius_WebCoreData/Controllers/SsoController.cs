using Genius_WebCoreData.FormModels.SSO;
using Genius_WebCoreData.Models.SSO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web_LED.App_Class;

namespace Genius_WebCoreData.Controllers
{
    public class SsoController : Controller
    {

        F_SSO_SendDataForm formModel = new F_SSO_SendDataForm();
        M_SSO_SendDataForm model = new M_SSO_SendDataForm();

        // GET: Sso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult transferDataXmlToSSO(F_SSO_SendDataForm formModelMethod) {

            formModelMethod.server_path_xml = Server.MapPath("~/Uploads/TestFileXml");
            var pathXml = Path.Combine(Server.MapPath("~/Uploads/TestFileXml"), "SSOSENA_ANSI_TEST_2.xml");
            formModelMethod.server_path_xml = pathXml;
            //return model.sendDataXmlToSSO(formModelMethod); 

            if (model.transferDataXmlToSSO(formModelMethod)) {
                return Json(SystemClass.returnResultJsonSuccess());
            }
            else {
                return Json(SystemClass.returnResultJsonFailure());
            }

        }

        public ActionResult sendDataXmlToSSO(F_SSO_SendDataForm formModelMethod) {

            formModelMethod.server_path_xml = Server.MapPath("~/Uploads/TestFileXml");

            //return model.sendDataXmlToSSO(formModelMethod); 
            
            if (model.sendDataXmlToSSO(formModelMethod)) {
                return Json(SystemClass.returnResultJsonSuccess());
            }
            else {
                return Json(SystemClass.returnResultJsonFailure());
            }
            

        }

        public ActionResult convertDataUploadXml(F_SSO_SendDataForm formModelMethod) {
            var pathDecode = Path.Combine(Server.MapPath("~/Uploads/TestFileDecode"), formModelMethod.server_file_name);
            formModelMethod.server_path_decode = pathDecode;
            formModelMethod.server_path_send_exe = Path.Combine(Server.MapPath("~/Plugins/SSO_SendData"), "SSO_Subsidy_SendData.exe");
            formModelMethod.server_path_plugins = Server.MapPath("~/Plugins/SSO_SendData"); ; 
            formModelMethod.server_path_xml = Server.MapPath("~/Uploads/TestFileXml"); 
            if (model.convertDataToXml(formModelMethod)) {
                return Json(SystemClass.returnResultJsonSuccess()); 
            }
            else {
                return Json(SystemClass.returnResultJsonFailure()); 
            }
        }

        public ActionResult V_SSO_SendDataFormUploadFile() {

            if (Request.Files.Count > 0) {
                //Debug.WriteLine("HAVE uploads");
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0) {
                    Debug.WriteLine(file.FileName);

                    // extract only the filename
                    string fileName = Path.GetFileName(file.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    var path = Path.Combine(Server.MapPath("~/Uploads/TestUploadFiles"), fileName);
                    var pathDecode = Path.Combine(Server.MapPath("~/Uploads/TestFileDecode"), fileName);

                    try {
                        file.SaveAs(path);
                        formModel.server_path = path;
                        formModel.server_path_decode = pathDecode;
                        formModel.server_file_name = fileName; 
                        return Json(model.getDecodeString(formModel));
                    }
                    catch (Exception ex) {

                        Debug.WriteLine(ex);
                        return Json(new { result = "FAILURE", fileCount = Request.Files.Count, errorException = ex.ToString() });
                    }

                }
            }

            return Json(new { result = "FAILURE", fileCount = Request.Files.Count });

        }

        public ActionResult V_SSO_SendDataForm() {

            //formModel.server_path = Server.MapPath("/Uploads/TestFileEncode/test_text_007.DATII");
            // ViewBag.jsonData = JsonConvert.SerializeObject(Json(SystemClass.returnResultJsonSuccess()));
            //ViewBag.jsonDatas = JsonConvert.SerializeObject(Json(model.getDecodeString(formModel)));

            return View();
        }

    }
}