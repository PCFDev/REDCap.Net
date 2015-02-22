namespace REDCapClient
{
    public class Event
    {
        public string UniqueEventName { get; set; } // Key
        public string EventName { get; set; }
        public string ArmNumber { get; set; }
        public string FormName { get; set; }
        public string DayOffset { get; set; }
        public string OffsetMin { get; set; }
        public string OffsetMax { get; set; }
        
        public override string ToString()
        {
            return this.EventName;
        }
    }
}