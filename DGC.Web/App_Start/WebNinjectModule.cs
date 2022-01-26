using Ninject.Modules;

namespace KBZ.Web
{
    public class WebNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // UOWS
            //Bind<ILookupCacheService>()  
            //    .To<LookupCacheService>();
        }
    }
}
