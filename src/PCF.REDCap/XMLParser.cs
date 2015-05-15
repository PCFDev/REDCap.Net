using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PCF.REDCap
{
    public class XMLParser : IParser
    {


        public IDictionary<string, string> HydrateArms(string data)
        {
            var xDocArms = XElement.Parse(data);

            Dictionary<string, string> arms = new Dictionary<string, string>();

            foreach (var item in xDocArms.Descendants("item"))
            {
                arms.Add(item.Element("arm_num").GetValue(), item.Element("name").GetValue());
            }

            return arms;
        }

        /// <summary>
        /// Parse event xml <see cref="REDCapClient.GetEventsAsync"/>
        /// </summary>
        /// <param name="data"></param>
        /// <returns name = "events"></param></returns>
        public IEnumerable<Event> HydrateEvent(string data)
        {
            var xml = XElement.Parse(data);

            List<Event> events = new List<Event>();

            foreach (var item in xml.Descendants("item"))
            {
                Event thisEvent = new Event
                {
                    UniqueEventName = item.Element("unique_event_name").GetValue(),
                    EventName = item.Element("event_name").GetValue(),
                    ArmNumber = item.Element("arm_num").GetValue(),
                    DayOffset = item.Element("day_offset").GetValue(),
                    OffsetMax = item.Element("offset_max").GetValue(),
                    OffsetMin = item.Element("offset_min").GetValue()
                };

                //TODO: Need arm information here?
                //Dictionary<string, string> arms = await GetArmsAsync();
                // Actual data is <items><arm><event></event>...</arm></itmes>

                events.Add(thisEvent);
            }

            return events;

        }

        /// <summary>
        /// Parse form xml <see cref="REDCapClient.GetFormsAsync"/>
        /// </summary>
        /// <param name="data"></param>
        /// <returns name = "forms"></returns>
        public IEnumerable<Instrument> HydrateInstrument(string data)
        {
            var xml = XElement.Parse(data);
            foreach (var item in xml.Descendants("item"))
            {
                yield return new Instrument
                {
                    InstrumentName = item.Element("instrument_name").GetValue(),
                    InstrumentLabel = item.Element("instrument_label").GetValue()
                };
            }
        }

        /// <summary>
        /// Parse metadata xml <see cref="REDCapClient.GetMetadataAsync"/>
        /// </summary>
        /// <param name="data"></param>
        /// <returns name = "metadata"></returns>
        public IEnumerable<Metadata> HydrateMetadata(string data)
        {
            var xml = XElement.Parse(data);

            foreach (XElement item in xml.Descendants("item"))
            {
                if (item != null)
                    yield return HydrateMetadataFields(item.ToString());
            }

        }



        public Metadata HydrateMetadataFields(string data)
        {
            var xml = XElement.Parse(data);

            //var exportFieldNames = new List<ExportFieldNames>();
            //---not working yet
            //exportFieldNames = await this.GetExportFieldNamesAsync();

            Metadata dataDictionary;

            try
            {
                dataDictionary = new Metadata
                {
                    FieldName = (xml.Element("field_name").GetValue()),
                    FormName = (xml.Element("form_name").GetValue()),
                    FieldType = (xml.Element("field_type").GetValue()),
                    FieldLabel = (xml.Element("field_label").GetValue()),
                    FieldNote = (xml.Element("field_note").GetValue()),
                    TextValidation = (xml.Element("text_validation_type_or_show_slider_number").GetValue()),
                    TextValidationMax = (xml.Element("text_validation_max").GetValue()),
                    TextValidationMin = (xml.Element("text_validation_min").GetValue()),
                    IsIdentifier = (xml.Element("identifier").GetValue().ToLower() == "y" ? true : false),
                    BranchingLogic = (xml.Element("branching_logic").GetValue()),
                    IsRequired = (xml.Element("required_field").GetValue().ToLower() == "y" ? true : false),
                    CustomAlignment = (xml.Element("custom_alignment").GetValue()),
                    QuestionNumber = (xml.Element("question_number").GetValue()),
                    MatrixGroupName = (xml.Element("matrix_group_name").GetValue()),
                    IsMatrixRanking = (xml.Element("matrix_ranking").GetValue().ToLower() == "y" ? true : false)
                };

                if (!xml.Element("select_choices_or_calculations").ElementIsEmpty())
                {
                    string element = xml.Element("select_choices_or_calculations").GetValue();
                    if (dataDictionary.FieldType == "calc")
                    {
                        dataDictionary.FieldCalculation = xml.Element("select_choices_or_calculations").GetValue();
                    }
                    else if (dataDictionary.FieldType == "slider")
                    {
                        dataDictionary.FieldChoices = ParseFieldChoicesSliderType(element);
                    }
                    //else if (dataDictionary.FieldType == "checkbox")
                    //{
                    //    if (exportFieldNames != null)
                    //    {
                    //        //int x = 10;
                    //        // dataDictionary.ExportFieldNames = await GetExportFieldNamesAsync(item.Element("field_name").Value.ToString());
                    //    }
                    //}
                    else
                    {
                        dataDictionary.FieldChoices = ParseFieldChoices(element);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return dataDictionary;
        }


        /// <summary>
        /// Parse user xml
        /// </summary>
        /// <param name="data"> this is the xml snippet containing the user xml data </param>
        /// <returns name = "users"></returns>
        public IEnumerable<User> HydrateUsers(string data)
        {
            var xml = XElement.Parse(data);

            List<User> users = new List<User>();

            foreach (var item in xml.Descendants("item"))
            {
                User user = new User
                {
                    UserName = item.Element("username").GetValue(),
                    Email = item.Element("email").GetValue(),
                    FirstName = item.Element("firstname").GetValue(),
                    LastName = item.Element("lastname").GetValue(),
                    Expiration = item.Element("expiration").GetValue(),
                    DataAccessGroup = item.Element("data_access_group").GetValue(),
                    DataExport = item.Element("data_export").GetValueAsInt()
                };

                foreach (XElement entries in item.Elements("forms").Elements())
                {
                    user.Forms.Add(entries.Name.ToString(), entries.GetValueAsInt());
                }

                users.Add(user);
            }

            return users;
        }

        /// <summary>
        /// Parse instrument event xml
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IEnumerable<InstrumentEventMapping> HydrateInstrumentEvents(string data)
        {
            var xml = XElement.Parse(data);

            var instruments = new List<InstrumentEventMapping>();

            try
            {
                foreach (XElement instrument in xml.Descendants("arm"))
                {
                    var iem = new InstrumentEventMapping()
                    {
                        ArmNumber = instrument.Element("number").GetValue()
                    };

                    foreach (XElement entries in instrument.Descendants("event"))
                    {
                        var forms = new List<string>();

                        foreach (XElement form in entries.Elements("form"))
                        {
                            forms.Add(form.GetValue());
                        }

                        iem.EventInstruments.Add(entries.Element("unique_event_name").GetValue(), forms);
                    }

                    instruments.Add(iem);
                }

            }
            catch (Exception ex)
            {

                throw;
            }

            return instruments;
        }

        /// <summary>
        /// Parse record xml
        /// </summary>
        /// <param name="data"> this is the xml snippet containing the user xml data </param>
        /// <returns name="records"></returns>
        public IEnumerable<Record> HydrateRecords(string data)
        {
            var xml = XElement.Parse(data);

            List<Record> records = new List<Record>();

            foreach (XElement item in xml.Descendants("records").Elements("item"))
            {
                Record record = new Record
                {
                    PatientId = item.Element("record").GetValue(),
                    Concept = item.Element("field_name").GetValue(),
                    ConceptValue = item.Element("value").GetValue(),
                    EventName = item.Element("redcap_event_name").GetValue(),
                };

                records.Add(record);
            }

            return records;
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

        public IEnumerable<ExportFieldNames> HydrateExportFieldNames(string data)
        {

            var xml = XElement.Parse(data);

            foreach (var item in xml.Descendants("field"))
            {
                yield return new ExportFieldNames()
                {
                    ChoiceValue = item.Element("choice_value").GetValue(),
                    ExportFieldName = item.Element("export_field_name").GetValue(),
                    OriginalFieldName = item.Element("original_field_name").GetValue()
                };
            }



        }
    }
}
