using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REDCapWeb
{
    public class UserStudyContext : DbContext
    {
        public UserStudyContext() : base ("name=DefaultConnection")
        {

        }

        public DbSet<UserStudy> UserStudies {get;set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserStudy>().HasKey(p => p.Id);
        }
    }
}