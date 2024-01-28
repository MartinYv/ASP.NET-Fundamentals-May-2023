using Library.Data.Models;
using Library.Models;

namespace Library.Contracts
{
	public interface IBookService
	{
		Task AddBookAsync(AddBookViewModel model);
		Task AddToCollectionAsync(int id, string userId);
		Task RemoveFromCollectionAsync(int id, string? userId);
		Task <ICollection<MineBooksViewModel>>MineBooksAsync(string? userId);
		Task<ICollection<AllBooksViewModel>> AllBooksAsync();
		Task<ICollection<Category>> GetAllCategoriesAsync();
	}
}
