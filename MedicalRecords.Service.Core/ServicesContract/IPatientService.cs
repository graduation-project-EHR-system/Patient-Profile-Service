﻿using MedicalRecords.Service.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Service.Core.ServicesContract
{
    public interface IPatientService
    {
        Task<PatientDto> CreatePatientAsync(PatientDto patientdto);
        Task<PatientDto> GetPatientByIdAsync(Guid id);
        Task<PatientDto> UpdatePatientAsync(PatientDto patientdto);
    }
}
