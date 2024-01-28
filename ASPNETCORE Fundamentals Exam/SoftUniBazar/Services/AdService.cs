namespace SoftUniBazar.Services
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using SoftUniBazar.Data;
	using SoftUniBazar.Data.Models;
	using SoftUniBazar.Models;
	using SoftUniBazar.Services.Contracts;

	public class AdService : IAdService
	{
		private readonly BazarDbContext context;

		public AdService(BazarDbContext _context)
		{
			context = _context;
		}

		public async Task AddAdAsync(AddAdViewModel model, string? userId)
		{
			IdentityUser? user = await GetUser(userId);

			if (user == null)
			{
				throw new ArgumentException("Not existing user.");
			}

			Ad ad = new Ad()
			{
				Name = model.Name,
				Description = model.Description,
				CategoryId = model.CategoryId,
				ImageUrl = model.ImageUrl,
				OwnerId = user.Id,
				Owner = user,
				Price = model.Price,
				CreatedOn = DateTime.UtcNow
			};

			await context.AddAsync(ad);
			await context.SaveChangesAsync();
		}

		public async Task AddToUsersCartAsync(int id, string? userId)
		{
			Ad? ad = await context.Ads.FindAsync(id);

			if (ad == null)
			{
				throw new ArgumentException("Not existing add.");
			}

			IdentityUser? user = await GetUser(userId);

			if (user == null)
			{
				throw new ArgumentException("Not existing user.");
			}

			AdBuyer adBuyer = new AdBuyer()
			{
				Ad = ad,
				AdId = ad.Id,
				Buyer = user,
				BuyerId = user.Id
			};

			if (await context.AdsBuyers.ContainsAsync(adBuyer))
			{
				throw new ArgumentException("Ad already added to your cart.");
			}

			await context.AdsBuyers.AddAsync(adBuyer);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<AllAdsViewModel>> GetAllAdsAsync()
		{
			return await context.Ads.Select(a => new AllAdsViewModel()
			{
				Id = a.Id,
				Name = a.Name,
				Description = a.Description,
				ImageUrl = a.ImageUrl,
				Price = a.Price,
				CreatedOn = a.CreatedOn,
				OwnerId = a.OwnerId,
				Owner = a.Owner.UserName,
				Category = a.Category.Name,
				CategoryId = a.CategoryId
			}).ToListAsync();
		}

		public async Task<List<Category>> GetAllCategoriesAsync()
		{
			return await context.Categories.ToListAsync();
		}

		public async Task<EditAdViewModel> GetModelForEditAsync(int id, string? userId)
		{
			Ad? ad = await context.Ads.FindAsync(id);

			if (ad == null)
			{
				throw new ArgumentException("Invalid ad id.");
			}

			if (ad.OwnerId != userId)
			{
				throw new ArgumentException("You can edit only your ads.");
			}

			EditAdViewModel model = new EditAdViewModel()
			{
				Id = id,
				Name = ad.Name,
				Description = ad.Description,
				ImageUrl = ad.ImageUrl,
				Price = ad.Price,
				CategoryId = ad.CategoryId,
				Categories = await GetAllCategoriesAsync()
			};

			return model;
		}
		public async Task EditAdAsync(EditAdViewModel model)
		{
			Ad? ad = await context.Ads.FindAsync(model.Id);

			if (ad == null)
			{
				throw new ArgumentException("Invalid ad id.");
			}

			ad.Name = model.Name;
			ad.Description = model.Description;
			ad.Price = model.Price;
			ad.CategoryId = model.CategoryId;
			ad.ImageUrl = model.ImageUrl;

			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<AdViewModel>> GetUserAdsAsync(string? userId)
		{
			IdentityUser? user = await GetUser(userId);

			if (user == null)
			{
				throw new ArgumentException("Invalid user.");
			}

			return await context.AdsBuyers.Where(x => x.BuyerId == userId).Select(a => new AdViewModel()
			{
				Id = a.Ad.Id,
				Name = a.Ad.Name,
				Description = a.Ad.Description,
				ImageUrl = a.Ad.ImageUrl,
				Category = a.Ad.Category.Name,
				Owner = a.Ad.Owner.UserName,
				Price = a.Ad.Price
			}).ToListAsync();
		}

		public async Task RemoveFromUsersCartAsync(int id, string? userId)
		{
			Ad? ad = await context.Ads.FindAsync(id);

			if (ad == null)
			{
				throw new ArgumentException("Not existing add.");
			}

			IdentityUser? user = await GetUser(userId);

			if (user == null)
			{
				throw new ArgumentException("Not existing user.");
			}

			AdBuyer? adBuyer = await context.AdsBuyers.FirstOrDefaultAsync(ab => ab.BuyerId == userId && ab.AdId == id);

			if (adBuyer != null)
			{
				context.AdsBuyers.Remove(adBuyer);
				await context.SaveChangesAsync();
			}
		}
		public async Task<IdentityUser?> GetUser(string? userId)
		{
			if (string.IsNullOrWhiteSpace(userId))
			{
				return null;
			}

			IdentityUser? user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

			return user;
		}
	}
}