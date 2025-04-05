using DemoApi.DTOs.Responses;
using DemoApi.Models;

namespace DemoApi.Repositories.Interfaces
{
    public interface IGenresRepository: IGenericRepository<Genre>
    {
        /// <summary>
        /// Retrieves a list of genres with their corresponding song counts, ordered by most songs to least.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<GenreSummaryResponseDto>> GetSongCountsPerGenreAsync(CancellationToken cancellationToken);
    }
}
