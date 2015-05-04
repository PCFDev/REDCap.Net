using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using REDCapClient;
using System.Linq;
using System.Collections.Generic;

namespace REDCapExporter
{
    public class DataManager
    {
        private DataContext _context = new DataContext();
        private REDCapClient.REDCapClient _redCapClient;
        private REDCapClient.SqlConvertor _sqlConvertor = new REDCapClient.SqlConvertor();
        private REDCapClient.REDCapStudy _study = new REDCapStudy();

        // FILE WRITING
        FileStream ostream;
        StreamWriter writer;
        TextWriter oldOut = Console.Out;
        // FILE WRITING

        public async Task<List<REDCapClient.FormMetadata>> GetStudyFormData(string apiUrl, string apiToken)
        {
            this._redCapClient = new REDCapClient.REDCapClient(apiUrl, apiToken);
            List<FormMetadata> results = new List<FormMetadata>();

            results = await this._redCapClient.GetFormMetadataAsync();

            return results.ToList();
            //this._study.Events = await this._redCapClient.GetEventsAsync();
            //this._study.Metadata = await this._redCapClient.GetMetadataAsync();

            //return this._study;
        }

        public async Task ProcessProject(string apiUrl, string token)
        {
            this._redCapClient = new REDCapClient.REDCapClient(apiUrl, token);
            var result = await this._redCapClient.TestRecords();

            //this._study.Arms = await this._redCapClient.GetArmsAsync();
            //this._study.Events = await this._redCapClient.GetEventsAsync();
            //this._study.Metadata = await this._redCapClient.GetMetadataAsync();

            //----HACK----
            //List<string> events = new List<string> { "month_1_arm_1", "month_2_arm_1", "month_3_arm_1" };
            //string[] formNames = { "demographics", "month_data" };
            //try
            //{
            //    ostream = new FileStream("./month_data.csv", FileMode.OpenOrCreate, FileAccess.Write);
            //    writer = new StreamWriter(ostream);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Cannot open text file for writing.");
            //    Console.WriteLine(ex.Message);

            //    return;
            //}

            //Console.SetOut(writer);

            //foreach (string item in events)
            //{    
            //    XDocument recordArray = await this._redCapClient.GetRecordsAsync(item, formNames);
            //    await ProcessRecord(recordArray, formNames);
            //}

            //Console.SetOut(oldOut);
            //writer.Close();
            //ostream.Close();
            //Console.WriteLine("Done");
            //----HACK----

            foreach (Event eventSub in this._study.Events)
            {
                foreach (Form formSub in eventSub.Forms)
                {
                    try
                    {
                        ostream = new FileStream("./" + eventSub.UniqueEventName + "_" + formSub.FormName + ".csv", FileMode.OpenOrCreate, FileAccess.Write);
                        writer = new StreamWriter(ostream);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Cannot open text file for writing.");
                        Console.WriteLine(ex.Message);

                        return;
                    }

                    Console.SetOut(writer);

                    //string[] formNames = { "demographics", "month_data" };
                    //XDocument recordArray = await this._redCapClient.GetRecordsAsync("month_1_arm_1", formNames);
                    //await ProcessRecord(recordArray, formNames);

                    XDocument records = await this._redCapClient.GetRecordsAsync(eventSub.UniqueEventName, formSub.FormName);
                    await ProcessRecord(records, formSub.FormName);

                    Console.SetOut(oldOut);
                    writer.Close();
                    ostream.Close();
                    Console.WriteLine("Done");
                }
            }
        }

        private async Task ProcessRecord(XDocument xDoc, string[] forms)
        {
            List<Metadata> fieldList = this._study.Metadata.Where(f => f.FormName == forms[0]).ToList();
            List<Metadata> secondList = this._study.Metadata.Where(f => f.FormName == forms[1]).ToList();

            fieldList.AddRange(secondList.ToList());

            ProcessEvents(fieldList, xDoc);
        }

        private async Task ProcessRecord(XDocument xDoc, string form)
        {
            List<Metadata> fieldList = this._study.Metadata.Where(f => f.FormName == form).ToList();


            ProcessEvents(fieldList, xDoc);
        }

        private string ProcessEvents(List<Metadata> fieldList, XDocument xDoc)
        {
            string line = string.Empty;

            foreach (var field in fieldList)
            {
                line = line + field.FieldName + ",";
            }

            line = line.Substring(0, line.Length - 1);
            line = line + Environment.NewLine;

            foreach (var item in xDoc.Descendants("item"))
            {
                foreach (var field in fieldList)
                {
                    //if(field.FieldChoices.Count > 0)
                    //{
                    //    foreach (var fieldChoice in field.FieldChoices)
                    //    {
                    //        // if field.FieldName exists...it's good to go, just one possible value
                    //        // if field.FieldName doesn't exist...we need to see which of the possible choices have a true value
                    //        int y = 10;
                    //    }
                    //}
                    if (field.FieldType != "checkbox")
                    {
                        line = line + item.Element(field.FieldName).GetValue() + ",";
                    }
                    else
                    {
                        line = line + ",";
                    }
                }

                line = line.Substring(0, line.Length - 1);
                line = line + Environment.NewLine;
            }

            Console.WriteLine(line);
            return line;
        }

        private async Task ProcessForm(string form)
        {

            try
            {
                var formDoc = await this._redCapClient.GetFormDataAsXmlAsync(form);
                formDoc.Save("output\\" + form + "_form_data.xml");

                var sqlCmd = this._sqlConvertor.GenerateCreateFormTableSQL(form, formDoc);
                File.WriteAllText("output\\" + form + "_create_table.sql", sqlCmd);
                //await ExecuteSQL(sqlCmd);

                var insertCmd = this._sqlConvertor.GenerateInsertFormDataSQL(form, formDoc);
                File.WriteAllText("output\\" + form + "_insert_data.sql", insertCmd);
                // await ExecuteSQL(insertCmd);

                Console.WriteLine("Processed : " + form);

            }
            catch (Exception ex)
            {

                Console.WriteLine("ERROR: Cannot Process form:" + form);
                Console.WriteLine(ex.Message);
            }


        }

        private async Task ExecuteSQL(string sqlCmd)
        {
            try
            {
                await _context.Database.ExecuteSqlCommandAsync(sqlCmd);
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
                Console.WriteLine("ERROR:");
                Console.WriteLine(ex.Message);

                Console.WriteLine(sqlCmd);
                Console.WriteLine("END ERROR:");
                Console.WriteLine("");
            }
        }
    }
}