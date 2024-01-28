using Homies.Models;
using Homies.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;

namespace Homies.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService _eventService)
        {
            eventService = _eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddEventViewModel model = new AddEventViewModel()
            {
                Types = await eventService.GetAllTypes()
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                await eventService.AddEventAsync(userId, model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(All));
            }
        }

        public async Task<IActionResult> All()
        {
            var model = await eventService.AllEventsAsync();
            return View(model);
        }

        public async Task<IActionResult> Joined()
        {
            string? userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var model = await eventService.JoinedEventsAsync(userId);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await eventService.EditEventGetModelAsync(id);

                return View(model);
            }
            catch (Exception)
            {

              return  RedirectToAction(nameof(All));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEventViewModel model)
        {

            try
            {
                await eventService.EditEventPostModelAsync(model);

            }
            catch (Exception)
            {
            }
            return RedirectToAction(nameof(Joined));

        }

        public async Task<IActionResult> Join(int id)
        {
            try
            {
                string? userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                await eventService.JoinToEventAsync(id, userId);

                return RedirectToAction(nameof(Joined));
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Joined));
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                EventDetailViewModel model = await eventService.EventDetailAsync(id);
                return View(model);

            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Joined));
            }
        }
    }
}