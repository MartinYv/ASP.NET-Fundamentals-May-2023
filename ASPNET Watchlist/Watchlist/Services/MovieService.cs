using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Watchlist.Contracts;
using Watchlist.Data;
using Watchlist.Data.Models;
using Watchlist.Models;

namespace Watchlist.Services
{
    public class MovieService : IMovieService
    {
        private readonly WatchlistDbContext context;

        public MovieService(WatchlistDbContext _context)
        {
            context = _context;
        }
        public async Task AddMovieAsync(AddMovieViewModel model)
        {

            var movie = new Movie()
            {
                Title = model.Title,
                Director = model.Director,
                GenreId = model.GenreId,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating
            };

            await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await context.Genres.ToListAsync();
        }

       
        public async Task<IEnumerable<MovieViewModel>> GetWatchedAsync(string userId)
        {
            var user = await context.Users.Where(u => u.Id == userId)
                .Include(um => um.UsersMovies)
                .ThenInclude(um => um.Movie)
                .ThenInclude(m => m.Genre).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id!");
            }

            return user.UsersMovies.Select(x => new MovieViewModel()
            {
                Director = x.Movie.Director,
                ImageUrl = x.Movie.ImageUrl,
                Rating = x.Movie.Rating,
                Title = x.Movie.Title,
                Genre = x?.Movie?.Genre?.Name
            });
        }

        
        public async Task<IEnumerable<MovieViewModel>> GetAllAsync()
        {
            var movies = await context.Movies.Include(x => x.Genre).ToListAsync();

            return movies.Select(x => new MovieViewModel()
            {
                Director = x.Director,
                ImageUrl = x.ImageUrl,
                Rating = x.Rating,
                Title = x.Title,
                Genre = x?.Genre?.Name
            });
        }

        public async Task RemoveMovieFromCollectionAsync(string userId, int movieId)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id!");
            }

            var movie = user.UsersMovies.Where(x => x.MovieId == movieId).FirstOrDefault();

            if (movie != null)
            {
                user.UsersMovies.Remove(movie);

                await context.SaveChangesAsync();
            }
        }

        public async Task AddMovieToCollectionAsync(string userId, int movieId)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user Id!");
            }

            var movie = await context.Movies.Where(x => x.Id == movieId).FirstOrDefaultAsync();

            if (movie != null)
            {
                user.UsersMovies.Add(new UserMovie()
                {
                    User = user,
                    UserId = userId,
                    Movie = movie,
                    MovieId = movieId
                });

               await context.SaveChangesAsync();
            }


        }
    }
}
