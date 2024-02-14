using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Model.AdminModel;
using Jazb.Servicelayer.EFServices.Enums;
using Jazb.Servicelayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jazb.Utilities.DateAndTime;

namespace Jazb.Servicelayer.EFServices
{
    public class ArticleService: IArticleService
    {
        private static int _searchTakeCount;
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<article> _article;

        public ArticleService(IUnitOfWork uow)
        {
            _uow = uow;
            _article = _uow.Set<article>();
            _searchTakeCount = 10;
        }

        public IList<ArticleDataTableViewModel> GetDataTable(int azmoonid,string term, int page, int count, Order order, ArticleOrderBy orderBy, ArticleSearchBy searchBy)
        {
            IQueryable<article> SelectArticles = _article.Include(x=>x.User)
                                                         .Where(x=>x.AzmoonId== azmoonid).AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case ArticleSearchBy.Title:
                        SelectArticles = SelectArticles.Where(x => x.Title.Contains(term)).AsQueryable();
                        break;
                  
                    case ArticleSearchBy.Body:
                        SelectArticles = SelectArticles.Where(x => x.Body.Contains(term)).AsQueryable();
                        break;
                    case ArticleSearchBy.Date:
                        {
                            DateTime dte = Convert.ToDateTime(term);
                            SelectArticles = SelectArticles.Where(x => x.StartDate>= dte).AsQueryable();
                            break;
                        }

                 
                }
            }

            if (order == Order.Asscending)
            {
                switch (orderBy)
                {
                
                    case ArticleOrderBy.Date:
                        SelectArticles = SelectArticles.OrderBy(x => x.StartDate).AsQueryable();
                        break;
                  
                
                    case ArticleOrderBy.Title:
                        SelectArticles = SelectArticles.OrderBy(x => x.Title).AsQueryable();
                        break;
                    case ArticleOrderBy.UserName:
                        SelectArticles = SelectArticles.OrderBy(x => x.User.UserName).AsQueryable();
                        break;


                }

            }
            else
            {
                switch (orderBy)
                {
                    case ArticleOrderBy.Date:
                        SelectArticles = SelectArticles.OrderByDescending(x => x.StartDate).AsQueryable();
                        break;
                 

                    case ArticleOrderBy.Title:
                        SelectArticles = SelectArticles.OrderByDescending(x => x.Title).AsQueryable();
                        break;
                    case ArticleOrderBy.UserName:
                        SelectArticles = SelectArticles.OrderByDescending(x => x.User.UserName).AsQueryable();
                        break;
                }
            }


            return SelectArticles.Skip(page * count).Take(count).Select(x =>
              new ArticleDataTableViewModel
              {
                 AzmoonId=x.AzmoonId,
                 Body=x.Body,              
                 ExpireDate=x.ExpireDate==null?DateTime.Now:x.ExpireDate.Value,
                 Id=x.Id,
                 MinBody=x.MinBody,
                 Picture=x.Picture,
                 SourceTitle=x.SourceTitle,
                 SourceUrl=x.SourceUrl,
                 StartDate=x.StartDate,
                 Title=x.Title,
                 UserName=x.User.UserName
              }).ToList();

        }

        public int Count
        {
            get { return _article.Count(); }
        }

    }
}
