using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace my_website.Models
{
    [Table("BlogComments")]
    public class BlogComment
    {
        [Key]
        public int ID { get; set; }
        public string Ip { get; set; }
        [MaxLength(70)]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}