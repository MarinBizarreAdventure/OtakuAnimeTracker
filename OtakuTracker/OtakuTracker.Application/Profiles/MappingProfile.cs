using AutoMapper;
using OtakuTracker.Application.AnimeLists.Commands;
using OtakuTracker.Application.AnimeLists.Responses;
using OtakuTracker.Application.Animes.Create;
using OtakuTracker.Application.Animes.Records;
using OtakuTracker.Application.Animes.Responses;
using OtakuTracker.Application.Reviews.Commands;
using OtakuTracker.Application.Reviews.Responses;
using OtakuTracker.Application.Users.Commands;
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
            CreateMap<UpdateUser, User>();
            CreateMap<CreateReview, Review>();
            CreateMap<UpdateReview, Review>();
            
            CreateMap<AnimeList, AnimeListDto>()
                .ReverseMap();

            CreateMap<CreateAnimeList, AnimeListDto>();
            CreateMap<CreateAnimeList, AnimeList>();
        }
    
    }
}
