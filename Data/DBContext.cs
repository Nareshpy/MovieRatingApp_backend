using Microsoft.EntityFrameworkCore;
using MovieRatingApp.Models;

namespace MovieRatingApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }    
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
    }
}
