using MovieRatingApp.Data;
using MovieRatingApp.Interfaces;
using MovieRatingApp.Models;
using System.Text.RegularExpressions;

namespace MovieRatingApp.Repository
{
    public class MovieRepository : IMovie
    {
        private DataContext _context;
        public MovieRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            return Save();
        }
        public Movie GetMovieById(int movieId)
        {
            return _context.Movies.FirstOrDefault(movie => movie.Id.Equals(movieId));
        }
            
        public ICollection<Movie> GetMoviesByName(string movieName)
        {
            string pattern = $"^{movieName}.*";
            var movies = _context.Movies.ToList();
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return movies.Where(movie => !string.IsNullOrEmpty(movie.MovieName) && regex.IsMatch(movie.MovieName)).ToList();
        }

        public decimal GetMovieRating(int movieId)
        {
            var allReviews = _context.Reviews.Where(mov => mov.Movie.Id==movieId);
            if (allReviews.Count() <= 0)
            {
                return 0;
            }
            return ((decimal)allReviews.Sum(review => review.Rating) / allReviews.Count());
        }

        public ICollection<Movie> GetMovies()
        {
            return _context.Movies.OrderByDescending(movie => movie.ReleaseDate).ToList();
        }

        public ICollection<Movie> GetMoviesByCategory(string category)
        {
            return _context.Movies.Where(movie => movie.Category.Equals(category)).ToList();
        }

        public bool MovieExists(int movieId)
        {
            return _context.Movies.Any(movie => movie.Id == movieId);
        }
        public bool UpdateMovie(Movie movie)
        {
            _context.Movies.Update(movie);
            return Save();
        }
        public bool DeleteMovie(int movieId)
        {
            _context.Movies.Remove(GetMovieById(movieId));
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

       
    }
}
