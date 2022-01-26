using System.Web.Http.Dependencies;
using Ninject;

namespace KBZ.Web
{
    public class NinjectResolver : NinjectScope, System.Web.Http.Dependencies.IDependencyResolver
    {
        private IKernel _kernel;
        public NinjectResolver(IKernel kernel)
            : base(kernel)
        {
            _kernel = kernel;
        }
        public IDependencyScope BeginScope()
        {
            return new NinjectScope(_kernel.BeginBlock());
        }
    }
}