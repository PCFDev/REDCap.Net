using System.Collections.Generic;

namespace PCF.REDCap.i2b2Importer
{
    public class FileConfigProvider : IConfigProvider
    {
        public IEnumerable<IProjectConfiguration> GetConfigurations()
        {
            return new List<ProjectConfiguration>()
            {
                new ProjectConfiguration() {
                    ApiKey = "Key",
                    ApiUrl = "file://TesFiles/",
                    Name = "File Project 1"                   
                }
            };
        }
    }
}