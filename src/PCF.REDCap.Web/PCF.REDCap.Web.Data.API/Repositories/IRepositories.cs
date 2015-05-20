using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCF.REDCap.Web.Data.API.Repositories
{
    public interface IRepositories
    {
        IConfigRepository Config { get; }
    }
}
