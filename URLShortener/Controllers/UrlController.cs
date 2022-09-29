using Microsoft.AspNetCore.Mvc;
using URLShortener.Data;
using URLShortener.Models;
using URLShortener.Services;

namespace URLShortener.Controllers
{
    public class UrlController : Controller
    {
        IUrlInfoService _urlInfoService;

        public UrlController(IUrlInfoService urlInfoService)
        {
            _urlInfoService = urlInfoService;
        }

        public IActionResult Index()
        {
            var listUrls = _urlInfoService.GetAll();
            return View(listUrls);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UrlInfo url)
        {
            if (_urlInfoService.GetByLongUrl(url.LongUrl) != null) // замена атрибуту Remote(он почему-то не сработал, попробую еще разобраться)
            {
                ModelState.AddModelError(nameof(UrlInfo.LongUrl), "Такой Url уже зарегестрирован");
            }            

            if (ModelState.IsValid)
            {
                url.ShortUrl = Request.Host + "/" + Encoder.Encode(url.LongUrl);
                url.DateCreate = DateTime.Now;
                url.NumberOfClicks = 0;

                _urlInfoService.Create(url);

                return RedirectToAction("Index");
            }
            return View(url);
        }
        
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                UrlInfo? url = _urlInfoService.GetById(id);
                if (url != null)
                    return View(url);
            }
            return NotFound();            
        }

        [HttpPost]
        public IActionResult Edit(UrlInfo url)
        {
            if (_urlInfoService.GetByLongUrl(url.LongUrl) != null)
            {
                ModelState.AddModelError(nameof(UrlInfo.LongUrl), "Такой Url уже зарегестрирован");
            }

            if (ModelState.IsValid)
            {
                url.ShortUrl = Request.Host + "/" + Encoder.Encode(url.LongUrl);
                _urlInfoService.Update(url);

                return RedirectToAction("Index");
            }
            return View(url);
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                UrlInfo? url = _urlInfoService.GetById(id);
                if (url != null)
                {
                    _urlInfoService.Delete(url);
                }                    
            }
            return RedirectToAction("Index");
        }

        [HttpGet("/{path:required}")]
        public IActionResult RedirectTo(string path)
        {
            if (path == null)
            {
                return NotFound();
            }

            string shUrl = Request.Host + "/" + path;
            var url = _urlInfoService.GetByShortUrl(shUrl);

            if (url == null)
            {
                return NotFound();
            }
            else
            {
                url.NumberOfClicks++;
                _urlInfoService.Update(url);
                return Redirect(url.LongUrl);
            }
        }

        // Переход по ссылке со страницы Index
        public IActionResult LinkRedirectTo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = _urlInfoService.GetById(id);
            if (url == null)
            {
                return NotFound();
            }
            else
            {
                url.NumberOfClicks++;
                _urlInfoService.Update(url);
                return Redirect(url.LongUrl);
            }
        }

    }
}
