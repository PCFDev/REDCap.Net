using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PCF.REDCap
{
    public interface IParser
    {
        IDictionary<string, string> HydrateArms(string data);

        IEnumerable<Event> HydrateEvent(string data);
        IEnumerable<Instrument> HydrateForms(string data);
        IEnumerable<InstrumentEventMapping> HydrateInstrumentEvents(string data);
        IEnumerable<Metadata> HydrateMetadata(string data);

        Metadata HydrateMetadataFields(string data);

        IEnumerable<Record> HydrateRecords(string data);
        IEnumerable<User> HydrateUsers(string data);
        IEnumerable<ExportFieldNames> HydrateExportFieldNames(string xml);
    }
}