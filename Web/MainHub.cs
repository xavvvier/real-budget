using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Web
{
    public class MainHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.broadCastMessage(name, message);
        }
    }
}