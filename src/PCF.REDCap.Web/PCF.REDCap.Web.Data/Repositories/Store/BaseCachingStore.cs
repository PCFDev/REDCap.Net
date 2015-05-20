using Ninject;
using PCF.REDCap.Web.Data.API.Repositories;

namespace PCF.REDCap.Web.Data.Repositories.Store
{
    public class BaseCachingStore<TRepo, TBacker>
        where TBacker : TRepo, IRepositoryStore
        where TRepo : class //*interface, really*/
    {
        [Inject]
        public TBacker RepositoryStore { get; set; }

        //TODO: Cache layer
        //[Inject]
        //public ICache Cache { get; set; }
    }
}
