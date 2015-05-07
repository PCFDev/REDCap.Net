using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PCF.REDCap.Model;

namespace REDCapClient
{
    public class XMLParser
    {

        public async Task<Event> HydrateEvent(XElement item)
        {


        }

        /// <summary>
        /// Parse metadata xml <see cref="REDCapClient.GetFormMetadataAsync"/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Metadata> HydrateMetadataFields(XElement item)
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
