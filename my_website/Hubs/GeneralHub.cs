using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace my_website.Hubs
{
    /*
     *  Sending message to specific user/group:
     *   https://docs.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/mapping-users-to-connections#IUserIdProvider
     */

    [HubName("GeneralHub")]
    public class GeneralHub : Hub
    {
        public void LogVisit()
        {
            Clients.All.spreadMessage("New connection: " + Context.Request.Environment["server.RemoteIpAddress"].ToString());
        }

        public void SendChatMessage(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

        public void UpdateWebCamStream(string name, string image)
        {
            TryCreatingGroup();

            Clients.Group(name).updateWebCamStreamGroup(image);
            Clients.All.updateWebCamStreamAll(image);
            Clients.User(name).updateWebCamStreamUser(image);
        }

        private void TryCreatingGroup()
        {
            try
            {
                string name = Context.User != null ? Context.User.Identity.Name : "quest";
                Groups.Add(Context.ConnectionId, name);
                Clients.All.spreadMessage("Group created: " + name);
            }
            catch (Exception e)
            {
                //Clients.All.spreadMessage(e.Message);
            }
        }

        public override Task OnConnected()
        {
            TryCreatingGroup();
            return base.OnConnected();
        }
    }
}