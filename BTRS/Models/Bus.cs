using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTRS.Models
{
    public class Bus
    {
        [Key]
        public int BusId { get; set; }
        [Required]
        public string CaptainName { get; set; }
        [Required]
        public int NumberOfSeats { get; set; }

        [ForeignKey("adminID")]
        public Admin admin { get; set; }

    }
}
