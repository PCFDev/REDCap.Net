using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using PCF.REDCap.Web.Data.API.DB;

namespace PCF.REDCap.Web.Data.DB
{
    public class Config : DbModel, IConfig
    {
        public int Id { get; private set; }
        public bool Enabled { get; set; }
        public string Key { get; set; }
        [Index("UQ_Name", IsUnique = true)]
        public string Name { get; set; }
        public string Url { get; set; }

        public Uri Uri
        {
            get
            {
                return new Uri(Url, UriKind.Absolute);
            }
        }

        public static void BuildModel(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Config>()
                .HasKey(_ => _.Id)
                .Property(_ => _.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Config>()
                .Property(_ => _.Name)
                .HasMaxLength(255)
                .IsVariableLength()
                .IsRequired();

            modelBuilder.Entity<Config>()
                .Property(_ => _.Key)
                .HasMaxLength(32)//Example, need proper size, might be variable
                .IsFixedLength()
                .IsRequired();

            modelBuilder.Entity<Config>()
                .Property(_ => _.Url)
                .HasMaxLength(2048)
                .IsVariableLength()
                .IsRequired();
        }
    }
}
