using ProjectFinal.Common;
using ProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectFinal.Repositories
{
    public interface INewCategoryRepository
    {
        List<NewCategory> GetListNewCategory();
        NewCategory GetNewCategoryDetail(int Id);
        NewCategory Add(NewCategoryModel newCategoryModel);
        void Update(NewCategoryModel newCategoryModel);
        NewCategory Delete(int id);
    }
    public class NewCategoryRepository : INewCategoryRepository
    {
        ProjectFinalEntities db = new ProjectFinalEntities();

        public NewCategory Add(NewCategoryModel newCategoryModel)
        {
            var newNewCategory = new NewCategory();
            newNewCategory.UpdateNewCategory(newCategoryModel);
            db.NewCategory.Add(newNewCategory);
            db.SaveChanges();
            return newNewCategory;
        }

        public NewCategory Delete(int id)
        {
            var newCategory = db.NewCategory.Find(id);
            db.NewCategory.Remove(newCategory);
            db.SaveChanges();

            return newCategory;
        }

        public List<NewCategory> GetListNewCategory()
        {
            var lst = db.NewCategory.ToList();
            return lst;
        }

        public NewCategory GetNewCategoryDetail(int Id)
        {
            var lst = db.NewCategory.SingleOrDefault(x => x.Id == Id);
            return lst;
        }

        public void Update(NewCategoryModel newCategoryModel)
        {
            var oldNewCategory = db.NewCategory.Find(newCategoryModel.Id);
            oldNewCategory.UpdateNewCategory(newCategoryModel);
            db.NewCategory.Attach(oldNewCategory);
            db.Entry(oldNewCategory).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}