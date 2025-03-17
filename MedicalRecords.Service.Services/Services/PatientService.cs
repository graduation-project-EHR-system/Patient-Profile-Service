using AutoMapper;
using MedicalRecords.Service.Core.DbContexts;
using MedicalRecords.Service.Core.Dtos;
using MedicalRecords.Service.Core.Entities;
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
    }
}
