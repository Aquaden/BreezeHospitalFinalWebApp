using Breeze.Application.DTOs.DoctorPatientDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Validators.DoctorPatientValids
{
    public class DoctorPatientDtoValidator : AbstractValidator<DoctorPatientDto>
    {
        public DoctorPatientDtoValidator()
        {
            RuleFor(dto => dto.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(dto => dto.DoctorId).GreaterThan(0).WithMessage("DoctorId must be greater than 0");
            RuleFor(dto => dto.PatientId).GreaterThan(0).WithMessage("PatientId must be greater than 0");
        }
    }
}
