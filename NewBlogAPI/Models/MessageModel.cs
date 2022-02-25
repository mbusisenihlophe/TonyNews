using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewBlogAPI.Models
{
    public class MessageModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message1 { get; set; }
        public short Status { get; set; }
    }
}