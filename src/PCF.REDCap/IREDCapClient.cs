using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace PCF.REDCap
{
    public interface IREDCapClient
    {
       
        Task Initialize(IProjectConfiguration config);


        #region REMOVED XML Functions
        //Task<XDocument> GetArmsAsXmlAsync();

        // Task<XDocument> GetEventsAsXmlAsync();

        //Task<XDocument> GetUsersAsXmlAsync();
        //Task<XDocument> GetFormDataAsXmlAsync();

        //Task<XDocument> GetInstrumentEventMappingAsXmlAsync();
        // Task<IEnumerable<FormMetadata>> GetFormMetadataAsync();

        // Task<XDocument> GetInstrumentsAsXmlAsync();
        //Task<XDocument> GetMetadataAsXmlAsync();

        //Task<XDocument> GetExportFieldNamesAsXmlAsync();



        //Task<XDocument> GetRecordsAsXmlAsync(string eventName, string[] formNames);

        //Task<XDocument> GetRecordsAsXmlAsync(string eventName, string formName);

        //Task<XDocument> GetRecordsAsync(string eventName, string[] formNames);

        //Task<XDocument> GetRecordsAsync(string eventName, string formName);

        //Task<XDocument> GetReportAsXmlAsync(string reportId);

        #endregion

        Task<IDictionary<string, string>> GetArmsAsync();

        Task<Study> GetStudyAsync(IProjectConfiguration project);


        Task<IEnumerable<User>> GetUsersAsync();


        Task<IEnumerable<Event>> GetEventsAsync();

        Task<IEnumerable<ExportFieldNames>> GetExportFieldNamesAsync();


        Task<IEnumerable<InstrumentEventMapping>> GetInsturmentEventMapAsync();


        Task<IEnumerable<Instrument>> GetInstrumentsAsync();

        Task<IEnumerable<Metadata>> GetMetadataAsync();

        Task<IEnumerable<Record>> GetRecords(IProjectConfiguration config);

  
    }
}