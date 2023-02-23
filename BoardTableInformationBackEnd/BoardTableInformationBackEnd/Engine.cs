using DataLayer.DBContext;
using DataLayer.Repositories.User;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;

namespace BoardTableInformationBackEnd
{
    public static class Engine
    {
        public static void SetupDependencies(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            
        }
    }
}
