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
        public async Task<ActionResult<IEnumerable<GenreResponseDto>>> GetAllGenres()
        {
            var genres = await _genresService.GetAllGenresAsync();
            return Ok(new ApiResponse<IEnumerable<GenreResponseDto>>(genres));
        }
        /// <summary>
        /// Gets song counts grouped by genre
        /// </summary>
        /// <returns>Standard API response with list of genres with their song counts</returns>
        [HttpGet("summary/song-counts")]
        public async Task<ActionResult<IEnumerable<GenreSummaryResponseDto>>> GetSongCountsPerGenre()
        {
            var result = await _genresService.GetSongCountsPerGenreAsync();
            return Ok(new ApiResponse<IEnumerable<GenreSummaryResponseDto>>(result));
        }
    }

}
