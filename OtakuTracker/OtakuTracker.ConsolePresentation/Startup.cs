using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OtakuTracker.Infrastructure;
using OtakuTracker.Infrastructure.Repositories;
using OtakuTracker.Infrastructure.UnitOfWork;
using MediatR;
using OtakuTracker.Application.Animes.Create;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Register DbContext
        services.AddDbContext<AnimeDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Register repositories and unit of work
        services.AddScoped<IAnimeRepository, AnimeRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register MediatR
        services.AddMediatR(typeof(CreateAnimeHandler).Assembly);

        // Other service configurations...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline...
    }
}
