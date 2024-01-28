using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Watchlist.Data.Models
{
    public class Movie
    {

        [Key]
        public int Id { get; set; }


        [Required, MaxLength(50)]
        public string Title { get; set; } = null!;


        [Required, MaxLength(50)]
        public string Director { get; set; } = null!;


        [Required]
        public string ImageUrl { get; set; } = null!;

        
        [Required, Range(0.00, 10.00)]
        public decimal Rating { get; set; }

        [ForeignKey("Genre")]
        public int? GenreId { get; set; }
        public Genre? Genre { get; set; } 

        public ICollection<UserMovie> UsersMovies { get; set; } = new List<UserMovie>();
    }
}

//Has Title – a string with min length 10 and max length 50 (required)

//· Has Director – a string with min length 5 and max length 50 (required)

//· Has ImageUrl – a string (required)

//· Has Rating – a decimal with min value 0.00 and max value 10.00 (required)

//· Has GenreId – an integer (required)

//· Has Genre – a Genre (required)

//· Has UsersMovies – a collection of type UserMovie