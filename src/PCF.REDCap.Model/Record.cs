namespace PCF.REDCap
{
    public class Record
    {
        public string PatientId { get; set; }
        public string Concept { get; set; }        
        public string ConceptValue { get; set; }
        public string InstrumentName { get; set; }
        public string EventName { get; set; }
    }
}