namespace SoftUniBazar.Data.Models
{
	using System.ComponentModel.DataAnnotations;

	using static SoftUniBazar.Data.DataConstants.Category;
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public List<Ad> Ads { get; set; } = new List<Ad>();
    }
}