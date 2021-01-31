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
using REST_Agencia_Viajes;

namespace API_Agencia_Viajes.Controllers
{
    public class RESTViajerosController : ApiController
    {
        private API_AgenciaViajesDB db = new API_AgenciaViajesDB();

        // GET: api/RESTViajeros
        public IEnumerable<Viajeros> GetViajeros()
        {
            return db.Viajeros.ToList();
        }

        // GET: api/RESTViajeros/5
        [ResponseType(typeof(Viajeros))]
        public IHttpActionResult GetViajeros(int id)
        {
            Viajeros viajeros = db.Viajeros.Find(id);
            if (viajeros == null)
            {
                return NotFound();
            }

            return Ok(viajeros);
        }

        // PUT: api/RESTViajeros/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutViajeros(int id, Viajeros viajeros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != viajeros.ID_Viajeros)
            {
                return BadRequest();
            }

            db.Entry(viajeros).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViajerosExists(id))
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

        // POST: api/RESTViajeros
        [ResponseType(typeof(Viajeros))]
        public IHttpActionResult PostViajeros(Viajeros viajeros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Viajeros.Add(viajeros);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = viajeros.ID_Viajeros }, viajeros);
        }

        // DELETE: api/RESTViajeros/5
        [ResponseType(typeof(Viajeros))]
        public IHttpActionResult DeleteViajeros(int id)
        {
            Viajeros viajeros = db.Viajeros.Find(id);
            if (viajeros == null)
            {
                return NotFound();
            }

            db.Viajeros.Remove(viajeros);
            db.SaveChanges();

            return Ok(viajeros);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ViajerosExists(int id)
        {
            return db.Viajeros.Count(e => e.ID_Viajeros == id) > 0;
        }
    }
}