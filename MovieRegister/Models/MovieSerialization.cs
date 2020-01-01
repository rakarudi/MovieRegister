using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MovieRegister.Models
{
    public class MovieSerialization
    {
        [XmlElement("title")]
        public string Title { get; set;}
        [XmlElement("description")]
        public string Description { get; set; }
        [XmlElement("length")]
        public string Length { get; set; }
        [XmlElement("year")]
        public string Year { get; set; }
        [XmlElement("genre")]
        public string Genre { get; set; }
        [XmlElement("hasSeen")]
        public string HasSeen { get; set; }
        [XmlElement("isFavourite")]
        public string IsFavourite { get; set; }

        // Used for serializeing
        public MovieSerialization()
        {

        }
    }
}
