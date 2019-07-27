namespace Solver.DataAccessLayer
{
     using Microsoft.EntityFrameworkCore;
    using Solver.Entities.Models;

    public class ApplicationDataContext : DbContext
    {


        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options)
            : base(options)
        {
        }
        public virtual DbSet<WorkingDays> WorkingDays { get; set; }
        public virtual DbSet<Elements> Elements{ get; set; }
        public virtual DbSet<TrackLog> TrackLogs{ get; set; }
        public virtual DbSet<TrackLogDetail> TrackLogDetails{ get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Account>().HasIndex(b => b.UserAssign).ForSqlServerIsClustered(true);
            //modelBuilder.Entity<Account>().HasIndex(b => b.Guid).IsUnique().ForSqlServerIsClustered(false);
        }
    }
}