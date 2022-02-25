using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheBestNewBlog.Models
{
    public class NewsModel
    {

        public int Id { get; set; }
        public string Tittle { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Status { get; set; }
        public string Content { get; set; }

    }
}