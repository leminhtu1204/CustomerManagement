using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataAccess.Entity;

namespace DataAccess.Mapping
{
    public class CameraMapping : EntityTypeConfiguration<Camera>
    {
        public CameraMapping()
        {
            ToTable("Cameras");
            HasKey(t => t.Id);
            //fields 
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Description);
            Property(t => t.Url);
 
        }
    }
}