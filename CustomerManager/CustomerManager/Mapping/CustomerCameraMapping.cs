using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CustomerManager.Model;
using DataAccess.Entity;

namespace CustomerManager.Mapping
{
    public class CustomerCameraMapping : EntityTypeConfiguration<CustomerCamera>
    {
        public CustomerCameraMapping()
        {
            ToTable("CustomerCameras");

            //fields 
            //Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CameraId);
            Property(t => t.CustomerId);
 
        }
    }
}