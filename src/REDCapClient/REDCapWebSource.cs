﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace REDCapClient
{
    public class REDCapWebSource : IREDCapClient
    {
        public REDCapStudy Study
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private static string _eventFileName;
        private static string _instrumentFileName;

        public async Task Initialize(string apiKey, string apiUri)
        {
            _eventFileName = apiKey;
            _instrumentFileName = apiUri;
        }

        public Task<XDocument> GetArmsAsXmlAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, string>> GetArmsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<XDocument> GetEventsAsXmlAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Event>> GetEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ExportFieldNames>> GetExportFieldNamesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<XDocument> GetExportFieldNamesXmlAsync()
        {
            throw new NotImplementedException();
        }

        public Task<XDocument> GetFormDataAsXmlAsync(string formName)
        {
            throw new NotImplementedException();
        }

        public Task<XDocument> GetFormEventMapAsXmlAsync()
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

        public Task<XDocument> GetFormsAsXmlAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Instrument>> GetFormsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<XDocument> GetMetadataAsXmlAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Metadata>> GetMetadataAsync()
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
    }
}