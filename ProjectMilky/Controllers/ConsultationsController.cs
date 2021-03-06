﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using ProjectMilky.Models;

namespace ProjectMilky.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using PagedList;
    using ProjectMilky.Attributes;

    
    [Authorize]
    public class ConsultationsController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private static ApplicationDbContext db = db ?? new ApplicationDbContext();


        private static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

        private static RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
        private RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Consultations
        public ActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            return View(db.Consultations.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Consultations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = await db.Consultations.FindAsync(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            return View(consultation);
        }

        [TeachersOnly]
        // GET: Consultations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Consultations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "Subject,Description,YoutubeUrl")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                consultation.Teacher = db.Users.First(x => x.UserName == User.Identity.Name);
                db.Consultations.Add(consultation);
                await db.SaveChangesAsync();
                foreach (var userRole in this.roleManager.FindByName("Student").Users)
                {
                    var user = db.Users.First(x => x.Id == userRole.UserId);
                    var body = $"<p>Здравейте, {user.FirstName} {user.LastName}!</p>"
                               + $"Бихме желали да Ви осведомим, че г-н/г-жа {consultation.Teacher.FirstName} {consultation.Teacher.LastName} току-що започна."
                               + $"<br />Можете да я видите тук: <a href=\"{consultation.YoutubeUrl}\">{consultation.Subject}</a></p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(user.Email));
                    message.From = new MailAddress("eschool.donotreply@gmail.com");
                    message.Subject = "Нова консултация";
                    message.Body = body;
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                                             {
                                                 UserName = "eschool.donotreply@gmail.com",
                                                 Password = "Destroy1"
                                             };
                        smtp.Credentials = credential;
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(message);
                    }
                }
                return RedirectToAction("Index");
            }
            return View(consultation);
        }

        // GET: Consultations/Edit/5
        [TeachersOnly]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = await db.Consultations.FindAsync(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            return View(consultation);
        }

        // POST: Consultations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Subject,Description,YoutubeUrl")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(consultation);
        }

        // GET: Consultations/Delete/5
        [TeachersOnly]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = await db.Consultations.FindAsync(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            return View(consultation);
        }

        // POST: Consultations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Consultation consultation = await db.Consultations.FindAsync(id);
            db.Consultations.Remove(consultation);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
