﻿using System.Collections.Generic;

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
                    ArmFileName = @"TestFiles\Fructose_Arms.xml",
                    ExportFieldNamesFileName = @"TestFiles\Fructose_ExportFieldNames.xml",
                    InstrumentFileName = @"TestFiles\Fructose_Forms.xml",
                    InstrumentEventMappingFileName = @"TestFiles\Fructose_Mapping.xml",
                    MetadataFileName = @"TestFiles\Fructose_Metadata.xml"
                }
            };
        }
    }
}