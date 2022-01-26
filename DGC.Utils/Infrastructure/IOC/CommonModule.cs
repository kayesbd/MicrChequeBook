using Ninject.Modules;
using KBZ.Utils.Infrastructure.Contract;

namespace KBZ.Utils.Infrastructure.IOC
{
    public class CommonModule : NinjectModule
    {
        public override void Load()
        {
            
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IConnectionStringProvider>().To<ConnectionStringProvider>();

      
        }
    }
}
