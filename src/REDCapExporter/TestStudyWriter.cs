using System.Threading.Tasks;

namespace REDCapExporter
{
    internal class TestStudyWriter : IStudyWriter
    {
        public async Task Write(string study)
        {
            await Task.FromResult(study);
        }
    }
}