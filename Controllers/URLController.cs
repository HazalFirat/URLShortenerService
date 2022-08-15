using System;
using Microsoft.AspNetCore.Mvc;
using URLShortenerService.Models;
using URLShortenerService.Services;

namespace URLShortenerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class URLController : ControllerBase
    {
        private readonly IURLShortenerService _service;

        public URLController(IURLShortenerService service)
        {
            _service = service;
        }

        [HttpPost("CreateShortUrl")]
        public IActionResult CreateShortUrl(string originalURL)
        {
            //Check input URL validity
            Uri uriResult;
            bool validURL = Uri.TryCreate(originalURL, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!validURL)
            {
                return BadRequest("Input is not a valid URL");
            }

            var url = new URL
            {
                OriginalURL = originalURL,
                ShortURL = "",
                DateCreated = DateTime.Now
            };

            _service.saveOriginalURL(url);
            var hash = _service.createShortURLHash(url);
            url.ShortURL = new Uri(Request.Scheme + "://" + Request.Host.Value + Request.PathBase + "/" + hash).ToString();
            _service.updateShortURL(url);

            return Ok(url.ShortURL);
        }

        [HttpGet("GetOriginalUrlByShortUrl")]
        public IActionResult GetOriginalUrlByShortUrl(string shortURL)
        {
            //Check input URL validity
            Uri uriResult;
            bool validURL = Uri.TryCreate(shortURL, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!validURL)
            {
                return BadRequest("Input is not a valid URL");
            }

            var url = new URL();

            url = _service.getByShortURL(shortURL);

            if (url != null)
            {
                return Ok(url.OriginalURL);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("CreateCustomShortUrl")]
        public IActionResult CreateCustomShortUrl(string originalURL, string customShortURL)
        {
            //Check input URL validity
            Uri uriResultOriginal, uriResultShort;
            bool validOriginalURL = Uri.TryCreate(originalURL, UriKind.Absolute, out uriResultOriginal)
                && (uriResultOriginal.Scheme == Uri.UriSchemeHttp || uriResultOriginal.Scheme == Uri.UriSchemeHttps);
            bool validShortURL = Uri.TryCreate(customShortURL, UriKind.Absolute, out uriResultShort)
                && (uriResultShort.Scheme == Uri.UriSchemeHttp || uriResultShort.Scheme == Uri.UriSchemeHttps);

            if (!validOriginalURL || !validShortURL)
            {
                return BadRequest("Input is not a valid URL");
            }

            //Create custom short URL with hash provided by input
            customShortURL = new Uri(Request.Scheme + "://" + Request.Host.Value + Request.PathBase + "/" + _service.getShortURLHash(customShortURL)).ToString();

            var url = new URL
            {
                OriginalURL = originalURL,
                ShortURL = customShortURL,
                DateCreated = DateTime.Now
            };

            if (_service.getByShortURL(customShortURL) != null)
            {
                return BadRequest("Custom short URL already exists");
            }

            if (_service.checkShortURLValidity(url))
            {
                _service.saveShortURL(url);
                return Ok(url.ShortURL);
            }
            else
            {
                return BadRequest("Custom short URL is not valid");
            }
        }
    }
}
