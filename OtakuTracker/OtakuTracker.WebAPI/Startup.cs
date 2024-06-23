using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nest;
using OtakuTracker.Application.Abstractions;
using OtakuTracker.Application.Profiles;
using OtakuTracker.Application.Services;
using OtakuTracker.Infrastructure;
using OtakuTracker.WebAPI.Middleware;

namespace OtakuTracker.WebAPI;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        var applicationAssembly = Assembly.Load("OtakuTracker.Application");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddDbContext<OtakutrackerContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        
        var jwtSection = Configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSection["Secret"] ?? string.Empty);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidAudience = jwtSection["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        services.AddAuthorization();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        var elasticsearchUrl = Configuration["Elasticsearch:Url"];
        var settings = new ConnectionSettings(new Uri(elasticsearchUrl));
        var client = new ElasticClient(settings);
    
        services.AddSingleton<IElasticClient>(client);

        services.AddScoped<IElasticAnimeRepository, ElasticAnimeRepository>();

        services.AddSwaggerGen();

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            c.RoutePrefix = string.Empty;
        });

        app.UseMiddleware<RequestTimingMiddleware>();
        app.UseMiddleware<GlobalExceptionMiddleware>();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}