using AutoMapper;
using Azure.Core;
using MedicalRecords.Service.Core;
using MedicalRecords.Service.Core.DbContexts;
using MedicalRecords.Service.Core.Dtos;
using MedicalRecords.Service.Core.Entities;
using MedicalRecords.Service.Core.Helper;
using MedicalRecords.Service.Core.ServicesContract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MedicalRecords.Service.Services.Services
{



    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly MedicalRecordsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<MedicalRecord> _logger;

        public MedicalRecordService(MedicalRecordsDbContext dbContext ,IMapper mapper  , ILogger<MedicalRecord> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MedicalRecordDto> CreateMedicalRecordAsync(MedicalRecordDto medicalRecordDto)
        {
            var IsPatientExist = _dbContext.Patients.Any(paitent => paitent.Id == medicalRecordDto.PatientId);

            if (!IsPatientExist )
                return null;

            var result = _mapper.Map<MedicalRecord>(medicalRecordDto);

            if (result is not null)
            {
                _dbContext.Add(result);
                await _dbContext.SaveChangesAsync();
            }

            medicalRecordDto.Id = result.Id;

            return medicalRecordDto;
        }

        public async Task<PaginationData> GetAllMedicalRecordForPatientByIdAsync(Guid id , PaginationRequest paginationRequest)
        {
            var query = _dbContext.MedicalRecords
                .Include(prop => prop.Observations)
                .Include(prop => prop.Conditions)
                .Include(prop => prop.Medications)
                .OrderByDescending(prop => prop.CreatedAt)
                .Where(p => p.PatientId == id);

            int totalRecords = await query.CountAsync();

            double last = totalRecords * 1.0 / paginationRequest.PageSize;

            int lastPage = Convert.ToInt32(Math.Ceiling(last));


            var records = await query.
                 Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .Select(m => new MedicalRecordDto
                {
                    Id = m.Id,
                    Diagnosis = m.Diagnosis,
                    Notes = m.Notes,
                    CreatedAt = m.CreatedAt,
                    PatientId = m.PatientId,
                    Medications = m.Medications.Select(med => new MedicationDto
                    {
                        Name = med.Name,
                        Dosage = med.Dosage,
                        Frequency = med.Frequency,
                        DurationInDays = med.DurationInDays
                    }).ToList(),
                    Conditions = m.Conditions.Select(c => new ConditionDto
                    {
                        Code = c.Code,
                        Description = c.Description,
                        CreatedAt = c.CreatedAt
                    }).ToList(),
                    Observations = m.Observations.Select(o => new ObservationDto
                    {
                        TestName = o.TestName,
                        Value = o.Value,
                        Unit = o.Unit,
                        CreatedAt = o.CreatedAt
                    }).ToList()
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

            return new PaginationData
            {
                Items = records,
                Meta = meta
            };
        }

        public async Task<PaginationData> GetAllMedicalRecordsAsync(PaginationRequest paginationRequest)
        {
            var query = _dbContext.MedicalRecords
                .Include(prop => prop.Observations)
                .Include(prop => prop.Conditions)
                .Include(prop => prop.Medications)
                .OrderByDescending(prop => prop.CreatedAt);

            int totalRecords = await query.CountAsync();

            double last = totalRecords * 1.0 / paginationRequest.PageSize;

            int lastPage = Convert.ToInt32( Math.Ceiling( last ) );


            var records = await query.
                 Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .Select(m => new MedicalRecordDto
                {
                     Id = m.Id,
                     Diagnosis = m.Diagnosis,
                     Notes = m.Notes,
                     CreatedAt = m.CreatedAt,
                     PatientId = m.PatientId,
                     Medications = m.Medications.Select(med => new MedicationDto
                     {
                         Name = med.Name,
                         Dosage = med.Dosage,
                         Frequency = med.Frequency,
                         DurationInDays = med.DurationInDays
                     }).ToList(),
                     Conditions = m.Conditions.Select(c => new ConditionDto
                     {
                         Code = c.Code,
                         Description = c.Description,
                         CreatedAt = c.CreatedAt
                     }).ToList(),
                     Observations = m.Observations.Select(o => new ObservationDto
                     {
                         TestName = o.TestName,
                         Value = o.Value,
                         Unit = o.Unit,
                         CreatedAt = o.CreatedAt
                     }).ToList()
                 })
            .ToListAsync();

            if(records.Count == 0 && totalRecords > 0)
                return null;


            var meta = new PaginationResponseWithData
            {
                CurrentPage = paginationRequest.PageNumber,
                PerPage = paginationRequest.PageSize,
                LastPage = lastPage ,
                Total = totalRecords
            };

            return new PaginationData
            {
                Items = records,
                Meta = meta
            };
        }

        public async Task<MedicalRecordDto> GetMedicalRecordByIdAsync(Guid id)
        {
            MedicalRecord medicalRecord = await _dbContext.MedicalRecords
                                                .Include(prop => prop.Observations)
                                                .Include(prop => prop.Conditions)
                                                .Include(prop => prop.Medications)
                                                .FirstOrDefaultAsync(m => m.Id == id);


            if (medicalRecord is null)
                return null;

            MedicalRecordDto medicalRecordDto = _mapper.Map<MedicalRecordDto>(medicalRecord);

            return medicalRecordDto;
        }

        

        public async Task<UpdateMedicalRecordDto> UpdateMedicalRecordAsync(UpdateMedicalRecordDto medicalRecordsDto)
        {
            MedicalRecord medicalRecord = await _dbContext.MedicalRecords.FindAsync(medicalRecordsDto.Id);

            var IsPatientExist = _dbContext.Patients.Any(paitent => paitent.Id == medicalRecordsDto.PatientId);
            var IsDoctorExist = _dbContext.CachedDoctors.Any(doctor => doctor.Id == medicalRecordsDto.CachedDoctorId);

            if (medicalRecord is null || !IsPatientExist || !IsDoctorExist)
                return null;

            medicalRecord.Diagnosis = medicalRecordsDto.Diagnosis;
            medicalRecord.Notes = medicalRecordsDto.Notes;
            medicalRecord.PatientId = medicalRecordsDto.PatientId;

            await _dbContext.SaveChangesAsync();

            return medicalRecordsDto;
        }




        public async Task<CachedDoctor> HandleUserCreatedEventAsync(KafkaUserEvent userEvent)
        {
            _logger.LogInformation("Handling doctor created event at {time}", DateTime.UtcNow);


            var doctor = new CachedDoctor
            {
                Id = Guid.Parse(userEvent.Body.Id),
                Name = userEvent.Body.FirstName + " " + userEvent.Body.LastName,
            };

            if (userEvent.Body.Role == "Doctor" || userEvent.Body.Role == "DOCTOR")
            {
                try
                {
                    var result = await _dbContext.CachedDoctors
                        .AddAsync(doctor);

                    await _dbContext.SaveChangesAsync();
                 
                    _logger.LogInformation("Doctor created successfully at {time}", DateTime.UtcNow);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Doctor Creation Failed at {time}", DateTime.UtcNow);
                    _logger.LogError(ex, "Error creating Doctor: {message}", ex.Message);
                }
            }

            return doctor;

        }

        public async Task<bool> HandleUserDeletedAsync(KafkaUserEvent userEvent)
        {
            _logger.LogInformation("Handling doctor deleing event at {time}", DateTime.UtcNow);

            var doctor = await _dbContext.CachedDoctors.FindAsync(Guid.Parse(userEvent.Body.Id));
            if (doctor == null)
            {
                _logger.LogWarning("doctor with ID {id} not found", userEvent.Body.Id);
                return false;
            }

            try
            {
                _dbContext.CachedDoctors.Remove(doctor);

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("User Deleted successfully at {time}", DateTime.UtcNow);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("User Deleted Failed at {time}", DateTime.UtcNow);
                _logger.LogError(ex, "Error Deleting user: {message}", ex.Message);
            }

            return true;
        }

        public async Task<CachedDoctor> HandleUserUpdatedAsync(KafkaUserEvent userEvent)
        {
            _logger.LogInformation("Handling Doctor Updating event at {time}", DateTime.UtcNow);

            var doctor = await _dbContext.CachedDoctors.FindAsync(Guid.Parse(userEvent.Body.Id));
            if (doctor == null)
            {
                _logger.LogWarning("Doctor with ID {id} not found", userEvent.Body.Id);
                return null;
            }

            doctor.Name = userEvent.Body.FirstName + " " + userEvent.Body.LastName;
            

            try
            {
                await _dbContext.SaveChangesAsync();


                _logger.LogInformation("User Updated successfully at {time}", DateTime.UtcNow);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("User Updated Failed at {time}", DateTime.UtcNow);
                _logger.LogError(ex, "Error Updating user: {message}", ex.Message);
            }

            return doctor;
        }

    }
}
