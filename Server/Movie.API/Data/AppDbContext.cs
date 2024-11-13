using Microsoft.EntityFrameworkCore;

namespace Movie.API.Data;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    public DbSet<Domains.Movie> Movies { get; set; }
}