namespace Homies.Data.Models
{
	using Microsoft.AspNetCore.Identity;
	using System.ComponentModel.DataAnnotations.Schema;

	public class EventParticipant
	{
		
		[ForeignKey("Helper")]
		public string HelperId { get; set; } = null!;
		public IdentityUser Helper { get; set; } = null!;

		
		[ForeignKey("Event")]
		public int EventId { get; set; }
		public Event Event { get; set; } = null!;
	}
}