using System.ComponentModel.DataAnnotations;

namespace TripXpert.Models
{
    public class Image
    {
        public Image()
        {
        }
        public Image(DAL.Image image)
        {
            this.ImageID = image.ImageID;
            this.Description = image.Description;
            this.Author = image.Author;
            this.ImageURL = image.ImageURL;
            this.Title = image.Title;
            this.FolderName = image.FolderName;
        }

        [Key]
        public int ImageID { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public string FolderName { get; set; }
    }
}