﻿using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers
{
    public class NotificationController : ApiController
    {

        private readonly static Lazy<IHubConnectionContext<dynamic>> _instance = 
            new Lazy<IHubConnectionContext<dynamic>>(() => 
            GlobalHost.ConnectionManager.GetHubContext<MainHub>().Clients);

        private readonly static Lazy<IHubConnectionContext<dynamic>> _instance2 =
            new Lazy<IHubConnectionContext<dynamic>>(() =>
            GlobalHost.ConnectionManager.GetHubContext<MainHub2>().Clients);

        [HttpPost]
        public void WorkspacesNotification(List<Workspace> workspace)
        {
            _instance.Value.All.broadCastMessage(workspace);
        }
        [HttpPost]

        public void SummaryNotification(List<Workspace> workspace)
        {
            _instance2.Value.All.broadCastMessage(workspace);
        }
    }
}
