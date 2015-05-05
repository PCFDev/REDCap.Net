using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REDCapExporter
{
    internal class FileConfigController : IConfigController
    {
        public List<ProjectConfiguration> GetConfigurations()
        {
            return new List<ProjectConfiguration>()
            {
                new ProjectConfiguration() {
                     ApiKey = @"TestFiles\Fructose_Events.xml",
                      ApiUrl = @"TestFiles\Fructose_Forms.xml"
                }
            };
        }
    }
}
