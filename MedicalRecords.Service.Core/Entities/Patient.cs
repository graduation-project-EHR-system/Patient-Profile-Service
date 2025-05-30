﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Service.Core.Entities
{
    public class Patient
    {
        public Patient()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalID { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }



        // Enums

        public BloodType BloodType { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public GenderOption Gender { get; set; }


        // Dates

        public DateTime DateOfBirth { get; set;}
        public DateTime CreatedAt { get; private set; }


        // Relations

        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
        
    }
}
