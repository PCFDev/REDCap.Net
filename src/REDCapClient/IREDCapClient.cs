using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace REDCapClient
{
    public interface IREDCapClient
    {
        REDCapStudy Study
        {
            get;
            set;
        }

        Task Initialize(string apiKey, string apiUri);
        Task<XDocument> GetArmsAsXmlAsync();
        Task<Dictionary<string, string>> GetArmsAsync();
        Task<XDocument> GetEventsAsXmlAsync();
        Task<List<Event>> GetEventsAsync();
        Task<List<ExportFieldNames>> GetExportFieldNamesAsync();
        Task<XDocument> GetExportFieldNamesXmlAsync();
        Task<XDocument> GetFormDataAsXmlAsync();
        Task<XDocument> GetFormEventMapAsXmlAsync();
        Task<List<Instrument>> GetFormEventMapAsync();
        Task<List<FormMetadata>> GetFormMetadataAsync();
        Task<XDocument> GetFormsAsXmlAsync();
        Task<List<Instrument>> GetFormsAsync();
        Task<XDocument> GetMetadataAsXmlAsync();
        Task<List<Metadata>> GetMetadataAsync();
        Task<XDocument> GetRecordsAsXmlAsync(string eventName, string[] formNames);
        Task<XDocument> GetRecordsAsXmlAsync(string eventName, string formName);
        Task<XDocument> GetRecordsAsync(string eventName, string[] formNames);
        Task<XDocument> GetRecordsAsync(string eventName, string formName);
        Task<XDocument> GetReportAsXmlAsync(string reportId);
        Task<string> TestRecords();
    }
}