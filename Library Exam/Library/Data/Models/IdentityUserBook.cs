using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Models
{
	public class IdentityUserBook
	{
        [ForeignKey("Collector")]
        public string CollectorId { get; set; } = null!;
        public IdentityUser Collector { get; set; } = null!;

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
