using Library.Contracts;
using Library.Data.Models;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
	[Authorize]
	public class BookController : Controller
	{
		private readonly IBookService bookService;

		public BookController(IBookService _bookService)
		{
			bookService = _bookService;
		}
		public async Task<IActionResult> All()
		{
			var model = await bookService.AllBooksAsync();

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			AddBookViewModel model = new AddBookViewModel()
			{
				Categories = await bookService.GetAllCategoriesAsync()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddBookViewModel model)
		{

			if (!ModelState.IsValid)
			{
				return View();
			}

			await bookService.AddBookAsync(model);

			return RedirectToAction("All", "Book");
		}

		public async Task<IActionResult> AddToCollection(int id)
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

			await bookService.AddToCollectionAsync(id, userId);
			return RedirectToAction("All", "Book");
		}

		public async Task<IActionResult> Mine()
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

			var model = await bookService.MineBooksAsync(userId);

			return View(model);
		}

		public async Task<IActionResult> RemoveFromCollection(int id)
		{
			var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

			await bookService.RemoveFromCollectionAsync(id, userId);
			return RedirectToAction("Mine", "Book");
		}
	}
}
