using System;
using System.Data.Entity;
using System.Linq;
using Ninject;

namespace PCF.REDCap.Web.Data.Repositories.Store
{
    public abstract class DbStore<TEntity, TContext> : DbStore<TContext>
        where TEntity : class
        where TContext : DbContext
    {
        protected abstract IQueryable<TEntity> ReadOnlySet { get; }
        protected abstract DbSet<TEntity> ReadWriteSet { get; }
    }

    public abstract class DbStore<TContext>
        where TContext : DbContext
    {
        [Inject]
        public IKernel Kernel { get; set; }

        public TContext ReadOnlyContext
        {
            get
            {
                return Kernel.Get<TContext>("ReadOnly");
            }
        }

        public TContext ReadWriteContext
        {
            get
            {
                return Kernel.Get<TContext>("ReadWrite");
            }
        }

        protected int SaveTracked()
        {
            return ReadWriteContext.SaveChanges();
        }
    }
}
