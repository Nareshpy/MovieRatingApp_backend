using Microsoft.EntityFrameworkCore.Diagnostics;
using MovieRatingApp.Data;
using MovieRatingApp.Models;

namespace MovieRatingApp.Repository
{
    public class ReviewRepository : Interfaces.IReview

    {
        private DataContext _context;
        public ReviewRepository(DataContext context) 
        { 
              _context = context;
        }
        public bool CreateReview(Reviews review)
        {
            _context.Reviews.Add(review);
            return Save();
        }

        public bool DeleteReview(Reviews review)
        {
            _context.Reviews.Remove(review);
            return Save();
        }

        public Reviews GetReview(int reviewId)
        {
            return _context.Reviews.Where(review => review.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Reviews> GetReviews()
        {
            return _context.Reviews.OrderBy(review => review.Id).ToList();  
        }

        public ICollection<Reviews> GetReviewsOfAMovie(int movieId)
        {
           return _context.Reviews.Where(review=>review.Movie.Id == movieId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            var review=_context.Reviews.Any(review=>review.Id == reviewId);
            return review;
        }

        public bool UpdateReview(Reviews review)
        {
            _context.Reviews.Update(review);
            return Save();
        }
        public bool Save()
        {
            var saved=_context.SaveChanges();
            return saved>0?true:false;
        }

        public bool DeleteReviews(List<Reviews> reviews)
        {
            _context.Reviews.RemoveRange(reviews);
            return Save();  
        }
    }
}
