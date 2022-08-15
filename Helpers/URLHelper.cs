using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortenerService.Helpers
{
    public class URLHelper
    {
        public static string getHashFromURL(string shortURL)
        {
            string hash;
            hash = shortURL.Substring(shortURL.LastIndexOf('/') + 1);

            return hash;
        }
    }
}
