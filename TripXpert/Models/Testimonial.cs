using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripXpert.Models
{
    public class Testimonial
    {
        public Testimonial()
        {
        }
        public Testimonial(DAL.Testimonial testimonial)
        {
            this.ID = testimonial.ID;
            this.TestimonialContent = testimonial.TestimonialContent;
            this.Author = testimonial.Author;
        }

        public int ID { get; set; }
        public string TestimonialContent { get; set; }
        public string Author { get; set; }
    }
}