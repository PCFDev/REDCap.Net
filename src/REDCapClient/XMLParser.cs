using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PCF.REDCap
{
    public class XMLParser
    {
        /// <summary>
        /// Parse event xml <see cref="REDCapClient.GetEventsAsync"/>
        /// </summary>
        /// <param name="xDocEvents"></param>
        /// <returns name = "events"></param></returns>
        public async Task<List<Event>> HydrateEvent(XElement xDocEvents)
        {

            List<Event> events = new List<Event>();

            foreach (var item in xDocEvents.Descendants("item"))
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
        /// <param name="xDocForms"></param>
        /// <returns name = "forms"></returns>
        public async Task<List<Instrument>> HydrateForms(XElement xDocForms)
        {
            List<Instrument> forms = new List<Instrument>();

            foreach (var item in xDocForms.Descendants("item"))
            {
                Instrument form = new Instrument
                {
                    InstrumentName = item.Element("instrument_name").GetValue(),
                    InstrumentLabel = item.Element("instrument_label").GetValue()
                };

                forms.Add(form);
            }

            return forms;
        }

        /// <summary>
        /// Parse metadata xml <see cref="REDCapClient.GetMetadataAsync"/>
        /// </summary>
        /// <param name="xDocMetadata"></param>
        /// <returns name = "metadata"></returns>
        public async Task<List<Metadata>> HydrateMetadata(XElement xDocMetadata)
        {
            List<Metadata> metadata = new List<Metadata>();

            foreach (XElement item in xDocMetadata.Descendants("item"))
            {
                metadata.Add(await HydrateMetadataFields(item));
            }

            return metadata;
        }

        /// <summary>
        /// Parse arms xml <see cref="REDCapClient.GetArmsAsync"/>
        /// </summary>
        /// <param name="xDocArms"></param>
        /// <returns name = "arms"></returns>
        public async Task<Dictionary<string, string>> HydrateArms(XElement xDocArms)
        {
            Dictionary<string, string> arms = new Dictionary<string, string>();

            foreach (var item in xDocArms.Descendants("item"))
            {
                arms.Add(item.Element("arm_num").GetValue(), item.Element("name").GetValue());
            }

            return arms;
        }

        public async Task<Metadata> HydrateMetadataFields(XElement item)
        {
            List<ExportFieldNames> exportFieldNames = new List<ExportFieldNames>();

            //---not working yet
            //exportFieldNames = await this.GetExportFieldNamesAsync();

            Metadata dataDictionary = new Metadata
            {
                FieldName = (item.Element("field_name").GetValue()),
                FormName = (item.Element("form_name").GetValue()),
                FieldType = (item.Element("field_type").GetValue()),
                FieldLabel = (item.Element("field_label").GetValue()),
                FieldNote = (item.Element("field_note").GetValue()),
                TextValidation = (item.Element("text_validation_type_or_show_slider_number").GetValue()),
                TextValidationMax = (item.Element("text_validation_max").GetValue()),
                TextValidationMin = (item.Element("text_validation_min").GetValue()),
                IsIdentifier = (item.Element("identifier").GetValue().ToLower() == "y" ? true : false),
                BranchingLogic = (item.Element("branching_logic").GetValue()),
                IsRequired = (item.Element("required_field").GetValue().ToLower() == "y" ? true : false),
                CustomAlignment = (item.Element("custom_alignment").GetValue()),
                QuestionNumber = (item.Element("question_number").GetValue()),
                MatrixGroupName = (item.Element("matrix_group_name").GetValue()),
                IsMatrixRanking = (item.Element("matrix_ranking").GetValue().ToLower() == "y" ? true : false)
            };

            if (!item.Element("select_choices_or_calculations").ElementIsEmpty())
            {
                string element = item.Element("select_choices_or_calculations").GetValue();
                if (dataDictionary.FieldType == "calc")
                {
                    dataDictionary.FieldCalculation = item.Element("select_choices_or_calculations").GetValue();
                }
                else if (dataDictionary.FieldType == "slider")
                {
                    dataDictionary.FieldChoices = ParseFieldChoicesSliderType(element);
                }
                else if (dataDictionary.FieldType == "checkbox")
                {
                    if (exportFieldNames != null)
                    {
                        //int x = 10;
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


        /// <summary>
        /// Parse user xml
        /// </summary>
        /// <param name="xDocUsers" <see cref="XElement"/>> this is the xml snippet containing the user xml data </param>
        /// <returns name = "users"></returns>
        public async Task<List<User>> HydrateUsers(XElement xDocUsers)
        {
            List<User> users = new List<User>();

            foreach (var item in xDocUsers.Descendants("item"))
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
        /// <param name="element"></param>
        /// <returns></returns>
        public async Task<List<InstrumentEventMapping>> HydrateInstrumentEvents(XElement xDocInstrumentEvents)
        {
            List<InstrumentEventMapping> instruments = new List<InstrumentEventMapping>();

            foreach (XElement instrument in xDocInstrumentEvents.Descendants("arm"))
            {
                InstrumentEventMapping iem = new InstrumentEventMapping()
                {
                    ArmNumber = instrument.Element("number").GetValue()
                };

                foreach (XElement entries in instrument.Descendants("event"))
                {
                    List<string> form = new List<string>();

                    foreach (XElement forms in entries.Elements("form"))
                    {
                        form.Add(forms.GetValue());
                    }

                    iem.EventInstruments.Add(entries.Element("unique_event_name").GetValue(), form);
                }

                instruments.Add(iem);
            }


            return instruments;
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
