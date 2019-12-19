using Dapper;
using IoT.Models;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace IoT.Controllers
{
    public class GasController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        public ActionResult GasMeasure(Login log) // datele de la logare vin prin post in action
        {
            if (log.Username == Settings.LoginCredentials.Login && log.Password == Settings.LoginCredentials.Password) // se face validare
            {
                // totul e ok. pregatim datele pentru view si le aratam.
                var model = new TwiceTableModel();

                string measuresSQL = "SELECT TOP 10 * FROM Measures ORDER BY Registered DESC";
                string alertsSQL = "SELECT TOP 10 * FROM Alerts ORDER BY Registered DESC";

                using (SqlConnection conn = new SqlConnection(Settings.DB_CONNECTION_STRING))
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