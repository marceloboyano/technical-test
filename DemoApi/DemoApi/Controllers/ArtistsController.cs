using DemoApi.DTOs.Responses;
using DemoApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/v1/artists")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistsService _artistService;

        public ArtistsController(IArtistsService artistService)
        {
            _artistService = artistService;
        }

        /// <summary>
        /// Gets all available artists.
        /// </summary>
        /// <returns>Standardized API response with list of artists</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ArtistResponseDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<IEnumerable<ArtistResponseDto>>>> GetAllArtists(CancellationToken cancellationToken)
        {
            var artists = await _artistService.GetAllArtistsAsync(cancellationToken);
            return Ok(new ApiResponse<IEnumerable<ArtistResponseDto>>(artists));
        }
    }
}
