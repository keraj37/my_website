using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace my_website.Hubs
{
    [HubName("As3tocsHub")]
    public class As3tocsHub : Hub
    {
        public void Send(string key, string className, string csFile)
        {
            //TODO Most likly I don't need this, as client usues proxy bind
            var context = GlobalHost.ConnectionManager.GetHubContext<As3tocsHub>();
            context.Clients.All.send(key, csFile, className);
        }
    }
}