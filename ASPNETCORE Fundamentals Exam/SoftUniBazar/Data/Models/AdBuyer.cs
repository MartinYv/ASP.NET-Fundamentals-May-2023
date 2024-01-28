namespace SoftUniBazar.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations.Schema;
    public class AdBuyer
    {
        [ForeignKey("Buyer")]
        public string BuyerId { get; set; } = null!;
        public IdentityUser Buyer { get; set; } = null!;

        [ForeignKey("Ad")]
        public int AdId { get; set; }
        public Ad Ad { get; set; } = null!;
    }
}