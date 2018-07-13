using Application.Management.Data.UnitsOfWork;
using Application.Management.Services;
using Application.Management.Services.Interfaces;
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

            Bind<IProfileUnitOfWork>().To<ProfileUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}