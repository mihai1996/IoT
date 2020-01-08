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

        public ActionResult Index(TwiceTableModel model)
        {
            var a = model.Limit.LIMIT;
            return View(a);
        }


        [HttpGet]
        public ActionResult GasMeasure() // datele de la logare vin prin post in action
        {

            if (Session["uname"] != null && Session["psw"] != null)
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
                    model.Limit = conn.Query<Limit>(limitSQL).FirstOrDefault();
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Login", "Logare");
            }
        }
        
        [HttpPost]
        public ActionResult GetGasLimit()
        {
            string limitSQL = "SELECT * FROM LIMIT";

            using (SqlConnection conn = new SqlConnection(Settings.DB_CONNECTION_STRING))
            {
                var limit = conn.Query<Limit>(limitSQL).FirstOrDefault();

                var limitView = new LimitViewModel();

                if(limit != null)
                {
                    limitView.LIMIT = limit.LIMIT;
                    limitView.Id = limit.Id;
                }

                return Json(limitView);
            }
        }

        [HttpPost]
        public ActionResult GetLastMeasure()
        {
            string lastMeasureSQL = "SELECT GasValue, Registered FROM LastMeasure";

            using (SqlConnection conn = new SqlConnection(Settings.DB_CONNECTION_STRING))
            {
                var lastMeasure = conn.Query<LastMeasure>(lastMeasureSQL).FirstOrDefault();
    
                var viewModel = new LastMeasureViewModel();
                if (lastMeasure != null)
                {
                    viewModel.Id = lastMeasure.Id;
                    viewModel.GasValue = lastMeasure.GasValue;
                    viewModel.Registered = lastMeasure.Registered.ToString("HH:mm MM/dd/yyyy");
                }
                
                return Json(viewModel);
            }
        }

        [HttpGet]
        public ActionResult EditLimit(int id)
        {
            if (Session["uname"] != null && Session["psw"] != null)
            {
                Limit _limit = new Limit();
                string limSQL = "SELECT * FROM Limit Where @Id = @id";
                using (SqlConnection conn = new SqlConnection(Settings.DB_CONNECTION_STRING))
                {
                    _limit = conn.Query<Limit>(limSQL, new { id }).FirstOrDefault();
                }
                return View(_limit);
            }
            else
            {
                return RedirectToAction("Login", "Logare");
            }     
        }

        [HttpPost]
        public ActionResult EditLimit(Limit _limit)
        {
           string editLimitSQL = "UPDATE Limit SET LIMIT = @LIMIT, Id = @Id";

            using (SqlConnection conn = new SqlConnection(Settings.DB_CONNECTION_STRING))
            {
                int rowsAffected = conn.Execute(editLimitSQL, _limit);
            }

            return RedirectToAction("GasMeasure", "Gas");
        }

    }
}
