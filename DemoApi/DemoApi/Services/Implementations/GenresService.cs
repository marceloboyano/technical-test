using AutoMapper;
using DemoApi.DTOs.Responses;
using DemoApi.Repositories.Interfaces;
using DemoApi.Services.Interfaces;

namespace DemoApi.Services.Implementations
{
    public class GenresService: IGenresService
    {
        private readonly IGenresRepository _repo;
        private readonly IMapper _mapper;

        public GenresService(IGenresRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GenreResponseDto>> GetAllGenresAsync(CancellationToken cancellationToken)
        {
            var genres = await _repo.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<GenreResponseDto>>(genres);
        }
        public async Task<IEnumerable<GenreSummaryResponseDto>> GetSongCountsPerGenreAsync(CancellationToken cancellationToken)
        {
            return await _repo.GetSongCountsPerGenreAsync(cancellationToken);
        }
    }
}
