using System.Collections.Generic;

namespace PCF.REDCap
{
    public class Event
    {
        public  Event()
        {
            _instruments = new List<Instrument>();
        }

        private List<Instrument> _instruments;

        public string UniqueEventName { get; set; } // Key
        public string EventName { get; set; }
        public string ArmNumber { get; set; }
        public string DayOffset { get; set; }
        public string OffsetMin { get; set; }
        public string OffsetMax { get; set; }
        
        public virtual List<Instrument> Instruments
        {
            get { return _instruments; }
            set { _instruments = value; }
        }

        public override string ToString()
        {
            return EventName;
        }
    }
}