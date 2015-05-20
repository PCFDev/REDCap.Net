using System;
using System.Data.Entity;
using PCF.REDCap.Web.Data.API.DB;

namespace PCF.REDCap.Web.Data.DB
{
    public class RepositoryContext : DbContext
    {
        private static void LogIt(string message)
        {
            System.Diagnostics.Debug.Write(message, "EFSQL");
        }

        public RepositoryContext()
            : this(true)
        {
        }

        public RepositoryContext(bool isReadWrite)
            : base("DbConnection")
        {
            if (System.Diagnostics.Debugger.IsAttached)
                this.Database.Log = LogIt;

            if (isReadWrite)
            {
                this.Configuration.ProxyCreationEnabled = true;
                this.Configuration.ValidateOnSaveEnabled = true;
            }
            else
            {
                this.Configuration.ProxyCreationEnabled = true; // ugh
                this.Configuration.AutoDetectChangesEnabled = false;
                this.Configuration.LazyLoadingEnabled = true; // ugh
                this.Configuration.ValidateOnSaveEnabled = false;   // no saving here!
            }
        }

        //DbSet interfaces or implementations?
        public DbSet<Config> Configs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            DbModel.BuildModel(modelBuilder);

            Config.BuildModel(modelBuilder);
        }
    }
}
