using MovieRatingApp.Data;
using MovieRatingApp.Interfaces;
using MovieRatingApp.Models;

namespace MovieRatingApp.Repository
{
    public class ReviewerRepository : IReviewer
    {
        private DataContext _context;
        public ReviewerRepository(DataContext context)
        {
            _context = context;  
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Reviewers.Add(reviewer);
            return Save();
        }

        public bool DeleteReviewer(int reviewerId)
        {
            _context.Reviewers.Remove(GetReviewer(reviewerId));
            return Save();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(reviewer=>reviewer.Id==reviewerId).FirstOrDefault();   
        }

        public ICollection<Reviewer> GetReviewers()
        {
           return _context.Reviewers.OrderBy(reviewer => reviewer.FirstName).ToList();
        }

        public ICollection<Reviews> GetReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(review=>review.Reviewer.Id==reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
           return _context.Reviewers.Any(reviewer=>reviewer.Id == reviewerId);
        }
        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Reviewers.Update(reviewer);
            return Save();
        }
        public bool Save()
        {
           var saved = _context.SaveChanges();
            return saved>0?true:false;
        }

       
    }
}
