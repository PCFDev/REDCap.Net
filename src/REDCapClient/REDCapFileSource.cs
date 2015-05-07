using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace PCF.REDCap
{
    public class REDCapFileSource : IREDCapClient
    {
        private static string _armFile = string.Empty;
        private static string _eventFile = string.Empty;
        private static string _exportFiledNamesFile = string.Empty;
        private static string _instrumentFile = string.Empty;
        private static string _instrumentEventMappingFile = string.Empty;
        private static string _metadataFile = string.Empty;
        private static string _userFile = string.Empty;

        public async Task Initialize(string apiKey, string apiUri)
        {
            throw new NotImplementedException();
        }

        public async Task Initialize(string armFileName,
            string eventFileName,
            string exportFiledNamesFileName,
            string instrumentFileName,
            string instrumentEventMappingFileName,
            string metadataFileName,
            string userFileName)
        {
            _armFile = armFileName;
            _eventFile = eventFileName;
            _exportFiledNamesFile = exportFiledNamesFileName;
            _instrumentFile = instrumentFileName;
            _instrumentEventMappingFile = instrumentEventMappingFileName;
            _metadataFile = metadataFileName;
            _userFile = userFileName;
        }

        public Task<XDocument> GetMetadataAsXmlAsync()
        {
            return Task.FromResult(XDocument.Load(_metadataFile));
        }
        public Task<XDocument> GetEventsAsXmlAsync()
        {
            return Task.FromResult(XDocument.Load(_eventFile));
        }

        public Task<XDocument> GetInstrumentsAsXmlAsync()
        {
            return Task.FromResult(XDocument.Load(_instrumentFile));
        }

        public Task<XDocument> GetInstrumentEventMappingAsXmlAsync()
        {
            return Task.FromResult(XDocument.Load(_instrumentEventMappingFile));
        }
        public Task<XDocument> GetArmsAsXmlAsync()
        {
            return Task.FromResult(XDocument.Load(_armFile));
        }

        public Task<XDocument> GetExportFieldNamesAsXmlAsync()
        {
            return Task.FromResult(XDocument.Load(_exportFiledNamesFile));
        }

        public Task<XDocument> GetUsersAsXmlAsync()
        {
            return Task.FromResult(XDocument.Load(_userFile));
        }

        public Study Study
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
        
        [Obsolete]
        public Task<Dictionary<string, string>> GetArmsAsync()
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public Task<List<Event>> GetEventsAsync()
        {
            //throw new NotImplementedException();
            var xDoc = XDocument.Load(_eventFile);
            List<Event> events = new List<Event>();

            foreach (var item in xDoc.Descendants("item"))
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

                //TODO: Need arm information here?
                //Dictionary<string, string> arms = await GetArmsAsync();
                // Actual data is <items><arm><event></event>...</arm></itmes>

                // not for a direct  proxy
                //var mappings = xDocMapping.Descendants("event").Where(e => e.Element("unique_event_name").Value.ToString() == item.Element("unique_event_name").Value.ToString());

                //foreach (var form in mappings.Descendants("form"))
                //{
                //    thisEvent.Forms.Add(forms.Where(f => f.FormName == form.Value.ToString()).FirstOrDefault());
                //}

                events.Add(thisEvent);
            }

            return Task.FromResult(events.ToList());
        }

        [Obsolete]
        public Task<List<ExportFieldNames>> GetExportFieldNamesAsync()
        {
            throw new NotImplementedException();
        }
        
        public Task<XDocument> GetFormDataAsXmlAsync()
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

        public async Task<List<Instrument>> GetInstrumentsAsync()
        {
            // throw new NotImplementedException();
            var xDoc = XDocument.Load(_instrumentFile);
            List<Instrument> forms = new List<Instrument>();

            foreach (var item in xDoc.Descendants("item"))
            {
                Instrument form = new Instrument
                {
                    InstrumentName = item.Element("instrument_name").Value.ToString(),
                    InstrumentLabel = item.Element("instrument_label").Value.ToString()
                };

                forms.Add(form);
            }

            return forms.ToList();
        }

        [Obsolete]
        public async Task<List<Metadata>> GetMetadataAsync()
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

    }
}