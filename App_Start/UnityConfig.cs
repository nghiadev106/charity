using ProjectFinal.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace ProjectFinal
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IServices, Services>();
            container.RegisterType<IPayment, Payment>();
            container.RegisterType<IAccount, Account>();
            container.RegisterType<INewCategoryRepository, NewCategoryRepository>();
            container.RegisterType<INewsRepository, NewsRepository>();
            container.RegisterType<IVideoRepository, VideoRepository>();
            container.RegisterType<IConfigRepositiory, ConfigRepositiory>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}