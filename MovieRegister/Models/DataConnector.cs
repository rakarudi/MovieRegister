using MovieRegister.Views.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MovieRegister.Models
{
    public class DataConnector
    {
        //Connection string towards XML file. (Could have been stored in a config file)
        // XML filepath have to be added to string below!

        string fileLocation = ""; // Add filepath here!
        
        // The connector that makes the connection towards the XML file.
        // Connector is used in Add, Delete and Edit Data.
        public Movies Connector()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Movies));
                using (FileStream fs = File.OpenRead(fileLocation))
                {
                    var movies = (Movies)serializer.Deserialize(fs);

                    return movies;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Adds a new movie to the XML file.
        public void AddData(MovieViewModel movie)
        {
            var currentMovies = Connector();
            var newMovie = ParseViewToSerialize(movie);

            currentMovies.Movie.Add(newMovie);

            XmlSerializer serializer = new XmlSerializer(typeof(Movies));
            using (Stream outputStream = File.OpenWrite(fileLocation))
                serializer.Serialize(outputStream, currentMovies);
        }

        // Deletes a movie form the XML file.
        public void DeleteData(MovieViewModel movie)
        {
            XmlDocument xDoc = new XmlDocument();
                xDoc.Load(fileLocation);

            foreach (XmlNode node in xDoc.SelectNodes("Movies/Movie"))
            {
                if(node.SelectSingleNode("title").InnerText == movie.Title)
                {
                    node.ParentNode.RemoveChild(node);
                    xDoc.Save(fileLocation);
                }
            }
        }

        // EditData has multipe functionallities that coul have been devided in several functions.
        // We can Edit a movie, Favoritize a movie and mark a movie as HasSeen if we recently watched it.
        public void EditData(MovieViewModel movie, string editType)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(fileLocation);

            foreach (XmlNode node in xDoc.SelectNodes("Movies/Movie"))
            {
                if (node.SelectSingleNode("title").InnerText == movie.Title)
                {
                    if (editType == "favorite")
                    {
                        node.SelectSingleNode("description").InnerText = movie.Description;

                        if (movie.Length.Contains("h"))
                        {
                            node.SelectSingleNode("length").InnerText = AntiCalcHoursAndMinutes(movie.Length);
                        }
                        else
                        {
                            node.SelectSingleNode("length").InnerText = movie.Length;
                        }

                        node.SelectSingleNode("year").InnerText = movie.Year;
                        node.SelectSingleNode("genre").InnerText = movie.Genre;
                        node.SelectSingleNode("hasSeen").InnerText = movie.HasSeen;

                        if(node.SelectSingleNode("isFavourite").InnerText == "false")
                        {
                            node.SelectSingleNode("isFavourite").InnerText = "true";
                        }
                        else
                        {
                            node.SelectSingleNode("isFavourite").InnerText = "false";
                        }

                        xDoc.Save(fileLocation);
                    }
                    else if(editType == "All")
                    {
                        node.SelectSingleNode("description").InnerText = movie.Description;

                        if (movie.Length.Contains("h"))
                        {
                            node.SelectSingleNode("length").InnerText = AntiCalcHoursAndMinutes(movie.Length);
                        }
                        else
                        {
                            node.SelectSingleNode("length").InnerText = movie.Length;
                        }

                        node.SelectSingleNode("year").InnerText = movie.Year;
                        node.SelectSingleNode("genre").InnerText = movie.Genre;
                        node.SelectSingleNode("hasSeen").InnerText = movie.HasSeen;
                        node.SelectSingleNode("isFavourite").InnerText = movie.IsFavourite;

                        xDoc.Save(fileLocation);
                    }
                    else if(editType == "hasSeen")
                    {
                        node.SelectSingleNode("description").InnerText = movie.Description;

                        if(movie.Length.Contains("h"))
                        {
                            node.SelectSingleNode("length").InnerText = AntiCalcHoursAndMinutes(movie.Length);
                        }
                        else
                        {
                            node.SelectSingleNode("length").InnerText = movie.Length;
                        }

                        node.SelectSingleNode("year").InnerText = movie.Year;
                        node.SelectSingleNode("genre").InnerText = movie.Genre;

                        if(node.SelectSingleNode("hasSeen").InnerText == "false")
                        {
                            node.SelectSingleNode("hasSeen").InnerText = movie.HasSeen;
                        }

                        node.SelectSingleNode("isFavourite").InnerText = movie.IsFavourite;

                        xDoc.Save(fileLocation);
                    }
                }
            }
        }

        // A private function to convert the format of 2h 0m to a string of 120 to follow the format of the database.
        private string AntiCalcHoursAndMinutes(string time)
        {
            var dataSplit = time.Split('h', ' ', 'm');

            var hoursInMinutes = int.Parse(dataSplit[0]) * 60;
            var minutes = int.Parse(dataSplit[2]);

            int result = hoursInMinutes + minutes;

            return result.ToString();
        }

        // A private function that is used to parse a MovieViewModel to a MovieSerialization object.
        // MovieSerialization objects are the type that is known to the database.
        private MovieSerialization ParseViewToSerialize(MovieViewModel movie)
        {
            MovieSerialization _movie = new MovieSerialization
            {
                Title = movie.Title,
                Description = movie.Description,
                Length = movie.Length,
                Genre = movie.Genre,
                Year = movie.Year,
                HasSeen = "false",
                IsFavourite = "false"
            };

            return _movie;
        }
    }
}
