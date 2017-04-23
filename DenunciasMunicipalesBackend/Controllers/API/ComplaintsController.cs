﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using DenunciasMunicipalesBackend.Models;

namespace DenunciasMunicipalesBackend.Controllers.API
{
    public class ComplaintsController : ApiController
    {
        private DataContext db = new DataContext();

        public IQueryable<Complaint> GetComplaints()
        {
            return db.Complaints;
        }

        [ResponseType(typeof(Complaint))]
        public IHttpActionResult GetComplaint(int id)
        {
            Complaint complaint = db.Complaints.Find(id);

            if (complaint == null)
            {
                return NotFound();
            }

            return Ok(complaint);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutComplaint(int id, Complaint complaint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != complaint.ComplaintId)
            {
                return BadRequest();
            }

            db.Entry(complaint).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Complaint))]
        public IHttpActionResult PostComplaint(Complaint complaint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Complaints.Add(complaint);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = complaint.ComplaintId }, complaint);
        }

        [ResponseType(typeof(Complaint))]
        public IHttpActionResult DeleteComplaint(int id)
        {
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return NotFound();
            }

            db.Complaints.Remove(complaint);
            db.SaveChanges();

            return Ok(complaint);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool ComplaintExists(int id)
        {
            return db.Complaints.Count(e => e.ComplaintId == id) > 0;
        }
    }
}