using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCF.REDCap;

namespace REDCapWeb
{
    public class Process
    {
        public Process()
        {
            this._formMetadata = new List<REDCapClient.FormMetadata>();
        }

        private List<REDCapClient.FormMetadata> _formMetadata;

        public UserStudy UserStudy { get; set; }

        public virtual List<REDCapClient.FormMetadata> FormMetadata
        {
            get { return this._formMetadata; }
            set { this._formMetadata = value; }
        }
    }
}
