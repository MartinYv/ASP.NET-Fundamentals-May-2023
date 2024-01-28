using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Watchlist.Data.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
//Has Id – a unique integer, Primary Key

//· Has Name – a string with min length 5 and max length 50 (required)

//· Has Movies – a collection of Movie