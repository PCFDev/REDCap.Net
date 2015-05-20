using System.Collections.Generic;

namespace PCF.REDCap
{
    public class Study
    {
        public Study()
        {
            _events = new List<Event>();
            _metadata = new List<Metadata>();
            _arms = new Dictionary<string, string>();
            _users = new List<User>();
        }

        private ICollection<Event> _events;
        private List<Metadata> _metadata;
        private Dictionary<string, string> _arms;
        private List<User> _users;

        public string ApiKey { get; set; } // Key
        public string StudyName { get; set; }

        public virtual ICollection<Event> Events
        {
            get { return _events; }
            set { _events = value; }
        }

        public virtual List<Metadata> Metadata
        {
            get { return _metadata; }
            set { _metadata = value; }
        }

        public virtual Dictionary<string,string> Arms
        {
            get { return _arms; }
            set { _arms = value; }
        }

        public virtual List<User> Users
        {
            get { return _users; }
            set { _users = value; }
        }
    }
}