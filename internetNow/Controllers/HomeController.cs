using internetNow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
using System.Threading.Tasks;

namespace internetNow.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public async Task<ActionResult> Report()
        {
            try
            {
                List<RatioModel> Ratio = new List<RatioModel>();
                WriteTextFile writeTextFile = new WriteTextFile();
                Ratio = await writeTextFile.Report();
                return View(Ratio);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        public void Post(string obj)
        {
            try
            {
                WriteTextFile writeTextFile = new WriteTextFile();
               _ =   writeTextFile.Write(obj);
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }
        public JsonResult FileSize(string obj)
        {
            try
            {
                WriteTextFile writeTextFile = new WriteTextFile();
                bool iswriteable = writeTextFile.FileSize(obj);
                return Json(iswriteable, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}