using Camillo.Domain;
using System.Data.Entity;

namespace Camillo.DataModel
{
    public class CamilloContext : DbContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Staff>().HasKey(s => s.AccountId).HasRequired(t => t.Account);
            modelBuilder.Entity<Patient>().HasKey(s => s.AccountId).HasRequired(t => t.Account);
         
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Bill> Bills { get; set; }
    }
}
