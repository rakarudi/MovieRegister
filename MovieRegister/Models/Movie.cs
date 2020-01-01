using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegister.Models
{
    public class Movie
    {
        public string title;
        public string description;
        public string length;
        public string year;
        public string genre;
        public string hasSeen;
        public string isFavourite;

        public Movie(string _title, string _description, string _length, string _year, string _genre, string _hasSeen, string _isFavourite)
        {
            title = _title;
            description = _description;
            length = _length;
            year = _year;
            genre = _genre;
            hasSeen = _hasSeen;
            isFavourite = _isFavourite;
        }
    }
}
