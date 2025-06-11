using AutoMapper;
using MedicalRecords.Service.Core.Dtos;
using MedicalRecords.Service.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Service.Core.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ObservationDto, Observation>()
                .ForMember(dest => dest.TestName, obt => obt.MapFrom(src => src.TestName))
                .ForMember(dest => dest.Value, obt => obt.MapFrom(src => src.Value))
                .ForMember(dest => dest.CreatedAt, obt => obt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.MedicalRecordId, obt => obt.MapFrom(src => src.MedicalRecordId))
                .ForMember(dest => dest.Unit, obt => obt.MapFrom(src => src.Unit))
                .ReverseMap();


            CreateMap<ConditionDto, Condition>()
                .ForMember(dest => dest.Description, obt => obt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatedAt, obt => obt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.MedicalRecordId, obt => obt.MapFrom(src => src.MedicalRecordId))
                .ForMember(dest => dest.Code, obt => obt.MapFrom(src => src.Code)).ReverseMap();

            CreateMap<MedicationDto, Medication>()
                .ForMember(dest => dest.Name, obt => obt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Dosage, obt => obt.MapFrom(src => src.Dosage))
                .ForMember(dest => dest.MedicalRecordId, obt => obt.MapFrom(src => src.MedicalRecordId))
                .ForMember(dest => dest.DurationInDays, obt => obt.MapFrom(src => src.DurationInDays))
                .ForMember(dest => dest.Frequency, obt => obt.MapFrom(src => src.Frequency))
                .ReverseMap();

            CreateMap<MedicalRecordDto, MedicalRecord>()
                .ForMember(dest => dest.PatientId, obt => obt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.CreatedAt, obt => obt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Conditions, obt => obt.MapFrom(src => src.Conditions))
                .ForMember(dest => dest.Medications, obt => obt.MapFrom(src => src.Medications))
                .ForMember(dest => dest.Observations, obt => obt.MapFrom(src => src.Observations))
                .ReverseMap();



            CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.Id, prop => prop.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, prop => prop.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, prop => prop.MapFrom(src => src.LastName))
                .ForMember(dest => dest.NationalID, prop => prop.MapFrom(src => src.NationalID))
                .ForMember(dest => dest.Age, prop => prop.MapFrom(src => src.Age))
                .ForMember(dest => dest.Address, prop => prop.MapFrom(src => src.Address))
                .ForMember(dest => dest.PhoneNumber, prop => prop.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, prop => prop.MapFrom(src => src.Email))
                .ForMember(dest => dest.BloodType, prop => prop.MapFrom(src => src.BloodType))
                .ForMember(dest => dest.MaritalStatus, prop => prop.MapFrom(src => src.MaritalStatus))
                .ForMember(dest => dest.Gender, prop => prop.MapFrom(src => src.Gender))
                .ForMember(dest => dest.DateOfBirth, prop => prop.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.CreatedAt, prop => prop.MapFrom(src => src.CreatedAt))
                .ReverseMap();



            CreateMap<PatientDto, LookUpPatients>()
                .ForMember(dest => dest.Id, prop => prop.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, prop => prop.MapFrom(src => src.FirstName + " " + src.LastName));
        }
    }
}
