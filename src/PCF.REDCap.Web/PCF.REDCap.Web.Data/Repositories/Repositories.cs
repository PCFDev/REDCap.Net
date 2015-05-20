using System;
using Ninject;
using PCF.REDCap.Web.Data.API.Repositories;

namespace PCF.REDCap.Web.Data.Repositories
{
    public class Repositories : IRepositories
    {
        public static Repositories Instance = new Repositories();

        private Repositories()
        {
        }

        public IConfigRepository Config
        {
            get
            {
                return Kernel.Get<IConfigRepository>();
            }
        }

        private static IKernel Kernel { get; set; }

        public void Inject(IKernel kernel)
        {
            Kernel = kernel;
        }
    }
}
