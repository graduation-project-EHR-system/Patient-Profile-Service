using MedicalRecords.Service.Api.Response;
using MedicalRecords.Service.Core.Dtos;
using MedicalRecords.Service.Core.Helper;
using MedicalRecords.Service.Core.ServicesContract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalRecords.Service.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PatientDto>> CreatePatientAsync( [FromBody] PatientDto patientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseWithData<PatientDto>(400, patientDto, "Invalid Model"));
            
            var result =  await _patientService.CreatePatientAsync(patientDto);

            if(result is null)      
                return BadRequest(new ApiResponse(400, "Some Of These Information are Already Exist"));

            return Ok(new ResponseWithData<PatientDto>(200,result,"Patient Is Added Successfuly"));

        }

        [HttpGet("{id}")]
        [ApiKeyAuth]
        public async Task<ActionResult<PatientDto>> GetPatientAsync([FromRoute] Guid id)
        {
            if(id == Guid.Empty)
                return BadRequest(new ApiResponse(400, "Invalid ID"));

            var result = await _patientService.GetPatientByIdAsync(id);

            if (result is null)
                return NotFound(new ApiResponse(400, "Patient With this ID Is Not Exist"));

            return Ok(new ResponseWithData<PatientDto>(200, result, "Patient Is Retriverd Successfuly"));

        }

        [HttpPut]
        [Authorize(Roles = "Doctor")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult<PatientDto>> UpdatePatientAsync([FromBody] PatientDto patientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseWithData<PatientDto>(400, patientDto, "Invalid Model"));

            if (patientDto.Id == Guid.Empty)
                return BadRequest(new ApiResponse(400, "Invalid ID"));

            var result = await _patientService.UpdatePatientAsync(patientDto);

            if (result is null)
                return BadRequest(new ApiResponse(400, "Some Of These Information are Already Exist"));

            return Ok(new ResponseWithData<PatientDto>(200, result, "Patient Is Updated Successfuly"));

        }

        [HttpGet("get-all-patients")]
        public async Task<ActionResult<PaginationPatients>> GetAllPatientAsync([FromBody] PaginationRequest paginationRequest)
        {

            var patientDtos = await _patientService.GetAllPatientAsync(paginationRequest);

            if (patientDtos is null)
                return NotFound(new ApiResponse(404, "There is no Patients yet!"));

            return Ok(new ResponseWithAllPatients(200, patientDtos, "Patient Is Retriverd Successfuly"));

        }

    }
}
