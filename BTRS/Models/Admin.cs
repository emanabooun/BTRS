using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BTRS.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }

        public ICollection<Bus> buses { get; set; }
        public ICollection<Trip> trips { get; set; }
    }
}
