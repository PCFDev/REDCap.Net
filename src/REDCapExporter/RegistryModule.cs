using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCF.REDCap
{
    public class RegistryModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {

            this.Bind<IConfigController>().To<FileConfigController>();
            this.Bind<IStudyWriter>().To<TestStudyWriter>();
                   
        }
    }
}
