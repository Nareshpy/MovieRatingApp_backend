namespace MovieRatingApp.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Category { get; set; }
        public byte[] MoviePoster { get; set; }
    }
}
