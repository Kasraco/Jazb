using System;
using System.Collections.Generic;

namespace Jazb.DomainClasses.Entities
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string IP { get; set; }
        public virtual bool IsBaned { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? BanedDate { get; set; }
        public virtual DateTime? LastLoginDate { get; set; }
        public virtual DateTime? LastPasswordChange { get; set; }
        public virtual UserMetaData UserMetaData { get; set; }
        public virtual Role Role { get; set; }      
        public virtual ICollection<MessageAnsware> MessageAnswares { get; set; }     
        public virtual ICollection<ForgottenPassword> ForgottenPasswords { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}