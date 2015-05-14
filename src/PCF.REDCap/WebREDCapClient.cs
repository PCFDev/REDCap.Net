using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace PCF.REDCap
{
    public class WebREDCapClient : IREDCapClient
    {
        private HttpClient _client = new HttpClient();
        private Uri _baseUri = new Uri("http://www.wustl.edu");
        private string _token = string.Empty;

        private XMLParser _parser = new XMLParser();

        // Query string constants
        protected const string PARAMS_GETEVENT = "token={0}&content=event&format={1}";
        protected const string PARAMS_GETARMS = "token={0}&content=arm&format={1}";
        protected const string PARAMS_GETINSTRUMENTS = "token={0}&content=instrument&format={1}";
        protected const string PARAMS_GETMETADATA = "token={0}&content=metadata&format={1}";
        protected const string PARAMS_GETUSERS = "token={0}&content=user&format={1}";
        protected const string PARAMS_GETINSTUMENTEVENTMAP = "token={0}&content=formEventMapping&format={1}";
        protected const string PARAMS_GETEXPORTFIELDNAMES = "token={0}&content=exportFieldNames&format={1}";
        // ----------------------


        public async Task Initialize(IProjectConfiguration config)
        {
            this._token = config.ApiKey;
            this._baseUri = new Uri(config.ApiUrl);
        }



        //public async Task<XDocument> GetArmsAsXmlAsync()
        //{
        //    return await GetXml(new StringContent(string.Format(PARAMS_GETARMS, _token, "xml")));
        //}

        public async Task<IDictionary<string, string>> GetArmsAsync()
        {
            var xml = await GetXml(string.Format(PARAMS_GETARMS, _token, "xml"));

            var arms = new Dictionary<string, string>();

            foreach (var item in xml.Descendants("item"))
            {
                arms.Add(item.Element("arm_num").Value.ToString(), item.Element("name").Value.ToString());
            }

            return arms;

        }

        //public async Task<XDocument> GetEventsAsXmlAsync()
        //{
        //    return await GetXml(string.Format(PARAMS_GETEVENT, _token, "xml"));
        //}

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            var xml = await GetXml(string.Format(PARAMS_GETEVENT, _token, "xml"));

            return await this._parser.HydrateEvent(xml);

        }

        //public async Task<XDocument> GetInstrumentsAsXmlAsync()
        //{
        //    return await GetXml(string.Format(PARAMS_GETINSTRUMENTS, _token, "xml"));
        //}

        public Task<IEnumerable<Instrument>> GetInstrumentsAsync()
        {
            throw new NotImplementedException();
        }

        //public async Task<XDocument> GetMetadataAsXmlAsync()
        //{
        //    return await GetXml(string.Format(PARAMS_GETMETADATA, _token, "xml"));
        //}

        public Task<IEnumerable<Metadata>> GetMetadataAsync()
        {
            throw new NotImplementedException();
        }

        //public async Task<XDocument> GetUsersAsXmlAsync()
        //{
        //    return await GetXml(string.Format(PARAMS_GETUSERS, _token, "xml"));
        //}

        //public async Task<XDocument> GetInstrumentEventMappingAsXmlAsync()
        //{
        //    return await GetXml(string.Format(PARAMS_GETINSTUMENTEVENTMAP, _token, "xml"));
        //}

        //public async Task<XDocument> GetExportFieldNamesAsXmlAsync()
        //{
        //    return await GetXml(string.Format(PARAMS_GETEXPORTFIELDNAMES, _token, "xml"));
        //}

        public Task<IEnumerable<ExportFieldNames>> GetExportFieldNamesAsync()
        {
            throw new NotImplementedException();
        }

        //public Task<XDocument> GetFormDataAsXmlAsync(string formName)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<IEnumerable<Instrument>> GetFormEventMapAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        //[Obsolete]
        //public Task<List<FormMetadata>> GetFormMetadataAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<XDocument> GetRecordsAsXmlAsync(string eventName, string formName)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<XDocument> GetRecordsAsXmlAsync(string eventName, string[] formNames)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<XDocument> GetRecordsAsync(string eventName, string formName)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<XDocument> GetRecordsAsync(string eventName, string[] formNames)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<XDocument> GetReportAsXmlAsync(string reportId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<string> TestRecords()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<XDocument> GetFormDataAsXmlAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task Initialize(string armFileName,
        //    string eventFileName,
        //    string exportFiledNamesFileName,
        //    string instrumentFileName,
        //    string instrumentEventMappingFileName,
        //    string metadataFileName,
        //    string userFileName)
        //{
        //    throw new NotImplementedException();
        //}


        protected virtual async Task<XElement> GetXml(string url)
        {
            using (var _client = new HttpClient())
            {
                var request = new StringContent(url);

                _client.BaseAddress = _baseUri;
                request.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                var response = await _client.PostAsync("", request);
                var data = await response.Content.ReadAsStringAsync();
                var xDoc = XElement.Parse(data);

                return xDoc;
            }
        }

        public async Task<Study> GetStudyAsync(IProjectConfiguration project)
        {
            var study = new Study();
            study.ApiKey = project.ApiKey;
            study.StudyName = project.Name;

            // Each instrument is a "table"
            var forms = await this.GetInstrumentsAsync();

            // Each item in metadata will be assigned to an instrument
            // Each item will contain data about that item (radio selection, checkbox values, etc.)
            study.Metadata = await this.GetMetadataAsync();

            // Multi-value fields have different names than the parent field, those are in this file
            var exportFieldNames = await this.GetExportFieldNamesAsync();

            // A study may have multiple arms, arm information is in this file
            study.Arms = await this.GetArmsAsync();

            // An event has a particular arm and can have multiple instruments used and
            // A particular instrument can be listed in multiple events
            study.Events = await this.GetEventsAsync();

            // This file lists each event in the study and the list of instruments used in that event
            var mapping = await this.GetFormEventMapAsync();

            // The user list for this study
            study.Users = await this.GetUsersAsync();

            return study;
        }
    }
}