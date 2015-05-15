using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PCF.REDCap
{
    public class DataManager
    {
        //private DataContext _context = new DataContext();
        //private REDCapClient _redCapClient;
        //private SqlConvertor _sqlConvertor = new SqlConvertor();

        // FILE WRITING
        //FileStream ostream;
        //StreamWriter writer;
        //TextWriter oldOut = Console.Out;
        // FILE WRITING

        public DataManager(IREDCapClient client)
        {

        }

        private XMLParser _parser = new XMLParser();
        private REDCap.Study _study = new Study();

        public async Task ProcessProject(ProjectConfiguration cfgItem)
        {
            if(string.IsNullOrEmpty(cfgItem.ApiKey))
            {
                // Process using files:
                await Task.FromResult(ProcessFileProject(cfgItem));
            }
            else
            {
                // Process using web API calls:
                await Task.FromResult(ProcessWebProject(cfgItem));
            }
        }

        private async Task ProcessWebProject(ProjectConfiguration cfgItem)
        {
            //// Using WEB API:
            //WebREDCapClient rcClient = new WebREDCapClient();
            //await rcClient.Initialize(cfgItem.ApiKey, cfgItem.ApiUrl);

            //// Each instrument is a "table"
            //XDocument xInstrument = await rcClient.GetInstrumentsAsXmlAsync();

            //// Each item in metadata will be assigned to an instrument
            //// Each item will contain data about that item (radio selection, checkbox values, etc.)
            //XDocument xMetadata = await rcClient.GetMetadataAsXmlAsync();

            //// Multi-value fields have different names than the parent field, those are in this file
            //XDocument xExportFieldNames = await rcClient.GetExportFieldNamesAsXmlAsync();

            //// A study may have multiple arms, arm information is in this file
            //XDocument xArms = await rcClient.GetArmsAsXmlAsync();

            //// An event has a particular arm and can have multiple instruments used and
            //// A particular instrument can be listed in multiple events
            //XDocument xEvents = await rcClient.GetEventsAsXmlAsync();

            //// This file lists each event in the study and the list of instruments used in that event
            //XDocument xMaping = await rcClient.GetInstrumentEventMappingAsXmlAsync();

            //// The user list for this study
            //XDocument xUsers = await rcClient.GetUsersAsXmlAsync();
        }

        private async Task ProcessFileProject(ProjectConfiguration cfgItem)
        {
            // Using file system source
            FileREDCapClient rcClient = new FileREDCapClient();
            await rcClient.Initialize(cfgItem.ArmFileName,
                cfgItem.EventFileName,
                cfgItem.ExportFieldNamesFileName,
                cfgItem.InstrumentFileName,
                cfgItem.InstrumentEventMappingFileName,
                cfgItem.MetadataFileName,
                cfgItem.UserFileName);

            // Each item in metadata will be assigned to an instrument
            // Each item will contain data about that item (radio selection, checkbox values, etc.)
            XDocument xMetadata = await rcClient.GetMetadataAsXmlAsync();
            _study.Metadata = await _parser.HydrateMetadata(xMetadata.Element("records"));
            
            // A study may have multiple arms, arm information is in this file
            XDocument xArms = await rcClient.GetArmsAsXmlAsync();
            _study.Arms = await _parser.HydrateArms(xArms.Element("arms"));

            // An event has a particular arm and can have multiple instruments used and
            // A particular instrument can be listed in multiple events
            XDocument xEvents = await rcClient.GetEventsAsXmlAsync();
            _study.Events = await _parser.HydrateEvent(xEvents.Element("events"));

            // Each instrument is a "table"
            XDocument xInstrument = await rcClient.GetInstrumentsAsXmlAsync();
            List<Instrument> instruments = await _parser.HydrateForms(xInstrument.Element("instruments"));
            
            // The user list for this study
            XDocument xUsers = await rcClient.GetUsersAsXmlAsync();
            List<User> users = await _parser.HydrateUsers(xUsers.Element("records"));
            
            // This file lists each event in the study and the list of instruments used in that event
            XDocument xMaping = await rcClient.GetInstrumentEventMappingAsXmlAsync();

            // Multi-value fields have different names than the parent field, those are in this file
            XDocument xExportFieldNames = await rcClient.GetExportFieldNamesAsXmlAsync();
        }

        //public async Task ProcessProject(string apiUrl, string token)
        //{
        //    // Working code...

        //    _redCapClient = new REDCapClient.REDCapClient(apiUrl, token); // Start the API client

        //    _study.Events = await _redCapClient.GetEventsAsync(); // Gets all the events available in the study
        //    _study.Metadata = await _redCapClient.GetMetadataAsync(); // Gets study metadata

        //    //...end working code


        //    // _study.Arms = await _redCapClient.GetArmsAsync();
        //    // var result = await _redCapClient.TestRecords();

        //    foreach (Event eventSub in _study.Events)
        //    {
        //        foreach (Instrument formSub in eventSub.Instruments)
        //        {
        //            try
        //            {
        //                ostream = new FileStream("./" + eventSub.UniqueEventName + "_" + formSub.InstrumentName + ".csv", FileMode.OpenOrCreate, FileAccess.Write);
        //                writer = new StreamWriter(ostream);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("Cannot open text file for writing.");
        //                Console.WriteLine(ex.Message);

        //                return;
        //            }

        //            Console.SetOut(writer);

        //            //string[] formNames = { "demographics", "month_data" };
        //            //XDocument recordArray = await this._redCapClient.GetRecordsAsync("month_1_arm_1", formNames);
        //            //await ProcessRecord(recordArray, formNames);

        //            XDocument records = await this._redCapClient.GetRecordsAsync(eventSub.UniqueEventName, formSub.InstrumentName);
        //            await ProcessRecord(records, formSub.InstrumentName);

        //            Console.SetOut(oldOut);
        //            writer.Close();
        //            ostream.Close();
        //            Console.WriteLine("Done");
        //        }
        //    }
        //}

        //public async Task<List<REDCapClient.FormMetadata>> GetStudyFormData(string apiUrl, string apiToken)
        //{
        //    this._redCapClient = new REDCapClient.REDCapClient(apiUrl, apiToken);
        //    List<FormMetadata> results = new List<FormMetadata>();

        //    results = await this._redCapClient.GetFormMetadataAsync();

        //    return results.ToList();
        //    //this._study.Events = await this._redCapClient.GetEventsAsync();
        //    //this._study.Metadata = await this._redCapClient.GetMetadataAsync();

        //    //return this._study;
        //}

        //private async Task ProcessRecord(XDocument xDoc, string[] forms)
        //{
        //    List<Metadata> fieldList = this._study.Metadata.Where(f => f.FormName == forms[0]).ToList();
        //    List<Metadata> secondList = this._study.Metadata.Where(f => f.FormName == forms[1]).ToList();

        //    fieldList.AddRange(secondList.ToList());

        //    ProcessEvents(fieldList, xDoc);
        //}

        //private async Task ProcessRecord(XDocument xDoc, string form)
        //{
        //    List<Metadata> fieldList = this._study.Metadata.Where(f => f.FormName == form).ToList();


        //    ProcessEvents(fieldList, xDoc);
        //}

        //private string ProcessEvents(List<Metadata> fieldList, XDocument xDoc)
        //{
        //    string line = string.Empty;

        //    foreach (var field in fieldList)
        //    {
        //        line = line + field.FieldName + ",";
        //    }

        //    line = line.Substring(0, line.Length - 1);
        //    line = line + Environment.NewLine;

        //    foreach (var item in xDoc.Descendants("item"))
        //    {
        //        foreach (var field in fieldList)
        //        {
        //            //if(field.FieldChoices.Count > 0)
        //            //{
        //            //    foreach (var fieldChoice in field.FieldChoices)
        //            //    {
        //            //        // if field.FieldName exists...it's good to go, just one possible value
        //            //        // if field.FieldName doesn't exist...we need to see which of the possible choices have a true value
        //            //        int y = 10;
        //            //    }
        //            //}
        //            if (field.FieldType != "checkbox")
        //            {
        //                line = line + item.Element(field.FieldName).GetValue() + ",";
        //            }
        //            else
        //            {
        //                line = line + ",";
        //            }
        //        }

        //        line = line.Substring(0, line.Length - 1);
        //        line = line + Environment.NewLine;
        //    }

        //    Console.WriteLine(line);
        //    return line;
        //}

        //private async Task ProcessForm(string form)
        //{

        //    try
        //    {
        //        var formDoc = await this._redCapClient.GetFormDataAsXmlAsync(form);
        //        formDoc.Save("output\\" + form + "_form_data.xml");

        //        var sqlCmd = this._sqlConvertor.GenerateCreateFormTableSQL(form, formDoc);
        //        File.WriteAllText("output\\" + form + "_create_table.sql", sqlCmd);
        //        //await ExecuteSQL(sqlCmd);

        //        var insertCmd = this._sqlConvertor.GenerateInsertFormDataSQL(form, formDoc);
        //        File.WriteAllText("output\\" + form + "_insert_data.sql", insertCmd);
        //        // await ExecuteSQL(insertCmd);

        //        Console.WriteLine("Processed : " + form);

        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine("ERROR: Cannot Process form:" + form);
        //        Console.WriteLine(ex.Message);
        //    }


        //}

        //private async Task ExecuteSQL(string sqlCmd)
        //{
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