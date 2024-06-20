using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        app.UseEndpoints(endpoints =>
        {
            // endpoints.MapControllerRoute(
            //     name: "default",
            //     pattern: "{controller=Home}/{action=Index}/{id?}");
            //
            // endpoints.MapControllerRoute(
            //     name: "createAnime",
            //     pattern: "api/anime/create",
            //     defaults: new { controller = "Anime", action = "CreateAnime" });
            //
            // endpoints.MapControllerRoute(
            //     name: "getAnime",
            //     pattern: "api/anime/{id}",
            //     defaults: new { controller = "Anime", action = "GetAnimeById" });
            //
            // endpoints.MapControllerRoute(
            //     name: "updateAnime",
            //     pattern: "api/anime/{id}/update",
            //     defaults: new { controller = "Anime", action = "UpdateAnime" });
            //
            // endpoints.MapControllerRoute(
            //     name: "deleteAnime",
            //     pattern: "api/anime/{id}/delete",
            //     defaults: new { controller = "Anime", action = "DeleteAnime" });
            //
            // endpoints.MapControllerRoute(
            //     name: "createUser",
            //     pattern: "api/user/create",
            //     defaults: new { controller = "User", action = "CreateUser" });
            //
            // endpoints.MapControllerRoute(
            //     name: "getUser",
            //     pattern: "api/user/{id}",
            //     defaults: new { controller = "User", action = "GetUserById" });
            //
            // endpoints.MapControllerRoute(
            //     name: "updateUser",
            //     pattern: "api/user/{id}/update",
            //     defaults: new { controller = "User", action = "UpdateUser" });
            //
            // endpoints.MapControllerRoute(
            //     name: "deleteUser",
            //     pattern: "api/user/{id}/delete",
            //     defaults: new { controller = "User", action = "DeleteUser" });
            //
            // endpoints.MapControllerRoute(
            //     name: "getAllUsers",
            //     pattern: "api/user",
            //     defaults: new { controller = "User", action = "GetAllUsers" });
            //
            // endpoints.MapControllerRoute(
            //     name: "createReview",
            //     pattern: "review/create",
            //     defaults: new { controller = "Review", action = "Create" });
            //
            // endpoints.MapControllerRoute(
            //     name: "getReview",
            //     pattern: "review/{id}",
            //     defaults: new { controller = "Review", action = "Get" });
            //
            // endpoints.MapControllerRoute(
            //     name: "updateReview",
            //     pattern: "review/{id}/update",
            //     defaults: new { controller = "Review", action = "Update" });

            endpoints.MapControllers();
        });
    }
}