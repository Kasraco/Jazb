using Jazb.DomainClasses.Entities;
using System.Collections.Generic;

namespace Jazb.Servicelayer.Interfaces
{
    public interface IAnonymousUser
    {
        void Add(AnonymousUser user);
    
        AnonymousUser GetUser(string name);
        AnonymousUser GetUser(int id);
        AnonymousUser GetUser(string name, string ip);
    }
}