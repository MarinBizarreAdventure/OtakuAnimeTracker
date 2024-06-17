using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OtakuTracker.Application.Animes.Create;
using OtakuTracker.Domain.Entities;
using OtakuTracker.Infrastructure;
using OtakuTracker.Infrastructure.Repositories;
using OtakuTracker.Infrastructure.UnitOfWork;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Register DbContext
                services.AddDbContext<AnimeDbContext>(options =>
                    options.UseSqlServer("Your_Connection_String"));

                // Register repositories and unit of work
                services.AddScoped<IAnimeRepository, AnimeRepository>();
                services.AddScoped<IUnitOfWork, UnitOfWork>();

                // Register MediatR
                services.AddMediatR(typeof(CreateAnimeHandler).Assembly);
            })
            .Build();

        // Example of creating an anime
        using (var scope = host.Services.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

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

            var result = await mediator.Send(createAnimeCommand);

            Console.WriteLine($"Created Anime: {result.Title}, Rating: {result.AverageRating}");
        }
    }
}
