using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Resources.Models;

namespace Web.Controllers
{
    public class NotificationController : ApiController
    {

        private readonly static Lazy<IHubConnectionContext<dynamic>> _instance = 
            new Lazy<IHubConnectionContext<dynamic>>(() => 
            GlobalHost.ConnectionManager.GetHubContext<MainHub>().Clients);

        [HttpPost]
        public void WorkspacesNotification(List<WorkspaceInfo> workspace)
        {
            _instance.Value.All.broadCastMessage(workspace);
        }
    }
}
