using System.Data.Entity.ModelConfiguration.Conventions;
using CustomerManager.Model;
using System.Data.Entity;

namespace CustomerManager.Repository
{
    public class UserManagerContext : DbContext
    {
        public UserManagerContext()
            : base("UserManagerContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        //// DEVELOPMENT ONLY: initialize the database
        static UserManagerContext()
        {
            Database.SetInitializer(new CustomerManagerDatabaseInitializer());
        }

        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<AssignedCamera> AssignedCameras { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}