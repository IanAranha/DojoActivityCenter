using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dojoactivitycenter.Models
{
    public class UserActivity
    {
        [Key]
        public int UserActivitiesId { get; set; }
        [ForeignKey("ActivityId")]
        public int ActivityId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User Participant { get; set; }
        public Activity Events { get; set; }

    }
}