using MovieRatingApp.Models;

namespace MovieRatingApp.Interfaces
{
    public interface IMovie
    {
        ICollection<Movie>GetMovies();
        Movie GetMovieById(int movieId);
        ICollection<Movie> GetMoviesByName(string movieName);
        decimal GetMovieRating(int movieId);
        bool MovieExists(int movieId);
        ICollection<Movie> GetMoviesByCategory(string category);
        bool UpdateMovie(Movie movie);
        bool DeleteMovie(int movieId);
        bool CreateMovie(Movie movie);
        bool Save();    
    }
}
