using System.Data.Entity.ModelConfiguration;
using Jazb.DomainClasses.Entities;

namespace Jazb.DomainClasses.EntityConfiguration
{
    public class ForgottenPasswordConfig : EntityTypeConfiguration<ForgottenPassword>
    {
        public ForgottenPasswordConfig()
        {
            Property(x => x.Key).HasMaxLength(40);
        }
    }
}