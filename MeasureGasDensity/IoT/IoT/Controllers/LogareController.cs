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
            return View();
        }

    }
}