using System.ComponentModel.DataAnnotations;
using Watchlist.Data.Models;

namespace Watchlist.Models
{
    public class MovieViewModel
    {
        public string Title { get; set; } = null!;
       
        public string Director { get; set; } = null!;
       
        public string ImageUrl { get; set; } = null!;
      
        public decimal Rating { get; set; }

       public string Genre { get; set; } = null!;

        public int Id { get; set; }
    }
}
