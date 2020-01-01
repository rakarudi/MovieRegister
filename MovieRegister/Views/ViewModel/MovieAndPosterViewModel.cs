using MovieRegister.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegister.Views.ViewModel
{
    public class MovieAndPosterViewModel
    {
        public string Poster { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string length { get; set; }
        public string year { get; set; }
        public string genre { get; set; }
        public string hasSeen { get; set; }
        public string isFavourite { get; set; }
    }
}
