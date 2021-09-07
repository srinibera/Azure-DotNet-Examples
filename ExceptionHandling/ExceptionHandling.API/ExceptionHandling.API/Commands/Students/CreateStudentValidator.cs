using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using System.Threading.Tasks;

namespace ExceptionHandling.API.Commands.Students
{
    public class CreateStudentValidator:AbstractValidator<CreateStudent>
    {
        public CreateStudentValidator()
        {
            RuleFor(p => p.FirstName)
               .NotEmpty().WithMessage("First Name is required.")
               .NotNull()
               .MaximumLength(250).WithMessage("First Name must not exceed 250 characters.");

            RuleFor(p => p.LastName)
               .NotEmpty().WithMessage("Last Name is required.")
               .NotNull()
               .MaximumLength(250).WithMessage("Last Name must not exceed 250 characters.");
        }
    }
}
