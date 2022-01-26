using Ninject;
using KBZ.Utils.Infrastructure.Contract;

namespace KBZ.Utils.Infrastructure.IOC
{
    public class DependencyInjector : IDependencyInjector
    {
        public void Inject(object container)
        {
            var kernel = container as IKernel;

            // Perform the interface matching based on convention
            kernel.Load<CommonModule>();
            
         }
    }
}
