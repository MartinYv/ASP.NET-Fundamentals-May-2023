﻿namespace SoftUniBazar.Models
{
	using SoftUniBazar.Data.Models;

	public class EditAdViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public decimal Price { get; set; }
		public string ImageUrl { get; set; } = null!;
		public int CategoryId { get; set; }
		public List<Category> Categories { get; set; } = new List<Category>();
	}
}