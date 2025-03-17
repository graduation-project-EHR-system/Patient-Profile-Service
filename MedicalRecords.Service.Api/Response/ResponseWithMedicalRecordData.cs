using MedicalRecords.Service.Core.Dtos;

namespace MedicalRecords.Service.Api.Response
{
    public class ResponseWithData<T> : ApiResponse
    {
        
        public T Data { get; set; }
        public ResponseWithData(int statusCode ,T data, string? message = null) : base(statusCode, message) 
        {
            Data = data;
        }
    }
}
