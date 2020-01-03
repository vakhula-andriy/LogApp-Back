using FluentValidation;
using LogApp.Core.Models;

namespace LogApp.Core.Validators
{
    public class RecordValidator : AbstractValidator<Record>
    {
        public RecordValidator()
        {
            RuleFor(record => record.FirstName)
                .NotNull()
                .MaximumLength(80)
                .Matches(@"^\w+[ \w-]*$")
                .WithMessage("Invalid user firstname");

            RuleFor(record => record.LastName)
                .NotNull()
                .MaximumLength(80)
                .Matches(@"^\w+[ \w-]*$")
                .WithMessage("Invalid user firstname");

            RuleFor(record => record.Email)
                .NotNull()
                .Matches(@"^\w+@\w+\.\w+$")
                .WithMessage("Invalid e-mail");

            RuleFor(record => record.Age)
                .NotNull()
                .LessThan(100)
                .GreaterThan(16)
                .WithMessage("Invalid user age");
        }
    }
}
