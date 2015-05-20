namespace PCF.REDCap
{
    public class Record
    {
        public string PatientId { get; set; } // "record"
        public string Concept { get; set; } // "field_name"
        public string ConceptValue { get; set; } // "value"
        public string EventName { get; set; } // "redcap_event_name"
    }
}