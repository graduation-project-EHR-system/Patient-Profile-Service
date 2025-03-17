using System.ComponentModel.DataAnnotations;

namespace MedicalRecords.Service.Core.Dtos
{
    public class ConditionDto
    {
        [Required(ErrorMessage = "Condition code is required.")]
        [StringLength(10, ErrorMessage = "Condition code must not exceed 10 characters.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required(ErrorMessage = "Medical medicalRecord Id Is Required.")]

        public Guid MedicalRecordId { get; set; }

    }

}