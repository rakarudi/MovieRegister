using MovieRegister.Models;
using MovieRegister.Views.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MovieRegister.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        public readonly MovieRepository movieRepository;
        public HomeController()
        {
            movieRepository = new MovieRepository(new DataConnector());
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(movieRepository.GetRecentlyWatched());
        }

        [HttpGet]
        [Route("Movie")]
        public ActionResult Movie(string searchString = "")
        {
            return View(movieRepository.BackToMovie(searchString));
        }

        [HttpGet]
        [Route("FavoritMovies")]
        public ActionResult FavoritMovies()
        {
            return View(movieRepository.GetFavorits());
        }

        [HttpGet]
        public ActionResult AddMoviePage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMoviePage(MovieViewModel movie)
        {
            if(ModelState.IsValid)
            { 
                movieRepository.AddMovie(movie);
                return View("BackToMovie", movieRepository.BackToMovie());
            }
            return View(movie);
        }

        [HttpGet]
        [Route("DeleteMovie")]
        public ActionResult DeleteMovie(MovieViewModel movie)
        {
            movieRepository.DeleteMovie(movie);

            return View("BackToMovie", movieRepository.BackToMovie());
        }

        [HttpGet]
        [Route("EditMovie")]
        public ActionResult EditMovie(MovieViewModel movie)
        {
            return View(movie);
        }

        [HttpPost]
        [Route("EditMovie")]
        public ActionResult EditMovie(MovieViewModel movie, string editType = "All")
        {
            if(ModelState.IsValid)
            {
                movieRepository.UpdateMovie(movie, editType);
                return View("BackToMovie", movieRepository.BackToMovie());
            }
            return View(movie);
        }


        [HttpGet]
        [Route("AddFavoriteMovie")]
        public ActionResult AddFavoriteMovie(MovieViewModel movie, string editType = "favorite")
        {
            movieRepository.UpdateMovie(movie, editType);

            return View("BackToMovie", movieRepository.BackToMovie());
        }

        [HttpGet]
        [Route("RemoveFavoriteMovie")]
        public ActionResult RemoveFavoriteMovie(MovieViewModel movie, string editType = "favorite")
        {
            movieRepository.UpdateMovie(movie, editType);

            return View("BackToMovie", movieRepository.BackToMovie());
        }

        [HttpGet]
        [Route("WatchMoviePage")]
        public ActionResult WatchMoviePage(MovieViewModel movie, string editType = "hasSeen")
        {
            movieRepository.UpdateMovie(movie, editType);

            return View("BackToMovie", movieRepository.BackToMovie());
        }

        [HttpGet]
        [Route("MovieDetails")]
        public ActionResult MovieDetails(MovieAndPosterViewModel movieAndPoster)
        {
            return View(movieAndPoster);
        }
    }
}