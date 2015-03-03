using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        private const string PARAMS_GETFORMEVENTMAP = "token={0}&content=formEventMapping&format={1}";
        private const string PARAMS_GETARMS = "token={0}&content=arm&format={1}";
        private const string PARAMS_GETEXPORTFIELDNAMES = "token={0}&content=exportFieldNames&format={1}";

        public REDCapClient(string apiUrl, string token)
        {
            this._baseUri = new Uri(apiUrl);
            this._token = token;
            this._study = new REDCapStudy();
        }

        public async Task<List<FormMetadata>> GetFormMetadataAsync()
        {
            List<FormMetadata> result = new List<FormMetadata>();
            List<Form> forms = await GetFormsAsync();
            List<Metadata> fieldData = await GetMetadataAsync();

            foreach (Form form in forms)
            {
                FormMetadata fm = new FormMetadata();

                fm.FormLabel = form.FormLabel;
                fm.FormName = form.FormName;
                fm.FieldData.AddRange(fieldData.Where(p => p.FormName == form.FormName));

                result.Add(fm);
            }

            return result;
        }

        public async Task<XDocument> GetArmsAsXmlAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this._baseUri;
                var req = new StringContent(string.Format(PARAMS_GETARMS, this._token, "xml"));
                req.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                var response = await client.PostAsync("", req);
                var data = await response.Content.ReadAsStringAsync();
                var xDoc = XDocument.Parse(data);

                return xDoc;
            }
        }

        public async Task<Dictionary<string, string>> GetArmsAsync()
        {
            var xDoc = await this.GetArmsAsXmlAsync();
            Dictionary<string, string> arms = new Dictionary<string, string>();

            foreach (var item in xDoc.Descendants("item"))
            {
                arms.Add(item.Element("arm_num").Value.ToString(), item.Element("name").Value.ToString());
            }

            return arms;
        }

        public async Task<XDocument> GetFormEventMapAsXmlAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this._baseUri;
                var req = new StringContent(string.Format(PARAMS_GETFORMEVENTMAP, this._token, "xml"));
                req.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                var response = await client.PostAsync("", req);
                var data = await response.Content.ReadAsStringAsync();
                var xDoc = XDocument.Parse(data);

                return xDoc;
            }
        }

        public async Task<List<Form>> GetFormEventMapAsync()
        {
            var xDoc = await this.GetFormEventMapAsXmlAsync();
            List<Form> forms = new List<Form>();

            foreach (var item in xDoc.Descendants("item"))
            {
                forms.Add(new Form
                {
                    FormLabel = item.Element("form_label").Value.ToString(),
                    FormName = item.Element("form_name").Value.ToString()
                });
            }

            return forms;
        }

        public async Task<XDocument> GetRecordsAsXmlAsync(string eventName, string formName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this._baseUri;
                var xDoc = new XDocument();

                var req = new StringContent(string.Format(PARAMS_GETRECORD, this._token, "xml", "flat", formName, eventName));
                req.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                var response = await client.PostAsync("", req);
                var data = await response.Content.ReadAsStringAsync();
                xDoc = XDocument.Parse(data);

                return xDoc;
            }
        }

        public async Task<XDocument> GetRecordsAsync(string eventName, string formName)
        {
            XDocument xDoc = await this.GetRecordsAsXmlAsync(eventName, formName);

            return xDoc;
        }

        public async Task<XDocument> GetRecordsAsync(string eventName, string[] formNames)
        {
            XDocument xDoc = await this.GetRecordsAsXmlAsync(eventName, formNames);

            return xDoc;
        }

        public async Task<XDocument> GetRecordsAsXmlAsync(string eventName, string[] formNames)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this._baseUri;
                var xDoc = new XDocument();

                var req = new StringContent(string.Format(PARAMS_GETRECORD, this._token, "xml", "flat", formNames, eventName));
                req.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                var response = await client.PostAsync("", req);
                var data = await response.Content.ReadAsStringAsync();
                xDoc = XDocument.Parse(data);

                return xDoc;
            }
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

        public async Task<List<Event>> GetEventsAsync()
        {
            var xDocEvents = await this.GetEventsAsXmlAsync();
            var xDocMapping = await this.GetFormEventMapAsXmlAsync();
            List<Form> forms = await this.GetFormsAsync();
            List<Event> events = new List<Event>();

            foreach (var item in xDocEvents.Descendants("item"))
            {
                Event thisEvent = new Event
                {
                    UniqueEventName = item.Element("unique_event_name").Value.ToString(),
                    EventName = item.Element("event_name").Value.ToString(),
                    ArmNumber = item.Element("arm_num").Value.ToString(),
                    DayOffset = item.Element("day_offset").Value.ToString(),
                    OffsetMax = item.Element("offset_max").Value.ToString(),
                    OffsetMin = item.Element("offset_min").Value.ToString()
                };

                var mappings = xDocMapping.Descendants("event").Where(e => e.Element("unique_event_name").Value.ToString() == item.Element("unique_event_name").Value.ToString());

                foreach (var form in mappings.Descendants("form"))
                {
                    thisEvent.Forms.Add(forms.Where(f => f.FormName == form.Value.ToString()).FirstOrDefault());
                }

                events.Add(thisEvent);
            }

            return events.ToList();
        }

        public async Task<List<Metadata>> GetMetadataAsync()
        {
            var xDoc = await this.GetMetadataAsXmlAsync();
            List<Metadata> metadata = new List<Metadata>();

            foreach (XElement item in xDoc.Descendants("item"))
            {
                metadata.Add(await HydrateMetadataFields(item));
            }

            return metadata.ToList();
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

        public async Task<List<ExportFieldNames>> GetExportFieldNamesAsync()
        {
            XDocument xDoc = await GetExportFieldNamesXmlAsync();

            if (xDoc == null)
            {
                return null;
            }
            else
            {
                List<ExportFieldNames> fieldNames = new List<ExportFieldNames>();

                return fieldNames;
            }
        }

        public async Task<XDocument> GetExportFieldNamesXmlAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this._baseUri;
                var req = BuildRequest(PARAMS_GETEXPORTFIELDNAMES);
                req.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                var response = await client.PostAsync("", req);
                var data = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(data))
                    return null;
                else
                {
                    var xDoc = XDocument.Parse(data);
                    return xDoc;
                }
            }
        }

        private StringContent BuildRequest(string parameter)
        {
            var req = new StringContent(string.Format(parameter, this._token, "xml"));
            return req;
        }

        public async Task<XDocument> GetFormsAsXmlAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this._baseUri;
                var req = BuildRequest(PARAMS_GETFORMS); //new StringContent(String.Format(PARAMS_GETFORMS, this._token, "xml"));
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

        public async Task<List<Form>> GetFormsAsync()
        {
            var xDoc = await this.GetFormsAsXmlAsync();
            List<Form> forms = new List<Form>();

            foreach (var item in xDoc.Descendants("item"))
            {
                Form form = new Form
                {
                    FormName = item.Element("instrument_name").Value.ToString(),
                    FormLabel = item.Element("instrument_label").Value.ToString()
                };

                forms.Add(form);
            }

            return forms.ToList();
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

        #region Private helper functions

        private async Task<Metadata> HydrateMetadataFields(XElement item)
        {
            List<ExportFieldNames> exportFieldNames = new List<ExportFieldNames>();
            
            //---not working yet
            //exportFieldNames = await this.GetExportFieldNamesAsync();

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
                else if (dataDictionary.FieldType == "checkbox")
                {
                    if (exportFieldNames != null)
                    {
                        int x = 10;
                        // dataDictionary.ExportFieldNames = await GetExportFieldNamesAsync(item.Element("field_name").Value.ToString());
                    }
                }
                else
                {
                    dataDictionary.FieldChoices = ParseFieldChoices(element);
                }
            }

            return dataDictionary;
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
                string key = string.Empty;
                string value = string.Empty;

                if (pos > 0)
                {
                    key = group.Substring(0, pos);
                    value = group.Substring(key.Length + 2, group.Length - (key.Length + 2));
                }
                else
                {
                    key = group.Trim();
                    value = group.Trim();
                }

                choices.Add(key.Trim(), value.Trim());
            }

            return choices;
        }

        #endregion
    }
}
