using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Service.Core.Entities
{
    public class CachedDoctor
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Speciality { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    }
}
