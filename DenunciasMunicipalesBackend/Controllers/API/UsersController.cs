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
using DenunciasMunicipalesBackend.Classes;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DenunciasMunicipalesBackend.Controllers.API
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string email, string password)
        {
            User user = db.Users.FirstOrDefault(u => u.Email == email);

            if (user.Password == password)
            {
                return Ok(user);
            }

            return NotFound();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string email)
        {
            User user = db.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("GetUserByEmail")]
        public async Task<IHttpActionResult> GetUserByEmail(JObject form)
        {
            var email = string.Empty;
            dynamic jsonObject = form;

            try
            {
                email = jsonObject.Email.Value;
            }
            catch
            {
                return BadRequest("Incorrect call");
            }

            var user = await db.Users.Where (u => u.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            MailHelper.SendMail(user.Email, "Bienvenido/a", $"Hola { user.FullName}, Gracias por registrarte en Denuncias Municipales App.Con tu ayuda haremos que Santo Domingo Este sea una mejor ciudad");

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}