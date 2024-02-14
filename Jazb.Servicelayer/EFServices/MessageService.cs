using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Servicelayer.Interfaces;

namespace Jazb.Servicelayer.EFServices
{
    public class MessageService : IMessageService
    {
        private readonly IDbSet<FAQ> _messages;
        private readonly IUnitOfWork _uow;

        public MessageService(IUnitOfWork uow)
        {
            _uow = uow;
            _messages = _uow.Set<FAQ>();
        }

        public void Add(FAQ model)
        {
            _messages.Add(model);
        }


        public void Edit(FAQ model)
        {
            var q = Find(model.Id);
            q.BodyAnswer = model.BodyAnswer;
           
        }


        public IList<FAQ> GetAll()
        {
            return _messages.OrderByDescending(m => m.QuestionDate).ToList();
        }

        public FAQ Find(int id)
        {
            return _messages.Find(id);
        }


        public IList<FAQ> GetAllNonAnswer(int AzmoonId)
        {
            return _messages.Where(x => x.AzmoonId == AzmoonId && string.IsNullOrEmpty(x.BodyAnswer)).OrderByDescending(m => m.QuestionDate).ToList();
        }

        public IList<FAQ> GetAllAnswer(int AzmoonId)
        {
            return _messages.Where(x => x.AzmoonId == AzmoonId && !string.IsNullOrEmpty(x.BodyAnswer)).OrderByDescending(m => m.QuestionDate).ToList();
        }





        public IList<FAQ> GetAll(int AzmoonId)
        {
            return _messages.Where(x => x.AzmoonId == AzmoonId).OrderByDescending(m => m.QuestionDate).ToList();
        }
    }
}