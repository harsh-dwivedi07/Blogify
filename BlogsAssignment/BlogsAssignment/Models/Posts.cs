using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BlogsAssignment.Models
{
    public class Posts
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid AuthorId { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime PublishedOn { get; set; }
        public string ImageUrl { get; set; }
        public byte[] ImageData { get; set; }

    }
    public class BlogsProxy
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string PublishedDate { get; set; }
    }
}