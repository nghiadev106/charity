using NHibernate.Linq;
using ProjectFinal.Common;
using ProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace ProjectFinal.Repositories
{
    public interface IServices
    {
        IEnumerable<Service> GetListServicePaging(int page, int pageSize, out int totalRow);
        IEnumerable<Service> Search(string keyword, int page, int pageSize, out int totalRow);
        List<Service> GetListService();
        Service GetServiceDetail(int Id);
        Service Add(ServiceModel serviceModel);
        void Update(ServiceModel serviceModel);
        Service Delete(int id);
    }
    public class Services : IServices
    {
        ProjectFinalEntities db = new ProjectFinalEntities();

        public IEnumerable<Service> GetListServicePaging(int page, int pageSize, out int totalRow)
        {
            var query = db.Service.Where(x => x.Status == 1).ToList();
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Service> Search(string keyword,int page, int pageSize, out int totalRow)
        {
            //var query = db.Service.Where(x => x.Status == 1 && x.Name.ToUpper().Contains(keyword.ToUpper())).ToList();
            string queryString = string.Format("SELECT * FROM Service WHERE dbo.fuConvertToUnsign1(Name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", keyword);
            var query = db.Service.SqlQuery(queryString).ToList();
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }


        private string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }
        public Service Add(ServiceModel serviceModel)
        {
            var Serviceervice = new Service();           
            Serviceervice.UpdateService(serviceModel);
            Serviceervice.CreateDate = DateTime.Now;
            db.Service.Add(Serviceervice);
            db.SaveChanges();
            return Serviceervice;
        }

        public Service Delete(int id)
        {
            var service = db.Service.Find(id);
            db.Service.Remove(service);
            db.SaveChanges();

            return service;
        }

        public List<Service> GetListService()
        {
            var lst = db.Service.OrderByDescending(y=>y.CreateDate).ToList();
            return lst;
        }

        public Service GetServiceDetail(int Id)
        {
            var lst = db.Service.SingleOrDefault(x=>x.Id == Id);
            return lst;
        }

        public void Update(ServiceModel serviceModel)
        {
            var oldService = db.Service.Find(serviceModel.Id);           
            serviceModel.CreateDate = oldService.CreateDate;
            oldService.UpdateService(serviceModel);
            db.Service.Attach(oldService);
            db.Entry(oldService).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}