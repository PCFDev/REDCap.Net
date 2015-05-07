using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCF.REDCap
{
    public class Arm
    {
        public string ArmNumber { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}