using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dojoactivitycenter.Models
{
    public class RegisterUserModel
    {

        [Required (ErrorMessage="First Name is required")]
        [MinLength(2)]
        [Display(Name= "First Name")]
        public string FirstName { get; set; }
        
        
        [Required (ErrorMessage="Last Name is required")]
        [MinLength(2)]
        [Display(Name= "Last Name")]
        public string LastName { get; set; }
        
        
        [Required (ErrorMessage="Email is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name= "Email")]
        public string Email { get; set; }
        
        
        [Required (ErrorMessage="Password is required")]
        [MinLength(8)]
        [Display(Name= "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name= "Confirm Password")]
        public string C_Password {get; set;}
    }
}