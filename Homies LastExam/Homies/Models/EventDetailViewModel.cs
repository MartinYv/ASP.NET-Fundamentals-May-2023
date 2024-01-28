namespace Homies.Models
{
    public class EventDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Organiser { get; set; } = null!;
        public string Type { get; set; } = null!;
    }
}
