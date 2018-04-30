namespace TripXpert.Models
{
    public class Attraction
    {
        public int AttractionID { get; set; }
        public int DestinationID { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public Image Image { get; set; }
    }
}