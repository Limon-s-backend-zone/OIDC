namespace Movie.API.Data;

public class SeedMovieData
{
    public static async Task SeedAsync(MovieDbContext moviesContext)
    {
        if (!moviesContext.Movies.Any())
        {
            var movies = new List<Domains.Movie>
            {
                new()
                {
                    Genre = "Drama",
                    Title = "The Shawshank Redemption",
                    Rating = "9.3",
                    ImageUrl = "images/src",
                    ReleaseDate = new DateTime(1994, 5, 5),
                    Owner = "alice"
                },
                new()
                {
                    Genre = "Crime",
                    Title = "The Godfather",
                    Rating = "9.2",
                    ImageUrl = "images/src",
                    ReleaseDate = new DateTime(1972, 5, 5),
                    Owner = "alice"
                },
                new()
                {
                    Genre = "Action",
                    Title = "The Dark Knight",
                    Rating = "9.1",
                    ImageUrl = "images/src",
                    ReleaseDate = new DateTime(2008, 5, 5),
                    Owner = "bob"
                },
                new()
                {
                    Genre = "Crime",
                    Title = "12 Angry Men",
                    Rating = "8.9",
                    ImageUrl = "images/src",
                    ReleaseDate = new DateTime(1957, 5, 5),
                    Owner = "bob"
                },
                new()
                {
                    Genre = "Biography",
                    Title = "Schindler's List",
                    Rating = "8.9",
                    ImageUrl = "images/src",
                    ReleaseDate = new DateTime(1993, 5, 5),
                    Owner = "alice"
                },
                new()
                {
                    Genre = "Drama",
                    Title = "Pulp Fiction",
                    Rating = "8.9",
                    ImageUrl = "images/src",
                    ReleaseDate = new DateTime(1994, 5, 5),
                    Owner = "alice"
                },
                new()
                {
                    Genre = "Drama",
                    Title = "Fight Club",
                    Rating = "8.8",
                    ImageUrl = "images/src",
                    ReleaseDate = new DateTime(1999, 5, 5),
                    Owner = "bob"
                },
                new()
                {
                    Genre = "Romance",
                    Title = "Forrest Gump",
                    Rating = "8.8",
                    ImageUrl = "images/src",
                    ReleaseDate = new DateTime(1994, 5, 5),
                    Owner = "bob"
                }
            };
            moviesContext.Movies.AddRange(movies);
            await moviesContext.SaveChangesAsync();
        }
    }
}