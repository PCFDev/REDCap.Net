using System.Collections.Generic;

namespace REDCapClient
{
    public class Event
    {
        public  Event()
        {
            this._forms = new List<Form>();
        }

        private List<Form> _forms;

        public string UniqueEventName { get; set; } // Key
        public string EventName { get; set; }
        public string ArmNumber { get; set; }
        public string DayOffset { get; set; }
        public string OffsetMin { get; set; }
        public string OffsetMax { get; set; }
        
        public virtual List<Form> Forms
        {
            get { return this._forms; }
            set { this._forms = value; }
        }

        public override string ToString()
        {
            return this.EventName;
        }
    }
}