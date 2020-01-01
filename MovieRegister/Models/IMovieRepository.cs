using MovieRegister.Views.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegister.Models
{
    public interface IMovieRepository
    {
        // Interface that predefines what functions we should ahve in our repository.

        Movies GetMovies();
        void AddMovie(MovieViewModel movie);
        void UpdateMovie(MovieViewModel movie, string editfavorite);
        void DeleteMovie(MovieViewModel movie);
        MoviesAndPosters BackToMovie(string searchString = "", string isFavorit = "");
        MoviesAndPosters GetFavorits();
        MoviesAndPosters GetRecentlyWatched();
        String GetOMDBData(string urlParameters);
    }
}
