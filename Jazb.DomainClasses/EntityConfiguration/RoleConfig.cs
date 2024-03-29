﻿using System.Data.Entity.ModelConfiguration;
using Jazb.DomainClasses.Entities;

namespace Jazb.DomainClasses.EntityConfiguration
{
    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            Property(x => x.Name).HasMaxLength(20);
            Property(x => x.Description).HasMaxLength(400);
        }
    }
}