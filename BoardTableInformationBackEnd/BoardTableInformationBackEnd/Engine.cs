using DataLayer.DBContext;
using DataLayer.Repositories.Address;
using DataLayer.Repositories.AditionalFiles;
using DataLayer.Repositories.BoardType;
using DataLayer.Repositories.Category;
using DataLayer.Repositories.GameBoard;
using DataLayer.Repositories.Image;
using DataLayer.Repositories.Invitation;
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
            serviceCollection.AddScoped<IBoardGameService, BoardGameService>();
            serviceCollection.AddScoped<IInvitationService, InvitationService>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IGameBoardRepository,GameBoardRepository>();
            serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
            serviceCollection.AddScoped<IBoardTypeRepository, BoardTypeRepository>();
            serviceCollection.AddScoped<IImageRepository, ImageRepository>();
            serviceCollection.AddScoped<IAditionalFilesRepository, AditionalFilesRepository>();
            serviceCollection.AddScoped<IAddressService, AddressService>();
            serviceCollection.AddScoped<IAddressRepository, AddressRepository>();
            serviceCollection.AddScoped<IInvitationRepository, InvitationRepository>();
            serviceCollection.AddScoped<ISelectListService, SelectListService>();
        }
    }
}
