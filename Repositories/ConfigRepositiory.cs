using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ProjectFinal.Common;
using ProjectFinal.Models;

namespace ProjectFinal.Repositories
{
    public interface IConfigRepositiory
    {
        void Update(ConfigModel configModel);
    }
    public class ConfigRepositiory : IConfigRepositiory
    {
        ProjectFinalEntities db = new ProjectFinalEntities();

        public void Update(ConfigModel configModel)
        {
            var model = db.Config.SingleOrDefault();
            model.UpdateConfig(configModel);
            db.Config.Attach(model);
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}