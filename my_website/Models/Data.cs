﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace my_website.Models
{
    public class Data
    {
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public string Subject { get; set; }
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
    }
}