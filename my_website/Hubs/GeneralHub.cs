using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace my_website.Hubs
{
    [HubName("GeneralHub")]
    public class GeneralHub : Hub
    {
        public void LogVisit()
        {
            Clients.All.spreadVisit(Context.Request.Environment["server.RemoteIpAddress"].ToString());
        }

        public void SendChatMessage(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

        //Moved to seperate hub - it was the only way to make it work :(
        /*
        public void SendCSFile(string key, string className, string csFile)
        {
            //TODO Most likly I don't need this, as client usues proxy
            var context = GlobalHost.ConnectionManager.GetHubContext<GeneralHub>();
            context.Clients.All.send(key, csFile, className);
        }
        */
    }
}