using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DenunciasMunicipalesBackend.Models;
using DenunciasMunicipalesBackend.Classes;

namespace DenunciasMunicipalesBackend.Controllers
{
    public class ComplaintsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Complaints
        public ActionResult Index()
        {
            var complaints = db.Complaints.Include(c => c.ComplaintType);
            return View(complaints.ToList());
        }

        // GET: Complaints/Details/5
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

        // GET: Complaints/Create
        public ActionResult Create()
        {
            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintTypes, "ComplaintTypeId", "Description");
            return View();
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComplaintView complaintView)
        {
            complaintView.Date = DateTime.Today;
            complaintView.CreatedBy = "Alfredo Martinez";

            if (ModelState.IsValid)
            {
                var image = string.Empty;
                var folder = "~/Content/Images";

                if (complaintView.ImageFile != null)
                {
                    image = FilesHelper.UploadPhoto(complaintView.ImageFile, folder);
                    image = string.Format("{0}/{1}", folder, image);
                }

                var complaint = ToComplaint(complaintView);
                complaint.Image = image;

                db.Complaints.Add(complaint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(complaintView);
        }

        // GET: Complaints/Edit/5
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
            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintTypes, "ComplaintTypeId", "Description", complaint.ComplaintTypeId);
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComplaintId,ComplaintTypeId,Description,CaseAddress,Date,CreatedBy,Image")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComplaintTypeId = new SelectList(db.ComplaintTypes, "ComplaintTypeId", "Description", complaint.ComplaintTypeId);
            return View(complaint);
        }

        // GET: Complaints/Delete/5
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

        // POST: Complaints/Delete/5
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

        private Complaint ToComplaint(ComplaintView complaintView)
        {
            return new Complaint
            {
                ComplaintId = complaintView.ComplaintId,
                ComplaintTypeId = complaintView.ComplaintTypeId,
                Latitude = complaintView.Latitude,
                Longitude = complaintView.Longitude,
                Description = complaintView.Description,
                CaseAddress = complaintView.CaseAddress,
                Date = complaintView.Date,
                CreatedBy = complaintView.CreatedBy,
                Image = complaintView.Image,
            };
        }
    }
}
