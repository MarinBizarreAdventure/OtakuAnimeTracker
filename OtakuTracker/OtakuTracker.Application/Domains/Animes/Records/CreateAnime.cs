using MediatR;
using OtakuTracker.Application.Animes.Responses;
using OtakuTracker.Domain.Models;
using System.ComponentModel.DataAnnotations;


namespace OtakuTracker.Application.Animes.Records;


public record CreateAnime(
    [Required(ErrorMessage = "Name is required")]
    string Name,
    
    [StringLength(100, ErrorMessage = "EnglishName length must be between 0 and 100")]
    string? EnglishName,
    
    [StringLength(100, ErrorMessage = "JapaneseName length must be between 0 and 100")]
    string? JapaneseName,
    
    [Url(ErrorMessage = "Invalid URL format for ImageUrl")]
    string? ImageUrl,
    
    [StringLength(50, ErrorMessage = "Type length must be between 0 and 50")]
    string? Type,
    
    [Range(0, int.MaxValue, ErrorMessage = "Episodes must be a non-negative integer")]
    int? Episodes,
    
    [StringLength(100, ErrorMessage = "Aired length must be between 0 and 100")]
    string? Aired,
    
    [StringLength(100, ErrorMessage = "Premiered length must be between 0 and 100")]
    string? Premiered,
    
    [StringLength(100, ErrorMessage = "Producers length must be between 0 and 100")]
    string? Producers,
    
    [StringLength(100, ErrorMessage = "Licensors length must be between 0 and 100")]
    string? Licensors,
    
    [StringLength(100, ErrorMessage = "Studios length must be between 0 and 100")]
    string? Studios,
    
    [StringLength(100, ErrorMessage = "Source length must be between 0 and 100")]
    string? Source,
    
    [StringLength(100, ErrorMessage = "Duration length must be between 0 and 100")]
    string? Duration,
    
    [StringLength(1000, ErrorMessage = "Synopsis length must be between 0 and 1000")]
    string? Synopsis,
    
    [StringLength(50, ErrorMessage = "Rating length must be between 0 and 50")]
    string? Rating,
    
    [Range(0, int.MaxValue, ErrorMessage = "Ranked must be a non-negative integer")]
    int? Ranked,
    
    [Range(0, int.MaxValue, ErrorMessage = "Popularity must be a non-negative integer")]
    int? Popularity,
    
    [Range(0, int.MaxValue, ErrorMessage = "Members must be a non-negative integer")]
    int? Members,
    
    [Range(0, int.MaxValue, ErrorMessage = "Favorites must be a non-negative integer")]
    int? Favorites,
    
    [Range(0, int.MaxValue, ErrorMessage = "Watching must be a non-negative integer")]
    int? Watching,
    
    [Range(0, int.MaxValue, ErrorMessage = "Completed must be a non-negative integer")]
    int? Completed,
    
    [Range(0, int.MaxValue, ErrorMessage = "OnHold must be a non-negative integer")]
    int? OnHold,
    
    [Range(0, int.MaxValue, ErrorMessage = "Dropped must be a non-negative integer")]
    int? Dropped,
    
    [Range(0, int.MaxValue, ErrorMessage = "PlanToWatch must be a non-negative integer")]
    int? PlanToWatch,
    
    [Range(0, int.MaxValue, ErrorMessage = "Score10 must be a non-negative integer")]
    int? Score10,
    
    [Range(0, int.MaxValue, ErrorMessage = "Score9 must be a non-negative integer")]
    int? Score9,
    
    [Range(0, int.MaxValue, ErrorMessage = "Score8 must be a non-negative integer")]
    int? Score8,
    
    [Range(0, int.MaxValue, ErrorMessage = "Score7 must be a non-negative integer")]
    int? Score7,
    
    [Range(0, int.MaxValue, ErrorMessage = "Score6 must be a non-negative integer")]
    int? Score6,
    
    [Range(0, int.MaxValue, ErrorMessage = "Score5 must be a non-negative integer")]
    int? Score5,
    
    [Range(0, int.MaxValue, ErrorMessage = "Score4 must be a non-negative integer")]
    int? Score4,
    
    [Range(0, int.MaxValue, ErrorMessage = "Score3 must be a non-negative integer")]
    int? Score3,
    
    [Range(0, int.MaxValue, ErrorMessage = "Score2 must be a non-negative integer")]
    int? Score2,
    
    [Range(0, int.MaxValue, ErrorMessage = "Score1 must be a non-negative integer")]
    int? Score1
) : IRequest<AnimeDto>;

