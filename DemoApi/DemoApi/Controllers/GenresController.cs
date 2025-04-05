using DemoApi.DTOs.Responses;
using DemoApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/v1/genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        /// <summary>
        /// Gets all available music genres
        /// </summary>
        /// <returns>Standard API response with list of all genres</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<GenreResponseDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GenreResponseDto>>> GetAllGenres(CancellationToken cancellationToken)
        {
            var genres = await _genresService.GetAllGenresAsync(cancellationToken);
            return Ok(new ApiResponse<IEnumerable<GenreResponseDto>>(genres));
        }
        /// <summary>
        /// Gets song counts grouped by genre
        /// </summary>
        /// <returns>Standard API response with list of genres with their song counts</returns>
        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<GenreSummaryResponseDto>>> GetSongCountsPerGenre(CancellationToken cancellationToken)
        {
            var result = await _genresService.GetSongCountsPerGenreAsync(cancellationToken);
            return Ok(new ApiResponse<IEnumerable<GenreSummaryResponseDto>>(result));
        }
    }

}
