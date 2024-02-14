using System.Data.Entity.ModelConfiguration;
using Jazb.DomainClasses.Entities;

namespace Jazb.DomainClasses.EntityConfiguration
{
    public class UserMetaDataConfig : EntityTypeConfiguration<UserMetaData>
    {
        public UserMetaDataConfig()
        {
            Property(x => x.Description).HasMaxLength(1000).IsOptional();
            Property(x => x.FirstName).HasMaxLength(50).IsOptional();
            Property(x => x.LastName).HasMaxLength(50).IsOptional();
            Property(x => x.Major).HasMaxLength(30).IsOptional();
            Property(x => x.AvatarPath).HasMaxLength(200);
        }
    }
}