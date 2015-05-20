using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCF.REDCap.Web.Data.API.DTO
{
    public class UpdateConfig
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Key { get; set; }
        public bool Enabled { get; set; }
    }
}
