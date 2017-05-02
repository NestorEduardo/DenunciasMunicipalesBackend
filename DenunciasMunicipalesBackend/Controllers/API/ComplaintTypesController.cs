using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DenunciasMunicipalesBackend.Models;

namespace DenunciasMunicipalesBackend.Controllers.API
{
    public class ComplaintTypesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ComplaintTypes
        public IQueryable<ComplaintType> GetComplaintTypes()
        {
            return db.ComplaintTypes;
        }

        // GET: api/ComplaintTypes/5
        [ResponseType(typeof(ComplaintType))]
        public IHttpActionResult GetComplaintType(int id)
        {
            ComplaintType complaintType = db.ComplaintTypes.Find(id);
            if (complaintType == null)
            {
                return NotFound();
            }

            return Ok(complaintType);
        }

        // PUT: api/ComplaintTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComplaintType(int id, ComplaintType complaintType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != complaintType.ComplaintTypeId)
            {
                return BadRequest();
            }

            db.Entry(complaintType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintTypeExists(id))
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

        // POST: api/ComplaintTypes
        [ResponseType(typeof(ComplaintType))]
        public IHttpActionResult PostComplaintType(ComplaintType complaintType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ComplaintTypes.Add(complaintType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = complaintType.ComplaintTypeId }, complaintType);
        }

        // DELETE: api/ComplaintTypes/5
        [ResponseType(typeof(ComplaintType))]
        public IHttpActionResult DeleteComplaintType(int id)
        {
            ComplaintType complaintType = db.ComplaintTypes.Find(id);
            if (complaintType == null)
            {
                return NotFound();
            }

            db.ComplaintTypes.Remove(complaintType);
            db.SaveChanges();

            return Ok(complaintType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComplaintTypeExists(int id)
        {
            return db.ComplaintTypes.Count(e => e.ComplaintTypeId == id) > 0;
        }
    }
}