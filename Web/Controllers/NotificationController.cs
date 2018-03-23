using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers
{
    public class NotificationController : ApiController
    {

        private readonly static Lazy<IHubConnectionContext<dynamic>> _instance = 
            new Lazy<IHubConnectionContext<dynamic>>(() => 
            GlobalHost.ConnectionManager.GetHubContext<MainHub>().Clients);

        [HttpPost]
        public void BroadcastNotification(Notification notification)
        {
            _instance.Value.All.broadCastMessage(notification.UserName, "Test");
        }
    }
}
