using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTRS.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string BusNumber { get; set; }

        public ICollection<Passengers_Trip> passengers_Trips { get; set; }

        [ForeignKey("adminID")]
        public Admin admin { get; set; }

        public ICollection <Bus> buses { get; set; }
    }
}
