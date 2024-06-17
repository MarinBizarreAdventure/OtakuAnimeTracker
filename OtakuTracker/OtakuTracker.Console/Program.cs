using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Animes.Create;
using OtakuTracker.Domain.Models;
using OtakuTracker.Infrastructure;
using OtakuTracker.Infrastructure.Repositories;
using OtakuTracker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OtakuTracker.Application.Animes.Commands;
using OtakuTracker.Application.Animes.Queries;

public class Program
{
    private static IMediator _mediator;

    public static async Task Main(string[] args)
    {
        // Initialize Mediator
        _mediator = Init();

        // Add Anime
        await AddAnime();

        // Get Anime by Id
        await GetAnimeById(1);

        // Update Anime
    }

    public static IMediator Init()
    {
        var diContainer = new ServiceCollection();

        // Register DbContext
        diContainer.AddDbContext<AnimeDbContext>(options =>
            options.UseSqlServer("Your_Connection_String"));

        // Register repositories and unit of work
        diContainer.AddScoped<IAnimeRepository, AnimeRepository>();
        diContainer.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register MediatR
        diContainer.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAnimeHandler).Assembly));

        var serviceProvider = diContainer.BuildServiceProvider();
        return serviceProvider.GetRequiredService<IMediator>();
    }

    public static async Task AddAnime()
    {
        var createAnimeCommand = new CreateAnime(
            "Naruto",
            "A story about a young ninja...",
            new DateTime(2002, 10, 3),
            new DateTime(2017, 3, 23),
            "Completed",
            "TV",
            500,
            23,
            "PG-13",
            "url_to_poster",
            "url_to_trailer",
            8.2m,
            1000000,
            new List<Genre> { new Genre { GenreName = "Action" }, new Genre { GenreName = "Adventure" } },
            new List<Theme> { new Theme { ThemeName = "Martial Arts" } }
        );

        var result = await _mediator.Send(createAnimeCommand);

        Console.WriteLine($"Created Anime: {result.Title}, Rating: {result.AverageRating}");
    }
    public static async Task GetAnimeById(int animeId)
    {
        var anime = await _mediator.Send(new GetByIdAnime(animeId));
        Console.WriteLine($"Anime with ID {animeId}: {anime.Title}");
    }

   
}
