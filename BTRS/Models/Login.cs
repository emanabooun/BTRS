using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    public class Login
    {
        [Required(ErrorMessage = "please fill data")]
        public string username { get; set; }
        [Required(ErrorMessage = "please fill data")]
        public string Password { get; set; }
    }
}
