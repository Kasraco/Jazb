﻿using System;

namespace Jazb.DomainClasses.Entities
{
    public class ForgottenPassword
    {
        public virtual int Id { get; set; }
        public virtual string Key { get; set; }
        public virtual DateTime ResetDateTime { get; set; }
        public virtual User User { get; set; }
    }
}