using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Notification
    {
        public int WorkspaceID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public int TotalEdits { get; set; }
        public int DistinctEdits { get; set; }
        public int AverageTime { get; set; }
    }
}