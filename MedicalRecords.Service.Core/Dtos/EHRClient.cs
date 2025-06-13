using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Service.Core.Dtos
{
    public class EHRClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public EHRClient(HttpClient httpClient , IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<bool> SendMedicalRecordAsync(MedicalRecordWithHospitalDto record)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["Links:EHRPost"], record);
            return response.IsSuccessStatusCode;
        }
    }

}
