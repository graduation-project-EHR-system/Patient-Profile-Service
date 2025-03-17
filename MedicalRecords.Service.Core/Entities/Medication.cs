using System.ComponentModel.DataAnnotations;

namespace MedicalRecords.Service.Core.Entities
{
    public class Medication
    {
        
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Dosage { get; set; }  

        public string Frequency { get; set; }
        public int DurationInDays { get; set; }

        public Guid MedicalRecordId { get; set; } 
        public MedicalRecord MedicalRecord { get; set; }
    }
}