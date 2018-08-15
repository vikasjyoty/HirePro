using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HireProSol.Models;

namespace HireProSol.Controllers
{
    [Authorize]
    public class UserServicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string _userId;
        public string UserId
        {
            get
            {
                return _userId = HttpContext.Request.Cookies["UserId"].Value;
            }
        }

        // GET: ApplicationUserServices
        public ActionResult Index()
        {
            var applicationUserServices = db.ApplicationUserServices.Include(a => a.Caregiver).Include(a => a.Service).Include(a => a.Status).Include(a => a.User);
            if (User.IsInRole("CareGiver"))
                applicationUserServices = applicationUserServices.Where(s => s.Caregiver_Id == UserId);
            if (User.IsInRole("Customer"))
                applicationUserServices = applicationUserServices.Where(s => s.Users_Id == UserId);


            return View(applicationUserServices.ToList());
        }

        // GET: ApplicationUserServices/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUserService applicationUserService = db.ApplicationUserServices.Include(a => a.Status).Where(s => s.Users_Id == UserId && s.Service_Id == id).FirstOrDefault();

            if (applicationUserService == null)
            {
                return HttpNotFound();
            }
            return View(applicationUserService);
        }

        // GET: ApplicationUserServices/Create
        public ActionResult Create()
        {
            ViewBag.Caregiver_Id = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.Service_Id = new SelectList(db.Services, "ServiceId", "Name");
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Name");
            ViewBag.Users_Id = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: ApplicationUserServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Users_Id,Service_Id,RequestedOn,Frequency,Status_Id,Caregiver_Id")] ApplicationUserService applicationUserService)
        {
            if (ModelState.IsValid)
            {
                db.ApplicationUserServices.Add(applicationUserService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Caregiver_Id = new SelectList(db.Users, "Id", "FirstName", applicationUserService.Caregiver_Id);
            ViewBag.Service_Id = new SelectList(db.Services, "ServiceId", "Name", applicationUserService.Service_Id);
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Name", applicationUserService.Status_Id);
            ViewBag.Users_Id = new SelectList(db.Users, "Id", "FirstName", applicationUserService.Users_Id);
            return View(applicationUserService);
        }

        // GET: ApplicationUserServices/Edit/5
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUserService applicationUserService = db.ApplicationUserServices.Where(s => s.Users_Id == UserId && s.Service_Id == id).FirstOrDefault();

            if (applicationUserService == null)
            {
                return HttpNotFound();
            }
            ViewBag.Caregiver_Id = new SelectList(db.Users.Where(s => s.Type_Id == applicationUserService.Service.Type_Id), "Id", "FirstName", applicationUserService.Caregiver_Id);
            ViewBag.Service_Id = new SelectList(db.Services, "ServiceId", "Name", applicationUserService.Service_Id);
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Name", applicationUserService.Status_Id);
            ViewBag.Users_Id = new SelectList(db.Users, "Id", "FirstName", applicationUserService.Users_Id);

            return View(applicationUserService);
        }

        // POST: ApplicationUserServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Users_Id,Service_Id,RequestedOn,Frequency,Status_Id,Caregiver_Id")] ApplicationUserService applicationUserService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUserService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Caregiver_Id = new SelectList(db.Users, "Id", "FirstName", applicationUserService.Caregiver_Id);
            ViewBag.Service_Id = new SelectList(db.Services, "ServiceId", "Name", applicationUserService.Service_Id);
            ViewBag.Status_Id = new SelectList(db.Status, "Id", "Name", applicationUserService.Status_Id);
            ViewBag.Users_Id = new SelectList(db.Users, "Id", "FirstName", applicationUserService.Users_Id);
            return View(applicationUserService);
        }

        // GET: ApplicationUserServices/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUserService applicationUserService = db.ApplicationUserServices.Include(a => a.Status).Where(s => s.Users_Id == UserId && s.Service_Id == id).FirstOrDefault();

            if (applicationUserService == null)
            {
                return HttpNotFound();
            }
            return View(applicationUserService);
        }

        // POST: ApplicationUserServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplicationUserService applicationUserService = db.ApplicationUserServices.Where(s => s.Users_Id == UserId && s.Service_Id == id).FirstOrDefault();
            db.ApplicationUserServices.Remove(applicationUserService);
            db.SaveChanges();
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
