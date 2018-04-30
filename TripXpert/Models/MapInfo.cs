using System.Collections;
using System.Collections.Generic;

namespace TripXpert.Models
{
    public class MapInfo
    {
        public Hashtable ZoomSettings { get; set; }
        public IEnumerable<Marker> Markers { get; set; }

    }
}