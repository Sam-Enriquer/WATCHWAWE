using System.ComponentModel.DataAnnotations;

namespace WATCHWAWE.Models
{
    public class Upcoming
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = "";

        [MaxLength(100)]
        public string Genre { get; set; } = "";

        [MaxLength(100)]
        public string ImageFileName { get; set; } = "";

        public DateTime CreatedAT { get; set; }


    }
}
