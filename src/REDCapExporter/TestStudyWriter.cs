using PCF.OdmXml;
using System.Threading.Tasks;

namespace PCF.REDCap
{
    internal class TestStudyWriter : IStudyWriter
    {
        public async Task Write(ODM study)
        {
            await Task.FromResult(study);
        }
    }
}