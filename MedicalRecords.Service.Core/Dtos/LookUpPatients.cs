using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Service.Core.Dtos
{
    public class LookUpPatients
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FullName { get; set; }

    }
}
