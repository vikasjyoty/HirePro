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
    public class ServicesController : Controller
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

        // GET: Services
        public ActionResult Index()
        {
            var services = db.Services.Include(s => s.Type);
            return View(services.ToList());
        }

        public ActionResult SelectService(int typeid)
        {
            var services = db.Services.Where(s => s.Type_Id==typeid);
            ViewBag.typeid = typeid;
            return View(services.ToList());
        }

        // GET: Services
        public ActionResult ServiceType()
        {
            return View();
        }
        // GET: Services/SelectCareGiver
        public ActionResult SelectCareGiver(int typeid,int serviceid)
        {
            var caregivers = db.Users.Where(s=>s.Type_Id==typeid);
            ViewBag.serviceid = serviceid;
            return View(caregivers.ToList());
        }

        // GET: Services
        public ActionResult SubmitService(string caregiverid, int serviceid)
        {
            ApplicationUserService aps = new ApplicationUserService();
            aps.Caregiver_Id = caregiverid;
            aps.Service_Id = serviceid;
            aps.Status_Id = 1;
            aps.Users_Id = UserId;
            aps.RequestedOn = DateTime.Now;
            aps.Frequency = 1;

            var user = db.Users.Find(UserId);
            user.ApplicationUserServices.Add(aps);
            db.SaveChanges();

            ViewBag.serviceid = serviceid;
            return RedirectToAction("SubmitComplete" , new { serviceid = serviceid});
        }

        public ActionResult SubmitComplete( int serviceid)
        {
            var aps = db.ApplicationUserServices.Where(s=>s.Service_Id==serviceid && s.Users_Id == UserId).FirstOrDefault();

            ViewBag.serviceid = serviceid;
            return View(aps);
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.Type_Id = new SelectList(db.Types, "Id", "TypeName");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceId,Name,CssClass,Type_Id,IsActive")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Type_Id = new SelectList(db.Types, "Id", "TypeName", service.Type_Id);
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type_Id = new SelectList(db.Types, "Id", "TypeName", service.Type_Id);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceId,Name,CssClass,Type_Id,IsActive")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Type_Id = new SelectList(db.Types, "Id", "TypeName", service.Type_Id);
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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
