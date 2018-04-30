using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripXpert.Models
{
    public class Marker
    {
        public Marker(double latitude, double longitude, string name)
        {
            this.LatLng = new double[] { latitude, longitude };
            this.Name = name;
        }

        public double[] LatLng { get; set; }
        public string Name { get; set; }
    }
}