using Library.Data.Models;
using System.ComponentModel.DataAnnotations;


namespace Library.Models
{
	public class AddBookViewModel
	{
		[Required]
		[MinLength(10), MaxLength(50)]
		public string Title { get; set; } = null!;

		[Required]
		[MinLength(5), MaxLength(50)]
		public string Author { get; set; } = null!;

		[Required]
		[MinLength(5), MaxLength(5000)]
		public string Description { get; set; } = null!;

		[Required]
		public string Url { get; set; } = null!;

		[Required]
		[Range(0, 10)]
		public string Rating { get; set; } = null!;
		public ICollection<Category> Categories { get; set; } = new List<Category>();
		
		[Required]
		public int CategoryId { get; set; }

	}
}