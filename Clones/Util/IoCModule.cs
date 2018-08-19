using Application.Data.UnitsOfWork;
using Application.Management;
using Application.Management.Interfaces;
using Application.Services;
using Application.Services.Interfaces;
using Infrastructure.Data.UnitsOfWork;
using Ninject.Modules;

namespace AgeOfClones.Util
{
    public class ModuleIoC : NinjectModule
    {
        private readonly string connectionString = "DefaultConnection";
        //private readonly string connectionString = "AgeOfClonesConnection";

        public override void Load()
        {
            Bind<IProfileService>().To<ProfileService>();
            Bind<IProfileManagementService>().To<ProfileManagementService>();
            Bind<IStockManagementService>().To<StockManagementService>();
            Bind<IResourceManagementService>().To<ResourceManagementService>();

            Bind<IProfileUnitOfWork>().To<ProfileUnitOfWork>().WithConstructorArgument(connectionString);
            Bind<IStockUnitOfWork>().To<StockUnitOfWork>().WithConstructorArgument(connectionString);
            Bind<IResourceUnitOfWork>().To<ResourceUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}