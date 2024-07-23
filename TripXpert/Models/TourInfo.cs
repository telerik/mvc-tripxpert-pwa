using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TripXpert.Models
{
    public class TourInfo
    {
        [Key]
        public int InfoID { get; set; }
        public int DestinationID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PerPersonSharing { get; set; }
        public int? PerSingleOccupancy { get; set; }
        public int? PerChild { get; set; }
        public short? Duration { get; set; }
        public List<DateTime>TourDates { get; set; }
    }
}