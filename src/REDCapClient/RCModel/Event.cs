namespace REDCapClient
{
    public class Event
    {
        private string _uniqueEventName;
        private string _eventName;
        private string _armNumber;
        private string _formName;

        public string FormName
        {
            get { return _formName; }
            set { _formName = value; }
        }
        
        public string ArmNumber
        {
            get { return _armNumber; }
            set { _armNumber = value; }
        }

        public string EventName
        {
            get { return _eventName; }
            set { _eventName = value; }
        }

        public string UniqueEventName
        {
            get { return _uniqueEventName; }
            set { _uniqueEventName = value; }
        }

        public override string ToString()
        {
            return this.EventName;
        }
    }
}