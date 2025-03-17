namespace MedicalRecords.Service.Core.Entities
{
    public class Observation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TestName { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }


        // date
        public DateTime CreatedAt { get; set; }
        

        // Relations
        public Guid MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
    }
}