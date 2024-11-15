namespace Movie.UI.ApiServices;

public interface IMovieService
{
    Task<IEnumerable<Models.Movie>> GetMoviesAsync();
    Task<Models.Movie> GetMovieByIdAsync(int id);
    Task<Models.Movie> CreateMovieAsync(Models.Movie movie);
    Task<Models.Movie> UpdateMovieAsync(Models.Movie movie);
    Task DeleteMovieAsync(int id);
}