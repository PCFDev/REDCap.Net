using System.Collections.Generic;

namespace REDCapClient
{
    public class REDCapStudy
    {
        public REDCapStudy()
        {
            this._events = new List<Event>();
            this._metadata = new List<Metadata>();
        }

        private ICollection<Event> _events;
        private ICollection<Metadata> _metadata;

        public string ApiKey { get; set; } // Key
        public string StudyName { get; set; }

        public virtual ICollection<Event> Events
        {
            get { return this._events; }
            set { this._events = value; }
        }

        public virtual ICollection<Metadata> Metadata
        {
            get { return this._metadata; }
            set { this._metadata = value; }
        }
    }
}