using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dojoactivitycenter.Models
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }


        public string LastName { get; set; }

        public string Email{ get; set; }

        public string Password { get; set; }
        
        public List<Activity> myPlannedActivities { get; set; }
        
        public List<UserActivity> Events { get; set; }
        
    }
}
