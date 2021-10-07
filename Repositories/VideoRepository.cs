using ProjectFinal.Common;
using ProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ProjectFinal.Repositories
{
    public interface IVideoRepository
    {
        List<Video> GetListVideo();
        Video GetVideoDetail(int Id);
        Video Add(VideoModel videoModel);
        void Update(VideoModel videoModel);
        Video Delete(int id);
        IEnumerable<Video> GetListVideoPaging(int page, int pageSize, out int totalRow);
        IEnumerable<Video> SearchVieo(string keyword, int page, int pageSize, out int totalRow);
    }
    public class VideoRepository : IVideoRepository
    {
        ProjectFinalEntities db = new ProjectFinalEntities();
        public IEnumerable<Video> GetListVideoPaging(int page, int pageSize, out int totalRow)
        {
            var query = db.Video.Where(x => x.Status == 1).ToList();
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Video> SearchVieo(string keyword, int page, int pageSize, out int totalRow)
        {
            //var query = db.Service.Where(x => x.Status == 1 && x.Name.ToUpper().Contains(keyword.ToUpper())).ToList();
            string queryString = string.Format("SELECT * FROM Video WHERE dbo.fuConvertToUnsign1(Name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", keyword);
            var query = db.Video.SqlQuery(queryString).ToList();
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public Video Add(VideoModel videoModel)
        {
            var newVideo = new Video();
            newVideo.UpdateVideo(videoModel);
            db.Video.Add(newVideo);
            db.SaveChanges();
            return newVideo;
        }

        public Video Delete(int id)
        {
            var video = db.Video.Find(id);
            db.Video.Remove(video);
            db.SaveChanges();

            return video;
        }

        public List<Video> GetListVideo()
        {
            var lst = db.Video.ToList();
            return lst;
        }

        public Video GetVideoDetail(int Id)
        {
            var lst = db.Video.SingleOrDefault(x => x.Id == Id);
            return lst;
        }

        public void Update(VideoModel videoModel)
        {
            var oldVideo = db.Video.Find(videoModel.Id);
            oldVideo.UpdateVideo(videoModel);
            db.Video.Attach(oldVideo);
            db.Entry(oldVideo).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}