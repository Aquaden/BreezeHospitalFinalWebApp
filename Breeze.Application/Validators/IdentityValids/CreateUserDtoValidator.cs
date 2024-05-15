using Breeze.Application.DTOs.IdentityDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Validators.IdentityValids
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(u => u.UserName)
                .NotNull().NotEmpty().WithMessage("Username cannot be empty.")
                .Length(5, 20).WithMessage("Username must be between 5 and 20 characters.");

            RuleFor(u => u.Email)
                .NotNull().NotEmpty().WithMessage("Email address cannot be empty.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(u => u.Password)
                .NotNull().NotEmpty().WithMessage("Password cannot be empty.")
                .MinimumLength(6).MaximumLength(30).WithMessage("Password must be at least 6 characters long.");

            RuleFor(u => u.FirstName)
                .NotNull().NotEmpty().WithMessage("First name cannot be empty.")
                .Length(1, 20).WithMessage("{PropertyName} must be between 5 and 20 characters.");

            RuleFor(u => u.LastName)
                .NotNull().NotEmpty().WithMessage("Last name cannot be empty.")
                .Length(1, 20).WithMessage("{PropertyName} must be between 5 and 20 characters.");

            RuleFor(u => u.BirthDay)
                .NotNull().NotEmpty().WithMessage("Birth date cannot be empty.")
                .Must(BeAValidDate).WithMessage("Please enter a valid birth date.");
            
        }

        private bool BeAValidDate(DateTime date)
        {
            // Additional validation for valid date can be added here
            return date < DateTime.Now;
        }
    }
}
