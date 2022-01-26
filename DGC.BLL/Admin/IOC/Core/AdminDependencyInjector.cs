
using KBZ.BLL;
using Ninject;

using KBZ.Utils.Infrastructure.Contract;

namespace KBZ.BLL.Transaction.IOC.Core 
{
	public partial class AdminDependencyInjector : IDependencyInjector
	{
		public void Inject(object container)
        {
            var kernel = container as IKernel;
            
            kernel.Load<TransactionRepositoryModule>();
            kernel.Load<TransactionServiceModule>();

            (new AutoMapperBootstrapper()).AutoMapperBase(kernel);
         }
      
    }
}

