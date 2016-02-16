using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CustomerManager.Model;
using DataAccess.Entity;

namespace CustomerManager.Mapping
{
    public class CustomerMapping : EntityTypeConfiguration<Customer>
    {
        public CustomerMapping()
        {
            ToTable("Customers");

            //key
            HasKey(t => t.Id);

            //fields 
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.FirstName);
            Property(t => t.LastName);
            Property(t => t.Address);
            Property(t => t.Gender);
            Property(t => t.City);
            Property(t => t.Email);
            Property(t => t.UserName);
            Property(t => t.Password);
            Property(t => t.Zip);
            Property(t => t.IsAdmin);
            Property(t => t.StateId);

            //relationship  
            HasMany(t => t.Cameras).WithMany(c => c.Customers)
                                 .Map(t => t.ToTable("CustomerCameras")
                                     .MapLeftKey("CustomerId")
                                     .MapRightKey("CameraId"));  
 
        }
    }
}