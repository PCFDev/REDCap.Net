using System.Collections.Generic;

namespace REDCapExporter
{
    internal class FileConfigController : IConfigController
    {
        public List<ProjectConfiguration> GetConfigurations()
        {
            return new List<ProjectConfiguration>()
            {
                new ProjectConfiguration() {
                    EventFileName = @"TestFiles\Fructose_Events.xml",
                    ArmFileName = "",
                    ExportFieldNamesFileName = "",
                    InstrumentFileName = @"TestFiles\Fructose_Forms.xml",
                    InstrumentEventMappingFileName = @"TestFiles\Fructose_Mapping.xml",
                    MetadataFileName = @"TestFiles\Fructose_Metadata.xml"
                }
            };
        }
    }
}