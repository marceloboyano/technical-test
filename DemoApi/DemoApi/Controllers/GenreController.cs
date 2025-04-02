using DemoApi.Models;
using DemoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/v1/genres")]
    public class GenreController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAll()
        {
            var repository = new GenresRepository();
            var genres = await repository.GetAsync();
            return Ok(genres);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<GenreSummary>>> GetSummary()
        {
            var repository = new GenresRepository(); 
            var summary = await repository.GetSongCountsPerGenreAsync(); 
            return Ok(summary);
        }
    }

}
