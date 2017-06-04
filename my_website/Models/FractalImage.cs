using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace my_website.Models
{
    [Bind(Exclude = "ID")]
    public class FractalImage
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageBase64 { get; set; }
    }
}