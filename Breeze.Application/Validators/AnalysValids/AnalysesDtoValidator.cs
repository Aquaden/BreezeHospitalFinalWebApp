using Breeze.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Validators.AnalysValids
{
    public class AnalysesDtoValidator : AbstractValidator<AnalysDto>
    {
        public AnalysesDtoValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(dto => dto.Date).NotEmpty().WithMessage("Date is required")
                                     .Must(BeAValidDate).WithMessage("Date must be a valid date");
            RuleFor(dto => dto.Result).NotEmpty().WithMessage("Result is required");
            RuleFor(dto => dto.PatientId).GreaterThan(0).WithMessage("PatientId must be greater than 0");
        }

        private bool BeAValidDate(DateTime date)
        {
            // Additional validation for valid date can be added here
            return date != default(DateTime);
        }
    }
}
