using AutoMapper;
using DemoApi.DTOs.Responses;
using DemoApi.Models;

namespace DemoApi.Mappings
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Genre, GenreResponseDto>().ReverseMap();
            CreateMap<Artist, ArtistResponseDto>().ReverseMap();
            CreateMap<Track, TrackResponseDto>().ReverseMap();
        }
    }
}
