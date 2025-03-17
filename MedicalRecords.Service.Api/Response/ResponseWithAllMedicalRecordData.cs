using MedicalRecords.Service.Core.Dtos;
using MedicalRecords.Service.Core.Helper;

namespace MedicalRecords.Service.Api.Response
{
    public class ResponseWithAllMedicalRecordData : ApiResponse
    {
        public PaginationResponse Data { get; set; }
        public ResponseWithAllMedicalRecordData(int statusCode, PaginationResponse data, string? message = null) : base(statusCode, message)
        {
            Data = data;
        }
    }
}
