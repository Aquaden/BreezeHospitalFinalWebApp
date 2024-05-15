using Breeze.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Validators.DoctorValids
{
    public  class DoctorsDtoValidator : AbstractValidator<DoctorsDto>
    {
        public DoctorsDtoValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(dto => dto.SurName).NotEmpty().WithMessage("Surname is required");
            RuleFor(dto => dto.Specialization).NotEmpty().WithMessage("Specialization is required");
            RuleFor(dto => dto.ExperienceYears).GreaterThan(0).WithMessage("Experience years must be greater than 0");
            RuleFor(dto => dto.Email).NotEmpty().WithMessage("Email is required")
                                      .EmailAddress().WithMessage("Invalid email address");
        }
    }
}
