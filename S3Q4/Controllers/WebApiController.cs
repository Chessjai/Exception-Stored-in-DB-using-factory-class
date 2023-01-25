using S3Q4.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace S3Q4.Controllers
{
    public class WebApiController : ApiController
    {
        FactoryClass obj = new FactoryClass();
        [Route("~/api/WebApi/GetExceptionDetails")]
        public IHttpActionResult GetExceptionDetails()
        {
            string data = "";
            try
            {
                int a = 0;
                int b = 10;
                int c = b / a;
                StringReader strbuild = new StringReader("text.txt");
                strbuild.Read();
                data = "No Execption found";
            }
            catch (Exception ex)
            {
                obj.LogtoSQL2(ex, null, "1215", "125");
                data = "Execption Found And Handled";
            }
            return Ok(data);
        }
    }
}
