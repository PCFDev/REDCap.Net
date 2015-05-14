namespace PCF.REDCap
{
    public class ProjectConfiguration : IProjectConfiguration
    {
        // Web API fields
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }
    }
}