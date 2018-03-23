using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Notification
    {
        public int CostDay { get; set; }
        public int ViewsHour { get; set; }
        public int ViewsHourBadge { get; set; }
        public int AverageTime { get; set; }
        public int EditsHour { get; set; }
        public int EditsHourBadge { get; set; }
    }
}