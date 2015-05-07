using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PCF.REDCap.Model;

namespace REDCapClient
{
    public class REDCapWebSource : IREDCapClient
    {
        private HttpClient _client = new HttpClient();
        private Uri _baseUri;
        private string _token = string.Empty;

        // Query string constants
        private const string PARAMS_GETEVENT = "token={0}&content=event&format={1}";
        private const string PARAMS_GETARMS = "token={0}&content=arm&format={1}";
        private const string PARAMS_GETINSTRUMENTS = "token={0}&content=instrument&format={1}";
        private const string PARAMS_GETMETADATA = "token={0}&content=metadata&format={1}";
        private const string PARAMS_GETUSERS = "token={0}&content=user&format={1}";
        private const string PARAMS_GETINSTUMENTEVENTMAP = "token={0}&content=formEventMapping&format={1}";
        private const string PARAMS_GETEXPORTFIELDNAMES = "token={0}&content=exportFieldNames&format={1}";
        // ----------------------

        public async Task Initialize(string apiKey, string apiUri)
        {
            _token = apiKey;
            _baseUri = new Uri(apiUri);
        }

        public REDCapStudy Study
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public async Task<XDocument> GetArmsAsXmlAsync()
        {
            return await GetXml(new StringContent(string.Format(PARAMS_GETARMS, _token, "xml")));
        }

        public Task<Dictionary<string, string>> GetArmsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<XDocument> GetEventsAsXmlAsync()
        {
            return await GetXml(new StringContent(string.Format(PARAMS_GETEVENT, _token, "xml")));
        }

        public Task<List<Event>> GetEventsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<XDocument> GetInstrumentsAsXmlAsync()
        {
            return await GetXml(new StringContent(string.Format(PARAMS_GETINSTRUMENTS, _token, "xml")));
        }

        public Task<List<Instrument>> GetFormsAsync()  // TODO: Need to change name to GetInstrumentsAsync
        {
            throw new NotImplementedException();
        }

        public async Task<XDocument> GetMetadataAsXmlAsync()
        {
            return await GetXml(new StringContent(string.Format(PARAMS_GETMETADATA, _token, "xml")));
        }

        public Task<List<Metadata>> GetMetadataAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<XDocument> GetUsersAsXmlAsync()
        {
            return await GetXml(new StringContent(string.Format(PARAMS_GETUSERS, _token, "xml")));
        }

        public async Task<XDocument> GetInstrumentEventMappingAsXmlAsync()
        {
            return await GetXml(new StringContent(string.Format(PARAMS_GETINSTUMENTEVENTMAP, _token, "xml")));
        }
        
        public async Task<XDocument> GetExportFieldNamesAsXmlAsync()
        {
            return await GetXml(new StringContent(string.Format(PARAMS_GETEXPORTFIELDNAMES, _token, "xml")));
        }

        public Task<List<ExportFieldNames>> GetExportFieldNamesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<XDocument> GetFormDataAsXmlAsync(string formName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Instrument>> GetFormEventMapAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<FormMetadata>> GetFormMetadataAsync()
        {
            throw new NotImplementedException();
        }
        
        public Task<XDocument> GetRecordsAsXmlAsync(string eventName, string formName)
        {
            throw new NotImplementedException();
        }

        public Task<XDocument> GetRecordsAsXmlAsync(string eventName, string[] formNames)
        {
            throw new NotImplementedException();
        }

        public Task<XDocument> GetRecordsAsync(string eventName, string formName)
        {
            throw new NotImplementedException();
        }

        public Task<XDocument> GetRecordsAsync(string eventName, string[] formNames)
        {
            throw new NotImplementedException();
        }

        public Task<XDocument> GetReportAsXmlAsync(string reportId)
        {
            throw new NotImplementedException();
        }

        public Task<string> TestRecords()
        {
            throw new NotImplementedException();
        }

        public Task<XDocument> GetFormDataAsXmlAsync()
        {
            throw new NotImplementedException();
        }

        public Task Initialize(string armFileName,
            string eventFileName,
            string exportFiledNamesFileName,
            string instrumentFileName,
            string instrumentEventMappingFileName,
            string metadataFileName,
            string userFileName)
        {
            throw new NotImplementedException();
        }


        private async Task<XDocument> GetXml(StringContent request)
        {
            using (var _client = new HttpClient())
            {
                _client.BaseAddress = _baseUri;
                request.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                var response = await _client.PostAsync("", request);
                var data = await response.Content.ReadAsStringAsync();
                var xDoc = XDocument.Parse(data);

                return xDoc;
            }
        }
    }
}