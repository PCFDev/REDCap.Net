using System.Threading.Tasks;
using PCF.OdmXml;

namespace PCF.REDCap
{
    public interface IOdmXmlConverter
    {
        Task<ODM> ConvertAsync(Study study);
    }
}