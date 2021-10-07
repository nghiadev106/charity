using AutoMapper;
using ProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFinal.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Service, ServiceModel>();
                cfg.CreateMap<Order, OrderModel>();
                cfg.CreateMap<NewCategory, NewCategoryModel>();
                cfg.CreateMap<News, NewsModel>();
                cfg.CreateMap<Video, VideoModel>();
                cfg.CreateMap<Config, ConfigModel>();
                cfg.CreateMap<User, AccountUpdateModel>();
            });
        }
    }
}