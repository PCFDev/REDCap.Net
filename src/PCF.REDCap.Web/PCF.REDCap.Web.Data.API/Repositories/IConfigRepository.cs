using System;
using System.Collections.Generic;
using PCF.REDCap.Web.Data.API.DB;
using PCF.REDCap.Web.Data.API.DTO;

namespace PCF.REDCap.Web.Data.API.Repositories
{
    public interface IConfigRepository : IRepository
    {
        IConfig Add(AddConfig info);
        bool Delete(int id);
        bool Delete(IConfig config);
        IConfig Get(int id);
        IEnumerable<IConfig> Get();
        IConfig Update(int id, UpdateConfig info);
    }

    public interface IConfigRepositoryStore : IConfigRepository, IRepositoryStore
    {
    }
}
