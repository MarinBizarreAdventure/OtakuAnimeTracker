using AutoMapper;
using OtakuTracker.Application.AnimeLists.Commands;
using OtakuTracker.Application.AnimeLists.Responses;
using OtakuTracker.Application.Animes.Create;
using OtakuTracker.Application.Animes.Records;
using OtakuTracker.Application.Animes.Responses;
using OtakuTracker.Application.Domains.AnimeGenres.Responses;
using OtakuTracker.Application.Genres.Responses;
using OtakuTracker.Application.Reviews.Commands;
using OtakuTracker.Application.Reviews.Responses;
using OtakuTracker.Application.Users.Commands;
using OtakuTracker.Application.Users.Responses;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAnime, Anime>();
            CreateMap<User, UserDto>().ReverseMap();
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
            
            CreateMap<Genre, GenreDto>()
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.GenreName))
                .ReverseMap(); 
            
            CreateMap<AnimeGenre, AnimeGenreDto>().ReverseMap();

            
        }
    
    }
}
