using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BTRS.Models
{
    public class Passengers_Trip
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("FK_Passengers")]
        public Passengers passengers { get; set; }
        public int passengersID { get; set; }

        [ForeignKey("TripID")]
        public Trip trip { get; set; }
        
    }
}
