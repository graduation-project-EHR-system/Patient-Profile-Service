using MedicalRecords.Service.Core.Dtos;
using MedicalRecords.Service.Core.Helper;

namespace MedicalRecords.Service.Core.Helper
{
    public class PaginationData
    {
        public PaginationResponseWithData Meta { get; set; }
        public IEnumerable<MedicalRecordDto> Items { get; set; } = new List<MedicalRecordDto>();
        
    }
}
