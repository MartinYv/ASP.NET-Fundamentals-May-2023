namespace SoftUniBazar.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using System.Security.Claims;

	using SoftUniBazar.Models;
	using SoftUniBazar.Services.Contracts;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class AdController : Controller
	{
		private readonly IAdService adService;

		public AdController(IAdService _adService)
		{
			adService = _adService;
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			AddAdViewModel model = new AddAdViewModel()
			{
				Categories = await adService.GetAllCategoriesAsync()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddAdViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				string? userId = GetUserId();

				await adService.AddAdAsync(model, userId);

				return RedirectToAction(nameof(All));
			}
			catch (Exception)
			{
				return RedirectToAction(nameof(Add));
			}
		}

		public async Task<IActionResult> All()
		{
			var model = await adService.GetAllAdsAsync();
			return View(model);
		}

		public async Task<IActionResult> Cart()
		{
			try
			{
				string? userId = GetUserId();

				var model = await adService.GetUserAdsAsync(userId);
				return View(model);
			}
			catch (Exception)
			{
				return RedirectToAction(nameof(All));
			}
		}

		public async Task<IActionResult> AddToCart(int id)
		{
			try
			{
				string? userId = GetUserId();

				await adService.AddToUsersCartAsync(id, userId);
				return RedirectToAction(nameof(Cart));


			}
			catch (Exception)
			{

				return RedirectToAction(nameof(All));
			}
		}

		public async Task<IActionResult> RemoveFromCart(int id)
		{
			try
			{
				string? userId = GetUserId();

				await adService.RemoveFromUsersCartAsync(id, userId);
				return RedirectToAction(nameof(All));
			}
			catch (Exception)
			{
				return RedirectToAction(nameof(All));
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			try
			{
				string? userId = GetUserId();

				EditAdViewModel model = await adService.GetModelForEditAsync(id, userId);
				return View(model);

			}
			catch (Exception)
			{
				return RedirectToAction(nameof(All));
			}
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditAdViewModel model)
		{
			try
			{
				await adService.EditAdAsync(model);
				return RedirectToAction(nameof(All));
			}
			catch (Exception)
			{
				return RedirectToAction(nameof(All));
			}
		}

		public string? GetUserId() => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
	}
}