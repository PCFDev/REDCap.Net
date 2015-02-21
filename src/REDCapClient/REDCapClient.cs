﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace REDCapClient
{
    public class REDCapClient
    {
        private List<string> _formNames = new List<string>();
        private HttpClient client = new HttpClient();
        private Uri _baseUri;
        private REDCapStudy _study;
        private string _token = string.Empty;
        private const string PARAMS_GETFORMDATA = "token={0}&content=record&format={1}&type=eav&returnFormat=label&forms={2}";
        private const string PARAMS_GETFLATFORMDATA =
            "token={0}&content=record&format={1}&type=eav" +
            "&rawOrLabel=label&rawOrLabelHeader=label&exportCheckboxLabel=true&exportSurveyFields=true&exportDataAccessGroups=true" +
            "&returnFormat=xml&forms={2}";

        private const string PARAMS_GETFORMS = "token={0}&content=instrument&format={1}&rawOrLabel=label&rawOrLabelHeader=label&exportCheckboxLabel=true";
        private const string PARAMS_GETREPORT = "token={0}&content=report&report_id={1}&rawOrLabel=label&rawOrLabelHeader=label&exportCheckboxLabel=true&format={2}";
        private const string PARAMS_GETEVENT = "token={0}&content=event&format={1}";
        private const string PARAMS_GETMETADATAPERFORM = "token={0}&content=metadata&format={1}&forms={2}";
        private const string PARAMS_GETMETADATA = "token={0}&content=metadata&format={1}";
        private const string PARAMS_GETRECORD = "token={0}&content=record&format={1}&type={2}&forms={3}&events={4}";

        public REDCapClient(string apiUrl, string token)
        {
            this._baseUri = new Uri(apiUrl);
            this._token = token;
            this._study = new REDCapStudy();
        }

        public async Task<XDocument> GetRecordsAsXmlAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this._baseUri;
                var xDoc = new XDocument();

                foreach (var item in this._study.Events)
                {
                    item.FormName = "month_data";
                    //var req = new StringContent(string.Format(PARAMS_GETRECORD, this._token, "xml", "flat", item.FormName, item.UniqueEventName));
                    var req = new StringContent(string.Format(PARAMS_GETRECORD, this._token, "xml", "flat", item.FormName, "month_1_arm_1"));
                    req.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                    var response = await client.PostAsync("", req);
                    var data = await response.Content.ReadAsStringAsync();
                    xDoc = XDocument.Parse(data);
                }

                return xDoc;
            }
        }

        public async Task<string> GetRecordsAsync()
        {
            var xDoc = await this.GetRecordsAsXmlAsync();

            return "help";
        }

        public async Task<XDocument> GetEventsAsXmlAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this._baseUri;
                var req = new StringContent(string.Format(PARAMS_GETEVENT, this._token, "xml"));
                req.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                var response = await client.PostAsync("", req);
                var data = await response.Content.ReadAsStringAsync();
                var xDoc = XDocument.Parse(data);

                return xDoc;
            }
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            var xDoc = await this.GetEventsAsXmlAsync();

            var events = xDoc.Descendants("unique_event_name").Select(e => e.Value);
            foreach (var item in xDoc.Descendants("item"))
            {
                this._study.Events.Add(new Event
                {
                    UniqueEventName = item.Element("unique_event_name").Value.ToString(),
                    EventName = item.Element("event_name").Value.ToString(),
                    ArmNumber = item.Element("arm_num").Value.ToString()
                });
            }

            return this._study.Events;
        }

        public async Task<XDocument> GetMetadataAsXmlAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this._baseUri;
                var req = new StringContent(string.Format(PARAMS_GETMETADATA, this._token, "xml"));
                req.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                var response = await client.PostAsync("", req);
                var data = await response.Content.ReadAsStringAsync();
                var xDoc = XDocument.Parse(data);
                return xDoc;
            }
        }

        public async Task<IEnumerable<Metadata>> GetMetadataAsync()
        {
            var xDoc = await this.GetMetadataAsXmlAsync();
            var fieldNames = xDoc.Descendants("field_name").Select(e => e.Value);

            foreach (var item in xDoc.Descendants("item"))
            {
                Metadata dataDictionary = new Metadata
                    {
                        FieldName = (item.Element("field_name").IsEmpty ? "" : item.Element("field_name").Value.ToString()),
                        FormName = (item.Element("form_name").IsEmpty ? "" : item.Element("form_name").Value.ToString()),
                        FieldType = (item.Element("field_type").IsEmpty ? "" : item.Element("field_type").Value.ToString()),
                        FieldLabel = (item.Element("field_label").IsEmpty ? "" : item.Element("field_label").Value.ToString()),
                        FieldNote = (item.Element("field_note").IsEmpty ? "" : item.Element("field_note").Value.ToString()),
                        TextValidation = (item.Element("text_validation_type_or_show_slider_number").IsEmpty ? "" : item.Element("text_validation_type_or_show_slider_number").Value.ToString()),
                        TextValidationMax = (item.Element("text_validation_max").IsEmpty ? "" : item.Element("text_validation_max").Value.ToString()),
                        TextValidationMin = (item.Element("text_validation_min").IsEmpty ? "" : item.Element("text_validation_min").Value.ToString()),
                        IsIdentifier = (item.Element("identifier").IsEmpty ? false : item.Element("identifier").Value.ToString().ToLower() == "y" ? true : false),
                        BranchingLogic = (item.Element("branching_logic").IsEmpty ? "" : item.Element("branching_logic").Value.ToString()),
                        IsRequired = (item.Element("required_field").IsEmpty ? false : item.Element("required_field").Value.ToString().ToLower() == "y" ? true : false),
                        CustomAlignment = (item.Element("custom_alignment").IsEmpty ? "" : item.Element("custom_alignment").Value.ToString()),
                        QuestionNumber = (item.Element("question_number").IsEmpty ? "" : item.Element("question_number").Value.ToString()),
                        MatrixGroupName = (item.Element("matrix_group_name").IsEmpty ? "" : item.Element("matrix_group_name").Value.ToString()),
                        IsMatrixRanking = (item.Element("matrix_ranking").IsEmpty ? false : item.Element("matrix_ranking").Value.ToString().ToLower() == "y" ? true : false)
                    };

                if (!String.IsNullOrEmpty(item.Element("select_choices_or_calculations").Value.ToString()))
                {
                    string element = item.Element("select_choices_or_calculations").Value.ToString();
                    if (dataDictionary.FieldType == "calc")
                    {
                        dataDictionary.FieldCalculation = item.Element("select_choices_or_calculations").Value.ToString();
                    }
                    else if (dataDictionary.FieldType == "slider")
                    {
                        dataDictionary.FieldChoices = ParseFieldChoicesSliderType(element);
                    }
                    else
                    {
                        dataDictionary.FieldChoices = ParseFieldChoices(element);
                    }
                }
            }

            return this._study.Metadata;
        }

        private Dictionary<string, string> ParseFieldChoicesSliderType(string element)
        {
            Dictionary<string, string> choices = new Dictionary<string, string>();
            string[] split = element.Split('|');
            int count = 0;

            foreach (string value in split)
            {
                choices.Add(count.ToString(), value);

                count += 1;
            }

            return choices;
        }

        private Dictionary<string, string> ParseFieldChoices(string element)
        {
            Dictionary<string, string> choices = new Dictionary<string, string>();
            string[] split = element.Split('|');

            foreach (string group in split)
            {
                int pos = group.IndexOf(',');
                string key = group.Substring(0, pos);
                string value = group.Substring(key.Length + 2, group.Length - (key.Length + 2));

                choices.Add(key, value.Trim());
            }


            return choices;
        }

        public async Task<XDocument> GetFormsAsXmlAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this._baseUri;
                var req = new StringContent(String.Format(PARAMS_GETFORMS, this._token, "xml"));
                req.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                var response = await client.PostAsync("", req);
                var data = await response.Content.ReadAsStringAsync();
                var xDoc = XDocument.Parse(data);

                return xDoc;
            }
        }

        public async Task<XDocument> GetReportAsXmlAsync(string reportId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this._baseUri;
                var req = new StringContent(String.Format(PARAMS_GETREPORT, this._token, reportId, "xml"));
                req.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                var response = await client.PostAsync("", req);
                var data = await response.Content.ReadAsStringAsync();
                var xDoc = XDocument.Parse(data);

                return xDoc;
            }
        }

        public async Task<IEnumerable<string>> GetFormNamesAsync()
        {
            var xDoc = await this.GetFormsAsXmlAsync();

            var names = xDoc.Descendants("instrument_name").Select(e => e.Value);

            return names;
        }

        public async Task<XDocument> GetFormDataAsXmlAsync(string formName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this._baseUri;

                var formRequestData = new StringContent(String.Format(PARAMS_GETFLATFORMDATA, this._token, "xml", formName));

                formRequestData.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";

                var formResponse = await client.PostAsync("", formRequestData);

                var formData = await formResponse.Content.ReadAsStringAsync();

                var formDoc = XDocument.Parse(formData);

                return formDoc;

            }
        }

        public REDCapStudy Study
        {
            get { return this._study; }
            set { this._study = value; }
        }

    }
}
