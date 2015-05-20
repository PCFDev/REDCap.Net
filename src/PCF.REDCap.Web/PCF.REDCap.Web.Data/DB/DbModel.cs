using System;
using System.Data.Entity;

namespace PCF.REDCap.Web.Data.DB
{
    public abstract class DbModel
    {
        public byte[] Timestamp { get; set; }

        #region Model Builder

        public static void BuildModel(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Types<DbModel>()
                .Configure(c => c.Property(_ => _.Timestamp).IsRowVersion());
        }

        #endregion Model Builder
    }
}
