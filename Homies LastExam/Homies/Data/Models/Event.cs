using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Homies.Data.Models
{
	public class Event
	{
		[Required]
		public int Id { get; set; }

		[Required]
		[StringLength(20)]
		public string Name { get; set; } = null!;

		[Required]
		[StringLength(150)]
		public string Description { get; set; } = null!;

		[Required]
		public string OrganiserId { get; set; } = null!;

		[ForeignKey("Organaiser")]
		public IdentityUser Organiser { get; set; } = null!;

		[Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public DateTime Start { get; set; }

		[Required]
		public DateTime End { get; set; }


		[ForeignKey("Type")]
		public int TypeId { get; set; }
		public Type Type { get; set; } = null!;

		//public List<EventParticipant> EventsParticipants { get; set; } = new List<EventParticipant>();
    }
}
