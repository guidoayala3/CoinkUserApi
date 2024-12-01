using FluentValidation;
using UserRegistrationApi.Models;

namespace UserRegistrationApi.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters")
                .Matches(@"^[\p{L}\s]+$").WithMessage("Name must contain only letters and spaces");

            RuleFor(user => user.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .Matches(@"^\d+$").WithMessage("Phone must be numeric");

            RuleFor(user => user.Address)
                .NotEmpty().WithMessage("Address is required");

            RuleFor(user => user.Country)
                .NotEmpty().WithMessage("Country is required");

            RuleFor(user => user.Department)
                .NotEmpty().WithMessage("Department is required");

            RuleFor(user => user.Municipality)
                .NotEmpty().WithMessage("Municipality is required");
        }
    }
}
