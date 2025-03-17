namespace MedicalRecords.Service.Core.Entities
{
    public class Condition
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        

        public string Code { get; set; } // ICD-10 Code
        public string Description { get; set; }

        // date
        public DateTime CreatedAt { get; set; }


        // Relation
        public Guid MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
    }
}