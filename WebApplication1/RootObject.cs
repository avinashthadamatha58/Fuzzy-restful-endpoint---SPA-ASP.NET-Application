using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class RootObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Image> Images { get; set; }
        public Location Location { get; set; }
        public DateTime Date { get; set; }
        public List<Comment> Comments { get; set; }
    }
    
    public class Image
    {
        public string Id { get; set; }
        public string Caption { get; set; }
    }

    public class Location
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }

    public class Comment
    {
        public string From { get; set; }
        public string Text { get; set; }
    }
}