using Breeze.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Validators.PatientValids
{
    public class PatientsDtoValidator : AbstractValidator<PatientsDto>
    {
        public PatientsDtoValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(dto => dto.SurName).NotEmpty().WithMessage("Surname is required");
            RuleFor(dto => dto.DateOfBirth).NotEmpty().WithMessage("Date of birth is required")
                                             .Must(BeAValidDate).WithMessage("Date of birth must be a valid date");
            RuleFor(dto => dto.Gender).NotEmpty().WithMessage("Gender is required");
            RuleFor(dto => dto.Number).NotEmpty().WithMessage("Number is required");
            RuleFor(dto => dto.AdmissionDate).NotEmpty().WithMessage("Admission date is required")
                                              .Must(BeAValidDate).WithMessage("Admission date must be a valid date")
                                              .GreaterThanOrEqualTo(dto => dto.DateOfBirth).WithMessage("Admission date must be greater than or equal to date of birth");
            RuleFor(dto => dto.MedicalHistory).NotEmpty().WithMessage("Medical history is required");
        }

        private bool BeAValidDate(DateTime date)
        {
            // Additional validation for valid date can be added here
            return date != default(DateTime);
        }
    }
}
