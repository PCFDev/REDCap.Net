namespace REDCapExporter
{
    public class ProjectConfiguration
    {
        // Web API fields
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }

        // File system fields
        public string InstrumentFileName { get; set; }
        public string ArmFileName { get; set; }
        public string EventFileName { get; set; }
        public string ExportFieldNamesFileName { get; set; }
        public string InstrumentEventMappingFileName { get; set; }
        public string MetadataFileName { get; set; }
        public string UserFileName { get; set; }
    }
}