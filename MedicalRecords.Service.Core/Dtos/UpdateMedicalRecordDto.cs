using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Service.Core.Dtos
{
    public class UpdateMedicalRecordDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Diagnosis is required.")]
        [StringLength(500, ErrorMessage = "Diagnosis must not exceed 500 characters.")]
        public string Diagnosis { get; set; }

        [StringLength(1000, ErrorMessage = "Notes must not exceed 1000 characters.")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Patient ID is required.")]
        public Guid PatientId { get; set; }

        [Required(ErrorMessage = "Doctor ID is required.")]
        public Guid CachedDoctorId { get; set; }
    }
}
