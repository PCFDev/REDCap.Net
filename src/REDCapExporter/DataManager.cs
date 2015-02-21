using System;
using System.IO;
using System.Threading.Tasks;
using REDCapClient;

namespace REDCapExporter
{
    public class DataManager
    {
        private DataContext _context = new DataContext();
        private REDCapClient.REDCapClient _redCapClient;
        private REDCapClient.SqlConvertor _sqlConvertor = new REDCapClient.SqlConvertor();


        public async Task ProcessProject(string apiUrl, string token)
        {
            this._redCapClient = new REDCapClient.REDCapClient(apiUrl, token);

            //var allDataXml = await this._redCapClient.GetReportAsXmlAsync("419");
            //allDataXml.Save("output\\Patient Tracking Export.xml");

            var names = await this._redCapClient.GetFormNamesAsync();
            var events = await this._redCapClient.GetEventsAsync();
            var fieldNames = await this._redCapClient.GetMetadataAsync();
            var records = await this._redCapClient.GetRecordsAsync();

            foreach (var form in names)
            {
                await ProcessForm(form);
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
