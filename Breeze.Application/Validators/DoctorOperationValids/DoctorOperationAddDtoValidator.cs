using Breeze.Application.DTOs.DoctorOperationsDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.Validators.DoctorOperationValids
{
    public class DoctorOperationAddDtoValidator : AbstractValidator<DoctorOperationAddDto>
    {
        public DoctorOperationAddDtoValidator()
        {
            RuleFor(dto => dto.DoctorId).GreaterThan(0).WithMessage("DoctorId must be greater than 0");
            RuleFor(dto => dto.OperationId).GreaterThan(0).WithMessage("OperationId must be greater than 0");
        }
    }
}
