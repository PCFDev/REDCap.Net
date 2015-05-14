using System.Collections.Generic;
using System.Linq;

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

        private List<Event> _events;
        private List<Metadata> _metadata;
        private IDictionary<string, string> _arms;
        private List<User> _users;

        public string ApiKey { get; set; } // Key
        public string StudyName { get; set; }

        public virtual IEnumerable<Event> Events
        {
            get { return _events; }
            set { _events = value.ToList(); }
        }

        public virtual IEnumerable<Metadata> Metadata
        {
            get { return _metadata; }
            set { _metadata = value.ToList(); }
        }

        public virtual IDictionary<string,string> Arms
        {
            get { return _arms; }
            set { _arms = value; }
        }

        public virtual IEnumerable<User> Users
        {
            get { return _users; }
            set { _users = value; }
        }
    }
}