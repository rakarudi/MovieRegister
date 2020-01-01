using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MovieRegister.Models
{
    [XmlRoot]
    public class Movies
    {
        [XmlElement("Movie", typeof(MovieSerialization))]
        public List<MovieSerialization> Movie { get; set; }

        // Used for serializeing
        public Movies()
        {

        }
    }
}
