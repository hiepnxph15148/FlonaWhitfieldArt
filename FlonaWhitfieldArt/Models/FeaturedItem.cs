using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlonaWhitfieldArt.Models
{
    public class FeaturedItem
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public string ImageUrl { get; set; }

        public string LinkUrl { get; set; }

        public FeaturedItem (string name, string cateogry, string imageUrl, string linkUrl)
        {
            Name = name;
            Category = Category;
            ImageUrl = imageUrl;
            LinkUrl = linkUrl;
        }
   }
}