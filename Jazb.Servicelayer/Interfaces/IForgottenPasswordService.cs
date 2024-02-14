using System;
using Jazb.DomainClasses.Entities;

namespace Jazb.Servicelayer.Interfaces
{
    public interface IForgottenPasswordService
    {
        void Add(ForgottenPassword model);
        DateTime RequestDate(string key);
        User FindUser(string key);
        void Remove(string key);
    }
}