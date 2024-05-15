using AutoMapper;
using Breeze.Application.DTOs;
using Breeze.Application.DTOs.DoctorOperationsDtos;
using Breeze.Application.DTOs.DoctorPatientDtos;
using Breeze.Application.DTOs.IdentityDtos;
using Breeze.Domain.Entities;
using Breeze.Domain.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Profiles
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<Doctors, DoctorsDto>();
            CreateMap<DoctorsDto, Doctors>().ForMember(x => x.Id , opt => opt.Ignore());
            CreateMap<Patients, PatientsDto>();
            CreateMap<PatientsDto, Patients>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Analyses, AnalysDto>().ReverseMap();//Analys ve operation-u reverseMaple verdim!
            CreateMap<Operations, OperationDto>().ReverseMap();
            CreateMap<DoctorPatients, DoctorPatientDto>().ReverseMap();
            CreateMap<DoctorPatientAddDto, DoctorPatients>().ReverseMap();
            CreateMap<DoctorOperations, DoctorOperationDto>().ReverseMap();
            CreateMap<DoctorOperationAddDto, DoctorOperations>().ReverseMap();
            CreateMap<AppUser, GetUserDto>().ReverseMap();
        }
    }
}
