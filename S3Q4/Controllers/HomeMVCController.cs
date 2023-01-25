using S3Q4.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3Q4.Controllers
{
    public class HomeMVCController : Controller
    {
        // GET: HmeMVC
        public ActionResult Index()
        {
            FactoryClass obj=new FactoryClass();
            try
            {
                int a = 0;
                int b = 10;
                int c = b / a;
                StringReader strbuild = new StringReader("text.txt");
                strbuild.Read();
                ViewBag.Exception1 = "No Execption Found";

            }
            catch (Exception ex)
            {
                obj.LogtoSQL2(ex, null, "1215", "125");
                ViewBag.Exception = "Execption Handeled";
            }
            return View();
        }
    }
}