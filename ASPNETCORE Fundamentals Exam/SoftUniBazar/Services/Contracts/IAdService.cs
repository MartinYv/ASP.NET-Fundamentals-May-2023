namespace SoftUniBazar.Services.Contracts
{
	using Microsoft.AspNetCore.Identity;

	using SoftUniBazar.Data.Models;
	using SoftUniBazar.Models;
	public interface IAdService
	{
		Task AddAdAsync(AddAdViewModel model, string? userId);
		Task<List<Category>> GetAllCategoriesAsync();
		Task<IdentityUser?> GetUser(string userId);
		Task<IEnumerable<AllAdsViewModel>> GetAllAdsAsync();
		Task<IEnumerable<AdViewModel>> GetUserAdsAsync(string? userId);
		Task AddToUsersCartAsync(int id, string? userId);
		Task RemoveFromUsersCartAsync(int id, string? userId);
		Task<EditAdViewModel> GetModelForEditAsync(int id, string? userId);
		Task EditAdAsync(EditAdViewModel model);
	}
}