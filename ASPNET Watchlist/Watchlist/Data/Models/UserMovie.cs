using System.ComponentModel.DataAnnotations.Schema;
using Watchlist.Data.Models;

namespace Watchlist.Data.Models
{
    public class UserMovie
    {
        [ForeignKey("User")]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;


        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

    }
}
//UserId – a string, Primary Key, foreign key (required)

//· User – User

//· MovieId – an integer, Primary Key, foreign key (required)

//· Movie – Movie