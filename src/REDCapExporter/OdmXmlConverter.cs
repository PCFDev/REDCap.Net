using PCF.OdmXml;
using PCF.REDCap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REDCapExporter
{
    public class OdmXmlConverter
    {

        public async Task<ODM> Convert(Study study)
        {
            var odm = new ODM();

            odm.ID = study.StudyName;           

            return odm;
        }
    }
}
