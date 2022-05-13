using KCMS.Domain.Advertising;
using KCMS.Domain.Article;
using KCMS.Domain.Base;
using KCMS.Domain.Match;
using KCMS.Domain.Team;
using KCMS.Domain.User;
using KCMS.Infrastructure;
using KCMS.Infrastructure.Repositories;
using KCMS.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;

namespace KCMS.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with Scoped lifetime
            services.AddDbContext<KCMSContext>(options =>
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseMySQL(connectionString);
            }
            );

            services.AddScoped<Func<KCMSContext>>((provider) => () => provider.GetService<KCMSContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IArticleRepostiory, ArticleRepository>()
                .AddScoped<ITeamRepository, TeamRepository>()
                .AddScoped<IAdvertisingRepository, AdvertisingRepository>()
                .AddScoped<IMatchRepository, MatchRepository>()
                .AddScoped<IUserRepository, UserRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ArticleService>()
                .AddScoped<TeamService>()
                .AddScoped<AdversitingService>()
                .AddScoped<MatchService>()
                .AddScoped<UserService>()
                .AddScoped<JwtService>();
        }
        
    }
}
