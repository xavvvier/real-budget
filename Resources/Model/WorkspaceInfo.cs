using System.Collections.Generic;

namespace Resources.Models
{
    public class WorkspaceInfo : Notification
    {
        public int WorkspaceArtifactId { get; set; }
        public string WorkspaceName { get; set; }
        public List<User> User { get; set; }

    }
}