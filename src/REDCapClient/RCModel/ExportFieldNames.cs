using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REDCapClient
{
    public class ExportFieldNames
    {
        public string OriginalFieldName { get; set; }
        public string ChoiceValue { get; set; }
        public string ExportFieldName { get; set; }

        public override string ToString()
        {
            return ExportFieldName;
        }
    }
}