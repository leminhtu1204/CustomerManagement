using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DataAccess.Entity;

namespace DataAccess.Mapping
{
    public class StateMapping : EntityTypeConfiguration<State>
    {
        public StateMapping()
        {
            ToTable("Cameras");
            HasKey(t => t.Id);
            //fields 
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Abbreviation);
            Property(t => t.Name);
 
        }
    }
}