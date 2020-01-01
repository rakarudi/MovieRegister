using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegister.Views.ViewModel
{
    public class MovieViewModel
    {
        [Required(ErrorMessage = "Title is required!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required!")]
        [MaxLength(350, ErrorMessage = "Max length is 350 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Movie length is required!")]
        public string Length { get; set; }
        [Required(ErrorMessage = "Year is required!")]
        public string Year { get; set; }
        [Required(ErrorMessage = "Genre is required!")]
        public string Genre { get; set; }
        public string HasSeen { get; set; }
        public string IsFavourite { get; set; }
    }
}
