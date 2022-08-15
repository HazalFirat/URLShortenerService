using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortenerService.Helpers
{
    public class URLShortenerHelper
    {
        private static List<char> characters = new List<char>()
        {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '_', '-', '.', '~',
         'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
         'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B',
         'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
         'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
        private static readonly int length = characters.Count;
        private static int characterLimit = 6;

        public static string generateHash(long id)
        {
            string hash = "";

            while (id > 0)
            {
                hash += characters.ElementAt((int)(id % length)).ToString();
                id = id / length;
            }

            //If length of generated hash is more than the character limit
            if (hash.Length > characterLimit)
            {
                hash = "";
                Random rand = new Random();

                for (int i = 0; i < 6; i++)
                {
                    int random = rand.Next(0, length);

                    hash += characters.ElementAt(random).ToString();
                }
            }

            return hash;
        }

        public static bool checkHashValidity(string hash)
        {
            //Check if hash length is more than the character limit
            if (hash.Length > 0 && hash.Length <= characterLimit)
            {
                var characterCounter = 0;

                //Check if all chars in hash are allowed
                foreach (var character in hash)
                {
                    if (characters.Contains(character))
                    {
                        characterCounter++;
                    }
                }

                return hash.Length == characterCounter;
            }

            return false;
        }
    }
}
