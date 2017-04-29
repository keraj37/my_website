using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_website.Models
{
    public class UserClient
    {
        public int ID { get; set; }
        public string IP { get; set; }
        public string Browser { get; set; }
        public string OS { get; internal set; }
    }
}