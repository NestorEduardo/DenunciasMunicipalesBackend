using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DenunciasMunicipalesBackend.Models;
using System;
using DenunciasMunicipalesBackend.Classes;

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

        private Complaint ToComplaint(ComplaintView complaintView)
        {
            return new Complaint
            {
                ComplaintId = complaintView.ComplaintId,
                Description = complaintView.Description,
                CaseAddress = complaintView.CaseAddress,
                Date = complaintView.Date,
                CreatedBy = complaintView.CreatedBy,
                Image = complaintView.Image,
            };
        }
    }
}
