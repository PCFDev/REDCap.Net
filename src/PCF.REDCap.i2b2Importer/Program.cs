using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Extensions.Xml.Extensions;
using Ninject;
using Ninject.Extensions.Xml;

namespace PCF.REDCap.i2b2Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new NinjectSettings { LoadExtensions = false };
            var kernel = new StandardKernel(settings, new XmlExtensionModule());
            kernel.Load("NinjectConfig.xml");





            Console.ReadLine();
        }
    }

}
