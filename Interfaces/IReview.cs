using MovieRatingApp.Models;

namespace MovieRatingApp.Interfaces
{
    public interface IReview
    {
        ICollection<Reviews> GetReviews();
        Reviews GetReview(int reviewId);
        ICollection<Reviews> GetReviewsOfAMovie(int movieId);
        bool ReviewExists(int reviewId);
        bool CreateReview(Reviews review);
        bool UpdateReview(Reviews review);
        bool DeleteReview(Reviews review);
        bool DeleteReviews(List<Reviews> reviews);
        bool Save();
    }
}
