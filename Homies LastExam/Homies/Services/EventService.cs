namespace Homies.Services
{
    using Homies.Data;
    using Homies.Data.Models;
    using Homies.Models;
    using Homies.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Event = Homies.Data.Models.Event;
    public class EventService : IEventService
    {
        private readonly HomiesDbContext context;

        public EventService(HomiesDbContext _context)
        {
            context = _context;
        }



        public async Task AddEventAsync(string? userId, AddEventViewModel model)
        {
            IdentityUser? user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user id.");
            }

            var eventToAdd = new Event()
            {
                Name = model.Name,
                Description = model.Description,
                CreatedOn = DateTime.UtcNow,
                Start = model.Start,
                End = model.End,
                TypeId = model.TypeId,
                Organiser = user,
                OrganiserId = user.Id
            };

            await context.Events.AddAsync(eventToAdd);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllEventsViewModel>> AllEventsAsync()
        {
            return await context.Events.Select(e => new AllEventsViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Start = e.Start,
                End = e.End,
                CreatedOn = e.CreatedOn,
                Description = e.Description,
                Type = e.Type.Name,
                Organiser = e.Organiser.Email
            }).ToListAsync();
        }

        public async Task<EditEventViewModel> EditEventGetModelAsync(int id)
        {
            var eventForEdit = await context.Events.FindAsync(id);

            if (eventForEdit == null)
            {
                throw new ArgumentException("Invalid event Id.");
            }

            var formModel = new EditEventViewModel()
            {
                Id = id,
                Name = eventForEdit.Name,
                Description = eventForEdit.Description,
                Start = eventForEdit.Start,
                End = eventForEdit.End,
                Types = await GetAllTypes()
            };

            return formModel;
        }

        public async Task EditEventPostModelAsync(EditEventViewModel model)
        {
            Event? eventToEdit = await context.Events.FindAsync(model.Id);

            if (eventToEdit == null)
            {
                throw new ArgumentException("Invalid event Id.");
            }

            eventToEdit.Name = model.Name;
            eventToEdit.Description = model.Description;
            eventToEdit.Start = model.Start;
            eventToEdit.End = model.End;
            eventToEdit.TypeId = model.TypeId;

            await context.SaveChangesAsync();
        }

        public async Task<EventDetailViewModel> EventDetailAsync(int id)
        {
            Event? entity = await context.Events.Include(e => e.Type).Include(e => e.Organiser).FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new ArgumentException("Not existing event.");
            }

            EventDetailViewModel model = new EventDetailViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                CreatedOn = entity.CreatedOn,
                Start = entity.Start,
                End = entity.End,
                Type = entity.Type.Name,
                Organiser = entity.Organiser.Email
            };

            return model;
        }

        public async Task<IEnumerable<Type>> GetAllTypes()
        {
            return await context.Types.ToListAsync();
        }

        public async Task<IEnumerable<AllEventsViewModel>> JoinedEventsAsync(string? userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("Invalid user id.");
            }

            IdentityUser? user = await context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("There isn't user with hte provided Id.");
            }

            var allEvents = await AllEventsAsync();
            var joinedEvents = allEvents.Where(x => x.Organiser == user.Email).ToList();

            return joinedEvents;
        }

        public async Task JoinToEventAsync(int id, string? userId)
        {
            var user = await context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("Not existing user.");
            }

            var eventToJoin = await context.Events.FindAsync(id);
            if (eventToJoin == null)
            {
                throw new ArgumentException("Not existing event.");
            }

            EventParticipant eventParticipant = new EventParticipant()
            {
                Event = eventToJoin,
                EventId = eventToJoin.Id,
                Helper = user,
                HelperId = user.Id
            };

            if (await context.EvensParticimants.ContainsAsync(eventParticipant))
            {
                throw new ArgumentException("Already added.");
            }

            await context.EvensParticimants.AddAsync(eventParticipant);
            await context.SaveChangesAsync();
        }
    }
}