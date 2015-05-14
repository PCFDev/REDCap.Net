using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace PCF.REDCap
{
    public interface IREDCapClient
    {
        //Study Study
        //{
        //    get;
        //    set;
        //}

        Task Initialize(IProjectConfiguration config);


        //Task Initialize(string apiKey, string apiUri);

        //Task Initialize(string armFileName,
        //    string eventFileName,
        //    string exportFiledNamesFileName,
        //    string instrumentFileName,
        //    string instrumentEventMappingFileName,
        //    string metadataFileName,
        //    string userFileName);

        //Task<XDocument> GetArmsAsXmlAsync();

        Task<IDictionary<string, string>> GetArmsAsync();

       // Task<XDocument> GetEventsAsXmlAsync();

        //Task<XDocument> GetUsersAsXmlAsync();

        Task<IEnumerable<User>> GetUsersAsync();


        Task<IEnumerable<Event>> GetEventsAsync();

        Task<IEnumerable<ExportFieldNames>> GetExportFieldNamesAsync();

        //Task<XDocument> GetFormDataAsXmlAsync();

        //Task<XDocument> GetInstrumentEventMappingAsXmlAsync();

        Task<IEnumerable<Instrument>> GetFormEventMapAsync();

       // Task<IEnumerable<FormMetadata>> GetFormMetadataAsync();

       // Task<XDocument> GetInstrumentsAsXmlAsync();

        Task<IEnumerable<Instrument>> GetInstrumentsAsync();

        Task<IEnumerable<Metadata>> GetMetadataAsync();
        //Task<XDocument> GetMetadataAsXmlAsync();

        //Task<XDocument> GetExportFieldNamesAsXmlAsync();

        

        //Task<XDocument> GetRecordsAsXmlAsync(string eventName, string[] formNames);

        //Task<XDocument> GetRecordsAsXmlAsync(string eventName, string formName);

        //Task<XDocument> GetRecordsAsync(string eventName, string[] formNames);

        //Task<XDocument> GetRecordsAsync(string eventName, string formName);

        //Task<XDocument> GetReportAsXmlAsync(string reportId);

        //Task<string> TestRecords();
    }
}