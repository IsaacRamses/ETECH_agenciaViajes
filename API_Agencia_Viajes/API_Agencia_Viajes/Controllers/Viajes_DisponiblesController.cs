using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using REST_Agencia_Viajes;

namespace API_Agencia_Viajes.Controllers
{
    public class Viajes_DisponiblesController : Controller
    {
        private API_AgenciaViajesDB db = new API_AgenciaViajesDB();

        // GET: Viajes_Disponibles
        public ActionResult Index()
        {
            ViewBag.Controller = "Viajes_Disponibles";
            return View(db.Viajes_Disponibles.ToList());
        }


        // GET: Viajes_Disponibles/Create
        public ActionResult Create()
        {
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Viajes_Disponibles,COD_Viaje,NUM_Plazas,Lugar_Origen,Lugar_Destino,Precio")] Viajes_Disponibles viajes_Disponibles)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Viajes_Disponibles.Add(viajes_Disponibles);
                    db.SaveChanges();
                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }  
        }

        // GET: Viajes_Disponibles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viajes_Disponibles viajes_Disponibles = db.Viajes_Disponibles.Find(id);
            if (viajes_Disponibles == null)
            {
                return HttpNotFound();
            }
            return View(viajes_Disponibles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Viajes_Disponibles,COD_Viaje,NUM_Plazas,Lugar_Origen,Lugar_Destino,Precio")] Viajes_Disponibles viajes_Disponibles)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(viajes_Disponibles).State = EntityState.Modified;
                    db.SaveChanges();
                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        // POST: Viajes_Disponibles/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var Validar = true;
            try
            {
                var Viajes_Existentes = db.Viajeros_Viajes.Where(x => x.ID_FK_Viajes_Disponibles == id).ToList();

                if (Viajes_Existentes.Count() > 0)
                {
                    foreach (var item in Viajes_Existentes)
                    {
                        Viajeros_Viajes Viajes = db.Viajeros_Viajes.Find(item.ID_Viajeros_Viajes);
                        db.Viajeros_Viajes.Remove(Viajes);
                        db.SaveChanges();
                    }
                }

                Viajes_Disponibles viajes_Disponibles = db.Viajes_Disponibles.Find(id);
                db.Viajes_Disponibles.Remove(viajes_Disponibles);
                db.SaveChanges();

                return Json(new { Validar }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                var Mensaje = "Error Interno: " + ex.Message + " Linea: " + Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' '))).ToString();
                Validar = false; //Capturo el error y lo envio al Request
                return Json(new { Validar, Mensaje }, JsonRequestBehavior.AllowGet);
            }
        
            
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
