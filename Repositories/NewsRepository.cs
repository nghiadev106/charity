using ProjectFinal.Common;
using ProjectFinal.Models;
using ProjectFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ProjectFinal.Repositories
{
    public interface INewsRepository
    {
        List<NewsViewModel> GetListNews();
        News GetNewsDetail(int Id);
        News Add(NewsModel newsModel);
        void Update(NewsModel newsModel);
        News Delete(int id);

        List<News> GetListNewsHome();

        IEnumerable<News> GetListNewsPaging(int page, int pageSize, out int totalRow);
        IEnumerable<News> GetListNewsByCategoryPaging(int id, int page, int pageSize, out int totalRow);
        IEnumerable<News> SearchNews(string keyword, int page, int pageSize, out int totalRow);
    }
    public class NewsRepository : INewsRepository
    {
        ProjectFinalEntities db = new ProjectFinalEntities();

        public IEnumerable<News> GetListNewsPaging(int page, int pageSize, out int totalRow)
        {
            var query = db.News.Where(x => x.Status == 1).ToList();
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<News> GetListNewsByCategoryPaging(int id,int page, int pageSize, out int totalRow)
        {
            var query = db.News.Where(x => x.Status==1 && x.NewCategoryId == id).ToList();
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
        public IEnumerable<News> SearchNews(string keyword, int page, int pageSize, out int totalRow)
        {
            //var query = db.Service.Where(x => x.Status == 1 && x.Name.ToUpper().Contains(keyword.ToUpper())).ToList();
            string queryString = string.Format("SELECT * FROM News WHERE dbo.fuConvertToUnsign1(Title) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", keyword);
            var query = db.News.SqlQuery(queryString).ToList();
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public News Add(NewsModel newsModel)
        {
            var newNews = new News();
            newNews.CreateDate = DateTime.Now;
            newNews.UpdateNews(newsModel);
            db.News.Add(newNews);
            db.SaveChanges();
            return newNews;
        }

        public News Delete(int id)
        {
            var news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();

            return news;
        }

        public List<NewsViewModel> GetListNews()
        {
            var lst = db.News.OrderBy(y => y.CreateDate).ToList();
            var query = from n in db.News
                        join nc in db.NewCategory on n.NewCategoryId equals nc.Id
                        select new NewsViewModel()
                        {
                            Id = n.Id,
                            Title = n.Title,
                            Description = n.Description,
                            Detail = n.Detail,
                            NewCategoryName = nc.Name,
                            Type=n.Type,
                            CreateDate=n.CreateDate,
                            Status = n.Status,
                            Image=n.Image
                        };
            return query.OrderBy(x => x.Id).ToList();
        }

        public List<News> GetListNewsHome()
        {
            var lst = db.News.OrderByDescending(x=>x.CreateDate).Where(x=>x.Status==1).ToList();
            return lst;
        }

        public News GetNewsDetail(int Id)
        {
            var lst = db.News.SingleOrDefault(x => x.Id == Id);
            return lst;
        }

        public void Update(NewsModel newsModel)
        {
            var oldNews = db.News.Find(newsModel.Id);
            oldNews.LastEditdate = DateTime.Now;
            oldNews.UpdateNews(newsModel);
            db.News.Attach(oldNews);
            db.Entry(oldNews).State = EntityState.Modified;
            db.Entry(oldNews).Property(x => x.CreateDate).IsModified = false;
            db.SaveChanges();
        }
    }
}