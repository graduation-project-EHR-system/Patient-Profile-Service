using MedicalRecords.Service.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Service.Core.DbContexts
{
    public class MedicalRecordsDbContext : DbContext
    {
        public MedicalRecordsDbContext(DbContextOptions<MedicalRecordsDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<CachedDoctor> CachedDoctors { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Observation> Observations { get; set; }
        public DbSet<Condition> Conditions { get; set; }
    }
}
