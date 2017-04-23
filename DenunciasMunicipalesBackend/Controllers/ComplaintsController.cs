using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DenunciasMunicipalesBackend.Models;
using System;

namespace DenunciasMunicipalesBackend.Controllers
{
    public class ComplaintsController : Controller
    {
        private DataContext db;

        public ComplaintsController()
        {
            db = new DataContext();
        }

        public ActionResult Index()
        {
            return View(db.Complaints.OrderBy(c => c.Date).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Complaint complaint = db.Complaints.Find(id);

            if (complaint == null)
            {
                return HttpNotFound();
            }

            return View(complaint);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Complaint complaint)
        {
            complaint.Date = DateTime.Today;
            complaint.CreatedBy = "Alfredo Martinez";

            if (ModelState.IsValid)
            {
                db.Complaints.Add(complaint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(complaint);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Complaint complaint = db.Complaints.Find(id);

            if (complaint == null)
            {
                return HttpNotFound();
            }

            return View(complaint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(complaint);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Complaint complaint = db.Complaints.Find(id);

            if (complaint == null)
            {
                return HttpNotFound();
            }

            return View(complaint);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Complaint complaint = db.Complaints.Find(id);
            db.Complaints.Remove(complaint);
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
