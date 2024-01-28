namespace Homies.Models
{
	using System.ComponentModel.DataAnnotations;

	public class AddEventViewModel
	{
        [Required]
		[StringLength(20, MinimumLength = 5)]
		public string Name { get; set; } = null!;

		[Required]
		[StringLength(150, MinimumLength = 15)]
		public string Description { get; set; } = null!;

		[Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public DateTime Start { get; set; }

		[Required]
		public DateTime End { get; set; }

		[Required]
        public int TypeId { get; set; }
		public IEnumerable<Homies.Data.Models.Type> Types { get; set; } = new List<Homies.Data.Models.Type>();
	}
}