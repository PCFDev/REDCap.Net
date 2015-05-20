using System;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;

namespace PCF.REDCap.Web
{
    // This class is the API resolver, but it is also the global scope
    // so we derive from NinjectScope.
    public class NinjectApiDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectApiDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            if (kernel == null)
                throw new ArgumentNullException("kernel");
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

    public class NinjectMvcDependencyResolver : NinjectDependencyScope, System.Web.Mvc.IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectMvcDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            _kernel = kernel;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

    // Provides a Ninject implementation of IDependencyScope
    // which resolves services using the Ninject container.
    public class NinjectDependencyScope : IDependencyScope
    {
        protected IResolutionRoot resolutionRoot;

        public NinjectDependencyScope(IResolutionRoot kernel)
        {
            if (kernel == null)
                throw new ArgumentNullException("kernel");
            this.resolutionRoot = kernel;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public object GetService(Type serviceType)
        {
            if (resolutionRoot == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return resolutionRoot.TryGet(serviceType);
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
        {
            if (resolutionRoot == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return resolutionRoot.GetAll(serviceType);
        }

        protected virtual void Dispose(bool disposing)
        {
            // nothing to actually dispose
        }
    }
}