using System.Collections.Generic;
using Jazb.DomainClasses.Entities;

namespace Jazb.Servicelayer.Interfaces
{
    public interface IMessageService
    {
        void Add(FAQ model);
        void Edit(FAQ model);
        IList<FAQ> GetAll();
        IList<FAQ> GetAll(int AzmoonId);
        IList<FAQ> GetAllNonAnswer(int AzmoonId);
        IList<FAQ> GetAllAnswer(int AzmoonId);
        FAQ Find(int id);

    }
}