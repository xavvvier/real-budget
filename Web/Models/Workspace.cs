using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Workspace : Notification
    {
        public int WorkspaceArtifactId { get; set; }
        public string WorkspaceName { get; set; }
        public List<User> User { get; set; }

    }
}