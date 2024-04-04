using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WATCHWAWE.Models
{
    public class AppUser:IdentityUser
    {
        internal string? Address;

        [StringLength(100)]
        [MaxLength(100)]
        [Required]

        public string? Name { get; set; }



    }
}
