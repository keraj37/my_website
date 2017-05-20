using System;
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

        public override string ToString()
        {
            return string.Format("ID: {0}\nTime: {1}\nSubject: {2}\nBody:\n{3}", ID.ToString(), Time.ToString(), Subject, Body);
        }
    }
}