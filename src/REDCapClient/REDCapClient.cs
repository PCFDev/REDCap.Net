using System;
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
        private string _token = string.Empty;

        private const string PARAMS_GETFORMDATA = "token={0}&content=record&format={1}&type=eav&returnFormat=label&forms={2}";

        private const string PARAMS_GETFLATFORMDATA = 
            "token={0}&content=record&format={1}&type=eav" +
            "&rawOrLabel=label&rawOrLabelHeader=label&exportCheckboxLabel=true&exportSurveyFields=true&exportDataAccessGroups=true" +
            "&returnFormat=xml&forms={2}";

        private const string PARAMS_GETFORMS = "token={0}&content=instrument&format={1}&rawOrLabel=label&rawOrLabelHeader=label&exportCheckboxLabel=true";

        private const string PARAMS_GETREPORT = "token={0}&content=report&report_id={1}&rawOrLabel=label&rawOrLabelHeader=label&exportCheckboxLabel=true&format={2}";

        public REDCapClient(string apiUrl, string token)
        {
            this._baseUri = new Uri(apiUrl);
            this._token = token;
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

    }
}
