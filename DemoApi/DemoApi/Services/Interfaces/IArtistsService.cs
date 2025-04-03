using DemoApi.DTOs.Responses;

namespace DemoApi.Services.Interfaces
{
    public interface IArtistsService
    {
        Task<IEnumerable<ArtistResponseDto>> GetAllArtistsAsync();
    }
}
