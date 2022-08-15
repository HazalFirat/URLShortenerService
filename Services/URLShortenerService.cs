using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortenerService.Data;
using URLShortenerService.Helpers;
using URLShortenerService.Models;

namespace URLShortenerService.Services
{
    public class URLShortenerService : IURLShortenerService
    {
        private readonly URLShortenerDbContext _context;

        public URLShortenerService(URLShortenerDbContext context)
        {
            _context = context;
        }

        public URL getById(long id)
        {
            return _context.URLs.Find(id);
        }

        public URL getByShortURL(string shortURL)
        {
            foreach (var url in _context.URLs)
            {
                if (url.ShortURL == shortURL)
                {
                    return url;
                }
            }

            return null;
        }

        public long saveOriginalURL(URL url)
        {
            _context.URLs.Add(url);
            _context.SaveChanges();

            return url.Id;
        }

        public long saveShortURL(URL url)
        {
            _context.URLs.Add(url);
            _context.SaveChanges();

            return url.Id;
        }

        public void updateShortURL(URL url)
        {
            _context.URLs.Update(url);
            _context.SaveChanges();
        }

        public string createShortURLHash(URL url)
        {
            var hash = URLShortenerHelper.generateHash(url.Id);

            return hash;
        }

        public string getShortURLHash(string shortURL)
        {
            var hash = URLHelper.getHashFromURL(shortURL);

            return hash;
        }

        public bool checkShortURLValidity(URL url)
        {
            var hash = URLHelper.getHashFromURL(url.ShortURL);
            if (URLShortenerHelper.checkHashValidity(hash))
            {
                if (getByShortURL(url.ShortURL) != null)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
