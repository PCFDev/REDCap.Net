using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Extensions.Xml.Extensions;
using Ninject;
using Ninject.Extensions.Xml;
using PCF.OdmXml.i2b2Importer;

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
                    var importTask = Task.Run(async () =>
                    {
                        Console.WriteLine(String.Format("Starting extraction of {0}", project.Name));
                        var study = await client.GetStudyAsync(project);

                        Console.WriteLine(String.Format("Starting conversion of {0}", project.Name));

                        var odmStudy = await converter.ConvertAsync(study);

                        Console.WriteLine(String.Format("Starting import of {0}", project.Name));
                    //Todo this needs to be changed to ImportAsync to follow convention
                    await importer.Import(odmStudy);

                    });

                    studyTasks.Add(importTask);
                }


                Task.WaitAll(studyTasks.ToArray());

            }
            catch (AggregateException aggEx)
            {
                //Todo this is not how you error handle... you're doing it wrong!
                Console.WriteLine(aggEx.Flatten().Message);
            }
            catch (Exception ex)
            {

                throw;
            }

            Console.ReadLine();
        }
    }

}
