using Ninject;

using KBZ.BLL.Transaction.IOC.Core;
using KBZ.Utils.Infrastructure.Contract;
using KBZ.Utils.Infrastructure.IOC;

namespace DGC.BLL
{
    public class BootStrapper
    {
    
        public static IKernel Initialize()
        {   
            IKernel kernel = new StandardKernel();

            ConfigureAdminModule(kernel);
            return kernel;
        }

   
        private static IKernel ConfigureAdminModule(IKernel kernel)
        {
            IDependencyInjector injector = new AdminDependencyInjector();
            injector.Inject(kernel);
            return kernel;
        }
    }
}
