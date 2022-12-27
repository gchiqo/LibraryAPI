using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public DateTime Created
        {
            get { return Created; }
            set
            {
                if (DateTime.Today > value)
                    Created = value;
                else throw new InvalidOperationException();
            }
        }
    }
}