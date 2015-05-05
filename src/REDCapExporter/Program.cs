using System;

namespace REDCapExporter
{
    class Program
    {
        static void Main(string[] args)
        {
            //var apiUrl = args[0];
            //var token = args[1]; 
            
            //----DEMO ENVIRONMENT----
            //var apiUrl = "https://redcap.wustl.edu/redcap/srvrs/dev_v3_1_0_001/redcap/api/";
            //var token = "820E65DD2D930A0859BB3F727989D29E"; // Ryan's Sample 2
            //var token = "065C7F9FE54C3D2793304136DD7991B3"; // Ryan's Sample 1
            //----

            //----LIVE ENVIRONMENT----
            var apiUrl = "https://redcap.wustl.edu/redcap/srvrs/prod_v3_1_0_001/redcap/api/";
            var token = "DEC10B186D64B4B5577D333055B85AE8"; // Fructose study
            //----

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