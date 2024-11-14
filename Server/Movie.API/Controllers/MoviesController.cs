using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.API.Data;

namespace Movie.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize("ClientIdPolicy")]
// [Authorize]
public class MoviesController : ControllerBase
{
    private readonly MovieDbContext _context;

    public MoviesController(MovieDbContext context)
    {
        _context = context;
    }

    // GET: api/Movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Domains.Movie>>> GetMovie()
    {
        return await _context.Movies.ToListAsync();
    }

    // GET: api/Movies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Domains.Movie>> GetMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie == null) return NotFound();

        return movie;
    }

    // PUT: api/Movies/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int id, Domains.Movie movie)
    {
        if (id != movie.Id) return BadRequest();

        _context.Entry(movie).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MovieExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Movies
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<Domains.Movie>> PostMovie(Domains.Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
    }

    // DELETE: api/Movies/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Domains.Movie>> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null) return NotFound();

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return movie;
    }

    private bool MovieExists(int id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }
}