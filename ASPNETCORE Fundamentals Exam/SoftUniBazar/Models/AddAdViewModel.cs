namespace SoftUniBazar.Models
{
	using Microsoft.EntityFrameworkCore;
	using SoftUniBazar.Data.Models;
	using System.ComponentModel.DataAnnotations;

	using static SoftUniBazar.Data.DataConstants.Ad;

	public class AddAdViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }  

        [Required]
        [Url]
        [MaxLength(UrlMaxLength)]
        public string ImageUrl { get; set; } = null!;
        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}