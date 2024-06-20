using AutoMapper;
using OtakuTracker.Application.Animes.Create;
using OtakuTracker.Application.Animes.Records;
using OtakuTracker.Application.Animes.Responses;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAnime, Anime>();
            CreateMap<UpdateAnime, Anime>();
            CreateMap<Anime, AnimeDto>();
            CreateMap<GenreDto, Genre>(); // Mapping configuration for GenreDto to Genre
            CreateMap<Genre, GenreDto>();
        }
    
    }
}
