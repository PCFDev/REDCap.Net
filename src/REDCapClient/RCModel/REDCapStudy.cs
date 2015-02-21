using System.Collections.Generic;

namespace REDCapClient
{
    public class REDCapStudy
    {
        private List<Event> _events;
        private List<Metadata> _metadata;
        private string _studyName;

        public string StudyName
        {
            get { return _studyName; }
            set { _studyName = value; }
        }

        public List<Event> Events
        {
            get
            {
                if (this._events == null)
                {
                    this._events = new List<Event>();
                }

                return _events;
            }
            set { _events = value; }
        }

        public List<Metadata> Metadata
        {
            get
            {
                if (this._metadata == null)
                {
                    this._metadata = new List<Metadata>();
                }
                return _metadata;
            }
            set { _metadata = value; }
        }
    }
}