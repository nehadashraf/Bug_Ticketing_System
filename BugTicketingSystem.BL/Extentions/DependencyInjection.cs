using BugTicketingSystem.BL.Managers;
using BugTicketingSystem.BL.Mangers.Bugs;
using BugTicketingSystem.BL.Mangers.Projects;
using BugTicketingSystem.BL.Mangers.Users;
using BugTicketingSystem.DAL.Repositories.AttachmentRepository;
using BugTicketingSystem.DAL.Repositories.ProjectRepository;
using BugTicketingSystem.DAL.Repositories.UserRepository;
using Microsoft.Extensions.DependencyInjection;

namespace BugTicketingSystem.BL.Extentions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBugRepository, BugRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();

            return services;
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IBugManager, BugManager>();
            services.AddScoped<IProjectManager, ProjectManager>();

            return services;
        }
    }
}
