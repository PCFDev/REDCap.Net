﻿using System;
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

        public async Task ProcessProject(string apiUrl, string token)
        {
            this._redCapClient = new REDCapClient.REDCapClient(apiUrl, token);
            
            //var allDataXml = await this._redCapClient.GetReportAsXmlAsync("419");
            //allDataXml.Save("output\\Patient Tracking Export.xml");

            this._study.Arms = await this._redCapClient.GetArmsAsync();
            this._study.Events = await this._redCapClient.GetEventsAsync();
            this._study.Metadata = await this._redCapClient.GetMetadataAsync();
            
            foreach (Event eventSub in this._study.Events)
            {
                foreach (Form formSub in eventSub.Forms)
                {
                    XDocument records = await this._redCapClient.GetRecordsAsync(eventSub.UniqueEventName, formSub.FormName);
                    ProcessRecord(records, formSub.FormName);
                }
            }            
        }

        private async Task ProcessRecord(XDocument xDoc, string form)
        {
            List<Metadata> test2 = this._study.Metadata.Where(f => f.FormName == form).ToList();
            string line = string.Empty;

            foreach (var field in test2)
            {
                line = line + field.FieldName + ",";
            }

            line = line.Substring(0, line.Length - 1);
            line = line + "\\r\\n";

            foreach (var item in xDoc.Descendants("item"))
            {
                foreach (var field in test2)
                {
                    var test = item.Element(field.FieldName).Value.ToString();
                    line = line + item.Element(field.FieldName).Value.ToString() + ",";
                }

                line = line.Substring(0, line.Length - 1);
                line = line + "\\r\\n";

                int x = 10;
            }
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
