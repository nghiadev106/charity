using ProjectFinal.Models;

namespace ProjectFinal.Common
{
    public static class EntityExtensions
    {
        public static void UpdateService(this Service service, ServiceModel serviceModel)
        {
            service.Id = serviceModel.Id;
            service.Name = serviceModel.Name;
            service.Money = serviceModel.Money;
            service.Descripttion = serviceModel.Descripttion;
            service.CreateDate = serviceModel.CreateDate;
            service.ToDate = serviceModel.ToDate;
            service.FromDate = serviceModel.FromDate;
            service.Status = serviceModel.Status;
            service.Image = serviceModel.Image;
        }

        public static void UpdateNewCategory(this NewCategory newCategory, NewCategoryModel newCategoryModel)
        {
            newCategory.Id = newCategoryModel.Id;
            newCategory.Name = newCategoryModel.Name;
            newCategory.Description = newCategoryModel.Description;
            newCategory.Status = newCategoryModel.Status;
        }

        public static void UpdateConfig(this Config config, ConfigModel configModel)
        {
            config.Id = configModel.Id;
            config.Phone = configModel.Phone;
            config.Description = configModel.Description;
            config.Email = configModel.Email;
            config.Facebook = configModel.Facebook;
            config.vnp_HashSecret = configModel.vnp_HashSecret;
            config.vnp_Returnurl = configModel.vnp_Returnurl;
            config.vnp_TmnCode = configModel.vnp_TmnCode;
            config.vnp_Url = configModel.vnp_Url;
        }

        public static void UpdateNews(this News news, NewsModel newsModel)
        {
            news.Title = newsModel.Title;
            news.Description = newsModel.Description;
            news.Detail = newsModel.Detail;
            news.Type = newsModel.Type;
            news.NewCategoryId = newsModel.NewCategoryId;
            news.Status = newsModel.Status;
            news.Image = newsModel.Image;
        }

        public static void UpdateVideo(this Video video, VideoModel videoModel)
        {
            video.Name = videoModel.Name;
            video.Description = videoModel.Description;
            video.Link = videoModel.Link;
            video.Status = videoModel.Status;
        }
    }
}