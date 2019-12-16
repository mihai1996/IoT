using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IoT.Models
{
    public class Alert
    {
        public int Id { get; set; }

        [Display(Name = "Valoarea sensorului")]
        public int GasValue { get; set; }

        [Display(Name = "Înregistrat")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{HH:mm MM/dd/yyyy}")]
        public DateTime Registered { get; set; }
    }
}