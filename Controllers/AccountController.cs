using DataModels;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;
using static DataModels.MvcVehiculosDBStoredProcedures;
using Progra6.Models;

    public class AccountController : Controller
    {

        

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UsuarioLogin usuario)
        {
            
            if (string.IsNullOrWhiteSpace(usuario.LoginName) ||
                string.IsNullOrWhiteSpace(usuario.Password))
            {
                ViewBag.Error = "Debe de Ingresar el usuario y/o la contraseña.";
                return View();
            }


            var user = new SpUsuariosLoginResult();



            using (var db = new MvcVehiculosDB("MyDatabase"))
            {
                user = db.SpUsuariosLogin(usuario.LoginName, usuario.Password).FirstOrDefault();
            }



            if (user != null)
            {
                var ticket = new FormsAuthenticationTicket(
                    1,
                    user.Usuario,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    user.Rol);
            

                var cookie = new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(ticket));

                Response.Cookies.Add(cookie);   


               return RedirectToAction("Index", "Home");

            }

            ViewBag.Error = "Usuario no encontrado.";

             return View();
            

        }


        // GET: Account/Logout

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");

        }
    }
