using System.ComponentModel.DataAnnotations;
using Watchlist.Data.Models;

namespace Watchlist.Models
{
    public class AddMovieViewModel
    {

        [Required, MinLength(10), MaxLength(50)]
        public string Title { get; set; } = null!;

        [Required, MinLength(5), MaxLength(50)]
        public string Director { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required, Range(0.00, 10.00)]
        public decimal Rating { get; set; }

        public int GenreId { get; set; }
        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();

    }
}
