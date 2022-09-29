using URLShortener.Models;

namespace URLShortener.Services
{
    public interface IUrlInfoService
    {
        void Create(UrlInfo urlInfo);
        void Update(UrlInfo urlInfo);
        void Delete(UrlInfo urlInfo);
        UrlInfo GetById(int? id);
        UrlInfo GetByShortUrl(string path);
        UrlInfo GetByLongUrl(string path);
        IEnumerable<UrlInfo> GetAll();
    }
}
