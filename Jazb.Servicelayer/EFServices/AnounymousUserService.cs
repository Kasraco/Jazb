using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Servicelayer.Interfaces;

namespace Jazb.Servicelayer.EFServices
{
    public class AnounymousUserService : IAnonymousUser
    {
        private readonly IDbSet<AnonymousUser> _anonymousUser;
        private readonly IUnitOfWork _uow;

        public AnounymousUserService(IUnitOfWork uow)
        {
            _uow = uow;
            _anonymousUser = _uow.Set<AnonymousUser>();
        }

       

        public void Add(AnonymousUser user)
        {
            _anonymousUser.Add(user);
        }

        public AnonymousUser GetUser(string name)
        {
            return _anonymousUser.FirstOrDefault(user => user.Name.Equals(name));
        }

        public AnonymousUser GetUser(int id)
        {
            return _anonymousUser.Find(id);
        }

        public AnonymousUser GetUser(string name, string ip)
        {
            return _anonymousUser.FirstOrDefault(user => user.IP.Equals(ip) && user.Name.Equals(name));
        }
    }
}