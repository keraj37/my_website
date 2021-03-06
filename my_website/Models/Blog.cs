﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace my_website.Models
{
    public class Blog
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string User { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        //public List<BlogComment> Comments { get; set; }
    }
}