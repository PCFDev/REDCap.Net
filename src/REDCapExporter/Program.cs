
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace REDCapExporter
{
    class Program
    {
        static void Main(string[] args)
        {
            //var apiUrl = args[0];
            //var token = args[1]; 
            
            var apiUrl = "https://redcap.wustl.edu/redcap/srvrs/dev_v3_1_0_001/redcap/api/";
            var token = "820E65DD2D930A0859BB3F727989D29E"; // Ryan's Sample 2


            var manager = new DataManager();

            manager.ProcessProject(apiUrl, token).ContinueWith(t =>
            {
                if (t.Exception != null)
                    Console.WriteLine(t.Exception.Message);
                else
                    Console.WriteLine("Done");

            });

            Console.ReadLine();

        }
    }
}
