using Dapper;
using IoT.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;

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
            if (log.Username == Settings.LoginCredentials.Login && log.Password == Settings.LoginCredentials.Password) // se face validare
            {
                // totul e ok. pregatim datele pentru view si le aratam.
                var model = new TwiceTableModel();

                string measuresSQL = "SELECT TOP 10 * FROM Measures ORDER BY Registered DESC";
                string alertsSQL = "SELECT TOP 10 * FROM Alerts ORDER BY Registered DESC";
                string limitSQL = "SELECT * FROM LIMIT";

                using (SqlConnection conn = new SqlConnection(Settings.DB_CONNECTION_STRING))
                {
                    model.Measures = conn.Query<Measure>(measuresSQL).ToList();
                    model.Alerts = conn.Query<Alert>(alertsSQL).ToList();
                    model.LIMIT = conn.Query<int>(limitSQL).FirstOrDefault();

                    return View(model);
                }

            }
            else // daca validarea nu este corecta
            {
                Console.WriteLine("Invalid");
                return RedirectToAction("Login", "Logare"); // redirect la login
            }

        }

        [HttpPost]
        public ActionResult GetGasLimit()
        {
            string limitSQL = "SELECT * FROM LIMIT";

            using (SqlConnection conn = new SqlConnection(Settings.DB_CONNECTION_STRING))
            {
                int limit = conn.Query<int>(limitSQL).FirstOrDefault();

                return Json(limit);
            }
        }

        [HttpPost]
        public ActionResult GetLastMeasure()
        {
            string lastMeasureSQL = "SELECT GasValue, Registered FROM LastMeasure";

            using (SqlConnection conn = new SqlConnection(Settings.DB_CONNECTION_STRING))
            {
                var lastMeasure = conn.Query<LastMeasure>(lastMeasureSQL).FirstOrDefault();
                //var result = new { data1 = lastMeasure.GasValue, data2 = lastMeasure.Registered };
                var viewModel = new LastMeasureViewModel();
                if (lastMeasure != null)
                {
                    viewModel.Id = lastMeasure.Id;
                    viewModel.GasValue = lastMeasure.GasValue;
                    viewModel.Registered = lastMeasure.Registered.ToString("hh:mm:ss dd/MM/yyyy");
                }
                
                return Json(viewModel);
            }

        }

    }
}