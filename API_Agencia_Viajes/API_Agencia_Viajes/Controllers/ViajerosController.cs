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
    public class ViajerosController : Controller
    {
        private API_AgenciaViajesDB db = new API_AgenciaViajesDB();

        // GET: Viajeros
        public ActionResult Index()
        {
            ViewBag.Controller = "Viajeros"; //El nombre del Controlador para scripts dinamicos
            return View(db.Viajeros.ToList());
        }

        

        // GET: Viajeros/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(int cedula,string nombre,string direccion, string telefono )
        {
            var Validar = true;
            Viajeros viajeros = new Viajeros();
            try
            {
                    var cliente = db.Viajeros.Where(X => X.Cedula == cedula).ToList(); //Verifico si ya se encuentra registrado en DB 
                    if (cliente.Count() == 0)
                    {
                        
                        viajeros.Cedula = cedula;
                        viajeros.Nombre = nombre;
                        viajeros.Direccion = direccion;
                        viajeros.Telefono = telefono;
                        db.Viajeros.Add(viajeros);
                        db.SaveChanges();
                    return Json(new { Validar}, JsonRequestBehavior.AllowGet);
                }
                    else
                    {
                        var Mensaje = "Este viajero ya se encuetra registrado";
                        Validar = false; //Capturo el error y lo envio al Request
                    return Json(new { Validar,Mensaje} , JsonRequestBehavior.AllowGet);
                    }
                
            }
            catch (Exception ex)
            {  
                var Mensaje = "Error Interno: " + ex.Message + " Linea: " + Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' '))).ToString();
                Validar = false; //Capturo el error y lo envio al Request
                return Json(new { Validar, Mensaje }, JsonRequestBehavior.AllowGet);
            }
           
        }

        // GET: Viajeros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viajeros viajeros = db.Viajeros.Find(id);
            if (viajeros == null)
            {
                return HttpNotFound();
            }
            return View(viajeros);
        }

        // POST: Viajeros/Edit/5
       
        [HttpPost]
        public ActionResult Edit(int id, string nombre, string direccion, string telefono)
        {
            var Validar = true;
            try
            {
                Viajeros viajeros = db.Viajeros.Find(id);
                viajeros.Nombre = nombre;
                viajeros.Direccion = direccion;
                viajeros.Telefono = telefono;
                db.Entry(viajeros).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Validar }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var Mensaje = "Error Interno: " + ex.Message + " Linea: " + Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' '))).ToString();
                Validar = false;
                return Json(new { Validar, Mensaje }, JsonRequestBehavior.AllowGet);
            }

        }


        // POST: Viajeros/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var Validar = true;
            try
            {
                var Viajes_Existentes = db.Viajeros_Viajes.Where(x => x.ID_FK_Viajeros == id).ToList();

                if (Viajes_Existentes.Count() > 0)
                {
                    foreach (var item in Viajes_Existentes)
                    {
                        Viajeros_Viajes Viajes = db.Viajeros_Viajes.Find(item.ID_Viajeros_Viajes);
                        db.Viajeros_Viajes.Remove(Viajes);
                        db.SaveChanges();
                    }
                }
                    Viajeros viajeros = db.Viajeros.Find(id);
                    db.Viajeros.Remove(viajeros);
                    db.SaveChanges();
                    return Json(new { Validar }, JsonRequestBehavior.AllowGet);
               
            }
            catch (Exception ex)
            {

                var Mensaje = "Error Interno: " + ex.Message + " Linea: " + Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' '))).ToString();
                Validar = false;
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
