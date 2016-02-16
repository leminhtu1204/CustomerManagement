using System.Data.Entity;
using CustomerManager.Model;
using DataAccess.Entity;
using DataAccess.Mapping;

namespace DataAccess
{
    public class CustomerManagerContext : DbContext
    {
        public CustomerManagerContext()
        {
            //Configuration.ProxyCreationEnabled = false;
        }
        // DEVELOPMENT ONLY: initialize the database
        static CustomerManagerContext()
        {
            Database.SetInitializer(new CustomerManagerDatabaseInitializer());
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Camera> Cameras { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMapping());
            modelBuilder.Configurations.Add(new CameraMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}