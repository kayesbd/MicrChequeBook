using KBZ.DAL.BankAdmin.Repository;
using Ninject.Modules;

namespace KBZ.BLL.Transaction.IOC.Core
{
	public partial class TransactionRepositoryModule : NinjectModule
	{
		public override void Load()
        {
			Bind<IUserInformationRepository>().To<UserInformationRepository>();
			
		}
	}
}

