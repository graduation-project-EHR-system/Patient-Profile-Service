using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Service.Core.Entities
{
    public class MedicalRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Diagnosis { get; set; }
        public string Notes { get; set; }


        // Date
        public DateTime CreatedAt { get; private set; }
        
        

        //Relations
        public Patient Patient { get; set; }
        public Guid PatientId { get; set; }
        public CachedDoctor CachedDoctor { get; set; }
        public Guid CachedDoctorId { get; set; }

        public ICollection<Medication> Medications { get; set; } = new List<Medication>();
        public ICollection<Condition> Conditions { get; set; } = new List<Condition>();
        public ICollection<Observation> Observations { get; set; } = new List<Observation>();

    }
}
