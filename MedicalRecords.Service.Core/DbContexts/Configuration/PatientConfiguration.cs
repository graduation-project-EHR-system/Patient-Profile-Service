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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(patient => patient.Id);

            builder.Property(prop => prop.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder.Property(prop => prop.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder.Property(prop => prop.NationalID)
                .IsRequired()
                .HasMaxLength(14)
                .HasColumnType("char(14)")
                .HasAnnotation("RegularExpression", "^[0-9]{14}$");

            builder.HasIndex(prop => prop.NationalID)
                .IsUnique();

            builder.Property(prop => prop.Age)
                .IsRequired();

            builder.Property(prop => prop.Address)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder.Property(prop => prop.NationalID)
                .IsRequired()
                .HasMaxLength(14)
                .HasColumnType("char(14)")
                .HasAnnotation("RegularExpression", "^[0-9]{14}$");


            builder.Property(prop => prop.Email)
                .IsRequired()
                .HasAnnotation("RegularExpression", @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            builder.Property(prop => prop.BloodType)
                .HasConversion<string>()
                .HasColumnType("nvarchar(10)");

            builder.Property(prop => prop.MaritalStatus)
                .HasConversion<string>()
                .HasColumnType("nvarchar(10)");

            builder.Property(prop => prop.Gender)
                .HasConversion<string>()
                .HasColumnType("nvarchar(5)");

            builder.Property(prop => prop.DateOfBirth)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(prop => prop.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();


        }
    }
}
