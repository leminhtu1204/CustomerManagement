using System.Data.Entity.ModelConfiguration;
using DataAccess.Entity;

namespace DataAccess.Mapping
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