using AutoMapper;
using MedicalRecords.Service.Core.DbContexts;
using MedicalRecords.Service.Core.Dtos;
using MedicalRecords.Service.Core.Entities;
using MedicalRecords.Service.Core.Helper;
using MedicalRecords.Service.Core.ServicesContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Service.Services.Services
{
    public class PatientService : IPatientService
    {
        private readonly MedicalRecordsDbContext _dbContext;
        private readonly IMapper _mapper;

        public PatientService(MedicalRecordsDbContext dbContext , IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PatientDto> CreatePatientAsync(PatientDto patientdto)
        {
            var IsEmailExist = await _dbContext.Patients.FirstOrDefaultAsync(patient => patient.Email == patientdto.Email);
            var IsNationalIDExist = await _dbContext.Patients.FirstOrDefaultAsync(patient => patient.NationalID == patientdto.NationalID);

            if (IsEmailExist is not null || IsNationalIDExist is not null)
                return null;

            Patient patient = _mapper.Map<Patient>(patientdto);
            
            await _dbContext.AddAsync(patient);
            await _dbContext.SaveChangesAsync();

            return patientdto;

        }

        

        public async Task<PatientDto> GetPatientByIdAsync(Guid id)
        {
            var IsPatientExist = await _dbContext.Patients.FirstOrDefaultAsync(patient => patient.Id == id);

            if (IsPatientExist is null )
                return null;

            PatientDto patient = _mapper.Map<PatientDto>(IsPatientExist);

            return patient;
        }

        public async Task<PatientDto> UpdatePatientAsync(PatientDto patientdto)
        {
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(patient => patient.Id == patientdto.Id);

            if (patient is null)
                return null;


            var IsNationalIDExist = await _dbContext.Patients.FirstOrDefaultAsync(p => p.NationalID == patientdto.NationalID &&
                                                                                  patientdto.NationalID != patient.NationalID);
            
            if (IsNationalIDExist is not null )
                return null;


            var IsEmailExist = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Email == patientdto.Email &&
                                                                                  patientdto.Email != patient.Email);

            if (IsEmailExist is not null)
                return null;

            _mapper.Map(patientdto,patient);

            await _dbContext.SaveChangesAsync();

            return patientdto;
        }

        public async Task<PaginationPatients> GetAllPatientAsync(PaginationRequest paginationRequest)
        {
          
            var query = _dbContext.Patients
                .OrderByDescending(prop => prop.CreatedAt);

            int totalRecords = await query.CountAsync();

            double last = totalRecords *1.0 / paginationRequest.PageSize;

            int lastPage = Convert.ToInt32(Math.Ceiling(last));


            var records = await query.
                 Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .Select(m => new PatientDto
                {
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Email = m.Email,
                    NationalID = m.NationalID,
                    PhoneNumber = m.PhoneNumber,
                    Address = m.Address,
                    Age = m.Age,
                    CreatedAt = m.CreatedAt,
                    BloodType = m.BloodType,    
                    DateOfBirth = m.DateOfBirth,
                    Gender = m.Gender,
                    Id = m.Id,
                    MaritalStatus = m.MaritalStatus
                })
            .ToListAsync();

            if (records.Count == 0 && totalRecords > 0)
                return null;


            var meta = new PaginationResponseWithData
            {
                CurrentPage = paginationRequest.PageNumber,
                PerPage = paginationRequest.PageSize,
                LastPage = lastPage,
                Total = totalRecords
            };

            return new PaginationPatients
            {
                Items = records,
                Meta = meta
            };

        }


        public async Task<IEnumerable<LookUpPatients>> GetAllPatientAsync()
        {

            var patients = _dbContext.Patients;

            var patientsDto = _mapper.Map<IEnumerable<PatientDto>>(patients);

            var lookUpPatient = _mapper.Map<IEnumerable<LookUpPatients>>(patientsDto);

            return lookUpPatient;
        }

    }
}
