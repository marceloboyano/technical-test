using DemoApi.DTOs.Responses;
using DemoApi.Models;

namespace DemoApi.Repositories.Interfaces
{
    public interface IGenresRepository: IGenericRepository<Genre>
    {
        Task<IEnumerable<GenreSummaryResponseDto>> GetSongCountsPerGenreAsync();
    }
}
