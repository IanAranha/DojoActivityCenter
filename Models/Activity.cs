using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dojoactivitycenter.Models
{
    [Table("Activities")]
    public class Activity : BaseEntity
    {
        [Key]
        public int ActivityId { get; set; }

        
        [Required(ErrorMessage="Title is mandatory")]
        [MinLength(2)]
        [Display(Name="Title")]
        public string ActivityTitle { get; set; }


        [Required(ErrorMessage="Time is mandatory")]
        [DataType(DataType.Time)]
        [Display(Name="Time")]
        public DateTime  ActivityTime { get; set; }


        [Required(ErrorMessage="Date is mandatory")]
        [RestrictedDate]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{):dd-MM-yyyy}", ApplyFormatInEditMode=true)]
        [Display(Name="Date")]
        public DateTime  ActivityDate { get; set; }

        
        [Required(ErrorMessage="Durations is mandatory")]
        [DataType(DataType.Duration)]
        [Display(Name="Duration")]
        public int  ActivityDuration { get; set; }


        public string Duration { get; set; }


        [Required(ErrorMessage="Description is mandatory")]
        [MinLength(10)]
        [Display(Name="Description")]
        public string ActivityDescription { get; set; }

        
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public User Planner { get; set; }

        public List<UserActivity> Participants { get; set; }

        
        internal class RestrictedDate : ValidationAttribute
        {
        public override bool IsValid(object date)
        {
            DateTime formdate = (DateTime)date;
            return formdate > DateTime.Now;
        }
        }
        
    }
}

