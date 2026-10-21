using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static DataModels.MvcVehiculosDBStoredProcedures;

namespace Progra6.Controllers
{
    [Authorize]
    public class MantenimientoController : Controller
    {
        // GET: Mantenimiento
        [Authorize]
        public ActionResult Index()
        {
            List<SpVehiculosListarDisponiblesResult> lista =
             new List<SpVehiculosListarDisponiblesResult>();

            try
            {
                using (var db = new DataModels.MvcVehiculosDB("MyDatabase"))
                {
                    lista = db.SpVehiculosListarDisponibles().ToList();
                }
            }
            catch 
            {
              
            } 
            return View(lista);
        }



        [Authorize]
        public ActionResult Crear()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult Crear(Vehiculo v)
        {
            string mensaje_backend = "";

            try
            {
                using (var db = new MvcVehiculosDB("MyDatabase"))
                {
                    db.SpVehiculosInsertar(
                        v.ModeloId,
                        v.Placa,
                        v.Año,
                        v.Color,
                        v.FechaCompra,
                        v.Disponible
                        );

                    mensaje_backend = "Se ha guardado con exito";
                }
            }

            catch
            {
                mensaje_backend = "No se guardo con exito";
            }
            ViewBag.Mensaje = mensaje_backend;
            return View();

        }

        


    }


}