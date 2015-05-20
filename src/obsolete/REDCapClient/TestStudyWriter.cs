using PCF.OdmXml;
using System.Threading.Tasks;

namespace PCF.REDCap
{
    public class TestStudyWriter : IStudyWriter
    {
        public async Task Write(ODM study)
        {
            await Task.FromResult(study);
        }
    }
}