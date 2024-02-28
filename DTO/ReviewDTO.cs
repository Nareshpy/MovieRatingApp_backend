namespace MovieRatingApp.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int reviewerId { get; set; }
        public int movieId { get; set; }
    }
}
