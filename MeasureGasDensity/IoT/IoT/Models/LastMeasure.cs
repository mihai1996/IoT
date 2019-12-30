using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoT.Models
{
    public class LastMeasure
    {
        public int Id { get; set; }

        public int GasValue { get; set; }

        public DateTime Registered { get; set; }
    }
}