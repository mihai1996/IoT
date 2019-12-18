using Dapper;
using IoT.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IoT.Controllers
{
    public class GasController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult GasMeasure(Login log) // datele de la logare vin prin post in action
        {
            if (log.Username == "admin" && log.Password == "mihaiernest123") // se face validare
            {
                // totul e ok. pregatim datele pentru view si le aratam.
                var model = new TwiceTableModel();

                string measuresSQL = "SELECT TOP 10 * FROM Measures ORDER BY Registered DESC";
                string alertsSQL = "SELECT TOP 10 * FROM Alerts ORDER BY Registered DESC";

                using (SqlConnection conn = new SqlConnection(Settings.connection))
                {
                    model.Measures = conn.Query<Measure>(measuresSQL).ToList();
                    model.Alerts = conn.Query<Alert>(alertsSQL).ToList();

                    return View(model);
                }

            }
            else // daca validarea nu este corecta
            {
                Console.WriteLine("Invalid");
                return RedirectToAction("Login", "Logare"); // redirect la login
            }
            
        }
     
    }
}