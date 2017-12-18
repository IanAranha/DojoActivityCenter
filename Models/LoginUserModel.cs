using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dojoactivitycenter.Models
{
    public class LoginUserModel
    {


        [Required(ErrorMessage="Email is required")]
        [DataType(DataType.EmailAddress)]
        public string LoginEmail { get; set; }
        
        [Required(ErrorMessage="Password is required")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }
    }
}