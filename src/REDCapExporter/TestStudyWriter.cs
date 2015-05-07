using System.Threading.Tasks;

namespace PCF.REDCap
{
    internal class TestStudyWriter : IStudyWriter
    {
        public async Task Write(string study)
        {
            await Task.FromResult(study);
        }
    }
}