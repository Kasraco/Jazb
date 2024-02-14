using Jazb.Model.AdminModel;
using Jazb.Servicelayer.EFServices.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Servicelayer.Interfaces
{
    public interface IArticleService
    {
         IList<ArticleDataTableViewModel> GetDataTable(int azmoonid, string term, int page, int count,
                                                             Order order, ArticleOrderBy orderBy, ArticleSearchBy searchBy);

        int Count { get; }
    }
}
