using System.ComponentModel.DataAnnotations;

namespace MedicalRecords.Service.Core.Dtos
{
    public class ObservationDto
    {
        [Required(ErrorMessage = "Test name is required.")]
        [StringLength(255, ErrorMessage = "Test name must not exceed 255 characters.")]
        public string TestName { get; set; }

        [Required(ErrorMessage = "Value is required.")]
        [StringLength(100, ErrorMessage = "Value must not exceed 100 characters.")]
        public string Value { get; set; }

        [Required(ErrorMessage = "Unit is required.")]
        [StringLength(50, ErrorMessage = "Unit must not exceed 50 characters.")]
        public string Unit { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Medical medicalRecord Id Is Required.")]
        public Guid MedicalRecordId { get; set; }

    }
}