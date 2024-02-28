namespace MovieRatingApp.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int ReviewerId { get; set; }
        public Reviewer Reviewer { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

    }
}
