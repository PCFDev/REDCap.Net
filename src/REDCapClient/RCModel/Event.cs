using System.Collections.Generic;

namespace REDCapClient
{
    public class Event
    {
        private ICollection<Form> _form;

        public string UniqueEventName { get; set; } // Key
        public string EventName { get; set; }
        public string ArmNumber { get; set; }
        public string DayOffset { get; set; }
        public string OffsetMin { get; set; }
        public string OffsetMax { get; set; }
        
        public virtual ICollection<Form> Forms
        {
            get { return this._form; }
            set { this._form = value; }
        }

        public override string ToString()
        {
            return this.EventName;
        }
    }
}