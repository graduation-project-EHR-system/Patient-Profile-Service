using MedicalRecords.Service.Core.Dtos;
using MedicalRecords.Service.Core.Helper;

namespace MedicalRecords.Service.Core
{
    public class ResponseWithAllMedicalRecordData : ApiResponseCore
    {
        public PaginationData Data { get; set; }
        public ResponseWithAllMedicalRecordData(int statusCode, PaginationData data, string? message = null) : base(statusCode, message)
        {
            Data = data;
        }
    }
}
