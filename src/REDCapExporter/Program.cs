using System;

namespace REDCapExporter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get Configuration
            // IConfigController controller = new TestConfigController();
            IConfigController controller = new FileConfigController();
            var configs = controller.GetConfigurations();

            IStudyWriter studyWriter = new TestStudyWriter();

            foreach (var item in configs)
            {
                // Process the study
                var manager = new DataManager();

                manager.ProcessProject(item).ContinueWith(t =>
                {

                    if (t.Exception != null)
                        Console.WriteLine(t.Exception.Message);
                    else
                    {
                        // Output study data to something
                        studyWriter.Write("");
                        //studyWriter.Write(t.Result);

                        Console.WriteLine("Done");
                    }
                });
            }

            Console.ReadLine();
        }
    }
}