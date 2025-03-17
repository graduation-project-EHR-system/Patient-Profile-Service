namespace MedicalRecords.Service.Api.Response
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IReadOnlyList<string> Errors { get; set; }
        public ApiValidationErrorResponse():base(400)
        {
            Errors = new List<string>();
        }
    }
}
