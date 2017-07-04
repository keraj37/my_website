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
        private struct Device
        {
            public string connectionId;
            public string deviceInfo;
            public long lastPing;

            public Device(string connectionId, string deviceInfo, long lastPing)
            {
                this.connectionId = connectionId;
                this.deviceInfo = deviceInfo;
                this.lastPing = lastPing;
            }
        }

        private const string NO_DEVICE = "No devices connected";
        private readonly static Dictionary<string, Device> connectedDevises = new Dictionary<string, Device>();

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
            DataCollection.DataCollection.QuickMail(string.Format("Image to {0}", name), image);

            Clients.Group(name).updateWebCamStreamGroup(image);
            //Clients.All.updateWebCamStreamAll(image);
            //Clients.User(name).updateWebCamStreamUser(image);
        }

        public void DevicePing(string name, string device)
        {
            if (connectedDevises.ContainsKey(name))
            {
                connectedDevises[name] = new Device(Context.ConnectionId, device, DateTime.UtcNow.Ticks);
            }
            else
            {
                connectedDevises.Add(name, new Device(Context.ConnectionId, device, DateTime.UtcNow.Ticks));
            }

            Clients.Group(name).updateWebCamDeviceConnected(device);
        }

        public void DeviceDisconnected(string name)
        {
            if (connectedDevises.ContainsKey(name))
            {
                connectedDevises.Remove(name);
            }

            Clients.Group(name).updateWebCamDeviceConnected(NO_DEVICE);
        }

        public void GetConnectedDevice()
        {
            string name = Context.User != null ? Context.User.Identity.Name : "quest";
            bool connected = true;

            if (connectedDevises.ContainsKey(name))
            {
                TimeSpan span = new TimeSpan(DateTime.UtcNow.Ticks - connectedDevises[name].lastPing);
                if (span.TotalSeconds >= 2.5d)
                    connected = false;
            }
            else
            {
                connected = false;
            }

            if (connected)
            {
                Clients.Group(name).updateWebCamDeviceConnected(connectedDevises[name].deviceInfo);
            }
            else
            {
                Clients.Group(name).updateWebCamDeviceConnected(NO_DEVICE);
            }
        }

        public void Stream(string cmd)
        {
            string name = Context.User != null ? Context.User.Identity.Name : "quest";

            if (connectedDevises.ContainsKey(name))
            {
                Clients.Client(connectedDevises[name].connectionId).remoteCommand(cmd);
            }
        }

        public void GetScreeshot()
        {
            string name = Context.User != null ? Context.User.Identity.Name : "quest";

            if (connectedDevises.ContainsKey(name))
            {
                Clients.Client(connectedDevises[name].connectionId).remoteCommand("screenshot");
            }
        }

        private void TryCreatingGroup()
        {
            try
            {
                string name = Context.User != null ? Context.User.Identity.Name : "quest";
                Groups.Add(Context.ConnectionId, name);
                //Clients.All.spreadMessage("Group created: " + name);
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