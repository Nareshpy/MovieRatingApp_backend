namespace MovieRatingApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Category { get; set; }
        public byte[] MoviePoster { get; set; }
        public ICollection<Reviews> Reviews { get; set;}
    }
}
