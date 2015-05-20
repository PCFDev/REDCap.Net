using System;
using System.Data.Entity;
using Ninject.Modules;
using Ninject.Web.Common;

namespace PCF.REDCap.Web.Data
{
    public class DataBindings : NinjectModule
    {
        public DataBindings()
        {
        }

        public override void Load()
        {
            //Bind<OpinionatedCache.API.ICache>().To<OpinionatedCache.Caches.HttpCacheShim>().InSingletonScope();

            Bind<PCF.REDCap.Web.Data.API.Repositories.IConfigRepository>().To<PCF.REDCap.Web.Data.Repositories.ConfigRepository>().InRequestScope();

            Bind<PCF.REDCap.Web.Data.API.Repositories.IConfigRepositoryStore>().To<PCF.REDCap.Web.Data.Repositories.ConfigRepositoryStore>().InRequestScope();

            Bind<PCF.REDCap.Web.Data.API.DB.IConfig>().To<PCF.REDCap.Web.Data.DB.Config>().InTransientScope();

            Bind<PCF.REDCap.Web.Data.DB.RepositoryContext>().ToSelf().InTransientScope().Named("ReadOnly").WithConstructorArgument(false);
            Bind<PCF.REDCap.Web.Data.DB.RepositoryContext>().ToSelf().InRequestScope().Named("ReadWrite").WithConstructorArgument(true);

            //Can we inject into repositories without doing this?
            PCF.REDCap.Web.Data.Repositories.Repositories.Instance.Inject(Kernel);
        }
    }
}
