using AutoMapper;
using OtakuTracker.Application.Animes.Create;
using OtakuTracker.Application.Animes.Responses;
using OtakuTracker.Domain.Models;

namespace OtakuTracker.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Anime, AnimeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.Synopsis))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Episodes, opt => opt.MapFrom(src => src.Episodes))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
            .ForMember(dest => dest.AgeRating, opt => opt.MapFrom(src => src.AgeRating))
            .ForMember(dest => dest.PosterImageUrl, opt => opt.MapFrom(src => src.PosterImageUrl))
            .ForMember(dest => dest.TrailerUrl, opt => opt.MapFrom(src => src.TrailerUrl))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating))
            .ForMember(dest => dest.TotalRatings, opt => opt.MapFrom(src => src.TotalRatings))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
            .ForMember(dest => dest.Themes, opt => opt.MapFrom(src => src.Themes));

            CreateMap<AnimeDto, Anime>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.Synopsis))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Episodes, opt => opt.MapFrom(src => src.Episodes))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.AgeRating, opt => opt.MapFrom(src => src.AgeRating))
                .ForMember(dest => dest.PosterImageUrl, opt => opt.MapFrom(src => src.PosterImageUrl))
                .ForMember(dest => dest.TrailerUrl, opt => opt.MapFrom(src => src.TrailerUrl))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating))
                .ForMember(dest => dest.TotalRatings, opt => opt.MapFrom(src => src.TotalRatings))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
                .ForMember(dest => dest.Themes, opt => opt.MapFrom(src => src.Themes));

            CreateMap<CreateAnime, Anime>()
              .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
              .ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.Synopsis))
              .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
              .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
              .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
              .ForMember(dest => dest.Episodes, opt => opt.MapFrom(src => src.Episodes))
              .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
              .ForMember(dest => dest.AgeRating, opt => opt.MapFrom(src => src.AgeRating))
              .ForMember(dest => dest.PosterImageUrl, opt => opt.MapFrom(src => src.PosterImageUrl))
              .ForMember(dest => dest.TrailerUrl, opt => opt.MapFrom(src => src.TrailerUrl))
              .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating))
              .ForMember(dest => dest.TotalRatings, opt => opt.MapFrom(src => src.TotalRatings))
              .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
              .ForMember(dest => dest.Themes, opt => opt.MapFrom(src => src.Themes));
        }
    
    }
}
