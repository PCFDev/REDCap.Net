using System;
using System.Collections.Generic;

namespace REDCapClient
{
    [Obsolete]
    public class FormMetadata
    {
        public FormMetadata()
        {
            this._fieldData = new List<Metadata>();
        }

        private List<Metadata> _fieldData;

        public string FormName { get; set; }
        public string FormLabel { get; set; } // Key
        public string EventId { get; set; } // Foreign Key to Event

        public virtual List<Metadata> FieldData
        {
            get { return _fieldData; }
            set { _fieldData = value; }
        }
        
        public override string ToString()
        {
            return FormName;
        }
    }
}