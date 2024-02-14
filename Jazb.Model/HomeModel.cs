using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jazb.DomainClasses.Entities;
using PagedList;

namespace Jazb.Model
{
    public class HomeModel
    {
        public IPagedList<article> ArticlesList { get; set; }
        public IPagedList<article> AnnouncementList { get; set; }
        public IList<Azmoon> Azmoons { get; set; }

    }
}
