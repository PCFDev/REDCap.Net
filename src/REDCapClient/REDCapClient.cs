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
        private string _token = "";

        private const string PARAMS_GETFORMDATA = "token={0}&content=record&format={1}&type=eav&returnFormat=label&forms={2}";

        public REDCapClient(string apiUrl, string token)
        {
            this._baseUri = new Uri(apiUrl);
            this._token = token;
        }


        public async Task<XDocument> GetFormDataAsXmlAsync(string formName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this._baseUri; // new Uri("https://redcap.wustl.edu/redcap/srvrs/dev_v3_1_0_001/redcap/api/");


                //var formRequestData = new StringContent("token=1F29244B68AFFC232491A4A69B40AAA0&content=record&format=xml&type=eav&returnFormat=label&forms=" + formName);
                var formRequestData = new StringContent(String.Format(PARAMS_GETFORMDATA, this._token, "xml", formName));

                formRequestData.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";

                var formResponse = await client.PostAsync("", formRequestData);

                var formData = await formResponse.Content.ReadAsStringAsync();

                var formDoc = XDocument.Parse(formData);

                //formDoc.Save(formName + "_output.xml");


                return formDoc;

            }
        }

        //private static string GenerateCreateTableSQL(string formName, XDocument formDoc)
        //{
        //    var formFields = formDoc.Descendants("field_name").Select(e => e.Value).Distinct();

        //    var sql = string.Format("CREATE TABLE [dbo].[{0}] (", formName);

        //    var sqlBuilder = new StringBuilder();


        //    sqlBuilder.Append(sql);

        //    sqlBuilder.Append("[Id] INT IDENTITY (1, 1) NOT NULL, ");
        //    sqlBuilder.Append("[PatientID] NVARCHAR(MAX) NULL, ");
        //    sqlBuilder.Append("[EventName] NVARCHAR(MAX) NULL");

        //    if (formFields.Count() > 0)
        //    {
        //        sqlBuilder.Append(", ");
        //        foreach (var field in formFields)
        //        {
        //            sqlBuilder.AppendFormat("[{0}] NVARCHAR(MAX) NULL", field);

        //            //if (field != formFields.Last())
        //            sqlBuilder.Append(", ");
        //        }


        //    }



        //    sqlBuilder.Append(" PRIMARY KEY CLUSTERED ([Id] ASC));");

        //    //Console.WriteLine(sqlBuilder.ToString());
        //    var sqlCmd = sqlBuilder.ToString();
        //    return sqlCmd;
        //}
     
        //public async Task ProcessProject()
        //{
        //    client.BaseAddress = new Uri("https://redcap.wustl.edu/redcap/srvrs/dev_v3_1_0_001/redcap/api/");

        //    var req = new StringContent("token=1F29244B68AFFC232491A4A69B40AAA0&content=instrument&format=xml");

        //    req.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";

        //    var response = await client.PostAsync("", req);

        //    var data = await response.Content.ReadAsStringAsync();

        //    var xDoc = XDocument.Parse(data);
            
        //    //xDoc.Save("forms_output.xml");

        //    var names = xDoc.Descendants("instrument_name").Select(e => e.Value);

        //    _formNames.AddRange(names);

        //    foreach (var form in _formNames)
        //    {
        //        await ProcessForm(form);

        //    }
        //}

        //private async Task ProcessForm(string form)
        //{
        //    var formRequestData = new StringContent("token=1F29244B68AFFC232491A4A69B40AAA0&content=record&format=xml&type=eav&returnFormat=label&forms=" + form);

        //    formRequestData.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";

        //    var formResponse = await client.PostAsync("", formRequestData);

        //    var formData = await formResponse.Content.ReadAsStringAsync();

        //    var formDoc = XDocument.Parse(formData);

        //   // formDoc.Save(form + "_output.xml");


        //    var formFields = formDoc.Descendants("field_name").Select(e => e.Value).Distinct();

        //    var sql = string.Format("CREATE TABLE [dbo].[{0}] (", form);

        //    var sqlBuilder = new StringBuilder();

        //    //            CREATE TABLE [dbo].[patient_tracking] (
        //    //    [Id]                        INT            IDENTITY (1, 1) NOT NULL,
        //    //    [PatientID]                 NVARCHAR (MAX) NULL,
        //    //    [EventName]                 NVARCHAR (MAX) NULL,
        //    //    [barnes_nicu]               NVARCHAR (MAX) NULL,
        //    //    [case_de]                   NVARCHAR (MAX) NULL,
        //    //    [city]                      NVARCHAR (MAX) NULL,
        //    //    [clinical_serv]             NVARCHAR (MAX) NULL,
        //    //    [currentpsy]                NVARCHAR (MAX) NULL,
        //    //    [currenttherap]             NVARCHAR (MAX) NULL,
        //    //    [eligible]                  NVARCHAR (MAX) NULL,
        //    //    [hxmentill]                 NVARCHAR (MAX) NULL,
        //    //    [id_tracking]               NVARCHAR (MAX) NULL,
        //    //    [mother_mother]             NVARCHAR (MAX) NULL,
        //    //    [nicutrans]                 NVARCHAR (MAX) NULL,
        //    //    [nurse_new]                 NVARCHAR (MAX) NULL,
        //    //    [ob]                        NVARCHAR (MAX) NULL,
        //    //    [patient_tracking_complete] NVARCHAR (MAX) NULL,
        //    //    [ptlocation]                NVARCHAR (MAX) NULL,
        //    //    [ptstatus]                  NVARCHAR (MAX) NULL,
        //    //    [refer_by]                  NVARCHAR (MAX) NULL,
        //    //    [refer_made]                NVARCHAR (MAX) NULL,
        //    //    [timemom]                   NVARCHAR (MAX) NULL,
        //    //    [zip]                       NVARCHAR (MAX) NULL,
        //    //    [consent_baby1]             NVARCHAR (MAX) NULL,
        //    //    [email]                     NVARCHAR (MAX) NULL,
        //    //    [phone1]                    NVARCHAR (MAX) NULL,
        //    //    [phone2]                    NVARCHAR (MAX) NULL,
        //    //    [ref_type]                  NVARCHAR (MAX) NULL,
        //    //    [research_share]            NVARCHAR (MAX) NULL,
        //    //    [research_store]            NVARCHAR (MAX) NULL,
        //    //    [barnes_male_female]        NVARCHAR (MAX) NULL,
        //    //    [consent_baby2]             NVARCHAR (MAX) NULL,
        //    //    [consent_baby3]             NVARCHAR (MAX) NULL,
        //    //    [ebdsby1]                   NVARCHAR (MAX) NULL,
        //    //    [ebdsby2]                   NVARCHAR (MAX) NULL,
        //    //    [ebdsby4]                   NVARCHAR (MAX) NULL,
        //    //    [ebdsby5]                   NVARCHAR (MAX) NULL,
        //    //    [ebdsby6]                   NVARCHAR (MAX) NULL,
        //    //    [interpreter_lang]          NVARCHAR (MAX) NULL,
        //    //    [modeepdsadm1]              NVARCHAR (MAX) NULL,
        //    //    [modeepdsadm2]              NVARCHAR (MAX) NULL,
        //    //    [modeepdsadm3]              NVARCHAR (MAX) NULL,
        //    //    [modeepdsadm4]              NVARCHAR (MAX) NULL,
        //    //    [modeepdsadm5]              NVARCHAR (MAX) NULL,
        //    //    [modeepdsadm6]              NVARCHAR (MAX) NULL,
        //    //    [phone3]                    NVARCHAR (MAX) NULL,
        //    //    [phone4]                    NVARCHAR (MAX) NULL,
        //    //    [psyfactors]                NVARCHAR (MAX) NULL,
        //    //    [reason_inelig]             NVARCHAR (MAX) NULL,
        //    //    PRIMARY KEY CLUSTERED ([Id] ASC)
        //    //);
        //    sqlBuilder.Append(sql);

        //    sqlBuilder.Append("[Id] INT IDENTITY (1, 1) NOT NULL, ");
        //    sqlBuilder.Append("[PatientID] NVARCHAR(MAX) NULL, ");
        //    sqlBuilder.Append("[EventName] NVARCHAR(MAX) NULL");

        //    if (formFields.Count() > 0)
        //    {
        //        sqlBuilder.Append(", ");
        //        foreach (var field in formFields)
        //        {
        //            sqlBuilder.AppendFormat("[{0}] NVARCHAR(MAX) NULL", field);

        //            //if (field != formFields.Last())
        //            sqlBuilder.Append(", ");
        //        }


        //    }



        //    sqlBuilder.Append(" PRIMARY KEY CLUSTERED ([Id] ASC));");

        //    //Console.WriteLine(sqlBuilder.ToString());
        //    var sqlCmd = sqlBuilder.ToString();
        //    try
        //    {

        //        await _context.Database.ExecuteSqlCommandAsync(sqlCmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("");
        //        Console.WriteLine("ERROR:");
        //        Console.WriteLine(ex.Message);

        //        Console.WriteLine(sqlCmd);
        //        Console.WriteLine("END ERROR:");
        //        Console.WriteLine("");
        //    }
        //}
    }
}
