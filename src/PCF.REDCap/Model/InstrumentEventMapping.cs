using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCF.REDCap
{
    public class InstrumentEventMapping
    {
        public InstrumentEventMapping()
        {
            this.EventInstruments = new Dictionary<string, List<string>>();
        }

        public string ArmNumber { get; set; }

        public Dictionary<string, List<string>> EventInstruments { get; set; }
    }
}