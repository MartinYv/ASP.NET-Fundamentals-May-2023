namespace Homies.Services.Interfaces
{
	using Homies.Models;
	public interface IEventService
	{
		Task AddEventAsync(string? userId, AddEventViewModel model);
		Task<IEnumerable<Homies.Data.Models.Type>>GetAllTypes();
		Task <IEnumerable<AllEventsViewModel>> AllEventsAsync();
		Task<IEnumerable<AllEventsViewModel>> JoinedEventsAsync(string? userId);
        Task<EditEventViewModel>EditEventGetModelAsync(int id);
        Task EditEventPostModelAsync(EditEventViewModel model);
        Task JoinToEventAsync(int id, string? userId);
        Task<EventDetailViewModel> EventDetailAsync(int id);
    }
}
