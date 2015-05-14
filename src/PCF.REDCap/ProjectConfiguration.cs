namespace PCF.REDCap
{
    public class ProjectConfiguration : IProjectConfiguration
    {
        // Web API fields

        public string Name { get; set; }

        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }
    }
}