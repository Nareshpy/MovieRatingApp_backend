using MovieRatingApp.Models;

namespace MovieRatingApp.Interfaces
{
    public interface IReviewer
    {
      ICollection<Reviewer> GetReviewers();
      Reviewer GetReviewer(int reviewerId);
      ICollection<Reviews> GetReviewsByReviewer(int reviewerId);
      bool CreateReviewer(Reviewer reviewer);
      bool UpdateReviewer(Reviewer reviewer);
      bool DeleteReviewer(int reviewerId);
      bool ReviewerExists(int reviewerId);
      bool Save();

    }
}
