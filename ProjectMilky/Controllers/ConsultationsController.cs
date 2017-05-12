using System;
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

    using ProjectMilky.Attributes;

    [TeachersOnly]
    public class ConsultationsController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private static ApplicationDbContext db = new ApplicationDbContext();


        private static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
        private UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

        private static RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(db);
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
        public async Task<ActionResult> Index()
        {
            return View(await db.Consultations.ToListAsync());
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
                consultation.Teacher = db.Users.First(x => x.Id == User.Identity.GetUserId());
                db.Consultations.Add(consultation);
                await db.SaveChangesAsync();
                foreach (var userRole in this.roleManager.FindByName("Student").Users)
                {
                    var user = db.Users.First(x => x.Id == userRole.UserId);
                    var body = $"<p>Salutations, {user.FirstName} {user.LastName}!</p>"
                               + $"We would love to inform you that your teacher, {User.Identity.Name}, has started a new consultation."
                               + $" You can see it here: <a href=\"{consultation.YoutubeUrl}\">{consultation.Subject}</a></p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(user.Email));
                    message.From = new MailAddress("eschool.donotreply@gmail.com");
                    message.Subject = "A new consultation has been added";
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
                    return RedirectToAction("Index");
                }
            }
            return View(consultation);
        }

        // GET: Consultations/Edit/5
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
