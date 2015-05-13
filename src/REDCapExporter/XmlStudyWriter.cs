using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCF.OdmXml;

namespace PCF.REDCap
{
    public class XmlStudyWriter : IStudyWriter
    {
        public async Task Write(ODM study)
        {
            await Task.Run(() =>study.Save("test.xml"));
        }
    }
}
