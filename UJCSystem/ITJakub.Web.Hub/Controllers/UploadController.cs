﻿using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ITJakub.Web.Hub.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
         [AllowAnonymous]
        public ActionResult Upload()
        {
            return View();
        }

        //Dropzone upload method
        public ActionResult UploadFile()
        {
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                if (file == null || file.ContentLength == 0) continue;

                string pathString = Path.Combine("D:\\", "UploadedFiles");

                
                if (!Directory.Exists(pathString))
                    Directory.CreateDirectory(pathString);

                string path = string.Format("{0}\\{1}", pathString, file.FileName);
                file.SaveAs(path);
            }
            return Json(new {});
        }

        public ActionResult UploadFrontImage()
        {
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                if (file == null || file.ContentLength == 0) continue;

                string pathString = Path.Combine("D:\\", "UploadedFiles");


                if (!Directory.Exists(pathString))
                    Directory.CreateDirectory(pathString);

                string path = string.Format("{0}\\{1}", pathString, file.FileName);
                file.SaveAs(path);
            }
            return Json(new { });
        }

        public ActionResult UploadImages()
        {
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                if (file == null || file.ContentLength == 0) continue;

                string pathString = Path.Combine("D:\\", "UploadedFiles");


                if (!Directory.Exists(pathString))
                    Directory.CreateDirectory(pathString);

                string path = string.Format("{0}\\{1}", pathString, file.FileName);
                file.SaveAs(path);
            }
            return Json(new { });
        }
    }
}