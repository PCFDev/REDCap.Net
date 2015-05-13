using System;
using Ninject;

namespace PCF.REDCap
{
    class Program
    {
        static void Main(string[] args)
        {
         
            var kernal = new StandardKernel(new RegistryModule());

            // File Controller:
            var controller = kernal.Get<IConfigController>();                       
            var configs = controller.GetConfigurations();
            var studyWriter = kernal.Get<IStudyWriter>();

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