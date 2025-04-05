using DemoApi.DTOs.Requests;
using DemoApi.DTOs.Responses;
using DemoApi.Repositories.Interfaces;
using DemoApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TracksController : ControllerBase
    {      
        private readonly ITracksService _tracksService;

        public TracksController(ITracksService tracksService)
        {
            _tracksService = tracksService;
        }
        /// <summary>
        /// Gets a specific track by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Standard API response with track details</returns>
        [HttpGet(Name = "{id}")]
        [ProducesResponseType(typeof(ApiResponse<TrackResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TrackResponseDto>> GetTrackById(int id,CancellationToken cancellationToken)
        {
            var track = await _tracksService.GetTrackByIdAsync(id, cancellationToken);

            return track == null
                ? NotFound(new ApiResponse<object>("Track not found", StatusCodes.Status404NotFound))
                : Ok(new ApiResponse<TrackResponseDto>(track, $"Track {id} retrieved successfully"));

        }
        /// <summary>
        /// Searches tracks with filtering and pagination
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>Paginated list of matching tracks</returns>
        [HttpGet("search")]
        public async Task<ActionResult<PagedResponse<TrackResponseDto>>> SearchTracks([FromQuery] TrackFilters filters,CancellationToken cancellationToken)
        {          
            var result = await _tracksService.SearchTracksAsync(filters, cancellationToken);
            if (result == null || result.Data == null || !result.Data.Any())
            {
                return Ok(new ApiResponse<PagedResponse<TrackSearchResultDto>>(
                    new PagedResponse<TrackSearchResultDto>(
                        new List<TrackSearchResultDto>(),
                        0,
                        filters.PageNumber,
                        filters.PageSize
                    ),
                    "No tracks found"
                ));
            }

            return Ok(new ApiResponse<PagedResponse<TrackSearchResultDto>>(result));
        }     
       
    }
}