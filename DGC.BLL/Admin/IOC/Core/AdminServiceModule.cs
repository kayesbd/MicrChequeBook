using KBZ.BLL.Security;
using Ninject.Modules;

namespace KBZ.BLL.Transaction.IOC.Core 
{
	public partial class TransactionServiceModule : NinjectModule
	{
		public override void Load()
        {

            Bind<IUserInformationService>().To<UserInformationService>();
            //Bind<IBankApprovalStatusService>().To<BankApprovalStatusService>();
            //Bind<IPasswordProcessStatusService>().To<PasswordProcessStatusService>();
        }
	}
}

