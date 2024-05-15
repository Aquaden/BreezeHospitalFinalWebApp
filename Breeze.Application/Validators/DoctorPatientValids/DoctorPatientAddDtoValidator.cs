using Breeze.Application.DTOs.DoctorPatientDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Validators.DoctorPatientValids
{
    public class DoctorPatientAddDtoValidator : AbstractValidator<DoctorPatientAddDto>
    {
        public DoctorPatientAddDtoValidator()
        {
            RuleFor(dto => dto.DoctorId).GreaterThan(0).WithMessage("DoctorId must be greater than 0");
            RuleFor(dto => dto.PatientId).GreaterThan(0).WithMessage("PatientId must be greater than 0");
        }
    }
}
