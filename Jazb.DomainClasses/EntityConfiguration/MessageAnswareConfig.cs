using System.Data.Entity.ModelConfiguration;
using Jazb.DomainClasses.Entities;

namespace Jazb.DomainClasses.EntityConfiguration
{
    public class MessageAnswareConfig : EntityTypeConfiguration<MessageAnsware>
    {
        public MessageAnswareConfig()
        {
            Property(x => x.Body).IsMaxLength();
        }
    }
}