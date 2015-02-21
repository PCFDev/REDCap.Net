namespace REDCapClient
{
    public class Event
    {
        private string _uniqueEventName;
        private string _eventName;
        private string _armNumber;

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

    }
}