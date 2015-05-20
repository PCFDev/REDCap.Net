using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using PCF.REDCap.Web.Data.API.DB;
using PCF.REDCap.Web.Data.API.DTO;
using PCF.REDCap.Web.Data.API.Repositories;
using PCF.REDCap.Web.Data.DB;
using PCF.REDCap.Web.Data.Repositories.Store;

namespace PCF.REDCap.Web.Data.Repositories
{
    public class ConfigRepositoryStore : DbStore<Config, RepositoryContext>, IConfigRepositoryStore
    {
        protected override IQueryable<Config> ReadOnlySet
        {
            get
            {
                return ReadOnlyContext.Configs.AsQueryable();
            }
        }

        protected override DbSet<Config> ReadWriteSet
        {
            get
            {
                return ReadWriteContext.Configs;
            }
        }

        public IConfig Add(AddConfig info)
        {
            int configId;
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                var config = ReadWriteSet.Create();
                config.Name = info.Name;
                config.Url = info.Url;
                config.Key = info.Key;
                config.Enabled = info.Enabled;

                ReadWriteSet.Add(config);
                SaveTracked();
                configId = config.Id;
                scope.Complete();
            }

            return Get(configId);
        }

        public bool Delete(int id)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                var config = ReadWriteSet.FirstOrDefault(_ => _.Id == id);
                ReadWriteSet.Remove(config);

                SaveTracked();
                scope.Complete();
            }
            return true;
        }

        public bool Delete(IConfig config)
        {
            return Delete(config.Id);
        }

        public IConfig Get(int id)
        {
            return ReadOnlySet.FirstOrDefault(_ => _.Id == id);
        }

        public IEnumerable<IConfig> Get()
        {
            return ReadOnlySet.ToList();
        }

        //TODO: Change tracking
        public IConfig Update(int id, UpdateConfig info)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                var config = ReadWriteSet.FirstOrDefault(_ => _.Id == id);
                //info.Resolve(config);

                config.Name = info.Name;
                config.Url = info.Url;
                config.Key = info.Key;
                config.Enabled = info.Enabled;

                SaveTracked();
                scope.Complete();
            }

            return Get(id);
        }
    }

    //TODO: Cache
    public class ConfigRepository : BaseCachingStore<IConfigRepository, IConfigRepositoryStore>, IConfigRepository//TODO: Caching
    {
        public IConfig Add(AddConfig info)
        {
            return RepositoryStore.Add(info);
        }

        public bool Delete(int id)
        {
            return RepositoryStore.Delete(id);
        }

        public bool Delete(IConfig config)
        {
            return RepositoryStore.Delete(config);
        }

        public IConfig Get(int id)
        {
            return RepositoryStore.Get(id);
        }

        public IEnumerable<IConfig> Get()
        {
            return RepositoryStore.Get();
        }

        public IConfig Update(int id, UpdateConfig info)
        {
            return RepositoryStore.Update(id, info);
        }
    }
}
