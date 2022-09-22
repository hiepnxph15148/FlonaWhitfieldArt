using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlonaWhitfieldArt.Models
{
    public class TestimonialsModel
    {
        public string Title { get; set; }
        public string Introduction { get; set; }

        public List<TestimonialModel> Testimonials { get; set; }

        public bool HasTestmoials { get { return Testimonials != null && Testimonials.Count > 0; } }

        public string ColumClass
        {
            get
            {
                switch (Testimonials.Count)
                {
                    case 1:
                        return "col-md-12";
                    case 2:
                        return "col-md-6";
                    case 3:
                        return "col-md-4";
                    case 4:
                        return "col-md-3";
                    default:
                        return "col-md-4";
                }
            }
        }
        public TestimonialsModel(string title, string introduction, List<TestimonialModel> testimonials)
        {
            Title = title;
            Introduction = introduction;
            Testimonials = testimonials;
        }
    }
}