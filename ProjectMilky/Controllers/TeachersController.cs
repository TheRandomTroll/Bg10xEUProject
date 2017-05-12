using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMilky.Controllers
{
    using System.IO;

    using ProjectMilky.Attributes;
    using ProjectMilky.Models;

    [TeachersOnly]
    public class TeachersController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Teachers
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                ViewBag.Message = "File uploaded successfully.";
            }

            return View();
        }
    }
}