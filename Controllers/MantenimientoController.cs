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
        public ActionResult Crear(int? id)  // Mantenimiento/Crear (es nulo) o Mantenimiento/Crear/3 (no es nulo)
        {
            SpVehiculosObtenerPorIdResult modelo;
            try
            {
                if (id == null)  // Crear
                {
                    modelo = new SpVehiculosObtenerPorIdResult();
                    using (var db = new MvcVehiculosDB("MyDatabase"))
                    {
                        ViewBag.modelos = db.SpObtenerModelosMarcasActivas().ToList();
                    }
                }
                else // Modificar
                {
                    using (var db = new MvcVehiculosDB("MyDatabase"))
                    {
                        modelo = db.SpVehiculosObtenerPorId(id).FirstOrDefault();
                        ViewBag.modelos = db.SpObtenerModelosMarcasActivas().ToList();
                    }

                    if (modelo == null)
                    {
                        return HttpNotFound();
                    }
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

            return View(modelo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Crear(SpVehiculosObtenerPorIdResult v)
        {
            string mensaje_backend = "";
            try
            {
                using (var db = new MvcVehiculosDB("MyDatabase"))
                {
                    ViewBag.modelos = db.SpObtenerModelosMarcasActivas().ToList();
                    if (v.VehiculoId == 0)
                    {
                        //Insertar
                        db.SpVehiculosInsertar(v.ModeloId, v.Placa, v.Año, v.Color, v.FechaCompra, v.Disponible);
                    }
                    else
                    {
                        //Actualizar
                        db.SpVehiculosActualizar(v.VehiculoId, v.ModeloId, v.Placa, v.Año, v.Color, v.FechaCompra, v.Disponible);
                    }

                    mensaje_backend = (v.VehiculoId == 0) ? "Se ha guardado con exito" : "Se ha modificado con exito";

                }
            }
            catch
            {
                mensaje_backend = "No se guardo con exito";
            }

            ViewBag.Mensaje = mensaje_backend;
            return View(v);
        }




        [Authorize]
        public ActionResult Eliminar(int id)
        {
            List<SpVehiculosListarDisponiblesResult> lista =
                    new List<SpVehiculosListarDisponiblesResult>();

            try
            {
                using (var db = new DataModels.MvcVehiculosDB("MyDatabase"))
                {
                    db.SpVehiculosEliminar(id);
                    lista = db.SpVehiculosListarDisponibles().ToList();
                }
            }
            catch
            {

            }
            return View("Index", lista);
        }


    }


}