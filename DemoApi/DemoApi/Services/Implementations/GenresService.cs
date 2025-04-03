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
        public async Task<IEnumerable<GenreResponseDto>> GetAllGenresAsync()
        {
            var genres = await _repo.GetAllAsync("Name ASC");
            return _mapper.Map<IEnumerable<GenreResponseDto>>(genres);
        }
        public async Task<IEnumerable<GenreSummaryResponseDto>> GetSongCountsPerGenreAsync()
        {
            return await _repo.GetSongCountsPerGenreAsync();
        }
    }
}
