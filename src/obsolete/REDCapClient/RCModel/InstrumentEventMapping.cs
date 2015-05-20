using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REDCapClient.RCModel
{
    public class InstrumentEventMapping
    {
        public string ArmNumber { get; set; }

        public Dictionary<string, List<string>> EventInstruments { get; set; }
    }
}