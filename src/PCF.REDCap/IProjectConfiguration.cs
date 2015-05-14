using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCF.REDCap
{
    public interface IProjectConfiguration
    {
        string Name { get; set; }
        string ApiKey { get; set; }
        string ApiUrl { get; set; }

    }
}
