using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library.Services
{
	public class BookService : IBookService
	{
		private readonly LibraryDbContext context;

		public BookService(LibraryDbContext _context)
		{
			context = _context;
		}

		public async Task AddBookAsync(AddBookViewModel model)
		{
			Book book = new Book()
			{
				Author = model.Author,
				CategoryId = model.CategoryId,
				ImageUrl = model.Url,
				Description = model.Description,
				Rating = decimal.Parse(model.Rating),
				Title = model.Title
			};

			await context.AddAsync(book);
			await context.SaveChangesAsync();
		}

		public async Task AddToCollectionAsync(int id, string userId)
		{			
			var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id);

			if (book != null)
			{
				if (!context.UsersBooks.Any(x => x.CollectorId == userId && x.BookId == id))
				{
					await context.UsersBooks.AddAsync(new IdentityUserBook()
					{
						BookId = id,
						CollectorId = userId
					});

					await context.SaveChangesAsync();
				}
			}
		}

		public async Task<ICollection<AllBooksViewModel>> AllBooksAsync()
		{
			return await context.Books.Select(x => new AllBooksViewModel()
			{
				Author = x.Author,
				Category = x.Category.Name,
				Id = x.Id,
				ImageUrl = x.ImageUrl,
				Rating = x.Rating.ToString(),
				Title = x.Title
			}).ToListAsync();
		}

		public async Task<ICollection<Category>> GetAllCategoriesAsync()
		{
			return await context.Categories.ToListAsync();
		}

		public async Task<ICollection<MineBooksViewModel>> MineBooksAsync(string? userId)
		{
			return await context.UsersBooks.Where(x => x.CollectorId == userId)
				.Select(x=> new MineBooksViewModel()
			{
				Id = x.BookId,
				Title = x.Book.Title,
				Author = x.Book.Author,
				ImageUrl = x.Book.ImageUrl,
				Description = x.Book.Description,
				Category = x.Book.Category.Name
			}).ToListAsync();	
		}

		public async Task RemoveFromCollectionAsync(int id, string? userId)
		{
			var userBook = await context.UsersBooks.FirstOrDefaultAsync(x => x.BookId == id && x.CollectorId == userId);

			if (userBook != null)
			{
				 context.UsersBooks.Remove(userBook);
				await context.SaveChangesAsync();
			}
		}
	}
}
