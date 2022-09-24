using Microsoft.AspNetCore.Mvc;
using URLShortener.Data;
using URLShortener.Models;

namespace URLShortener.Controllers
{
    public class UrlController : Controller
    {
        ApplicationContext _db;
        public UrlController(ApplicationContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var listUrls = _db.Urls;
            return View(listUrls);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UrlInfo url)
        {
            if (_db.Urls.FirstOrDefault(x => x.LongUrl == url.LongUrl) != null) // замена атрибуту Remote(он почему-то не сработал, попробую еще разобраться)
            {
                ModelState.AddModelError(nameof(url.LongUrl), "Такой Url уже зарегестрирован");
            }

            if (ModelState.IsValid)
            {                
                url.ShortUrl = Encoder.Encode(url.LongUrl);
                url.DateCreate = DateTime.Now;
                url.NumberOfClicks = 0;

                _db.Urls.Add(url);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(url);
        }
        
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                UrlInfo? url = _db.Urls.FirstOrDefault(x => x.Id == id);
                if (url != null)
                    return View(url);
            }
            return NotFound();            
        }

        [HttpPost]
        public IActionResult Edit(UrlInfo url)
        {
            if (_db.Urls.FirstOrDefault(x => x.LongUrl == url.LongUrl) != null)
            {
                ModelState.AddModelError(nameof(url.LongUrl), "Такой Url уже зарегестрирован");
            }

            if (ModelState.IsValid)
            {
                url.ShortUrl = Encoder.Encode(url.LongUrl);
                _db.Urls.Update(url);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(url);
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                UrlInfo? url = _db.Urls.FirstOrDefault(x => x.Id == id);
                if (url != null)
                {
                    _db.Urls.Remove(url);
                    _db.SaveChanges();                   
                }                    
            }
            return RedirectToAction("Index");
        }
                
        public IActionResult Increment(int? id)
        {
            if (id != null)
            {
                UrlInfo? url = _db.Urls.FirstOrDefault(x => x.Id == id);
                if (url != null)
                {
                    url.NumberOfClicks++;
                    _db.Urls.Update(url);
                    _db.SaveChanges();
                }                    
            }
            return RedirectToAction("Index");
        }
    }
}
