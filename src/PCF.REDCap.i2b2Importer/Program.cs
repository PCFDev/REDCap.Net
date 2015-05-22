using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ninject;
using Ninject.Extensions.Xml;
using PCF.OdmXml.i2b2Importer;
using PCF.OdmXml;

namespace PCF.REDCap.i2b2Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var settings = new NinjectSettings { LoadExtensions = false };
                var kernel = new StandardKernel(settings, new XmlExtensionModule());
                kernel.Load("NinjectConfig.xml");

                var configProvider = kernel.Get<IConfigProvider>();

                var client = kernel.Get<IREDCapClient>();

                var converter = kernel.Get<IOdmXmlConverter>();

                var importer = kernel.Get<IOdmImporter>();

                var projects = configProvider.GetConfigurations();

                var studyTasks = new List<Task>();

                foreach (var project in projects)
                {

                    var study = new Study();
                    var odmStudy = new ODM();

                    Console.WriteLine(study.StudyName);

                    ////var importTask = 

                    Task.Run(async () =>
                    {
                        //Study study = new Study() { StudyName = "Placeholder" };

                        Console.WriteLine(String.Format("Starting extraction of {0}", project.Name));
                        study = await client.GetStudyAsync(project);

                        Console.WriteLine(String.Format("Starting conversion of {0}", project.Name));
                        odmStudy = await converter.ConvertAsync(study);

                        Console.WriteLine(String.Format("Starting import of {0}", project.Name));
                        await importer.ImportAsync(odmStudy, new Dictionary<string, string>());
                        Console.WriteLine(String.Format("Import of {0} completed", project.Name));

                    })
                    .ContinueWith(t =>
                    {
                        var aggEx = t.Exception;

                        if (aggEx != null)
                        {
                            Console.WriteLine(aggEx.Flatten().Message);
                            foreach (var ex in aggEx.InnerExceptions)
                            {
                                Console.WriteLine(ex.Message);
                                Console.WriteLine(ex.StackTrace);
                            }
                        }
                    });

                    ////studyTasks.Add(importTask);
                }


                //Task.WaitAll(studyTasks.ToArray());

            }
            catch (AggregateException aggEx)
            {
                //Todo this is not how you error handle... you're doing it wrong!
                Console.WriteLine(aggEx.Flatten().Message);
                foreach (var ex in aggEx.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }

}
