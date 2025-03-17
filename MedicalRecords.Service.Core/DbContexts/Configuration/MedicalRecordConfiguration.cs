using MedicalRecords.Service.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Service.Core.DbContexts.Configuration
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.ToTable("MedicalRecords");

           
            builder.HasKey(mr => mr.Id);

            
            builder.Property(mr => mr.Diagnosis)
                .IsRequired()
                .HasMaxLength(500);

            
            builder.Property(mr => mr.Notes)
                .HasMaxLength(1000);

            
            builder.Property(mr => mr.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

        }
    }
}
