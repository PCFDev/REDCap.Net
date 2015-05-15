﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace PCF.REDCap
{
    public class REDCapClient : IREDCapClient
    {
        private Uri _baseUri = new Uri("http://www.wustl.edu");
        private string _apiKey = string.Empty;

        private IParser _parser = new XMLParser();
        private IProjectConfiguration _config;

        // Query string constants
        protected const string PARAMS_GETEVENT = "token={0}&content=event&format={1}";
        protected const string PARAMS_GETARMS = "token={0}&content=arm&format={1}";
        protected const string PARAMS_GETINSTRUMENTS = "token={0}&content=instrument&format={1}";
        protected const string PARAMS_GETMETADATA = "token={0}&content=metadata&format={1}";
        protected const string PARAMS_GETUSERS = "token={0}&content=user&format={1}";
        protected const string PARAMS_GETINSTUMENTEVENTMAP = "token={0}&content=formEventMapping&format={1}";
        protected const string PARAMS_GETEXPORTFIELDNAMES = "token={0}&content=exportFieldNames&format={1}";
        private const string FORMAT_TOKEN = "xml";

        // ----------------------


        public async Task Initialize(IProjectConfiguration config)
        {
            this._apiKey = config.ApiKey;
            this._baseUri = new Uri(config.ApiUrl);
            this._config = config;
        }

        /// <summary>
        /// Creates a fully loaded study object.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<Study> GetStudyAsync(IProjectConfiguration project)
        {
            await this.Initialize(project);

            var study = new Study();
            study.ApiKey = project.ApiKey;
            study.StudyName = project.Name;


            // Each item in metadata will be assigned to an instrument
            // Each item will contain data about that item (radio selection, checkbox values, etc.)
            study.Metadata = await this.GetMetadataAsync();

            // Multi-value fields have different names than the parent field, those are in this file
            var exportFieldNames = await this.GetExportFieldNamesAsync(); 

            foreach (var item in study.Metadata)
            {
                item.ExportFieldNames = exportFieldNames
                    .Where(f => f.OriginalFieldName == item.FieldName).ToList();
            }
          

            // A study may have multiple arms, arm information is in this file
            study.Arms = await this.GetArmsAsync();

            // An event has a particular arm and can have multiple instruments used and
            // A particular instrument can be listed in multiple events
            study.Events = await this.GetEventsAsync();

            // Each instrument is a "table"
            var forms = await this.GetInstrumentsAsync();

            // This file lists each event in the study and the list of instruments used in that event
            var mappings = await this.GetInsturmentEventMapAsync();  //TODO: what do I do with this?


            foreach (var item in study.Events)
            {

                item.Arm = study.Arms
                    .Where(a => a.Key == item.ArmNumber)
                    .Select(a => new Arm() { ArmNumber = a.Key, Name = a.Value })
                    .FirstOrDefault();

                //var mapping = mappings.FirstOrDefault(m => m.ArmNumber == item.ArmNumber);

                //var formNames = mapping.EventInstruments[item.UniqueEventName];


               var formNames = mappings.FirstOrDefault(m => m.ArmNumber == item.ArmNumber)
                    .EventInstruments.Where(ei => ei.Key == item.UniqueEventName)
                    .SelectMany(ei => ei.Value);

                item.Instruments = forms.Where(f => formNames.Contains(f.InstrumentName)).ToList();
            }


            // The user list for this study
            study.Users = await this.GetUsersAsync();

            return study;
        }


        public async Task<IDictionary<string, string>> GetArmsAsync()
        {
            var xml = await GetXml(string.Format(PARAMS_GETARMS, _apiKey, FORMAT_TOKEN));

            return this._parser.HydrateArms(xml);

        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            var xml = await GetXml(string.Format(PARAMS_GETEVENT, _apiKey, FORMAT_TOKEN));

            return this._parser.HydrateEvent(xml);

        }

        public async Task<IEnumerable<Instrument>> GetInstrumentsAsync()
        {
            var xml = await GetXml(string.Format(PARAMS_GETINSTRUMENTS, _apiKey, FORMAT_TOKEN));

            return this._parser.HydrateInstrument(xml);
        }

        public async Task<IEnumerable<Metadata>> GetMetadataAsync()
        {
            var xml = await GetXml(string.Format(PARAMS_GETMETADATA, _apiKey, FORMAT_TOKEN));

            return this._parser.HydrateMetadata(xml);
        }


        public async Task<IEnumerable<ExportFieldNames>> GetExportFieldNamesAsync()
        {
            var xml = await GetXml(string.Format(PARAMS_GETEXPORTFIELDNAMES, _apiKey, FORMAT_TOKEN));

            return this._parser.HydrateExportFieldNames(xml);
        }

        public async Task<IEnumerable<InstrumentEventMapping>> GetInsturmentEventMapAsync()
        {
            var data = await GetXml(string.Format(PARAMS_GETINSTUMENTEVENTMAP, _apiKey, FORMAT_TOKEN));

            return this._parser.HydrateInstrumentEvents(data);
        }


        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var xml = await GetXml(string.Format(PARAMS_GETUSERS, _apiKey, FORMAT_TOKEN));

            return this._parser.HydrateUsers(xml);
        }

        protected virtual async Task<string> GetXml(string url)
        {
            using (var _client = new HttpClient())
            {
                var request = new StringContent(url);

                _client.BaseAddress = _baseUri;
                request.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                var response = await _client.PostAsync("", request);
                var data = await response.Content.ReadAsStringAsync();

                return data;
            }
        }

        //public async Task<XDocument> GetInstrumentEventMappingAsXmlAsync()
        //{
        //    return await GetXml(string.Format(PARAMS_GETINSTUMENTEVENTMAP, _token, "xml"));
        //}

        //public async Task<XDocument> GetExportFieldNamesAsXmlAsync()
        //{
        //    return await GetXml(string.Format(PARAMS_GETEXPORTFIELDNAMES, _token, "xml"));
        //}
        //public Task<XDocument> GetFormDataAsXmlAsync(string formName)
        //{
        //    throw new NotImplementedException();
        //}

    }
}