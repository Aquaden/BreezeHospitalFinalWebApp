using Breeze.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Validators.OperationValids
{
    public class OperationDtoValidator : AbstractValidator<OperationDto>
    {
        public OperationDtoValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(dto => dto.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(dto => dto.PatientId).GreaterThan(0).WithMessage("PatientId must be greater than 0");
            RuleFor(dto => dto.Date).NotEmpty().WithMessage("Date is required")
                                     .Must(BeAValidDate).WithMessage("Date must be a valid date");
        }

        private bool BeAValidDate(DateTime date)
        {
            // Additional validation for valid date can be added here
            return date != default(DateTime);
        }
    }
}
