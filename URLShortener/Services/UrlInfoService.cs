using Microsoft.AspNetCore.Mvc;
using System;
using URLShortener.Data;
using URLShortener.Models;

namespace URLShortener.Services
{
    public class UrlInfoService : IUrlInfoService
    {
        ApplicationContext _db;
        public UrlInfoService(ApplicationContext db)
        {
            _db = db;
        }

        public void Create(UrlInfo urlInfo)
        {
            _db.Urls.Add(urlInfo);
            _db.SaveChanges();
        }

        public void Update(UrlInfo urlInfo)
        {
            _db.Urls.Update(urlInfo);
            _db.SaveChanges();
        }

        public void Delete(UrlInfo urlInfo)
        {
            _db.Urls.Remove(urlInfo);
            _db.SaveChanges();
        }

        public UrlInfo GetById(int? id)
        {
            var urlInfo = _db.Urls.FirstOrDefault(x => x.Id == id);

            if (urlInfo != null)
            {
                return urlInfo;
            }
            else
            {
                return null;
            }
        }

        public UrlInfo GetByLongUrl(string path)
        {
            var urlInfo = _db.Urls.FirstOrDefault(x => x.LongUrl == path);

            if (urlInfo != null)
            {
                return urlInfo;
            }
            else
            {
                return null;
            }            
        }

        public UrlInfo GetByShortUrl(string path)
        {
            var urlInfo = _db.Urls.FirstOrDefault(x => x.ShortUrl == path);

            if (urlInfo != null)
            {
                return urlInfo;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<UrlInfo> GetAll()
        {
            return _db.Urls;
        }
    }
}
