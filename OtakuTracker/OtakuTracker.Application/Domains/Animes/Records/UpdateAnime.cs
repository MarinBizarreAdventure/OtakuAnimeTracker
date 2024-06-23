using System.ComponentModel.DataAnnotations;
using MediatR;
using OtakuTracker.Application.Animes.Responses;

namespace OtakuTracker.Application.Animes.Records;

public record UpdateAnime(
    [Range(1, int.MaxValue, ErrorMessage = "AnimeId must be a positive integer.")]
    int AnimeId,

    [StringLength(100, ErrorMessage = "Name must be at most 100 characters long.")]
    string? Name,

    [StringLength(100, ErrorMessage = "EnglishName must be at most 100 characters long.")]
    string? EnglishName,

    [StringLength(100, ErrorMessage = "JapaneseName must be at most 100 characters long.")]
    string? JapaneseName,

    [Url(ErrorMessage = "ImageUrl must be a valid URL.")]
    string? ImageUrl,

    [StringLength(50, ErrorMessage = "Type must be at most 50 characters long.")]
    string? Type,

    [Range(0, int.MaxValue, ErrorMessage = "Episodes must be a non-negative integer.")]
    int? Episodes,

    [StringLength(50, ErrorMessage = "Aired must be at most 50 characters long.")]
    string? Aired,

    [StringLength(50, ErrorMessage = "Premiered must be at most 50 characters long.")]
    string? Premiered,

    [StringLength(100, ErrorMessage = "Producers must be at most 100 characters long.")]
    string? Producers,

    [StringLength(100, ErrorMessage = "Licensors must be at most 100 characters long.")]
    string? Licensors,

    [StringLength(100, ErrorMessage = "Studios must be at most 100 characters long.")]
    string? Studios,

    [StringLength(50, ErrorMessage = "Source must be at most 50 characters long.")]
    string? Source,

    [StringLength(50, ErrorMessage = "Duration must be at most 50 characters long.")]
    string? Duration,

    [StringLength(500, ErrorMessage = "Synopsis must be at most 500 characters long.")]
    string? Synopsis,

    [StringLength(50, ErrorMessage = "Rating must be at most 50 characters long.")]
    string? Rating,

    [Range(0, int.MaxValue, ErrorMessage = "Ranked must be a non-negative integer.")]
    int? Ranked,

    [Range(0, int.MaxValue, ErrorMessage = "Popularity must be a non-negative integer.")]
    int? Popularity,

    [Range(0, int.MaxValue, ErrorMessage = "Members must be a non-negative integer.")]
    int? Members,

    [Range(0, int.MaxValue, ErrorMessage = "Favorites must be a non-negative integer.")]
    int? Favorites,

    [Range(0, int.MaxValue, ErrorMessage = "Watching must be a non-negative integer.")]
    int? Watching,

    [Range(0, int.MaxValue, ErrorMessage = "Completed must be a non-negative integer.")]
    int? Completed,

    [Range(0, int.MaxValue, ErrorMessage = "OnHold must be a non-negative integer.")]
    int? OnHold,

    [Range(0, int.MaxValue, ErrorMessage = "Dropped must be a non-negative integer.")]
    int? Dropped,

    [Range(0, int.MaxValue, ErrorMessage = "PlanToWatch must be a non-negative integer.")]
    int? PlanToWatch,

    [Range(0, int.MaxValue, ErrorMessage = "Score10 must be a non-negative integer.")]
    int? Score10,

    [Range(0, int.MaxValue, ErrorMessage = "Score9 must be a non-negative integer.")]
    int? Score9,

    [Range(0, int.MaxValue, ErrorMessage = "Score8 must be a non-negative integer.")]
    int? Score8,

    [Range(0, int.MaxValue, ErrorMessage = "Score7 must be a non-negative integer.")]
    int? Score7,

    [Range(0, int.MaxValue, ErrorMessage = "Score6 must be a non-negative integer.")]
    int? Score6,

    [Range(0, int.MaxValue, ErrorMessage = "Score5 must be a non-negative integer.")]
    int? Score5,

    [Range(0, int.MaxValue, ErrorMessage = "Score4 must be a non-negative integer.")]
    int? Score4,

    [Range(0, int.MaxValue, ErrorMessage = "Score3 must be a non-negative integer.")]
    int? Score3,

    [Range(0, int.MaxValue, ErrorMessage = "Score2 must be a non-negative integer.")]
    int? Score2,

    [Range(0, int.MaxValue, ErrorMessage = "Score1 must be a non-negative integer.")]
    int? Score1,

    [MinLength(1, ErrorMessage = "At least one genre must be specified.")]
    List<int>? Genres
) : IRequest<AnimeDto>;