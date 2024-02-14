using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using PagedList;
using Jazb.Servicelayer.EFServices.Enums;
using Jazb.Model.AdminModel;
using Jazb.Servicelayer.Interfaces;

namespace Jazb.Web.Areas.Admin.Controllers
{

        [Authorize(Roles = "admin,moderator")]
    public partial class articlesController : Controller
    {
        private JazbDbContext context = new JazbDbContext();

        private readonly IUnitOfWork _uow;
        private readonly IArticleService _articleService;
        
        public articlesController(IUnitOfWork uow, IArticleService articleService)
        {
            _uow = uow;
            _articleService = articleService;
        }
        //
        // GET: /articles/
        [Authorize]
        public virtual ActionResult Index(int AzmoonId)
        {
            ViewBag.AzmoonId = AzmoonId;

            return PartialView();
        }

        public virtual ActionResult DataTable(int azmoonid, string term = "", int page = 0, int count = 10,
      Order order = Order.Descending, ArticleOrderBy orderBy = ArticleOrderBy.Date,
      ArticleSearchBy searchBy = ArticleSearchBy.Title)
        {
            ViewBag.AzmoonId = azmoonid;
            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.ORDER = order;
            ViewBag.ORDERBY = orderBy;
            ViewBag.SEARCHBY = searchBy;

            IList<ArticleDataTableViewModel> selectAzmoon = _articleService.GetDataTable(azmoonid, term, page, count, order, orderBy, searchBy);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _articleService.Count : selectAzmoon.Count;

            return PartialView(MVC.Admin.articles.Views._DataTable, selectAzmoon);
        }

     

        public virtual ActionResult NavBar(int aid)
        {
            var az = context.Azmoons.Find(aid).Title;
            ViewBag.AzmoonName = az;
            return PartialView(MVC.Admin.articles.Views._Nav, aid.ToString());
        }


        //
        // GET: /articles/Details/5

        public virtual ViewResult Details(int id)
        {
            article article = context.articles.Single(x => x.Id == id);
            return View(article);
        }

        //
        // GET: /articles/Create
        [Authorize]
        public virtual ActionResult Create(string AzmoonId)
        {
            article ar = new article();        
            ar.AzmoonId = int.Parse(AzmoonId);
           // ViewBag.Possiblecategories = context.categories;
            return PartialView(ar);
        }

        //
        // POST: /articles/Create


        protected byte[] getBytefile(HttpPostedFileBase file)
        {
            string fileName;
            string fileContentType;
            byte[] fileBytes;

            fileName = file.FileName;
            fileContentType = file.ContentType;
            fileBytes = new byte[file.ContentLength];
            file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

            return fileBytes;
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Create(article article)
        {
            //ViewBag.Possiblecategories = context.categories;
            var Picture1 = Request.Files["img_Picture"];
            bool bln = false,blnfile=false;
            if(Picture1!=null)
                if(Picture1.ContentLength>0)
                {
                    bln = true;
                    article.Picture = getBytefile(Picture1);
                }

            if(!bln)
            {
                ModelState.AddModelError("", "خطایی رخ داده");
                return PartialView(MVC.Admin.Shared.Views._Alert, new Alert { Message = "خطایی رخ داده", Mode = AlertMode.Error });
            }

            var fil_AttachmentFile = Request.Files["fil_AttachmentFile"];
            blnfile = false;
            if (fil_AttachmentFile != null)
                if (fil_AttachmentFile.ContentLength > 0)
                {
                    blnfile = true;
                    article.AttachmentFile = getBytefile(fil_AttachmentFile);
                    article.AttachmentFileName = fil_AttachmentFile.FileName;
                    article.AttachmentFiletype = fil_AttachmentFile.ContentType;
                }

            if (!blnfile)
            {
                ModelState.AddModelError("", "خطایی رخ داده");
                return PartialView(MVC.Admin.Shared.Views._Alert, new Alert { Message = "خطایی رخ داده", Mode = AlertMode.Error });
            }


            //  article.User = _usersContext.GetUser(User.Identity.Name);
            var qusers= context.Users.Where(x=> x.UserName.Equals(User.Identity.Name)).ToList();
            if (qusers.Count() > 0)
                article.UserId = qusers.First().Id;
            //var cq = from p in context.categories.ToList()
            //         where p.CategoryId.Equals(article.CategoryId)
            //         select p;

            if (article.ExpireDate != null)
                if (article.ExpireDate.Value.ToString() != string.Empty)
                {
                    article.ExpireDate = article.ExpireDate;
                }

            if (article.StartDate != null)
                if (article.StartDate.ToString() != string.Empty)
                {
                    article.StartDate = article.StartDate;
                }

            // article.Category = cq.First();
            if (ModelState.IsValid)
            {
                context.articles.Add(article);
               try
                {
                    context.SaveChanges();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "خطایی رخ داده");
                    return PartialView(MVC.Admin.Shared.Views._Alert, new Alert { Message = "خطایی رخ داده", Mode = AlertMode.Error });
                }
                
                ModelState.AddModelError("", "ثبت شد");
                return PartialView(MVC.Admin.Shared.Views._Alert, new Alert { Message = "اطلاعات خبر با موفقیت ثبت شد", Mode = AlertMode.Error });
            }

            ModelState.AddModelError("", "خطایی رخ داده");
            return PartialView(MVC.Admin.Shared.Views._Alert, new Alert { Message = "خطایی رخ داده", Mode = AlertMode.Error });
        }

        //
        // GET: /articles/Edit/5

        public virtual ActionResult Edit(int id)
        {
            ViewBag.Possiblecategories = context.categories;
            article article = context.articles.Single(x => x.Id == id);


            if (article.ExpireDate != null)
            {
                article.ExpireDate = null;
            }
            return View(article);
        }

        //
        // POST: /articles/Edit/5

        [Authorize]
        [HttpPost]
        public virtual ActionResult Edit(article article)
        {
            bool bln = false, blnfile = false;
            ViewBag.Possiblecategories = context.categories;
            var Picture1 = Request.Files["img_Picture"];
            if (Picture1 != null)
                if (Picture1.ContentLength != 0)
                {
                    bln = true;
                    article.Picture = getBytefile(Picture1);
                }


            var fil_AttachmentFile = Request.Files["fil_AttachmentFile"];
            if (fil_AttachmentFile != null)
                if (fil_AttachmentFile.ContentLength != 0)
                {
                    blnfile = true;
                    article.AttachmentFile = getBytefile(fil_AttachmentFile);
                    article.AttachmentFileName = fil_AttachmentFile.FileName;
                    article.AttachmentFiletype = fil_AttachmentFile.ContentType;
                }
            //  article.User = _usersContext.GetUser(User.Identity.Name);
            var qusers = context.Users.Where(x => x.UserName.Equals(User.Identity.Name)).ToList();
            if (qusers.Count() > 0)
                article.UserId = qusers.First().Id;
            //var cq = from p in context.categories.ToList()
            //         where p.CategoryId.Equals(article.CategoryId)
            //         select p;

            if (article.ExpireDate != null)
                if (article.ExpireDate.Value.ToString() != string.Empty)
                {
                    article.ExpireDate = article.ExpireDate;
                }

            if (ModelState.IsValid)
            {
                context.Entry(article).State = EntityState.Modified;
               
                context.Entry(article).Property("StartDate").IsModified = false;

                if (bln == false)
                    context.Entry(article).Property(x => x.Picture).IsModified = false;

                if (blnfile==false)
                {
                    context.Entry(article).Property(x => x.AttachmentFile).IsModified = false;
                    context.Entry(article).Property(x => x.AttachmentFileName).IsModified = false;
                    context.Entry(article).Property(x => x.AttachmentFiletype).IsModified = false;
                }
                    

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        //
        // GET: /articles/Delete/5
        [Authorize]
        public virtual ActionResult Delete(int id)
        {
            article article = context.articles.Single(x => x.Id == id);
            return View(article);
        }

        //
        // POST: /articles/Delete/5

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            article article = context.articles.Single(x => x.Id == id);
            context.articles.Remove(article);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
