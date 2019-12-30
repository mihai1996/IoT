using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoT.Models
{
    public class TwiceTableModel
    {
        public IEnumerable<IoT.Models.Measure> Measures { get; set; }

        public IEnumerable<IoT.Models.Alert> Alerts { get; set; }

        public int LIMIT { get; set; }

    }
}