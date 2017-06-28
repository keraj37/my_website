﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace my_website.Hubs
{
    [HubName("WebCamHub")]
    public class WebCamHub : Hub
    {
        public void UpdateWebCamStream(string name, string image)
        {
            Clients.Group(name).updateWebCamStreamGroup(image);
            Clients.All.updateWebCamStreamAll(image);
            Clients.User(name).updateWebCamStreamUser(image);
        }
    }
}