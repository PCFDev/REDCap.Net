using System;
using System.Data.Entity;
using REDCapClient;
using System.Linq;

namespace REDCapMVC.Models
{
    public class StudyContext : DbContext
    {
        public StudyContext()
            : base("Name=REDCapConnection")
        { }

        public DbSet<REDCapStudy> Studies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Event
            modelBuilder.Entity<Event>().HasKey<string>(p => p.UniqueEventName);

            // Form
            modelBuilder.Entity<Form>().HasKey<string>(p => p.FormLabel);

            // Metadata
            modelBuilder.Entity<Metadata>().HasKey<string>(p => p.FieldLabel);

            // Study
            modelBuilder.Entity<REDCapStudy>().HasKey<string>(p => p.ApiKey);
        }
    }
}