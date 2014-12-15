
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
            var apiUrl = args[0];
            var token = args[1]; 
            
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
