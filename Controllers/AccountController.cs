using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static DataModels.MvcVehiculosDBStoredProcedures;


namespace Progra6.Controllers
{
    public class AccountController : Controller
    {

        

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(String t_usuario, string t_password)
        {
            
            if (string.IsNullOrWhiteSpace(t_usuario) ||
                string.IsNullOrWhiteSpace(t_password))
            {
                ViewBag.Error = "Debe de Ingresar el usuario y/o la contraseña.";
                return View();
            }


            var user = new SpUsuariosLoginResult();



            using (var db = new MvcVehiculosDB("MyDataBase"))
            {
                user = db.SpUsuariosLogin(t_usuario, t_password).FirstOrDefault();
            }

            if (user != null)
            {
                var ticket = new FormsAuthenticationTicket(
                    1, user.Usuario,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    user.Rol);
            }

                var cookie = new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(ticket));

                Response.Cookies.Add(cookie);   


               return RedirectToAction("Index", "Home");
             }
            
             ViewBag.Error = "Usuario no encontrado.";

             return View();
            

        }

    }

}