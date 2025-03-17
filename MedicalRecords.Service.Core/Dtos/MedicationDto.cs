using System.ComponentModel.DataAnnotations;

namespace MedicalRecords.Service.Core.Dtos
{
    public class MedicationDto
    {
        [Required(ErrorMessage = "Medication name is required.")]
        [StringLength(255, ErrorMessage = "Medication name must not exceed 255 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Dosage is required.")]
        [StringLength(50, ErrorMessage = "Dosage must not exceed 50 characters.")]
        public string Dosage { get; set; }

        [Required(ErrorMessage = "Frequency is required.")]
        [StringLength(50, ErrorMessage = "Frequency must not exceed 50 characters.")]
        public string Frequency { get; set; }

        [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365 days.")]
        public int DurationInDays { get; set; }

        [Required(ErrorMessage = "Medical medicalRecord Id Is Required.")]
        public Guid MedicalRecordId { get; set; }

    }
}