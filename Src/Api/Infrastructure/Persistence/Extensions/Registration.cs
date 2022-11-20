using Application.Interfaces.Repositories;
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services)
        {
            services.AddDbContext<FikirPaylasimSitesiContext>(conf =>
            {
                conf.UseSqlServer(Configuration.SqlServerConnectionString, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            });

            //SeedData.SeedDataAsync(configuration).GetAwaiter().GetResult();

            services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
            services.AddScoped<IEntryCommentFavoriteRepository, EntryCommentFavoriteRepository>();
            services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();
            services.AddScoped<IEntryCommentVoteRepository, EntryCommentVoteRepository>();
            services.AddScoped<IEntryFavoriteRepository, EntryFavoriteRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IEntryTagRepository, EntryTagRepository>();
            services.AddScoped<IEntryVoteRepository, EntryVoteRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
