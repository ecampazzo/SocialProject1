using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebUpFile.Controllers
{
    public class UploadController : ApiController
    {
        [Route("api/files/Uploads   ")]
        public string PostFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count>0)
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        var fileName = postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                        var filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + fileName);
                        postedFile.SaveAs(filePath);
                        return "/Uploads/" + fileName;
                   
                    }

            }
            catch (Exception exception)
            {
                return exception.Message;

            }
            return "no files";
        }
    }
}