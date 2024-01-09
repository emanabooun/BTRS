using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    public class Passengers
    {
        [Key]
        public int PassengerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Gender { get; set; }

        
    }
}
