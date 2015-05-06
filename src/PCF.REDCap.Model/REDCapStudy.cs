﻿using System.Collections.Generic;

namespace PCF.REDCap.Model
{
    public class REDCapStudy
    {
        public REDCapStudy()
        {
            this._events = new List<Event>();
            this._metadata = new List<Metadata>();
            this._arms = new Dictionary<string, string>();
        }

        private ICollection<Event> _events;
        private List<Metadata> _metadata;
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

        public virtual Dictionary<string,string> Arms
        {
            get { return this._arms; }
            set { this._arms = value; }
        }
    }
}