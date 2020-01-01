using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MovieRegister.Models;
using MovieRegister.Views.ViewModel;

namespace MovieRegister.Models
{
    public class MovieRepository : IMovieRepository
    {
        // Crates a DataConnector obecjt called data
        private readonly DataConnector data;
        public MovieRepository(DataConnector _data)
        {
            data = _data;
        }

        // Adds data using data.AddData.
        public void AddMovie(MovieViewModel movie)
        {
            data.AddData(movie);
        }

        // Deletes data using data.DeleteData.
        public void DeleteMovie(MovieViewModel movie)
        {
            data.DeleteData(movie);
        }

        // Gets all movies using the data connector.
        public Movies GetMovies()
        {
            var result = data.Connector();
            return result;
        }
        
        // Updates a movie using data.EditData.
        public void UpdateMovie(MovieViewModel movie, string editfavorite)
        {
            data.EditData(movie, editfavorite);
        }

        // A function that makes a new get from the XML data and always return the updated view.
        // Used in most calls to redirect the userd back to the movie page.
        public MoviesAndPosters BackToMovie(string searchString = "", string isFavorit = "")
        {
            MoviesAndPosters mp = new MoviesAndPosters();

            IEnumerable<MovieSerialization> moviesAfterFilter;
            var movies = GetMovies();
            var movieView = new List<Movie>();
            var posterView = new List<String>();

            if (isFavorit == "true")
            {
                moviesAfterFilter = movies.Movie.Where((m =>
                    m.IsFavourite == isFavorit));
            }
            else
            {
                moviesAfterFilter = movies.Movie.Where((m =>
                    m.Title.Contains(searchString) ||
                    m.Description.Contains(searchString) ||
                    m.Genre.Contains(searchString)));
            }

            foreach (var r in moviesAfterFilter)
            {
                // Set url parameters for OMDB search
                string urlParameters = "?s=" + HttpUtility.UrlEncode(r.Title) + "&apikey=12d85cab";
                posterView.Add(GetOMDBData(urlParameters));

                var addMoveToView = new Movie(r.Title, r.Description, CalcHoursAndMinutes(r.Length), r.Year, r.Genre, r.HasSeen, r.IsFavourite);
                movieView.Add(addMoveToView);
            }

            mp.Movies = movieView;
            mp.Posters = posterView;

            return mp;
        }

        // A function that searched for all movies that has IsFavourite = "true".
        public MoviesAndPosters GetFavorits()
        {
            MoviesAndPosters mp = new MoviesAndPosters();

            IEnumerable<MovieSerialization> moviesAfterFilter;
            var movies = GetMovies();
            var movieView = new List<Movie>();
            var posterView = new List<String>();


            moviesAfterFilter = movies.Movie.Where((m =>
                m.IsFavourite == "true"));


            foreach (var r in moviesAfterFilter)
            {
                // Set url parameters for OMDB search
                string urlParameters = "?s=" + HttpUtility.UrlEncode(r.Title) + "&apikey=12d85cab";
                posterView.Add(GetOMDBData(urlParameters));

                var addMoveToView = new Movie(r.Title, r.Description, CalcHoursAndMinutes(r.Length), r.Year, r.Genre, r.HasSeen, r.IsFavourite);
                movieView.Add(addMoveToView);
            }

            mp.Movies = movieView;
            mp.Posters = posterView;

            return mp;
        }

        // A function that search for all movies with HasSeen = "true".
        public MoviesAndPosters GetRecentlyWatched()
        {
            MoviesAndPosters mp = new MoviesAndPosters();

            IEnumerable<MovieSerialization> moviesAfterFilter;
            var movies = GetMovies();
            var movieView = new List<Movie>();
            var posterView = new List<String>();


            moviesAfterFilter = movies.Movie.Where((m =>
                m.HasSeen == "true"));


            foreach (var r in moviesAfterFilter)
            {
                // Set url parameters for OMDB search
                string urlParameters = "?s=" + HttpUtility.UrlEncode(r.Title) + "&apikey=12d85cab";
                posterView.Add(GetOMDBData(urlParameters));

                var addMoveToView = new Movie(r.Title, r.Description, CalcHoursAndMinutes(r.Length), r.Year, r.Genre, r.HasSeen, r.IsFavourite);
                movieView.Add(addMoveToView);
            }

            mp.Movies = movieView;
            mp.Posters = posterView;

            return mp;
        }

        // A function that gets data from OMDM to return poster urls and other data.
        public String GetOMDBData(string urlParameters)
        {
            string poster = "";
            string OMDP_URL = "http://www.omdbapi.com/";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(OMDP_URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var omdbData = response.Content.ReadAsAsync<OMDBResult>().Result;

                if (omdbData.search == null)
                {
                    poster = "";
                }
                else
                {
                    poster = omdbData.search[0].Poster;
                }
            }
            else
            {
                poster = "";
            }

            return poster;
        }

        // A private function to convert 120 to 2h 0m for a more userfrendly view.
        private string CalcHoursAndMinutes(string time)
        {
            var timeInNumber = int.Parse(time);

            TimeSpan span = TimeSpan.FromMinutes(timeInNumber);
            string result = span.Hours + "h " + span.Minutes + "m";

            return result;
        }


    }
}
