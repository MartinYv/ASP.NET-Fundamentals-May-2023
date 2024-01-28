using Watchlist.Data.Models;
using Watchlist.Models;

namespace Watchlist.Contracts
{
    public interface IMovieService
    {
        Task AddMovieAsync(AddMovieViewModel model);

        Task <IEnumerable<Genre>> GetGenresAsync();

        Task<IEnumerable<MovieViewModel>> GetWatchedAsync(string userId);

        Task<IEnumerable<MovieViewModel>> GetAllAsync();

        Task RemoveMovieFromCollectionAsync(string userId, int movieId);
        Task AddMovieToCollectionAsync(string userId, int movieId);
    }
}
