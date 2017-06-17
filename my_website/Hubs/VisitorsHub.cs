using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace my_website.Hubs
{
    [HubName("VisitorsHub")]
    public class VisitorsHub : Hub
    {
        public void LogVisit()
        {
            Clients.All.spreadVisit(Context.Request.Environment["server.RemoteIpAddress"].ToString());
        }
    }
}