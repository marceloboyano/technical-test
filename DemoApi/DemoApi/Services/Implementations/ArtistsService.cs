using AutoMapper;
using DemoApi.DTOs.Responses;
using DemoApi.Repositories.Interfaces;
using DemoApi.Services.Interfaces;

namespace DemoApi.Services.Implementations
{
    public class ArtistsService: IArtistsService
    {
        private readonly IArtistsRepository _repo;
        private readonly IMapper _mapper;
        public ArtistsService(IArtistsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ArtistResponseDto>> GetAllArtistsAsync(CancellationToken cancellationToken)
        {
            var artists = await _repo.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ArtistResponseDto>>(artists);
           
        }
    }
}
