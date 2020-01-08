using IoT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IoT.Controllers
{
    public class LogareController : Controller
    {
        //GET: Logare
        [HttpGet]
        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login log)
        {
            Session["uname"] = log.Username;
            Session["psw"] = log.Password;
            
            if ((string)Session["uname"] == Settings.LoginCredentials.Login && (string)Session["psw"] == Settings.LoginCredentials.Password)
            {
                return RedirectToAction("GasMeasure", "Gas");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}