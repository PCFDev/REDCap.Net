using System.Collections.Generic;

namespace PCF.REDCap.i2b2Importer
{
    public class FileConfigProvider : IConfigProvider
    {
        public List<ProjectConfiguration> GetConfigurations()
        {
            return new List<ProjectConfiguration>()
            {
                new ProjectConfiguration() {
                    ApiKey = "Key",
                    ApiUrl = "file://TesFiles/",
                    Name = "File Project 1"
                    //EventFileName = @"TestFiles\Fructose_Events.xml",
                    //ArmFileName = @"TestFiles\Fructose_Arms.xml",
                    //ExportFieldNamesFileName = @"TestFiles\Fructose_ExportFieldNames.xml",
                    //InstrumentFileName = @"TestFiles\Fructose_Forms.xml",
                    //InstrumentEventMappingFileName = @"TestFiles\Fructose_Mapping.xml",
                    //MetadataFileName = @"TestFiles\Fructose_Metadata.xml",
                    //UserFileName = @"TestFiles\Fructose_Users.xml"
                }
            };
        }
    }
}