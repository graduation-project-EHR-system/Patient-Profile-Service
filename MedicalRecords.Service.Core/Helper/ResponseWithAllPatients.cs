using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Service.Core.Helper
{
    public class ResponseWithAllPatients : ApiResponseCore
    {
        public PaginationPatients Data { get; set; }
        public ResponseWithAllPatients(int statusCode, PaginationPatients data, string? message = null) : base(statusCode, message)
        {
            Data = data;
        }
    }
}
