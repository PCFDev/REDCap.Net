using System.Collections.Generic;

namespace REDCapClient
{
    public class REDCapStudy
    {
        public REDCapStudy()
        {
            this._events = new List<Event>();
            this._metadata = new List<Metadata>();
            this._forms = new List<Form>();
            this._arms = new Dictionary<string, string>();
        }

        private ICollection<Event> _events;
        private List<Metadata> _metadata;
        private ICollection<Form> _forms;
        private Dictionary<string, string> _arms;

        public string ApiKey { get; set; } // Key
        public string StudyName { get; set; }

        public virtual ICollection<Event> Events
        {
            get { return this._events; }
            set { this._events = value; }
        }

        public virtual List<Metadata> Metadata
        {
            get { return this._metadata; }
            set { this._metadata = value; }
        }

        public virtual ICollection<Form> Forms
        {
            get { return this._forms; }
            set { this._forms = value; }
        }

        public virtual Dictionary<string,string> Arms
        {
            get { return this._arms; }
            set { this._arms = value; }
        }
    }
}