using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortenerService.Models;

namespace URLShortenerService.Services
{
    public interface IURLShortenerService
    {
        URL getById(long id);
        URL getByShortURL(string shortURL);
        long saveOriginalURL(URL url);
        long saveShortURL(URL url);
        void updateShortURL(URL url);
        string createShortURLHash(URL url);
        string getShortURLHash(string shortURL);
        bool checkShortURLValidity(URL url);
    }
}
