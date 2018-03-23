using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class User : Notification
    {
        public int UserArtifactId { get; set; }
        public string UserName { get; set; }
    }
}