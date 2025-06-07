using MedicalRecords.Service.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Service.Core.Helper
{
    public class PaginationPatients
    {
        public PaginationResponseWithData Meta { get; set; }
        public IEnumerable<PatientDto> Items { get; set; } = new List<PatientDto>();
    }
}
