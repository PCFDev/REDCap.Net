using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace PCF.REDCap
{
    public interface IREDCapClient
    {
        Study Study
        {
            get;
            set;
        }

        Task Initialize(string apiKey, string apiUri);
        Task Initialize(string armFileName,
            string eventFileName,
            string exportFiledNamesFileName,
            string instrumentFileName,
            string instrumentEventMappingFileName,
            string metadataFileName,
            string userFileName);
        Task<XDocument> GetArmsAsXmlAsync();
        Task<Dictionary<string, string>> GetArmsAsync();
        Task<XDocument> GetEventsAsXmlAsync();
        Task<XDocument> GetUsersAsXmlAsync();
        Task<List<Event>> GetEventsAsync();
        Task<List<ExportFieldNames>> GetExportFieldNamesAsync();
        Task<XDocument> GetFormDataAsXmlAsync();
        Task<XDocument> GetInstrumentEventMappingAsXmlAsync();
        Task<List<Instrument>> GetFormEventMapAsync();
        Task<List<FormMetadata>> GetFormMetadataAsync();
        Task<XDocument> GetInstrumentsAsXmlAsync();
        Task<List<Instrument>> GetInstrumentsAsync();
        Task<XDocument> GetMetadataAsXmlAsync();
        Task<XDocument> GetExportFieldNamesAsXmlAsync();
        Task<List<Metadata>> GetMetadataAsync();
        Task<XDocument> GetRecordsAsXmlAsync(string eventName, string[] formNames);
        Task<XDocument> GetRecordsAsXmlAsync(string eventName, string formName);
        Task<XDocument> GetRecordsAsync(string eventName, string[] formNames);
        Task<XDocument> GetRecordsAsync(string eventName, string formName);
        Task<XDocument> GetReportAsXmlAsync(string reportId);
        Task<string> TestRecords();
    }
}