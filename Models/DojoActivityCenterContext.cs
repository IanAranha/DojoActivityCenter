using Microsoft.EntityFrameworkCore;
 
namespace dojoactivitycenter.Models
{
    public class DojoActivityCenterContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public DojoActivityCenterContext(DbContextOptions<DojoActivityCenterContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<UserActivity> UsersActivities { get; set; }


    }
}

    