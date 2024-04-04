using System.ComponentModel.DataAnnotations;

namespace WATCHWAWE.Models
{
    public class UpcomingDto
    {
        [Required,MaxLength(100)]
        public string Name { get; set; } = "";

        [Required,MaxLength(100)]
        public string Genre { get; set; } = "";

       
        public IFormFile? ImageFile { get; set; }

    }
}
