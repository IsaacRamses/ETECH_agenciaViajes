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
    public class RESTViajes_DisponiblesController : ApiController
    {
        private API_AgenciaViajesDB db = new API_AgenciaViajesDB();

        // GET: api/RESTViajes_Disponibles
        public IQueryable<Viajes_Disponibles> GetViajes_Disponibles()
        {
            return db.Viajes_Disponibles;
        }

        // GET: api/RESTViajes_Disponibles/5
        [ResponseType(typeof(Viajes_Disponibles))]
        public IHttpActionResult GetViajes_Disponibles(int id)
        {
            Viajes_Disponibles viajes_Disponibles = db.Viajes_Disponibles.Find(id);
            if (viajes_Disponibles == null)
            {
                return NotFound();
            }

            return Ok(viajes_Disponibles);
        }

        // PUT: api/RESTViajes_Disponibles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutViajes_Disponibles(int id, Viajes_Disponibles viajes_Disponibles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != viajes_Disponibles.ID_Viajes_Disponibles)
            {
                return BadRequest();
            }

            db.Entry(viajes_Disponibles).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Viajes_DisponiblesExists(id))
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

        // POST: api/RESTViajes_Disponibles
        [ResponseType(typeof(Viajes_Disponibles))]
        public IHttpActionResult PostViajes_Disponibles(Viajes_Disponibles viajes_Disponibles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Viajes_Disponibles.Add(viajes_Disponibles);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = viajes_Disponibles.ID_Viajes_Disponibles }, viajes_Disponibles);
        }

        // DELETE: api/RESTViajes_Disponibles/5
        [ResponseType(typeof(Viajes_Disponibles))]
        public IHttpActionResult DeleteViajes_Disponibles(int id)
        {
            Viajes_Disponibles viajes_Disponibles = db.Viajes_Disponibles.Find(id);
            if (viajes_Disponibles == null)
            {
                return NotFound();
            }

            db.Viajes_Disponibles.Remove(viajes_Disponibles);
            db.SaveChanges();

            return Ok(viajes_Disponibles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Viajes_DisponiblesExists(int id)
        {
            return db.Viajes_Disponibles.Count(e => e.ID_Viajes_Disponibles == id) > 0;
        }
    }
}