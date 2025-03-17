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
    public class ConditionConfiguration : IEntityTypeConfiguration<Condition>
    {
        public void Configure(EntityTypeBuilder<Condition> builder)
        {
            builder.ToTable("Conditions");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(prop => prop.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(prop => prop.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd(); 
        }
    }
}
