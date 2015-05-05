using System.Threading.Tasks;

namespace REDCapExporter
{
    internal class TestStudyWriter : IStudyWriter
    {
        public async Task Write(REDCapClient.REDCapStudy study)
        {
            await Task.FromResult(study);
        }
    }
}