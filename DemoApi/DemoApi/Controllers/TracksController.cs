using DemoApi.Models;
using DemoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TracksController : ControllerBase
    {
        [HttpGet(Name = "{id}")]
        public async Task<Track> GetTrackById(int id)
        {
            var repository = new TracksRepository();
            var tracks = await repository.GetById(id);
            return tracks;
        }

        [HttpGet("search")]
        public async Task<ActionResult<PagedResponse<Track>>> SearchTracks([FromQuery] TrackFilters filters)
        {
            var repository = new TracksRepository();
            var result = await repository.SearchTracksAsync(filters);
            return Ok(result);
        }     
       
    }
}