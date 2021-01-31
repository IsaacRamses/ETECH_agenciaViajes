using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using REST_Agencia_Viajes;

namespace API_Agencia_Viajes.Controllers
{
    public class SolicitarViajeController : Controller
    {
        private API_AgenciaViajesDB db = new API_AgenciaViajesDB();
        // GET: SolicitarViaje

        public ActionResult Index()
        {

            ViewBag.Controller = "SolicitarViaje";
            ViewBag.Viajeros = db.Viajeros.ToList();
            ViewBag.ViajerosViajes = db.SP_Traer_Viajeros_Viajes().ToList();

            return View();
        }

        // GET: SolicitarViaje/Create
        public ActionResult Create()
        {
            var Viajes_DISP = (from V in db.Viajes_Disponibles select new { ID = V.ID_Viajes_Disponibles, Valor = V.COD_Viaje + " " + V.Lugar_Origen + "-" + V.Lugar_Destino }).ToList();
            ViewBag.Viajes = new SelectList(Viajes_DISP, "ID","Valor");

            return View();
        }
        [HttpPost]
        public ActionResult Create(int? IDViajero,int Cedula,int IDViaje)
        {
            var Validar = true;
            try
            {
                if (IDViajero != null)
                {

                    Viajeros_Viajes Nuevo = new Viajeros_Viajes();
                    Nuevo.ID_FK_Viajeros = Convert.ToInt32(IDViajero);
                    Nuevo.ID_FK_Viajes_Disponibles = IDViaje;
                    db.Viajeros_Viajes.Add(Nuevo);
                    db.SaveChanges();

                    return Json(new { Validar }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var Viajero = db.Viajeros.Where(x => x.Cedula == Cedula).ToList();
                    if (Viajero.Count() != 0)
                    {
                        Viajeros_Viajes Nuevo = new Viajeros_Viajes();
                        Nuevo.ID_FK_Viajeros = Viajero.FirstOrDefault().ID_Viajeros;
                        Nuevo.ID_FK_Viajes_Disponibles = IDViaje;
                        db.Viajeros_Viajes.Add(Nuevo);
                        db.SaveChanges();
                        return Json(new { Validar }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        var Mensaje = "Este viajero no se encuentra registrado";
                        Validar = false;
                        return Json(new { Validar, Mensaje }, JsonRequestBehavior.AllowGet);
                    }                      
                }
            }
            catch (Exception ex)
            {
                var Mensaje = "Error Interno: " + ex.Message + " Linea: " + Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' '))).ToString();
                Validar = false; //Capturo el error y lo envio al Request
                return Json(new { Validar, Mensaje }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            
                var Validar = true;
                try
                {
                    Viajeros_Viajes Viajes = db.Viajeros_Viajes.Find(id);
                    db.Viajeros_Viajes.Remove(Viajes);
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

        [HttpPost]
        public ActionResult BuscarViajero(int Cedula)
        {
            var Validar = true;
            try
            {
                var Viajero = db.Viajeros.Where(x => x.Cedula == Cedula).ToList();


                if (Viajero.Count() != 0)
                {
                    var Mensaje = Viajero.FirstOrDefault().Nombre.ToString();
                    var ID = Viajero.FirstOrDefault().ID_Viajeros.ToString();
                    return Json(new { Validar, Mensaje, ID }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var Mensaje = "Este viajero no se encuentra Registrado";
                    Validar = false;
                    return Json(new { Validar, Mensaje }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                var Mensaje = "Error Interno: " + ex.Message + " Linea: " + Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' '))).ToString();
                Validar = false; //Capturo el error y lo envio al Request
                return Json(new { Validar, Mensaje }, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}